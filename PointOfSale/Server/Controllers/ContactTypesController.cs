using Microsoft.AspNetCore.Mvc;
using PointOfSale.DAL.Domains;
using PointOfSale.DAL.ViewModels;
using PointOfSale.Services.ISevices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PointOfSale.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactTypesController : _BaseController<ContactType, ContactTypeDTO>
    {
       
        private IContactTypeService _contactTypeService;
        public ContactTypesController(IContactTypeService businessService) : base(businessService)
        {
            _contactTypeService = businessService;
        }
    }
}
