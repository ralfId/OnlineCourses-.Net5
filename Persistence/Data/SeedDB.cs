using Domain.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data
{
    public class SeedDB
    {
        public static async Task InsertData(
            OnlineCoursesContext coursesContext, 
            UserManager<Users> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            await SeedRoles(roleManager);
            await SeedUsers(userManager);
          
        }

        private static async Task SeedUsers(UserManager<Users> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new Users
                {
                    Name = "Rafael",
                    LastName = "ID",
                    UserName = "ralfId",
                    Email = "ralf_raid@yopmail.com"
                };

                var createUser =  await userManager.CreateAsync(user, "Password@123");

                if (createUser.Succeeded)
                {
                    var newUser = await userManager.FindByNameAsync(user.UserName);
                    if (newUser!= null)
                    {
                        await userManager.AddToRoleAsync(newUser, "Admin");
                    }
                }
            }

        }

        private static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            //ADMIN
            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            //ROOTINSTRUCTOR
            if (!roleManager.RoleExistsAsync("rootInstructor").Result)
            {
                await roleManager.CreateAsync(new IdentityRole("rootInstructor"));
            }
            //INSTRUCTOR
            if (!roleManager.RoleExistsAsync("Instructor").Result)
            {
                await roleManager.CreateAsync(new IdentityRole("Instructor"));
            }
            //STUDENT
            if (!roleManager.RoleExistsAsync("Student").Result)
            {
                await roleManager.CreateAsync(new IdentityRole("Student"));
            }

        }

      
    }
}
