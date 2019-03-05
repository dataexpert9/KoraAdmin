using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasketWebPanel.ViewModels
{
    public class SettingsSelectProductViewModel
    {
        public List<SelectProductViewModel> Products { get; set; }
    }
    public class SelectProductViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public string ImageUrl { get; set; }

        public List<ProductImages> ProductImages { get; set; }

    }
    public class ProductImages
    {
        public int Id { get; set; }

        public string Url { get; set; }
    }
}