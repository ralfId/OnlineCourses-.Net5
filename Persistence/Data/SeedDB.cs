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
        public static async Task InsertData(OnlineCoursesContext coursesContext, UserManager<Users> userManager)
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

                await userManager.CreateAsync(user, "Password@123");

            }
        }
    }
}
