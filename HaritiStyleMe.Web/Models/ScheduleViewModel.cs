using HaritiStyleMe.Models;
using HaritiStyleMe.Web.Common.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HaritiStyleMe.Web.Models
{
    public class ScheduleViewModel : IMapFrom<Appointment>, IMapFrom<TimeOff>
    {
        public int Id { get; set; }

        public DateTime Time { get; set; }

        public TimeSpan Duration { get; set; }

        public int ServiceItemId { get; set; }

        public virtual ServiceItem ServiceItem { get; set; }

        public string EmployeeId { get; set; }

        public virtual User Employee { get; set; }
    }
}