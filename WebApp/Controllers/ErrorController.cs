using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public string NotAuthorised()
        {
            return "You Are not autorised for this, can concern with App Admin";
        }
    }
}