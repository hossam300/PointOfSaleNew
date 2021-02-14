using PointOfSale.DAL.Domains;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PointOfSale.DAL.ViewModels
{
    public class ShopInterfaceDTO
    {
        public int Id { get; set; }
        [Display(Name = "ShopName", ResourceType = typeof(Resources.Resources))]
        public string Name { get; set; }
        [Display(Name = "TableManagement", ResourceType = typeof(Resources.Resources))]
        public bool TableManagement { get; set; }
        [Display(Name = "FloorsTables", ResourceType = typeof(Resources.Resources))]
        public bool FloorsTables { get; set; }
        [Display(Name = "Floors", ResourceType = typeof(Resources.Resources))]
        public List<ShopFloorDTO> Floors { get; set; }
        [Display(Name = "OrderlineNotes", ResourceType = typeof(Resources.Resources))]
        public bool OrderlineNotes { get; set; }
        [Display(Name = "CategoryPictures", ResourceType = typeof(Resources.Resources))]
        public bool CategoryPictures { get; set; }
        [Display(Name = "RestrictAvailableCategories", ResourceType = typeof(Resources.Resources))]
        public bool RestrictAvailableCategories { get; set; }
        [Display(Name = "AvailableCategories", ResourceType = typeof(Resources.Resources))]
        public List<ShopProductCategoryDTO> AvailableCategories { get; set; }
        [Display(Name = "VirtualKeyBoard", ResourceType = typeof(Resources.Resources))]
        public bool VirtualKeyBoard { get; set; }
        [Display(Name = "SetStartCategory", ResourceType = typeof(Resources.Resources))]
        public bool SetStartCategory { get; set; }
        [Display(Name = "StartCategory", ResourceType = typeof(Resources.Resources))]
        public int? StartCategoryId { get; set; }
        public ProductCategoryDTO StartCategory { get; set; }
        [Display(Name = "LargeScrollbars", ResourceType = typeof(Resources.Resources))]
        public bool LargeScrollbars { get; set; }
        [Display(Name = "ConnectDevices", ResourceType = typeof(Resources.Resources))]
        public bool ConnectDevices { get; set; }
        [Display(Name = "PrinterIPAddress", ResourceType = typeof(Resources.Resources))]
        public string PrinterIPAddress { get; set; }
        [Display(Name = "BarcodeScanner", ResourceType = typeof(Resources.Resources))]
        public int? BarcodeScannerId { get; set; }

        public BarcodeScannerDTO BarcodeScanner { get; set; }
        [Display(Name = "OrderPrinter", ResourceType = typeof(Resources.Resources))]
        public bool OrderPrinter { get; set; }
        [Display(Name = "Printers", ResourceType = typeof(Resources.Resources))]
        public List<ShopPrinterDTO> Printers { get; set; }
        [Display(Name = "FiscalPositionPerOrder", ResourceType = typeof(Resources.Resources))]
        public bool FiscalPositionPerOrder { get; set; }
        [Display(Name = "SpecificFiscalPosition", ResourceType = typeof(Resources.Resources))]
        public bool SpecificFiscalPosition { get; set; }
        [Display(Name = "Fiscalposition", ResourceType = typeof(Resources.Resources))]
        public int? FiscalPositionId { get; set; }
        public FiscalPositionDTO FiscalPosition { get; set; }
    }
}
