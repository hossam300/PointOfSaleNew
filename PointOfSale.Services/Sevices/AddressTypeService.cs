using AutoMapper;
using PointOfSale.BLL.BaseObjects.RePointOfSaleitoriesInterfaces;
using PointOfSale.DAL.Domains;
using PointOfSale.DAL.ViewModels;
using PointOfSale.Services.ISevices;
using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSale.Services.Sevices
{
    public class AddressTypeService : BusinessService<AddressType, AddressTypeDTO>, IAddressTypeService
    {
        public AddressTypeService(IUnitOfWork unitOfWork,IMapper mapper) : base(unitOfWork, mapper)
        {
        }
    }
}
