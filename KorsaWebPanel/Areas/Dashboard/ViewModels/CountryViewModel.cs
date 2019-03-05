using BasketWebPanel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KorsaWebPanel.Areas.Dashboard.ViewModels
{

    public class CountryViewModelList : BaseViewModel
    {
        public List<CountryViewModel> countries { get; set; }
    }
    public class CountryViewModel : BaseViewModel
    {
        public CountryViewModel()
        {
            Culture = 1;
            IsActive = true;
        }
        public int Id { get; set; }
        public int Culture { get; set; }
        public bool IsActive { get; set; }
        
        public language English { get; set; }
        public language Arabic { get; set; }
    }

    public class language
    {
        public string Name;
    }


}