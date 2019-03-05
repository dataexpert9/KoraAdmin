﻿using BasketWebPanel.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KorsaWebPanel.Areas.Dashboard.BindingModels
{
    public class CountryBindingModel : BaseViewModel
    {
        public CountryBindingModel()
        {
            Culture = 1;
            Name = string.Empty;
            IsActive = true;
        }
        public int Id { get; set; }
        public int Culture { get; set; }
        public bool IsActive { get; set; }

        [Required(ErrorMessage = "Country Name is Required")]
        public string Name { get; set; }
    }
}