using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PointOfSale.DAL.Domains
{
    public class Order
    {
        public Order()
        {
            OrderItem = new List<OrderItem>();
            OrderPayments = new List<OrderPayment>();
        }
        public int Id { get; set; }
        [Display(Name = "Customer", ResourceType = typeof(Resources.Resources))]
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        [Display(Name = "OrderNumber", ResourceType = typeof(Resources.Resources))]
        public string OrderNumber { get; set; }
        [Display(Name = "OrderType", ResourceType = typeof(Resources.Resources))]
        public OrderType OrderType { get; set; }
        [Display(Name = "OrderDate", ResourceType = typeof(Resources.Resources))]
        public DateTime OrderDate { get; set; }
        [Display(Name = "Total", ResourceType = typeof(Resources.Resources))]
        public double? Total { get; set; }
        [Display(Name = "Discount", ResourceType = typeof(Resources.Resources))]
        public double? Discount { get; set; }
        [Display(Name = "OrderTax", ResourceType = typeof(Resources.Resources))]
        public double? OrderTax { get; set; }
        [Display(Name = "Notes", ResourceType = typeof(Resources.Resources))]
        public string Notes { get; set; }
        [Display(Name = "LoyaltyProgram", ResourceType = typeof(Resources.Resources))]
        public int? LoyaltyProgramId { get; set; }
        public virtual LoyaltyProgram LoyaltyProgram { get; set; }
        [Display(Name = "TotalItemCount", ResourceType = typeof(Resources.Resources))]
        public int TotalItemCount { get; set; }
        [Display(Name = "Session", ResourceType = typeof(Resources.Resources))]
        public int? SessionId { get; set; }
        public virtual Session Session { get; set; }
        [Display(Name = "CreatedUserId", ResourceType = typeof(Resources.Resources))]
        public string CreatorId { get; set; }
        public virtual SahlUserIdentity Creator { get; set; }
        [Display(Name = "CreationDate", ResourceType = typeof(Resources.Resources))]
        public DateTime CreationDate { get; set; }
        [Display(Name = "ModifiedByID", ResourceType = typeof(Resources.Resources))]
        public string ModifiedByID { get; set; }
        public virtual SahlUserIdentity ModifiedBy { get; set; }
        [Display(Name = "LastModificationDate", ResourceType = typeof(Resources.Resources))]
        public DateTime LastModificationDate { get; set; }
        public virtual List<OrderItem> OrderItem { get; set; }
        public virtual List<OrderPayment> OrderPayments { get; set; }
        [Display(Name = "Shop", ResourceType = typeof(Resources.Resources))]
        public int ShopId { get; set; }
    }
    public enum OrderType
    {
        [Display(Name = "Draft", ResourceType = typeof(Resources.Resources))]
        Draft =1,
        [Display(Name = "Payed", ResourceType = typeof(Resources.Resources))]
        Payed =2,
        [Display(Name = "Instalment", ResourceType = typeof(Resources.Resources))]
        Instalment =3,
        [Display(Name = "Refund", ResourceType = typeof(Resources.Resources))]
        Refund =4,
        [Display(Name = "Cancled", ResourceType = typeof(Resources.Resources))]
        Cancled =5,
    }
}
