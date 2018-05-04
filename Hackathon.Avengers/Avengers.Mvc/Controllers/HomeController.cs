using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Avengers.Mvc.Controllers
{
    public class HomeController : Controller
    {
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

        public ActionResult Login()
        {
            ViewBag.Message = "Your login page.";

            return View();
        }

        public ActionResult AdminLogin()
        {
            ViewBag.Message = "Your admin login page.";

            return View();
        }

        public ActionResult Dashboard()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Report()
        {
            ViewBag.Message = "Your report page.";

            return View();
        }

        public ActionResult ActivityDetail()
        {
            ViewBag.Message = "Your activity detail page.";

            return View();
        }
    }
}