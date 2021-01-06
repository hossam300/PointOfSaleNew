using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PointOfSale.DAL.Domains
{
    public class Shop
    {
        //public Shop()
        //{
        //    AllowedEmployees = new ICollection<ShopEmployee>();
        //    Floors = new ICollection<ShopFloor>();
        //    AvailableCategories = new ICollection<ShopProductCategory>();
        //    Printers = new ICollection<ShopPrinter>();
        //    AvailablePriceICollections = new ICollection<ShopPriceICollection>();
        //    PaymentMethods = new ICollection<ShopPaymentMethod>();
        //}
        #region Main Info
        public int Id { get; set; }
        [Display(Name = "Name", ResourceType = typeof(Resources.Resources))]
        public string Name { get; set; }
        [Display(Name = "Branch", ResourceType = typeof(Resources.Resources))]
        public int? BranchId { get; set; }
        public virtual Branch Branch { get; set; }
        [Display(Name = "IsRestaurant", ResourceType = typeof(Resources.Resources))]
        public bool IsRestaurant { get; set; }
        [Display(Name = "LoginWithEmployees", ResourceType = typeof(Resources.Resources))]
        public bool LoginWithEmployees { get; set; }
        [Display(Name = "AllowedEmployees", ResourceType = typeof(Resources.Resources))]
        public virtual ICollection<ShopEmployee> AllowedEmployees { get; set; }
        #endregion
        #region PointOfSale Interface
        [Display(Name = "TableManagement", ResourceType = typeof(Resources.Resources))]
        public bool TableManagement { get; set; }
        [Display(Name = "FloorsTables", ResourceType = typeof(Resources.Resources))]
        public bool FloorsTables { get; set; }
        [Display(Name = "Floors", ResourceType = typeof(Resources.Resources))]
        public virtual ICollection<ShopFloor> Floors { get; set; }
        [Display(Name = "OrderlineNotes", ResourceType = typeof(Resources.Resources))]
        public bool OrderlineNotes { get; set; }
        [Display(Name = "CategoryPictures", ResourceType = typeof(Resources.Resources))]
        public bool CategoryPictures { get; set; }
        [Display(Name = "RestrictAvailableCategories", ResourceType = typeof(Resources.Resources))]
        public bool RestrictAvailableCategories { get; set; }
        [Display(Name = "AvailableCategories", ResourceType = typeof(Resources.Resources))]
        public virtual ICollection<ShopProductCategory> AvailableCategories { get; set; }
        [Display(Name = "VirtualKeyBoard", ResourceType = typeof(Resources.Resources))]
        public bool VirtualKeyBoard { get; set; }
        [Display(Name = "SetStartCategory", ResourceType = typeof(Resources.Resources))]
        public bool SetStartCategory { get; set; }
        [Display(Name = "StartCategory", ResourceType = typeof(Resources.Resources))]
        public int? StartCategoryId { get; set; }
        public virtual ProductCategory StartCategory { get; set; }
        [Display(Name = "LargeScrollbars", ResourceType = typeof(Resources.Resources))]
        public bool LargeScrollbars { get; set; }
        #endregion
        #region Connected Devices
        [Display(Name = "ConnectDevices", ResourceType = typeof(Resources.Resources))]
        public bool ConnectDevices { get; set; }
        [Display(Name = "PrinterIPAddress", ResourceType = typeof(Resources.Resources))]
        public string PrinterIPAddress { get; set; }
        [Display(Name = "BarcodeScanner", ResourceType = typeof(Resources.Resources))]
        public int? BarcodeScannerId { get; set; }

        public virtual BarcodeScanner BarcodeScanner { get; set; }
        [Display(Name = "OrderPrinter", ResourceType = typeof(Resources.Resources))]
        public bool OrderPrinter { get; set; }
        [Display(Name = "Printers", ResourceType = typeof(Resources.Resources))]
        public virtual ICollection<ShopPrinter> Printers { get; set; }
        #endregion
        #region Taxes
        [Display(Name = "FiscalpositionPerOrder", ResourceType = typeof(Resources.Resources))]
        public bool FiscalPointOfSaleitionPerOrder { get; set; }
        [Display(Name = "SpecificFiscalPosition", ResourceType = typeof(Resources.Resources))]
        public bool SpecificFiscalPointOfSaleition { get; set; }
        [Display(Name = "Fiscalposition", ResourceType = typeof(Resources.Resources))]
        public int? FiscalPointOfSaleitionId { get; set; }
        public virtual FiscalPosition FiscalPointOfSaleition { get; set; }
        #endregion
        #region Pricing
        [Display(Name = "Pricelist", ResourceType = typeof(Resources.Resources))]
        public int? PricelistId { get; set; }
        public virtual Pricelist Pricelist { get; set; }
        [Display(Name = "AdvancedPriceICollections", ResourceType = typeof(Resources.Resources))]
        public bool AdvancedPriceICollections { get; set; }
        [Display(Name = "AvailablePriceLists", ResourceType = typeof(Resources.Resources))]
        public virtual ICollection<ShopPricelist> AvailablePriceLists { get; set; }
        [Display(Name = "ProductPrices", ResourceType = typeof(Resources.Resources))]
        public virtual ProductPrices ProductPrices { get; set; }
        [Display(Name = "GlobalDiscounts", ResourceType = typeof(Resources.Resources))]
        public bool GlobalDiscounts { get; set; }
        [Display(Name = "ManualDiscounts", ResourceType = typeof(Resources.Resources))]
        public bool ManualDiscounts { get; set; }
        [Display(Name = "LoyaltyProgram", ResourceType = typeof(Resources.Resources))]
        public bool LoyaltyProgram { get; set; }
        [Display(Name = "PriceControl", ResourceType = typeof(Resources.Resources))]
        public bool PriceControl { get; set; }
        #endregion
        #region Payments
        [Display(Name = "PaymentMethods", ResourceType = typeof(Resources.Resources))]
        public virtual ICollection<ShopPaymentMethod> PaymentMethods { get; set; }
        [Display(Name = "PrefillCashPayment", ResourceType = typeof(Resources.Resources))]
        public bool PrefillCashPayment { get; set; }
        [Display(Name = "CashControl", ResourceType = typeof(Resources.Resources))]
        public bool CashControl { get; set; }
        //Default Opening
        [Display(Name = "AuthorizedDifference", ResourceType = typeof(Resources.Resources))]
        public double AuthorizedDifference { get; set; }
        [Display(Name = "Tips", ResourceType = typeof(Resources.Resources))]
        public bool Tips { get; set; }
        [Display(Name = "TipProduct", ResourceType = typeof(Resources.Resources))]
        public int? TipProductId { get; set; }
        public virtual Product TipProduct { get; set; }
        #endregion
        #region Bills & Receipts
        [Display(Name = "HeaderFooter", ResourceType = typeof(Resources.Resources))]
        public bool HeaderFooter { get; set; }
        [Display(Name = "Header", ResourceType = typeof(Resources.Resources))]
        public string Header { get; set; }
        [Display(Name = "Footer", ResourceType = typeof(Resources.Resources))]
        public string Footer { get; set; }
        [Display(Name = "AutomaticReceiptPrinting", ResourceType = typeof(Resources.Resources))]
        public bool AutomaticReceiptPrinting { get; set; }
        [Display(Name = "ReprintReceipt", ResourceType = typeof(Resources.Resources))]
        public bool ReprintReceipt { get; set; }
        [Display(Name = "BillPrinting", ResourceType = typeof(Resources.Resources))]
        public bool BillPrinting { get; set; }
        [Display(Name = "BillSpliitting", ResourceType = typeof(Resources.Resources))]
        public bool BillSpliitting { get; set; }
        [Display(Name = "Invoicing", ResourceType = typeof(Resources.Resources))]
        public bool Invoicing { get; set; }
        //Print invoices on customer request 
        //Invoice Journal
        #endregion


        #region Integration
        #region Inventory
        //Operation Type
        #endregion
        #region Accounting
        //Journal Entries
        #endregion
        #region Sales Reporting
        #endregion
        // Sales Team
        #endregion
    }
    public enum ProductPrices
    {
        [Display(Name = "TaxExcludedPrice", ResourceType = typeof(Resources.Resources))]
        TaxExcludedPrice = 1,
        [Display(Name = "TaxIncludedPrice", ResourceType = typeof(Resources.Resources))]
        TaxIncludedPrice = 2
    }
}
