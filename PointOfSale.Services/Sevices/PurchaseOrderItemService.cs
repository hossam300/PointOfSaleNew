using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PointOfSale.BLL.BaseObjects.RePointOfSaleitoriesInterfaces;
using PointOfSale.DAL.Domains;
using PointOfSale.Services.ISevices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PointOfSale.Services.Sevices
{
    public class PurchaseOrderItemService : BusinessService<PurchaseOrderItem, PurchaseOrderItem>, IPurchaseOrderItemService
    {
        IUnitOfWork _unitOfWork;
        private IRePointOfSaleitory<PurchaseOrderItem> _rePointOfSaleitory;
        public PurchaseOrderItemService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _rePointOfSaleitory = _unitOfWork.GetRepository<PurchaseOrderItem>();
        }

        public List<PurchaseOrderItem> GetAllWithInclude()
        {
            return _rePointOfSaleitory.GetAll().Include(c => c.Product).ThenInclude(p => p.ProductCategory).ToList();
        }
    }
}