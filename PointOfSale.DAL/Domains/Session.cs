using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PointOfSale.DAL.Domains
{
    public class Session
    {
        public int Id { get; set; }
        [Display(Name = "SessionNo", ResourceType = typeof(Resources.Resources))]
        public string SessionNo { get; set; }
        [Display(Name = "Shop", ResourceType = typeof(Resources.Resources))]
        public int ShopId { get; set; }
        public virtual Shop Shop { get; set; }
        [Display(Name = "CreatedUserId", ResourceType = typeof(Resources.Resources))]
        public string CreatorId { get; set; }
        public virtual SahlUserIdentity Creator { get; set; }
        [Display(Name = "CreationDate", ResourceType = typeof(Resources.Resources))]
        public DateTime CreationDate { get; set; }
        [Display(Name = "CosedBy", ResourceType = typeof(Resources.Resources))]
        public string CosedById { get; set; }
        public virtual SahlUserIdentity CosedBy { get; set; }
        [Display(Name = "ClosedDate", ResourceType = typeof(Resources.Resources))]
        public DateTime? ClosedDate { get; set; }
        [Display(Name = "Status", ResourceType = typeof(Resources.Resources))]
        public Status Status { get; set; }
    }
    public enum Status
    {
        [Display(Name = "Open", ResourceType = typeof(Resources.Resources))]
        Open = 1,
        [Display(Name = "Colsed", ResourceType = typeof(Resources.Resources))]
        Colsed = 2
    }
}
