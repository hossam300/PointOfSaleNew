﻿using Microsoft.AspNetCore.Http;
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
    public class ShopsController : _BaseController<Shop, Shop>
    {
        private readonly IShopService _shopService;

        public ShopsController(IShopService businessService) : base(businessService)
        {
            this._shopService = businessService;
        }
    }
}