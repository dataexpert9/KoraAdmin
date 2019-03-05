using KorsaWebPanel.Areas.Dashboard.BindingModels;
using BasketWebPanel.Areas.Dashboard.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Net;
using BasketWebPanel.ViewModels;
using BasketWebPanel.BindingModels;
using KorsaWebPanel.Areas.Dashboard.ViewModels;

namespace BasketWebPanel.Areas.Dashboard.Controllers
{
    public class SubscriptionPackagesController : Controller
    {
        
        public ActionResult Index(int id = 0)
        {
            if (id == 0)
            {
                AddSubscriptionPackageBindingModel model = new AddSubscriptionPackageBindingModel();
                model.DurationTypeOptions = Utility.GetDurationTypeOptions();
                model.SetSharedData(User);
                return View(model);
            }
            else
            {
                var response = AsyncHelpers.RunSync<JObject>(() => ApiCall.CallApi("/api/Admin/GetEntityById", User, null, true, false, null,"Id="+id+"&EntityType=2"));

                AddSubscriptionPackageBindingModel model = new AddSubscriptionPackageBindingModel();

                if (response is Error)
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Internal Server Error");
                else
                    model = response.GetValue("result").ToObject<AddSubscriptionPackageBindingModel>();
                    model.DurationTypeOptions = Utility.GetDurationTypeOptions();
                    model.SetSharedData(User);
                    return View(model);
            }
        }

        public async Task<ActionResult> AddSubscription(AddSubscriptionPackageBindingModel model)
        {
            if (ModelState.IsValid) {
                
                JObject response;
                response = await ApiCall.CallApi("api/Admin/AddSubscriptionPackage", User, model);
                if (response.ToString().Contains("UnAuthorized"))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "UnAuthorized Error");
                }

                else if (response is Error)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, (response as Error).ErrorMessage);
                }

                return RedirectToAction("ManageSubscriptions", "SubscriptionPackages");
            }
            else
            {
                return View();
            }
           
        }

        public ActionResult ManageSubscriptions()
        {
            var response = AsyncHelpers.RunSync<JObject>(() => ApiCall.CallApi("/api/Driver/GetAllSubscriptionPackage", User, null, true, false, null));

            SubscriptionModelList model = new SubscriptionModelList();

            if (response is Error)
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Internal Server Error");
            else
                model = response.GetValue("result").ToObject<SubscriptionModelList>();
                model.SetSharedData(User);

            

            return View(model);
        }


        public async Task<ActionResult> Delete(int id)
        {
            JObject response;
            response = await ApiCall.CallApi("/api/Admin/DeleteSubscriptionPackage", User,null,true,false,null,"id="+id);
            if (response.ToString().Contains("UnAuthorized"))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "UnAuthorized Error");
            }

            else if (response is Error)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, (response as Error).ErrorMessage);
            }

            return RedirectToAction("ManageSubscriptions", "SubscriptionPackages");
        }


    }
}