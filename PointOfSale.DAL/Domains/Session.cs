using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSale.DAL.Domains
{
    public class Session
    {
        public int Id { get; set; }
        public string SessionNo { get; set; }
        public int ShopId { get; set; }
        public virtual Shop Shop { get; set; }
        public string CreatorId { get; set; }
        public virtual SahlUserIdentity Creator { get; set; }
        public DateTime CreationDate { get; set; }
        public string CosedById { get; set; }
        public virtual SahlUserIdentity CosedBy { get; set; }
        public DateTime? ClosedDate { get; set; }
        public Status Status { get; set; }
    }
    public enum Status
    {
        Open = 1,
        Colsed = 2
    }
}
