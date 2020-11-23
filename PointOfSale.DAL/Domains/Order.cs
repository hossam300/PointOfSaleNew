using System;
using System.Collections.Generic;
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
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public string OrderNumber { get; set; }
        public OrderType OrderType { get; set; }
        public DateTime OrderDate { get; set; }
        public double? Total { get; set; }
        public double? Discount { get; set; }
        public double? OrderTax { get; set; }
        public string Notes { get; set; }
        public int? LoyaltyProgramId { get; set; }
        public virtual LoyaltyProgram LoyaltyProgram { get; set; }
        public int TotalItemCount { get; set; }
        public int? SessionId { get; set; }
        public virtual Session Session { get; set; }
        public string CreatorId { get; set; }
        public virtual SahlUserIdentity Creator { get; set; }
        public DateTime CreationDate { get; set; }
        public string ModifiedByID { get; set; }
        public virtual SahlUserIdentity ModifiedBy { get; set; }
        public DateTime LastModificationDate { get; set; }
        public virtual List<OrderItem> OrderItem { get; set; }
        public virtual List<OrderPayment> OrderPayments { get; set; }
        public int ShopId { get; set; }
    }
    public enum OrderType
    {

        Draft=1,
        Payed=2,
        Instalment=3,
        Refund=4,
        Cancled=5,
    }
}
