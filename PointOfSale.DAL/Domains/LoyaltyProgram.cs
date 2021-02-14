using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PointOfSale.DAL.Domains
{
    public class LoyaltyProgram
    {
        public int Id { get; set; }
        [Display(Name = "LoyaltyProgramName", ResourceType = typeof(Resources.Resources))]
        public string Name { get; set; }
        [Display(Name = "PointPerMoneySpent", ResourceType = typeof(Resources.Resources))]
        public double PointPerMoneySpent { get; set; }
        public virtual List<Reward> Rewards { get; set; }
        public virtual List<Rules> Rules { get; set; }
    }
}
