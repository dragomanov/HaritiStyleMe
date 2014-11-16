using System;
using System.ComponentModel.DataAnnotations;

namespace HaritiStyleMe.Models
{
    public class Appointment
    {
        #region Constructors
        public Appointment()
        {
        }

        public Appointment(DateTime time, ServiceItem serviceItem, User client, User employee)
        {
            this.Time = time;
            this.ServiceItem = serviceItem;
            this.Client = client;
            this.Employee = employee;
        }
        #endregion

        public int Id { get; set; }

        [Required]
        public DateTime Time { get; set; }

        [Required]
        public int ServiceItemId { get; set; }

        public virtual ServiceItem ServiceItem { get; set; }

        public string ClientId { get; set; }

        public virtual User Client { get; set; }

        public string EmployeeId { get; set; }

        public virtual User Employee { get; set; }
    }
}
