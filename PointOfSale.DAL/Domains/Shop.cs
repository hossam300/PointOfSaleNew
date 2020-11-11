using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSale.DAL.Domains
{
    public class Shop
    {
        public Shop()
        {
            AllowedEmployees = new List<AspNetUser>();
            Floors = new List<Floor>();
            AvailableCategories = new List<ProductCategory>();
            Printers = new List<Printer>();
            PaymentMethods = new List<PaymentMethod>();
        }
        #region Main Info
        public int Id { get; set; }
        public string Name { get; set; }
        public int BranchId { get; set; }
        public virtual Branch Branch { get; set; }
        public bool IsRestaurant { get; set; }
        public bool LoginWithEmployees { get; set; }
        public virtual List<AspNetUser> AllowedEmployees { get; set; }
        #endregion
        #region PointOfSale Interface
        public bool TableManagement { get; set; }
        public virtual List<Floor> Floors { get; set; }
        public bool OrderlineNotes { get; set; }
        public bool CategoryPictures { get; set; }
        public bool RestrictAvailableCategories { get; set; }
        public virtual List<ProductCategory> AvailableCategories { get; set; }
        public bool VirtualKeyBoard { get; set; }
        public bool SetStartCategory { get; set; }
        public int? StartCategoryId { get; set; }
        public virtual ProductCategory StartCategory { get; set; }
        public bool LargeScrollbars { get; set; }
        #endregion
        #region Connected Devices
        public bool ConnectDevices { get; set; }
        public string PrinterIPAddress { get; set; }
        public int? BarcodeScannerId { get; set; }
        public virtual BarcodeScanner BarcodeScanner { get; set; }
        public bool OrderPrinter { get; set; }
        public virtual List<Printer> Printers { get; set; }
        #endregion
        #region Taxes
        public bool FiscalPointOfSaleitionPerOrder { get; set; }
        public bool SpecificFiscalPointOfSaleition { get; set; }
        public int? FiscalPointOfSaleitionId { get; set; }
        public virtual FiscalPointOfSaleition FiscalPointOfSaleition { get; set; }
        #endregion
        #region Pricing
        public int? DefaultPricelistId { get; set; }
        public virtual Pricelist Pricelist { get; set; }
        public bool AdvancedPricelists { get; set; }
        public virtual List<Pricelist> AvailablePricelists { get; set; }
        public virtual ProductPrices ProductPrices { get; set; }
        public bool GlobalDiscounts { get; set; }
        public bool LoyaltyProgram { get; set; }
        public bool PriceControl { get; set; }
        #endregion
        #region Payments
        public virtual List<PaymentMethod> PaymentMethods { get; set; }
        public bool PrefillCashPayment { get; set; }
        public bool CashControl { get; set; }
        //Default Opening
        public double AuthorizedDifference { get; set; }
        public bool Tips { get; set; }
        public int? TipProductId { get; set; }
        public virtual Product TipProduct { get; set; }
        #endregion
        #region Bills & Receipts
        public bool HeaderFooter { get; set; }
        public string Header { get; set; }
        public string Footer { get; set; }
        public bool AutomaticReceiptPrinting { get; set; }
        public bool ReprintReceipt { get; set; }
        public bool BillPrinting { get; set; }
        public bool BillSpliitting { get; set; }
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
        TaxExcludedPrice = 1,
        TaxIncludedPrice = 2
    }
}
