using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Avengers.Mvc.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult ActivityDetail()
        {
            ViewBag.Message = "Your activity detail page.";

            return View();
        }
    }
}