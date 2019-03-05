using BasketWebPanel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BasketWebPanel.Areas.Dashboard.ViewModels
{
    public class HelpBindingModel : BaseViewModel
    {
        public Help Help { get; set; }
        public SelectList LanguageOptions { get; internal set; }

    }

    public class Help
    {
        public int Id { get; set; }

        public string HelpText_ar { get; set; }

        public string HelpText_en { get; set; }

        public int Language { get; set; }

    }
}