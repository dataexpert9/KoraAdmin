using BasketWebPanel.Areas.Dashboard.ViewModels;
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
    [Authorize]
    public class UsersController : Controller
    {
        // GET: Dashboard/Users
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ManageUsers()
        {
            var response = AsyncHelpers.RunSync<JObject>(() => ApiCall.CallApi("api/Admin/GetAllUsers", User, null, true, false, null));

            SearchUserViewModel model = new SearchUserViewModel();

            if (response is Error)
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Internal Server Error");
            else
                model = response.GetValue("result").ToObject<SearchUserViewModel>();

            foreach (var user in model.Users)
            {
                user.StatusName = user.IsDeleted ? "Blocked" : "Active";
                //if (user.ProfilePictureUrl == null || user.ProfilePictureUrl == "")
                //    user.ProfilePictureUrl = "UserImages/Default.png";
            }
            model.StatusOptions = Utility.GetUserStatusOptions();

            model.SetSharedData(User);

         //   model.Users = model.Users.OrderByDescending(x => x.Id).ToList();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveUserStatuses(List<ChangeUserStatusModel> selectedUsers)
        {
            try
            {
                if (selectedUsers == null)
                {
                    return new HttpStatusCodeResult((int)HttpStatusCode.Forbidden, "Select a user to save");
                }

                ChangeUserStatusListModel postModel = new ChangeUserStatusListModel();
                postModel.Users = selectedUsers;

                var apiResponse = AsyncHelpers.RunSync<JObject>(() => ApiCall.CallApi("api/Admin/ChangeUserStatuses", User, postModel));

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

        public ActionResult GetUserDetails(int User_Id)
        {
            try
            {
                var response = AsyncHelpers.RunSync<JObject>(() => ApiCall.CallApi("api/User/GetUserDetails", User, null, true, false, null, "User_Id=" + User_Id));

                UserDetailsViewModel model = null;

                if (response is Error)
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Internal Server Error");
                else
                    model = response.GetValue("Result").ToObject<UserDetailsViewModel>();

                if (model.ProfilePictureUrl == null || model.ProfilePictureUrl == "")
                    model.ProfilePictureUrl = "UserImages/Default.png";
           
                model.SetSharedData(User);

                return View("UserProfile", model);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(Utility.LogError(ex), "Internal Server Error");
            }
        }

        public ActionResult GetUser(int UserId)
        {
            try
            {
                var response = AsyncHelpers.RunSync<JObject>(() => ApiCall.CallApi("api/Admin/GetUser", User, null, true, false, null, "User_Id=" + UserId));

                UserDataViewModel model = null;

                if (response is Error)
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Internal Server Error");
                else
                    model = response.GetValue("result").ToObject<UserDataViewModel>();

                if (model.ProfilePictureUrl == null || model.ProfilePictureUrl == "")
                    model.ProfilePictureUrl = "UserImages/Default.png";
                //foreach (var subscription in model.UserSubscriptions)
                //{
                //    subscription.Box.CategoryName = Utility.GetBoxCategoryName(subscription.Box.BoxCategory_Id);

                //}

                //foreach (var order in model.Orders)
                //{
                //    order.PaymentMethodName = Utility.GetPaymentMethodName(order.PaymentMethod);
                //    order.PaymentStatusName = Utility.GetPaymentStatusName(order.PaymentStatus);
                //}


                //model.UserAddresses = model.UserAddresses.Where(x => x.IsDeleted == false).ToList();
                //model.PaymentCards = model.PaymentCards.Where(x => x.IsDeleted == false).ToList();

                //foreach (var notification in model.Notifications)
                //{
                //    notification.StatusName = notification.Status == 0 ? "Unread" : "Read";
                //}
                model.SetSharedData(User);

                return View("User", model);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(Utility.LogError(ex), "Internal Server Error");
            }
        }

        public ActionResult Group(int Group_Id)
        {
            try
            {
                var response = AsyncHelpers.RunSync<JObject>(() => ApiCall.CallApi("api/Admin/GetGroupData", User, null, true, false, null, "Group_Id=" + Group_Id));

                GroupDataViewModel model = null;

                if (response is Error)
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Internal Server Error");
                else
                    model = response.GetValue("Result").ToObject<GroupDataViewModel>();

                if (model.ProfilePictureUrl == null || model.ProfilePictureUrl == "")
                    model.ProfilePictureUrl = "UserImages/Default.png";
              
                model.SetSharedData(User);

                return View("Group", model);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(Utility.LogError(ex), "Internal Server Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendNotificationToUser(UserBindingModel model)
        {
            try
            {
                var apiResponse = AsyncHelpers.RunSync<JObject>(() => ApiCall.CallApi("api/Admin/SendNotificationToUser", User, model.SendNotification));
                if (apiResponse is Error || apiResponse == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Internal Server Error");
                }
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(Utility.LogError(ex), "Internal Server Error");
            }
        }

    }
}