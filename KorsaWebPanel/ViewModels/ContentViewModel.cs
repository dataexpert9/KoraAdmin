using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BasketWebPanel.ViewModels
{
    public class PrivacyViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "This field is required")]
        public string PrivacyPolicy { get; set; }
    }

    public class AboutUsViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "This field is required")]
        public string AboutUs { get; set; }
    }

    public class TermsConditionsViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "This field is required")]
        public string TermsConditions { get; set; }
    }

    public class RefundExchangeViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "This field is required")]
        public string RefundExchange { get; set; }
    }

    public class UrlsViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "This field is required")]
        public string GoogleUrl { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string FacebookUrl { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string TwitterUrl { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string InstagramUrl { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string PintrestUrl { get; set; }
    }
}