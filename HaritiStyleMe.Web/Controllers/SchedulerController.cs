using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DayPilot.Web.Mvc;
using DayPilot.Web.Mvc.Enums;
using DayPilot.Web.Mvc.Events.Calendar;
using DayPilot.Web.Ui;
using HaritiStyleMe.Web.Controllers;
using HaritiStyleMe.Data.Interfaces;

namespace HaritiStyleMe.Web.Controllers
{
    [HandleError]
    public class SchedulerController : BaseController
    {
        public SchedulerController(IHaritiStyleMeData data)
            : base(data)
        {
        }

        //
        // GET: /Scheduler/

        public ActionResult Index()
        {
            return View();
        }

        //
        // POST: /Scheduler/Backend

        public ActionResult Backend()
        {
            return new Dpm(data).CallBack(this);
        }

        class Dpm : DayPilotMonth
        {
            private IHaritiStyleMeData db { get; set; }

            protected Dpm()
            {
            }

            public Dpm(IHaritiStyleMeData data)
            {
                this.db = data;
            }

            protected void OnInit(InitArgs e)
            {
                //var db = new DataClasses1DataContext();
                Events = db.Appointments.All().ToList();

                DataIdField = "id";
                DataTextField = "text";
                DataStartField = "time";
                DataEndField = "time";

                Update();
            }
        }
    }
}