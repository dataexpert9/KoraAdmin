using BasketWebPanel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BasketWebPanel.Areas.Dashboard.ViewModels
{
    public enum Gender
    {
        Male = 1,
        Female = 2
    }
    public enum MediaType
    {
        LicenseFront = 1,
        LicenseBack = 2,
        CarImage = 3,
        RegistrationCopyImage = 4
    }
    public class DriverVM
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string DateOfBirth { get; set; }

        public string PhoneNo { get; set; }

        public string HomeAddress { get; set; }

        public string LicenseNo { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public bool IsNotificationsOn { get; set; }

        public int SignInType { get; set; }

        public string Email { get; set; }

        public bool IsAvailable { get; set; }

        public string Token { get; set; }

        public string BriefInfo { get; set; }

        public string WorkHistory { get; set; }

        public string ProfilePictureUrl { get; set; }

        public double AverageRating { get; set; }

        public int Company_Id { get; set; }

        public int Vehicle_Id { get; set; }

        public int Model_Id { get; set; }

        public string PlateNumber { get; set; }

        public string Color { get; set; }

        public string CompanyName { get; set; }

        public string VehicleName { get; set; }

        public string ModelName { get; set; }

        public string StatusName { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsChecked { get; set; }

        public DateTime CreatedDate { get; set; }
        
        public string Username { get; set; }

        public Gender Gender { get; set; }
        public DateTime LicenseExpiry { get; set; }
        public List<DriverMediaDTO> Medias { get; set; }
        public int Year_Id { get; set; }
        public int Type_Id { get; set; }
        public int Capacity_Id { get; set; }
        public List<VehicleDTO> Vehicles { get; set; }
        public float Rating { get; set; }
        public int RidesCount { get; set; }
        public int TotalMileage { get; set; }
        public string BriefIntro { get; set; }
        //----------Optionals
        public Locations Location { get; set; }
        public string CarColor { get; set; }
        public string CarName { get; set; }
        public string CarNumber { get; set; }
        public Guid UserId { get; set; }
        public string ZipCode { get; set; }
        public string InvitationCode { get; set; }
        public bool TermsAndConditions { get; set; }

    }

    public class SearchDriverViewModel : BaseViewModel
    {
        public SearchDriverViewModel()
        {
            Drivers = new List<DriverVM>();
        }
        public List<DriverVM> Drivers { get; set; }
        public SelectList StatusOptions { get; internal set; }

    }

    public class DriverViewModel : BaseViewModel
    {
        public DriverViewModel()
        {
            Driver = new DriverVM();
        }

        public DriverVM Driver { get; set; }
    }

    public class VehicleDTO
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public DateTime RegistrationExpiry { get; set; }
        public string Company { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public string Type { get; set; }
        public string Capacity { get; set; }
        public int Company_Id { get; set; }
        public int Model_Id { get; set; }
        public int Year_Id { get; set; }
        public int Type_Id { get; set; }
        public int Capacity_Id { get; set; }
        public int Driver_Id { get; set; }

        public virtual ICollection<VehicleMediaDTO> Medias { get; set; }

    }
    public class DriverMediaDTO
    {
        public int Id { get; set; }
        public MediaType Type { get; set; }
        public string MediaUrl { get; set; }
    }
    public class VehicleMediaDTO
    {
        public int Id { get; set; }
        public MediaType Type { get; set; }
        public string MediaUrl { get; set; }
    }
}