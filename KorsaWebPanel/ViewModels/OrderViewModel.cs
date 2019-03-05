using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasketWebPanel.ViewModels
{
     public class OrderViewModel : BaseViewModel
    {
        public OrderViewModel()
        {
            StoreOrders = new HashSet<StoreOrderViewModel>();
        }
        public int Id { get; set; }

        public string OrderNo { get; set; }

        public int Status { get; set; }

        //[JsonConverter(typeof(JsonCustomDateTimeConverter))]
        public DateTime OrderDateTime { get; set; }

        //[JsonConverter(typeof(JsonCustomDateTimeConverter))]
        public DateTime DeliveryTime_From { get; set; }

        //[JsonConverter(typeof(JsonCustomDateTimeConverter))]
        public DateTime DeliveryTime_To { get; set; }

        public string AdditionalNote { get; set; }

        public int PaymentMethod { get; set; }

        public string PaymentMethodName { get; set; } = "";

        public double Subtotal { get; set; }

        public double ServiceFee { get; set; }

        public double DeliveryFee { get; set; }

        public double Total { get; set; }

        public int User_ID { get; set; }

        public bool IsDeleted { get; set; }

        public int? OrderPayment_Id { get; set; }

        public string DeliveryAddress { get; set; }

        public int? DeliveryMan_Id { get; set; }

        public short PaymentStatus { get; set; }

        public string PaymentStatusName { get; set; }


        public string UserFullName { get; set; }

        public string UserProfilePictureUrl { get; set; }

        public virtual ICollection<StoreOrderViewModel> StoreOrders { get; set; }

        public virtual UserViewModel User { get; set; }

        public double Tax { get; set; }
    }


    public class UserViewModel
    {
        public UserViewModel()
        {

        }

        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public string ProfilePictureUrl { get; set; }

        public string Email { get; set; }

        public string PhoneNo { get; set; }

        public string AccountType { get; set; }

        public string ZipCode { get; set; }

        public string Location { get; set; }

        public string DateofBirth { get; set; }

        public short? SignInType { get; set; }

        public string UserName { get; set; }

        public short? Status { get; set; }

        public bool EmailConfirmed { get; set; }

        public bool PhoneConfirmed { get; set; }

        public bool DeActive { get; set; }

        public DateTime JoinedOn { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

    }
    public class TrendLogsViewModel
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public int Post_Id { get; set; }

        public int Comment_Id { get; set; }

        public int User_Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool IsDeleted { get; set; }
    }

    public class InterestsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }


    public class StoreOrderViewModel
    {
        public StoreOrderViewModel()
        {
            OrderItems = new List<OrderItemViewModel>();
        }
        public int Id { get; set; }

        public string OrderNo { get; set; }

        public int Status { get; set; }

        public int Store_Id { get; set; }

        public double Subtotal { get; set; }

        public double Total { get; set; }

        public bool IsDeleted { get; set; }

        public int Order_Id { get; set; }

        public string StoreName { get; set; }

        public string ImageUrl { get; set; }

        public List<OrderItemViewModel> OrderItems { get; set; }
    }

    public class OrderItemViewModel
    {
        public int Id { get; set; }

        public int ItemId { get; set; }

        public int Qty { get; set; }

        public int StoreOrder_Id { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public string Description { get; set; }

        public string Weight { get; set; }

        public double WeightInGrams { get; set; }

        public double WeightInKiloGrams { get; set; }

        public string ImageUrl { get; set; }
        public bool IsFavourite { get; internal set; }
    }
}