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
  public  class PurchasePaymentService : BusinessService<PurchaseOrderPayment, PurchaseOrderPayment>, IPurchasePaymentService
    {
        IUnitOfWork _unitOfWork;
        public PurchasePaymentService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
        }

        public List<PurchaseOrderDTO> GetOdersByMethod(int id)
        {
            return _unitOfWork.GetRepository<PurchaseOrder>().GetAll().Where(c => c.PurchaseOrderPayments.Any(c => c.PaymentMethodId == id)).Select(x => new PurchaseOrderDTO
            {
                CreationDate = x.CreationDate,
                CreatorId = x.CreatorId,
                SupplierName = x.Supplier.Name,
                Discount = x.Discount,
                Id = x.Id,
                LastModificationDate = x.LastModificationDate,
                ModifiedByID = x.ModifiedByID,
                Notes = x.Notes,
                OrderDate = x.OrderDate,
                OrderNumber = x.OrderNumber,
                OrderTax = x.OrderTax,
                OrderType = x.OrderType,
                ShopId = x.ShopId,
                Total = x.Total,
                TotalItemCount = x.TotalItemCount,
                SessionId = x.SupplierId,
                OrderId = x.Id
            }).ToList();
        }
    }
}