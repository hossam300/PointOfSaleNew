using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PointOfSale.BLL.BaseObjects.RePointOfSaleitoriesInterfaces;
using PointOfSale.DAL.Domains;
using PointOfSale.DAL.ViewModels;
using PointOfSale.Services.ISevices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PointOfSale.Services.Sevices
{
    public class ShopProductService : BusinessService<ShopProduct, ShopProduct>, IShopProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IRePointOfSaleitory<ShopProduct> _ShopProductRepo;
        private IRePointOfSaleitory<Shop> _ShopRepo;
        private IRePointOfSaleitory<Product> _ProductRepo;
        public ShopProductService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _ShopRepo = _unitOfWork.GetRepository<Shop>();
            _ShopProductRepo = _unitOfWork.GetRepository<ShopProduct>();
            _ProductRepo = _unitOfWork.GetRepository<Product>();
        }
        public List<ShopProductDTO> GetAllByShopId(int id)
        {
            var Products = _unitOfWork.GetRepository<Product>().GetAllWithoutInclude().Include(x => x.ShopProducts).ThenInclude(x => x.Shop).ToList();
            List<ShopProductDTO> ShopProducts = new List<ShopProductDTO>();
            foreach (var item in Products)
            {
                var shopproduct = item.ShopProducts.FirstOrDefault(x => x.ProductId == item.Id && x.ShopId == id);
                if (shopproduct != null)
                {
                    ShopProducts.Add(new ShopProductDTO
                    {
                        ActualQuantity = shopproduct.ActualQuantity,
                        CurrentQuantity = shopproduct.CurrentQuantity,
                        OldQuantity = shopproduct.OldQuantity,
                        ProductId = item.Id,
                        ProductName = item.Name,
                        Id = shopproduct.Id,
                        ShopId=id
                    });
                }
                else
                {
                    ShopProducts.Add(new ShopProductDTO
                    {
                        ActualQuantity = 0,
                        CurrentQuantity = 0,
                        OldQuantity = 0,
                        ProductId = item.Id,
                        ProductName = item.Name,
                        Id = 0,
                        ShopId = id
                    });
                }

            }
            return ShopProducts;
        }
    }
}
