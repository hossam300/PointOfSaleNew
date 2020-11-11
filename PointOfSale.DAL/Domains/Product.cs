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
        }
        public int Id { get; set; }
        #region General Information
        public string Name { get; set; }
        public string ProductImage { get; set; }
        public bool CanBeSold { get; set; }
        public bool CanBePurchased { get; set; }
        public bool CanBeExpensed { get; set; }
        public bool CanBeRented { get; set; }
        public ProductType ProductType { get; set; }
        public int? ProductCategoryId { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
        public string InternalReference { get; set; }
        public string Barcode { get; set; }
        public double SalesPrice { get; set; } = 0;
        public double Cost { get; set; } = 0;
        public virtual List<CustomerTax> CustomerTaxes { get; set; }
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public string Notes { get; set; }
        #endregion
        #region Sales
        public InvoicingPolicy InvoicingPolicy { get; set; }
        public bool SubscriptionProduct { get; set; }
        public bool IsEventTicket { get; set; }
        public virtual List<OptionalProduct> OptionalProducts { get; set; }
        public string SalesDescription { get; set; }
        #endregion 
        #region Point of Sale
        public bool AvailableInPOS { get; set; }
        public int? CategoryId { get; set; }
        public virtual ProductCategory Category { get; set; }
        public bool ToWeighWithScale { get; set; }
        #endregion
        #region Purchase
        public virtual List<VendorProduct> VendorProducts { get; set; }
        public Procurement Procurement { get; set; }
        public virtual List<VendorTax> VendorTaxes { get; set; }
        public ControlPolicy ControlPolicy { get; set; }
        public string PurchaseDescription { get; set; }
        #endregion
        #region Inventory
        public bool Manufacture { get; set; }
        public bool Buy { get; set; }
        public double Weight { get; set; } = 0;
        public double Volume { get; set; } = 0;
        public double ManufacturingLeadTime { get; set; } = 0;
        public double CustomerLeadTime { get; set; } = 0;
        public string HSCode { get; set; }
        public string DescriptionForDeliveryOrders { get; set; }
        public string DescriptionForReceipts { get; set; }
        [NotMapped]
        public bool GenerateBarcode { get; set; }
        #endregion
        #region Account
        // TODO ADD GLAccounts
        #endregion
    }
    public enum ProductType
    {
        Consumable = 1,
        Service = 2,
        [Display(Name = "Storable Product")]
        StorableProduct = 3
    }
    public enum InvoicingPolicy
    {
        [Display(Name = "Ordered Quantities")]
        OrderedQuantities = 1,
        [Display(Name = "Delivered Quantities")]
        DeliveredQuantities = 2,
    }
    public enum Procurement
    {
        [Display(Name = "Create A Draft Purchase Orders")]
        CreateADraftPurchaseOrder = 1,
        [Display(Name = "ProPointOfSalee A Call For Tenders")]
        ProposeACallForTenders = 2,
    }
    public enum ControlPolicy
    {
        [Display(Name = "On Ordered Quantities")]
        OnOrderedQuantities = 1,
        [Display(Name = "On Received Quantities")]
        OnReceivedQuantities = 2,
    }
}