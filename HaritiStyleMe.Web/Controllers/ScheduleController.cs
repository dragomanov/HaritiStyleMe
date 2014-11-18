using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HaritiStyleMe.Data;
using HaritiStyleMe.Web.Models;
using HaritiStyleMe.Data.Interfaces;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using HaritiStyleMe.Models;

namespace HaritiStyleMe.Web.Controllers
{
    public class ScheduleController : BaseController
    {
        public ScheduleController(IHaritiStyleMeData data)
            : base(data)
        {
        }

        // GET: Schedule
        public ActionResult Index()
        {
            Mapper.CreateMap<Appointment, ScheduleViewModel>()
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.ServiceItem.Duration));
            var scheduleViewModels = this.data.Appointments.All().Project().To<ScheduleViewModel>().ToList();
            scheduleViewModels.AddRange(this.data.TimesOff.All().Project().To<ScheduleViewModel>().ToList());
            return View(scheduleViewModels);
        }

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

            ViewBag.EmployeeId = new SelectList(data.Users.All(), "Id", "Name", scheduleViewModel.EmployeeId);
            ViewBag.ServiceItemId = new SelectList(data.ServiceItems.All(), "Id", "Name", scheduleViewModel.ServiceItemId);
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

            ViewBag.EmployeeId = new SelectList(data.Users.All(), "Id", "Name", scheduleViewModel.EmployeeId);
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

            ViewBag.EmployeeId = new SelectList(data.Users.All(), "Id", "Name", scheduleViewModel.EmployeeId);
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
    }
}
