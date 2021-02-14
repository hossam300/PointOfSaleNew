using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PointOfSale.DAL.Domains;
using PointOfSale.DAL.ViewModels;
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
        [HttpGet("GetAddShopDTOById/{id}")]
        public IActionResult GetAddShopDTOById(int id)
        {
            AddShopDTO shop = _shopService.GetAddShopDTOById(id);
            return Ok(new AddShopDTO { Id = shop.Id, Name = shop.Name, AllowedEmployees = shop.AllowedEmployees, BranchId = null, IsRestaurant = shop.IsRestaurant, LoginWithEmployees = shop.LoginWithEmployees });
        }
        //GetShopInterfaceDTOById
        [HttpGet("GetShopInterfaceDTOById/{id}")]
        public IActionResult GetShopInterfaceDTOById(int id)
        {
            ShopInterfaceDTO shop = _shopService.GetShopInterfaceDTOById(id);
            return Ok(new ShopInterfaceDTO
            {
                Id = shop.Id,
                AvailableCategories = shop.AvailableCategories,
                BarcodeScanner = shop.BarcodeScanner,
                BarcodeScannerId = shop.BarcodeScannerId,
                CategoryPictures = shop.CategoryPictures,
                ConnectDevices = shop.ConnectDevices,
                Floors = shop.Floors,
                FloorsTables = shop.FloorsTables,
                LargeScrollbars = shop.LargeScrollbars,
                OrderlineNotes = shop.OrderlineNotes,
                OrderPrinter = shop.OrderPrinter,
                PrinterIPAddress = shop.PrinterIPAddress,
                Printers = shop.Printers,
                RestrictAvailableCategories = shop.RestrictAvailableCategories,
                SetStartCategory = shop.SetStartCategory,
                StartCategory = shop.StartCategory,
                StartCategoryId = shop.StartCategoryId,
                TableManagement = shop.TableManagement,
                VirtualKeyBoard = shop.VirtualKeyBoard,
                FiscalPosition = shop.FiscalPosition,
                FiscalPositionId = shop.FiscalPositionId,
                FiscalPositionPerOrder = shop.FiscalPositionPerOrder,
                SpecificFiscalPosition = shop.SpecificFiscalPosition
            });
        }

        //GetShopPricingPaymentDTOById
        [HttpGet("GetShopPricingPaymentDTOById/{id}")]
        public IActionResult GetShopPricingPaymentDTOById(int id)
        {
            ShopPricingPaymentDTO shop = _shopService.GetShopPricingPaymentDTOById(id);
            return Ok(shop);
        }
        //GetShopBillsReceiptDTOById
        [HttpGet("GetShopBillsReceiptDTOById/{id}")]
        public IActionResult GetShopBillsReceiptDTOById(int id)
        {
            ShopBillsReceiptDTO shop = _shopService.GetShopBillsReceiptDTOById(id);
            return Ok(shop);
        }
        [HttpPost("InsertAddShop")]
        public IActionResult InsertAddShop([FromBody] AddShopDTO addShop)
        {
            var shop = new Shop();
            shop.Name = addShop.Name;
            shop.AllowedEmployees = addShop.AllowedEmployees.Select(x => new ShopEmployee { ShopId = x.ShopId, UserId = x.UserId, Id = x.Id }).ToList();
            shop.BranchId = addShop.BranchId;
            shop.IsRestaurant = addShop.IsRestaurant;
            shop.LoginWithEmployees = addShop.LoginWithEmployees;
            _shopService.Insert(shop);
            return Ok(new AddShopDTO { Id = shop.Id, Name = shop.Name, AllowedEmployees = shop.AllowedEmployees.Select(x => new ShopEmployeeDTO { ShopId = x.ShopId, UserId = x.UserId, Id = x.Id }).ToList(), BranchId = null, IsRestaurant = shop.IsRestaurant, LoginWithEmployees = shop.LoginWithEmployees });
        }
        [HttpPost("UpdateShopInterfaceDTO")]
        public IActionResult UpdateShopInterfaceDTO([FromBody] ShopInterfaceDTO shopInterface)
        {
            _shopService.UpdateShopInterfaceDTO(shopInterface);
            return Ok(shopInterface);
        }
        [HttpPost("UpdateAddShop")]
        public IActionResult UpdateAddShop([FromBody] AddShopDTO addShop)
        {
            //var shop = _shopService.GetDetails(addShop.Id);
            //shop.Name = addShop.Name;
            //shop.AllowedEmployees = addShop.AllowedEmployees;
            //shop.BranchId = addShop.BranchId;
            //shop.Branch = addShop.Branch;
            //shop.IsRestaurant = addShop.IsRestaurant;
            //shop.LoginWithEmployees = addShop.LoginWithEmployees;
            _shopService.UpdateAddShopDTO(addShop);
            return Ok(addShop);
        }
        [HttpPost("UpdateShopPricingPaymentDTO")]
        public IActionResult UpdateShopPricingPaymentDTO([FromBody] ShopPricingPaymentDTO shopPricing)
        {
            //var shop = _shopService.GetDetails(addShop.Id);
            //shop.Name = addShop.Name;
            //shop.AllowedEmployees = addShop.AllowedEmployees;
            //shop.BranchId = addShop.BranchId;
            //shop.Branch = addShop.Branch;
            //shop.IsRestaurant = addShop.IsRestaurant;
            //shop.LoginWithEmployees = addShop.LoginWithEmployees;
            _shopService.UpdateShopPricingPaymentDTO(shopPricing);
            return Ok(shopPricing);
        }
        //UpdateShopBillsReceiptDTO
        [HttpPost("UpdateShopBillsReceiptDTO")]
        public IActionResult UpdateShopBillsReceiptDTO([FromBody] ShopBillsReceiptDTO shopBills)
        {
            _shopService.UpdateShopBillsReceiptDTO(shopBills);
            return Ok(shopBills);
        }
        [HttpGet("GetByIdWithoutInclude/{id}")]
        public IActionResult GetByIdWithoutInclude(int id)
        {
            var shop = _shopService.GetDetails(id);
            return Ok(new ShopDTO { Id = shop.Id, ShopName = shop.Name, Footer = shop.Footer, Header = shop.Header, Branch = null });
        }
        [HttpGet("GetAllWithoutInclude")]
        public IActionResult GetAllWithoutInclude()
        {
            var shop = _shopService.GetAllWithoutInclude().Select(c => new ShopDTO
            {
                Id = c.Id,
                ShopName = c.Name,
                Footer = c.Footer,
                Header = c.Header,
                Branch = null,
                session = _shopService.GetAllSessionByShopId(c.Id).Select(x => new SessionDTO
                {
                    Id = x.Id,
                    SessionNo = x.SessionNo,
                    Status = x.Status,
                    ClosedDate = x.ClosedDate,
                    Cash = _shopService.GetOrderCashBySessionId(x.Id),
                    CosedById = x.CosedById,
                    CreationDate = x.CreationDate,
                    CreatorId = x.CreatorId,
                    ShopId = x.ShopId
                }).OrderByDescending(x => x.Id).FirstOrDefault()
            });
            return Ok(shop);
        }
        BranchDTO GetBranch(Branch branch)
        {
            return new BranchDTO { Id = branch.Id, Name = branch.Name };
        }
    }
}