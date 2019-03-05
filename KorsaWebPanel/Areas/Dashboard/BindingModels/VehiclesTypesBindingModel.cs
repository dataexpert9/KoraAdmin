using BasketWebPanel.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KorsaWebPanel.Areas.Dashboard.BindingModels
{


    public class VehiclesTypesBindingModel : BaseViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Personal Capacity is Required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid  number greater then 0")]
        public int PersonsCapacity { get; set; }

        [Required(ErrorMessage = "Fare/Km is Required")]
        [Range(1, double.MaxValue, ErrorMessage = "Please enter valid  number greater then 0")]
        public double FarePerKm { get; set; }

        //[Required(ErrorMessage = "Peak Factor is Required")]
        //[Range(1, int.MaxValue, ErrorMessage = "Please enter valid  number greater then 0")]
        //public int peakFactor { get; set; }

        [Required(ErrorMessage = "Basic Charges value is Required")]
        [Range(1, double.MaxValue, ErrorMessage = "Please enter valid  number greater then 0")]
        public double BasicCharges { get; set; }

        [Required(ErrorMessage = "Title is Reuqired")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is Reuqired")]
        public string AboutRideType { get; set; }
        public string DefaultImageUrl { get; set; }
        public string SelectedImageUrl { get; set; }
        public HttpPostedFileBase DefaultImageFile { get; set; }
        public HttpPostedFileBase SelectedImageFile { get; set; }
        public PictureModel DefaultImage { get; set; }
        public PictureModel SelectedImage { get; set; }
        public int Culture { get; set; } = 1;
    }

    

    public class PictureModel
    {
        public string Name { get; set; } = "";
        public string ImageString { get; set; } = "";
    }
}