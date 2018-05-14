using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Avengers.Mvc.Controllers
{
    public class AuditorController : Controller
    {

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
    }
}