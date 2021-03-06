﻿using System;
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
using Microsoft.AspNet.Identity;

namespace HaritiStyleMe.Web.Areas.Employee.Controllers
{
    public class TimesOffController : BaseController
    {
        private HaritiStyleMeDbContext db = new HaritiStyleMeDbContext();

        public TimesOffController(IHaritiStyleMeData data)
            : base(data)
        {
        }

        // GET: Employee/TimesOff
        public ActionResult Index()
        {
            var timeOffs = db.TimesOff.Where(t => t.EmployeeId == CurrentUser.Id);
            return View(timeOffs.ToList());
        }

        // GET: Employee/TimesOff/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TimeOff timeOff = db.TimesOff.FirstOrDefault(t => t.Id == id && t.EmployeeId == CurrentUser.Id);

            if (timeOff == null)
            {
                return HttpNotFound();
            }

            return View(timeOff);
        }

        // GET: Employee/TimesOff/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/TimesOff/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Time,Duration")] TimeOff timeOff)
        {
            if (ModelState.IsValid)
            {
                timeOff.EmployeeId = CurrentUser.Id;
                db.TimesOff.Add(timeOff);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(timeOff);
        }

        // GET: Employee/TimesOff/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TimeOff timeOff = db.TimesOff.FirstOrDefault(t => t.Id == id && t.EmployeeId == CurrentUser.Id);

            if (timeOff == null)
            {
                return HttpNotFound();
            }

            return View(timeOff);
        }

        // POST: Employee/TimesOff/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Time,Duration,EmployeeId")] TimeOff timeOff)
        {
            if (ModelState.IsValid)
            {
                db.Entry(timeOff).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Users, "Id", "Name", timeOff.EmployeeId);
            return View(timeOff);
        }

        // GET: Employee/TimesOff/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TimeOff timeOff = db.TimesOff.FirstOrDefault(t => t.Id == id && t.EmployeeId == CurrentUser.Id);

            if (timeOff == null)
            {
                return HttpNotFound();
            }

            return View(timeOff);
        }

        // POST: Employee/TimesOff/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TimeOff timeOff = db.TimesOff.FirstOrDefault(t => t.Id == id && t.EmployeeId == CurrentUser.Id);
            db.TimesOff.Remove(timeOff);
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
