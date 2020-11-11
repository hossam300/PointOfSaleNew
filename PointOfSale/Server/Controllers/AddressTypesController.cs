using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PointOfSale.DAL.Domains;
using PointOfSale.DAL.ViewModels;
using PointOfSale.Services.ISevices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PointOfSale.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressTypesController : _BaseController<AddressType, AddressTypeDTO>
    {
        private readonly IAddressTypeService _addressTypeService;

        public AddressTypesController(IAddressTypeService businessService) : base(businessService)
        {
            this._addressTypeService = businessService;
        }
    }
}
