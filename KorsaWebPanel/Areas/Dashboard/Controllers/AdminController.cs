using BasketWebPanel.Areas.Dashboard.BindingModels;
using BasketWebPanel.Areas.Dashboard.Models;
using BasketWebPanel.BindingModels;
using BasketWebPanel.ViewModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static BasketWebPanel.Utility;

namespace BasketWebPanel.Areas.Dashboard.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        [HttpPost]
        public JsonResult DeleteImageOnEdit()
        {
            return Json("Success");
        }

        [HttpPost]
        public async Task<JObject> UploadImage(HttpPostedFileBase files)
        {

            //var ImageFileProduct = Request.Files;
            //ByteArrayContent fileContent;
            //List<byte[]> productImages = new List<byte[]>();
            //byte[] fileDataProductImage = null;
            //MultipartFormDataContent content = new MultipartFormDataContent();
            //foreach (string fileName in ImageFileProduct)
            //{
            //    HttpPostedFileBase file = ImageFileProduct[fileName];
            //    if (file.ContentLength > 0)
            //    {
            //        using (var binaryReader = new BinaryReader(file.InputStream))
            //        {
            //            fileDataProductImage = binaryReader.ReadBytes(file.ContentLength);
            //            productImages.Add(fileDataProductImage);
            //        }
            //    }
            //}
            //for (int i = 0; i < productImages.Count; i++)
            //{
            //    fileContent = new ByteArrayContent(productImages[i]);
            //    fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = "Product_" + i + Path.GetExtension(ImageFileProduct[0].FileName) };
            //    content.Add(fileContent, "ProductImage");
            //}
            //// JObject response;
            //JObject response = await ApiCall.CallApi("api/Common/UploadImages", User, isMultipart: true, multipartContent: content);
            //return response;

            try
            {
                UploadImageBindingModel model = new UploadImageBindingModel();

                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0];
                    model.Image= file;
                    //if (file != null && file.ContentLength > 0)
                    //{
                    //    model.Image.FileName = Path.GetFileName(file.FileName);
                    //    var path = Path.Combine(Server.MapPath("~/Images/"), model.Image.FileName);

                    //    byte[] thePictureAsBytes = new byte[file.ContentLength];
                    //    using (BinaryReader theReader = new BinaryReader(file.InputStream))
                    //    {
                    //        model.Image.Buffer = theReader.ReadBytes(file.ContentLength);
                    //    }

                    //}
                }
                JObject response = await ApiCall.CallApi("api/Common/UploadImages", User, model);
                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost]
        public JsonResult DeleteImage()
        {
            Request.RequestContext.HttpContext.Session.Remove("AddAdminImage");
            Request.RequestContext.HttpContext.Session.Add("ImageDeletedOnEdit", true);
            return Json("Session Cleared");
        }

        public ActionResult Index(int? AdminId)
        {
            Request.RequestContext.HttpContext.Session.Remove("AddAdminImage");
            Request.RequestContext.HttpContext.Session.Remove("ImageDeletedOnEdit");
            AddAdminViewModel model = new AddAdminViewModel();

            model.StoreOptions = Utility.GetStoresOptions(User);
            model.SetSharedData(User);

            if (model.Role == RoleTypes.SubAdmin)
            {
                AdminId = model.Id;
            }

            if (AdminId.HasValue)
            {
                var responseAdmin = AsyncHelpers.RunSync<JObject>(() => ApiCall.CallApi("api/Admin/GetEntityById", User, null, true, false, null, "EntityType=" + (int)KorsaEntityTypes.Admin, "Id=" + AdminId.Value));
                if (responseAdmin == null || responseAdmin is Error)
                    ;
                else
                {
                    model.Admin = responseAdmin.GetValue("result").ToObject<AdminViewModel>();
                }
            }


            model.RoleOptions = new SelectList(
                new List<SelectListItem> {
                    new SelectListItem { Text = Utility.RoleTypes.SubAdmin.ToString(), Value = Utility.RoleTypes.SubAdmin.ToString("D") }
                    //new SelectListItem { Text = Utility.RoleTypes.SuperAdmin.ToString(), Value = Utility.RoleTypes.SuperAdmin.ToString("D") }
                });


            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(AddAdminViewModel model)
        {
            model.SetSharedData(User);

            if (model.Admin.Id == 0)
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
            }


            MultipartFormDataContent content;

            //bool FileAttached = (Request.RequestContext.HttpContext.Session["AddAdminImage"] != null);
            //bool ImageDeletedOnEdit = false;
            //var imgDeleteSessionValue = Request.RequestContext.HttpContext.Session["ImageDeletedOnEdit"];
            //if (imgDeleteSessionValue != null)
            //{
            //    ImageDeletedOnEdit = Convert.ToBoolean(imgDeleteSessionValue);
            //}
            //byte[] fileData = null;
            //var ImageFile = (HttpPostedFileWrapper)Request.RequestContext.HttpContext.Session["AddAdminImage"];
            //if (FileAttached)
            //{
            //    using (var binaryReader = new BinaryReader(ImageFile.InputStream))
            //    {

            //        fileData = binaryReader.ReadBytes(ImageFile.ContentLength);
            //    }
            //}

            //   ByteArrayContent fileContent;
            JObject response;

            bool firstCall = true;
            callAgain: content = new MultipartFormDataContent();
            //if (FileAttached)
            //{
            //    fileContent = new ByteArrayContent(fileData);
            //    fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = ImageFile.FileName };
            //    content.Add(fileContent);
            //}

            //if (model.Admin.Id > 0)
            //    content.Add(new StringContent(model.Admin.Id.ToString()), "Id");

            //if (model.Admin.Store_Id.HasValue)
            //    content.Add(new StringContent(model.Admin.Store_Id.Value.ToString()), "Store_Id");

            //content.Add(new StringContent(model.Admin.firstName), "FirstName");
            //content.Add(new StringContent(model.Admin.LastName), "LastName");
            //content.Add(new StringContent(model.Admin.Email), "Email");
            //content.Add(new StringContent(model.Admin.Phone), "Phone");
            //content.Add(new StringContent(model.Admin.ImageUrl), "ImageUrl");

            //content.Add(new StringContent(model.Admin.Role.ToString()), "Role");


            //if (model.Admin.Id == 0)
            //    content.Add(new StringContent(model.Admin.Password), "Password");

            //    content.Add(new StringContent(Convert.ToString(ImageDeletedOnEdit)), "ImageDeletedOnEdit");
            response = await ApiCall.CallApi("api/Admin/AddAdmin", User,model.Admin);

            if (firstCall && response.ToString().Contains("UnAuthorized"))
            {
                firstCall = false;
                goto callAgain;
            }
            else if (response.ToString().Contains("UnAuthorized"))
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "UnAuthorized Error");
            }

            if (response is Error)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, (response as Error).ErrorMessage);
            }
            else
            {
                model.Admin = response.GetValue("result").ToObject<AdminViewModel>();
                var claimIdentity = ((ClaimsIdentity)User.Identity);
                if (model.Admin.Id == Convert.ToInt32(claimIdentity.Claims.FirstOrDefault(x => x.Type == "AdminId").Value))
                {
                    User.AddUpdateClaim("FullName", model.Admin.firstName + " " + model.Admin.LastName);
                    User.AddUpdateClaim("ProfilePictureUrl", model.Admin.ImageUrl);
                }
                model.SetSharedData(User);

                return Json(new { success = true, responseText = "Success" }, JsonRequestBehavior.AllowGet);
                //return RedirectToAction("Index");
            }
        }

        public ActionResult ManageAdmins()
        {
            Global.sharedDataModel.SetSharedData(User);
            return View(Global.sharedDataModel);
        }

        public ActionResult SearchAdmin()
        {
            SearchAdminModel returnModel = new SearchAdminModel();

            var responseStores = AsyncHelpers.RunSync<JObject>(() => ApiCall.CallApi("api/GetAllStores", User, GetRequest: true));
            if (responseStores == null || responseStores is Error)
            {
            }
            else
            {
                var Stores = responseStores.GetValue("Result").ToObject<List<StoreBindingModel>>();
                IEnumerable<SelectListItem> selectList = from store in Stores
                                                         select new SelectListItem
                                                         {
                                                             Selected = false,
                                                             Text = store.Name,
                                                             Value = store.Id.ToString()
                                                         };
                Stores.Insert(0, new StoreBindingModel { Id = 0, Name = "All" });

                returnModel.StoreOptions = new SelectList(selectList);
            }
            return PartialView("_SearchAdmin", returnModel);
        }

        public ActionResult SearchAdminResults(SearchAdminModel model)
        {
            SearchAdminsViewModel returnModel = new SearchAdminsViewModel();
            var response = AsyncHelpers.RunSync<JObject>(() => ApiCall.CallApi("api/Admin/SearchAdmins", User, null, true, false, null, "FirstName=" + model.FirstName + "", "LastName=" + model.LastName, "Email=" + model.Email, "Phone=" + model.Phone, "StoreId=" + model.StoreId));
            if (response is Error)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, (response as Error).ErrorMessage);
            }
            else
            {
                returnModel = response.GetValue("Result").ToObject<SearchAdminsViewModel>();
            }
            foreach (var admin in returnModel.Admins)
            {
                admin.RoleName = ((RoleTypes)admin.Role).ToString();
            }
            returnModel.SetSharedData(User);
            return PartialView("_SearchAdminResults", returnModel);
        }

        public JsonResult DeleteAdmin(int AdminId)
        {
            try
            {
                var response = AsyncHelpers.RunSync<JObject>(() => ApiCall.CallApi("api/Admin/DeleteEntity", User, null, true, false, null, "EntityType=" + (int)KorsaEntityTypes.Admin, "Id=" + AdminId));
                if (response is Error)
                    return Json("An error has occurred, error code : 500", JsonRequestBehavior.AllowGet);
                else
                    return Json("Success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}