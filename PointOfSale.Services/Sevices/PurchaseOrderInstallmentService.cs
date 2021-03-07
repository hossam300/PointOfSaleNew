using AutoMapper;
using PointOfSale.BLL.BaseObjects.RePointOfSaleitoriesInterfaces;
using PointOfSale.DAL.Domains;
using PointOfSale.Services.ISevices;
using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSale.Services.Sevices
{
   public class PurchaseOrderInstallmentService : BusinessService<PurchaseOrderInstallment, PurchaseOrderInstallment>, IPurchaseOrderInstallmentService
    {
        public PurchaseOrderInstallmentService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
    }
}