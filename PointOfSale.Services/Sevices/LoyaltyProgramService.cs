﻿using AutoMapper;
using PointOfSale.BLL.BaseObjects.RePointOfSaleitoriesInterfaces;
using PointOfSale.DAL.Domains;
using PointOfSale.Services.ISevices;
using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSale.Services.Sevices
{
    public class LoyaltyProgramService : BusinessService<LoyaltyProgram, LoyaltyProgram>, ILoyaltyProgramService
    {
        public LoyaltyProgramService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
    }
}
