using BasketWebPanel.Areas.Dashboard.ViewModels;
using BasketWebPanel.BindingModels;
using BasketWebPanel.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static BasketWebPanel.Utility;

namespace BasketWebPanel.Areas.Dashboard.Controllers
{
    [Authorize]
    public class SettingsController : Controller
    {
        // GET: Dashboard/Settings
        public ActionResult Index()
        {
            Request.RequestContext.HttpContext.Session.Remove("AddBannerImage");
            Request.RequestContext.HttpContext.Session.Remove("AddInstagramImage");
            SettingsDTO returnModel = new SettingsDTO();
            var response = AsyncHelpers.RunSync<JObject>(() => ApiCall.CallApi("api/Admin/GetEntityById", User, null, true,false,null, "EntityType=" + (int)KorsaEntityTypes.Settings));
            if (response == null || response is Error)
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, (response as Error).ErrorMessage);
            else
                returnModel = response.GetValue("result").ToObject<SettingsDTO>();

            returnModel.SetSharedData(User);
            return View("Index", returnModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(SettingsDTO model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bool firstCall = true;
            
            JObject response;
          

            response = await ApiCall.CallApi("api/Admin/SetSettings", User, isMultipart: false, GetRequest:false,model:model);
            if (firstCall && Convert.ToString(response).Contains("UnAuthorized"))
            {
                firstCall = false;
            }
            else if (Convert.ToString(response).Contains("UnAuthorized"))
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "UnAuthorized Error");
            }

            if (response is Error)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, (response as Error).ErrorMessage);
            }
            else
            {
                model.SetSharedData(User);
                TempData["SuccessMessage"] = "Settings updated successfully.";
                return Json(new { success = true, responseText = "Success" }, JsonRequestBehavior.AllowGet);
            }
        }



        public ActionResult Help()
        {
            try
            {
                var response = AsyncHelpers.RunSync<JObject>(() => ApiCall.CallApi("api/Settings/GetHelp", User, null, true));

                if (response is Error)
                {
                    return new HttpStatusCodeResult((int)HttpStatusCode.InternalServerError, (response as Error).ErrorMessage);
                }

                HelpBindingModel model = response.GetValue("Result").ToObject<HelpBindingModel>();

                model.SetSharedData(User);
                model.LanguageOptions = Utility.GetLanguageOptions();

                return View(model);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(Utility.LogError(ex), "Internal Server Error");
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Help(HelpBindingModel model)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var response = AsyncHelpers.RunSync<JObject>(() => ApiCall.CallApi("api/Settings/SetHelp", User, model.Help));

                if (response is Error || response == null)
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Internal Server Error");

                return Json("Success");
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(Utility.LogError(ex), "Internal Server Error");
            }
        }












        [HttpPost]
        public async Task<ActionResult> SetProducts(SettingsSetProductViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var response = await ApiCall.CallApi("api/Settings/SetSettingsProduct", User, GetRequest: false, model: model);

            if (Convert.ToString(response).Contains("UnAuthorized"))
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "UnAuthorized Error");
            }

            if (response is Error)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, (response as Error).ErrorMessage);
            }
            else
            {
                if (model.Product_Id > 0)
                    return Json(new { success = true, responseText = "Added" }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new { success = true, responseText = "Removed" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetProducts()
        {
            SettingsSelectProductViewModel productList = new SettingsSelectProductViewModel();
            var response = AsyncHelpers.RunSync<JObject>(() => ApiCall.CallApi("api/GetAllProducts", User, null, true, false, null, "UserId=1", "PageSize=1000", "PageNo=0"));
            if (response == null || response is Error)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, (response as Error).ErrorMessage);
            }
            else
            {
                productList = response.GetValue("Result").ToObject<SettingsSelectProductViewModel>();
            }
            return Json(productList);
        }

        public ActionResult GeneralSettings()
        {
            Global.sharedDataModel.SetSharedData(User);
            return View(Global.sharedDataModel);
        }

        [HttpPost]
        public async Task<JObject> UploadImage(HttpPostedFileBase files)
        {
            //if (Request.Files.Count > 0)
            //{
            //    if (Type == 1)
            //    {
            //        Request.RequestContext.HttpContext.Session.Remove("AddBannerImage");
            //        Request.RequestContext.HttpContext.Session.Add("AddBannerImage", Request.Files);
            //    }
            //    else if (Type == 2)
            //    {
            //        Request.RequestContext.HttpContext.Session.Remove("AddInstagramImage");
            //        Request.RequestContext.HttpContext.Session.Add("AddInstagramImage", Request.Files[0]);
            //    }
            //}
            //return Json("Success", JsonRequestBehavior.AllowGet);


            var ImageFileProduct = Request.Files;
            ByteArrayContent fileContent;
            List<byte[]> productImages = new List<byte[]>();
            byte[] fileDataProductImage = null;
            MultipartFormDataContent content = new MultipartFormDataContent();
            foreach (string fileName in ImageFileProduct)
            {
                HttpPostedFileBase file = ImageFileProduct[fileName];
                if (file.ContentLength > 0)
                {
                    using (var binaryReader = new BinaryReader(file.InputStream))
                    {
                        fileDataProductImage = binaryReader.ReadBytes(file.ContentLength);
                        productImages.Add(fileDataProductImage);
                    }
                }
            }
            for (int i = 0; i < productImages.Count; i++)
            {
                fileContent = new ByteArrayContent(productImages[i]);
                fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = "Product_" + i + Path.GetExtension(ImageFileProduct[0].FileName) };
                content.Add(fileContent, "ProductImage");
            }
            // JObject response;
            JObject response = await ApiCall.CallApi("api/UploadProductImages", User, isMultipart: true, multipartContent: content);
            return response;
        }


        [HttpGet]
        public JsonResult DeleteImage(int Type)
        {
            if (Type == 1)
            {
                Request.RequestContext.HttpContext.Session.Remove("AddBannerImage");
                Request.RequestContext.HttpContext.Session.Add("AddBannerImage", true);
            }
            else if (Type == 2)
            {
                Request.RequestContext.HttpContext.Session.Remove("AddInstagramImage");
                Request.RequestContext.HttpContext.Session.Add("AddInstagramImage", true);
            }
            return Json("Session Cleared", JsonRequestBehavior.AllowGet);
        }
    }
}