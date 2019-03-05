using BasketWebPanel.Areas.Dashboard.ViewModels;
using BasketWebPanel.BindingModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BasketWebPanel.Areas.Dashboard.Controllers
{
    [Authorize]
    public class DriversController : Controller
    {
        // GET: Dashboard/Drivers
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ManageDrivers()
        {
            var response = AsyncHelpers.RunSync<JObject>(() => ApiCall.CallApi("api/Admin/GetAllDrivers", User, null, true, false, null));

            SearchDriverViewModel model = new SearchDriverViewModel();

            if (response is Error)
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Internal Server Error");
            else
                model = response.GetValue("result").ToObject<SearchDriverViewModel>();

            foreach (var user in model.Drivers)
            {
                user.StatusName = user.IsDeleted ? "Blocked" : "Active";
            }

            model.StatusOptions = Utility.GetUserStatusOptions();

            model.SetSharedData(User);
            return View(model);
        }



        public ActionResult ManageDriversRequests()
        {
            var response = AsyncHelpers.RunSync<JObject>(() => ApiCall.CallApi("api/Admin/GetAllDrivers", User, null, true, false, null, "isBecomeDriverRequests="+ true));

            SearchDriverViewModel model = new SearchDriverViewModel();

            if (response is Error)
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Internal Server Error");
            else
                model = response.GetValue("result").ToObject<SearchDriverViewModel>();

            foreach (var user in model.Drivers)
            {
                user.StatusName = "Pending";
            }

            model.StatusOptions = Utility.GetDriverRequestStatusOptions();

            model.SetSharedData(User);
            return View(model);
        }

        public ActionResult GetDriver(int Driver_Id)
        {
            var response = AsyncHelpers.RunSync<JObject>(() => ApiCall.CallApi("api/Admin/GetDriver", User, null, true, false, null, "Driver_Id=" + Driver_Id));

            DriverViewModel model = new DriverViewModel();

            if (response is Error)
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Internal Server Error");
            else
                model = response.GetValue("result").ToObject<DriverViewModel>();

            model.SetSharedData(User);
            return View("Driver",model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveDriverStatuses(List<ChangeUserStatusModel> selectedUsers)
        {
            try
            {
                if (selectedUsers == null)
                {
                    return new HttpStatusCodeResult((int)HttpStatusCode.Forbidden, "Select a user to save");
                }

                ChangeUserStatusListModel postModel = new ChangeUserStatusListModel();
                postModel.Users = selectedUsers;

                var apiResponse = AsyncHelpers.RunSync<JObject>(() => ApiCall.CallApi("api/Admin/SaveDriverStatuses", User, postModel));

                if (apiResponse == null || apiResponse is Error)
                    return new HttpStatusCodeResult(500, "Internal Server Error");
                else
                {
                    return Json("Success");
                }

            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(Utility.LogError(ex), "Internal Server Error");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveDriverRequestStatuses(List<ChangeUserStatusModel> selectedUsers)
        {
            try
            {
                if (selectedUsers == null)
                {
                    return new HttpStatusCodeResult((int)HttpStatusCode.Forbidden, "Select a request to perform action");
                }

                ChangeUserStatusListModel postModel = new ChangeUserStatusListModel();
                postModel.Users = selectedUsers;

                var apiResponse = AsyncHelpers.RunSync<JObject>(() => ApiCall.CallApi("api/Admin/SaveDriverRequestStatuses", User, postModel));

                if (apiResponse == null || apiResponse is Error)
                    return new HttpStatusCodeResult(500, "Internal Server Error");
                else
                {
                    return Json("Success");
                }

            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(Utility.LogError(ex), "Internal Server Error");
            }
        }
    }
}