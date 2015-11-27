using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartSpaceWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "About work";


            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Our contacts";

            return View();
        }
    }
}