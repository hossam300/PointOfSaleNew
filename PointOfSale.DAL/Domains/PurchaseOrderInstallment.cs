using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PointOfSale.DAL.Domains
{
    public class PurchaseOrderInstallment
    {
        public int Id { get; set; }
        [Display(Name = "PurchaseOrder", ResourceType = typeof(Resources.Resources))]
        public int PurchaseOrderId { get; set; }
        public PurchaseOrder PurchaseOrder { get; set; }
        [Display(Name = "InstallmentAmount", ResourceType = typeof(Resources.Resources))]
        public double InstallmentAmount { get; set; }
        [Display(Name = "InstallmentDate", ResourceType = typeof(Resources.Resources))]
        public DateTime InstallmentDate { get; set; }
    }
}
