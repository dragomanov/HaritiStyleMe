using HaritiStyleMe.Data.Interfaces;
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

        // GET: Book
        public ActionResult Index()
        {
            var serviceItems = data.ServiceItems.All();
            return View(serviceItems.ToList());
        }
    }
}