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
    public class OrderService : BusinessService<Order, Order>, IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IRePointOfSaleitory<Order> _rePointOfSaleitory;
        private IRePointOfSaleitory<ShopProduct> _shopProductReo;
        public OrderService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _rePointOfSaleitory = _unitOfWork.GetRepository<Order>();
            _shopProductReo = _unitOfWork.GetRepository<ShopProduct>();
        }
        public override Order Insert(Order entities)
        {
             base.Insert(entities);
            foreach (var item in entities.OrderItem)
            {
                var shopProduct = _shopProductReo.GetAll().Where(c => c.ProductId == item.ProductId && c.ShopId == entities.ShopId).FirstOrDefault();
                if (shopProduct != null)
                {
                    shopProduct.OldQuantity = shopProduct.CurrentQuantity;
                    shopProduct.CurrentQuantity = shopProduct.CurrentQuantity -(int) item.Quantity;
                    shopProduct.ActualQuantity = shopProduct.CurrentQuantity;

                    _shopProductReo.Update(shopProduct);
                    _unitOfWork.SaveChanges();
                }
                else
                {
                    shopProduct = new ShopProduct
                    {
                        ActualQuantity = (int)item.Quantity,
                        OldQuantity = (int)item.Quantity,
                        ProductId = item.ProductId,
                        ShopId = entities.ShopId,
                        CreationDate = DateTime.Now,
                        CreatorId = entities.CreatorId,
                        CurrentQuantity =(int) item.Quantity,
                        ModfiedDate = DateTime.Now
                    };
                    _shopProductReo.Insert(shopProduct);
                    _unitOfWork.SaveChanges();
                }
            }
            return entities;
        }
        public List<Order> GetAllWithInclude()
        {
            return  _rePointOfSaleitory.GetAll().Include(c => c.OrderItem).ThenInclude(o => o.Product).ToList();
        }

        public async Task<Order> GetOrderByNo(string id)
        {
            return await _rePointOfSaleitory.GetAll().Include(c => c.OrderItem).ThenInclude(o => o.Product).FirstOrDefaultAsync(c => c.OrderNumber == id);
        }
    }
}
