using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HaritiStyleMe.Models;

namespace HaritiStyleMe.Data.Interfaces
{
    public interface IHaritiStyleMeData
    {
        GenericRepository<User> Users { get; }

        GenericRepository<Category> Categories { get; }

        GenericRepository<ServiceItem> ServiceItems { get; }

        GenericRepository<Appointment> Appointments { get; }

        GenericRepository<TimeOff> TimesOff { get; }

        void SaveChanges();
    }
}
