using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PointOfSale.DAL.Domains
{
    public class Product
    {
        public Product()
        {
            CustomerTaxes = new List<CustomerTax>();
            OptionalProducts = new List<OptionalProduct>();
            VendorProducts = new List<VendorProduct>();
            VendorTaxes = new List<VendorTax>();
            ShopProducts = new List<ShopProduct>();
        }
        public int Id { get; set; }
        #region General Information
        [Display(Name = "ProductName", ResourceType = typeof(Resources.Resources))]
        public string Name { get; set; }
        [Display(Name = "ProductImage", ResourceType = typeof(Resources.Resources))]
        public string ProductImage { get; set; }
        [Display(Name = "CanBeSold", ResourceType = typeof(Resources.Resources))]
        public bool CanBeSold { get; set; }
        [Display(Name = "CanBePurchased", ResourceType = typeof(Resources.Resources))]
        public bool CanBePurchased { get; set; }
        [Display(Name = "CanBeExpensed", ResourceType = typeof(Resources.Resources))]
        public bool CanBeExpensed { get; set; }
        [Display(Name = "CanBeRented", ResourceType = typeof(Resources.Resources))]
        public bool CanBeRented { get; set; }
        [Display(Name = "ProductType", ResourceType = typeof(Resources.Resources))]
        public ProductType ProductType { get; set; }
        [Display(Name = "ProductCategory", ResourceType = typeof(Resources.Resources))]
        public int? ProductCategoryId { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
        [Display(Name = "InternalReference", ResourceType = typeof(Resources.Resources))]
        public string InternalReference { get; set; }
        [Display(Name = "Barcode", ResourceType = typeof(Resources.Resources))]
        public string Barcode { get; set; }
        [Display(Name = "SalesPrice", ResourceType = typeof(Resources.Resources))]
        public double SalesPrice { get; set; } = 0;
        [Display(Name = "Cost", ResourceType = typeof(Resources.Resources))]
        public double Cost { get; set; } = 0;

        public virtual List<CustomerTax> CustomerTaxes { get; set; }
        [Display(Name = "Company", ResourceType = typeof(Resources.Resources))]
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
        [Display(Name = "Notes", ResourceType = typeof(Resources.Resources))]
        public string Notes { get; set; }
        #endregion
        #region Sales
        [Display(Name = "InvoicingPolicy", ResourceType = typeof(Resources.Resources))]
        public InvoicingPolicy InvoicingPolicy { get; set; }
        [Display(Name = "SubscriptionProduct", ResourceType = typeof(Resources.Resources))]
        public bool SubscriptionProduct { get; set; }
        [Display(Name = "IsEventTicket", ResourceType = typeof(Resources.Resources))]
        public bool IsEventTicket { get; set; }
        public virtual List<OptionalProduct> OptionalProducts { get; set; }
        [Display(Name = "SalesDescription", ResourceType = typeof(Resources.Resources))]
        public string SalesDescription { get; set; }
        #endregion 
        #region Point of Sale
        [Display(Name = "AvailableInPOS", ResourceType = typeof(Resources.Resources))]
        public bool AvailableInPOS { get; set; }
        [Display(Name = "Category", ResourceType = typeof(Resources.Resources))]
        public int? CategoryId { get; set; }
        public virtual ProductCategory Category { get; set; }
        [Display(Name = "ToWeighWithScale", ResourceType = typeof(Resources.Resources))]
        public bool ToWeighWithScale { get; set; }
        #endregion
        #region Purchase
        public virtual List<VendorProduct> VendorProducts { get; set; }
        [Display(Name = "Procurement", ResourceType = typeof(Resources.Resources))]
        public Procurement Procurement { get; set; }
        public virtual List<VendorTax> VendorTaxes { get; set; }
        [Display(Name = "ControlPolicy", ResourceType = typeof(Resources.Resources))]
        public ControlPolicy ControlPolicy { get; set; }
        [Display(Name = "PurchaseDescription", ResourceType = typeof(Resources.Resources))]
        public string PurchaseDescription { get; set; }
        #endregion
        #region Inventory
        [Display(Name = "Manufacture", ResourceType = typeof(Resources.Resources))]
        public bool Manufacture { get; set; }
        [Display(Name = "Buy", ResourceType = typeof(Resources.Resources))]
        public bool Buy { get; set; }
        [Display(Name = "Weight", ResourceType = typeof(Resources.Resources))]
        public double Weight { get; set; } = 0;
        [Display(Name = "Volume", ResourceType = typeof(Resources.Resources))]
        public double Volume { get; set; } = 0;
        [Display(Name = "ManufacturingLeadTime", ResourceType = typeof(Resources.Resources))]
        public double ManufacturingLeadTime { get; set; } = 0;
        [Display(Name = "CustomerLeadTime", ResourceType = typeof(Resources.Resources))]
        public double CustomerLeadTime { get; set; } = 0;
        [Display(Name = "HSCode", ResourceType = typeof(Resources.Resources))]
        public string HSCode { get; set; }
        [Display(Name = "DescriptionForDeliveryOrders", ResourceType = typeof(Resources.Resources))]
        public string DescriptionForDeliveryOrders { get; set; }
        [Display(Name = "DescriptionForReceipts", ResourceType = typeof(Resources.Resources))]
        public string DescriptionForReceipts { get; set; }
        [NotMapped]
        [Display(Name = "GenerateBarcode", ResourceType = typeof(Resources.Resources))]
        public bool GenerateBarcode { get; set; }
        public virtual List<ShopProduct> ShopProducts { get; set; }
        #endregion
        #region Account
        // TODO ADD GLAccounts
        #endregion
    }
    public enum ProductType
    {
        [Display(Name = "Consumable", ResourceType = typeof(Resources.Resources))]
        Consumable = 1,
        [Display(Name = "Service", ResourceType = typeof(Resources.Resources))]
        Service = 2,
        [Display(Name = "StorableProduct", ResourceType = typeof(Resources.Resources))]
        StorableProduct = 3
    }
    public enum InvoicingPolicy
    {
        [Display(Name = "OrderedQuantities", ResourceType = typeof(Resources.Resources))]
        OrderedQuantities = 1,
        [Display(Name = "DeliveredQuantities", ResourceType = typeof(Resources.Resources))]
        DeliveredQuantities = 2,
    }
    public enum Procurement
    {
        [Display(Name = "CreateADraftPurchaseOrder", ResourceType = typeof(Resources.Resources))]
        CreateADraftPurchaseOrder = 1,
        [Display(Name = "ProposeACallForTenders", ResourceType = typeof(Resources.Resources))]
        ProposeACallForTenders = 2,
    }
    public enum ControlPolicy
    {
        [Display(Name = "OnOrderedQuantities", ResourceType = typeof(Resources.Resources))]
        OnOrderedQuantities = 1,
        [Display(Name = "OnReceivedQuantities", ResourceType = typeof(Resources.Resources))]
        OnReceivedQuantities = 2,
    }
}