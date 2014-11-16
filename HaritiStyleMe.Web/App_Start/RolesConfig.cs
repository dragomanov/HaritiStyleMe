using HaritiStyleMe.Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Web;

namespace HaritiStyleMe.Web
{
    public static class RolesConfig
    {
        public const string adminRole = "Admin";
        public const string employeeRole = "Employee";
        public const string userRole = "User";

        public static void RegisterRoles()
        {
            var rm = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new HaritiStyleMeDbContext()));
            if (!rm.Roles.Any())
            {
                rm.Create(new IdentityRole(adminRole));
                rm.Create(new IdentityRole(employeeRole));
                rm.Create(new IdentityRole(userRole));
            }
        }
    }
}