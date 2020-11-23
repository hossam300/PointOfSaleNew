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
        public BranchDTo Branch { get; set; }
    }
    public class BranchDTo
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
