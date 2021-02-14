using PointOfSale.DAL.Domains;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PointOfSale.DAL.ViewModels
{
    public class AddProductDTO
    {
        public int Id { get; set; }
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
        public virtual ProductCategoryDTO ProductCategory { get; set; }
        [Display(Name = "InternalReference", ResourceType = typeof(Resources.Resources))]
        public string InternalReference { get; set; }
        [Display(Name = "Barcode", ResourceType = typeof(Resources.Resources))]
        public string Barcode { get; set; }
        [Display(Name = "SalesPrice", ResourceType = typeof(Resources.Resources))]
        public double SalesPrice { get; set; } = 0;
        [Display(Name = "Cost", ResourceType = typeof(Resources.Resources))]
        public double Cost { get; set; } = 0;

        public virtual List<CustomerTaxDTO> CustomerTaxes { get; set; }
        [Display(Name = "Company", ResourceType = typeof(Resources.Resources))]
        public int CompanyId { get; set; }
        public virtual CompanyDTO Company { get; set; }
        [Display(Name = "Notes", ResourceType = typeof(Resources.Resources))]
        public string Notes { get; set; }
        [Display(Name = "GenerateBarcode", ResourceType = typeof(Resources.Resources))]
        public bool GenerateBarcode { get; set; }
    }
}
