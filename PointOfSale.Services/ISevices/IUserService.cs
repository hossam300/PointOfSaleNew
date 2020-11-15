using PointOfSale.DAL.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSale.Services.ISevices
{
    public interface IUserService : IBusinessService<SahlUserIdentity, SahlUserIdentity>
    {
    }
}
