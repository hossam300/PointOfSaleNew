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
    public class ShopService : BusinessService<Shop, Shop>, IShopService
    {
        IUnitOfWork _unitOfWork;
        public ShopService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Session> GetAllSessionByShopId(int id)
        {
          return  _unitOfWork.GetRepository<Session>().GetAll().AsNoTracking().Where(c=>c.ShopId==id).ToList();
        }

        public double GetOrderCashBySessionId(int id)
        {
            return _unitOfWork.GetRepository<OrderPayment>().GetAll().AsNoTracking().Where(c=>c.Order.SessionId==id).ToList().Sum(c => c.Amount);
        }
    }
}
