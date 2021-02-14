using PointOfSale.DAL.Domains;
using PointOfSale.DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PointOfSale.Services.ISevices
{
    public interface IShopService : IBusinessService<Shop, Shop>
    {
        List<Session> GetAllSessionByShopId(int id);
        double GetOrderCashBySessionId(int id);
        AddShopDTO GetAddShopDTOById(int id);
        void UpdateAddShopDTO(AddShopDTO shop);
        ShopInterfaceDTO GetShopInterfaceDTOById(int id);
        void UpdateShopInterfaceDTO(ShopInterfaceDTO shopInterface);
        ShopPricingPaymentDTO GetShopPricingPaymentDTOById(int id);
    }
}
