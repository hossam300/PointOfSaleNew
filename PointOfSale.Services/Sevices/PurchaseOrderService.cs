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
        private IRePointOfSaleitory<ShopProduct> _shopProductReo;
        public PurchaseOrderService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _rePointOfSaleitory = _unitOfWork.GetRepository<PurchaseOrder>();
            _shopProductReo = _unitOfWork.GetRepository<ShopProduct>();
        }
        public List<PurchaseOrder> GetAllWithInclude()
        {
            return _rePointOfSaleitory.GetAll().Include(c => c.PurchaseOrderItem).ThenInclude(o => o.Product).ToList();
        }

        public async Task<PurchaseOrder> GetOrderByNo(string id)
        {
            return await _rePointOfSaleitory.GetAll().Include(c => c.PurchaseOrderItem).ThenInclude(o => o.Product).FirstOrDefaultAsync(c => c.OrderNumber == id);
        }

        public PurchaseOrder InsertPurchaseOrder(PurchaseOrder purchaseOrder)
        {
            base.Insert(purchaseOrder);
            foreach (var item in purchaseOrder.PurchaseOrderItem)
            {
                var shopProduct = _shopProductReo.GetAll().Where(c => c.ProductId == item.ProductId && c.ShopId == purchaseOrder.ShopId).FirstOrDefault();
                if (shopProduct != null)
                {
                    shopProduct.OldQuantity = shopProduct.CurrentQuantity;
                    shopProduct.CurrentQuantity = shopProduct.CurrentQuantity + item.Quantity;
                    shopProduct.ActualQuantity = shopProduct.CurrentQuantity ;
                    
                    _shopProductReo.Update(shopProduct);
                    _unitOfWork.SaveChanges();
                }
                else
                {
                    shopProduct = new ShopProduct
                    {
                        ActualQuantity=item.Quantity,
                        OldQuantity=item.Quantity,
                        ProductId=item.ProductId,
                        ShopId=purchaseOrder.ShopId,
                        CreationDate=DateTime.Now,
                        CreatorId=purchaseOrder.CreatorId,
                        CurrentQuantity=item.Quantity,
                        ModfiedDate=DateTime.Now
                    };
                    _shopProductReo.Insert(shopProduct);
                    _unitOfWork.SaveChanges();
                }
            }
            return purchaseOrder;
        }
    }
}