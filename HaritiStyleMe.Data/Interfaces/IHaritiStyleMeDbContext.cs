using HaritiStyleMe.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaritiStyleMe.Data.Interfaces
{
    interface IHaritiStyleMeDbContext
    {
        IDbSet<User> Users { get; set; }

        IDbSet<Category> Categories { get; set; }

        IDbSet<ServiceItem> ServiceItems { get; set; }

        IDbSet<Appointment> Appointments { get; set; }

        IDbSet<TimeOff> TimesOff { get; set; }
    }
}
