using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSale.DAL.ViewModels
{
    public class PurchasePaymentMethodDTO
    {
        public PurchasePaymentMethodDTO()
        {
            Orders = new List<PurchaseOrderDTO>();
        }
        public int Id { get; set; }
        public string MehtodName { get; set; }
        public double Cash { get; set; }
        public List<PurchaseOrderDTO> Orders { get; set; }
    }
}
