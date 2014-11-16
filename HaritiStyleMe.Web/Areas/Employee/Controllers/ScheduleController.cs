using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HaritiStyleMe.Data;
using HaritiStyleMe.Models;
using HaritiStyleMe.Data.Interfaces;

namespace HaritiStyleMe.Web.Areas.Employee.Controllers
{
    public class ScheduleController : BaseController
    {
        private HaritiStyleMeDbContext db = new HaritiStyleMeDbContext();

        public ScheduleController(IHaritiStyleMeData data)
            : base(data)
        {
        }

        // GET: Employee/Schedule
        public ActionResult Index()
        {
            var appointments = db.Appointments.Include(a => a.Client).Include(a => a.Employee).Include(a => a.ServiceItem);
            return View(appointments.ToList());
        }

        // GET: Employee/Schedule/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // GET: Employee/Schedule/Create
        public ActionResult Create()
        {
            ViewBag.ClientId = new SelectList(db.Users, "Id", "Name");
            ViewBag.EmployeeId = new SelectList(db.Users, "Id", "Name");
            ViewBag.ServiceItemId = new SelectList(db.ServiceItems, "Id", "Name");
            return View();
        }

        // POST: Employee/Schedule/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Time,ServiceItemId,ClientId,EmployeeId")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                db.Appointments.Add(appointment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClientId = new SelectList(db.Users, "Id", "Name", appointment.ClientId);
            ViewBag.EmployeeId = new SelectList(db.Users, "Id", "Name", appointment.EmployeeId);
            ViewBag.ServiceItemId = new SelectList(db.ServiceItems, "Id", "Name", appointment.ServiceItemId);
            return View(appointment);
        }

        // GET: Employee/Schedule/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientId = new SelectList(db.Users, "Id", "Name", appointment.ClientId);
            ViewBag.EmployeeId = new SelectList(db.Users, "Id", "Name", appointment.EmployeeId);
            ViewBag.ServiceItemId = new SelectList(db.ServiceItems, "Id", "Name", appointment.ServiceItemId);
            return View(appointment);
        }

        // POST: Employee/Schedule/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Time,ServiceItemId,ClientId,EmployeeId")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appointment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClientId = new SelectList(db.Users, "Id", "Name", appointment.ClientId);
            ViewBag.EmployeeId = new SelectList(db.Users, "Id", "Name", appointment.EmployeeId);
            ViewBag.ServiceItemId = new SelectList(db.ServiceItems, "Id", "Name", appointment.ServiceItemId);
            return View(appointment);
        }

        // GET: Employee/Schedule/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // POST: Employee/Schedule/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Appointment appointment = db.Appointments.Find(id);
            db.Appointments.Remove(appointment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
