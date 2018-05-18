using Avengers.Mvc.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Avengers.Mvc.Models;

namespace Avengers.Mvc.Controllers
{
    public class AuditorController : Controller
    {
        ProvidersService _providerService;
        public AuditorController()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:60225/api/");
            var clientWrapper = new HttpClientWrapper(client);
            _providerService = new ProvidersService(clientWrapper);
        }

        public ActionResult Dashboard()
        {
            var providers = _providerService.GetProviders();
            //var model = BuildDashboardViewModel(providers);
            ViewBag.Message = "Your contact page.";
            ViewBag.CaseIndex = UpdateIndex(0);

            return View(providers);
        }

        public Int32 UpdateIndex(Int32 newIdx)
        {
            return newIdx;
        }

        private object BuildDashboardViewModel(IEnumerable<Provider> providers)
        {
            throw new NotImplementedException();
        }

        public ActionResult Report()
        {
            ViewBag.Message = "Your report page.";

            return View();
        }
    }
}