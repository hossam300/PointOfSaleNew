using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PointOfSale.DAL.Domains
{
    public class PurchaseOrder
    {
        public PurchaseOrder()
        {
            PurchaseOrderItem = new List<PurchaseOrderItem>();
            PurchaseOrderPayments = new List<PurchaseOrderPayment>();
        }
        public int Id { get; set; }
        [Display(Name = "Supplier", ResourceType = typeof(Resources.Resources))]
        public int SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }
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
        [Display(Name = "TotalItemCount", ResourceType = typeof(Resources.Resources))]
        public int TotalItemCount { get; set; }
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
        public virtual List<PurchaseOrderItem> PurchaseOrderItem { get; set; }
        public virtual List<PurchaseOrderPayment> PurchaseOrderPayments { get; set; }
        [Display(Name = "Shop", ResourceType = typeof(Resources.Resources))]
        public int ShopId { get; set; }
    }
}
