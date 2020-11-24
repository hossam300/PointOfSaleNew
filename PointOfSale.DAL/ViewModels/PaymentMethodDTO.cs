using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSale.DAL.ViewModels
{
    public class PaymentMethodDTO
    {
        public PaymentMethodDTO()
        {
            Orders = new List<OrderDTO>();
        }
        public int Id { get; set; }
        public string MehtodName { get; set; }
        public double Cash { get; set; }
        public List<OrderDTO> Orders { get; set; }
    }
}
