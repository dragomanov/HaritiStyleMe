using HaritiStyleMe.Data;
using HaritiStyleMe.Data.Interfaces;
using HaritiStyleMe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HaritiStyleMe.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        protected IHaritiStyleMeData data { get; set; }

        protected User CurrentUser 
        {
            get { return this.data.Users.All().FirstOrDefault(u => u.UserName == this.HttpContext.User.Identity.Name); }
        }

        public BaseController(IHaritiStyleMeData data)
        {
            this.data = data;
        }
    }
}