using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSale.DAL.ViewModels
{
    public class PaymentMethodDTO
    {
        public int Id { get; set; }
        public string MehtodName { get; set; }
        public bool Cash { get; set; }
    }
}
