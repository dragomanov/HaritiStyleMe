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
    [Authorize]
    public class BookController : BaseController
    {
        public BookController(IHaritiStyleMeData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            var categories = data.Categories.All();
            ViewBag.Category = new SelectList(categories, "Id", "Name");

            return View(categories);
        }

        [ChildActionOnly()]
        public ActionResult GetServiceItemsByCategoryId(int? categoryId)
        {
            IEnumerable<ServiceItemViewModel> services;
            if (categoryId == null)
            {
                services = data.ServiceItems.All()
                    .Project()
                    .To<ServiceItemViewModel>();
            }
            else
            {
                services = data.ServiceItems.All()
                    .Where(s => s.CategoryId == categoryId)
                    .Project()
                    .To<ServiceItemViewModel>();
            }

            return PartialView(services);
        }
    }
}