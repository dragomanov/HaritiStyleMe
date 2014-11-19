using AutoMapper;
using AutoMapper.QueryableExtensions;
using HaritiStyleMe.Data;
using HaritiStyleMe.Data.Interfaces;
using HaritiStyleMe.Models;
using HaritiStyleMe.Web.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HaritiStyleMe.Web.Controllers
{
    [Authorize]
    public class ScheduleController : BaseController
    {
        public ScheduleController(IHaritiStyleMeData data)
            : base(data)
        {
        }

        // GET: Schedule
        public ActionResult Index()
        {
            var scheduleViewModels = GetUnavailableTimes();
            return View(scheduleViewModels);
        }

        public virtual JsonResult Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(data.Appointments.All().ToDataSourceResult(request));
        }

        public virtual JsonResult Destroy([DataSourceRequest] DataSourceRequest request, ScheduleViewModel task)
        {
            if (ModelState.IsValid)
            {
                //data.Appointments.Delete(task);
            }

            return Json(new[] { task }.ToDataSourceResult(request, ModelState));
        }

        public virtual JsonResult Create([DataSourceRequest] DataSourceRequest request, ScheduleViewModel task)
        {
            if (ModelState.IsValid)
            {
                //data.Appointments.Add(task);
            }

            return Json(new[] { task }.ToDataSourceResult(request, ModelState));
        }

        public virtual JsonResult Update([DataSourceRequest] DataSourceRequest request, ScheduleViewModel task)
        {
            if (ModelState.IsValid)
            {
                //data.Appointments.Update(task);
            }

            return Json(new[] { task }.ToDataSourceResult(request, ModelState));
        }

        //protected override void Dispose(bool disposing)
        //{
        //    taskService.Dispose();
        //    meetingService.Dispose();
        //    base.Dispose(disposing);
        //}

        // GET: Schedule/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var scheduleViewModel = Mapper.Map<ScheduleViewModel>(this.data.Appointments.Find(id));

            if (scheduleViewModel == null)
            {
                return HttpNotFound();
            }

            return View(scheduleViewModel);
        }

        // GET: Schedule/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(data.Users.All(), "Id", "Name");
            ViewBag.ServiceItemId = new SelectList(data.ServiceItems.All(), "Id", "Name");
            return View();
        }

        // POST: Schedule/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Time,Duration,ServiceItemId,EmployeeId")] ScheduleViewModel scheduleViewModel)
        {
            if (ModelState.IsValid)
            {
                data.Appointments.Add(Mapper.Map<Appointment>(scheduleViewModel));
                data.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ServiceItemId = new SelectList(data.ServiceItems.All(), "Id", "Name", scheduleViewModel);
            return View(scheduleViewModel);
        }

        // GET: Schedule/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var scheduleViewModel = Mapper.Map<ScheduleViewModel>(data.Appointments.Find(id));

            if (scheduleViewModel == null)
            {
                return HttpNotFound();
            }

            ViewBag.ServiceItemId = new SelectList(data.ServiceItems.All(), "Id", "Name", scheduleViewModel.ServiceItemId);
            return View(scheduleViewModel);
        }

        // POST: Schedule/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Time,Duration,ServiceItemId,EmployeeId")] ScheduleViewModel scheduleViewModel)
        {
            if (ModelState.IsValid)
            {
                data.Appointments.Update(Mapper.Map<Appointment>(scheduleViewModel));
                data.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ServiceItemId = new SelectList(data.ServiceItems.All(), "Id", "Name", scheduleViewModel.ServiceItemId);
            return View(scheduleViewModel);
        }

        // GET: Schedule/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var scheduleViewModel = Mapper.Map<ScheduleViewModel>(data.Appointments.Find(id));

            if (scheduleViewModel == null)
            {
                return HttpNotFound();
            }

            return View(scheduleViewModel);
        }

        // POST: Schedule/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var appointment = data.Appointments.Find(id);
            data.Appointments.Delete(appointment);
            data.SaveChanges();
            return RedirectToAction("Index");
        }

        private List<ScheduleViewModel> GetUnavailableTimes()
        {
            var today = DateTime.Now.Date;
            var scheduleViewModels = this.data.Appointments.All()
                .Where(a => a.Time >= today)
                .Project()
                .To<ScheduleViewModel>()
                .ToList();

            scheduleViewModels.AddRange(this.data.TimesOff.All()
                .Where(a => a.Time >= DateTime.Now)
                .Project()
                .To<ScheduleViewModel>()
                .ToList());

            Mapper.CreateMap<Appointment, ScheduleViewModel>()
                  .ForMember(dest => dest.Start, opt => opt.MapFrom(src => src.Time))
                  .ForMember(dest => dest.End, opt => opt.MapFrom(src => src.Time + src.ServiceItem.Duration));

            return scheduleViewModels;
        }
    }
}
