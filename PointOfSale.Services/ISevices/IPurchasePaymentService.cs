using System;
using System.Collections.Generic;
using System.Text;
using PointOfSale.DAL.Domains;
using PointOfSale.DAL.ViewModels;

namespace PointOfSale.Services.ISevices
{
   public interface IPurchasePaymentService : IBusinessService<PurchaseOrderPayment, PurchaseOrderPayment>
    {
        List<PurchaseOrderDTO> GetOdersByMethod(int id);
    }
}
