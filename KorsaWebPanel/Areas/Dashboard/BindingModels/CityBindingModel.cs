using BasketWebPanel.ViewModels;
using BasketWebPanel.BindingModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using KorsaWebPanel.Areas.Dashboard.ViewModels;

namespace KorsaWebPanel.Areas.Dashboard.BindingModels
{
    public class CityBindingModel : BaseViewModel
    {
        public CityBindingModel()
        {
            Culture = 1;
            Name = string.Empty;
            IsActive = true;
        }
        public int Id { get; set; }
        public int Country_Id { get; set; }
        public int Culture { get; set; }
        public bool IsActive { get; set; }

        [Required(ErrorMessage = "Country Name is Required")]
        public string Name { get; set; }
        public CountryViewModelList Countries { get; set; }
    }
}