using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSale.DAL.Domains
{
    public class LoyaltyProgram
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double PointPerMoneySpent { get; set; }
        public virtual List<Reward> Rewards { get; set; }
        public virtual List<Rules> Rules { get; set; }
    }
}
