using BasketWebPanel.BindingModels;
using KorsaWebPanel.Areas.Dashboard.BindingModels;
using KorsaWebPanel.Areas.Dashboard.ViewModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BasketWebPanel.Areas.Dashboard.Controllers
{
    public class CountryCitiesController : Controller
    {
        public ActionResult Index(int id = 0)
        {
            CountryBindingModel model = new CountryBindingModel();
            CountryViewModel viewModel = new CountryViewModel();
            model.SetSharedData(User);
            if (id == 0)
            {

                return View(model);
            }
            else
            {
                var response = AsyncHelpers.RunSync<JObject>(() => ApiCall.CallApi("/api/Admin/GetEntityById", User, null, true, false, null, "Id=" + id + "&EntityType=" + Utility.KorsaEntityTypes.Country));
                if (response is Error)
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Internal Server Error");
                else
                    viewModel = response.GetValue("result").ToObject<CountryViewModel>();
                model.IsActive = viewModel.IsActive;
                model.Name = viewModel.English.Name;
                model.SetSharedData(User);
                return View(model);
            }
            
        }

        public async Task<ActionResult> AddCountry(CountryBindingModel model)
        {
            if (ModelState.IsValid)
            {

                JObject response;
                response = await ApiCall.CallApi("/api/Admin/AddUpdateCountry", User, model);
                if (response.ToString().Contains("UnAuthorized"))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "UnAuthorized Error");
                }

                else if (response is Error)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, (response as Error).ErrorMessage);
                }

                return RedirectToAction("ManageCountries", "CountryCities");
            }
            else
            {
                return View();
            }

        }

        [HttpGet]
        public ActionResult ManageCountries()
        {
            var response = AsyncHelpers.RunSync<JObject>(() => ApiCall.CallApi("/api/Driver/GetAllCountries", User, null, true, false, null));

            CountryViewModelList model = new CountryViewModelList();

            if (response is Error)
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Internal Server Error");
            else
                model = response.GetValue("result").ToObject<CountryViewModelList>();
            model.SetSharedData(User);



            return View(model);
        }

        public async Task<ActionResult> DeleteCountry(int id)
        {
            JObject response;
            response = await ApiCall.CallApi("/api/Admin/DeleteCountry", User, null, true, false, null, "id="+id);
            if (response.ToString().Contains("UnAuthorized"))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "UnAuthorized Error");
            }

            else if (response is Error)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, (response as Error).ErrorMessage);
            }

            return RedirectToAction("ManageCountries", "CountryCities");
        }

        public ActionResult CityIndex(int id = 0)
        {
            CityBindingModel model = new CityBindingModel();
            CityViewModel viewModel = new CityViewModel();
            CountryViewModelList Countries = new CountryViewModelList();
            var response = AsyncHelpers.RunSync<JObject>(() => ApiCall.CallApi("/api/Driver/GetAllCountries", User, null, true, false, null));
            if (response is Error)
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Internal Server Error");
            else
                Countries = response.GetValue("result").ToObject<CountryViewModelList>();

            model.Countries = Countries;
            model.SetSharedData(User);
            if (id == 0)
            {

                return View(model);
            }
            else
            {
                var response1 = AsyncHelpers.RunSync<JObject>(() => ApiCall.CallApi("/api/Admin/GetEntityById", User, null, true, false, null, "Id=" + id + "&EntityType=" + Utility.KorsaEntityTypes.City));
                if (response1 is Error)
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Internal Server Error");
                else
                    viewModel = response1.GetValue("result").ToObject<CityViewModel>();

                model.Name = viewModel.English.Name;
                model.Country_Id = viewModel.Country.id;
                model.IsActive = viewModel.IsActive;

                return View(model);
            }
            
        }


        public async Task<ActionResult> AddCity(CityBindingModel model)
        {
            if (ModelState.IsValid)
            {

                JObject response;
                response = await ApiCall.CallApi("/api/Admin/AddUpdateCity", User, model);
                if (response.ToString().Contains("UnAuthorized"))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "UnAuthorized Error");
                }

                else if (response is Error)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, (response as Error).ErrorMessage);
                }

                return RedirectToAction("ManageCities", "CountryCities");
            }
            else
            {
                return View();
            }

        }

        public ActionResult ManageCities()
        {
            var response = AsyncHelpers.RunSync<JObject>(() => ApiCall.CallApi("/api/Driver/GetAllCities", User, null, true, false, null));

            CityViewModelList model = new CityViewModelList();

            if (response is Error)
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Internal Server Error");
            else
                model = response.GetValue("result").ToObject<CityViewModelList>();
            model.SetSharedData(User);
            return View(model);
        }

        public async Task<ActionResult> DeleteCity(int id)
        {
            JObject response;
            response = await ApiCall.CallApi("/api/Admin/DeleteCity", User, null, true, false, null, "id=" + id);
            if (response.ToString().Contains("UnAuthorized"))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "UnAuthorized Error");
            }

            else if (response is Error)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, (response as Error).ErrorMessage);
            }

            return RedirectToAction("ManageCities", "CountryCities");
        }
    }
}