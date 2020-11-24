using PointOfSale.DAL.Domains;
using PointOfSale.DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSale.Services.ISevices
{
    public interface IOrderPaymentService : IBusinessService<OrderPayment, OrderPayment>
    {
        List<OrderDTO> GetOdersByMethod(int id);
    }
}
