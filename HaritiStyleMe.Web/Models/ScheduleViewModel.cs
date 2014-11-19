﻿using HaritiStyleMe.Models;
using HaritiStyleMe.Web.Common.Mapping;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HaritiStyleMe.Web.Models
{
    public class ScheduleViewModel : IMapFrom<Appointment>, IMapFrom<TimeOff>, ISchedulerEvent
    {
        public int Id { get; set; }

        public int ServiceItemId { get; set; }

        public string Description { get; set; }

        public DateTime End { get; set; }

        public string EndTimezone { get; set; }

        public bool IsAllDay { get; set; }

        public string RecurrenceException { get; set; }

        public string RecurrenceRule { get; set; }

        public DateTime Start { get; set; }

        public string StartTimezone { get; set; }

        public string Title { get; set; }
    }
}