using BasketWebPanel.Areas.Dashboard.Models;
using BasketWebPanel.Areas.Dashboard.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BasketWebPanel.ViewModels
{

    public class SearchUserViewModel : BaseViewModel
    {
        public SearchUserViewModel()
        {
            Users = new List<UserBindingModel>();
        }

        public List<UserBindingModel> Users { get; set; }

        public SelectList StatusOptions { get; internal set; }
    }


    public class ChatHistoryViewModel : BaseViewModel
    {
        public ChatHistoryViewModel()
        {
            ChatHistory = new List<ChatHistoryModel>();
        }

        public List<ChatHistoryModel> ChatHistory { get; set; }
    }
    public class ChatHistoryModel
    {          
        public int id { get; set; }
        public int entityId { get; set; }
        public int userType { get; set; }
        public string userName { get; set; }
        public DateTime lastConversationDate { get; set; }
        public Boolean isDeleted { get; set; }
    }

    public class UserDetailsViewModel : BaseViewModel
    {
        public UserDetailsViewModel()
        {
            User = new UserViewModel();
        }
        public UserViewModel User { get; set; }
        public int TotalPosts { get; set; }
        public int TotalGroups { get; set; }
        public int TotalFollowers { get; set; }
        public int TotalFollowings { get; set; }
        public int TotalShares { get; set; }
        public int TotalLikes { get; set; }
    }
    public class UserDataViewModel : BaseViewModel
    {
        public UserDataViewModel()
        {
            User = new UserViewModel();
        }
        public UserViewModel User { get; set; }
    }

    public class ReportUsersViewModel
    {
        public int Id { get; set; }

        public int FirstUser_Id { get; set; }

        public virtual UserViewModel FirstUser { get; set; }

        public int SecondUser_Id { get; set; }

        public virtual UserViewModel SecondUser { get; set; }

        public DateTime CreatedDate { get; set; }

        public int ReportCount { get; set; }

        public bool IsDeleted { get; set; }

        public int ReportStatus { get; set; }
    }

    public class UserBindingModel : BaseViewModel
    {
        public UserBindingModel()
        {
            UserAddresses = new List<UserAddressBindingModel>();
            PaymentCards = new List<UserPaymentCardBindingModel>();
            Feedback = new List<FeedbackViewModel>();
            UserSubscriptions = new List<UserSubscriptionViewModel>();
            Orders = new List<OrderViewModel>();
            Notifications = new List<SendNotificationToUserViewModel>();
        }


        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public string ProfilePictureUrl { get; set; }

        public string Email { get; set; }

        public string PhoneNo { get; set; }

        public string AccountType { get; set; }

        public string ZipCode { get; set; }

        public string DateofBirth { get; set; }

        public short? SignInType { get; set; }

        public string UserName { get; set; }

        public short? Status { get; set; }

        public bool EmailConfirmed { get; set; }

        public bool PhoneConfirmed { get; set; }

        public bool IsDeleted { get; set; }

        public bool DeActive { get; set; }

        public string StatusName { get; set; }

        public bool IsChecked { get; set; }

        public float Rating { get; set; }

        public int RidesCount { get; set; }

        public double TotalDistance { get; set; } //In Miles

        public Gender DriverPreference { get; set; }
        public bool IsNotificationsOn { get; set; }

        public Locations Location { get; set; }

        public string Token { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Address { get; set; }

        public string InvitationCode { get; set; }



        public enum Gender
        {
            Male = 1,
            Female = 2
        }

        
        public List<UserAddressBindingModel> UserAddresses;

        public List<UserPaymentCardBindingModel> PaymentCards { get; set; }

        public List<FeedbackViewModel> Feedback { get; set; }

        public List<UserSubscriptionViewModel> UserSubscriptions { get; set; }

        public List<OrderViewModel> Orders { get; set; }

        public List<FavouriteViewModel> Favourites { get; set; }

        public List<SendNotificationToUserViewModel> Notifications { get; set; }

        public SendNotificationToUserViewModel SendNotification { get; set; }
    }

    public class UserPaymentCardBindingModel
    {
        public int Id { get; set; }

        public string CardNumber { get; set; }

        public DateTime ExpiryDate { get; set; }

        public string CCV { get; set; }

        public string NameOnCard { get; set; }

        public int CardType { get; set; } //1 for Credit, 2 for Debit

        public int User_ID { get; set; }

        public bool IsDeleted { get; set; }
    }

    public class UserAddressBindingModel
    {
        public int Id { get; set; }

        public int User_ID { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string StreetName { get; set; }

        public string Floor { get; set; }

        public string Apartment { get; set; }

        public string NearestLandmark { get; set; }

        public string BuildingName { get; set; }

        public short Type { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsPrimary { get; set; }
    }

    public class FeedbackViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int? UserId { get; set; }
        public UserViewModel User { get; set; }
        public int? Store_Id { get; set; }
        public StoreViewModel Store { get; set; }
    }

    public class FavouriteViewModel
    {
        public int Id { get; set; }

        public int Product_Id { get; set; }

        public int User_ID { get; set; }

        public bool IsDeleted { get; set; }

        public virtual SearchProductViewModel Product { get; set; }
    }

    public class UserSubscriptionViewModel
    {
        public int Id { get; set; }

        public int User_Id { get; set; }

        public int Box_Id { get; set; }

        public int Type { get; set; }

        public string ActivationCode { get; set; }

        public int Status { get; set; }

        public DateTime SubscriptionDate { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ExpiryDate { get; set; }

        public bool IsDeleted { get; set; }

        public Box Box { get; set; }
    }

    //public class Favourites
    //{
    //    public int Id { get; set; }

    //    public int Product_Id { get; set; }

    //    public int User_ID { get; set; }

    //    public bool IsDeleted { get; set; }

    //    public ProductViewModel Product { get; set; }
    //}


    //public class ProductViewModel
    //{
    //    public int Id { get; set; }

    //    public string Name { get; set; }

    //    public double Price { get; set; }

    //    //[Required]
    //    public string Description { get; set; }


    //    public int WeightUnit { get; set; }

    //    public double WeightInGrams { get; set; }

    //    public double WeightInKiloGrams { get; set; }

    //    public string ImageUrl { get; set; }

    //    public string VideoUrl { get; set; }

    //    public short Status { get; set; }

    //    public int Category_Id { get; set; }

    //    public int Store_Id { get; set; }

    //    public bool IsDeleted { get; set; }

    //    public string Size { get; set; }

    //}

}