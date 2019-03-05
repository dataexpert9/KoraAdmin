using BasketWebPanel.BindingModels;
using BasketWebPanel.ViewModels;
using Highsoft.Web.Mvc.Charts;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Data.Entity;
using System.Linq;

namespace BasketWebPanel.Areas.Dashboard.Controllers
{
    public class InsightsController : Controller
    {
        public async Task<ActionResult> Sales()
        {
            try
            {
                
                ListOrderSalesGraph model;

                var response = AsyncHelpers.RunSync<JObject>(() => ApiCall.CallApi("api/Admin/GetSalesGraphData", User, null, true));

                if (response is Error || response == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Internal Server Error");
                }

                model = response.GetValue("Result").ToObject<ListOrderSalesGraph>();
                
                List<LineSeriesData> lstOrdersData = new List<LineSeriesData>();
                var yAxis = model.Orders.Select(x => new LineSeriesData
                {
                    Y = x.Total
                }).ToList();

                var xAxis = model.Orders.Select(x => x.OrderDateTime.ToString("dd/MM/yyyy")).ToList();

                #region Commented
                //List<LineSeriesData> lstTestUsa = new List<LineSeriesData>();
                //lstTestUsa.Add(new LineSeriesData
                //{
                //    Y = 6
                //});
                //lstTestUsa.Add(new LineSeriesData
                //{
                //    Y = 11
                //});

                //List<LineSeriesData> lstTestRussia = new List<LineSeriesData>();
                //lstTestRussia.Add(new LineSeriesData
                //{
                //    Y = 8,

                //});
                //lstTestRussia.Add(new LineSeriesData
                //{
                //    Y = 11
                //}); 
                #endregion

                ViewData["sales"] = yAxis;
                ViewData["xAxis"] = xAxis;
                //ViewData["russiaData"] = lstTestRussia;
                Global.sharedDataModel.SetSharedData(User);
                return View(Global.sharedDataModel);
            }
            catch (Exception ex)
            {
                Utility.LogError(ex);
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Internal Server Error");
            }
        }
    }
}
