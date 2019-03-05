using BasketWebPanel.BindingModels;
using BasketWebPanel.ViewModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BasketWebPanel.Areas.Dashboard.Controllers
{
    public class ContentController : Controller
    {
        public ActionResult PrivacyPolicy()
        {
            try
            {
                var response = AsyncHelpers.RunSync<JObject>(() => ApiCall.CallApi("api/Settings/GetSettings", User, null, true));

                if (response is Error)
                {
                    return new HttpStatusCodeResult((int)HttpStatusCode.InternalServerError, (response as Error).ErrorMessage);
                }

                SettingsViewModel model = response.GetValue("Result").ToObject<SettingsViewModel>();
                PrivacyViewModel viewModel = new PrivacyViewModel();
                viewModel.PrivacyPolicy = model.PrivacyPolicy == null ? "" : model.PrivacyPolicy;
                viewModel.SetSharedData(User);
                return View(viewModel);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(Utility.LogError(ex), "Internal Server Error");
            }
        }

        [HttpPost]
        public ActionResult PrivacyPolicy(PrivacyViewModel model)
        {
            try
            {
                var response = AsyncHelpers.RunSync<JObject>(() => ApiCall.CallApi("api/Settings/SetPrivacyPolicy", User, model));

                if (response is Error || response == null)
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Internal Server Error");

                return Json("Success");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(Utility.LogError(ex), "Internal Server Error");
            }
        }

        public ActionResult AboutUs()
        {
            try
            {
                var response = AsyncHelpers.RunSync<JObject>(() => ApiCall.CallApi("api/Settings/GetSettings", User, null, true));

                if (response is Error)
                {
                    return new HttpStatusCodeResult((int)HttpStatusCode.InternalServerError, (response as Error).ErrorMessage);
                }

                SettingsViewModel model = response.GetValue("Result").ToObject<SettingsViewModel>();
                AboutUsViewModel viewModel = new AboutUsViewModel();
                viewModel.AboutUs = model.PrivacyPolicy == null ? "" : model.AboutUs;
                viewModel.SetSharedData(User);
                return View(viewModel);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(Utility.LogError(ex), "Internal Server Error");
            }
        }

        [HttpPost]
        public ActionResult AboutUs(AboutUsViewModel model)
        {
            try
            {
                var response = AsyncHelpers.RunSync<JObject>(() => ApiCall.CallApi("api/Settings/SetAboutUs", User, model));

                if (response is Error || response == null)
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Internal Server Error");

                return Json("Success");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(Utility.LogError(ex), "Internal Server Error");
            }
        }

        public ActionResult RefundExchange()
        {
            try
            {
                var response = AsyncHelpers.RunSync<JObject>(() => ApiCall.CallApi("api/Settings/GetSettings", User, null, true));

                if (response is Error)
                {
                    return new HttpStatusCodeResult((int)HttpStatusCode.InternalServerError, (response as Error).ErrorMessage);
                }

                SettingsViewModel model = response.GetValue("Result").ToObject<SettingsViewModel>();
                RefundExchangeViewModel viewModel = new RefundExchangeViewModel();
                viewModel.RefundExchange = model.RefundExchange == null ? "" : model.RefundExchange;
                viewModel.SetSharedData(User);
                return View(viewModel);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(Utility.LogError(ex), "Internal Server Error");
            }
        }

        [HttpPost]
        public ActionResult RefundExchange(RefundExchangeViewModel model)
        {
            try
            {
                var response = AsyncHelpers.RunSync<JObject>(() => ApiCall.CallApi("api/Settings/SetRefundExchange", User, model));

                if (response is Error || response == null)
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Internal Server Error");

                return Json("Success");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(Utility.LogError(ex), "Internal Server Error");
            }
        }

        public ActionResult TermsAndConditions()
        {
            try
            {
                var response = AsyncHelpers.RunSync<JObject>(() => ApiCall.CallApi("api/Settings/GetSettings", User, null, true));

                if (response is Error)
                {
                    return new HttpStatusCodeResult((int)HttpStatusCode.InternalServerError, (response as Error).ErrorMessage);
                }

                SettingsViewModel model = response.GetValue("Result").ToObject<SettingsViewModel>();
                TermsConditionsViewModel viewModel = new TermsConditionsViewModel();
                viewModel.TermsConditions = model.TermsConditions == null ? "" : model.TermsConditions;
                viewModel.SetSharedData(User);
                return View(viewModel);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(Utility.LogError(ex), "Internal Server Error");
            }
        }

        [HttpPost]
        public ActionResult TermsAndConditions(TermsConditionsViewModel model)
        {
            try
            {
                var response = AsyncHelpers.RunSync<JObject>(() => ApiCall.CallApi("api/Settings/SetTermsAndConditions", User, model));

                if (response is Error || response == null)
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Internal Server Error");

                return Json("Success");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(Utility.LogError(ex), "Internal Server Error");
            }
        }

        public ActionResult Urls()
        {
            try
            {
                var response = AsyncHelpers.RunSync<JObject>(() => ApiCall.CallApi("api/Settings/GetSettings", User, null, true));

                if (response is Error)
                {
                    return new HttpStatusCodeResult((int)HttpStatusCode.InternalServerError, (response as Error).ErrorMessage);
                }

                UrlsViewModel model = response.GetValue("Result").ToObject<UrlsViewModel>();
                model.SetSharedData(User);
                return View(model);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(Utility.LogError(ex), "Internal Server Error");
            }
        }

        [HttpPost]
        public ActionResult Urls(UrlsViewModel model)
        {
            try
            {
                var response = AsyncHelpers.RunSync<JObject>(() => ApiCall.CallApi("api/Settings/SetUrls", User, model));

                if (response is Error || response == null)
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Internal Server Error");

                return Json("Success");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(Utility.LogError(ex), "Internal Server Error");
            }
        }
    }


}