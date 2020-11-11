using System.Collections.Generic;

namespace PointOfSale.DAL.Domains
{
    public class Printer
    {
        public Printer()
        {
            PrintedProductCategories = new List<ProductCategory>();
        }
        public int Id { get; set; }
        public virtual PrinterType PrinterType { get; set; }
        public string ProxyIPAddress { get; set; }
        public virtual List<ProductCategory> PrintedProductCategories { get; set; }

    }
    public enum PrinterType
    {
        IoTBox=1,
        Epsonprinter=2
    }
}