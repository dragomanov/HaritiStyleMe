using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HaritiStyleMe.Data.Interfaces;
using HaritiStyleMe.Models;
using System.Data.Entity;

namespace HaritiStyleMe.Data
{
    public class HaritiStyleMeData : IHaritiStyleMeData
    {
        private static IHaritiStyleMeData data;
        private HaritiStyleMeDbContext context;
        private IDictionary<Type, object> repositories;

        public HaritiStyleMeData(HaritiStyleMeDbContext context)
        {
            data = this;
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public static IHaritiStyleMeData Data { get { return data; } }

        public GenericRepository<User> Users
        {
            get { return this.GetRepository<User>(); }
        }

        public GenericRepository<Category> Categories
        {
            get { return this.GetRepository<Category>(); }
        }

        public GenericRepository<ServiceItem> ServiceItems 
        {
            get { return this.GetRepository<ServiceItem>(); }
        }

        public GenericRepository<Appointment> Appointments
        {
            get { return this.GetRepository<Appointment>(); }
        }

        public GenericRepository<TimeOff> TimesOff
        {
            get { return this.GetRepository<TimeOff>(); }
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }

        private GenericRepository<T> GetRepository<T>() where T : class
        {
            var typeOfModel = typeof(T);

            if (!this.repositories.ContainsKey(typeOfModel))
            {
                var type = typeof(GenericRepository<T>);
                this.repositories.Add(typeOfModel, Activator.CreateInstance(type, this.context));
            }

            return (GenericRepository<T>)this.repositories[typeOfModel];
        }
    }
}
