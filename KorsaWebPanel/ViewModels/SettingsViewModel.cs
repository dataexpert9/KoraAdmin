using BasketWebPanel.BindingModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BasketWebPanel.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        public SettingsViewModel()
        {
            BannerImages = new List<ProductImagesBindingModel>();
        }
        public int Id { get; set; }


        [Required(ErrorMessage = "This field is required")]
        [Range(1, 10000, ErrorMessage = "Please enter a valid delivery fee")]
        [RegularExpression(MyRegularExpressions.Price, ErrorMessage = "Please enter a valid delivery fee")]
        public double DeliveryFee { get; set; } = 0;

        [Required]
        public string Currency { get; set; } = "";
        public string HowItWorksUrl { get; set; } = "";
        public string HowItWorksDescription { get; set; } = "";

        public string AboutUs { get; set; } = "";
        public string PrivacyPolicy { get; set; } = "";
        public string ContactNo { get; set; } = "";
        public string BannerImage { get; set; } = "";
        public string InstagramImage { get; set; } = "";

        [Required(ErrorMessage = "This field is required")]
        [Range(1, 10000, ErrorMessage = "Please enter a valid delivery threshold")]
        [RegularExpression(MyRegularExpressions.Price, ErrorMessage = "Please enter a valid delivery threshold")]
        public double FreeDeliveryThreshold { get; set; } = 0;

        [Required(ErrorMessage = "This field is required")]
        [Range(1, 10000, ErrorMessage = "Please enter a valid tax")]
        [RegularExpression(MyRegularExpressions.Price, ErrorMessage = "Please enter a valid tax")]
        public double Tax { get; set; }
        public string RefundExchange { get; set; } = "";
        public string TermsConditions { get; set; } = "";
        public List<ProductImagesBindingModel> BannerImages { get; set; }
        public string GoogleUrl { get; set; }
        public string FacebookUrl { get; set; }
        public string TwitterUrl { get; set; }
        public string InstagramUrl { get; set; }
        public string PintrestUrl { get; set; }
    }

    public class SettingsSetProductViewModel
    {
        public int? Product_Id { get; set; }

        public int BannerImage_Id { get; set; }
    }


    public class SettingsDTO : BaseViewModel
    {
        public SettingsDTO()
        {
            Arabic = new SettingsMLsDTO();
            English = new SettingsMLsDTO();
        }
        public int Id { get; set; }
        public string CurrencySymbol { get; set; }
        public float InvitationBonus { get; set; }
        public SettingsMLsDTO English { get; set; }
        public SettingsMLsDTO Arabic { get; set; }

    }
    public class SettingsMLsDTO
    {
        public string AboutUs { get; set; }
        public string PrivacyPolicy { get; set; }
        public string Currency { get; set; }
        public string TermsOfUse { get; set; }
    }
}