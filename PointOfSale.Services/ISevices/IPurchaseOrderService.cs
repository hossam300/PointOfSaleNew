using PointOfSale.DAL.Domains;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.Services.ISevices
{
    public interface IPurchaseOrderService : IBusinessService<PurchaseOrder, PurchaseOrder>
    {
        List<PurchaseOrder> GetAllWithInclude();
        Task<PurchaseOrder> GetOrderByNo(string id);
    }
}
