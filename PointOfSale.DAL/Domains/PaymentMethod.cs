namespace PointOfSale.DAL.Domains
{
    public class PaymentMethod
    {
        public int Id { get; set; }
        public string MehtodName { get; set; }
        public bool Cash { get; set; }
        //Intermediary Account
    }
}