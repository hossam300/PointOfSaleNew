using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PointOfSale.BLL.BaseObjects.RePointOfSaleitoriesInterfaces;
using PointOfSale.DAL.Domains;
using PointOfSale.DAL.ViewModels;
using PointOfSale.Services.ISevices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PointOfSale.Services.Sevices
{
    public class ShopService : BusinessService<Shop, Shop>, IShopService
    {
        IUnitOfWork _unitOfWork;
        public ShopService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
        }

        public AddShopDTO GetAddShopDTOById(int id)
        {
            return _unitOfWork.GetRepository<Shop>().GetAll().AsNoTracking().Where(c => c.Id == id).Select(x => new AddShopDTO
            {
                Id = x.Id,
                AllowedEmployees = x.AllowedEmployees.Select(x => new ShopEmployeeDTO { Id = x.Id, UserId = x.UserId, ShopId = x.ShopId }).ToList(),
                BranchId = x.BranchId,
                IsRestaurant = x.IsRestaurant,
                LoginWithEmployees = x.LoginWithEmployees,
                Name = x.Name
            }).FirstOrDefault();
        }

        public List<Session> GetAllSessionByShopId(int id)
        {
            return _unitOfWork.GetRepository<Session>().GetAll().AsNoTracking().Where(c => c.ShopId == id).ToList();
        }

        public double GetOrderCashBySessionId(int id)
        {
            return _unitOfWork.GetRepository<OrderPayment>().GetAll().AsNoTracking().Where(c => c.Order.SessionId == id).ToList().Sum(c => c.Amount);
        }

        public ShopInterfaceDTO GetShopInterfaceDTOById(int id)
        {
            return _unitOfWork.GetRepository<Shop>().GetAllWithoutInclude().Include(x => x.AvailableCategories)
                .Include(x => x.BarcodeScanner).Include(x => x.Floors).Include(x => x.Printers).Include(x => x.StartCategory).Include(x => x.FiscalPosition)
                .AsNoTracking().Where(c => c.Id == id).Select(shop => new ShopInterfaceDTO
                {
                    Id = shop.Id,
                    AvailableCategories = shop.AvailableCategories.Select(x => new ShopProductCategoryDTO { Id = x.Id, ShopId = x.ShopId, ProductCategoryId = x.ProductCategoryId }).ToList(),
                    BarcodeScanner = (shop.BarcodeScannerId == null) ? null : getBarcodeScannerDTO(shop.BarcodeScanner),
                    BarcodeScannerId = shop.BarcodeScannerId,
                    CategoryPictures = shop.CategoryPictures,
                    ConnectDevices = shop.ConnectDevices,
                    Floors = shop.Floors.Select(x => new ShopFloorDTO { Id = x.Id, ShopId = x.ShopId, FloorId = x.FloorId }).ToList(),
                    FloorsTables = shop.FloorsTables,
                    LargeScrollbars = shop.LargeScrollbars,
                    OrderlineNotes = shop.OrderlineNotes,
                    OrderPrinter = shop.OrderPrinter,
                    PrinterIPAddress = shop.PrinterIPAddress,
                    Printers = shop.Printers.Select(x => new ShopPrinterDTO { Id = x.Id, ShopId = x.ShopId, PrinterId = x.PrinterId }).ToList(),
                    RestrictAvailableCategories = shop.RestrictAvailableCategories,
                    SetStartCategory = shop.SetStartCategory,
                    StartCategory = (shop.StartCategoryId == null) ? null : GetStartCategoryDTO(shop.StartCategory),
                    StartCategoryId = shop.StartCategoryId,
                    TableManagement = shop.TableManagement,
                    VirtualKeyBoard = shop.VirtualKeyBoard,
                    FiscalPosition = (shop.FiscalPositionId == null) ? null : GetFiscalPositionDTO(shop.FiscalPosition),
                    FiscalPositionId = shop.FiscalPositionId,
                    FiscalPositionPerOrder = shop.FiscalPositionPerOrder,
                    SpecificFiscalPosition = shop.SpecificFiscalPosition
                }).FirstOrDefault();
        }

        public static FiscalPositionDTO GetFiscalPositionDTO(FiscalPosition fiscalPosition)
        {
            return new FiscalPositionDTO
            {
                Id = fiscalPosition.Id,
                CountryGroupId = fiscalPosition.CountryGroupId,
                CountryId = fiscalPosition.CountryId,
                DetectAutomatically = fiscalPosition.DetectAutomatically,
                FiscalPositionName = fiscalPosition.FiscalPositionName,
                VATRequired = fiscalPosition.VATRequired
            };
        }

        public static ProductCategoryDTO GetStartCategoryDTO(ProductCategory startCategory)
        {
            return new ProductCategoryDTO
            {
                CategoryName = startCategory.CategoryName,
                Id = startCategory.Id,
                ImagePath = startCategory.ImagePath,
                //ParentCategory = startCategory.ParentCategory,
                ParentCategoryId = startCategory.ParentCategoryId
            };
        }

        public static BarcodeScannerDTO getBarcodeScannerDTO(BarcodeScanner barcodeScanner)
        {
            return new BarcodeScannerDTO
            {
                BarcodeNomenclature = barcodeScanner.BarcodeNomenclature,
                BarcodePattern = barcodeScanner.BarcodePattern,
                BarcodeTypeId = barcodeScanner.BarcodeTypeId,
                Encoding = barcodeScanner.Encoding,
                Id = barcodeScanner.Id,
                Sequence = barcodeScanner.Sequence
            };
        }

        public void UpdateShopInterfaceDTO(ShopInterfaceDTO ShopInterfaceDTO)
        {
            var shop = _unitOfWork.GetRepository<Shop>().GetAllWithoutInclude().Include(x => x.Floors)
                .Include(x => x.AvailableCategories).Include(x => x.StartCategory).Include(x => x.BarcodeScanner)
                .Include(x => x.Printers).Include(x => x.FiscalPosition)
                .AsNoTracking().Where(c => c.Id == ShopInterfaceDTO.Id).FirstOrDefault();
            _unitOfWork.GetRepository<ShopProductCategory>().Delete(shop.AvailableCategories);
            _unitOfWork.GetRepository<ShopPrinter>().Delete(shop.Printers);
            _unitOfWork.GetRepository<ShopFloor>().Delete(shop.Floors);
            _UnitOfWork.SaveChanges();
            shop.Id = ShopInterfaceDTO.Id;
            shop.AvailableCategories = new List<ShopProductCategory>();
            shop.AvailableCategories = ShopInterfaceDTO.AvailableCategories.Select(x => new ShopProductCategory { Id = x.Id, ProductCategoryId = x.ProductCategoryId, ShopId = x.ShopId }).ToList();
            shop.BarcodeScanner = GetBarcodeScanner(ShopInterfaceDTO.BarcodeScanner);
            shop.BarcodeScannerId = ShopInterfaceDTO.BarcodeScannerId;
            shop.CategoryPictures = ShopInterfaceDTO.CategoryPictures;
            shop.ConnectDevices = ShopInterfaceDTO.ConnectDevices;
            shop.Floors = new List<ShopFloor>();
            shop.Floors = ShopInterfaceDTO.Floors.Select(x => new ShopFloor { Id = x.Id, FloorId = x.FloorId, ShopId = x.ShopId }).ToList();
            shop.FloorsTables = ShopInterfaceDTO.FloorsTables;
            shop.LargeScrollbars = ShopInterfaceDTO.LargeScrollbars;
            shop.OrderlineNotes = ShopInterfaceDTO.OrderlineNotes;
            shop.OrderPrinter = ShopInterfaceDTO.OrderPrinter;
            shop.PrinterIPAddress = ShopInterfaceDTO.PrinterIPAddress;
            shop.Printers = new List<ShopPrinter>();
            shop.Printers = ShopInterfaceDTO.Printers.Select(x => new ShopPrinter { Id = x.Id, PrinterId = x.PrinterId, ShopId = x.ShopId }).ToList();
            shop.RestrictAvailableCategories = ShopInterfaceDTO.RestrictAvailableCategories;
            shop.SetStartCategory = ShopInterfaceDTO.SetStartCategory;
            shop.StartCategory = GetStartCategory(ShopInterfaceDTO.StartCategory);
            shop.StartCategoryId = ShopInterfaceDTO.StartCategoryId;
            shop.TableManagement = ShopInterfaceDTO.TableManagement;
            shop.VirtualKeyBoard = ShopInterfaceDTO.VirtualKeyBoard;
            shop.FiscalPosition = GetFiscalPosition(ShopInterfaceDTO.FiscalPosition);
            shop.FiscalPositionId = ShopInterfaceDTO.FiscalPositionId;
            shop.FiscalPositionPerOrder = ShopInterfaceDTO.FiscalPositionPerOrder;
            shop.SpecificFiscalPosition = ShopInterfaceDTO.SpecificFiscalPosition;
            _unitOfWork.GetRepository<Shop>().Update(shop);
            _UnitOfWork.SaveChanges();
        }

        public static FiscalPosition GetFiscalPosition(FiscalPositionDTO fiscalPosition)
        {
            return new FiscalPosition
            {
                CountryId = fiscalPosition.CountryId,
                CountryGroupId = fiscalPosition.CountryGroupId,
                DetectAutomatically = fiscalPosition.DetectAutomatically,
                FiscalPositionName = fiscalPosition.FiscalPositionName,
                Id = fiscalPosition.Id,
                VATRequired = fiscalPosition.VATRequired
            };
        }

        public static ProductCategory GetStartCategory(ProductCategoryDTO startCategory)
        {
            return new ProductCategory
            {
                CategoryName = startCategory.CategoryName,
                Id = startCategory.Id,
                ImagePath = startCategory.ImagePath,
                ParentCategoryId = startCategory.ParentCategoryId
            };
        }

        public static BarcodeScanner GetBarcodeScanner(BarcodeScannerDTO barcodeScanner)
        {
            return new BarcodeScanner
            {
                BarcodeNomenclature = barcodeScanner.BarcodeNomenclature,
                BarcodePattern = barcodeScanner.BarcodePattern,
                BarcodeTypeId = barcodeScanner.BarcodeTypeId,
                Encoding = barcodeScanner.Encoding,
                Id = barcodeScanner.Id,
                Sequence = barcodeScanner.Sequence
            };
        }

        public void UpdateAddShopDTO(AddShopDTO AddShop)
        {
            var shop = _unitOfWork.GetRepository<Shop>().GetAllWithoutInclude().Include(x => x.AllowedEmployees).AsNoTracking().Where(c => c.Id == AddShop.Id).FirstOrDefault();
            shop.Id = AddShop.Id;
            _unitOfWork.GetRepository<ShopEmployee>().Delete(shop.AllowedEmployees);
            _UnitOfWork.SaveChanges();
            shop.AllowedEmployees = AddShop.AllowedEmployees.Select(x => new ShopEmployee
            {
                Id = x.Id,
                ShopId = x.ShopId,
                UserId = x.UserId
            }).ToList();
            shop.BranchId = AddShop.BranchId;
            shop.IsRestaurant = AddShop.IsRestaurant;
            shop.LoginWithEmployees = AddShop.LoginWithEmployees;
            shop.Name = AddShop.Name;
            _unitOfWork.GetRepository<Shop>().Update(shop);
            _UnitOfWork.SaveChanges();
        }

        public ShopPricingPaymentDTO GetShopPricingPaymentDTOById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
