using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PointOfSale.DAL.Domains
{
    public class Printer
    {
        public Printer()
        {
            PrintedProductCategories = new List<ProductCategory>();
        }
        public int Id { get; set; }
        [Display(Name = "PrinterName", ResourceType = typeof(Resources.Resources))]
        public string PrinterName { get; set; }
        [Display(Name = "PrinterType", ResourceType = typeof(Resources.Resources))]
        public virtual PrinterType PrinterType { get; set; }
        [Display(Name = "ProxyIPAddress", ResourceType = typeof(Resources.Resources))]
        public string ProxyIPAddress { get; set; }
        public virtual List<ProductCategory> PrintedProductCategories { get; set; }

    }
    public enum PrinterType
    {
        [Display(Name = "IoTBox", ResourceType = typeof(Resources.Resources))]
        IoTBox =1,
        [Display(Name = "Epsonprinter", ResourceType = typeof(Resources.Resources))]
        Epsonprinter =2
    }
}