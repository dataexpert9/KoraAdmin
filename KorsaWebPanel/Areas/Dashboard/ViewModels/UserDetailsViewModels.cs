using BasketWebPanel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasketWebPanel.Areas.Dashboard.ViewModels
{
    public class UserDetailsViewModels
    {
    }
    public class MediaViewModel
    {
        public int Id { get; set; }

        public int Type { get; set; }

        public string Url { get; set; }

        public int Post_Id { get; set; }

        public int? User_Id { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; } //
    }
    public class GroupDataViewModel : BaseViewModel
    {
        public GroupDataViewModel()
        {
            Group = new GroupViewModel();
            GroupMembers = new List<UserViewModel>();
        }
        public GroupViewModel Group { get; set; }
        public List<UserViewModel> GroupMembers { get; set;}
    }
    public class GroupViewModel
    {
        public GroupViewModel()
        {
            Interest = new InterestViewModel();
            User = new UserViewModel();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public int? Interest_Id { get; set; }

        public int User_Id { get; set; }

        public bool AdminViewBlocked { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public InterestViewModel Interest { get; set; }

        public UserViewModel User { get; set; }

        public bool IsDeleted { get; set; }
    }
    public class InterestViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public bool IsDeleted { get; set; }
    }
    public class PostViewMode
    {

    }
}