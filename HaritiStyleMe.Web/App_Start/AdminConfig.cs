using HaritiStyleMe.Data;
using HaritiStyleMe.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HaritiStyleMe.Web
{
    public static class AdminConfig
    {
        private static HaritiStyleMeDbContext context = new HaritiStyleMeDbContext();
        private static UserManager manager = new UserManager(new UserStore<User>(context));

        public static void RegisterAdmin()
        {
            if (context.Users.FirstOrDefault(u => u.Email == "adragomanov@gmail.com") == null)
            {
                var admin = new User
                {
                    Name = "Anthony Dragomanov",
                    Email = "adragomanov@gmail.com",
                    UserName = "adragomanov@gmail.com",
                    PhoneNumber = null
                };

                var result = manager.CreateAsync(admin, "QWEqwe123").Result;
                result = manager.AddToRolesAsync(admin.Id, RolesConfig.adminRole, RolesConfig.employeeRole, RolesConfig.userRole).Result; 
            }
        }
    }
}