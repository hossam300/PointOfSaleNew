using PointOfSale.DAL.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSale.DAL.ViewModels
{
    public class ShopDTO
    {
        public int Id { get; set; }
        public string ShopName { get; set; }
        public string Header { get; set; }
        public string Footer { get; set; }
        public BranchDTO Branch { get; set; }
        public SessionDTO session { get; set; }
    }
    public class BranchDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class SessionDTO
    {
        public int Id { get; set; }
        public string SessionNo { get; set; }
        public int ShopId { get; set; }
        public string CreatorId { get; set; }
        public DateTime CreationDate { get; set; }
        public string CosedById { get; set; }
        public DateTime? ClosedDate { get; set; }
        public Status Status { get; set; }
        public double Cash { get; set; }
    }
}
