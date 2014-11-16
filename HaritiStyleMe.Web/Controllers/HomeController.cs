using HaritiStyleMe.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HaritiStyleMe.Web.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IHaritiStyleMeData data)
            : base(data)
        {
        }

        //[OutputCache(Duration = 15 * 60, VaryByHeader = "Cookie")]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}