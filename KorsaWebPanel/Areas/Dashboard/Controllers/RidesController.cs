using BasketWebPanel.Areas.Dashboard.ViewModels;
using BasketWebPanel.BindingModels;
using KorsaWebPanel.Areas.Dashboard.BindingModels;
using KorsaWebPanel.Areas.Dashboard.ViewModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BasketWebPanel.Areas.Dashboard.Controllers
{

    public class RidesController : Controller
    {
        // GET: Dashboard/Requests
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult ManageRides()
        {
            var response = AsyncHelpers.RunSync<JObject>(() => ApiCall.CallApi("api/Admin/GetAllRides", User, null, true, false, null));

            SearchRequestViewModel model = new SearchRequestViewModel();

            if (response is Error)
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Internal Server Error");
            else
                model = response.GetValue("result").ToObject<SearchRequestViewModel>();

            //foreach (var user in model.Drivers)
            //{
            //    user.StatusName = user.IsDeleted ? "Blocked" : "Active";
            //}

           // model.StatusOptions = Utility.GetUserStatusOptions();

            model.SetSharedData(User);
            return View(model);
        }


        public ActionResult ManageVehicleTypes()
        {
            var response = AsyncHelpers.RunSync<JObject>(() => ApiCall.CallApi("api/Ride/GetAllRideTypes", User, null, true, false, null));

            VehicleTypeListViewModel model = new VehicleTypeListViewModel();

            if (response is Error)
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Internal Server Error");
            else
                model = response.GetValue("result").ToObject<VehicleTypeListViewModel>();


            model.SetSharedData(User);

            //   model.Users = model.Users.OrderByDescending(x => x.Id).ToList();

            return View(model);
        }

        public ActionResult VehicleTypeIndex(int id = 0)
        {
            VehiclesTypesBindingModel model = new VehiclesTypesBindingModel();
            model.SetSharedData(User);
            if (id == 0)
            {
               
                return View(model);
            }
            else
            {
                var response = AsyncHelpers.RunSync<JObject>(() => ApiCall.CallApi("/api/Admin/GetEntityById", User, null, true, false, null, "Id=" + id + "&EntityType="+Utility.KorsaEntityTypes.RideType));
                if (response is Error)
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Internal Server Error");
                else
                    model = response.GetValue("result").ToObject<VehiclesTypesBindingModel>();
                    model.SetSharedData(User);
                return View(model);
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddVehicleType(VehiclesTypesBindingModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.DefaultImageFile != null)
                {
                    string theFileName = Path.GetFileName(model.DefaultImageFile.FileName);
                    byte[] thePictureAsBytes = new byte[model.DefaultImageFile.ContentLength];
                    using (BinaryReader theReader = new BinaryReader(model.DefaultImageFile.InputStream))
                    {
                        thePictureAsBytes = theReader.ReadBytes(model.DefaultImageFile.ContentLength);
                    }
                    model.DefaultImage = new PictureModel();
                    string picturestring = Convert.ToBase64String(thePictureAsBytes);
                    model.DefaultImage.ImageString = picturestring;
                    model.DefaultImage.Name = theFileName;
                    model.DefaultImageFile = null;
                }
                if (model.SelectedImageFile != null)
                {
                    string theFileName1 = Path.GetFileName(model.SelectedImageFile.FileName);
                    byte[] thePictureAsBytes1 = new byte[model.SelectedImageFile.ContentLength];
                    using (BinaryReader theReader = new BinaryReader(model.SelectedImageFile.InputStream))
                    {
                        thePictureAsBytes1 = theReader.ReadBytes(model.SelectedImageFile.ContentLength);
                    }
                    model.SelectedImageFile = null;
                    model.SelectedImage = new PictureModel();
                    model.SelectedImage.ImageString = Convert.ToBase64String(thePictureAsBytes1);
                    model.SelectedImage.Name = theFileName1;
                    model.SelectedImageFile = null;
                }
                JObject response;
                response = await ApiCall.CallApi("/api/Admin/AddEditRideType", User, model);
                if (response.ToString().Contains("UnAuthorized"))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "UnAuthorized Error");
                }

                else if (response is Error)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, (response as Error).ErrorMessage);
                }

                return RedirectToAction("ManageVehicleTypes", "Rides");
            }
            else
            {
                return View(); 
            }

        }


        public async Task<ActionResult> DeleteVehicleType(int id)
        {
            JObject response;
            response = await ApiCall.CallApi("/api/Admin/DeleteRideType", User, null, true, false, null, "id=" + id);
            if (response.ToString().Contains("UnAuthorized"))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "UnAuthorized Error");
            }

            else if (response is Error)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, (response as Error).ErrorMessage);
            }

            return RedirectToAction("ManageVehicleTypes", "Rides");
        }

    }
}