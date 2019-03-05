using BasketWebPanel.BindingModels;
using BasketWebPanel.ViewModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using static BasketWebPanel.Utility;

namespace BasketWebPanel.Areas.Dashboard.Controllers
{
    [Authorize]
    public class BoxController : Controller
    {
        // GET: Dashboard/Box
        //public ActionResult Index(int? BoxId)
        //{
        //    try
        //    {
        //        AddBoxViewModel model = new AddBoxViewModel();
        //        model.BoxVideos.Add(new Video { VideoUrl = "" });
        //        model.ReleaseDate = DateTime.Now;

        //        if (BoxId > 0)
        //        {
        //            var response = AsyncHelpers.RunSync<JObject>(() => ApiCall.CallApi("api/GetEntityById", User, null, true, false, null, "EntityType=" + (int)KorsaEntityTypes.Box, "Id=" + BoxId.Value));
        //            model = response.GetValue("Result").ToObject<AddBoxViewModel>();
        //        }

        //        List<SelectListItem> catOptions = new List<SelectListItem>();
        //        catOptions.Add(new SelectListItem { Text = "Junior", Value = "1" });
        //        catOptions.Add(new SelectListItem { Text = "Monthly", Value = "2" });
        //        catOptions.Add(new SelectListItem { Text = "Pro Box", Value = "3" });

        //        model.Categories = new SelectList(catOptions);


        //        model.SetSharedData(User);
        //        return View(model);
        //    }
        //    catch (Exception ex)
        //    {
        //        Utility.LogError(ex);
        //        return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Internal Server Error");
        //    }
        //}

        [HttpPost]
        public ActionResult Index(AddBoxViewModel model)
        {
            if (model.BoxVideos == null || model.BoxVideos.Count == 0 || model.BoxVideos.First().VideoUrl == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Please add at least one video url.");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model.BoxVideos.RemoveAll(x => String.IsNullOrEmpty(x.VideoUrl));
         

            var response = AsyncHelpers.RunSync<JObject>(() => ApiCall.CallApi("api/Admin/AddBox", User, model));

            if (response is Error)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, (response as Error).ErrorMessage);
            }

            if (model.Id > 0)
                TempData["SuccessMessage"] = "The box has been updated successfully.";
            else
                TempData["SuccessMessage"] = "The box has been added successfully.";

            return Json("Success");
        }

        public ActionResult ManageBoxes()
        {
            try
            {
                var response = AsyncHelpers.RunSync<JObject>(() => ApiCall.CallApi("api/Admin/GetAllBoxes", User, null, true));

                if (response is Error)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Internal Server Error");
                }

                var boxes = response.GetValue("Result").ToObject<SearchBoxesViewModel>();

                foreach (var box in boxes.Boxes)
                {
                    box.StatusName = Utility.GetStatusName(box.Status);
                    //box.CategoryName = ((BoxCategoryOptions)box.BoxCategory_Id).ToString();
                }

                boxes.StatusOptions = Utility.GetBoxStatusOptions();
                boxes.SetSharedData(User);

                return View(boxes);
            }
            catch (Exception ex)
            {
                Utility.LogError(ex);
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Internal Server Error");
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveBoxStatuses(List<ChangeBoxStatusModel> selectedBoxes)
        {
            try
            {
                if (selectedBoxes == null)
                {
                    return new HttpStatusCodeResult((int)HttpStatusCode.Forbidden, "Select a box to save");
                }

                ChangeBoxStatusListModel postModel = new ChangeBoxStatusListModel();
                postModel.Boxes = selectedBoxes;

                var apiResponse = AsyncHelpers.RunSync<JObject>(() => ApiCall.CallApi("api/Admin/ChangeBoxStatuses", User, postModel));

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

        //public JsonResult DeleteBox(int BoxId)
        //{
        //    try
        //    {
        //        var response = AsyncHelpers.RunSync<JObject>(() => ApiCall.CallApi("api/Admin/DeleteEntity", User, null, true, false, null, "EntityType=" + (int)KorsaEntityTypes.Box, "Id=" + BoxId));
        //        if (response is Error)
        //            return Json("An error has occurred, error code : 500", JsonRequestBehavior.AllowGet);
        //        else
        //            return Json("Success", JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}
    }
}