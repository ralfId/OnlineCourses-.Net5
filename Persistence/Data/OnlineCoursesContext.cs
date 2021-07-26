
using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data
{
    public class OnlineCoursesContext : IdentityDbContext<Users>
    {
        public OnlineCoursesContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CourseInstructor>().HasKey(x => new { x.InstructorId, x.CourseId });
        }

        public DbSet<Courses> Courses { get; set; }
        public DbSet<Instructors> Instructors { get; set; }
        public DbSet<CourseInstructor> CourseInstructor { get; set; }
        public DbSet<Prices> Prices { get; set; }
        public DbSet<Comments> Comments { get; set; }


    }
}
