using BasketWebPanel.Areas.Dashboard.Models;
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
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static BasketWebPanel.Utility;

namespace BasketWebPanel.Areas.Dashboard.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        [HttpPost]
        public JsonResult DeleteImageOnEdit()
        {
            return Json("Success");
        }

        [HttpPost]
        public async Task<JObject> UploadImage(HttpPostedFileBase files)
        {
            //if (Request.Files.Count > 0)
            //{
            //    Request.RequestContext.HttpContext.Session.Remove("AddProductImage");
            //    Request.RequestContext.HttpContext.Session.Add("AddProductImage", Request.Files);
            //}

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

        [HttpPost]
        public JsonResult DeleteImage()
        {
            Request.RequestContext.HttpContext.Session.Remove("AddProductImage");
            Request.RequestContext.HttpContext.Session.Add("AddProductImage", true);
            return Json("Session Cleared");
        }

        public JsonResult FetchCategories(int storeId) // its a GET, not a POST
        {
            var response = AsyncHelpers.RunSync<JObject>(() => ApiCall.CallApi("api/GetAllCategoriesByStoreId", User, GetRequest: true, parameters: "StoreId=" + storeId));
            var responseCategories = response.GetValue("Result").ToObject<List<CategoryBindingModel>>();
            var tempCats = responseCategories.ToList();

            foreach (var cat in responseCategories)
            {
                cat.Name = cat.GetFormattedBreadCrumb(tempCats);
            }

            responseCategories.Insert(0, new CategoryBindingModel { Id = 0, Name = "None" });

            return Json(responseCategories, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult Index(int? ProductId)
        //{
        //    Request.RequestContext.HttpContext.Session.Remove("AddProductImage");
        //    Request.RequestContext.HttpContext.Session.Remove("ImageDeletedOnEdit");
        //    AddProductViewModel model = new AddProductViewModel();

        //    model.SetSharedData(User);


        //    //Providing StoresList
        //    if (ProductId.HasValue)
        //    {
        //        var responseProduct = AsyncHelpers.RunSync<JObject>(() => ApiCall.CallApi("api/GetEntityById", User, null, true, false, null, "EntityType=" + (int)KorsaEntityTypes.Product, "Id=" + ProductId.Value));
        //        if (responseProduct == null || responseProduct is Error)
        //            ;
        //        else
        //        {
        //            model.Product = responseProduct.GetValue("Result").ToObject<ProductBindingModel>();
        //            model.ProductVideos = model.Product.ProductImages.Where(x => x.IsVideo == true).ToList();
        //            model.Product.ProductImages.RemoveAll(x => x.IsVideo == true);
        //        }
        //    }

        //    //Providing CategoryList
        //    model.CategoryOptions = Utility.GetCategoryOptions(User, "None");
        //    model.WeightOptions = Utility.GetWeightOptions();
        //    if (model.ProductVideos.Count == 0)
        //    {
        //        model.ProductVideos.Add(new ProductImagesBindingModel { Url = "" });
        //    }
        //    return View(model);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(AddProductViewModel model)
        {
            try
            {
                model.Product.Description = model.Product.Description ?? "";
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                model.ProductVideos.RemoveAll(x => String.IsNullOrEmpty(x.Url));

                //model.Product.ProductImages = model.ProductImages;

                MultipartFormDataContent content;

              //  bool FileAttached = (Request.RequestContext.HttpContext.Session["AddProductImage"] != null);
              //  bool ImageDeletedOnEdit = false;
                //var imgDeleteSessionValue = Request.RequestContext.HttpContext.Session["ImageDeletedOnEdit"];
                //if (imgDeleteSessionValue != null)
                //{
                //    ImageDeletedOnEdit = Convert.ToBoolean(imgDeleteSessionValue);
                //}

              //  List<byte[]> productImages = new List<byte[]>();
              //  byte[] fileDataProductImage = null;
              //  var ImageFileProduct = (HttpFileCollectionWrapper)Request.RequestContext.HttpContext.Session["AddProductImage"];

                //if (FileAttached)
                //{
                //    foreach (string fileName in ImageFileProduct)
                //    {
                //        HttpPostedFileBase file = ImageFileProduct[fileName];
                //        if (file.ContentLength > 0)
                //        {
                //            using (var binaryReader = new BinaryReader(file.InputStream))
                //            {
                //                fileDataProductImage = binaryReader.ReadBytes(file.ContentLength);
                //                productImages.Add(fileDataProductImage);
                //            }
                //        }
                //    }
                //}

                ByteArrayContent fileContent;
                JObject response;

                bool firstCall = true;
                callAgain: content = new MultipartFormDataContent();
                //if (FileAttached)
                //{
                //    List<ByteArrayContent> contentProductImages = new List<ByteArrayContent>();

                //    for (int i = 0; i < productImages.Count; i++)
                //    {
                //        fileContent = new ByteArrayContent(productImages[i]);
                //        fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = "Product_" + i + Path.GetExtension(ImageFileProduct[0].FileName) };
                //        content.Add(fileContent, "ProductImage");
                //    }
                //}
                if (model.Product.Id > 0)
                {
                    content.Add(new StringContent(model.Product.Id.ToString()), "Id");
                }

                content.Add(new StringContent(model.Product.Name), "Name");
                content.Add(new StringContent(model.Product.Price.ToString()), "Price");
                content.Add(new StringContent(model.Product.Category_Id.ToString()), "Category_Id");
                content.Add(new StringContent(model.Product.Store_Id.ToString()), "Store_Id");
                content.Add(new StringContent(model.Product.Description), "Description");
                content.Add(new StringContent(model.Product.IsPopular.ToString()), "IsPopular");
                if (model.ProductVideos.Count > 0)
                {
                    content.Add(new StringContent(JsonConvert.SerializeObject(model.ProductVideos)), "ProductVideos");
                }
                content.Add(new StringContent(JsonConvert.SerializeObject(model.ProductImages)), "ProductImages");

                //   content.Add(new StringContent(Convert.ToString(ImageDeletedOnEdit)), "ImageDeletedOnEdit");
                response = await ApiCall.CallApi("api/Admin/AddProduct", User, isMultipart: true, multipartContent: content);
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
                    if (model.Product.Id > 0)
                        TempData["SuccessMessage"] = "The product has been updated successfully.";
                    else
                        TempData["SuccessMessage"] = "The product has been added successfully.";

                    return Json(new { success = true, responseText = "Success" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult ManageProducts()
        {
            Global.sharedDataModel.SetSharedData(User);
            return View(Global.sharedDataModel);
        }

        public ActionResult SearchProduct()
        {
            SearchProductModel returnModel = new SearchProductModel();

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

            returnModel.SetSharedData(User);
            //returnModel. returnModel.StoreId
            if (returnModel.Role == RoleTypes.SubAdmin)
            {
                returnModel.StoreId = (returnModel as BaseViewModel).StoreId;

            }



            return PartialView("_SearchProduct", returnModel);
        }

        public ActionResult SearchProductResults(SearchProductModel model)
        {
            SearchProductsViewModel returnModel = new SearchProductsViewModel();
            var response = AsyncHelpers.RunSync<JObject>(() => ApiCall.CallApi("api/Admin/SearchProducts", User, null, true, false, null, "ProductName=" + model.ProductName + "", "ProductPrice=" + model.ProductPrice, "CategoryName=" + model.CategoryName + "", "StoreId=" + Global.StoreId, "CatId=null"));
            if (response is Error)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, (response as Error).ErrorMessage);
            }
            else
            {
                returnModel = response.GetValue("Result").ToObject<SearchProductsViewModel>();
            }
            returnModel.SetSharedData(User);
            return PartialView("_SearchProductResults", returnModel);
        }

        //public JsonResult DeleteProduct(int ProductId)
        //{
        //    try
        //    {
        //        var response = AsyncHelpers.RunSync<JObject>(() => ApiCall.CallApi("api/Admin/DeleteEntity", User, null, true, false, null, "EntityType=" + (int)KorsaEntityTypes.Product, "Id=" + ProductId));
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