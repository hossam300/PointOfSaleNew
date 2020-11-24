using AutoMapper;
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
    public class OrderPaymentService : BusinessService<OrderPayment, OrderPayment>, IOrderPaymentService
    {
        IUnitOfWork _unitOfWork;
        public OrderPaymentService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
        }

        public List<OrderDTO> GetOdersByMethod(int id)
        {
            return _unitOfWork.GetRepository<Order>().GetAll().Where(c => c.OrderPayments.Any(c => c.PaymentMethodId == id)).Select(x => new OrderDTO
            {
                CreationDate = x.CreationDate,
                CreatorId = x.CreatorId,
                CustomerName = x.Customer.Name,
                Discount = x.Discount,
                Id = x.Id,
                LastModificationDate = x.LastModificationDate,
                ModifiedByID = x.ModifiedByID,
                Notes = x.Notes,
                OrderDate = x.OrderDate,
                OrderNumber = x.OrderNumber,
                OrderTax = x.OrderTax,
                OrderType = x.OrderType,
                SessionId = x.SessionId,
                ShopId = x.ShopId,
                Total = x.Total,
                TotalItemCount = x.TotalItemCount


            }).ToList();
        }
    }
}