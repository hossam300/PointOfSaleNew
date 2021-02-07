using AutoMapper;
using PointOfSale.BLL.BaseObjects.RePointOfSaleitoriesInterfaces;
using PointOfSale.DAL.Domains;
using PointOfSale.Services.ISevices;
using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSale.Services.Sevices
{
    public class SupplierContactService : BusinessService<SupplierContact, SupplierContact>, ISupplierContactService
    {
        public SupplierContactService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
    }
}
