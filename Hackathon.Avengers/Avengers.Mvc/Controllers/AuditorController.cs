using Avengers.Mvc.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Avengers.Mvc.Models;
using WebGrease.Css.Extensions;

namespace Avengers.Mvc.Controllers
{
    public class AuditorController : Controller
    {
        ProvidersService _providerService;
        PatientsService _patientService;
        IEnumerable<Provider> _providers;
        IEnumerable<Patient> _patients;

        public AuditorController()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:60225/api/");
            var clientWrapper = new HttpClientWrapper(client);
            _providerService = new ProvidersService(clientWrapper);
            _patientService = new PatientsService(clientWrapper);
        }

        public ActionResult Dashboard()
        {
            var viewModel = GetDashboardViewModel();
            ViewBag.Message = "Your contact page.";

            return View(viewModel);
        }

        private DashboardViewModel GetDashboardViewModel()
        {
            _providers = _providerService.GetProviders();
            _patients = _patientService.GetPatients();
            var dashboardEntities = new List<DashboardEntity>();

            _providers.ForEach(p => dashboardEntities.Add(new DashboardEntity { ID = p.ProviderID, FullName = $"{p.FirstName} {p.LastName}", Type = "provider", Entity = p }));
            _patients.ForEach(p => dashboardEntities.Add(new DashboardEntity { ID = p.Ssn, FullName = $"{p.FirstName} {p.LastName}", Type = "patient", Entity = p }));

            return new DashboardViewModel { Entities = dashboardEntities.OrderBy(x => x.FullName) };
        }

        [Route("/GetDetailPartialView/{id}/{type}")]
        public PartialViewResult GetDetailPartialView(string id, string type)
        {
            var entity = GetEntity(id, type);
            if (entity == null)
                return null;
            var partialName = type == "provider" ? "_ProviderDashboardDetailsPartial" : "_PatientDashboardDetailsPartial";
            return PartialView(partialName, entity);
        }

        private object GetEntity(string id, string type)
        {
            switch (type)
            {
                case "provider":
                    // This call takes FOREVERRRR
                    _providers = _providerService.GetProviders();
                    return _providerService.GetProvider(id, _providers);
                case "patient":
                    _patients = _patientService.GetPatients();
                    return _patientService.GetPatient(id, _patients);
                default:
                    return null;
            }
        }
    }
}