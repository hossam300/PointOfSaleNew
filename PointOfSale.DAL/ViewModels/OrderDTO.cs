using PointOfSale.DAL.Domains;
using System;

namespace PointOfSale.DAL.ViewModels
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string OrderNumber { get; set; }
        public int OrderId { get; set; }
        public OrderType OrderType { get; set; }
        public DateTime OrderDate { get; set; }
        public double? Total { get; set; }
        public double? Discount { get; set; }
        public double? OrderTax { get; set; }
        public string Notes { get; set; }
        public int TotalItemCount { get; set; }
        public int? SessionId { get; set; }
        public string CreatorId { get; set; }
        public DateTime CreationDate { get; set; }
        public string ModifiedByID { get; set; }
        public DateTime LastModificationDate { get; set; }
        public int ShopId { get; set; }
    }
}