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
    public class RewardsController : _BaseController<Reward, Reward>
    {
        private readonly IRewardService _rewardService;

        public RewardsController(IRewardService businessService) : base(businessService)
        {
            this._rewardService = businessService;
        }
    }
}
