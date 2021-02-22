using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSale.DAL.ViewModels
{
  public  class ShopProductDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ShopId { get; set; }
        public string ProductName { get; set; }
        public int CurrentQuantity { get; set; }
        public int OldQuantity { get; set; }
        public int ActualQuantity { get; set; }
    }
}
