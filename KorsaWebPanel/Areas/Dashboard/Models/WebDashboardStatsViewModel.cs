using BasketWebPanel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasketWebPanel.Areas.Dashboard.Models
{
    public class WebDashboardStatsViewModel : BaseViewModel
    {
        public WebDashboardStatsViewModel()
        {
            DeviceUsage = new List<DeviceStats>();
        }

        public int TotalUsers { get; set; }

        public int TotalDrivers { get; set; }

        public int TotalRides { get; set; }

        public int TotalVehicles { get; set; }


        public List<DeviceStats> DeviceUsage { get; set; }
    }

    public class DeviceStats
    {
        public int Count { get; set; }
        public int Percentage { get; set; }
    }
}