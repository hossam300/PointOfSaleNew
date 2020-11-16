using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PointOfSale.DAL.Domains;
using PointOfSale.Services.ISevices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointOfSale.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopPrintersController : _BaseController<ShopPrinter, ShopPrinter>
    {
        private readonly IShopPrinterService _shopPrinterService;

        public ShopPrintersController(IShopPrinterService businessService) : base(businessService)
        {
            this._shopPrinterService = businessService;
        }
    }
}