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
    public class DriverLocationController : Controller
    {
        // GET: Dashboard/Users
        public ActionResult Index()
        {
            ChatHistoryViewModel _model = new ChatHistoryViewModel();            
            _model.SetSharedData(User);
            return View(_model);
        }               

    }
}