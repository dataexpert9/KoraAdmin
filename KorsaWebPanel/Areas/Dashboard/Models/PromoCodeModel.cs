using BasketWebPanel.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KorsaWebPanel.Areas.Dashboard.Models
{
    public class AddCodeBindingModel : BaseViewModel
    {
        public AddCodeBindingModel()
        {
            Code = new AddCodeViewModel();
            Coupon = new CouponCode();
        }
        public AddCodeViewModel Code { get; set; }
        public CouponCode Coupon { get; set; }
    }


    public class AddCodeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please generate code first.")]
        public string Code { get; set; }

        public int CodeType { get; set; }

        //public int ExpiryHours { get; set; }

        public SelectList ExpiryTime { get; set; }

        public DateTime CodeExpiry { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsExpired { get; set; }

        public int? Device_Id { get; set; }

        public DateTime? CreatedDate { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "PhoneNumber")]
        [StringLength(maximumLength: 15, MinimumLength = 10, ErrorMessage = "Your phone number must be at least 10 digits.")]
        public string PhoneNumber { get; set; }
    }



    public class CouponCode
    {
        public CouponCode()
        {  
        }

        public int NoOfCodes { get; set; } = 1;

        public int Length { get; set; } = 6;

        public string Prefix { get; set; }

        public string Suffix { get; set; }

        public SelectList NumberList { get; internal set; }

        public SelectList LetterList { get; set; }

        public SelectList SymbolList { get; internal set; }

        public SelectList RandomRegisterList { get; internal set; }

        public bool Numbers { get; set; }

        public bool Letters { get; set; }

        public bool Symbols { get; set; }

        public bool RandomRegister { get; set; }

        public string Mask { get; set; }
    }
}