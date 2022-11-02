using Homemade.BLL.General;
using Homemade.BLL.Setting;
using Homemade.BLL.ViewModel.Driver;
using Homemade.BLL.ViewModel.Order;
using Homemade.BLL.ViewModel.Site;
using Homemade.BLL.ViewModel.Vendor;
using Homemade.DAL.Contract;
using Homemade.Model;
using Homemade.Model.Order;
using Homemade.Model.Setting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.Order
{
    public class BlOrders
    {
        #region Declartion
        private readonly IUOW Uow;
        private readonly BLGeneral _bLGeneral;
        private readonly BlTokens _blTokens;
        private readonly IConfiguration _configuration;
        private readonly BlConfiguration _blConfiguration;
        public BlOrders(IUOW _uow, BlTokens blTokens, BLGeneral bLGeneral, IConfiguration configuration, BlConfiguration blConfiguration)
        {
            this.Uow = _uow;
            _bLGeneral = bLGeneral;
            _blTokens = blTokens;
            _configuration = configuration;
            _blConfiguration = blConfiguration;
        }
        #endregion
        #region Helpers
        public IQueryable<OrdersVendorViewModel> GetAllOrdersVendorViewModelByCustomer(int CustomersID, string lang, string MainPathView)
        {
            var getData = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.Orders.CustomersID == CustomersID).OrderByDescending(p => p.CreateDate)
            .Select(p => new OrdersVendorViewModel()
            {
                DeliveryPrice = p.DeliveryPrice,
                Discount = p.Discount,
                InvoiceImage = !string.IsNullOrEmpty(p.InvoiceImage) ? (MainPathView + p.InvoiceImage) : "/Images/noImage.png",
                Notes = p.Notes,
                OrdersID = p.OrdersID,
                OrderStatusID = p.OrderStatusID,
                OrderVendorGuid = p.OrderVendorGuid,
                OrdersGuid = p.Orders.OrdersGuid,
                OrderVendorID = p.OrderVendorID,
                Price = p.Price,
                Total = p.Total,
                TrackNo = p.TrackNo,
                Vat = p.VatValue,
                EnableDate = p.EnableDate,
                IsDeleted = p.IsDeleted,
                UpdateDate = p.UpdateDate,
                UserUpdate = p.UserUpdate,
                CreateDate = p.CreateDate,
                DeleteDate = p.DeleteDate,
                UserId = p.UserId,
                UserDelete = p.UserDelete,
                UserEnable = p.UserEnable,
                VendorsID = p.VendorsID,
                IsEnable = p.IsEnable,
                CustomersID = p.Orders.CustomersID,
                CustomersName = p.Orders.Customers.FirstName + " " + p.Orders.Customers.SeconedName,
                CustomersMobileNo = p.Orders.Customers.MobileNo,
                VendorsName = lang == "ar" ? p.Vendors.StoreNameAr : (p.Vendors.StoreNameEn),
                OrderStatusName = lang == "ar" ? p.OrderStatus.NameAR : p.OrderStatus.NameEN,
                CustomerBlockName = lang == "ar" ? p.Orders.CustomerLocation.Block.NameAR : p.Orders.CustomerLocation.Block.NameEN,
                VendorBlockName = p.Vendors.Block != null ? (lang == "ar" ? p.Vendors.Block.NameAR : p.Vendors.Block.NameEN) : string.Empty,
                CreateDateString = p.CreateDate.ToString("hh:mm tt"),
                OrderTypeName = lang == "ar" ? (p.OrderTypeId == (int)OrderTypeEnum.Now ? "فورى" : "مجدول") : (p.OrderTypeId == (int)OrderTypeEnum.Now ? "Now" : "Schedule"),
                ScheduleDateString = p.OrderTypeId == (int)OrderTypeEnum.Schedule ? (p.ScheduleDate != null ? p.ScheduleDate.Value.ToString("dd/MM/yyyy") : "---") : "---",
                ScheduleDate = p.ScheduleDate,
                ScheduleTimeString = p.OrderTypeId == (int)OrderTypeEnum.Schedule ? (p.ScheduleTime != null ? p.ScheduleTime.Value.ToString("hh:mm tt") : "---") : "---",
                IntegrationOrderId = !string.IsNullOrEmpty(p.IntegrationOrderId) ? p.IntegrationOrderId : "---",
                VatProduct = Math.Round(p.VatProduct, 2),
                DeliveryVatValue = Math.Round(p.DeliveryVatValue, 2),
                VatValue = Math.Round(p.VatValue, 2),
                Place = p.CaptainTypeId == (int)CaptainTypeEnum.Shipping_Company ? (lang == "ar" ? p.ShippingCompany.NameAr : p.ShippingCompany.NameAr) :
                 p.CaptainTypeId == (int)CaptainTypeEnum.Store ? (lang == "ar" ? "المتجر" : "Store") : (lang == "ar" ? "هوم ميد" : "Home Made"),
            });
            return getData;
        }
        public bool IsExistOrderHistoryByOrderStatus(int OrderVendorID, int OrderStatusID) => Uow.OrderHistory.GetAll(x => !x.IsDeleted && x.OrderVendorID == OrderVendorID && x.OrderStatusID == OrderStatusID).Any();
        public IQueryable<OrdersVendorViewModel> GetAllOrdersVendorViewModelBySearch(int? OrdersStatus, string[] ListRegionID, string[] ListCityID, string[] ListBlockID, string[] ListVendorID, string CustomerNameMobile, int? orderTypeId, DateTime? fromDateTime, DateTime? toDateTime, string[] ListOrdersStatusID, string lang, string MainPathView)
        {
            if (ListRegionID != null)
            {
                if (ListRegionID.Count(p => !string.IsNullOrEmpty(p)) == 0)
                {
                    ListRegionID = null;
                }
            }
            if (ListCityID != null)
            {
                if (ListCityID.Count(p => !string.IsNullOrEmpty(p)) == 0)
                {
                    ListCityID = null;
                }
            }
            if (ListBlockID != null)
            {
                if (ListBlockID.Count(p => !string.IsNullOrEmpty(p)) == 0)
                {
                    ListBlockID = null;
                }
            }
            if (ListVendorID != null)
            {
                if (ListVendorID.Count(p => !string.IsNullOrEmpty(p)) == 0)
                {
                    ListVendorID = null;
                }
            }
            if (ListOrdersStatusID != null)
            {
                if (ListOrdersStatusID.Count(p => !string.IsNullOrEmpty(p)) == 0)
                {
                    ListOrdersStatusID = null;
                }
            }
            var getData = Uow.OrderVendor.GetAll(x => !x.IsDeleted
                                                   && (ListRegionID != null ? ListRegionID.Any(y => x.Orders.CustomerLocation.Block.City.RegionID.ToString() == y) : true)
                                                   && (ListCityID != null ? ListCityID.Any(y => x.Orders.CustomerLocation.Block.CityID.ToString() == y) : true)
                                                   && (OrdersStatus != null ? x.OrderStatusID == OrdersStatus : true)
                                                   && (ListBlockID != null ? ListBlockID.Any(y => x.Orders.CustomerLocation.BlockID.ToString() == y) : true)
                                                   && (ListVendorID != null ? ListVendorID.Any(y => x.VendorsID.ToString() == y) : true)
                                                   && (!string.IsNullOrEmpty(CustomerNameMobile) ? (x.Orders.Customers.FirstName.Contains(CustomerNameMobile) || x.Orders.Customers.MobileNo.Contains(CustomerNameMobile)) : true)
                                                   && (orderTypeId != null ? x.OrderTypeId == orderTypeId : true)
                                                    && (fromDateTime != null ? fromDateTime.Value.Date <= x.ScheduleDate.Value.Date : true)
                                                    && (toDateTime != null ? toDateTime.Value.Date >= x.ScheduleDate.Value.Date : true)
                                                    && (fromDateTime != null ? fromDateTime.Value.TimeOfDay <= x.ScheduleDate.Value.TimeOfDay : true)
                                                    && (toDateTime != null ? toDateTime.Value.TimeOfDay >= x.ScheduleDate.Value.TimeOfDay : true)
                                                   && (ListOrdersStatusID != null ? ListOrdersStatusID.Any(y => x.OrderStatusID.ToString() == y) : true)
                                                   ).OrderByDescending(p => p.CreateDate)
            .Select(p => new OrdersVendorViewModel()
            {
                DeliveryPrice = p.DeliveryPrice,
                Discount = p.Discount,
                InvoiceImage = !string.IsNullOrEmpty(p.InvoiceImage) ? (MainPathView + p.InvoiceImage) : "/Images/noImage.png",
                Notes = p.Notes,
                OrdersID = p.OrdersID,
                OrderStatusID = p.OrderStatusID,
                OrderVendorGuid = p.OrderVendorGuid,
                OrdersGuid = p.Orders.OrdersGuid,
                OrderVendorID = p.OrderVendorID,
                Price = p.Price,
                Total = p.Total,
                TrackNo = p.TrackNo,
                Vat = p.VatValue,
                EnableDate = p.EnableDate,
                IsDeleted = p.IsDeleted,
                UpdateDate = p.UpdateDate,
                UserUpdate = p.UserUpdate,
                CreateDate = p.CreateDate,
                DeleteDate = p.DeleteDate,
                UserId = p.UserId,
                UserDelete = p.UserDelete,
                UserEnable = p.UserEnable,
                VendorsID = p.VendorsID,
                IsEnable = p.IsEnable,
                CustomersID = p.Orders.CustomersID,
                CustomersName = p.Orders.Customers.FirstName + " " + p.Orders.Customers.SeconedName,
                CustomersMobileNo = p.Orders.Customers.MobileNo,
                VendorsName = lang == "ar" ? p.Vendors.StoreNameAr : (p.Vendors.StoreNameEn),
                OrderStatusName = lang == "ar" ? p.OrderStatus.NameAR : p.OrderStatus.NameEN,
                CustomerBlockName = lang == "ar" ? p.Orders.CustomerLocation.Block.NameAR : p.Orders.CustomerLocation.Block.NameEN,
                VendorBlockName = p.Vendors.Block != null ? (lang == "ar" ? p.Vendors.Block.NameAR : p.Vendors.Block.NameEN) : string.Empty,
                CreateDateString = p.CreateDate.ToString("hh:mm tt"),
                OrderTypeName = lang == "ar" ? (p.OrderTypeId == (int)OrderTypeEnum.Now ? "فورى" : "مجدول") : (p.OrderTypeId == (int)OrderTypeEnum.Now ? "Now" : "Schedule"),
                ScheduleDateString = p.OrderTypeId == (int)OrderTypeEnum.Schedule ? (p.ScheduleDate != null ? p.ScheduleDate.Value.ToString("dd/MM/yyyy") : "---") : "---",
                ScheduleDate = p.ScheduleDate,
                ScheduleTimeString = p.OrderTypeId == (int)OrderTypeEnum.Schedule ? (p.ScheduleTime != null ? p.ScheduleTime.Value.ToString("hh:mm tt") : "---") : "---",
                IntegrationOrderId = !string.IsNullOrEmpty(p.IntegrationOrderId) ? p.IntegrationOrderId : "---",
                VatProduct = Math.Round(p.VatProduct, 2),
                DeliveryVatValue = Math.Round(p.DeliveryVatValue, 2),
                VatValue = Math.Round(p.VatValue, 2),
                Place = p.CaptainTypeId == (int)CaptainTypeEnum.Shipping_Company ? (lang == "ar" ? p.ShippingCompany.NameAr : p.ShippingCompany.NameAr) :
                 p.CaptainTypeId == (int)CaptainTypeEnum.Store ? (lang == "ar" ? "المتجر" : "Store") : (lang == "ar" ? "هوم ميد" : "Home Made"),
            });
            return getData;
        }
        public IQueryable<OrdersVendorViewModel> GetAllOrdersVendorViewModelForDashBoard(string[] ListOrdersStatusID, string lang, string MainPathView)
        {
            if (ListOrdersStatusID != null)
            {
                if (ListOrdersStatusID.Count(p => !string.IsNullOrEmpty(p)) == 0)
                {
                    ListOrdersStatusID = null;
                }
            }
            var getData = Uow.OrderVendor.GetAll(x => !x.IsDeleted
                                                   && (ListOrdersStatusID != null ? ListOrdersStatusID.Any(y => x.OrderStatusID.ToString() == y) : true)
                                                   ).OrderByDescending(p => p.CreateDate)
            .Select(p => new OrdersVendorViewModel()
            {
                DeliveryPrice = p.DeliveryPrice,
                Discount = p.Discount,
                InvoiceImage = !string.IsNullOrEmpty(p.InvoiceImage) ? (MainPathView + p.InvoiceImage) : "/Images/noImage.png",
                Notes = p.Notes,
                OrdersID = p.OrdersID,
                OrderStatusID = p.OrderStatusID,
                OrderVendorGuid = p.OrderVendorGuid,
                OrdersGuid = p.Orders.OrdersGuid,
                OrderVendorID = p.OrderVendorID,
                Price = p.Price,
                Total = p.Total,
                TrackNo = p.TrackNo,
                Vat = p.VatValue,
                EnableDate = p.EnableDate,
                IsDeleted = p.IsDeleted,
                UpdateDate = p.UpdateDate,
                UserUpdate = p.UserUpdate,
                CreateDate = p.CreateDate,
                DeleteDate = p.DeleteDate,
                UserId = p.UserId,
                UserDelete = p.UserDelete,
                UserEnable = p.UserEnable,
                VendorsID = p.VendorsID,
                IsEnable = p.IsEnable,
                CustomersID = p.Orders.CustomersID,
                CustomersName = p.Orders.Customers.FirstName + " " + p.Orders.Customers.SeconedName,
                CustomersMobileNo = p.Orders.Customers.MobileNo,
                VendorsName = lang == "ar" ? p.Vendors.StoreNameAr : (p.Vendors.StoreNameEn),
                OrderStatusName = lang == "ar" ? p.OrderStatus.NameAR : p.OrderStatus.NameEN,
                CustomerBlockName = lang == "ar" ? p.Orders.CustomerLocation.Block.NameAR : p.Orders.CustomerLocation.Block.NameEN,
                VendorBlockName = p.Vendors.Block != null ? (lang == "ar" ? p.Vendors.Block.NameAR : p.Vendors.Block.NameEN) : string.Empty,
                CreateDateString = p.CreateDate.ToString("hh:mm tt"),
                OrderTypeName = lang == "ar" ? (p.OrderTypeId == (int)OrderTypeEnum.Now ? "فورى" : "مجدول") : (p.OrderTypeId == (int)OrderTypeEnum.Now ? "Now" : "Schedule"),
                ScheduleDateString = p.OrderTypeId == (int)OrderTypeEnum.Schedule ? (p.ScheduleDate != null ? p.ScheduleDate.Value.ToString("dd/MM/yyyy") : "---") : "---",
                ScheduleDate = p.ScheduleDate,
                ScheduleTimeString = p.OrderTypeId == (int)OrderTypeEnum.Schedule ? (p.ScheduleTime != null ? p.ScheduleTime.Value.ToString("hh:mm tt") : "---") : "---",
                IntegrationOrderId = !string.IsNullOrEmpty(p.IntegrationOrderId) ? p.IntegrationOrderId : "---",
                VatValue = Math.Round(p.VatValue, 2),
            });
            return getData;
        }
        public IQueryable<OrdersVendorViewModel> GetAllOrdersVendorViewModelForDashBoardVendor(int VendorsID, string[] ListOrdersStatusID, string lang, string MainPathView)
        {
            if (ListOrdersStatusID != null)
            {
                if (ListOrdersStatusID.Count(p => !string.IsNullOrEmpty(p)) == 0)
                {
                    ListOrdersStatusID = null;
                }
            }
            var getData = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.VendorsID == VendorsID
            && x.OrderStatusID == (int)OrderStatusEnum.Create
                                                   && (ListOrdersStatusID != null ? ListOrdersStatusID.Any(y => x.OrderStatusID.ToString() == y) : true)
                                                   ).OrderByDescending(p => p.CreateDate)
            .Select(p => new OrdersVendorViewModel()
            {
                DeliveryPrice = p.DeliveryPrice,
                Discount = p.Discount,
                InvoiceImage = !string.IsNullOrEmpty(p.InvoiceImage) ? (MainPathView + p.InvoiceImage) : "/Images/noImage.png",
                Notes = p.Notes,
                OrdersID = p.OrdersID,
                OrderStatusID = p.OrderStatusID,
                OrderVendorGuid = p.OrderVendorGuid,
                OrdersGuid = p.Orders.OrdersGuid,
                OrderVendorID = p.OrderVendorID,
                Price = p.Price,
                Total = p.Total,
                TrackNo = p.TrackNo,
                Vat = p.VatValue,
                EnableDate = p.EnableDate,
                IsDeleted = p.IsDeleted,
                UpdateDate = p.UpdateDate,
                UserUpdate = p.UserUpdate,
                CreateDate = p.CreateDate,
                DeleteDate = p.DeleteDate,
                UserId = p.UserId,
                UserDelete = p.UserDelete,
                UserEnable = p.UserEnable,
                VendorsID = p.VendorsID,
                IsEnable = p.IsEnable,
                CustomersID = p.Orders.CustomersID,
                CustomersName = p.Orders.Customers.FirstName + " " + p.Orders.Customers.SeconedName,
                CustomersMobileNo = p.Orders.Customers.MobileNo,
                VendorsName = lang == "ar" ? p.Vendors.StoreNameAr : (p.Vendors.StoreNameEn),
                OrderStatusName = lang == "ar" ? p.OrderStatus.NameAR : p.OrderStatus.NameEN,
                CustomerBlockName = lang == "ar" ? p.Orders.CustomerLocation.Block.NameAR : p.Orders.CustomerLocation.Block.NameEN,
                VendorBlockName = p.Vendors.Block != null ? (lang == "ar" ? p.Vendors.Block.NameAR : p.Vendors.Block.NameEN) : string.Empty,
                CreateDateString = p.CreateDate.ToString("hh:mm tt"),
                OrderTypeName = lang == "ar" ? (p.OrderTypeId == (int)OrderTypeEnum.Now ? "فورى" : "مجدول") : (p.OrderTypeId == (int)OrderTypeEnum.Now ? "Now" : "Schedule"),
                ScheduleDateString = p.OrderTypeId == (int)OrderTypeEnum.Schedule ? (p.ScheduleDate != null ? p.ScheduleDate.Value.ToString("dd/MM/yyyy") : "---") : "---",
                ScheduleDate = p.ScheduleDate,
                ScheduleTimeString = p.OrderTypeId == (int)OrderTypeEnum.Schedule ? (p.ScheduleTime != null ? p.ScheduleTime.Value.ToString("hh:mm tt") : "---") : "---",
                IntegrationOrderId = !string.IsNullOrEmpty(p.IntegrationOrderId) ? p.IntegrationOrderId : "---",
                VatProduct = Math.Round(p.VatProduct, 2),
                DeliveryVatValue = Math.Round(p.DeliveryVatValue, 2),
                VatValue = Math.Round(p.VatValue, 2),
            });
            var defaultValue = Uow.Banks.GetAll().First();
            return getData;
        }
        public IQueryable<OrdersVendorViewModel> GetAllOrdersVendorViewModelForTrackingOrder(string[] ListOrdersStatusID, string[] ListDriversID, string[] ListVendorsID, int? CaptainTypeId, string lang, string MainPathView)
        {
            if (ListOrdersStatusID != null)
            {
                if (ListOrdersStatusID.Count(p => !string.IsNullOrEmpty(p)) == 0)
                {
                    ListOrdersStatusID = null;
                }
            }
            if (ListDriversID != null)
            {
                if (ListDriversID.Count(p => !string.IsNullOrEmpty(p)) == 0)
                {
                    ListDriversID = null;
                }
            }
            if (ListVendorsID != null)
            {
                if (ListVendorsID.Count(p => !string.IsNullOrEmpty(p)) == 0)
                {
                    ListVendorsID = null;
                }
            }
            var getData = Uow.OrderVendor.GetAll(x => !x.IsDeleted && !x.Orders.Customers.IsDeleted
            && (x.OrderStatusID == (int)OrderStatusEnum.Assign || x.OrderStatusID == (int)OrderStatusEnum.Being_Delivery || x.OrderStatusID == (int)OrderStatusEnum.OnWay_Store)
                                                   && (ListOrdersStatusID != null ? ListOrdersStatusID.Any(y => x.OrderStatusID.ToString() == y) : true)
                                                   && (ListDriversID != null ? ListDriversID.Any(y => x.DriversID.ToString() == y) : true)
                                                   && (ListVendorsID != null ? ListVendorsID.Any(y => x.VendorsID.ToString() == y) : true)
                                                   && (CaptainTypeId != null ? x.CaptainTypeId == CaptainTypeId : true)
                            ).OrderByDescending(p => p.CreateDate)
            .Select(p => new OrdersVendorViewModel()
            {
                DeliveryPrice = p.DeliveryPrice,
                Discount = p.Discount,
                InvoiceImage = !string.IsNullOrEmpty(p.InvoiceImage) ? (MainPathView + p.InvoiceImage) : "/Images/noImage.png",
                Notes = p.Notes,
                OrdersID = p.OrdersID,
                OrderStatusID = p.OrderStatusID,
                OrderVendorGuid = p.OrderVendorGuid,
                OrdersGuid = p.Orders.OrdersGuid,
                OrderVendorID = p.OrderVendorID,
                Price = p.Price,
                Total = p.Total,
                TrackNo = p.TrackNo,
                Vat = p.VatValue,
                EnableDate = p.EnableDate,
                IsDeleted = p.IsDeleted,
                UpdateDate = p.UpdateDate,
                UserUpdate = p.UserUpdate,
                CreateDate = p.CreateDate,
                DeleteDate = p.DeleteDate,
                UserId = p.UserId,
                UserDelete = p.UserDelete,
                UserEnable = p.UserEnable,
                VendorsID = p.VendorsID,
                IsEnable = p.IsEnable,
                CustomersID = p.Orders.CustomersID,
                CustomersName = p.Orders.Customers.FirstName + " " + p.Orders.Customers.SeconedName,
                CustomersMobileNo = p.Orders.Customers.MobileNo,
                VendorsName = lang == "ar" ? p.Vendors.StoreNameAr : (p.Vendors.StoreNameEn),
                OrderStatusName = lang == "ar" ? p.OrderStatus.NameAR : p.OrderStatus.NameEN,
                CaptainName = p.CaptainTypeId == (int)CaptainTypeEnum.Shipping_Company ? (lang == "ar" ? "كابتن " + p.ShippingCompany.NameAr + "" : "" + p.ShippingCompany.NameEn + " Captain") :
                p.CaptainTypeId == (int)CaptainTypeEnum.Store ? (lang == "ar" ? "كابتن المتجر" : "Store Captain")
                : (lang == "ar" ? p.Drivers.NameAr : p.Drivers.NameEn),
                Place = p.CaptainTypeId == (int)CaptainTypeEnum.Shipping_Company ? (lang == "ar" ? p.ShippingCompany.NameAr : p.ShippingCompany.NameAr) :
                 p.CaptainTypeId == (int)CaptainTypeEnum.Store ? (lang == "ar" ? "المتجر" : "Store") : (lang == "ar" ? "هوم ميد" : "Home Made"),
                UpdateDateString = p.UpdateDate != null ? p.UpdateDate.Value.ToString("hh:mm tt") : "",
                DriversID = p.DriversID,
                IntegrationOrderId = !string.IsNullOrEmpty(p.IntegrationOrderId) ? p.IntegrationOrderId : "---",
                CaptainTypeId = p.CaptainTypeId,
                VatProduct = Math.Round(p.VatProduct, 2),
                DeliveryVatValue = Math.Round(p.DeliveryVatValue, 2),
                VatValue = Math.Round(p.VatValue, 2),
            });
            return getData;
        }
        public IQueryable<OrdersVendorViewModel> GetAllOrdersVendorViewModelByPending(string[] ListRegionID, string[] ListCityID, string[] ListBlockID, string[] ListVendorID, string CustomerNameMobile, string lang, string MainPathView)
        {
            if (ListRegionID != null)
            {
                if (ListRegionID.Count(p => !string.IsNullOrEmpty(p)) == 0)
                {
                    ListRegionID = null;
                }
            }
            if (ListCityID != null)
            {
                if (ListCityID.Count(p => !string.IsNullOrEmpty(p)) == 0)
                {
                    ListCityID = null;
                }
            }
            if (ListBlockID != null)
            {
                if (ListBlockID.Count(p => !string.IsNullOrEmpty(p)) == 0)
                {
                    ListBlockID = null;
                }
            }
            if (ListVendorID != null)
            {
                if (ListVendorID.Count(p => !string.IsNullOrEmpty(p)) == 0)
                {
                    ListVendorID = null;
                }
            }
            var getData = Uow.OrderVendor.GetAll(x => x.IsDeleted && x.OrderStatusID == (int)OrderStatusEnum.Pending && x.IsIncreaseQuantity && x.ApprovalQuantity == (int)ApprovalQuantityEnum.NotAction
                                                   && (ListRegionID != null ? ListRegionID.Any(y => x.Orders.CustomerLocation.Block.City.RegionID.ToString() == y) : true)
                                                   && (ListCityID != null ? ListCityID.Any(y => x.Orders.CustomerLocation.Block.CityID.ToString() == y) : true)
                                                   && (ListBlockID != null ? ListBlockID.Any(y => x.Orders.CustomerLocation.BlockID.ToString() == y) : true)
                                                   && (ListVendorID != null ? ListVendorID.Any(y => x.VendorsID.ToString() == y) : true)
                                                   && (!string.IsNullOrEmpty(CustomerNameMobile) ? (x.Orders.Customers.FirstName.Contains(CustomerNameMobile) || x.Orders.Customers.MobileNo.Contains(CustomerNameMobile)) : true)
                                                   ).OrderByDescending(p => p.CreateDate)
            .Select(p => new OrdersVendorViewModel()
            {
                DeliveryPrice = p.DeliveryPrice,
                Discount = p.Discount,
                InvoiceImage = !string.IsNullOrEmpty(p.InvoiceImage) ? (MainPathView + p.InvoiceImage) : "/Images/noImage.png",
                Notes = p.Notes,
                OrdersID = p.OrdersID,
                OrderStatusID = p.OrderStatusID,
                OrderVendorGuid = p.OrderVendorGuid,
                OrdersGuid = p.Orders.OrdersGuid,
                OrderVendorID = p.OrderVendorID,
                Price = p.Price,
                Total = p.Total,
                TrackNo = p.TrackNo,
                Vat = p.VatValue,
                EnableDate = p.EnableDate,
                IsDeleted = p.IsDeleted,
                UpdateDate = p.UpdateDate,
                UserUpdate = p.UserUpdate,
                CreateDate = p.CreateDate,
                DeleteDate = p.DeleteDate,
                UserId = p.UserId,
                UserDelete = p.UserDelete,
                UserEnable = p.UserEnable,
                VendorsID = p.VendorsID,
                IsEnable = p.IsEnable,
                CustomersID = p.Orders.CustomersID,
                CustomersName = p.Orders.Customers.FirstName + " " + p.Orders.Customers.SeconedName,
                CustomersMobileNo = p.Orders.Customers.MobileNo,
                VendorsName = lang == "ar" ? p.Vendors.StoreNameAr : (p.Vendors.StoreNameEn),
                OrderStatusName = lang == "ar" ? p.OrderStatus.NameAR : p.OrderStatus.NameEN,
                CustomerBlockName = lang == "ar" ? p.Orders.CustomerLocation.Block.NameAR : p.Orders.CustomerLocation.Block.NameEN,
                VendorBlockName = p.Vendors.Block != null ? (lang == "ar" ? p.Vendors.Block.NameAR : p.Vendors.Block.NameEN) : string.Empty,
                CreateDateString = p.CreateDate.ToString("hh:mm tt"),
                OrderTypeName = lang == "ar" ? (p.OrderTypeId == (int)OrderTypeEnum.Now ? "فورى" : "مجدول") : (p.OrderTypeId == (int)OrderTypeEnum.Now ? "Now" : "Schedule"),
                ScheduleDateString = p.OrderTypeId == (int)OrderTypeEnum.Schedule ? (p.ScheduleDate != null ? p.ScheduleDate.Value.ToString("dd/MM/yyyy") : "---") : "---",
                ScheduleDate = p.ScheduleDate,
                ScheduleTimeString = p.OrderTypeId == (int)OrderTypeEnum.Schedule ? (p.ScheduleTime != null ? p.ScheduleTime.Value.ToString("hh:mm tt") : "---") : "---",
                IntegrationOrderId = !string.IsNullOrEmpty(p.IntegrationOrderId) ? p.IntegrationOrderId : "---",
                VatProduct = Math.Round(p.VatProduct, 2),
                DeliveryVatValue = Math.Round(p.DeliveryVatValue, 2),
                VatValue = Math.Round(p.VatValue, 2),
            });
            return getData;
        }
        public IQueryable<OrdersVendorViewModel> GetAllOrdersVendorViewModelByStatus(int? OrdersStatusId, int? CustomersID, int CancelTypeID, int VendorsID, string lang, string MainPathView)
        {
            var getData = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.CancelTypeID == CancelTypeID
                                                   && (OrdersStatusId != null ? x.OrderStatusID == OrdersStatusId : false)
                                                   && (VendorsID != 0 ? x.VendorsID == VendorsID : true)
                                                   && ((CustomersID != 0 && CustomersID != null) ? x.Orders.CustomersID == CustomersID : true)
                                                   ).OrderByDescending(p => p.CreateDate)
            .Select(p => new OrdersVendorViewModel()
            {
                DeliveryPrice = p.DeliveryPrice,
                Discount = p.Discount,
                InvoiceImage = !string.IsNullOrEmpty(p.InvoiceImage) ? (MainPathView + p.InvoiceImage) : "/Images/noImage.png",
                Notes = p.Notes,
                OrdersID = p.OrdersID,
                OrderStatusID = p.OrderStatusID,
                OrderVendorGuid = p.OrderVendorGuid,
                OrdersGuid = p.Orders.OrdersGuid,
                OrderVendorID = p.OrderVendorID,
                Price = p.Price,
                Total = p.Total,
                TrackNo = p.TrackNo,
                Vat = p.VatValue,
                EnableDate = p.EnableDate,
                IsDeleted = p.IsDeleted,
                UpdateDate = p.UpdateDate,
                UserUpdate = p.UserUpdate,
                CreateDate = p.CreateDate,
                DeleteDate = p.DeleteDate,
                UserId = p.UserId,
                UserDelete = p.UserDelete,
                UserEnable = p.UserEnable,
                VendorsID = p.VendorsID,
                IsEnable = p.IsEnable,
                CustomersID = p.Orders.CustomersID,
                CustomersName = p.Orders.Customers.FirstName + " " + p.Orders.Customers.SeconedName,
                CustomersMobileNo = p.Orders.Customers.MobileNo,
                VendorsName = lang == "ar" ? p.Vendors.StoreNameAr : (p.Vendors.StoreNameEn),
                OrderStatusName = lang == "ar" ? p.OrderStatus.NameAR : p.OrderStatus.NameEN,
                CustomerBlockName = lang == "ar" ? p.Orders.CustomerLocation.Block.NameAR : p.Orders.CustomerLocation.Block.NameEN,
                VendorBlockName = p.Vendors.Block != null ? (lang == "ar" ? p.Vendors.Block.NameAR : p.Vendors.Block.NameEN) : string.Empty,
                CreateDateString = p.CreateDate.ToString("hh:mm tt"),
                OrderTypeName = lang == "ar" ? (p.OrderTypeId == (int)OrderTypeEnum.Now ? "فورى" : "مجدول") : (p.OrderTypeId == (int)OrderTypeEnum.Now ? "Now" : "Schedule"),
                ScheduleDateString = p.OrderTypeId == (int)OrderTypeEnum.Schedule ? (p.ScheduleDate != null ? p.ScheduleDate.Value.ToString("dd/MM/yyyy") : "---") : "---",
                ScheduleDate = p.ScheduleDate,
                ScheduleTimeString = p.OrderTypeId == (int)OrderTypeEnum.Schedule ? (p.ScheduleTime != null ? p.ScheduleTime.Value.ToString("hh:mm tt") : "---") : "---",
                IntegrationOrderId = !string.IsNullOrEmpty(p.IntegrationOrderId) ? p.IntegrationOrderId : "---",
                VatProduct = Math.Round(p.VatProduct, 2),
                DeliveryVatValue = Math.Round(p.DeliveryVatValue, 2),
                VatValue = Math.Round(p.VatValue, 2),
                Place = p.CaptainTypeId == (int)CaptainTypeEnum.Shipping_Company ? (lang == "ar" ? p.ShippingCompany.NameAr : p.ShippingCompany.NameAr) :
                 p.CaptainTypeId == (int)CaptainTypeEnum.Store ? (lang == "ar" ? "المتجر" : "Store") : (lang == "ar" ? "هوم ميد" : "Home Made"),
            });
            return getData;
        }
        public List<OrderHistoryViewModel> GetOrderHistory(int OrdersID, string lang)
        {
            return Uow.OrderHistory.GetAll(s => s.OrderVendorID == OrdersID).Select(m => new OrderHistoryViewModel()
            {
                UserType = lang == "ar" ? (m.User.UserType == (int)UserTypeEnum.ShipingCompany ? "شركة شحن" : m.User.UserType == (int)UserTypeEnum.Admin ? "مسئول نظام" : m.User.UserType == (int)UserTypeEnum.Customer ? "عميل" : m.User.UserType == (int)UserTypeEnum.Employee ? "موظف نظام" : m.User.UserType == (int)UserTypeEnum.Vendor ? "مزود خدمة" : "كابتن") :
                (m.User.UserType == (int)UserTypeEnum.ShipingCompany ? "Shiping Company" : m.User.UserType == (int)UserTypeEnum.Admin ? "Admin" : m.User.UserType == (int)UserTypeEnum.Customer ? "Customer" : m.User.UserType == (int)UserTypeEnum.Employee ? "Employee" : m.User.UserType == (int)UserTypeEnum.Vendor ? "Vendor" : "Captain"),
                StatusName = lang == "ar" ? m.OrderStatus.NameAR : m.OrderStatus.NameEN,
                CreateDate = m.CreateDate.ToString("dd/MM/yyyy hh:mm tt"),
                UserName = lang == "ar" ? ((m.User.UserType == (int)UserTypeEnum.Admin || m.User.UserType == (int)UserTypeEnum.Employee) ? m.User.Employees.Where(s => s.UserId == m.UserId).FirstOrDefault().FirstNameAR
                : m.User.UserType == (int)UserTypeEnum.Customer ? m.User.Customers.Where(s => s.UserId == m.UserId).FirstOrDefault().FirstName
                : m.User.UserType == (int)UserTypeEnum.Vendor ? m.User.Vendors.Where(s => s.UserId == m.UserId).FirstOrDefault().FirstNameAr
                : m.User.UserType == (int)UserTypeEnum.ShipingCompany ? m.OrderVendor.ShippingCompany.NameAr
                : m.User.Drivers.Where(s => s.UserId == m.UserId).FirstOrDefault().NameAr) :
                ((m.User.UserType == (int)UserTypeEnum.Admin || m.User.UserType == (int)UserTypeEnum.Employee) ? m.User.Employees.Where(s => s.UserId == m.UserId).FirstOrDefault().FirstNameEN
                : m.User.UserType == (int)UserTypeEnum.Customer ? m.User.Customers.Where(s => s.UserId == m.UserId).FirstOrDefault().FirstName
                : m.User.UserType == (int)UserTypeEnum.Vendor ? m.User.Vendors.Where(s => s.UserId == m.UserId).FirstOrDefault().FirstNameEn
                : m.User.UserType == (int)UserTypeEnum.ShipingCompany ? m.OrderVendor.ShippingCompany.NameEn
                : m.User.Drivers.Where(s => s.UserId == m.UserId).FirstOrDefault().NameEn)
            }).ToList();
        }
        public OrderStatus GetOrderStatusById(int OrderStatusID)
        {
            return Uow.OrderStatus.GetAll(x => !x.IsDeleted && x.OrderStatusID == OrderStatusID).FirstOrDefault();
        }
        public List<ShipCompanyHistoryViewModel> GetAllShipCompanyHistoryByOrder(int OrderVendorID, string lang)
        {
            return Uow.ShipCompanyHistory.GetAll(s => s.OrderVendorID == OrderVendorID, "ShippingCompany").ToList().Select(m => new ShipCompanyHistoryViewModel()
            {
                ShippingCompanyName = lang == "ar" ? m.ShippingCompany.NameAr : m.ShippingCompany.NameEn,
                StatusName = lang == "ar" ? GetOrderStatusById(m.ShippingStatusId).NameAR : GetOrderStatusById(m.ShippingStatusId).NameEN,
                ResponseStatusName = m.ResponseStatusName,
                CancelReason = !string.IsNullOrEmpty(m.CancelReason) ? m.CancelReason : "---",
            }).ToList();
        }
        public List<OrdersVendorViewModel> GetAllOrdersVendorViewModelByOrderID(int OrdersID, string lang)
        {

            var getData = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.OrdersID == OrdersID
                            ).OrderByDescending(p => p.CreateDate)
            .Select(p => new OrdersVendorViewModel()
            {
                DeliveryPrice = p.Orders.DeliveryPrice,
                Discount = p.Orders.Discount,
                Notes = p.Notes,
                OrdersID = p.OrdersID,
                OrderStatusID = p.OrderStatusID,
                OrderVendorGuid = p.OrderVendorGuid,
                OrderVendorID = p.OrderVendorID,
                Price = p.Orders.Price,
                Total = p.Orders.Total,
                TrackNo = p.TrackNo,
                Vat = p.Orders.Vat,
                EnableDate = p.EnableDate,
                IsDeleted = p.IsDeleted,
                UpdateDate = p.UpdateDate,
                UserUpdate = p.UserUpdate,
                CreateDate = p.CreateDate,
                DeleteDate = p.DeleteDate,
                UserId = p.UserId,
                UserDelete = p.UserDelete,
                UserEnable = p.UserEnable,
                VendorsID = p.VendorsID,
                IsEnable = p.IsEnable,
                CustomersID = p.Orders.CustomersID,
                CustomersName = p.Orders.Customers.FirstName + " " + p.Orders.Customers.SeconedName,
                CustomersMobileNo = p.Orders.Customers.MobileNo,
                VendorsName = lang == "ar" ? (p.Vendors.FirstNameAr + " " + p.Vendors.SeconedNameAr) : (p.Vendors.FirstNameEn + " " + p.Vendors.SeconedNameEn),
                ProductsCount = p.Vendors.Product.Count(x => !x.IsDeleted),
                OrderStatusName = lang == "ar" ? p.OrderStatus.NameAR : p.OrderStatus.NameEN,
                VatValue = Math.Round(p.VatValue, 2),
            }).ToList();
            return getData;
        }
        public OrdersVendorViewModel GetOrderVendorByGuid(Guid orderVendorGuid, string lang, string productsImagePathView, string vendorImagesPathView, string customerImagesPathView)
        {

            var getData = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.OrderVendorGuid == orderVendorGuid
                            ).OrderByDescending(p => p.CreateDate)
            .Select(p => new OrdersVendorViewModel()
            {
                DeliveryPrice = Math.Round(p.DeliveryPrice, 2),
                Discount = Math.Round(p.Discount, 2),
                Notes = p.Notes,
                OrdersID = p.OrdersID,
                OrderStatusID = p.OrderStatusID,
                OrderVendorGuid = p.OrderVendorGuid,
                OrderVendorID = p.OrderVendorID,
                Price = Math.Round(p.Price, 2),
                Total = Math.Round(p.Total, 2),
                TrackNo = p.TrackNo,
                Vat = Math.Round(p.VatValue, 2),
                EnableDate = p.EnableDate,
                IsDeleted = p.IsDeleted,
                UpdateDate = p.UpdateDate,
                UserUpdate = p.UserUpdate,
                CreateDate = p.CreateDate,
                DeleteDate = p.DeleteDate,
                UserId = p.UserId,
                UserDelete = p.UserDelete,
                UserEnable = p.UserEnable,
                VendorsID = p.VendorsID,
                IsEnable = p.IsEnable,
                CustomersID = p.Orders.CustomersID,
                CustomersName = p.Orders.Customers.FirstName + " " + p.Orders.Customers.SeconedName,
                CustomersMobileNo = p.Orders.Customers.MobileNo,
                VendorsName = lang == "ar" ? (p.Vendors.StoreNameAr) : (p.Vendors.StoreNameEn),
                ProductsCount = p.Vendors.Product.Count(x => !x.IsDeleted),
                OrderStatusName = lang == "ar" ? p.OrderStatus.NameAR : p.OrderStatus.NameEN,
                CustomersEmail = p.Orders.Customers.Email,
                VendorEmail = p.Vendors.Email,
                VendorMobilNo = p.Vendors.MobileNo,
                CustomerProgilePic = !string.IsNullOrEmpty(p.Orders.Customers.ProfilePic) ? (customerImagesPathView + p.Orders.Customers.ProfilePic) : "/Images/noImage.png",
                VendorLogo = !string.IsNullOrEmpty(p.Vendors.Logo) ? (vendorImagesPathView + p.Vendors.Logo) : "/Images/noImage.png",
                Products = p.OrderItems.Where(x => !x.IsDeleted && x.Quantity > 0).Select(prod => new ProductViewModel()
                {
                    Name = lang == "ar" ? prod.ProdNameAr : prod.ProdNameEn,
                    Quantity = prod.Quantity.ToString(),
                    Price = prod.Price.ToString("N2"),
                    GrossPrice = (prod.Quantity * prod.Price).ToString("N2"),
                    Logo = !string.IsNullOrEmpty(prod.Product.Logo) ? (productsImagePathView + prod.Product.Logo) : "/Images/noImage.png",
                    ProductID = prod.OrderItemsID
                }).ToList(),
                CaptainTypeName = p.CaptainTypeId == (int)CaptainTypeEnum.Shipping_Company ? (lang == "ar" ? p.ShippingCompany.NameAr : p.ShippingCompany.NameAr) :
                p.CaptainTypeId == (int)CaptainTypeEnum.Store ? (lang == "ar" ? "المتجر" : "Store")
                : (lang == "ar" ? "شغل بيت" : "Home Made"),
                CaptainName = p.CaptainTypeId == (int)CaptainTypeEnum.Shipping_Company ?
                (lang == "ar" ? "كابتن " + p.ShippingCompany.NameAr + "" : "" + p.ShippingCompany.NameEn + " Captain") :
                p.Drivers != null ? (lang == "ar" ? p.Drivers.NameAr : p.Drivers.NameEn) : (lang == "ar" ? "لم يتم اسناد الطلب الي الكابتن." : "The order was not assigned to the captain."),
                CaptainTypeId = p.CaptainTypeId,
                CaptainPhone = p.Drivers != null ? p.Drivers.PhoneNumber : string.Empty,
                IsOnlineString = p.Drivers != null ? (p.Drivers.IsOnline ? (lang == "ar" ? "متصل" : "Online") : (lang == "ar" ? "غير متصل" : "Offline")) : (lang == "ar" ? "غير متصل" : "Offline"),
                CustomerBlockName = lang == "ar" ? p.Orders.CustomerLocation.Block.NameAR : p.Orders.CustomerLocation.Block.NameEN,
                VendorBlockName = p.Vendors.Block != null ? (lang == "ar" ? p.Vendors.Block.NameAR : p.Vendors.Block.NameEN) : string.Empty,
                OrderTypeId = p.OrderTypeId,
                OrderTypeName = lang == "ar" ? (p.OrderTypeId == (int)OrderTypeEnum.Now ? "طلب فورى" : "طلب مجدول") : (p.OrderTypeId == (int)OrderTypeEnum.Now ? "Now Order" : "Schedule Order"),
                ScheduleDateString = p.OrderTypeId == (int)OrderTypeEnum.Schedule ? (p.ScheduleDate != null ? p.ScheduleDate.Value.ToString("dd/MM/yyyy") : "---") : "---",
                ScheduleTimeString = p.OrderTypeId == (int)OrderTypeEnum.Schedule ? (p.ScheduleTime != null ? p.ScheduleTime.Value.ToString("hh:mm tt") : "---") : "---",
                IntegrationOrderId = !string.IsNullOrEmpty(p.IntegrationOrderId) ? p.IntegrationOrderId : "---",
                VatProduct = Math.Round(p.VatProduct, 2),
                DeliveryVatValue = Math.Round(p.DeliveryVatValue, 2),
                VatValue = Math.Round(((p.Total) - ((p.Total) / (1 + (p.VatPercent / 100)))), 2),
            }).FirstOrDefault();
            return getData;
        }
        public OrdersVendorViewModel GetOrderVendorByGuidAndPending(Guid orderVendorGuid, string lang, string productsImagePathView, string vendorImagesPathView, string customerImagesPathView)
        {

            var getData = Uow.OrderVendor.GetAll(x => x.IsDeleted && x.OrderVendorGuid == orderVendorGuid && x.IsIncreaseQuantity && x.ApprovalQuantity == (int)ApprovalQuantityEnum.NotAction
                            ).OrderByDescending(p => p.CreateDate)
            .Select(p => new OrdersVendorViewModel()
            {
                DeliveryPrice = Math.Round(p.DeliveryPrice, 2),
                Discount = Math.Round(p.Discount, 2),
                Notes = p.Notes,
                OrdersID = p.OrdersID,
                OrderStatusID = p.OrderStatusID,
                OrderVendorGuid = p.OrderVendorGuid,
                OrderVendorID = p.OrderVendorID,
                Price = Math.Round(p.Price, 2),
                Total = Math.Round(p.Total, 2),
                TrackNo = p.TrackNo,
                Vat = Math.Round(p.VatValue, 2),
                EnableDate = p.EnableDate,
                IsDeleted = p.IsDeleted,
                UpdateDate = p.UpdateDate,
                UserUpdate = p.UserUpdate,
                CreateDate = p.CreateDate,
                DeleteDate = p.DeleteDate,
                UserId = p.UserId,
                UserDelete = p.UserDelete,
                UserEnable = p.UserEnable,
                VendorsID = p.VendorsID,
                IsEnable = p.IsEnable,
                CustomersID = p.Orders.CustomersID,
                CustomersName = p.Orders.Customers.FirstName + " " + p.Orders.Customers.SeconedName,
                CustomersMobileNo = p.Orders.Customers.MobileNo,
                VendorsName = lang == "ar" ? (p.Vendors.StoreNameAr) : (p.Vendors.StoreNameEn),
                ProductsCount = p.Vendors.Product.Count(x => !x.IsDeleted),
                OrderStatusName = lang == "ar" ? p.OrderStatus.NameAR : p.OrderStatus.NameEN,
                CustomersEmail = p.Orders.Customers.Email,
                VendorEmail = p.Vendors.Email,
                VendorMobilNo = p.Vendors.MobileNo,
                CustomerProgilePic = !string.IsNullOrEmpty(p.Orders.Customers.ProfilePic) ? (customerImagesPathView + p.Orders.Customers.ProfilePic) : "/Images/noImage.png",
                VendorLogo = !string.IsNullOrEmpty(p.Vendors.Logo) ? (vendorImagesPathView + p.Vendors.Logo) : "/Images/noImage.png",
                Products = p.OrderItems.Where(x => !x.IsDeleted && x.IsIncreaseItem).Select(prod => new ProductViewModel()
                {
                    Name = lang == "ar" ? prod.ProdNameAr : prod.ProdNameEn,
                    Quantity = prod.Quantity.ToString(),
                    Price = prod.Price.ToString("N2"),
                    GrossPrice = (prod.Quantity * prod.Price).ToString("N2"),
                    Logo = !string.IsNullOrEmpty(prod.Product.Logo) ? (productsImagePathView + prod.Product.Logo) : "/Images/noImage.png",
                    ProductID = prod.OrderItemsID
                }).ToList(),
                CaptainTypeName = p.CaptainTypeId == (int)CaptainTypeEnum.Shipping_Company ? (lang == "ar" ? p.ShippingCompany.NameAr : p.ShippingCompany.NameAr) :
                p.CaptainTypeId == (int)CaptainTypeEnum.Store ? (lang == "ar" ? "المتجر" : "Store")
                : (lang == "ar" ? "شغل بيت" : "Home Made"),
                CaptainName = p.CaptainTypeId == (int)CaptainTypeEnum.Shipping_Company ?
                (lang == "ar" ? "كابتن " + p.ShippingCompany.NameAr + "" : "" + p.ShippingCompany.NameEn + " Captain") :
                p.Drivers != null ? (lang == "ar" ? p.Drivers.NameAr : p.Drivers.NameEn) : (lang == "ar" ? "لم يتم اسناد الطلب الي الكابتن." : "The order was not assigned to the captain."),
                CaptainTypeId = p.CaptainTypeId,
                CaptainPhone = p.Drivers != null ? p.Drivers.PhoneNumber : string.Empty,
                IsOnlineString = p.Drivers != null ? (p.Drivers.IsOnline ? (lang == "ar" ? "متصل" : "Online") : (lang == "ar" ? "غير متصل" : "Offline")) : (lang == "ar" ? "غير متصل" : "Offline"),
                CustomerBlockName = lang == "ar" ? p.Orders.CustomerLocation.Block.NameAR : p.Orders.CustomerLocation.Block.NameEN,
                VendorBlockName = p.Vendors.Block != null ? (lang == "ar" ? p.Vendors.Block.NameAR : p.Vendors.Block.NameEN) : string.Empty,
                OrderTypeId = p.OrderTypeId,
                OrderTypeName = lang == "ar" ? (p.OrderTypeId == (int)OrderTypeEnum.Now ? "طلب فورى" : "طلب مجدول") : (p.OrderTypeId == (int)OrderTypeEnum.Now ? "Now Order" : "Schedule Order"),
                ScheduleDateString = p.OrderTypeId == (int)OrderTypeEnum.Schedule ? (p.ScheduleDate != null ? p.ScheduleDate.Value.ToString("dd/MM/yyyy") : "---") : "---",
                ScheduleTimeString = p.OrderTypeId == (int)OrderTypeEnum.Schedule ? (p.ScheduleTime != null ? p.ScheduleTime.Value.ToString("hh:mm tt") : "---") : "---",
                IntegrationOrderId = !string.IsNullOrEmpty(p.IntegrationOrderId) ? p.IntegrationOrderId : "---",
                VatProduct = Math.Round(p.VatProduct, 2),
                DeliveryVatValue = Math.Round(p.DeliveryVatValue, 2),
                VatValue = Math.Round(p.VatValue, 2),
            }).FirstOrDefault();
            return getData;
        }
        public OrdersVendorViewModel GetOrderVendorByGuidAndPending(int OrderVendorID, string lang, string productsImagePathView, string vendorImagesPathView, string customerImagesPathView)
        {

            var getData = Uow.OrderVendor.GetAll(x => x.IsDeleted && x.OrderVendorID == OrderVendorID && x.IsIncreaseQuantity && x.ApprovalQuantity == (int)ApprovalQuantityEnum.NotAction
                            ).OrderByDescending(p => p.CreateDate)
            .Select(p => new OrdersVendorViewModel()
            {
                DeliveryPrice = Math.Round(p.DeliveryPrice, 2),
                Discount = Math.Round(p.Discount, 2),
                Notes = p.Notes,
                OrdersID = p.OrdersID,
                OrderStatusID = p.OrderStatusID,
                OrderVendorGuid = p.OrderVendorGuid,
                OrderVendorID = p.OrderVendorID,
                Price = Math.Round(p.Price, 2),
                Total = Math.Round(p.Total, 2),
                TrackNo = p.TrackNo,
                Vat = Math.Round(p.VatValue, 2),
                EnableDate = p.EnableDate,
                IsDeleted = p.IsDeleted,
                UpdateDate = p.UpdateDate,
                UserUpdate = p.UserUpdate,
                CreateDate = p.CreateDate,
                DeleteDate = p.DeleteDate,
                UserId = p.UserId,
                UserDelete = p.UserDelete,
                UserEnable = p.UserEnable,
                VendorsID = p.VendorsID,
                IsEnable = p.IsEnable,
                CustomersID = p.Orders.CustomersID,
                CustomersName = p.Orders.Customers.FirstName + " " + p.Orders.Customers.SeconedName,
                CustomersMobileNo = p.Orders.Customers.MobileNo,
                VendorsName = lang == "ar" ? (p.Vendors.StoreNameAr) : (p.Vendors.StoreNameEn),
                ProductsCount = p.Vendors.Product.Count(x => !x.IsDeleted),
                OrderStatusName = lang == "ar" ? p.OrderStatus.NameAR : p.OrderStatus.NameEN,
                CustomersEmail = p.Orders.Customers.Email,
                VendorEmail = p.Vendors.Email,
                VendorMobilNo = p.Vendors.MobileNo,
                CustomerProgilePic = !string.IsNullOrEmpty(p.Orders.Customers.ProfilePic) ? (customerImagesPathView + p.Orders.Customers.ProfilePic) : "/Images/noImage.png",
                VendorLogo = !string.IsNullOrEmpty(p.Vendors.Logo) ? (vendorImagesPathView + p.Vendors.Logo) : "/Images/noImage.png",
                Products = p.OrderItems.Where(x => !x.IsDeleted && x.IsIncreaseItem).Select(prod => new ProductViewModel()
                {
                    Name = lang == "ar" ? prod.ProdNameAr : prod.ProdNameEn,
                    Quantity = prod.Quantity.ToString(),
                    Price = prod.Price.ToString("N2"),
                    GrossPrice = (prod.Quantity * prod.Price).ToString("N2"),
                    Logo = !string.IsNullOrEmpty(prod.Product.Logo) ? (productsImagePathView + prod.Product.Logo) : "/Images/noImage.png",
                    ProductID = prod.OrderItemsID
                }).ToList(),
                CaptainTypeName = p.CaptainTypeId == (int)CaptainTypeEnum.Shipping_Company ? (lang == "ar" ? p.ShippingCompany.NameAr : p.ShippingCompany.NameAr) :
                p.CaptainTypeId == (int)CaptainTypeEnum.Store ? (lang == "ar" ? "المتجر" : "Store")
                : (lang == "ar" ? "شغل بيت" : "Home Made"),
                CaptainName = p.CaptainTypeId == (int)CaptainTypeEnum.Shipping_Company ?
                (lang == "ar" ? "كابتن " + p.ShippingCompany.NameAr + "" : "" + p.ShippingCompany.NameEn + " Captain") :
                p.Drivers != null ? (lang == "ar" ? p.Drivers.NameAr : p.Drivers.NameEn) : (lang == "ar" ? "لم يتم اسناد الطلب الي الكابتن." : "The order was not assigned to the captain."),
                CaptainTypeId = p.CaptainTypeId,
                CaptainPhone = p.Drivers != null ? p.Drivers.PhoneNumber : string.Empty,
                IsOnlineString = p.Drivers != null ? (p.Drivers.IsOnline ? (lang == "ar" ? "متصل" : "Online") : (lang == "ar" ? "غير متصل" : "Offline")) : (lang == "ar" ? "غير متصل" : "Offline"),
                CustomerBlockName = lang == "ar" ? p.Orders.CustomerLocation.Block.NameAR : p.Orders.CustomerLocation.Block.NameEN,
                VendorBlockName = p.Vendors.Block != null ? (lang == "ar" ? p.Vendors.Block.NameAR : p.Vendors.Block.NameEN) : string.Empty,
                OrderTypeId = p.OrderTypeId,
                OrderTypeName = lang == "ar" ? (p.OrderTypeId == (int)OrderTypeEnum.Now ? "طلب فورى" : "طلب مجدول") : (p.OrderTypeId == (int)OrderTypeEnum.Now ? "Now Order" : "Schedule Order"),
                ScheduleDateString = p.OrderTypeId == (int)OrderTypeEnum.Schedule ? (p.ScheduleDate != null ? p.ScheduleDate.Value.ToString("dd/MM/yyyy") : "---") : "---",
                ScheduleTimeString = p.OrderTypeId == (int)OrderTypeEnum.Schedule ? (p.ScheduleTime != null ? p.ScheduleTime.Value.ToString("hh:mm tt") : "---") : "---",
                IntegrationOrderId = !string.IsNullOrEmpty(p.IntegrationOrderId) ? p.IntegrationOrderId : "---",
                VatProduct = Math.Round(p.VatProduct, 2),
                DeliveryVatValue = Math.Round(p.DeliveryVatValue, 2),
                VatValue = Math.Round(p.VatValue, 2),
            }).FirstOrDefault();
            return getData;
        }
        public OrdersViewModel GetOrdersViewModelByGuid(Guid OrdersGuid, string lang, string MainPathView)
        {
            var getData = Uow.Orders.GetAll(x => !x.IsDeleted && x.OrdersGuid == OrdersGuid)
                .Select(p => new OrdersViewModel()
                {
                    DeliveryPrice = p.DeliveryPrice,
                    Discount = p.Discount,
                    Notes = p.Notes,
                    OrdersID = p.OrdersID,
                    OrderStatusID = p.OrderStatusID,
                    Price = p.Price,
                    Total = p.Total,
                    Vat = p.Vat,
                    EnableDate = p.EnableDate,
                    IsDeleted = p.IsDeleted,
                    UpdateDate = p.UpdateDate,
                    UserUpdate = p.UserUpdate,
                    CreateDate = p.CreateDate,
                    DeleteDate = p.DeleteDate,
                    UserId = p.UserId,
                    UserDelete = p.UserDelete,
                    UserEnable = p.UserEnable,
                    IsEnable = p.IsEnable,
                    CustomersID = p.CustomersID,
                    CustomersName = p.Customers.FirstName + " " + p.Customers.SeconedName,
                    CustomersMobileNo = p.Customers.MobileNo,
                    CustomersProfilePic = !string.IsNullOrEmpty(p.Customers.ProfilePic) ? (MainPathView + p.Customers.ProfilePic) : "/Images/noImage.png",
                    CustomersEmail = p.Customers.Email,
                    CustomerLocationAddress = p.CustomerLocation.Address,
                    CustomerLocationLat = p.CustomerLocation.Lat,
                    CustomerLocationLng = p.CustomerLocation.Lng,
                    CustomerLocationBlockName = lang == "ar" ? p.CustomerLocation.Block.NameAR : p.CustomerLocation.Block.NameEN,
                    CustomerLocationCityName = lang == "ar" ? p.CustomerLocation.Block.City.NameAR : p.CustomerLocation.Block.City.NameEN,
                    OrdersGuid = p.OrdersGuid,
                    OrderStatusName = lang == "ar" ? p.OrderStatus.NameAR : p.OrderStatus.NameEN,
                }).FirstOrDefault();
            return getData;
        }
        public OrderStatusViewModel OrderStatusByGuid(Guid OrderStatusGuid)
        {
            var getData = Uow.OrderStatus.GetAll(x => !x.IsDeleted && x.OrderStatusGuid == OrderStatusGuid)
                .Select(p => new OrderStatusViewModel()
                {
                    OrderStatusID = p.OrderStatusID,
                    EnableDate = p.EnableDate,
                    IsDeleted = p.IsDeleted,
                    UpdateDate = p.UpdateDate,
                    UserUpdate = p.UserUpdate,
                    CreateDate = p.CreateDate,
                    DeleteDate = p.DeleteDate,
                    UserId = p.UserId,
                    UserDelete = p.UserDelete,
                    UserEnable = p.UserEnable,
                    IsEnable = p.IsEnable,
                    NameEN = p.NameEN,
                    NameAR = p.NameAR,
                    DescEN = p.DescEN,
                    OrderStatusGuid = p.OrderStatusGuid,
                    DescAr = p.DescAr
                }).FirstOrDefault();
            return getData;
        }
        public List<OrderStatusViewModel> GetAllOrdersStatus(string lang)
        {
            var getData = Uow.OrderStatus.GetAll(x => !x.IsDeleted)
                .Select(p => new OrderStatusViewModel()
                {
                    OrderStatusID = p.OrderStatusID,
                    EnableDate = p.EnableDate,
                    IsDeleted = p.IsDeleted,
                    UpdateDate = p.UpdateDate,
                    UserUpdate = p.UserUpdate,
                    CreateDate = p.CreateDate,
                    DeleteDate = p.DeleteDate,
                    UserId = p.UserId,
                    UserDelete = p.UserDelete,
                    UserEnable = p.UserEnable,
                    IsEnable = p.IsEnable,
                    NameEN = p.NameEN,
                    NameAR = p.NameAR,
                    DescEN = p.DescEN,
                    OrderStatusGuid = p.OrderStatusGuid,
                    DescAr = p.DescAr,
                    OrderStatusName = lang == "ar" ? p.NameAR : p.NameEN
                }).ToList();
            return getData;
        }
        public Guid OrderStatusByGuid(int orderStatusEnumId)
        {
            var OrderStatusGuid = Uow.OrderStatus.GetAll(x => !x.IsDeleted && x.OrderStatusID == orderStatusEnumId).FirstOrDefault().OrderStatusGuid;
            return OrderStatusGuid;
        }
        public OrdersViewModelApi GetOrdersViewModelApi(int orderId, string lang, string pathProduct, string pathvendor)
        {
            var VatSettingPercent = _blConfiguration.GetDeliveryPriceVatPercent();
            var getData = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.OrderVendorID == orderId, "Vendors")
                .Select(m => new OrdersViewModelApi()
                {
                    deliveryPrice = lang == "ar" ? m.DeliveryPrice + " " : m.DeliveryPrice + " ",
                    discount = lang == "ar" ? m.Discount + " " : m.Discount + " ",
                    orderStatusID = m.OrderStatusID == (int)OrderStatusEnum.Create ? 1 :
                    (m.OrderStatusID == (int)OrderStatusEnum.Accept || m.OrderStatusID == (int)OrderStatusEnum.Being_Processed) ? 2 :
                    (m.OrderStatusID == (int)OrderStatusEnum.Shipping || m.OrderStatusID == (int)OrderStatusEnum.Being_Delivery || m.OrderStatusID == (int)OrderStatusEnum.Assign || m.OrderStatusID == (int)OrderStatusEnum.OnWay_Store) ? 3 : m.OrderStatusID == (int)OrderStatusEnum.Delivered ? 4 : 5,
                    price = lang == "ar" ? m.Price + " " : m.Price + " ",
                    total = lang == "ar" ? m.Total + " ريـال" : m.Total + " SAR",
                    totalBeforDiscount = lang == "ar" ? Math.Round(m.Price + m.DeliveryPrice, 2) + " " : Math.Round(m.Price + m.DeliveryPrice, 2) + " ",
                    totalWithDelivery = lang == "ar" ? (m.Total + m.DeliveryPrice) + " ريـال" : (m.Total + m.DeliveryPrice) + " SAR",
                    vat = lang == "ar" ? Math.Round(((m.Total) - ((m.Total) / (1 + (m.VatPercent / 100)))), 2).ToString("0.00") : Math.Round(((m.Total) - ((m.Total) / (1 + (m.VatPercent / 100)))), 2).ToString("0.00"),
                    vatPercent = VatSettingPercent,
                    paymentStatus = lang == "ar" ? (m.Orders.PaymentTypeId == (int)PaymentTypeEnum.COD ? "لم يتم الدفع" : "تم الدفع") : m.Orders.PaymentTypeId == (int)PaymentTypeEnum.COD ? "Un Paid" : "Paid",
                    vendorName = lang == "ar" ? m.Vendors.StoreNameAr : m.Vendors.StoreNameEn,
                    paymentType = lang == "ar" ? (m.Orders.PaymentTypeId == (int)PaymentTypeEnum.COD ? "نقد عند الإستلام" : m.Orders.PaymentTypeId == (int)PaymentTypeEnum.Payment_Card ? "بطاقة دفع" :
                m.Orders.PaymentTypeId == (int)PaymentTypeEnum.Bank_Transfer ? "تحويل بنكي" : "المحفظة") :
                (m.Orders.PaymentTypeId == (int)PaymentTypeEnum.COD ? "Cash on Receive" : m.Orders.PaymentTypeId == (int)PaymentTypeEnum.Payment_Card ? "Payment Card" :
                m.Orders.PaymentTypeId == (int)PaymentTypeEnum.Bank_Transfer ? "Bank Transfer" : "Wallet"),
                    vendorPic = !string.IsNullOrEmpty(m.Vendors.ProfilePic) ? (pathvendor + m.Vendors.ProfilePic) : "/Images/noImage.png",
                    address = m.Orders.CustomerLocation.Address,
                    trackNo = m.TrackNo,
                    date = m.CreateDate.ToString("hh:mm tt") + " " + m.CreateDate.ToString("dd/MM/yyyy"),
                    orderItems = m.OrderItems.Where(x => !x.IsDeleted).Select(z => new OrderItemsApi()
                    {
                        logo = !string.IsNullOrWhiteSpace(z.Product.Logo) ? pathProduct + z.Product.Logo : "",
                        name = lang == "ar" ? z.ProdNameAr : z.ProdNameEn,
                        quantity = z.Quantity,
                        price = lang == "ar" ? Math.Round(z.Quantity * z.Price, 2).ToString("0.00") : Math.Round((z.Quantity * z.Price), 2).ToString("0.00"),
                    }).ToList(),
                    createDate = m.OrderHistory.Where(s => s.OrderStatusID == (int)OrderStatusEnum.Create).FirstOrDefault() != null ?
                    m.OrderHistory.Where(s => s.OrderStatusID == (int)OrderStatusEnum.Create).FirstOrDefault().CreateDate.ToString("dd/MM/yyyy") : "",
                    createTime = m.OrderHistory.Where(s => s.OrderStatusID == (int)OrderStatusEnum.Create).FirstOrDefault() != null ?
                    m.OrderHistory.Where(s => s.OrderStatusID == (int)OrderStatusEnum.Create).FirstOrDefault().CreateDate.ToString("hh:mm tt") : "",
                    processDate = m.OrderHistory.Where(s => s.OrderStatusID == (int)OrderStatusEnum.Accept).FirstOrDefault() != null ?
                    m.OrderHistory.Where(s => s.OrderStatusID == (int)OrderStatusEnum.Accept).FirstOrDefault().CreateDate.ToString("dd/MM/yyyy") : "",
                    processTime = m.OrderHistory.Where(s => s.OrderStatusID == (int)OrderStatusEnum.Accept).FirstOrDefault() != null ?
                    m.OrderHistory.Where(s => s.OrderStatusID == (int)OrderStatusEnum.Accept).FirstOrDefault().CreateDate.ToString("hh:mm tt") : "",
                    deliveryDate = m.OrderHistory.Where(s => s.OrderStatusID == (int)OrderStatusEnum.Shipping).FirstOrDefault() != null ?
                    m.OrderHistory.Where(s => s.OrderStatusID == (int)OrderStatusEnum.Shipping).FirstOrDefault().CreateDate.ToString("dd/MM/yyyy") : "",
                    deliveryTime = m.OrderHistory.Where(s => s.OrderStatusID == (int)OrderStatusEnum.Shipping).FirstOrDefault() != null ?
                    m.OrderHistory.Where(s => s.OrderStatusID == (int)OrderStatusEnum.Shipping).FirstOrDefault().CreateDate.ToString("hh:mm tt") : "",
                    cancelDate = m.OrderHistory.Where(s => s.OrderStatusID == (int)OrderStatusEnum.Cancel).FirstOrDefault() != null ?
                    m.OrderHistory.Where(s => s.OrderStatusID == (int)OrderStatusEnum.Cancel).FirstOrDefault().CreateDate.ToString("dd/MM/yyyy") : "",
                    cancelTime = m.OrderHistory.Where(s => s.OrderStatusID == (int)OrderStatusEnum.Cancel).FirstOrDefault() != null ?
                    m.OrderHistory.Where(s => s.OrderStatusID == (int)OrderStatusEnum.Cancel).FirstOrDefault().CreateDate.ToString("hh:mm tt") : "",
                    receivedDate = m.OrderHistory.Where(s => s.OrderStatusID == (int)OrderStatusEnum.Delivered).FirstOrDefault() != null ?
                    m.OrderHistory.Where(s => s.OrderStatusID == (int)OrderStatusEnum.Delivered).FirstOrDefault().CreateDate.ToString("dd/MM/yyyy") : "",
                    receivedTime = m.OrderHistory.Where(s => s.OrderStatusID == (int)OrderStatusEnum.Delivered).FirstOrDefault() != null ?
                    m.OrderHistory.Where(s => s.OrderStatusID == (int)OrderStatusEnum.Delivered).FirstOrDefault().CreateDate.ToString("hh:mm tt") : "",
                    isRated = m.OrderRate.Any(s => !s.IsDeleted),

                    isShowContact = m.Vendors.IsShowContact,
                    mobileNo = m.Vendors.MobileNo,
                    deliveryVatPercent = lang == "ar" ? m.VatPercent + " " : m.VatPercent + " ",
                    deliveryVatValue = lang == "ar" ? m.DeliveryVatValue + " " : m.DeliveryVatValue + " ",
                    taxNumber = string.Empty,
                    orderType = lang == "ar" ? (m.OrderTypeId == (int)OrderTypeEnum.Now ? "طلب فورى" : "طلب مجدول - " + (m.ScheduleDate != null ? m.ScheduleDate.Value.ToString("dd/MM/yyyy") + " " + m.ScheduleTime.Value.ToString("hh:mm tt") : "")) : (m.OrderTypeId == (int)OrderTypeEnum.Now ? "Now Order" : "Schedule Order - " + (m.ScheduleDate != null ? m.ScheduleDate.Value.ToString("dd/MM/yyyy") + " " + m.ScheduleTime.Value.ToString("hh:mm tt") : "")),
                    orderVendorID = m.OrderVendorID,

                    companyName = m.CaptainTypeId == (int)CaptainTypeEnum.Home_Made ? (lang == "ar" ? "شغل بيت" : "Home Made") : (m.ShippingCompany != null ? (lang == "ar" ? m.ShippingCompany.NameAr : m.ShippingCompany.NameEn) : "--"),
                    captainName = m.CaptainTypeId == (int)CaptainTypeEnum.Home_Made ? (m.Drivers != null ? (lang == "ar" ? m.Drivers.NameAr : m.Drivers.NameEn) : (lang == "ar" ? "بانتظار اسناد الطلب" : "Watting Assigned Order")) : "--",
                    captainMobile = m.CaptainTypeId == (int)CaptainTypeEnum.Home_Made ? (m.Drivers != null ? m.Drivers.PhoneNumber : "--") : "--",
                    invoiceCompanyName = lang == "ar" ? _blConfiguration.GetFirstConfiguration().CompanNameAr : _blConfiguration.GetFirstConfiguration().CompanNameEn
                }).FirstOrDefault();
            if (getData != null)
            {
                getData.taxNumber = Uow.Configuration.GetAll(s => !s.IsDeleted).FirstOrDefault() != null ? Uow.Configuration.GetAll(s => !s.IsDeleted).FirstOrDefault().TaxNumber : string.Empty;
                getData.vatAddress = Uow.Configuration.GetAll(s => !s.IsDeleted).FirstOrDefault() != null ? Uow.Configuration.GetAll(s => !s.IsDeleted).FirstOrDefault().Address : string.Empty;
            }
            return getData;
        }
        public IncreaseOrdersViewModelApi GetIncreaseOrdersViewModelApi(int OrderVendorID, string lang, string pathProduct)
        {
            var VatSettingPercent = _blConfiguration.GetDeliveryPriceVatPercent();
            var getData = Uow.OrderVendor.GetAll(x => x.IsDeleted && x.OrderVendorID == OrderVendorID && x.IsIncreaseQuantity && x.ApprovalQuantity == (int)ApprovalQuantityEnum.NotAction
            , "Vendors")
                .Select(m => new IncreaseOrdersViewModelApi()
                {
                    orderItems = m.OrderItems.Where(x => !x.IsDeleted && x.IsIncreaseItem).Select(z => new OrderItemsIncreaseApi()
                    {
                        logo = !string.IsNullOrWhiteSpace(z.Product.Logo) ? pathProduct + z.Product.Logo : "",
                        name = lang == "ar" ? z.ProdNameAr : z.ProdNameEn,
                        quantity = z.Quantity,
                        price = lang == "ar" ? z.Price + " " : z.Price + " ",
                        orderItemId = z.OrderItemsID,
                    }).ToList(),
                    orderVendorID = m.OrderVendorID,
                    approvalQuantity = m.ApprovalQuantity
                }).FirstOrDefault();
            return getData;
        }
        public OrdersVendorViewModelApi VendorOrderDetails(int orderId, string lang, string pathProduct, string custoemrPath)
        {
            var VatSettingPercent = _blConfiguration.GetDeliveryPriceVatPercent();
            var getData = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.OrderVendorID == orderId)
                .Select(m => new OrdersVendorViewModelApi()
                {
                    image = !string.IsNullOrWhiteSpace(m.Orders.Customers.ProfilePic) ? custoemrPath + m.Orders.Customers.ProfilePic : "",
                    name = m.Orders.Customers.FirstName + " " + m.Orders.Customers.SeconedName,
                    city = lang == "ar" ? m.Orders.CustomerLocation.Block.City.NameAR : m.Orders.CustomerLocation.Block.City.NameEN,
                    mobile = m.Orders.Customers.MobileNo,
                    isCompany = true,
                    statusName = lang == "ar" ? m.OrderStatus.NameAR : m.OrderStatus.NameEN,
                    orderStatusID = m.OrderStatusID,
                    //comapnyname = lang == "ar" ? "شركة الشحن" : "Shipping Company",
                    address = m.Orders.CustomerLocation.Address,
                    uniqueSign = m.Orders.CustomerLocation.UniqueSign,
                    orderId = m.OrderVendorID,
                    paymentType = lang == "ar" ? (m.Orders.PaymentTypeId == (int)PaymentTypeEnum.COD ? "نقد عند الإستلام" : m.Orders.PaymentTypeId == (int)PaymentTypeEnum.Payment_Card ? "بطاقة دفع" :
                m.Orders.PaymentTypeId == (int)PaymentTypeEnum.Bank_Transfer ? "تحويل بنكي" : "المحفظة") :
                (m.Orders.PaymentTypeId == (int)PaymentTypeEnum.COD ? "Cash on Receive" : m.Orders.PaymentTypeId == (int)PaymentTypeEnum.Payment_Card ? "Payment Card" :
                m.Orders.PaymentTypeId == (int)PaymentTypeEnum.Bank_Transfer ? "Bank Transfer" : "Wallet"),
                    date = m.CreateDate.ToString("dd/MM/yyyy"),
                    time = m.CreateDate.ToString("hh:mm tt"),
                    trackNo = m.TrackNo,
                    notes = !string.IsNullOrWhiteSpace(m.Notes) ? m.Notes : (lang == "ar" ? "لا يوجد ملاحظات على الطلب" : "no notes for this order"),
                    price = lang == "ar" ? m.Total + " ريـال" : m.Total + " SAR",
                    paymentStatus = lang == "ar" ? (m.Orders.PaymentTypeId == (int)PaymentTypeEnum.COD ? "لم يتم الدفع" : "تم الدفع") : m.Orders.PaymentTypeId == (int)PaymentTypeEnum.COD ? "Un Paid" : "Paid",
                    orderItems = m.OrderItems.Where(x => !x.IsDeleted).Select(z => new OrderItemsApi()
                    {
                        logo = !string.IsNullOrWhiteSpace(z.Product.Logo) ? pathProduct + z.Product.Logo : "",
                        name = lang == "ar" ? z.ProdNameAr : z.ProdNameEn,
                        quantity = z.Quantity,
                        price = lang == "ar" ? z.Price + " ريـال" : z.Price + " SAR",
                    }).ToList(),
                    invoiceNo = m.TrackNo,
                    invoicePrice = lang == "ar" ? m.Price + " ريـال" : m.Price + " SAR",
                    invoiceVat = lang == "ar" ? Math.Round(((m.Total) - ((m.Total) / (1 + (m.VatPercent / 100)))), 2).ToString("0.00") + " ريـال" : Math.Round(((m.Total) - ((m.Total) / (1 + (m.VatPercent / 100)))), 2).ToString("0.00") + " SAR",
                    invoiceDeliveryPrice = lang == "ar" ? m.DeliveryPrice + " ريـال" : m.DeliveryPrice + " SAR",
                    invoiceDiscount = lang == "ar" ? m.Discount + " ريـال" : m.Discount + " SAR",
                    invoicePromo = "",
                    invoiceTotal = lang == "ar" ? m.Total + " ريـال" : m.Total + " SAR",
                    invoiceTotalDelivery = lang == "ar" ? (m.DeliveryPrice + m.Total) + " ريـال" : (m.DeliveryPrice + m.Total) + " SAR",
                    invoiceTotalWithDelivery = (m.DeliveryPrice + m.Total),
                    invoiceVatPercent = VatSettingPercent,
                    orderType = lang == "ar" ? (m.OrderTypeId == (int)OrderTypeEnum.Now ? "طلب فورى"
                    : "طلب مجدول : " + (m.ScheduleDate != null ? m.ScheduleDate.Value.ToString("dd/MM/yyyy") + "   " + m.ScheduleTime.Value.ToString("hh:mm tt") : ""))
                    : (m.OrderTypeId == (int)OrderTypeEnum.Now ? "Now Order" :
                    "Schedule Order : " + (m.ScheduleDate != null ? m.ScheduleDate.Value.ToString("dd/MM/yyyy") + "   " + m.ScheduleTime.Value.ToString("hh:mm tt") : "")),

                    comapnyname = m.CaptainTypeId == (int)CaptainTypeEnum.Home_Made ? (lang == "ar" ? "شغل بيت" : "Home Made") : (m.ShippingCompany != null ? (lang == "ar" ? m.ShippingCompany.NameAr : m.ShippingCompany.NameEn) : "--"),
                    captainName = m.CaptainTypeId == (int)CaptainTypeEnum.Home_Made ? (m.Drivers != null ? (lang == "ar" ? m.Drivers.NameAr : m.Drivers.NameEn) : (lang == "ar" ? "بانتظار اسناد الطلب" : "Watting Assigned Order")) : "--",
                    captainMobile = m.CaptainTypeId == (int)CaptainTypeEnum.Home_Made ? (m.Drivers != null ? m.Drivers.PhoneNumber : "--") : "--",


                }).FirstOrDefault();
            return getData;
        }
        public OrderVendor VendorOrderDetails(int orderId)
        {
            var getData = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.OrderVendorID == orderId, "Vendors,OrderStatus").FirstOrDefault();
            return getData;
        }
        public IQueryable<OrderItemsViewModel> GetAllOrderItemsViewModelByOrderVendorID(int OrderVendorID, string MainPathView, string lang)
        {

            var getData = Uow.OrderItems.GetAll(x => !x.IsDeleted && x.OrderVendorID == OrderVendorID
                            ).OrderByDescending(p => p.CreateDate)
            .Select(p => new OrderItemsViewModel()
            {
                OrderItemsID = p.OrderItemsID,
                OrderItemsGuid = p.OrderItemsGuid,
                Discount = p.Discount,
                OrderVendorID = p.OrderVendorID,
                Price = p.Price,
                EnableDate = p.EnableDate,
                IsDeleted = p.IsDeleted,
                UpdateDate = p.UpdateDate,
                UserUpdate = p.UserUpdate,
                CreateDate = p.CreateDate,
                DeleteDate = p.DeleteDate,
                UserId = p.UserId,
                UserDelete = p.UserDelete,
                UserEnable = p.UserEnable,
                IsEnable = p.IsEnable,
                ProdImage = !string.IsNullOrEmpty(p.ProdImage) ? (MainPathView + p.ProdImage) : "/Images/noImage.png",
                ProdName = lang == "ar" ? p.ProdNameEn : p.ProdNameEn,
                ProductID = p.ProductID,
                Quantity = p.Quantity,
            });
            return getData;
        }
        public IEnumerable<SiteOrdersViewModel> GetAllSiteOrdersViewModelByUserCustomer(int UserId, string lang, int type, string MainPathView, string VendorPathView)
        {
            var getData = Uow.OrderVendor.GetAll(s => !s.IsDeleted && s.IsEnable && s.Orders.Customers.UserId == UserId &&
      (type == 1 ? (s.OrderStatusID != (int)OrderStatusEnum.Delivered && s.OrderStatusID != (int)OrderStatusEnum.Cancel) : (s.OrderStatusID == (int)OrderStatusEnum.Delivered || s.OrderStatusID == (int)OrderStatusEnum.Cancel)),
      "Vendors,Orders.CustomerLocation,OrderStatus,Orders.Customers").OrderByDescending(s => s.CreateDate)
          .Select(p => new SiteOrdersViewModel()
          {
              DeliveryPrice = p.DeliveryPrice,
              Discount = p.Discount,
              Notes = p.Notes,
              OrdersID = p.OrdersID,
              OrderStatusID = p.OrderStatusID,
              DeleteDate = p.DeleteDate,
              Price = p.Price,
              Total = p.Total,
              Vat = p.VatValue,
              EnableDate = p.EnableDate,
              IsDeleted = p.IsDeleted,
              UpdateDate = p.UpdateDate,
              UserUpdate = p.UserUpdate,
              CreateDate = p.CreateDate,
              UserId = p.UserId,
              UserDelete = p.UserDelete,
              UserEnable = p.UserEnable,
              IsEnable = p.IsEnable,
              CustomersID = p.Orders.CustomersID,
              CustomersName = p.Orders.Customers.FirstName + " " + p.Orders.Customers.SeconedName,
              CustomersMobileNo = p.Orders.Customers.MobileNo,
              CustomersProfilePic = !string.IsNullOrEmpty(p.Orders.Customers.ProfilePic) ? (MainPathView + p.Orders.Customers.ProfilePic) : "/Images/noImage.png",
              CustomersEmail = p.Orders.Customers.Email,
              CustomerLocationAddress = p.Orders.CustomerLocation.Address,
              CustomerLocationLat = p.Orders.CustomerLocation.Lat,
              CustomerLocationLng = p.Orders.CustomerLocation.Lng,
              CustomerLocationBlockName = lang == "ar" ? p.Orders.CustomerLocation.Block.NameAR : p.Orders.CustomerLocation.Block.NameEN,
              CustomerLocationCityName = lang == "ar" ? p.Orders.CustomerLocation.Block.City.NameAR : p.Orders.CustomerLocation.Block.City.NameEN,
              OrdersGuid = p.Orders.OrdersGuid,
              OrderStatusName = lang == "ar" ? p.OrderStatus.NameAR : p.OrderStatus.NameEN,
              VendorsLogo = !string.IsNullOrEmpty(p.Vendors.Logo) ? (VendorPathView + p.Vendors.Logo) : string.Empty,
              VendorsName = lang == "ar" ? p.Vendors.StoreNameAr : p.Vendors.StoreNameEn,
              PaymentTypeName = lang == "ar" ? (p.Orders.PaymentTypeId == (int)PaymentTypeEnum.Bank_Transfer ? "تحويل بنكي" : p.Orders.PaymentTypeId == (int)PaymentTypeEnum.COD ? "كاش" : p.Orders.PaymentTypeId == (int)PaymentTypeEnum.Payment_Card ? "بطاقة الدفع" :
              p.Orders.PaymentTypeId == (int)PaymentTypeEnum.Wallet ? "المحفظة" : "") : (p.Orders.PaymentTypeId == (int)PaymentTypeEnum.Bank_Transfer ? "Bank Transfer" : p.Orders.PaymentTypeId == (int)PaymentTypeEnum.COD ? "COD" : p.Orders.PaymentTypeId == (int)PaymentTypeEnum.Payment_Card ? "Payment Card" :
              p.Orders.PaymentTypeId == (int)PaymentTypeEnum.Wallet ? "Wallet" : ""),
              Type = type,
              VendorsGuid = p.Vendors.VendorsGuid,
              TrackNo = p.TrackNo,
              VendorsContact = p.Vendors.IsShowContact ? p.Vendors.MobileNo : string.Empty,
              OrderTypeId = p.OrderTypeId,
              OrderTypeName = lang == "ar" ? (p.OrderTypeId == (int)OrderTypeEnum.Now ? "طلب فورى" : "طلب مجدول") : (p.OrderTypeId == (int)OrderTypeEnum.Now ? "Now Order" : "Schedule Order"),
              ScheduleDateString = p.OrderTypeId == (int)OrderTypeEnum.Schedule ? (p.ScheduleDate != null ? p.ScheduleDate.Value.ToString("dd/MM/yyyy") : "---") : "---",
              ScheduleTimeString = p.OrderTypeId == (int)OrderTypeEnum.Schedule ? (p.ScheduleTime != null ? p.ScheduleTime.Value.ToString("hh:mm tt") : "---") : "---",
              OrderVendorID = p.OrderVendorID,
          });
            return getData;
        }
        public IEnumerable<SiteOrdersViewModel> GetAllSiteOrdersViewModelByUserCustomerAndPending(int UserId, string lang, string MainPathView, string VendorPathView)
        {
            var getData = Uow.OrderVendor.GetAll(s => s.IsDeleted && s.IsEnable && s.Orders.Customers.UserId == UserId &&
      (s.OrderStatusID == (int)OrderStatusEnum.Pending),
      "Vendors,Orders.CustomerLocation,OrderStatus,Orders.Customers").OrderByDescending(s => s.CreateDate)
          .Select(p => new SiteOrdersViewModel()
          {
              DeliveryPrice = p.DeliveryPrice,
              Discount = p.Discount,
              Notes = p.Notes,
              OrdersID = p.OrdersID,
              OrderStatusID = p.OrderStatusID,
              DeleteDate = p.DeleteDate,
              Price = p.Price,
              Total = p.Total,
              Vat = p.VatValue,
              EnableDate = p.EnableDate,
              IsDeleted = p.IsDeleted,
              UpdateDate = p.UpdateDate,
              UserUpdate = p.UserUpdate,
              CreateDate = p.CreateDate,
              UserId = p.UserId,
              UserDelete = p.UserDelete,
              UserEnable = p.UserEnable,
              IsEnable = p.IsEnable,
              CustomersID = p.Orders.CustomersID,
              CustomersName = p.Orders.Customers.FirstName + " " + p.Orders.Customers.SeconedName,
              CustomersMobileNo = p.Orders.Customers.MobileNo,
              CustomersProfilePic = !string.IsNullOrEmpty(p.Orders.Customers.ProfilePic) ? (MainPathView + p.Orders.Customers.ProfilePic) : "/Images/noImage.png",
              CustomersEmail = p.Orders.Customers.Email,
              CustomerLocationAddress = p.Orders.CustomerLocation.Address,
              CustomerLocationLat = p.Orders.CustomerLocation.Lat,
              CustomerLocationLng = p.Orders.CustomerLocation.Lng,
              CustomerLocationBlockName = lang == "ar" ? p.Orders.CustomerLocation.Block.NameAR : p.Orders.CustomerLocation.Block.NameEN,
              CustomerLocationCityName = lang == "ar" ? p.Orders.CustomerLocation.Block.City.NameAR : p.Orders.CustomerLocation.Block.City.NameEN,
              OrdersGuid = p.Orders.OrdersGuid,
              OrderStatusName = lang == "ar" ? (p.ApprovalQuantity == (int)ApprovalQuantityEnum.Reject ? "تم الرفض" : p.ApprovalQuantity == (int)ApprovalQuantityEnum.Approve ? "تم الموافقة" : p.OrderStatus.NameAR)
              : (p.ApprovalQuantity == (int)ApprovalQuantityEnum.Reject ? "Reject" : p.ApprovalQuantity == (int)ApprovalQuantityEnum.Approve ? "Approve" : p.OrderStatus.NameEN),
              VendorsLogo = !string.IsNullOrEmpty(p.Vendors.Logo) ? (VendorPathView + p.Vendors.Logo) : string.Empty,
              VendorsName = lang == "ar" ? (p.Vendors.FirstNameAr + " " + p.Vendors.SeconedNameAr)
              : (p.Vendors.FirstNameEn + " " + p.Vendors.SeconedNameEn),
              PaymentTypeName = lang == "ar" ? (p.Orders.PaymentTypeId == (int)PaymentTypeEnum.Bank_Transfer ? "تحويل بنكي" : p.Orders.PaymentTypeId == (int)PaymentTypeEnum.COD ? "كاش" : p.Orders.PaymentTypeId == (int)PaymentTypeEnum.Payment_Card ? "بطاقة الدفع" :
              p.Orders.PaymentTypeId == (int)PaymentTypeEnum.Wallet ? "المحفظة" : "") : (p.Orders.PaymentTypeId == (int)PaymentTypeEnum.Bank_Transfer ? "Bank Transfer" : p.Orders.PaymentTypeId == (int)PaymentTypeEnum.COD ? "COD" : p.Orders.PaymentTypeId == (int)PaymentTypeEnum.Payment_Card ? "Payment Card" :
              p.Orders.PaymentTypeId == (int)PaymentTypeEnum.Wallet ? "Wallet" : ""),
              VendorsGuid = p.Vendors.VendorsGuid,
              TrackNo = p.TrackNo,
              VendorsContact = p.Vendors.IsShowContact ? p.Vendors.MobileNo : string.Empty,
              OrderTypeId = p.OrderTypeId,
              OrderTypeName = lang == "ar" ? (p.OrderTypeId == (int)OrderTypeEnum.Now ? "طلب فورى" : "طلب مجدول") : (p.OrderTypeId == (int)OrderTypeEnum.Now ? "Now Order" : "Schedule Order"),
              ScheduleDateString = p.OrderTypeId == (int)OrderTypeEnum.Schedule ? (p.ScheduleDate != null ? p.ScheduleDate.Value.ToString("dd/MM/yyyy") : "---") : "---",
              ScheduleTimeString = p.OrderTypeId == (int)OrderTypeEnum.Schedule ? (p.ScheduleTime != null ? p.ScheduleTime.Value.ToString("hh:mm tt") : "---") : "---",
              OrderVendorID = p.OrderVendorID,
          });
            return getData;
        }
        public SiteOrdersDetailsViewModel GetSiteOrdersDetailsViewModelByUserOrderID(int OrderVendorID, string lang, string CustomerPathView, string VendorPathView, string ProductPathView)
        {
            var getData = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.OrderVendorID == OrderVendorID)
                .Select(p => new SiteOrdersDetailsViewModel()
                {
                    DeliveryPrice = p.DeliveryPrice,
                    Discount = p.Discount,
                    Notes = p.Notes,
                    OrdersID = p.OrdersID,
                    OrderStatusID = p.OrderStatusID,
                    DeleteDate = p.DeleteDate,
                    Price = p.Price,
                    Total = p.Total,
                    Vat = p.VatValue,
                    EnableDate = p.EnableDate,
                    IsDeleted = p.IsDeleted,
                    UpdateDate = p.UpdateDate,
                    UserUpdate = p.UserUpdate,
                    CreateDate = p.CreateDate,
                    UserId = p.UserId,
                    UserDelete = p.UserDelete,
                    UserEnable = p.UserEnable,
                    IsEnable = p.IsEnable,
                    CustomersID = p.Orders.CustomersID,
                    CustomersName = p.Orders.Customers.FirstName + " " + p.Orders.Customers.SeconedName,
                    CustomersMobileNo = p.Orders.Customers.MobileNo,
                    CustomersProfilePic = !string.IsNullOrEmpty(p.Orders.Customers.ProfilePic) ? (CustomerPathView + p.Orders.Customers.ProfilePic) : "/Images/noImage.png",
                    CustomersEmail = p.Orders.Customers.Email,
                    CustomerLocationAddress = p.Orders.CustomerLocation.Address,
                    CustomerLocationLat = p.Orders.CustomerLocation.Lat,
                    CustomerLocationLng = p.Orders.CustomerLocation.Lng,
                    CustomerLocationBlockName = lang == "ar" ? p.Orders.CustomerLocation.Block.NameAR : p.Orders.CustomerLocation.Block.NameEN,
                    CustomerLocationCityName = lang == "ar" ? p.Orders.CustomerLocation.Block.City.NameAR : p.Orders.CustomerLocation.Block.City.NameEN,
                    OrdersGuid = p.Orders.OrdersGuid,
                    OrderStatusName = lang == "ar" ? p.OrderStatus.NameAR : p.OrderStatus.NameEN,
                    VendorsLogo = !string.IsNullOrEmpty(p.Vendors.Logo) ? (VendorPathView + p.Vendors.Logo) : string.Empty,
                    VendorsName = lang == "ar" ? (p.Vendors.FirstNameAr + " " + p.Vendors.SeconedNameAr)
                    : (p.Vendors.FirstNameEn + " " + p.Vendors.SeconedNameEn),
                    PaymentTypeName = lang == "ar" ? (p.Orders.PaymentTypeId == (int)PaymentTypeEnum.Bank_Transfer ? "تحويل بنكي" : p.Orders.PaymentTypeId == (int)PaymentTypeEnum.COD ? "كاش" : p.Orders.PaymentTypeId == (int)PaymentTypeEnum.Payment_Card ? "بطاقة الدفع" :
                    p.Orders.PaymentTypeId == (int)PaymentTypeEnum.Wallet ? "المحفظة" : "") : (p.Orders.PaymentTypeId == (int)PaymentTypeEnum.Bank_Transfer ? "Bank Transfer" : p.Orders.PaymentTypeId == (int)PaymentTypeEnum.COD ? "COD" : p.Orders.PaymentTypeId == (int)PaymentTypeEnum.Payment_Card ? "Payment Card" :
                    p.Orders.PaymentTypeId == (int)PaymentTypeEnum.Wallet ? "Wallet" : ""),

                    OrderAccept = p.OrderHistory.Where(s => s.OrderStatusID == (int)OrderStatusEnum.Accept).FirstOrDefault() != null ?
                    p.OrderHistory.Where(s => s.OrderStatusID == (int)OrderStatusEnum.Accept).FirstOrDefault().CreateDate.ToString("dd/MM/yyyy hh:mm tt") : "",
                    OrderBeingProcessed = p.OrderHistory.Where(s => s.OrderStatusID == (int)OrderStatusEnum.Being_Processed).FirstOrDefault() != null ?
                    p.OrderHistory.Where(s => s.OrderStatusID == (int)OrderStatusEnum.Being_Processed).FirstOrDefault().CreateDate.ToString("dd/MM/yyyy hh:mm tt") : "",
                    OrderBeingDelivery = p.OrderHistory.Where(s => s.OrderStatusID == (int)OrderStatusEnum.Being_Delivery).FirstOrDefault() != null ?
                    p.OrderHistory.Where(s => s.OrderStatusID == (int)OrderStatusEnum.Being_Delivery).FirstOrDefault().CreateDate.ToString("dd/MM/yyyy hh:mm tt") : "",
                    OrderDelivered = p.OrderHistory.Where(s => s.OrderStatusID == (int)OrderStatusEnum.Delivered).FirstOrDefault() != null ?
                    p.OrderHistory.Where(s => s.OrderStatusID == (int)OrderStatusEnum.Delivered).FirstOrDefault().CreateDate.ToString("dd/MM/yyyy hh:mm tt") : "",
                    OrderCreate = p.OrderHistory.Where(s => s.OrderStatusID == (int)OrderStatusEnum.Create).FirstOrDefault() != null ?
                    p.OrderHistory.Where(s => s.OrderStatusID == (int)OrderStatusEnum.Create).FirstOrDefault().CreateDate.ToString("dd/MM/yyyy hh:mm tt") : "",
                    OrderCancel = p.OrderHistory.Where(s => s.OrderStatusID == (int)OrderStatusEnum.Cancel).FirstOrDefault() != null ?
                    p.OrderHistory.Where(s => s.OrderStatusID == (int)OrderStatusEnum.Cancel).FirstOrDefault().CreateDate.ToString("dd/MM/yyyy hh:mm tt") : "",
                    OrderShipping = p.OrderHistory.Where(s => s.OrderStatusID == (int)OrderStatusEnum.Shipping).FirstOrDefault() != null ?
                    p.OrderHistory.Where(s => s.OrderStatusID == (int)OrderStatusEnum.Shipping).FirstOrDefault().CreateDate.ToString("dd/MM/yyyy hh:mm tt") : "",
                    SiteOrderItems = p.OrderItems.Where(x => !x.IsDeleted && x.Quantity > 0).Select(m => new SiteOrderItems()
                    {
                        ProdImage = !string.IsNullOrEmpty(m.ProdImage) ? (ProductPathView + m.ProdImage) : string.Empty,
                        ProdName = lang == "ar" ? m.ProdNameAr : m.ProdNameEn,
                        Quantity = m.Quantity,
                        Price = m.Price,
                    }),
                    TrackNo = p.TrackNo,
                    VendorsGuid = p.Vendors.VendorsGuid,
                    CaptainName = p.CaptainTypeId == (int)CaptainTypeEnum.Shipping_Company ?
                (lang == "ar" ? "كابتن " + p.ShippingCompany.NameAr + "" : "" + p.ShippingCompany.NameEn + " Captain") :
                p.Drivers != null ? (lang == "ar" ? p.Drivers.NameAr : p.Drivers.NameEn) : (lang == "ar" ? "لم يتم اسناد الطلب الي الكابتن." : "The order was not assigned to the captain."),
                    CaptainTypeId = p.CaptainTypeId,
                    CaptainPhone = p.Drivers != null ? p.Drivers.PhoneNumber : string.Empty,
                    DeliveryVatValue = p.DeliveryVatValue,
                    VatProduct = p.VatProduct,
                    //VatValue = Math.Round((p.Total * p.VatPercent / 100), 2),
                    VatPercent = p.VatPercent,
                    VatValue = Math.Round(((p.Total) - ((p.Total) / (1 + (p.VatPercent / 100)))), 2),
                    OrderVendorID = p.OrderVendorID,
                }).FirstOrDefault();
            return getData;
        }
        public SiteOrdersVatViewModel GetSiteOrdersVatViewModelByOrderID(int OrdersID, string lang)
        {
            var getData = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.OrdersID == OrdersID)
                .Select(p => new SiteOrdersVatViewModel()
                {
                    TaxIdentificationNumber = string.Empty,
                    TrackNo = p.TrackNo,
                    OrderDate = p.CreateDate.ToString("dd/MM/yyyy hh:mm tt"),
                    DeliveryPrice = p.DeliveryPrice,
                    DeliveryVatPercent = p.VatPercent,
                    DeliveryVatValue = p.DeliveryVatValue,
                    OrdersID = p.OrdersID,
                    OrdersGuid = p.OrderVendorGuid,
                    Address = p.Vendors.Address,
                    VendorsName = p.Vendors.StoreNameAr,
                    SiteOrderItems = p.OrderItems.Where(x => !x.IsDeleted).Select(m => new SiteOrderItems()
                    {
                        ProdName = lang == "ar" ? m.ProdNameAr : m.ProdNameEn,
                        Quantity = m.Quantity,
                        Price = m.Price,
                    }),
                    Discount = p.Discount,
                    Price = p.Price,
                    VatProduct = p.VatProduct,
                    Total = p.Total,
                    //VatValue = Math.Round((p.Total * p.VatPercent / 100), 2),
                    VatValue = Math.Round(((p.Total) - ((p.Total) / (1 + (p.VatPercent / 100)))), 2),
                    OrderVendorID = p.OrderVendorID,
                }).FirstOrDefault();
            getData.TaxIdentificationNumber = Uow.Configuration.GetAll(s => !s.IsDeleted).FirstOrDefault() != null ? Uow.Configuration.GetAll(s => !s.IsDeleted).FirstOrDefault().TaxNumber : string.Empty;
            getData.Address = Uow.Configuration.GetAll(s => !s.IsDeleted).FirstOrDefault() != null ? Uow.Configuration.GetAll(s => !s.IsDeleted).FirstOrDefault().Address : string.Empty;

            return getData;
        }
        #endregion
        #region Actions
        public bool IsPromoExisits(string PromoCode)
        {
            return Uow.PromoCode.GetAll(e => !e.IsDeleted).Where(e => e.Code == PromoCode).Any();
        }
        public bool IsPromoExisits(string PromoCode, int PromoId)
        {
            return Uow.PromoCode.GetAll(e => !e.IsDeleted).Where(e => e.Code == PromoCode && e.PromoCodeID != PromoId).Any();
        }
        public bool Insert(PromoCodeMasterViewModel viewModel)
        {
            //StoredResultViewModel _res = new StoredResultViewModel();
            try
            {
                //viewModel.PromoCode = _bLGeneral.KeyGeneratorNumbersOnly(10);
                //bool PromoCodeExists = Uow.PromoCode.GetAll().Where(e => e.Code == viewModel.PromoCode).Any();
                //while (PromoCodeExists)
                //{
                //    viewModel.PromoCode = _bLGeneral.KeyGeneratorNumbersOnly(10);
                //    PromoCodeExists = Uow.PromoCode.GetAll(e => e.Code == viewModel.PromoCode).Any();
                //}
                var Data = new Model.Order.PromoCode()
                {
                    IsDeleted = false,
                    IsEnable = true,
                    Code = viewModel.PromoCode,
                    Content = viewModel.Content,
                    CreateDate = _bLGeneral.GetDateTimeNow(),
                    DiscountType = viewModel.DiscountType,
                    DiscountValue = viewModel.DiscountValue,
                    ExpiryDate = viewModel.ExpiryDate,
                    LimitCount = viewModel.LimitCount,
                    PromoCodeGuid = Guid.NewGuid(),
                    StartDate = viewModel.StartDate,
                    Subject = viewModel.Subject,
                    UserId = viewModel.UserId,
                };
                //if (viewModel.PromoType == (int)PromoTypeEnum.On_Store)
                //{
                //    viewModel.lstCityID.ForEach(CityID => Data.VendorPromo.Add(new VendorPromo()
                //    {
                //        VendorsID = CityID,
                //        CreateDate = _bLGeneral.GetDateTimeNow(),
                //        IsDeleted = false,
                //        VendorPromoGuid = Guid.NewGuid(),
                //        UserId = viewModel.UserId,
                //    }));
                //}
                Uow.PromoCode.Insert(Data);
                Uow.Commit();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            //using (SqlConnection sqlcon = new SqlConnection(conString))
            //{
            //    using (SqlCommand cmd = new SqlCommand("proc_Fare_InsertPromoCode", sqlcon))
            //    {
            //        DataTable Citytable = new DataTable();
            //        Citytable.Columns.Add("CityID", typeof(Int32));
            //        foreach (var item in viewModel.lstCityID)
            //        {
            //            Citytable.Rows.Add(
            //                item
            //                );
            //        }
            //        // add the table-valued-parameter. 
            //        cmd.CommandType = CommandType.StoredProcedure;
            //        cmd.Parameters.AddWithValue("@Content", viewModel.Content);
            //        cmd.Parameters.AddWithValue("@DiscountType", viewModel.DiscountType);
            //        cmd.Parameters.AddWithValue("@DiscountValue", viewModel.DiscountValue);
            //        cmd.Parameters.AddWithValue("@ExpiryDate", viewModel.ExpiryDate);
            //        cmd.Parameters.AddWithValue("@UserId", viewModel.UserId);
            //        cmd.Parameters.AddWithValue("@LimitCount", viewModel.LimitCount);
            //        cmd.Parameters.AddWithValue("@PromoCode", viewModel.PromoCode);
            //        cmd.Parameters.AddWithValue("@StartDate", viewModel.StartDate);
            //        cmd.Parameters.AddWithValue("@Subject", viewModel.Subject);
            //        cmd.Parameters.Add("@lstCityID", SqlDbType.Structured).Value = Citytable;

            //        // execute
            //        sqlcon.Open();
            //        SqlDataReader ResultsMeeting = cmd.ExecuteReader();
            //        DataTable dtMeeting = new DataTable();
            //        dtMeeting.Load(ResultsMeeting);
            //        if (dtMeeting.Rows.Count > 0)
            //        {
            //            _res.ErrorNumber = Convert.ToString(dtMeeting.Rows[0]["ErrorNumber"]);
            //            _res.ErrorSeverity = Convert.ToString(dtMeeting.Rows[0]["ErrorSeverity"]);
            //            _res.ErrorState = Convert.ToString(dtMeeting.Rows[0]["ErrorState"]);
            //            _res.ErrorProcedure = Convert.ToString(dtMeeting.Rows[0]["ErrorProcedure"]);
            //            _res.ErrorLine = Convert.ToString(dtMeeting.Rows[0]["ErrorLine"]);
            //            _res.ErrorMessage = Convert.ToString(dtMeeting.Rows[0]["ErrorMessage"]);
            //            IsSave = false;
            //        }
            //        sqlcon.Close();
            //    }
            //}
        }
        /// <summary>
        /// Update PromoCode
        /// </summary>
        /// <param name="PromoCodeMasterViewModel">New PromoCodeMaster</param>
        /// <returns></returns>
        public bool Update(PromoCodeMasterViewModel viewModel)
        {
            ResultMessage result = new ResultMessage();
            var promoCodeMaster = GetPromoCodeMasterByID(viewModel.PromoCodeMasterID);

            if (promoCodeMaster == null) return false;
            //if (promoCodeMaster.VendorPromo?.Count() > 0)
            //{
            //    var deletedObjects = promoCodeMaster.VendorPromo.Where(x => !viewModel.lstCityID.Contains(x.VendorsID)).ToList();
            //    var ExistsObjects = promoCodeMaster.VendorPromo.Where(x => viewModel.lstCityID.Contains(x.VendorsID)).ToList();

            //    var NewObjects = viewModel.lstCityID
            //        .Where(x => deletedObjects.Count(y => y.VendorsID == x) == 0 && ExistsObjects.Count(y => y.VendorsID == x) == 0).
            //        Select(x => new VendorPromo()
            //        {
            //            VendorsID = x,
            //            CreateDate = _bLGeneral.GetDateTimeNow(),
            //            IsDeleted = false,
            //            VendorPromoGuid = Guid.NewGuid(),
            //            UserId = viewModel.UserId,
            //            PromoCodeID = viewModel.PromoCodeMasterID
            //        }).ToList();
            //    foreach (var item in deletedObjects)
            //    {
            //        item.UserDelete = viewModel.UserId;
            //        item.DeleteDate = _bLGeneral.GetDateTimeNow();
            //        item.IsDeleted = true;
            //        Uow.VendorPromo.Update(item);
            //    }
            //    foreach (var item in deletedObjects.ToList())
            //    {
            //        promoCodeMaster.VendorPromo.Remove(item);
            //    }
            //    foreach (var item in NewObjects)
            //    {
            //        promoCodeMaster.VendorPromo.Add(item);
            //        Uow.VendorPromo.Insert(item);
            //    }
            //}
            //else 
            //if (viewModel.lstCityID?.Count() > 0)
            //{
            //    viewModel.lstCityID.ForEach(CityID => Uow.VendorPromo.Insert(new VendorPromo()
            //    {
            //        VendorsID = CityID,
            //        CreateDate = _bLGeneral.GetDateTimeNow(),
            //        IsDeleted = false,
            //        PromoCodeID = promoCodeMaster.PromoCodeID,
            //        VendorPromoGuid = Guid.NewGuid(),
            //        UserId = viewModel.UserId,
            //    }));
            //}
            promoCodeMaster.Content = viewModel.Content;
            promoCodeMaster.DiscountType = viewModel.DiscountType;
            promoCodeMaster.Code = viewModel.PromoCode;
            promoCodeMaster.DiscountValue = viewModel.DiscountValue;
            promoCodeMaster.ExpiryDate = viewModel.ExpiryDate;
            promoCodeMaster.UpdateDate = _bLGeneral.GetDateTimeNow();
            promoCodeMaster.LimitCount = viewModel.LimitCount;
            promoCodeMaster.StartDate = viewModel.StartDate;
            promoCodeMaster.Subject = viewModel.Subject;
            promoCodeMaster.UserUpdate = viewModel.UserUpdate;
            Uow.PromoCode.Update(promoCodeMaster);
            Uow.Commit();
            return true;
        }
        public Orders updateOrders(Orders orders)
        {
            orders.UpdateDate = _bLGeneral.GetDateTimeNow();
            orders = Uow.Orders.Update(orders);
            Uow.Commit();
            return orders;
        }
        public bool UpdateIncreaseOrder(UpdateIncreaseOrderViewModelApi model, int UserId)
        {
            bool IsSuccess = true;
            if (model.listItem != null)
            {
                foreach (var item in model.listItem)
                {
                    var orderItems = GetOrderItemsById(item.orderItemId);

                    if (orderItems == null) return false;

                    if (orderItems.Quantity != item.quantity)
                    {
                        if (item.quantity > 0)
                        {
                            orderItems.Quantity = item.quantity;
                            orderItems.UpdateDate = _bLGeneral.GetDateTimeNow();
                            orderItems.UserUpdate = UserId;
                            orderItems = Uow.OrderItems.Update(orderItems);
                        }
                    }
                }
            }
            if (model.type == (int)ApprovalQuantityEnum.Approve)
            {
                IsSuccess = ApproveOrder(model.orderVendorID, UserId);
            }
            else if (model.type == (int)ApprovalQuantityEnum.Reject)
            {
                IsSuccess = RejectOrder(model.orderVendorID, UserId);
            }

            Uow.Commit();
            return IsSuccess;
        }
        public bool UpdateOrderItems(int orderVendorID, int[] listOrderItemsID, string[] listQuantity, int UserId)
        {
            for (int i = 0; i < listOrderItemsID.Count(); i++)
            {
                var orderItems = GetOrderItemsById(listOrderItemsID[i]);

                if (orderItems == null) return false;

                orderItems.Quantity = decimal.Parse(listQuantity[i], CultureInfo.InvariantCulture);
                orderItems.UpdateDate = _bLGeneral.GetDateTimeNow();
                orderItems.UserUpdate = UserId;
                orderItems = Uow.OrderItems.Update(orderItems);
            }
            var orderVendor = GetOrderVendorByIdAndDeleted(orderVendorID);

            if (orderVendor == null) return false;

            orderVendor.ApprovalQuantity = (int)ApprovalQuantityEnum.Approve;
            orderVendor.UpdateDate = _bLGeneral.GetDateTimeNow();
            orderVendor.UserUpdate = UserId;
            orderVendor = Uow.OrderVendor.Update(orderVendor);
            var orderVendordata = GetOrderVendorByIdAndDeleted(orderVendorID, "Vendors,Orders.Customers");
            Uow.Notification.Insert(new Notification()
            {
                CreateDate = _bLGeneral.GetDateTimeNow(),
                CustomersID = orderVendordata.Orders.CustomersID,
                TitleAR = "تم الموافقة  على طلبك رقم " + orderVendordata.OrderVendorID,
                TitleEN = "your order has been Approved No " + orderVendordata.OrderVendorID,
                DescAR = "تم الموافقة على طلبك رقم " + orderVendordata.OrderVendorID + " من خلال " + orderVendordata.Vendors.StoreNameAr + " في تاريخ " + _bLGeneral.GetDateTimeNow(),
                DescEN = "order No " + orderVendordata.OrderVendorID + " has been Approved from " + orderVendordata.Vendors.StoreNameEn + " on date " + _bLGeneral.GetDateTimeNow(),
                IsDeleted = false,
                IsEnable = true,
                IsSeen = false,
                NotificationGuid = Guid.NewGuid(),
                NotificationTypeID = (int)NotificationTypeEnum.Order,
                UserId = UserId,
                NotificationDate = _bLGeneral.GetDateTimeNow(),
                OrderVendorID = orderVendor.OrderVendorID,
            });
            var _PushListVendor = new PushList()
            {
                orderId = orderVendor.OrdersID,
                lat = 0,
                lng = 0,
                trackNo = "",
                profit = "",
                cusotmerAddress = "",
                vendorAddress = "",
                vendorLogo = "",
                vendorName = "",
                body = "تم الموافقة  على طلبك رقم " + orderVendordata.OrderVendorID,
                sound = "default", //For IOS
                title = "طلب زيادة الكمية",
                content_available = "true", //For Android
                priority = "high", //For Android,
                serverKey = _configuration["FireBaseKey"],
                estimation = 20,
                pushType = (int)PushTypeEnum.PendingApprove
            };
            var UserDataVendor = _blTokens.GetMobileDataByUserID(orderVendor.Orders.Customers.UserId);
            if (UserDataVendor != null)
            {
                _bLGeneral.SendPush(UserDataVendor.TokenVal, UserDataVendor.DeviceType, _PushListVendor);
            }
            Uow.Commit();
            return true;
        }
        public bool ApproveOrder(int orderVendorID, int UserId)
        {
            var orderVendor = GetOrderVendorByIdAndDeleted(orderVendorID);

            if (orderVendor == null) return false;

            orderVendor.ApprovalQuantity = (int)ApprovalQuantityEnum.Approve;
            orderVendor.UpdateDate = _bLGeneral.GetDateTimeNow();
            orderVendor.UserUpdate = UserId;
            orderVendor = Uow.OrderVendor.Update(orderVendor);
            var orderVendordata = GetOrderVendorByIdAndDeleted(orderVendorID, "Vendors,Orders.Customers");
            Uow.Notification.Insert(new Notification()
            {
                CreateDate = _bLGeneral.GetDateTimeNow(),
                CustomersID = orderVendordata.Orders.CustomersID,
                TitleAR = "تم الموافقة  على طلبك رقم " + orderVendordata.OrderVendorID,
                TitleEN = "your order has been Approved No " + orderVendordata.OrderVendorID,
                DescAR = "تم الموافقة على طلبك رقم " + orderVendordata.OrderVendorID + " من خلال " + orderVendordata.Vendors.StoreNameAr + " في تاريخ " + _bLGeneral.GetDateTimeNow(),
                DescEN = "order No " + orderVendordata.OrderVendorID + " has been Approved from " + orderVendordata.Vendors.StoreNameEn + " on date " + _bLGeneral.GetDateTimeNow(),
                IsDeleted = false,
                IsEnable = true,
                IsSeen = false,
                NotificationGuid = Guid.NewGuid(),
                NotificationTypeID = (int)NotificationTypeEnum.Order,
                UserId = UserId,
                NotificationDate = _bLGeneral.GetDateTimeNow(),
                OrderVendorID = orderVendor.OrderVendorID,
            });
            var _PushListVendor = new PushList()
            {
                orderId = orderVendor.OrdersID,
                lat = 0,
                lng = 0,
                trackNo = "",
                profit = "",
                cusotmerAddress = "",
                vendorAddress = "",
                vendorLogo = "",
                vendorName = "",
                body = "تم الموافقة  على طلبك رقم " + orderVendordata.OrderVendorID,
                sound = "default", //For IOS
                title = "طلب زيادة الكمية",
                content_available = "true", //For Android
                priority = "high", //For Android,
                serverKey = _configuration["FireBaseKey"],
                estimation = 20,
                pushType = (int)PushTypeEnum.PendingApprove
            };
            var UserDataVendor = _blTokens.GetMobileDataByUserID(orderVendor.Orders.Customers.UserId);
            if (UserDataVendor != null)
            {
                _bLGeneral.SendPush(UserDataVendor.TokenVal, UserDataVendor.DeviceType, _PushListVendor);
            }
            Uow.Commit();
            return true;
        }
        public bool RejectOrder(int orderVendorID, int UserId)
        {
            var orderVendor = GetOrderVendorByIdAndDeleted(orderVendorID);

            if (orderVendor == null) return false;

            orderVendor.ApprovalQuantity = (int)ApprovalQuantityEnum.Reject;
            orderVendor.UpdateDate = _bLGeneral.GetDateTimeNow();
            orderVendor.UserUpdate = UserId;
            orderVendor = Uow.OrderVendor.Update(orderVendor);
            var orderVendordata = GetOrderVendorByIdAndDeleted(orderVendorID, "Vendors,Orders.Customers");
            Uow.Notification.Insert(new Notification()
            {
                CreateDate = _bLGeneral.GetDateTimeNow(),
                CustomersID = orderVendordata.Orders.CustomersID,
                TitleAR = "تم رفض طلبك رقم " + orderVendordata.OrderVendorID,
                TitleEN = "your order has been Rejected No " + orderVendordata.OrderVendorID,
                DescAR = "تم رفض طلبك رقم " + orderVendordata.OrderVendorID + " من خلال " + orderVendordata.Vendors.StoreNameAr + " في تاريخ " + _bLGeneral.GetDateTimeNow(),
                DescEN = "order No " + orderVendordata.OrderVendorID + " has been Rejected from " + orderVendordata.Vendors.StoreNameEn + " on date " + _bLGeneral.GetDateTimeNow(),
                IsDeleted = false,
                IsEnable = true,
                IsSeen = false,
                NotificationGuid = Guid.NewGuid(),
                NotificationTypeID = (int)NotificationTypeEnum.Order,
                UserId = UserId,
                NotificationDate = _bLGeneral.GetDateTimeNow(),
                OrderVendorID = orderVendor.OrderVendorID,
            });
            var _PushListVendor = new PushList()
            {
                orderId = orderVendor.OrdersID,
                lat = 0,
                lng = 0,
                trackNo = "",
                profit = "",
                cusotmerAddress = "",
                vendorAddress = "",
                vendorLogo = "",
                vendorName = "",
                body = "تم رفض طلبك رقم " + orderVendordata.OrderVendorID,
                sound = "default", //For IOS
                title = "طلب زيادة الكمية",
                content_available = "true", //For Android
                priority = "high", //For Android,
                serverKey = _configuration["FireBaseKey"],
                estimation = 20,
                pushType = (int)PushTypeEnum.PendingReject
            };
            var UserDataVendor = _blTokens.GetMobileDataByUserID(orderVendor.Orders.Customers.UserId);
            if (UserDataVendor != null)
            {
                _bLGeneral.SendPush(UserDataVendor.TokenVal, UserDataVendor.DeviceType, _PushListVendor);
            }
            Uow.Commit();
            return true;
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResultMessage Delete(int id, int UserId)
        {
            var promoCodeMaster = GetPromoCodeMasterByID(id);
            var success = false;
            if (promoCodeMaster != null)
            {
                foreach (Model.Order.OrderPromo item in promoCodeMaster.OrderPromo)
                {
                    item.IsDeleted = true;
                    item.UserDelete = UserId;
                    item.DeleteDate = _bLGeneral.GetDateTimeNow();
                    Uow.OrderPromo.Update(item);
                }
                promoCodeMaster.IsDeleted = true;
                promoCodeMaster.UserDelete = UserId;
                promoCodeMaster.DeleteDate = _bLGeneral.GetDateTimeNow();
                promoCodeMaster = Uow.PromoCode.Update(promoCodeMaster);
                Uow.Commit();
                success = true;
            }
            return new ResultMessage() { Status = success };
        }
        public bool DeleteOrderVendor(int vendorId, int ordersID)
        {
            var ordervendor = GetOrdersByVendorAndOrderAndPending(vendorId, ordersID);
            if (ordervendor != null)
            {
                var history = Uow.OrderHistory.GetAll(s => s.OrderVendorID == ordervendor.OrderVendorID);
                if (history.Any())
                {
                    Uow.OrderHistory.DeleteRang(history);
                }
                var notifications = Uow.Notification.GetAll(s => s.OrderVendorID == ordervendor.OrderVendorID);
                if (notifications.Any())
                {
                    Uow.Notification.DeleteRang(notifications);
                }
                var items = Uow.OrderItems.GetAll(s => s.OrderVendorID == ordervendor.OrderVendorID);
                if (items.Any())
                {
                    Uow.OrderItems.DeleteRang(items);
                }
                Uow.OrderVendor.Delete(ordervendor);
                Uow.Commit();
                return true;
            }
            return false;
        }
        public bool DeleteItemFromPending(int productID, int ordersID, out bool IsDeletedVendor)
        {
            var orderItems = GetOrderItemsByProductAndOrderAndPending(productID, ordersID);
            IsDeletedVendor = false;
            if (orderItems != null)
            {
                orderItems.IsDeleted = true;
                orderItems.DeleteDate = _bLGeneral.GetDateTimeNow();
                Uow.OrderItems.Update(orderItems);
                if (!IsExistOrderItemsByIDAndOrderVendorAndPending(orderItems.OrderItemsID, orderItems.OrderVendorID))
                {
                    IsDeletedVendor = true;
                    var ordervendor = GetOrderVendorByIDAndPending(orderItems.OrderVendorID);
                    if (ordervendor != null)
                    {
                        var history = Uow.OrderHistory.GetAll(s => s.OrderVendorID == orderItems.OrderVendorID);
                        if (history.Any())
                        {
                            Uow.OrderHistory.DeleteRang(history);
                        }
                        var notifications = Uow.Notification.GetAll(s => s.OrderVendorID == orderItems.OrderVendorID);
                        if (notifications.Any())
                        {
                            Uow.Notification.DeleteRang(notifications);
                        }
                        var items = Uow.OrderItems.GetAll(s => s.OrderVendorID == ordervendor.OrderVendorID);
                        if (items.Any())
                        {
                            Uow.OrderItems.DeleteRang(items);
                        }
                        Uow.OrderVendor.Delete(ordervendor);
                    }
                }
                Uow.Commit();
                return true;
            }
            return false;
        }
        /// <summary>
        /// Get PromoCodeMasterViewModels for DataTable
        /// </summary>
        /// <returns></returns>
        public IQueryable<PromoCodeMasterViewModel> GetPromoCodeMasterViewModels(Guid? PromoCodeGuid = null)
        {
            if (PromoCodeGuid.HasValue)
            {
                return Uow.PromoCode.GetAll(p => p.PromoCodeGuid == PromoCodeGuid, "").Select(p => new PromoCodeMasterViewModel
                {
                    PromoCode = p.Code,
                    Content = p.Content,
                    Subject = p.Subject,
                    StartDate = p.StartDate,
                    PromoCodeGuid = p.PromoCodeGuid,
                    PromoCodeMasterID = p.PromoCodeID,
                    ExpiryDate = p.ExpiryDate,
                    DiscountType = p.DiscountType,
                    DiscountValue = p.DiscountValue,
                    LimitCount = p.LimitCount,
                    UserId = p.UserId,
                });
            }

            return Uow.PromoCode.GetAll(p => !p.IsDeleted).Select(p => new PromoCodeMasterViewModel
            {
                PromoCode = p.Code,
                Content = p.Content,
                Subject = p.Subject,
                StartDate = p.StartDate,
                PromoCodeGuid = p.PromoCodeGuid,
                PromoCodeMasterID = p.PromoCodeID,
                ExpiryDate = p.ExpiryDate,
                DiscountType = p.DiscountType,
                DiscountValue = p.DiscountValue,
                LimitCount = p.LimitCount,
                UserId = p.UserId,
                Cities = p.VendorPromo.Select(x => x.Vendors).ToList(),
                CreateDate = p.CreateDate,
                PromoType = p.PromoType,
            });
        }
        /// <summary>
        /// Get PromoCodeMasterViewModels for Details
        /// </summary>
        /// <returns></returns>
        public PromoCodeMasterViewModel GetPromoCodeMasterViewModelByGuid(Guid PromoCodeGuid, bool IsAr)
        {
            var getData = Uow.PromoCode.GetAll(p => p.PromoCodeGuid == PromoCodeGuid, "")
            .Select(p => new PromoCodeMasterViewModel()
            {
                PromoCode = p.Code,
                Content = p.Content,
                Subject = p.Subject,
                StartDate = p.StartDate,
                PromoCodeGuid = p.PromoCodeGuid,
                PromoCodeMasterID = p.PromoCodeID,
                ExpiryDate = p.ExpiryDate,
                DiscountType = p.DiscountType,
                DiscountValue = p.DiscountValue,
                LimitCount = p.LimitCount,
                UserId = p.UserId,
                lstCityName = p.VendorPromo.Select(x => IsAr ? x.Vendors.StoreNameAr : x.Vendors.StoreNameEn).ToList(),
                lstCityID = p.VendorPromo.Select(x => x.VendorsID).ToList(),
                TripPromoCodeCount = p.OrderPromo.Count(x => !x.IsDeleted),
            }).FirstOrDefault();
            return getData;
        }
        /// <summary>
        /// Get PromoCodeMaster by ID With Details
        /// </summary>
        /// <returns></returns>
        public Model.Order.PromoCode GetPromoCodeMasterByID(int PromoCodeMasterID)
        {
            var getData = Uow.PromoCode.GetAll(p => p.PromoCodeID == PromoCodeMasterID, "").FirstOrDefault();
            return getData;
        }
        public Model.Order.OrderItems GetOrderItemsById(int id)
        {
            return Uow.OrderItems.GetAll(p => !p.IsDeleted && p.OrderItemsID == id).FirstOrDefault();
        }
        public int GetPromoCodeDetailsCountByPromoCodeMasterID(int PromoCodeMasterID)
        {
            return Uow.OrderPromo.GetAll(s => s.PromoCodeID == PromoCodeMasterID && !s.IsDeleted).Count();
        }
        #endregion
        #region AssignOrder
        public OrderVendor GetOrderVendorById(int id)
        {
            return Uow.OrderVendor.GetAll(p => !p.IsDeleted && p.OrderVendorID == id, "Vendors").FirstOrDefault();
        }
        public Orders GetOrdersById(int id)
        {
            return Uow.Orders.GetAll(p => !p.IsDeleted && p.OrdersID == id).FirstOrDefault();
        }
        public OrderVendor GetOrderVendorByIdAndDeleted(int id)
        {
            return Uow.OrderVendor.GetAll(p => p.IsDeleted && p.OrderVendorID == id).FirstOrDefault();
        }
        public OrderVendor GetOrderVendorByIdAndDeleted(int id, string include)
        {
            return Uow.OrderVendor.GetAll(p => p.IsDeleted && p.OrderVendorID == id, include).FirstOrDefault();
        }
        public OrderVendor GetOrderVendorById(int id, string include)
        {
            return Uow.OrderVendor.GetAll(p => !p.IsDeleted && p.OrderVendorID == id, include).FirstOrDefault();
        }
        public OrderVendor GetOrderVendorByOrderAndVendorAndDelete(int OrdersID, int VendorsID, string include = "")
        {
            return Uow.OrderVendor.GetAll(p => p.IsDeleted && p.OrdersID == OrdersID && p.VendorsID == VendorsID, include).FirstOrDefault();
        }
        public List<BlockAssignOrderViewModel> GetAllBlockAssignOrderViewModel(string lang)
        {
            var getData = Uow.OrderVendor.GetAll(x => !x.IsDeleted && (x.OrderStatusID == (int)OrderStatusEnum.Accept
            || x.OrderStatusID == (int)OrderStatusEnum.Being_Processed || x.OrderStatusID == (int)OrderStatusEnum.Shipping
            || x.OrderStatusID == (int)OrderStatusEnum.Reject_Shipping)
            && x.Vendors.BlockID != null
            , "Vendors.Block").ToList().GroupBy(z => z.Vendors.BlockID)
            .Select(p => new BlockAssignOrderViewModel()
            {
                OrdersCount = p.Count(),
                BlockName = lang == "ar" ? (p.FirstOrDefault().Vendors.Block.NameAR) : (p.FirstOrDefault().Vendors.Block.NameEN),
                BlockID = p.Key != null ? (int)p.Key : 0,
            }).ToList();
            return getData;
        }
        public List<AssignOrderVendorViewModel> GetAllAssignOrderVendorViewModel(string[] ListBlockID, string lang)
        {
            if (ListBlockID != null)
            {
                if (ListBlockID.Count(p => !string.IsNullOrEmpty(p)) == 0)
                {
                    ListBlockID = null;
                }
            }
            var getData = Uow.OrderVendor.GetAll(x => !x.IsDeleted && (x.OrderStatusID == (int)OrderStatusEnum.Accept
            || x.OrderStatusID == (int)OrderStatusEnum.Being_Processed || x.OrderStatusID == (int)OrderStatusEnum.Shipping
            || x.OrderStatusID == (int)OrderStatusEnum.Reject_Shipping)
             && (ListBlockID != null ? ListBlockID.Any(y => x.Vendors.BlockID.ToString() == y) : false)
            , "Orders.Customers,Vendors.Block").OrderByDescending(x => x.CreateDate)
            .Select(p => new AssignOrderVendorViewModel()
            {
                OrderVendorID = p.OrderVendorID,
                CustomersName = p.Orders.Customers.FirstName + " " + p.Orders.Customers.SeconedName,
                BlockName = lang == "ar" ? p.Vendors.Block.NameAR : p.Vendors.Block.NameEN,
                DistanceKM = p.DistanceKM,
                BlockID = p.Vendors.BlockID,
                OrderStatusName = lang == "ar" ? p.OrderStatus.NameAR : p.OrderStatus.NameEN,
            }).ToList();
            return getData;
        }
        public List<DriverAssignOrderViewModel> GetAllDriverAssignOrderViewModel(string[] ListBlockID, int? OnlineTypeID, string lang)
        {
            if (ListBlockID != null)
            {
                if (ListBlockID.Count(p => !string.IsNullOrEmpty(p)) == 0)
                {
                    ListBlockID = null;
                }
            }
            var getData = Uow.CaptainZone.GetAll(x => !x.IsDeleted
             && (ListBlockID != null ? ListBlockID.Any(y => x.BlockID.ToString() == y) : false)
             && (OnlineTypeID != null ? x.Drivers.IsOnline == Convert.ToBoolean(OnlineTypeID) : true)
            , "Drivers,Block").OrderByDescending(x => x.CreateDate)
            .Select(p => new DriverAssignOrderViewModel()
            {
                DriversID = p.DriversID,
                DriversName = lang == "ar" ? p.Drivers.NameAr : p.Drivers.NameEn,
                BlockName = lang == "ar" ? p.Block.NameAR : p.Block.NameEN,
            }).ToList();
            return getData;
        }
        public bool AssignOrderVendorToHomeMade(string[] ListOrderVendorID, int DriversID, int UserId)
        {
            foreach (var item in ListOrderVendorID)
            {
                try
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        var orderVendor = GetOrderVendorById(Convert.ToInt32(item), "Orders.CustomerLocation,Vendors");
                        if (orderVendor != null)
                        {
                            orderVendor.UpdateDate = _bLGeneral.GetDateTimeNow();
                            orderVendor.UserUpdate = UserId;
                            orderVendor.DriversID = DriversID;
                            orderVendor.OrderStatusID = (int)OrderStatusEnum.Assign;
                            Uow.OrderVendor.Update(orderVendor);

                            OrderHistory orderHistory = new OrderHistory()
                            {
                                CreateDate = _bLGeneral.GetDateTimeNow(),
                                IsDeleted = false,
                                IsEnable = true,
                                OrderHistoryGuid = Guid.NewGuid(),
                                OrderStatusID = (int)OrderStatusEnum.Assign,
                                OrderVendorID = orderVendor.OrderVendorID,
                                UserId = UserId,
                                Lat = 0,
                                Lng = 0,
                                CancelReason = string.Empty,
                            };
                            Uow.OrderHistory.Insert(orderHistory);
                        }
                        decimal DriverCommision = 0;
                        var DeliverySetting = Uow.DeliverySetting.GetAll(s => !s.IsDeleted).FirstOrDefault();
                        if (DeliverySetting != null)
                        {
                            DriverCommision = DeliverySetting.DriverCommision; //Math.Round(((DeliverySetting.DriverCommision * DeliverySetting.BaseFare)) / 100, 2);
                            var Distance = orderVendor.DistanceKM != null ? (decimal)orderVendor.DistanceKM : 0;
                            if (Distance > DeliverySetting.MinKM)
                            {
                                var OverKm = Math.Round(Distance - DeliverySetting.MinKM, 2);
                                var OverKmPrice = OverKm * DeliverySetting.OverKmFare;
                                DriverCommision = Math.Round(DriverCommision + OverKmPrice, 2);
                            }
                        }
                        // Push
                        sendNotificationToDriver(orderVendor.TrackNo, orderVendor.Orders.CustomerLocation.Address, orderVendor.Vendors.Address,
                            orderVendor.Vendors.Logo, orderVendor.Vendors.StoreNameAr, orderVendor.OrderVendorID, Uow.Drivers.GetById(DriversID).UserId, DriverCommision);
                    }
                }
                catch (Exception)
                {


                }

            }
            Uow.Commit();
            return true;
        }
        public bool ReAssignOrderVendorToHomeMade(int OrderVendorID, int DriversID, int UserId)
        {
            try
            {
                var orderVendor = GetOrderVendorById(OrderVendorID, "Orders.CustomerLocation,Vendors");
                if (orderVendor != null)
                {
                    orderVendor.UpdateDate = _bLGeneral.GetDateTimeNow();
                    orderVendor.UserUpdate = UserId;
                    orderVendor.DriversID = DriversID;
                    orderVendor.OrderStatusID = (int)OrderStatusEnum.Assign;
                    Uow.OrderVendor.Update(orderVendor);

                    OrderHistory orderHistory = new OrderHistory()
                    {
                        CreateDate = _bLGeneral.GetDateTimeNow(),
                        IsDeleted = false,
                        IsEnable = true,
                        OrderHistoryGuid = Guid.NewGuid(),
                        OrderStatusID = (int)OrderStatusEnum.Assign,
                        OrderVendorID = orderVendor.OrderVendorID,
                        UserId = UserId,
                        Lat = 0,
                        Lng = 0,
                        CancelReason = string.Empty,
                    };
                    Uow.OrderHistory.Insert(orderHistory);
                }
                decimal DriverCommision = 0;
                var DeliverySetting = Uow.DeliverySetting.GetAll(s => !s.IsDeleted).FirstOrDefault();
                if (DeliverySetting != null)
                {
                    DriverCommision = DeliverySetting.DriverCommision; //Math.Round(((DeliverySetting.DriverCommision * DeliverySetting.BaseFare)) / 100, 2);
                    var Distance = orderVendor.DistanceKM != null ? (decimal)orderVendor.DistanceKM : 0;
                    if (Distance > DeliverySetting.MinKM)
                    {
                        var OverKm = Math.Round(Distance - DeliverySetting.MinKM, 2);
                        var OverKmPrice = OverKm * DeliverySetting.OverKmFare;
                        DriverCommision = Math.Round(DriverCommision + OverKmPrice, 2);
                    }
                }
                // Push
                sendNotificationToDriver(orderVendor.TrackNo, orderVendor.Orders.CustomerLocation.Address, orderVendor.Vendors.Address, orderVendor.Vendors.Logo, string.Concat(orderVendor.Vendors.StoreNameAr, " - ", orderVendor.Vendors.StoreNameEn), orderVendor.OrderVendorID, Uow.Drivers.GetById(DriversID).UserId, DriverCommision);
                Uow.Commit();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public void sendNotificationToDriver(string trackNo, string cusotmerAddress, string vendorAddress, string vendorLogo, string vendorName, int orderId, int UserId, decimal Profit)
        {
            try
            {

                #region Send Push
                /// TOKEN
                /// DEVICE TYPE
                var _PushList = new PushList()
                {
                    orderId = orderId,
                    lat = 30606907,
                    lng = 32293909,
                    trackNo = trackNo,
                    profit = "سوف تجني " + Profit + " ريـال",
                    cusotmerAddress = cusotmerAddress,
                    vendorAddress = vendorAddress,
                    vendorLogo = _configuration["VendorImageView"] + vendorLogo, //"https://cdn.made-home.com/Home/Vendor/pF83O4kZaITQklwg5zW9WF3Bty647O2KEc9iU13o.jpg",
                    vendorName = vendorName,
                    body = "New Order - طلب جديد",//For IOS
                    sound = "default", //For IOS
                    title = "New Order - طلب جديد",
                    content_available = "true", //For Android
                    priority = "high", //For Android,
                    serverKey = _configuration["FireBaseKey"],
                    estimation = 20,
                    pushType = (int)PushTypeEnum.Order
                };
                var UserData = _blTokens.GetMobileDataByUserID(UserId);
                if (UserData != null)
                {
                    var res = _bLGeneral.SendPush(UserData.TokenVal, UserData.DeviceType, _PushList);
                }
                #endregion

            }
            catch (Exception ex)
            {
                // send mail
                var message = string.Concat(ex.Message, " // ", ex.InnerException?.Message, " // ", ex.InnerException?.InnerException?.Message);
                _bLGeneral.SendEmail("a.elaf@hotmail.com", "Urgent Push Issues", message);

            }
        }
        public bool AssignOrderVendorToExternalCompany(string[] ListOrderVendorID, int ShippingCompanyID, int CaptainTypeID, int UserId)
        {
            if (Uow.ShippingCompany.GetAll(s => s.ShippingCompanyID == ShippingCompanyID && s.ShippingEnum == (int)ShippingCompanyEnum.Shourq).Any())
            {
                AssignOrderVendorToShrouq(ListOrderVendorID, ShippingCompanyID, CaptainTypeID, UserId);
            }
            else if (Uow.ShippingCompany.GetAll(s => s.ShippingCompanyID == ShippingCompanyID && s.ShippingEnum == (int)ShippingCompanyEnum.Lavender).Any())
            {
                AssigLavender(ListOrderVendorID, ShippingCompanyID, CaptainTypeID, UserId);
            }
            else if (Uow.ShippingCompany.GetAll(s => s.ShippingCompanyID == ShippingCompanyID && s.ShippingEnum == (int)ShippingCompanyEnum.Barq).Any())
            {
                AssigBarq(ListOrderVendorID, ShippingCompanyID, CaptainTypeID, UserId);
            }
            else
            {
                return false;
            }
            return true;
        }
        #endregion
        #region Charge
        public bool UpdateOrderVendorById(int id, string FireBaseKey, string Card)
        {
            try
            {

                var orderdata = Uow.Orders.GetAll(e => e.OrdersID == id, "OrderVendor,Customers,OrderVendor.Vendors").FirstOrDefault();

                foreach (var item in orderdata.OrderVendor)
                {
                    item.UpdateDate = _bLGeneral.GetDateTimeNow();
                    item.IsDeleted = false;
                    item.OrderStatusID = (int)OrderStatusEnum.Create;
                    item.CardType = Card;
                    var Notification = new Notification()
                    {
                        CreateDate = _bLGeneral.GetDateTimeNow(),
                        TitleAR = "تم إرسال الطلب رقم " + item.TrackNo + " من خلال " + orderdata.Customers.FirstName + "",
                        TitleEN = "your order has been send from " + orderdata.Customers.FirstName + "",
                        DescAR = "تم إرسال الطلب رقم " + item.TrackNo + " من خلال " + orderdata.Customers.FirstName + " في تاريخ " + _bLGeneral.GetDateTimeNow() + "",
                        DescEN = "order No " + item.TrackNo + " has been send from " + orderdata.Customers.FirstName + " on date " + _bLGeneral.GetDateTimeNow() + "",
                        IsDeleted = false,
                        IsEnable = true,
                        IsSeen = false,
                        NotificationGuid = Guid.NewGuid(),
                        NotificationTypeID = (int)NotificationTypeEnum.Order,
                        UserId = orderdata.Customers.UserId,
                        NotificationDate = _bLGeneral.GetDateTimeNow(),
                        OrderVendorID = item.OrderVendorID,
                        VendorsID = item.VendorsID
                    };
                    item.Notification.Add(Notification);
                    Uow.OrderVendor.Update(item);
                }
                orderdata.OrderStatusID = (int)OrderStatusEnum.Create;
                Uow.Orders.Update(orderdata);

                Uow.Commit();


                foreach (var item in orderdata.OrderVendor)
                {
                    #region Push
                    var _PushListCusotmer = new PushList()
                    {
                        orderId = item.OrderVendorID,
                        lat = 0,
                        lng = 0,
                        trackNo = item.TrackNo,
                        profit = "",
                        cusotmerAddress = "",
                        vendorAddress = "",
                        vendorLogo = "",
                        vendorName = "",
                        body = "تم إرسال طلبك من متجر " + item.Vendors.StoreNameAr + " اضغط هنا لتفاصيل الطلب",//For IOS
                        sound = "default", //For IOS
                        title = "طلب جديد",
                        content_available = "true", //For Android
                        priority = "high", //For Android,
                        serverKey = FireBaseKey,
                        estimation = 20,
                        pushType = (int)PushTypeEnum.Order
                    };
                    var UserData = _blTokens.GetMobileDataByUserID(orderdata.Customers.UserId);
                    if (UserData != null)
                    {
                        _bLGeneral.SendPush(UserData.TokenVal, UserData.DeviceType, _PushListCusotmer);
                    }
                    var _PushListVendor = new PushList()
                    {
                        orderId = item.OrderVendorID,
                        lat = 0,
                        lng = 0,
                        trackNo = item.TrackNo,
                        profit = "",
                        cusotmerAddress = "",
                        vendorAddress = "",
                        vendorLogo = "",
                        vendorName = "",
                        body = "طلب جديد من الزبون " + orderdata.Customers.FirstName + " فضلا قم بتجهيز الطلب الان",//For IOS
                        sound = "custom_sound.wav", //For IOS
                        title = "طلب جديد",
                        content_available = "true", //For Android
                        priority = "high", //For Android,
                        serverKey = FireBaseKey,
                        estimation = 20,
                        pushType = (int)PushTypeEnum.Order
                    };
                    var UserDataVendor = _blTokens.GetMobileDataByUserID(item.Vendors.UserId);
                    if (UserDataVendor != null)
                    {
                        _bLGeneral.SendPush(UserDataVendor.TokenVal, UserDataVendor.DeviceType, _PushListVendor);
                    }
                    #endregion
                }
                return true;
            }
            catch (Exception ex)
            {
                string message = "UpdateOrderVendorById";
                message += "order ID : " + id;
                message += "<br />";
                message += "<br />";
                message += "Message : " + ex.Message + " <br />" + "InnerException Message : " + ex.InnerException?.Message + " <br />" + "InnerException+1 Message : " + ex.InnerException?.InnerException?.Message;
                message += "<br />";
                message += "<br />";
                // send mail 
                _bLGeneral.SendEmail("a.elaf@hotmail.com", "Urgent Paying Issues", message);

                return false;
            }
        }
        public Orders getOrderData(int id)
        {
            var orderdata = Uow.Orders.GetAll(e => e.OrdersID == id, "OrderVendor,Customers,PromoCode").FirstOrDefault();
            return orderdata;
        }
        public Orders getOrderData(int id, string include)
        {
            var orderdata = Uow.Orders.GetAll(e => e.OrdersID == id, include).FirstOrDefault();
            return orderdata;
        }
        public Orders getOrderData(Guid id, string include)
        {
            var orderdata = Uow.Orders.GetAll(e => e.OrdersGuid == id && e.OrderStatusID == (int)OrderStatusEnum.Pending, include).FirstOrDefault();
            return orderdata;
        }
        public OrderVendor GetOrdersByVendorAndOrderAndPending(int VendorsID, int OrdersID)
        {
            var orderdata = Uow.OrderVendor.GetAll(e => e.OrdersID == OrdersID && e.VendorsID == VendorsID && e.OrderStatusID == (int)OrderStatusEnum.Pending && e.IsDeleted).FirstOrDefault();
            return orderdata;
        }
        public OrderVendor GetOrderVendorByIDAndPending(int OrderVendorID)
        {
            var orderdata = Uow.OrderVendor.GetAll(e => e.OrderVendorID == OrderVendorID && e.OrderStatusID == (int)OrderStatusEnum.Pending && e.IsDeleted).FirstOrDefault();
            return orderdata;
        }
        public OrderItems GetOrderItemsByProductAndOrderAndPending(int ProductID, int OrdersID, string include = "")
        {
            var orderdata = Uow.OrderItems.GetAll(e => e.OrderVendor.OrdersID == OrdersID && e.ProductID == ProductID && e.OrderVendor.Orders.OrderStatusID == (int)OrderStatusEnum.Pending && !e.IsDeleted, include).FirstOrDefault();
            return orderdata;
        }
        public bool IsExistOrderItemsByIDAndOrderVendorAndPending(int OrderItemsID, int OrderVendorID)
        {
            var orderdata = Uow.OrderItems.GetAll(e => e.OrderVendorID == OrderVendorID && e.OrderItemsID != OrderItemsID && e.OrderVendor.Orders.OrderStatusID == (int)OrderStatusEnum.Pending && !e.IsDeleted).Any();
            return orderdata;
        }
        public TabCharge GetTripChargeLog(string tapID)
        {
            return Uow.TabCharge.GetAll(e => e.TapChargeID == tapID, "").FirstOrDefault();
        }
        public TabCharge GetTripChargeLogCustomer(string tapID)
        {
            return Uow.TabCharge.GetAll(e => e.TapChargeID == tapID, "Customers").FirstOrDefault();
        }
        public TabCharge UpdateTripChargeLogWithoutCommit(TabCharge tripChargeLog)
        {
            tripChargeLog = Uow.TabCharge.Update(tripChargeLog);
            return tripChargeLog;
        }
        public bool UpdateTripChargeLog(TabCharge tripChargeLog)
        {
            try
            {
                tripChargeLog = Uow.TabCharge.Update(tripChargeLog);
                Uow.Commit();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public TabChargeExLog InsertTripChargeExLog(TabChargeExLog tripChargeExLog)
        {
            tripChargeExLog = Uow.TabChargeExLog.Insert(tripChargeExLog);
            Uow.Commit();
            return tripChargeExLog;
        }
        public TabCharge InsertTripCharge(TabCharge tripChargeExLog)
        {
            tripChargeExLog = Uow.TabCharge.Insert(tripChargeExLog);
            Uow.Commit();
            return tripChargeExLog;
        }
        public Orders OrderDetails(int orderId)
        {
            var getData = Uow.Orders.GetAll(x => !x.IsDeleted && x.OrdersID == orderId, "").FirstOrDefault();
            return getData;
        }
        #endregion
        #region Shoruq
        public class RootRequest
        {
            public int pickup_id { get; set; }
            public object client_order_id { get; set; }
            public string value { get; set; }
            public int payment_type { get; set; }
            public int preparation_time { get; set; }
            public double lat { get; set; }
            public double lng { get; set; }
            public string customer_phone { get; set; }
            public string customer_name { get; set; }
            public string deliver_at { get; set; }
            public string details { get; set; }
            public int ingr_shop_id { get; set; }
            public string ingr_shop_name { get; set; }
            public string ingr_branch_name { get; set; }
            public int ingr_branch_id { get; set; }
            public double ingr_branch_lat { get; set; }
            public double ingr_branch_lng { get; set; }
            public string ingr_branch_phone { get; set; }
            public string Ingr_logo { get; set; }
        }
        public bool AssignOrderVendorToShrouq(string[] ListOrderVendorID, int ShippingCompanyID, int CaptainTypeID, int UserId)
        {
            try
            {
                foreach (var item in ListOrderVendorID)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        var orderVendorObj = GetOrderVendorById(Convert.ToInt32(item), "Orders.CustomerLocation.Block.City,Orders.Customers,ShippingCompany,Orders.Customers,Vendors");
                        if (orderVendorObj != null)
                        {
                            var client = new RestClient(_configuration["AlshroukCompanyAPI"] + "/order/add");
                            client.Timeout = -1;
                            var request = new RestRequest(Method.POST);
                            request.AddHeader("Content-Type", "application/json");
                            RootRequest rootRequest = new RootRequest
                            {
                                //pickup_lat = (double)orderVendorObj.Orders.CustomerLocation.Lat,
                                // pickup_lng = (double)orderVendorObj.Orders.CustomerLocation.Lng,

                                client_order_id = orderVendorObj.OrderVendorID,
                                value = "",
                                payment_type = 1,
                                preparation_time = 0,
                                lat = (double)orderVendorObj.Orders.CustomerLocation.Lat,
                                lng = (double)orderVendorObj.Orders.CustomerLocation.Lng,
                                //address = orderVendorObj.Orders.CustomerLocation.Address,
                                //city = orderVendorObj.Orders.CustomerLocation.Block.City.NameEN,
                                customer_phone = orderVendorObj.Orders.Customers.MobileNo,
                                customer_name = orderVendorObj.Orders.Customers.FirstName,
                                deliver_at = "",
                                details = "",
                                //pickup_poa = "",
                                //dropoff_poa = ""

                            };
                            var body = "";
                            //if (string.IsNullOrWhiteSpace(orderVendorObj.Vendors.PickupId))
                            //{
                            rootRequest.ingr_branch_id = orderVendorObj.VendorsID;
                            rootRequest.ingr_shop_id = orderVendorObj.VendorsID;
                            rootRequest.ingr_shop_name = orderVendorObj.Vendors.StoreNameEn;
                            rootRequest.ingr_branch_name = orderVendorObj.Vendors.StoreNameEn;
                            rootRequest.ingr_branch_lat = orderVendorObj.Vendors.Lat;
                            rootRequest.ingr_branch_lng = orderVendorObj.Vendors.Lng;
                            rootRequest.ingr_branch_phone = orderVendorObj.Vendors.MobileNo;
                            rootRequest.Ingr_logo = "https://cdn.made-home.com/Home/Vendor/" + orderVendorObj.Vendors.Logo + "";
                            body = Newtonsoft.Json.JsonConvert.SerializeObject(rootRequest).Replace("\"pickup_id\":0,", "");

                            //}
                            //else
                            //{
                            //    rootRequest.pickup_id = int.Parse(orderVendorObj.Vendors.PickupId);
                            //    body = Newtonsoft.Json.JsonConvert.SerializeObject(rootRequest);

                            //}




                            request.AddParameter("application/json", body, ParameterType.RequestBody);
                            IRestResponse response = client.Execute(request);
                            Root myDeserializedRoot = JsonConvert.DeserializeObject<Root>(response.Content);
                            if (myDeserializedRoot.status_id == 1)
                            {
                                var orderVendor = GetOrderVendorById(Convert.ToInt32(item));
                                if (orderVendor != null)
                                {
                                    var DeliveryPrice = Uow.ShippingCompany.GetAll(e => e.ShippingCompanyID == ShippingCompanyID && e.ShippingEnum == (int)ShippingCompanyEnum.Shourq).FirstOrDefault().DeliveryPrice;
                                    orderVendor.UpdateDate = _bLGeneral.GetDateTimeNow();
                                    orderVendor.UserUpdate = UserId;
                                    orderVendor.CaptainTypeId = CaptainTypeID;
                                    orderVendor.ShippingCompanyID = ShippingCompanyID;
                                    orderVendor.ShippingCompanyPrice = DeliveryPrice;//orderVendorObj.ShippingCompany.DeliveryPrice;
                                    orderVendor.OrderStatusID = (int)OrderStatusEnum.Assign;
                                    orderVendor.IntegrationOrderId = myDeserializedRoot.order_id.ToString();
                                    //orderVendor.Vendors.PickupId = myDeserializedRoot.branch_id.ToString();

                                    Uow.OrderVendor.Update(orderVendor);

                                    OrderHistory orderHistory = new OrderHistory()
                                    {
                                        CreateDate = _bLGeneral.GetDateTimeNow(),
                                        IsDeleted = false,
                                        IsEnable = true,
                                        OrderHistoryGuid = Guid.NewGuid(),
                                        OrderStatusID = (int)OrderStatusEnum.Assign,
                                        OrderVendorID = orderVendor.OrderVendorID,
                                        UserId = UserId,
                                        Lat = 0,
                                        Lng = 0,
                                        CancelReason = string.Empty,
                                    };
                                    Uow.OrderHistory.Insert(orderHistory);
                                }
                            }
                            else
                            {
                                // errorList.Add(orderVendorObj.OrderVendorID);
                            }
                        }

                    }
                }
                Uow.Commit();
                return true;
            }
            catch (Exception ex)
            {
                string messakkge = "( Shrouq order Add Ex)";
                messakkge += "<br />";
                messakkge += "<br />";
                messakkge += "Message : " + ex.Message;
                _bLGeneral.SendEmail("a.elaf@hotmail.com", "Shrouq order Updated", messakkge);
                _bLGeneral.SendEmail("loaymamdouh56@hotmail.com", "Shrouq order Updated", messakkge);
                return false;
            }
        }
        public bool AssignOrderVendorToShrouq(int ListOrderVendorID, int ShippingCompanyID, int CaptainTypeID, int UserId)
        {
            try
            {
                var orderVendorObj = GetOrderVendorById(ListOrderVendorID, "Orders.CustomerLocation.Block.City,Orders.Customers,ShippingCompany,Orders.Customers,Vendors");
                if (orderVendorObj != null)
                {
                    var client = new RestClient(_configuration["AlshroukCompanyAPI"] + "/order/add");
                    client.Timeout = -1;
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Content-Type", "application/json");
                    RootRequest rootRequest = new RootRequest
                    {
                        //pickup_lat = (double)orderVendorObj.Orders.CustomerLocation.Lat,
                        // pickup_lng = (double)orderVendorObj.Orders.CustomerLocation.Lng,

                        client_order_id = orderVendorObj.OrderVendorID,
                        value = "",
                        payment_type = 1,
                        preparation_time = 0,
                        lat = (double)orderVendorObj.Orders.CustomerLocation.Lat,
                        lng = (double)orderVendorObj.Orders.CustomerLocation.Lng,
                        //address = orderVendorObj.Orders.CustomerLocation.Address,
                        //city = orderVendorObj.Orders.CustomerLocation.Block.City.NameEN,
                        customer_phone = orderVendorObj.Orders.Customers.MobileNo,
                        customer_name = orderVendorObj.Orders.Customers.FirstName,
                        deliver_at = "",
                        details = "",
                        //pickup_poa = "",
                        //dropoff_poa = ""

                    };
                    var body = "";
                    //if (string.IsNullOrWhiteSpace(orderVendorObj.Vendors.PickupId))
                    //{
                    rootRequest.ingr_branch_id = orderVendorObj.VendorsID;
                    rootRequest.ingr_shop_id = orderVendorObj.VendorsID;
                    rootRequest.ingr_shop_name = orderVendorObj.Vendors.StoreNameEn;
                    rootRequest.ingr_branch_name = orderVendorObj.Vendors.StoreNameEn;
                    rootRequest.ingr_branch_lat = orderVendorObj.Vendors.Lat;
                    rootRequest.ingr_branch_lng = orderVendorObj.Vendors.Lng;
                    rootRequest.ingr_branch_phone = orderVendorObj.Vendors.MobileNo;
                    rootRequest.Ingr_logo = "https://cdn.made-home.com/Home/Vendor/" + orderVendorObj.Vendors.Logo + "";
                    body = Newtonsoft.Json.JsonConvert.SerializeObject(rootRequest).Replace("\"pickup_id\":0,", "");

                    //}
                    //else
                    //{
                    //    rootRequest.pickup_id = int.Parse(orderVendorObj.Vendors.PickupId);
                    //    body = Newtonsoft.Json.JsonConvert.SerializeObject(rootRequest);

                    //}




                    request.AddParameter("application/json", body, ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);
                    Root myDeserializedRoot = JsonConvert.DeserializeObject<Root>(response.Content);
                    if (myDeserializedRoot.status_id == 1)
                    {
                        var orderVendor = GetOrderVendorById(ListOrderVendorID);
                        if (orderVendor != null)
                        {
                            var DeliveryPrice = Uow.ShippingCompany.GetAll(e => e.ShippingCompanyID == ShippingCompanyID && e.ShippingEnum == (int)ShippingCompanyEnum.Shourq).FirstOrDefault().DeliveryPrice;
                            orderVendor.UpdateDate = _bLGeneral.GetDateTimeNow();
                            orderVendor.UserUpdate = UserId;
                            orderVendor.CaptainTypeId = CaptainTypeID;
                            orderVendor.ShippingCompanyID = ShippingCompanyID;
                            orderVendor.ShippingCompanyPrice = DeliveryPrice;//orderVendorObj.ShippingCompany.DeliveryPrice;
                            orderVendor.OrderStatusID = (int)OrderStatusEnum.Assign;
                            orderVendor.IntegrationOrderId = myDeserializedRoot.order_id.ToString();
                            //orderVendor.Vendors.PickupId = myDeserializedRoot.branch_id.ToString();

                            Uow.OrderVendor.Update(orderVendor);

                            OrderHistory orderHistory = new OrderHistory()
                            {
                                CreateDate = _bLGeneral.GetDateTimeNow(),
                                IsDeleted = false,
                                IsEnable = true,
                                OrderHistoryGuid = Guid.NewGuid(),
                                OrderStatusID = (int)OrderStatusEnum.Assign,
                                OrderVendorID = orderVendor.OrderVendorID,
                                UserId = UserId,
                                Lat = 0,
                                Lng = 0,
                                CancelReason = string.Empty,
                            };
                            Uow.OrderHistory.Insert(orderHistory);
                        }
                    }
                    else
                    {
                        // errorList.Add(orderVendorObj.OrderVendorID);
                    }
                }
                Uow.Commit();
                return true;
            }
            catch (Exception ex)
            {
                string messakkge = "( Shrouq order Add Ex)";
                messakkge += "<br />";
                messakkge += "<br />";
                messakkge += "Message : " + ex.Message;
                _bLGeneral.SendEmail("a.elaf@hotmail.com", "Shrouq order Updated", messakkge);
                _bLGeneral.SendEmail("loaymamdouh56@hotmail.com", "Shrouq order Updated", messakkge);
                return false;
            }
        }
        public bool CancelShoruq(int OrderId, int UserId)
        {
            try
            {
                var OrderData = Uow.OrderVendor.GetAll(s => s.OrderVendorID == OrderId).FirstOrDefault();
                var client = new RestClient(_configuration["AlshroukCompanyAPI"] + "/order/cancel/" + OrderData.IntegrationOrderId);
                var request = new RestRequest(Method.GET);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", _configuration["BarqBearer"]);
                IRestResponse response = client.Execute(request);
                var ModelJson = JsonConvert.SerializeObject(response.Content);
                if (!string.IsNullOrEmpty(response.Content))
                {
                    OrderData.ShipCompanyHistory.Add(new ShipCompanyHistory()
                    {
                        ShippingCompanyID = (int)OrderData.ShippingCompanyID,
                        ShippingStatusId = (int)OrderStatusEnum.Cancel,
                        Response = ModelJson,
                        CreateDate = _bLGeneral.GetDateTimeNow(),
                        ResponseStatusId = "10",
                        ShipCompanyHistoryGuid = Guid.NewGuid(),
                        ResponseStatusName = "Order cancelled",
                        DriverName = "",
                        DriverPhone = "",
                    });
                    return true;
                }
                else
                {
                    OrderData.ShipCompanyHistory.Add(new ShipCompanyHistory()
                    {
                        ShippingCompanyID = (int)OrderData.ShippingCompanyID,
                        ShippingStatusId = (int)OrderStatusEnum.Cancel,
                        Response = ModelJson,
                        CreateDate = _bLGeneral.GetDateTimeNow(),
                        ResponseStatusId = "",
                        ShipCompanyHistoryGuid = Guid.NewGuid(),
                        ResponseStatusName = "",
                        DriverName = "",
                        DriverPhone = "",
                    });
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion
        #region Lavender
        [Obsolete]
        public bool AssigLavender(string[] ListOrderVendorID, int ShippingCompanyID, int CaptainTypeID, int UserId)
        {
            try
            {
                foreach (var item in ListOrderVendorID)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        var orderVendorObj = GetOrderVendorById(Convert.ToInt32(item), "Orders.CustomerLocation.Block.City,Orders.Customers,ShippingCompany,Orders.Customers,Vendors");
                        if (orderVendorObj != null)
                        {
                            var client = new RestClient(_configuration["LavenderCreate"]);
                            var request = new RestRequest(Method.POST);
                            request.AddHeader("Content-Type", "application/json");
                            request.AddHeader("Authorization", "Bearer " + _configuration["BaererLavender"]);
                            var rootRequest = new LavenderCreate
                            {
                                RefOrderNo = orderVendorObj.OrderVendorID.ToString(),
                                CustomerName = orderVendorObj.Orders.Customers.FirstName,
                                CustomerPhone = orderVendorObj.Orders.Customers.MobileNo,
                                DestinationAddress = orderVendorObj.Orders.CustomerLocation.Address,
                                DestinationLat = orderVendorObj.Orders.CustomerLocation.Lat.ToString(),
                                DestinationLong = orderVendorObj.Orders.CustomerLocation.Lng.ToString(),
                                LogoStore = !string.IsNullOrWhiteSpace(orderVendorObj.Vendors.Logo) ? _configuration["VendorImageView"] + orderVendorObj.Vendors.Logo + "" : "",
                                PickupAddress = orderVendorObj.Vendors.Address,
                                PickupLat = orderVendorObj.Vendors.Lat.ToString(),
                                PickupLong = orderVendorObj.Vendors.Lng.ToString(),
                                StoreName = orderVendorObj.Vendors.StoreNameEn,
                                StorePhone = orderVendorObj.Vendors.MobileNo
                            };
                            var body = Newtonsoft.Json.JsonConvert.SerializeObject(rootRequest);
                            request.AddJsonBody(body);
                            IRestResponse response = client.Execute(request);
                            var myDeserializedRoot = JsonConvert.DeserializeObject<LavenderResp0nse>(response.Content);
                            if (myDeserializedRoot.status_code == 11)
                            {
                                var orderVendor = GetOrderVendorById(Convert.ToInt32(item));
                                if (orderVendor != null)
                                {
                                    var DeliveryPrice = Uow.ShippingCompany.GetAll(e => e.ShippingCompanyID == ShippingCompanyID && e.ShippingEnum == (int)ShippingCompanyEnum.Lavender).FirstOrDefault().DeliveryPrice;
                                    orderVendor.UpdateDate = _bLGeneral.GetDateTimeNow();
                                    orderVendor.UserUpdate = UserId;
                                    orderVendor.CaptainTypeId = CaptainTypeID;
                                    orderVendor.ShippingCompanyID = ShippingCompanyID;
                                    orderVendor.ShippingCompanyPrice = DeliveryPrice;
                                    orderVendor.OrderStatusID = (int)OrderStatusEnum.Assign;
                                    orderVendor.IntegrationOrderId = myDeserializedRoot.lavender_order_id.ToString();
                                    Uow.OrderVendor.Update(orderVendor);
                                    OrderHistory orderHistory = new OrderHistory()
                                    {
                                        CreateDate = _bLGeneral.GetDateTimeNow(),
                                        IsDeleted = false,
                                        IsEnable = true,
                                        OrderHistoryGuid = Guid.NewGuid(),
                                        OrderStatusID = (int)OrderStatusEnum.Assign,
                                        OrderVendorID = orderVendor.OrderVendorID,
                                        UserId = UserId,
                                        Lat = 0,
                                        Lng = 0,
                                        CancelReason = string.Empty,
                                    };
                                    Uow.OrderHistory.Insert(orderHistory);
                                }
                            }
                            else
                            {
                                // errorList.Add(orderVendorObj.OrderVendorID);
                            }
                        }

                    }
                }
                Uow.Commit();
                return true;
            }
            catch (Exception ex)
            {
                string messakkge = "(Lavender order Add Ex)";
                messakkge += "<br />";
                messakkge += "<br />";
                messakkge += "Message : " + ex.Message;
                _bLGeneral.SendEmail("loaymamdouh56@hotmail.com", "Lavender order Create", messakkge);
                return false;
            }
        }
        [Obsolete]
        public bool AssigLavender(int ListOrderVendorID, int ShippingCompanyID, int CaptainTypeID, int UserId)
        {
            try
            {
                var orderVendorObj = GetOrderVendorById(ListOrderVendorID, "Orders.CustomerLocation.Block.City,Orders.Customers,ShippingCompany,Orders.Customers,Vendors");
                if (orderVendorObj != null)
                {
                    var client = new RestClient(_configuration["LavenderCreate"]);
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Content-Type", "application/json");
                    request.AddHeader("Authorization", "Bearer " + _configuration["BaererLavender"]);
                    var rootRequest = new LavenderCreate
                    {
                        RefOrderNo = orderVendorObj.OrderVendorID.ToString(),
                        CustomerName = orderVendorObj.Orders.Customers.FirstName,
                        CustomerPhone = orderVendorObj.Orders.Customers.MobileNo,
                        DestinationAddress = orderVendorObj.Orders.CustomerLocation.Address,
                        DestinationLat = orderVendorObj.Orders.CustomerLocation.Lat.ToString(),
                        DestinationLong = orderVendorObj.Orders.CustomerLocation.Lng.ToString(),
                        LogoStore = !string.IsNullOrWhiteSpace(orderVendorObj.Vendors.Logo) ? _configuration["VendorImageView"] + orderVendorObj.Vendors.Logo + "" : "",
                        PickupAddress = orderVendorObj.Vendors.Address,
                        PickupLat = orderVendorObj.Vendors.Lat.ToString(),
                        PickupLong = orderVendorObj.Vendors.Lng.ToString(),
                        StoreName = orderVendorObj.Vendors.StoreNameEn,
                        StorePhone = orderVendorObj.Vendors.MobileNo
                    };
                    var body = Newtonsoft.Json.JsonConvert.SerializeObject(rootRequest);
                    request.AddJsonBody(body);
                    IRestResponse response = client.Execute(request);
                    var myDeserializedRoot = JsonConvert.DeserializeObject<LavenderResp0nse>(response.Content);
                    if (myDeserializedRoot.status_code == 11)
                    {
                        var orderVendor = GetOrderVendorById(ListOrderVendorID);
                        if (orderVendor != null)
                        {
                            var DeliveryPrice = Uow.ShippingCompany.GetAll(e => e.ShippingCompanyID == ShippingCompanyID && e.ShippingEnum == (int)ShippingCompanyEnum.Lavender).FirstOrDefault().DeliveryPrice;
                            orderVendor.UpdateDate = _bLGeneral.GetDateTimeNow();
                            orderVendor.UserUpdate = UserId;
                            orderVendor.CaptainTypeId = CaptainTypeID;
                            orderVendor.ShippingCompanyID = ShippingCompanyID;
                            orderVendor.ShippingCompanyPrice = DeliveryPrice;
                            orderVendor.OrderStatusID = (int)OrderStatusEnum.Assign;
                            orderVendor.IntegrationOrderId = myDeserializedRoot.lavender_order_id.ToString();
                            Uow.OrderVendor.Update(orderVendor);
                            OrderHistory orderHistory = new OrderHistory()
                            {
                                CreateDate = _bLGeneral.GetDateTimeNow(),
                                IsDeleted = false,
                                IsEnable = true,
                                OrderHistoryGuid = Guid.NewGuid(),
                                OrderStatusID = (int)OrderStatusEnum.Assign,
                                OrderVendorID = orderVendor.OrderVendorID,
                                UserId = UserId,
                                Lat = 0,
                                Lng = 0,
                                CancelReason = string.Empty,
                            };
                            Uow.OrderHistory.Insert(orderHistory);
                        }
                    }
                    else
                    {
                        // errorList.Add(orderVendorObj.OrderVendorID);
                    }
                }
                Uow.Commit();
                return true;
            }
            catch (Exception ex)
            {
                string messakkge = "(Lavender order Add Ex)";
                messakkge += "<br />";
                messakkge += "<br />";
                messakkge += "Message : " + ex.Message;
                _bLGeneral.SendEmail("loaymamdouh56@hotmail.com", "Lavender order Create", messakkge);
                return false;
            }
        }
        public bool CancelLavender(int OrderId, int UserId)
        {
            try
            {
                var OrderData = Uow.OrderVendor.GetAll(s => s.OrderVendorID == OrderId).FirstOrDefault();
                var client = new RestClient(_configuration["LavenderCancel"] + OrderData.OrderVendorID);
                var request = new RestRequest(Method.DELETE);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", "Bearer " + _configuration["BaererLavender"]);
                IRestResponse response = client.Execute(request);
                var myDeserializedRoot = JsonConvert.DeserializeObject<LavenderResp0nse>(response.Content);
                var ModelJson = JsonConvert.SerializeObject(response.Content);
                if (myDeserializedRoot.status_code == 16)
                {
                    OrderData.ShipCompanyHistory.Add(new ShipCompanyHistory()
                    {
                        ShippingCompanyID = (int)OrderData.ShippingCompanyID,
                        ShippingStatusId = (int)OrderStatusEnum.Cancel,
                        Response = ModelJson,
                        CreateDate = _bLGeneral.GetDateTimeNow(),
                        ResponseStatusId = "16",
                        ShipCompanyHistoryGuid = Guid.NewGuid(),
                        ResponseStatusName = "Order Cancelled",
                        DriverName = "",
                        DriverPhone = "",
                    });
                }
                else
                {
                    OrderData.ShipCompanyHistory.Add(new ShipCompanyHistory()
                    {
                        ShippingCompanyID = (int)OrderData.ShippingCompanyID,
                        ShippingStatusId = (int)OrderStatusEnum.Cancel,
                        Response = ModelJson,
                        CreateDate = _bLGeneral.GetDateTimeNow(),
                        ResponseStatusId = "",
                        ShipCompanyHistoryGuid = Guid.NewGuid(),
                        ResponseStatusName = "",
                        DriverName = "",
                        DriverPhone = "",
                    });
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion
        #region Barq
        [Obsolete]
        public bool AssigBarq(string[] ListOrderVendorID, int ShippingCompanyID, int CaptainTypeID, int UserId)
        {
            try
            {
                foreach (var item in ListOrderVendorID)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        var orderVendorObj = GetOrderVendorById(Convert.ToInt32(item), "Orders.CustomerLocation.Block.City,Orders.Customers,ShippingCompany,Orders.Customers,Vendors,OrderItems.Product");
                        if (orderVendorObj != null)
                        {
                            var client = new RestClient(_configuration["BarqUrl"] + _configuration["BarqOrder"]);
                            var request = new RestRequest(Method.POST);
                            request.AddHeader("Content-Type", "application/json");
                            request.AddHeader("Authorization", _configuration["BarqBearer"]);
                            var rootRequest = new BarqCreate();
                            rootRequest.destination = new Destination()
                            {
                                latitude = (double)orderVendorObj.Orders.CustomerLocation.Lat,
                                longitude = (double)orderVendorObj.Orders.CustomerLocation.Lng
                            };
                            rootRequest.customer_details = new CustomerDetails()
                            {
                                address = orderVendorObj.Orders.CustomerLocation.Address,
                                city = orderVendorObj.Orders.CustomerLocation.Block.City.NameEN,
                                country = "Saudi Arabia",
                                first_name = orderVendorObj.Orders.Customers.FirstName,
                                last_name = orderVendorObj.Orders.Customers.SeconedName,
                                mobile = orderVendorObj.Orders.Customers.MobileNo
                            };
                            rootRequest.hub_code = _configuration["BarqHubCode"];
                            rootRequest.hub_id = Convert.ToInt32(_configuration["BarqHubId"]);
                            rootRequest.invoice_total = orderVendorObj.Total.ToString() + " SAR";
                            rootRequest.merchant_order_id = orderVendorObj.OrderVendorID.ToString();
                            rootRequest.payment_type = 0; //Paid Online
                            rootRequest.shipment_type = 0; //Instant Delivery
                            rootRequest.products = orderVendorObj.OrderItems.Where(x => !x.IsDeleted).Select(m => new ProductBarq()
                            {
                                brand = "",
                                name = m.ProdNameEn,
                                color = "",
                                price = (double)m.Price,
                                qty = Convert.ToInt32(m.Quantity),
                                serial_no = m.ProductID.ToString(),
                                sku = "",
                                weight_kg = (double)m.Product.Weight
                            }).ToList();
                            var body = Newtonsoft.Json.JsonConvert.SerializeObject(rootRequest);
                            request.AddJsonBody(body);
                            IRestResponse response = client.Execute(request);
                            var myDeserializedRoot = JsonConvert.DeserializeObject<BarqResponse>(response.Content);
                            if (myDeserializedRoot.id != 0)
                            {
                                var clieant = new RestClient(_configuration["BarqUrl"] + _configuration["BarqOrder"] + "/" + myDeserializedRoot.id.ToString() + "/ready_dispatch_order");
                                var requesst = new RestRequest(Method.POST);
                                request.AddHeader("Authorization", _configuration["BarqBearer"]);
                                IRestResponse sss = clieant.Execute(requesst);
                                var orderVendor = GetOrderVendorById(Convert.ToInt32(item));
                                if (orderVendor != null)
                                {
                                    var DeliveryPrice = Uow.ShippingCompany.GetAll(e => e.ShippingCompanyID == ShippingCompanyID && e.ShippingEnum == (int)ShippingCompanyEnum.Barq).FirstOrDefault().DeliveryPrice;
                                    orderVendor.UpdateDate = _bLGeneral.GetDateTimeNow();
                                    orderVendor.UserUpdate = UserId;
                                    orderVendor.CaptainTypeId = CaptainTypeID;
                                    orderVendor.ShippingCompanyID = ShippingCompanyID;
                                    orderVendor.ShippingCompanyPrice = DeliveryPrice;
                                    orderVendor.OrderStatusID = (int)OrderStatusEnum.Assign;
                                    orderVendor.IntegrationOrderId = myDeserializedRoot.id.ToString();
                                    Uow.OrderVendor.Update(orderVendor);
                                    OrderHistory orderHistory = new OrderHistory()
                                    {
                                        CreateDate = _bLGeneral.GetDateTimeNow(),
                                        IsDeleted = false,
                                        IsEnable = true,
                                        OrderHistoryGuid = Guid.NewGuid(),
                                        OrderStatusID = (int)OrderStatusEnum.Assign,
                                        OrderVendorID = orderVendor.OrderVendorID,
                                        UserId = UserId,
                                        Lat = 0,
                                        Lng = 0,
                                        CancelReason = string.Empty,
                                    };
                                    Uow.OrderHistory.Insert(orderHistory);
                                }
                            }
                            else
                            {
                                // errorList.Add(orderVendorObj.OrderVendorID);
                            }
                        }

                    }
                }
                Uow.Commit();
                return true;
            }
            catch (Exception ex)
            {
                string messakkge = "(Barq order Add Ex)";
                messakkge += "<br />";
                messakkge += "<br />";
                messakkge += "Message : " + ex.Message;
                _bLGeneral.SendEmail("loaymamdouh56@hotmail.com", "Barq order Create", messakkge);
                return false;
            }
        }
        [Obsolete]
        public bool AssigBarq(int ListOrderVendorID, int ShippingCompanyID, int CaptainTypeID, int UserId)
        {
            try
            {

                var orderVendorObj = GetOrderVendorById(ListOrderVendorID, "Orders.CustomerLocation.Block.City,Orders.Customers,ShippingCompany,Orders.Customers,Vendors,OrderItems.Product");
                if (orderVendorObj != null)
                {
                    var client = new RestClient(_configuration["BarqUrl"] + _configuration["BarqOrder"]);
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Content-Type", "application/json");
                    request.AddHeader("Authorization", _configuration["BarqBearer"]);
                    var rootRequest = new BarqCreate();
                    rootRequest.destination = new Destination()
                    {
                        latitude = (double)orderVendorObj.Orders.CustomerLocation.Lat,
                        longitude = (double)orderVendorObj.Orders.CustomerLocation.Lng
                    };
                    rootRequest.customer_details = new CustomerDetails()
                    {
                        address = orderVendorObj.Orders.CustomerLocation.Address,
                        city = orderVendorObj.Orders.CustomerLocation.Block.City.NameEN,
                        country = "Saudi Arabia",
                        first_name = orderVendorObj.Orders.Customers.FirstName,
                        last_name = orderVendorObj.Orders.Customers.SeconedName,
                        mobile = orderVendorObj.Orders.Customers.MobileNo
                    };
                    rootRequest.hub_code = _configuration["BarqHubCode"];
                    rootRequest.hub_id = Convert.ToInt32(_configuration["BarqHubId"]);
                    rootRequest.invoice_total = orderVendorObj.Total.ToString() + " SAR";
                    rootRequest.merchant_order_id = orderVendorObj.OrderVendorID.ToString();
                    rootRequest.payment_type = 0; //Paid Online
                    rootRequest.shipment_type = 0; //Instant Delivery
                    rootRequest.products = orderVendorObj.OrderItems.Where(x => !x.IsDeleted).Select(m => new ProductBarq()
                    {
                        brand = "",
                        name = m.ProdNameEn,
                        color = "",
                        price = (double)m.Price,
                        qty = Convert.ToInt32(m.Quantity),
                        serial_no = m.ProductID.ToString(),
                        sku = "",
                        weight_kg = (double)m.Product.Weight
                    }).ToList();
                    var body = Newtonsoft.Json.JsonConvert.SerializeObject(rootRequest);
                    request.AddJsonBody(body);
                    IRestResponse response = client.Execute(request);
                    var myDeserializedRoot = JsonConvert.DeserializeObject<BarqResponse>(response.Content);
                    if (myDeserializedRoot.id != 0)
                    {
                        var clieant = new RestClient(_configuration["BarqUrl"] + _configuration["BarqOrder"] + "/" + myDeserializedRoot.id.ToString() + "/ready_dispatch_order");
                        var requesst = new RestRequest(Method.POST);
                        request.AddHeader("Authorization", _configuration["BarqBearer"]);
                        IRestResponse sss = clieant.Execute(requesst);
                        var orderVendor = GetOrderVendorById(ListOrderVendorID);
                        if (orderVendor != null)
                        {
                            var DeliveryPrice = Uow.ShippingCompany.GetAll(e => e.ShippingCompanyID == ShippingCompanyID && e.ShippingEnum == (int)ShippingCompanyEnum.Barq).FirstOrDefault().DeliveryPrice;
                            orderVendor.UpdateDate = _bLGeneral.GetDateTimeNow();
                            orderVendor.UserUpdate = UserId;
                            orderVendor.CaptainTypeId = CaptainTypeID;
                            orderVendor.ShippingCompanyID = ShippingCompanyID;
                            orderVendor.ShippingCompanyPrice = DeliveryPrice;
                            orderVendor.OrderStatusID = (int)OrderStatusEnum.Assign;
                            orderVendor.IntegrationOrderId = myDeserializedRoot.id.ToString();
                            Uow.OrderVendor.Update(orderVendor);
                            OrderHistory orderHistory = new OrderHistory()
                            {
                                CreateDate = _bLGeneral.GetDateTimeNow(),
                                IsDeleted = false,
                                IsEnable = true,
                                OrderHistoryGuid = Guid.NewGuid(),
                                OrderStatusID = (int)OrderStatusEnum.Assign,
                                OrderVendorID = orderVendor.OrderVendorID,
                                UserId = UserId,
                                Lat = 0,
                                Lng = 0,
                                CancelReason = string.Empty,
                            };
                            Uow.OrderHistory.Insert(orderHistory);
                        }
                    }
                    else
                    {
                        // errorList.Add(orderVendorObj.OrderVendorID);
                    }
                }
                Uow.Commit();
                return true;
            }
            catch (Exception ex)
            {
                string messakkge = "(Barq order Add Ex)";
                messakkge += "<br />";
                messakkge += "<br />";
                messakkge += "Message : " + ex.Message;
                _bLGeneral.SendEmail("loaymamdouh56@hotmail.com", "Barq order Create", messakkge);
                return false;
            }
        }
        public bool CancelBarq(int OrderId, int UserId)
        {
            try
            {
                var OrderData = Uow.OrderVendor.GetAll(s => s.OrderVendorID == OrderId).FirstOrDefault();
                var client = new RestClient(_configuration["BarqUrl"] + _configuration["BarqOrder"] + "/" + OrderData.IntegrationOrderId + "/cancellation");
                var request = new RestRequest(Method.GET);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", _configuration["BarqBearer"]);
                IRestResponse response = client.Execute(request);
                var myDeserializedRoot = JsonConvert.DeserializeObject<BarqCancelResponse>(response.Content);
                var ModelJson = JsonConvert.SerializeObject(response.Content);
                if (myDeserializedRoot.code == 200)
                {
                    OrderData.ShipCompanyHistory.Add(new ShipCompanyHistory()
                    {
                        ShippingCompanyID = (int)OrderData.ShippingCompanyID,
                        ShippingStatusId = (int)OrderStatusEnum.Cancel,
                        Response = ModelJson,
                        CreateDate = _bLGeneral.GetDateTimeNow(),
                        ResponseStatusId = "8",
                        ShipCompanyHistoryGuid = Guid.NewGuid(),
                        ResponseStatusName = "Order Cancelled",
                        DriverName = "",
                        DriverPhone = "",
                    });
                }
                else
                {
                    OrderData.ShipCompanyHistory.Add(new ShipCompanyHistory()
                    {
                        ShippingCompanyID = (int)OrderData.ShippingCompanyID,
                        ShippingStatusId = (int)OrderStatusEnum.Cancel,
                        Response = ModelJson,
                        CreateDate = _bLGeneral.GetDateTimeNow(),
                        ResponseStatusId = "",
                        ShipCompanyHistoryGuid = Guid.NewGuid(),
                        ResponseStatusName = "",
                        DriverName = "",
                        DriverPhone = "",
                    });
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion
    }
}
