namespace HaritiStyleMe.Web
{
    using HaritiStyleMe.Data;
    using HaritiStyleMe.Models;
    using HaritiStyleMe.Web.Common;
    using HaritiStyleMe.Web.Common.Extensions;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Linq;

    public static class DataGenerator
    {
        private static Random rng = new Random();
        private static HaritiStyleMeDbContext context = new HaritiStyleMeDbContext();
        private static UserManager manager = new UserManager(new UserStore<User>(context));

        public static void SeedDatabase()
        {
            IdentityResult result;

            var categories = context.Categories.ToArray();
            if (!categories.Any())
            {
                categories = new Category[] 
                {
                    new Category("Hairstyling"),
                    new Category("Manicure & pedicure"),
                    new Category("Cosmetic")
                };

                context.Categories.AddOrUpdate(c => c.Name, categories);
                SaveChanges();
            }

            var serviceItems = context.ServiceItems.ToArray();
            if (!serviceItems.Any())
            {
                serviceItems = new ServiceItem[] 
                {
                    new ServiceItem("Haircut", 10, TimeSpan.FromMinutes(30), categories[0]),
                    new ServiceItem("Official haircut", 30, TimeSpan.FromMinutes(150), categories[0]),
                    new ServiceItem("Flat Iron", 15, TimeSpan.FromMinutes(30), categories[0]),
                    new ServiceItem("Men's Hair Cut", 14, TimeSpan.FromMinutes(30), categories[0]),
                    new ServiceItem("Men's Wash and Cut", 17, TimeSpan.FromMinutes(30), categories[0]),
                    new ServiceItem("Flat Top", 16, TimeSpan.FromMinutes(30), categories[0]),
                    new ServiceItem("Men's Hair Color", 30, TimeSpan.FromMinutes(60), categories[0]),
                    new ServiceItem("Face Shave", 13, TimeSpan.FromMinutes(30), categories[0]),
                    new ServiceItem("Full Head Shave", 20, TimeSpan.FromMinutes(60), categories[0]),
                    new ServiceItem("Full Head & Face Shave", 28, TimeSpan.FromMinutes(60), categories[0]),
                    new ServiceItem("Women's Hair Cut", 15, TimeSpan.FromMinutes(30), categories[0]),
                    new ServiceItem("Bang Trims", 6, TimeSpan.FromMinutes(15), categories[0]),
                    new ServiceItem("Shampoo and Blow Dry", 17, TimeSpan.FromMinutes(30), categories[0]),
                    new ServiceItem("Deep Conditioning Treatment", 15, TimeSpan.FromMinutes(30), categories[0]),
                    new ServiceItem("Perms", 60, TimeSpan.FromMinutes(120), categories[0]),
                    new ServiceItem("Spiral Perms", 80, TimeSpan.FromMinutes(150), categories[0]),
                    new ServiceItem("Chemical Straightener", 60, TimeSpan.FromMinutes(120), categories[0]),
                    new ServiceItem("Color", 55, TimeSpan.FromMinutes(90), categories[0]),
                    new ServiceItem("Hilting", 85, TimeSpan.FromMinutes(150), categories[0]),
                    new ServiceItem("Extensions", 50, TimeSpan.FromMinutes(90), categories[0]),
                    new ServiceItem("French Braid", 15, TimeSpan.FromMinutes(30), categories[0]),
                    new ServiceItem("File & Polish", 36, TimeSpan.FromMinutes(30), categories[1]),
                    new ServiceItem("Shellac French File & Polish", 41, TimeSpan.FromMinutes(30), categories[1]),
                    new ServiceItem("Manicure", 61, TimeSpan.FromMinutes(60), categories[1]),
                    new ServiceItem("Shellac French Manicure", 66, TimeSpan.FromMinutes(90), categories[1]),
                    new ServiceItem("Pedicure", 71, TimeSpan.FromMinutes(90), categories[1]),
                    new ServiceItem("Removal of shellac", 10, TimeSpan.FromMinutes(15), categories[1]),
                    new ServiceItem("Lower half leg", 28, TimeSpan.FromMinutes(30), categories[2]),
                    new ServiceItem("Top half leg", 35, TimeSpan.FromMinutes(30), categories[2]),
                    new ServiceItem("Full Leg", 50, TimeSpan.FromMinutes(60), categories[2]),
                    new ServiceItem("Underarm", 18, TimeSpan.FromMinutes(30), categories[2]),
                    new ServiceItem("Arms", 28, TimeSpan.FromMinutes(30), categories[2])
                };

                context.ServiceItems.AddOrUpdate(s => s.Name, serviceItems);
                SaveChanges();
            }

            var employees = GetEmployees().ToArray();
            if (employees.Count() == 1)
            {
                employees = new User[] 
                {
                    new User { Name = "Brice Lambson", Email = "blambson@gmail.com", UserName = "blambson@gmail.com", PhoneNumber = null },
                    new User { Name = "Beatrice Wimberlou", Email = "bwimberlou@gmail.com", UserName = "bwimberlou@gmail.com", PhoneNumber = null },
                    new User { Name = "Rowan Miller", Email = "rmiller@gmail.com", UserName = "rmiller@gmail.com", PhoneNumber = null }
                };

                foreach (var employee in employees)
                {
                    if (context.Users.FirstOrDefault(u => u.Email == employee.Email) == null)
                    {
                        result = manager.CreateAsync(employee, "QWEqwe123").Result;
                    }
                }

                result = manager.AddToRoleAsync(employees[0].Id, RolesConfig.employeeRole).Result;
                result = manager.AddToRoleAsync(employees[1].Id, RolesConfig.employeeRole).Result;
                result = manager.AddToRoleAsync(employees[2].Id, RolesConfig.employeeRole).Result;
                result = manager.AddToRoleAsync(employees[0].Id, RolesConfig.userRole).Result;
                result = manager.AddToRoleAsync(employees[1].Id, RolesConfig.userRole).Result;
                result = manager.AddToRoleAsync(employees[2].Id, RolesConfig.userRole).Result;
                SaveChanges();

                employees = context.Users.ToArray();

                AddUserInCategory(employees[0], categories[0]);
                AddUserInCategory(employees[1], categories[0]);
                AddUserInCategory(employees[2], categories[1]);
                AddUserInCategory(employees[3], categories[2]);

                context.Users.AddOrUpdate(u => u.Email, employees);
                SaveChanges();
            }

            var users = context.Users.ToArray();
            if (users.Count() <= 4)
            {
                users = GenerateUsers(100);
                context.Users.AddOrUpdate(u => u.Email, users);
                SaveChanges();

                foreach (var user in users)
                {
                    result = manager.AddToRoleAsync(user.Id, RolesConfig.userRole).Result;
                }

                SaveChanges();
            }

            if (!context.TimesOff.Any())
            {
                var TimesOff = GenerateTimesOffForUsers(employees, 10);
                context.TimesOff.AddOrUpdate(TimesOff);
                SaveChanges();
            }

            if (!context.Appointments.Any())
            {
                var appointments = GenerateAppointments(employees, users, 600);
                context.Appointments.AddOrUpdate(appointments);
                SaveChanges();
            }
        }

        private static List<User> GetEmployees()
        {
            var employeeList = new List<User>();
            var employeeRole = context.Roles.First(r => r.Name == RolesConfig.employeeRole);
            foreach (var user in context.Users)
            {
                if (user.Roles.Any(r => r.RoleId == employeeRole.Id))
                {
                    employeeList.Add(user);
                }
            }

            return employeeList;
        }

        private static void AddUserInCategory(User user, Category category)
        {
            user.Categories.Add(category);
        }

        private static User[] GenerateUsers(int count)
        {
            var users = new User[count];

            for (int i = 0; i < count; i++)
            {
                var name = "test" + i;
                var user = new User { Name = name, UserName = name, Email = name + "@gmail.com" };
                users[i] = user;
            }

            return users;
        }

        private static TimeOff[] GenerateTimesOffForUsers(IList<User> users, int count)
        {
            var TimesOff = new TimeOff[count];

            for (int i = 0; i < count; i++)
            {
                var timeSlots = TimeSlots.Count;
                var fromSlot = rng.Next(0, timeSlots);
                var durationSlots = rng.Next(1, timeSlots - fromSlot + 1);
                var time = TimeSlots.ToDateTime(GetRandomDate(0, 30), fromSlot);
                var duration = TimeSlots.ToDuration(durationSlots);
                var user = users.GetRandom();
                var timeOff = new TimeOff(time, duration, user.Id);
                TimesOff[i] = timeOff;
            }

            return TimesOff;
        }

        private static Appointment[] GenerateAppointments(IList<User> employees, IList<User> users, int count)
        {
            var appointments = new Appointment[count];
            var serviceItems = context.ServiceItems.Include(s => s.Category).ToArray();

            for (int i = 0; i < count; i++)
            {
                var client = users.GetRandom();
                var serviceItem = serviceItems.GetRandom();
                var employeesInCategory = employees.Where(e => e.Categories.Contains(serviceItem.Category)).ToArray();
                var employee = employeesInCategory.GetRandom();
                var times = GetAvailableTimesForService(employee, serviceItem, GetRandomDate(0, 30));
                if (times.Count == 0)
                {
                    i--;
                    continue;
                }
                var time = times.GetRandom();
                var appointment = new Appointment(time, serviceItem, client, employee);
                appointments[i] = appointment;
            }

            return appointments;
        }

        private static IList<DateTime> GetAvailableTimesForService(User employee, ServiceItem serviceItem, DateTime date)
        {
            var times = new List<DateTime>();
            var availableSlots = TimeSlots.All;
            var TimesOff = context.TimesOff.Include(t => t.Employee).Where(t => t.EmployeeId == employee.Id).ToArray();
            var appointments = context.Appointments.Include(t => t.Employee).Where(t => t.EmployeeId == employee.Id).ToArray();

            foreach (var timeOff in TimesOff)
            {
                if (timeOff.Time.Date == date.Date)
                {
                    var time = timeOff.Time;
                    var duration = timeOff.Duration;
                    TimeSlots.RemoveSlots(availableSlots, time, duration);
                }
            }

            foreach (var appointment in appointments)
            {
                if (appointment.Time.Date == date.Date)
                {
                    var time = appointment.Time;
                    var duration = appointment.ServiceItem.Duration;
                    TimeSlots.RemoveSlots(availableSlots, time, duration);
                }
            }

            availableSlots = TimeSlots.AvailableSlots(availableSlots, serviceItem.Duration).ToList();

            return TimeSlots.ToTimesOfDay(date, availableSlots);
        }

        /// <summary>
        /// Gets a random date in the range of days, relative to today
        /// </summary>
        /// <param name="from">The lower bound of days in the future (negative for past)</param>
        /// <param name="to">The upper bound of days in the future (negative for past)</param>
        /// <returns>A random Date in the specified range</returns>
        public static DateTime GetRandomDate(int from, int to)
        {
            return DateTime.Now.Date.AddDays(rng.Next(from, to));
        }

        private static void SaveChanges()
        {
            try
            {
                context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}