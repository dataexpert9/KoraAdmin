using BasketWebPanel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace KorsaWebPanel.Areas.Dashboard.BindingModels
{
    
    public class AddSubscriptionPackageBindingModel : BaseViewModel
    {
        public int Id { get; set; }

        [Required( ErrorMessage = "Name is Reuqired")]
        public string Name { get; set; }

        [Required( ErrorMessage = "Number of Rides is Required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid  number greater then 0")]
        public int NumOfRides { get; set; }

        [Required(ErrorMessage = "Price is Required")]
        [Range(1, double.MaxValue, ErrorMessage = "Please enter valid price greater then 0")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Duration is Required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid duration greater then 0")]
        public int Duration { get; set; }


        [Required(ErrorMessage = "Duration is Required")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int DurationType {get; set;}

        public SelectList DurationTypeOptions { get; set; }




    }
   
}