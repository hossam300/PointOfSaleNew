namespace PointOfSale.DAL.Domains
{
    public class TaxMapping
    {
        // TODO Add Tax Adetional Prop
        public int Id { get; set; }
        public int? TaxOnProductId { get; set; }
        public virtual Tax TaxOnProduct { get; set; }
        public int? TaxToApplyId { get; set; }
        public virtual Tax TaxToApply { get; set; }
    }
}