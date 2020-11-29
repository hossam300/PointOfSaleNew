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
   public class OrderItemService : BusinessService<OrderItem, OrderItem>, IOrderItemService
    {
        IUnitOfWork _unitOfWork;
        private IRePointOfSaleitory<OrderItem> _rePointOfSaleitory;
        public OrderItemService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _rePointOfSaleitory = _unitOfWork.GetRepository<OrderItem>();
        }

        public List<OrderItem> GetAllWithInclude()
        {
            return  _rePointOfSaleitory.GetAll().Include(c => c.Product).ThenInclude(p => p.ProductCategory).ToList();
        }
    }
}