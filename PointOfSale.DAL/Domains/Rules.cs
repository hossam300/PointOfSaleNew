namespace PointOfSale.DAL.Domains
{
    public class Rules
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double PointsPerUnit { get; set; }
        public double PointsPerMoneySpent { get; set; }
    }
}