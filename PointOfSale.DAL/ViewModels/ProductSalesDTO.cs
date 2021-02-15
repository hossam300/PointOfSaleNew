using PointOfSale.DAL.Domains;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PointOfSale.DAL.ViewModels
{
    public class ProductSalesDTO
    {
        public int Id { get; set; }
        [Display(Name = "InvoicingPolicy", ResourceType = typeof(Resources.Resources))]
        public InvoicingPolicy InvoicingPolicy { get; set; }
        [Display(Name = "SubscriptionProduct", ResourceType = typeof(Resources.Resources))]
        public bool SubscriptionProduct { get; set; }
        [Display(Name = "IsEventTicket", ResourceType = typeof(Resources.Resources))]
        public bool IsEventTicket { get; set; }
        public virtual List<OptionalProductDTO> OptionalProducts { get; set; }
        [Display(Name = "SalesDescription", ResourceType = typeof(Resources.Resources))]
        public string SalesDescription { get; set; }

        [Display(Name = "AvailableInPOS", ResourceType = typeof(Resources.Resources))]
        public bool AvailableInPOS { get; set; }
        [Display(Name = "Category", ResourceType = typeof(Resources.Resources))]
        public int? CategoryId { get; set; }
        public virtual ProductCategoryDTO Category { get; set; }
        [Display(Name = "ToWeighWithScale", ResourceType = typeof(Resources.Resources))]
        public bool ToWeighWithScale { get; set; }
    }
}
