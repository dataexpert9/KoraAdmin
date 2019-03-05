using BasketWebPanel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KorsaWebPanel.Areas.Dashboard.ViewModels
{
    public class VehicleTypesViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PersonsCapacity { get; set; }
        public string AboutRideType { get; set; }
        public float FarePerKm { get; set; }
        public float BasicCharges { get; set; }
        public string DefaultImageUrl { get; set; }
        public string SelectedImageUrl { get; set; }
        public float PeakFactor { get; set; }
        public double EstimatedFare { get; set; }
    }
    public class VehicleTypeListViewModel : BaseViewModel
    {
        public  List<VehicleTypesViewModel> RideTypeList { get; set; }
    }
}