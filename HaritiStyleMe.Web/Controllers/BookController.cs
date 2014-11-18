using AutoMapper.QueryableExtensions;
using HaritiStyleMe.Data.Interfaces;
using HaritiStyleMe.Models;
using HaritiStyleMe.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HaritiStyleMe.Web.Controllers
{
    public class BookController : BaseController
    {
        public BookController(IHaritiStyleMeData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            var categories = data.Categories.All();
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name");

            return View(categories);
        }

        public ActionResult GetServiceItemsByCategoryId(int? categoryId)
        {
            IEnumerable<ServiceItemViewModel> services;
            var allServices = data.ServiceItems.All();
            if (categoryId != null)
            {
                allServices = allServices.Where(s => s.CategoryId == categoryId);
            }

            services = allServices.Project().To<ServiceItemViewModel>();

            return PartialView(services);
        }
    }
}