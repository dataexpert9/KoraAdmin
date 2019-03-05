using MultipartDataMediaFormatter.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasketWebPanel.Areas.Dashboard.BindingModels
{
    public class UploadImageBindingModel
    {
        public UploadImageBindingModel()
        {
            File = new HttpFile();
        }

        public HttpPostedFileBase Image { get; set; }

        public HttpFile File { get; set; }

        public int Type { get; set; }
    }
}