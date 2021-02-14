using PointOfSale.DAL.Domains;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PointOfSale.DAL.ViewModels
{
    public class ShopPricingPaymentDTO
    {
        public int Id { get; set; }
        [Display(Name = "Pricelist", ResourceType = typeof(Resources.Resources))]
        public int? PricelistId { get; set; }
        public PricelistDTO Pricelist { get; set; }
        [Display(Name = "AdvancedPriceICollections", ResourceType = typeof(Resources.Resources))]
        public bool AdvancedPriceICollections { get; set; }
        [Display(Name = "AvailablePriceLists", ResourceType = typeof(Resources.Resources))]
        public ICollection<ShopPricelistDTO> AvailablePriceLists { get; set; }
        [Display(Name = "ProductPrices", ResourceType = typeof(Resources.Resources))]
        public ProductPrices ProductPrices { get; set; }
        [Display(Name = "GlobalDiscounts", ResourceType = typeof(Resources.Resources))]
        public bool GlobalDiscounts { get; set; }
        [Display(Name = "ManualDiscounts", ResourceType = typeof(Resources.Resources))]
        public bool ManualDiscounts { get; set; }
        [Display(Name = "LoyaltyProgram", ResourceType = typeof(Resources.Resources))]
        public bool LoyaltyProgram { get; set; }
        [Display(Name = "PriceControl", ResourceType = typeof(Resources.Resources))]
        public bool PriceControl { get; set; }
        [Display(Name = "PaymentMethods", ResourceType = typeof(Resources.Resources))]
        public List<ShopPaymentMethodDTO> PaymentMethods { get; set; }
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
        public ProductDTO TipProduct { get; set; }
    }
}