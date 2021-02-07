using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PointOfSale.BLL.BaseObjects.RePointOfSaleitoriesInterfaces;
using PointOfSale.DAL.Domains;
using PointOfSale.Services.ISevices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.Services.Sevices
{
   public class PurchaseOrderService : BusinessService<PurchaseOrder, PurchaseOrder>, IPurchaseOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IRePointOfSaleitory<PurchaseOrder> _rePointOfSaleitory;
        public PurchaseOrderService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _rePointOfSaleitory = _unitOfWork.GetRepository<PurchaseOrder>();
        }
        public List<PurchaseOrder> GetAllWithInclude()
        {
            return _rePointOfSaleitory.GetAll().Include(c => c.PurchaseOrderItem).ThenInclude(o => o.Product).ToList();
        }

        public async Task<PurchaseOrder> GetOrderByNo(string id)
        {
            return await _rePointOfSaleitory.GetAll().Include(c => c.PurchaseOrderItem).ThenInclude(o => o.Product).FirstOrDefaultAsync(c => c.OrderNumber == id);
        }
    }
}