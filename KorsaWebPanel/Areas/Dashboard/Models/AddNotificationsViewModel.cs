using BasketWebPanel.BindingModels;
using BasketWebPanel.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BasketWebPanel.Areas.Dashboard.Models
{
    public class AddNotificationsViewModel : BaseViewModel
    {
        public AddNotificationsViewModel()
        {
            TargetOptions = new SelectList(new List<SelectListItem>());
            Notification = new NotificationBindingModel();
        }

        public SelectList TargetOptions { get; set; }

        public NotificationBindingModel Notification;

    }

    public class SendNotificationToUserViewModel
    {
        public int Id { get; set; }

        public int User_Id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Title { get; set; }

        [Required(ErrorMessage ="This field is required")]
        public string Text { get; set; }

        public int Status { get; set; }

        public string StatusName { get; set; }

        public AdminNotificationViewModel AdminNotification { get; set; }
    }

    public class AdminNotificationViewModel
    {

        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }
        
    }
}