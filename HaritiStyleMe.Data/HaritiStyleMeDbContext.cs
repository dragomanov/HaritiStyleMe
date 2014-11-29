using HaritiStyleMe.Data.Interfaces;
using HaritiStyleMe.Data.Migrations;
using HaritiStyleMe.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaritiStyleMe.Data
{
    public class HaritiStyleMeDbContext : IdentityDbContext<User>, IHaritiStyleMeDbContext
    {
        public HaritiStyleMeDbContext()
            : base("SQLSERVER_CONNECTION_STRING", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<HaritiStyleMeDbContext, Configuration>());
        }

        public static HaritiStyleMeDbContext Create()
        {
            return new HaritiStyleMeDbContext();
        }

        public virtual IDbSet<Category> Categories { get; set; }

        public virtual IDbSet<ServiceItem> ServiceItems { get; set; }

        public virtual IDbSet<Appointment> Appointments { get; set; }

        public virtual IDbSet<TimeOff> TimesOff { get; set; }
    }
}
