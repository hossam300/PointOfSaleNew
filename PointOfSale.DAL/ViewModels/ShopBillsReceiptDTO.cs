using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PointOfSale.DAL.ViewModels
{
    public class ShopBillsReceiptDTO
    {
        public int Id { get; set; }
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
    }
}
