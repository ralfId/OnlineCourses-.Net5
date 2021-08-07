using Application.Contracts;
using Application.CoursesFeatures.Commands;
using Application.CoursesFeatures.Queries;
using Domain.Models;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Persistence.DapperConfiguration;
using Persistence.Data;
using Persistence.Repository.IServices;
using Persistence.Repository.Services;
using Security.TokenSecurity;
using Security.UserSecurity;
using System.Text;
using WebApi.Middlewares;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //allow requests from any site using cors config
            services.AddCors(co => co.AddPolicy("corsApp", builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            }));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //sql connection
            services.AddDbContext<OnlineCoursesContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddOptions();
            //dapper connection
            services.Configure<ConnectionConfiguration>(Configuration.GetSection("ConnectionStrings"));

            services.AddAutoMapper(typeof(GetAllCoursesQuery));
            services.AddMediatR(typeof(GetAllCoursesQuery).Assembly);
            services.AddControllers(opt =>
           {
               //adds security to all endpoints and request a token for requests
               var JwtRule = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
               opt.Filters.Add(new AuthorizeFilter(JwtRule));
           })
                .AddFluentValidation(config => config.RegisterValidatorsFromAssemblyContaining<CreateCourseCommand>());

            //config CoreIdentity
            var builder = services.AddIdentityCore<Users>();
            var identityBuilder = new IdentityBuilder(builder.UserType, builder.Services);
            //add roles
            identityBuilder.AddRoles<IdentityRole>();
            identityBuilder.AddClaimsPrincipalFactory<UserClaimsPrincipalFactory<Users, IdentityRole>>();

            identityBuilder.AddEntityFrameworkStores<OnlineCoursesContext>();
            identityBuilder.AddSignInManager<SignInManager<Users>>();



            services.TryAddSingleton<ISystemClock, SystemClock>();

            //Add Jwt security to controllers
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ud8?xv$K5f7rvJ2=H3E5J*mk!9G"));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(config =>
            {
                config.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                };
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
            });

            services.AddScoped<IJwtGenerator, JwtGenerator>();
            services.AddScoped<ICurrentUser, CurrentUser>();
            services.AddTransient<IFactoryConnection, FactoryConnection>();
            services.AddTransient<IInstructorRepository, InstructorRepository>();
            services.AddTransient<IPaginationRepository, PaginationRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<HandlerErrorsMiddleware>();

            app.UseCors("corsApp");

            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));
            }
            //set to use Jwt security
            app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
