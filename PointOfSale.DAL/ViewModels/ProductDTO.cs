using PointOfSale.DAL.Domains;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PointOfSale.DAL.ViewModels
{
   public class ProductDTO
    {
        public ProductDTO()
        {
            CustomerTaxes = new List<CustomerTaxDTO>();
            OptionalProducts = new List<OptionalProduct>();
            VendorProducts = new List<VendorProduct>();
            VendorTaxes = new List<VendorTaxDTO>();
        }
        public int Id { get; set; }
        [Display(Name= "Name")]
        public string Name { get; set; }
        [Display(Name = "Image")]
        public string ProductImage { get; set; }
        [Display(Name = "Can Be Sold")]
        public bool CanBeSold { get; set; }
        [Display(Name = "Can Be Purchased")]
        public bool CanBePurchased { get; set; }
        [Display(Name = "Can Be Expensed")]
        public bool CanBeExpensed { get; set; }
        [Display(Name = "Can Be Rented")]
        public bool CanBeRented { get; set; }
        [Display(Name = "Product Type")]
        public ProductType ProductType { get; set; }
        [Display(Name = "Product Category")]
        public int? ProductCategoryId { get; set; }
      //  public  ProductCategory ProductCategory { get; set; }
        [Display(Name = "Internal Reference")]
        public string InternalReference { get; set; }
        [Display(Name = "Barcode")]
        public string Barcode { get; set; }
        [Display(Name = "Generate Barcode")]
        public bool GenerateBarcode { get; set; } = false;
        [Display(Name = "Sales Price")]
        public double SalesPrice { get; set; }
        [Display(Name = "Cost")]
        public double Cost { get; set; }
        public  List<CustomerTaxDTO> CustomerTaxes { get; set; }
        [Display(Name = "Company")]
        public int CompanyId { get; set; }
      //  public  Company Company { get; set; }
        [Display(Name = "Notes")]
        public string Notes { get; set; }
        [Display(Name = "Invoicing Policy")]
        public InvoicingPolicy InvoicingPolicy { get; set; }
        [Display(Name = "Subscription Product")]
        public bool SubscriptionProduct { get; set; }
        [Display(Name = "Is Event Ticket")]
        public bool IsEventTicket { get; set; }
        [Display(Name = "Optional Products")]
        public  List<OptionalProduct> OptionalProducts { get; set; }
        [Display(Name = "Sales Description")]
        public string SalesDescription { get; set; }
        [Display(Name = "Available In PointOfSale")]
        public bool AvailableInPointOfSale { get; set; }
        [Display(Name = "Category")]
        public int? CategoryId { get; set; }
      //  public  ProductCategoryDTO Category { get; set; }
        [Display(Name = "To Weigh With Scale")]
        public bool ToWeighWithScale { get; set; }
        public  List<VendorProduct> VendorProducts { get; set; }
        [Display(Name = "Procurement")]
        public Procurement Procurement { get; set; }
        public  List<VendorTaxDTO> VendorTaxes { get; set; }
        [Display(Name = "Control Policy")]
        public ControlPolicy ControlPolicy { get; set; }
        [Display(Name = "Purchase Description")]
        public string PurchaseDescription { get; set; }
        [Display(Name = "Manufacture")]
        public bool Manufacture { get; set; }
        [Display(Name = "Buy")]
        public bool Buy { get; set; }
        [Display(Name = "Weight")]
        public double Weight { get; set; }
        [Display(Name = "Volume")]
        public double Volume { get; set; }
        [Display(Name = "Manufacturing Lead Time")]
        public double ManufacturingLeadTime { get; set; }
        [Display(Name = "Customer Lead Time")]
        public double CustomerLeadTime { get; set; }
        [Display(Name = "HSCode")]
        public string HSCode { get; set; }
        [Display(Name = "Description For Delivery Orders")]
        public string DescriptionForDeliveryOrders { get; set; }
        [Display(Name = "Description For Receipts")]
        public string DescriptionForReceipts { get; set; }
       
    }
}
