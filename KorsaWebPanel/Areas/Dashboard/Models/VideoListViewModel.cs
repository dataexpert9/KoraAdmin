using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasketWebPanel.Areas.Dashboard.Models
{
    public class VideoListViewModel
    {
        public VideoListViewModel()
        {
            VideoUrl = new VideoUrlViewModel();
        }
        public VideoUrlViewModel VideoUrl { get; set; }
    }
    public class VideoUrlViewModel
    {
        public int Id { get; set; }
        public string Url { get; set; }

        public int Index { get; set; }
    }

}