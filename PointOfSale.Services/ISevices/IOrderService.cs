using PointOfSale.DAL.Domains;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.Services.ISevices
{
    public interface IOrderService : IBusinessService<Order, Order>
    {
        Task<Order> GetOrderByNo(string id);
    }
}
