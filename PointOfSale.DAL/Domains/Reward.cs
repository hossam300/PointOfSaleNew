namespace PointOfSale.DAL.Domains
{
    public class Reward
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double RewardCost { get; set; }
        public double MinimumPoints { get; set; }
        public RewardType RewardType { get; set; }
        public int? GiftProductId { get; set; }
        public virtual Product GiftProduct { get; set; }
        public int? DiscountProductId { get; set; }
        public virtual Product DiscountProduct { get; set; }
        public ApplyDiscountType ApplyDiscountType { get; set; }
        public double ApplyDiscountAmount { get; set; }
        public double ApplyDiscountPercentage { get; set; }
        public double MaxDiscountAmount { get; set; }
        public double FixedAmount { get; set; }
        public double MinimumAmount { get; set; }
    }
    public enum RewardType
    {
        FreeProduct = 1,
        Discount = 2
    }
    public enum ApplyDiscountType
    {
        Percentage = 1,
        FixedAmount = 2
    }
}