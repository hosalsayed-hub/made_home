using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Homemade.BLL.General;
using Homemade.BLL.Setting;
using Homemade.BLL.ViewModel.Driver;
using Homemade.BLL.ViewModel.Order;
using Homemade.BLL.ViewModel.Setting;
using Homemade.DAL.Contract;
using Homemade.Model.Order;
using Homemade.Model.Setting;

namespace Homemade.BLL.Order
{
    public class BlOrderRate
    {
        #region Decleration
        private readonly BLGeneral _blGeneral;
        private readonly IUOW Uow;
        private readonly BlTokens _blTokens;
        public BlOrderRate(IUOW _uow, BLGeneral blGeneral, BlTokens blTokens)
        {
            _blGeneral = blGeneral;
            this.Uow = _uow;
            _blTokens = blTokens;
        }
        #endregion
        #region Helpers
        public OrderRate GetById(int id) => Uow.OrderRate.GetAll(x => x.OrderRateID == id).FirstOrDefault();
        public IQueryable<OrderRateViewModel> GetAllOrderRateViewModelRepley(int VendorsID, string customerPath, string vendorPath, string lang)
        {
            var data = Uow.OrderRate.GetAll(s => !s.IsDeleted && s.OrderVendor.VendorsID == VendorsID && s.IsRepley,
                       "OrderVendor.Orders.Customers").Select(m => new OrderRateViewModel()
                       {
                           CustomerName = m.OrderVendor.Orders.Customers.FirstName + " " + m.OrderVendor.Orders.Customers.SeconedName,
                           RateDate = m.CreateDate.ToString("dd-MM-yyyy hh:mm tt"),
                           OrderRateID = m.OrderRateID,
                           RateOrder = m.RateOrder,
                           IsRepley = m.IsRepley,
                           TrackNo = m.OrderVendor.TrackNo,
                           CommentOrder = m.CommentOrder,
                           RateTitle = lang == "ar" ? (m.RateOrder <= 1 ? "سيء جدا" : m.RateOrder <= 2 ? "سيء" : m.RateOrder <= 3 ? "مقبول" : m.RateOrder <= 4 ? "ممتاز" : "عالى الجودة") :
                           (m.RateOrder <= 1 ? "Very bad" : m.RateOrder <= 2 ? "Bad" : m.RateOrder <= 3 ? "Acceptable" : m.RateOrder <= 4 ? "Excellent" : "High quality"),
                           OrderRateGuid = m.OrderRateGuid,
                           AnswerRate = m.AnswerRate,
                           CommentDelivery = m.CommentDelivery,
                           OrderVendorID = m.OrderVendorID,
                           RateDelivery = m.RateDelivery,
                           VendorName = lang == "ar" ? m.OrderVendor.Vendors.StoreNameAr : m.OrderVendor.Vendors.StoreNameEn,
                           CustomerImage = !string.IsNullOrWhiteSpace(m.OrderVendor.Orders.Customers.ProfilePic) ? (customerPath + m.OrderVendor.Orders.Customers.ProfilePic) : "/assets/homeMadeSite/img/logo.svg",
                           VendorImage = !string.IsNullOrWhiteSpace(m.OrderVendor.Vendors.ProfilePic) ? (vendorPath + m.OrderVendor.Vendors.ProfilePic) : "/assets/homeMadeSite/img/logo.svg",
                           CreateDate = m.CreateDate,
                           CustomersGuid = m.OrderVendor.Orders.Customers.CustomersGuid,
                           VendorsGuid = m.OrderVendor.Vendors.VendorsGuid,
                       }).OrderByDescending(x => x.CreateDate);
            return data;
        }
        public OrderRateViewModel GetOrderRateViewModelRepley(int OrderVendorsID, string customerPath, string vendorPath, string lang)
        {
            var data = Uow.OrderRate.GetAll(s => !s.IsDeleted && s.OrderVendorID == OrderVendorsID/* && s.IsRepley*/,
                       "OrderVendor.Orders.Customers").Select(m => new OrderRateViewModel()
                       {
                           CustomerName = m.OrderVendor.Orders.Customers.FirstName + " " + m.OrderVendor.Orders.Customers.SeconedName,
                           RateDate = m.CreateDate.ToString("dd-MM-yyyy hh:mm tt"),
                           OrderRateID = m.OrderRateID,
                           RateOrder = m.RateOrder,
                           IsRepley = m.IsRepley,
                           TrackNo = m.OrderVendor.TrackNo,
                           CommentOrder = m.CommentOrder,
                           RateTitle = lang == "ar" ? (m.RateOrder <= 1 ? "سيء جدا" : m.RateOrder <= 2 ? "سيء" : m.RateOrder <= 3 ? "مقبول" : m.RateOrder <= 4 ? "ممتاز" : "عالى الجودة") :
                           (m.RateOrder <= 1 ? "Very bad" : m.RateOrder <= 2 ? "Bad" : m.RateOrder <= 3 ? "Acceptable" : m.RateOrder <= 4 ? "Excellent" : "High quality"),
                           OrderRateGuid = m.OrderRateGuid,
                           AnswerRate = m.AnswerRate,
                           CommentDelivery = m.CommentDelivery,
                           OrderVendorID = m.OrderVendorID,
                           RateDelivery = m.RateDelivery,
                           VendorName = lang == "ar" ? m.OrderVendor.Vendors.StoreNameAr : m.OrderVendor.Vendors.StoreNameEn,
                           CustomerImage = !string.IsNullOrWhiteSpace(m.OrderVendor.Orders.Customers.ProfilePic) ? (customerPath + m.OrderVendor.Orders.Customers.ProfilePic) : "/assets/homeMadeSite/img/logo.svg",
                           VendorImage = !string.IsNullOrWhiteSpace(m.OrderVendor.Vendors.ProfilePic) ? (vendorPath + m.OrderVendor.Vendors.ProfilePic) : "/assets/homeMadeSite/img/logo.svg",
                           CreateDate = m.CreateDate,
                           CustomersGuid = m.OrderVendor.Orders.Customers.CustomersGuid,
                           VendorsGuid = m.OrderVendor.Vendors.VendorsGuid,
                       }).FirstOrDefault();
            return data;
        }

        public IQueryable<OrderRateViewModel> GetAllOrderRateViewModelRepley(string[] ListVendorID, string lang, string customerPath, string vendorPath)
        {
            if (ListVendorID != null)
            {
                if (ListVendorID.Count(p => !string.IsNullOrEmpty(p)) == 0)
                {
                    ListVendorID = null;
                }
            }
            var data = Uow.OrderRate.GetAll(s => !s.IsDeleted && s.IsRepley
             && (ListVendorID != null ? ListVendorID.Any(y => s.OrderVendor.VendorsID.ToString() == y) : true),
                       "OrderVendor.Orders.Customers").Select(m => new OrderRateViewModel()
                       {
                           CustomerName = m.OrderVendor.Orders.Customers.FirstName + " " + m.OrderVendor.Orders.Customers.SeconedName,
                            RateDate = m.CreateDate.ToString("dd-MM-yyyy hh:mm tt"),
                           OrderRateID = m.OrderRateID,
                           RateOrder = m.RateOrder,
                           IsRepley = m.IsRepley,
                           TrackNo = m.OrderVendor.TrackNo,
                           CommentOrder = m.CommentOrder,
                           RateTitle = lang == "ar" ? (m.RateOrder <= 1 ? "سيء جدا" : m.RateOrder <= 2 ? "سيء" : m.RateOrder <= 3 ? "مقبول" : m.RateOrder <= 4 ? "ممتاز" : "عالى الجودة") :
                           (m.RateOrder <= 1 ? "Very bad" : m.RateOrder <= 2 ? "Bad" : m.RateOrder <= 3 ? "Acceptable" : m.RateOrder <= 4 ? "Excellent" : "High quality"),
                           OrderRateGuid = m.OrderRateGuid,
                           AnswerRate = m.AnswerRate,
                           CommentDelivery = m.CommentDelivery,
                           OrderVendorID = m.OrderVendorID,
                           RateDelivery = m.RateDelivery,
                           VendorName = lang == "ar" ? m.OrderVendor.Vendors.StoreNameAr : m.OrderVendor.Vendors.StoreNameEn,
                           CustomerImage = !string.IsNullOrWhiteSpace(m.OrderVendor.Orders.Customers.ProfilePic) ? (customerPath + m.OrderVendor.Orders.Customers.ProfilePic) : "/assets/homeMadeSite/img/logo.svg",
                           VendorImage = !string.IsNullOrWhiteSpace(m.OrderVendor.Vendors.ProfilePic) ? (vendorPath + m.OrderVendor.Vendors.ProfilePic) : "/assets/homeMadeSite/img/logo.svg",
                           CreateDate = m.CreateDate,
                           CustomersGuid = m.OrderVendor.Orders.Customers.CustomersGuid,
                           VendorsGuid = m.OrderVendor.Vendors.VendorsGuid,
                       }).OrderByDescending(x => x.CreateDate);
            return data;
        }
        public IQueryable<OrderRateViewModel> GetAllOrderRateViewModelNotRepley(string[] ListVendorID, string lang, string customerPath, string vendorPath)
        {
            if (ListVendorID != null)
            {
                if (ListVendorID.Count(p => !string.IsNullOrEmpty(p)) == 0)
                {
                    ListVendorID = null;
                }
            }
            return Uow.OrderRate.GetAll(s => !s.IsDeleted && !s.IsRepley
             && (ListVendorID != null ? ListVendorID.Any(y => s.OrderVendor.VendorsID.ToString() == y) : true),
                       "OrderVendor.Orders.Customers").Select(m => new OrderRateViewModel()
                       {
                           CustomerName = m.OrderVendor.Orders.Customers.FirstName + " " + m.OrderVendor.Orders.Customers.SeconedName,
                            RateDate = m.CreateDate.ToString("dd-MM-yyyy hh:mm tt"),
                           OrderRateID = m.OrderRateID,
                           RateOrder = m.RateOrder,
                           IsRepley = m.IsRepley,
                           TrackNo = m.OrderVendor.TrackNo,
                           CommentOrder = m.CommentOrder,
                           RateTitle = lang == "ar" ? (m.RateOrder <= 1 ? "سيء جدا" : m.RateOrder <= 2 ? "سيء" : m.RateOrder <= 3 ? "مقبول" : m.RateOrder <= 4 ? "ممتاز" : "عالى الجودة") :
                           (m.RateOrder <= 1 ? "Very bad" : m.RateOrder <= 2 ? "Bad" : m.RateOrder <= 3 ? "Acceptable" : m.RateOrder <= 4 ? "Excellent" : "High quality"),
                           OrderRateGuid = m.OrderRateGuid,
                           AnswerRate = m.AnswerRate,
                           CommentDelivery = m.CommentDelivery,
                           OrderVendorID = m.OrderVendorID,
                           RateDelivery = m.RateDelivery,
                           VendorName = lang == "ar" ? m.OrderVendor.Vendors.StoreNameAr : m.OrderVendor.Vendors.StoreNameEn,
                           CustomerImage = !string.IsNullOrWhiteSpace(m.OrderVendor.Orders.Customers.ProfilePic) ? (customerPath + m.OrderVendor.Orders.Customers.ProfilePic) : "/assets/homeMadeSite/img/logo.svg",
                           VendorImage = !string.IsNullOrWhiteSpace(m.OrderVendor.Vendors.ProfilePic) ? (vendorPath + m.OrderVendor.Vendors.ProfilePic) : "/assets/homeMadeSite/img/logo.svg",
                           CreateDate = m.CreateDate,
                           CustomersGuid = m.OrderVendor.Orders.Customers.CustomersGuid,
                           VendorsGuid = m.OrderVendor.Vendors.VendorsGuid,
                       }).OrderByDescending(x => x.CreateDate);
        }
        public IQueryable<OrderRateViewModel> GetAllOrderRateViewModelNotRepley(int VendorsID, string customerPath, string vendorPath, string lang)
        {
            return Uow.OrderRate.GetAll(s => !s.IsDeleted && s.OrderVendor.VendorsID == VendorsID && !s.IsRepley,
                       "OrderVendor.Orders.Customers").Select(m => new OrderRateViewModel()
                       {
                           CustomerName = m.OrderVendor.Orders.Customers.FirstName + " " + m.OrderVendor.Orders.Customers.SeconedName,
                            RateDate = m.CreateDate.ToString("dd-MM-yyyy hh:mm tt"),
                           OrderRateID = m.OrderRateID,
                           RateOrder = m.RateOrder,
                           IsRepley = m.IsRepley,
                           TrackNo = m.OrderVendor.TrackNo,
                           CommentOrder = m.CommentOrder,
                           RateTitle = lang == "ar" ? (m.RateOrder <= 1 ? "سيء جدا" : m.RateOrder <= 2 ? "سيء" : m.RateOrder <= 3 ? "مقبول" : m.RateOrder <= 4 ? "ممتاز" : "عالى الجودة") :
                           (m.RateOrder <= 1 ? "Very bad" : m.RateOrder <= 2 ? "Bad" : m.RateOrder <= 3 ? "Acceptable" : m.RateOrder <= 4 ? "Excellent" : "High quality"),
                           OrderRateGuid = m.OrderRateGuid,
                           AnswerRate = m.AnswerRate,
                           CommentDelivery = m.CommentDelivery,
                           OrderVendorID = m.OrderVendorID,
                           RateDelivery = m.RateDelivery,
                           VendorName = lang == "ar" ? m.OrderVendor.Vendors.StoreNameAr : m.OrderVendor.Vendors.StoreNameEn,
                           CustomerImage = !string.IsNullOrWhiteSpace(m.OrderVendor.Orders.Customers.ProfilePic) ? (customerPath + m.OrderVendor.Orders.Customers.ProfilePic) : "/assets/homeMadeSite/img/logo.svg",
                           VendorImage = !string.IsNullOrWhiteSpace(m.OrderVendor.Vendors.ProfilePic) ? (vendorPath + m.OrderVendor.Vendors.ProfilePic) : "/assets/homeMadeSite/img/logo.svg",
                           CreateDate = m.CreateDate,
                           CustomersGuid = m.OrderVendor.Orders.Customers.CustomersGuid,
                           VendorsGuid = m.OrderVendor.Vendors.VendorsGuid,
                       }).OrderByDescending(x => x.CreateDate);
        }
        public int GetOrderRateCount(bool IsReply, string[] ListVendorID)
        {
            if (ListVendorID != null)
            {
                if (ListVendorID.Count(p => !string.IsNullOrEmpty(p)) == 0)
                {
                    ListVendorID = null;
                }
            }
            return Uow.OrderRate.GetAll(s => !s.IsDeleted && s.IsRepley == IsReply
             && (ListVendorID != null ? ListVendorID.Any(y => s.OrderVendor.VendorsID.ToString() == y) : true)).Count();
        }
        #endregion
        #region Actions
        public bool UpdateRate(int rateId, string lang, string Relpey, int userid, string FireBaseKey)
        {
            var Data = Uow.OrderRate.GetAll(s => !s.IsDeleted && s.OrderRateID == rateId, "OrderVendor.Orders.Customers").FirstOrDefault();
            Data.IsRepley = true;
            Data.AnswerRate = Relpey;
            Data.UserUpdate = userid;
            Data.UpdateDate = _blGeneral.GetDateTimeNow();
            Uow.OrderRate.Update(Data);
            var Noti = new Notification()
            {
                CreateDate = _blGeneral.GetDateTimeNow(),
                CustomersID = Data.OrderVendor.Orders.CustomersID,
                TitleAR = "تم الرد علي تقييمك للطلب رقم " + Data.OrderVendor.TrackNo + "",
                TitleEN = "Vendor has repley to your rate on order " + Data.OrderVendor.TrackNo + "",
                DescEN = Relpey,
                DescAR = Relpey,
                IsDeleted = false,
                IsEnable = true,
                IsSeen = false,
                NotificationGuid = Guid.NewGuid(),
                NotificationTypeID = (int)NotificationTypeEnum.Rate,
                UserId = userid,
                NotificationDate = _blGeneral.GetDateTimeNow(),
                OrderRateID = Data.OrderRateID,
            };
            Uow.Notification.Insert(Noti);
            var _PushListVendor = new PushList()
            {
                orderId = Data.OrderRateID,
                lat = 0,
                lng = 0,
                trackNo = Data.OrderRateID.ToString(),
                profit = "",
                cusotmerAddress = "",
                vendorAddress = "",
                vendorLogo = "",
                vendorName = "",
                body = "تم الرد علي تقييمك للطلب رقم " + Data.OrderVendor.TrackNo + "",
                sound = "default", //For IOS
                title = " الرد علي التقييم",
                content_available = "true", //For Android
                priority = "high", //For Android,
                serverKey = FireBaseKey,
                estimation = 20,
                pushType = (int)PushTypeEnum.Rate
            };
            var UserDataVendor = _blTokens.GetMobileDataByUserID(Data.OrderVendor.Orders.Customers.UserId);
            _blGeneral.SendPush(UserDataVendor.TokenVal, UserDataVendor.DeviceType, _PushListVendor);
            Uow.Commit();
            return true;
        }
        public bool Delete(int id, int userId)
        {
            var orderRate = GetById(id);
            if (orderRate != null)
            {
                orderRate.IsDeleted = true;
                orderRate.UserDelete = userId;
                orderRate.DeleteDate = _blGeneral.GetDateTimeNow();
                Uow.OrderRate.Update(orderRate);
                Uow.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

    }
}
