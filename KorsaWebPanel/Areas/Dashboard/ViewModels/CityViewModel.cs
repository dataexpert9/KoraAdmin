using BasketWebPanel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KorsaWebPanel.Areas.Dashboard.ViewModels
{
    public class CityViewModelList : BaseViewModel
    {
        public List<CityViewModel> cities { get; set; }
    }
    public class CityViewModel : BaseViewModel
    {
        public int Id { get; set; }
        public int Culture { get; set; }
        public bool IsActive { get; set; }
        public bool Branch_Id { get; set; }

        public language English { get; set; }
        public language Arabic { get; set; }

        public country Country { get; set; }
    }

    public class country
    {
        public int id { get; set; }
        public bool IsActive { get; set; }
        public language English { get; set; }
        public language Arabic { get; set; }

    }
    
}