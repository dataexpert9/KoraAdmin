using BasketWebPanel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KorsaWebPanel.Areas.Dashboard.ViewModels
{
    public class SubscriptionModelList : BaseViewModel
    {
        public List<SubscriptionViewModel> subscriptionPackagesList { get; set; }
    }
    public class SubscriptionViewModel : BaseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumOfRides { get; set; }
        public double Price { get; set; }
        public int Duration { get; set; }
        public int DurationType { get; set; }
    }
}