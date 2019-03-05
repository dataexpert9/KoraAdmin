using BasketWebPanel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasketWebPanel.Areas.Dashboard.ViewModels
{
    public class SearchRequestViewModel : BaseViewModel
    {
        public SearchRequestViewModel()
        {
            Rides = new List<RidesVM>();
        }
        public List<RidesVM> Rides { get; set; }
    }

    public class Locations
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
    public enum TripStatus
    {
        Requested = 1,
        AssignedToDriver = 2,
        Arrived = 3,
        Started = 4,
        Completed = 5,
        CancelledByDriver = 6,
        CancelledByUser = 7,

    }

    public class RidesVM
    {
        public int Id { get; set; }
        public Locations PickupLocation { get; set; }
        public Locations DestinationLocation { get; set; }
        public string PickupLocationName { get; set; }
        public string DestinationLocationName { get; set; }
        public float EstimatedFare { get; set; }
        public float FarePerKm { get; set; }
        public float PeakFactor { get; set; }
        public short DriverRating { get; set; }
        public short UserRating { get; set; }
        public float Fare { get; set; }
        public float Discount { get; set; }
        public DateTime PickupDateTime { get; set; }
        public TripStatus Status { get; set; }
        public bool isScheduled { get; set; }
        public DateTime RequestTime { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string ImageUrl { get; set; }
        public int? Promocode_Id { get; set; }

        public string RideTypeName { get; set; }
        public int? PrimaryUser_Id { get; set; }
        public UserBindingModel PrimaryUser { get; set; }
        public int RideType_Id { get; set; }
        //public RideTypeDTO RideType { get; set; }
        public int Driver_Id { get; set; }
        public DriverVM Driver { get; set; }
    }

    public class RequestItemImages
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }

        public int RequestItem_Id { get; set; }
    }
    public class StoreVM
    {
        public int Id { get; set; }

        public string StoreDescription { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string About { get; set; }

        public TimeSpan Open_From { get; set; }

        public TimeSpan Open_To { get; set; }

        public double AverageRating { get; set; }

        public string RatingText { get; set; }

        public string ImageUrl { get; set; }

        public string ContactNumber { get; set; }

        public int AverageDeliveryTime { get; set; }

        public float? MinOrder { get; set; }

        public double DeliveryFee { get; set; }

        public int PaymentMethod { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsFeature { get; set; }

        public DateTime? CreatedAt { get; set; }

    }
}