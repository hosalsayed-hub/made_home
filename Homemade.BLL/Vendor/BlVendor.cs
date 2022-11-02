using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Homemade.BLL.Customer;
using Homemade.BLL.Driver;
using Homemade.BLL.General;
using Homemade.BLL.Order;
using Homemade.BLL.Setting;
using Homemade.BLL.ViewModel;
using Homemade.BLL.ViewModel.Customer;
using Homemade.BLL.ViewModel.Driver;
using Homemade.BLL.ViewModel.Employees;
using Homemade.BLL.ViewModel.Notifications;
using Homemade.BLL.ViewModel.Setting;
using Homemade.BLL.ViewModel.Site;
using Homemade.BLL.ViewModel.Vendor;
using Homemade.DAL.Contract;
using Homemade.Model;
using Homemade.Model.Order;
using Homemade.Model.Setting;
using Homemade.Model.Vendor;
using Microsoft.AspNetCore.Identity;

namespace Homemade.BLL.Vendor
{
    public class BlVendor
    {
        #region Declartion
        private readonly IUOW Uow;
        private readonly BLGeneral _bLGeneral;
        private readonly BlDriverBalance _BlDriverBalance;
        private readonly BLPermission _bLPermission;
        private readonly BlCustomerBalance _BlCustomerBalance;
        private readonly BlVendorBalance _BlVendorBalance;
        private readonly UserManager<User> _userManager;
        private readonly BlTokens _blTokens;
        private readonly BlOrders _BlOrders;
        private readonly BlConfiguration _blConfiguration;
        private readonly BlNationality _blNationality;
        public BlVendor(BlOrders BlOrders, BlTokens BlTokens, IUOW _uow, BLGeneral bLGeneral, BLPermission bLPermission, UserManager<User> userManager, BlDriverBalance BlDriverBalance, BlCustomerBalance BlCustomerBalance, BlVendorBalance BlVendorBalance, BlConfiguration blConfiguration, BlNationality blNationality)
        {
            this.Uow = _uow;
            _bLGeneral = bLGeneral;
            _bLPermission = bLPermission;
            _userManager = userManager;
            _BlDriverBalance = BlDriverBalance;
            _BlCustomerBalance = BlCustomerBalance;
            _BlVendorBalance = BlVendorBalance;
            _blTokens = BlTokens;
            _BlOrders = BlOrders;
            _blConfiguration = blConfiguration;
            _blNationality = blNationality;
        }
        #endregion
        #region Notifications
        public int GetUserNotificationNotSeenCount(int UserId) => Uow.Notification.GetAll(x => !x.IsDeleted && x.Vendors.UserId == UserId && !x.IsSeen).Count();
        public Notification GetNotificationById(int NotificationID) => Uow.Notification.GetAll(x => !x.IsDeleted && x.NotificationID == NotificationID).FirstOrDefault();
        public bool UpdateNotificationSeen(int notificationID, int userId)
        {
            var notification = GetNotificationById(notificationID);
            if (notification != null)
            {
                notification.UpdateDate = _bLGeneral.GetDateTimeNow();
                notification.UserUpdate = userId;
                notification.IsSeen = true;
                notification = Uow.Notification.Update(notification);
                Uow.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool UpdateNotificationSeen(int userId)
        {
            var notifications = Uow.Notification.GetAll(x => !x.IsDeleted && x.Vendors.UserId == userId && !x.IsSeen);
            if (notifications != null)
            {
                foreach (var notification in notifications)
                {
                    notification.UpdateDate = _bLGeneral.GetDateTimeNow();
                    notification.UserUpdate = userId;
                    notification.IsSeen = true;
                    Uow.Notification.Update(notification);
                }
                Uow.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }
        public ResponsListNoti GetNotificationListApiViewModelByDriver(int vendorId, string accLanguage, int page)
        {
            var listret = new List<NotificationListApiViewModel>();
            var data = Uow.Notification.GetAll(x => !x.IsDeleted && x.VendorsID == vendorId).OrderByDescending(e => e.CreateDate)
                .GroupBy(s => s.CreateDate.Date).Select(m => new NotificationListApiViewModel()
                {
                    date = accLanguage == "ar" ? (m.Key.Date == DateTime.Now.Date ? "اليوم" : m.Key.Date.AddDays(1) == DateTime.Now.Date ? "امس" : m.Key.ToString("dd/MM/yyyy")) :
                 m.Key.Date == DateTime.Now.Date ? "Today" : m.Key.Date.AddDays(1) == DateTime.Now.Date ? "yesterday" : m.Key.ToString("dd/MM/yyyy"),
                    notificationCount = m.Count(),
                    datekey = m.Key
                }).ToList().OrderByDescending(e => e.datekey);
            foreach (var item in data)
            {
                item.dayNotifications = Uow.Notification.GetAll(x => !x.IsDeleted && x.CreateDate.Date == item.datekey.Date && x.VendorsID == vendorId
                ).OrderByDescending(e => e.CreateDate).Select(m => new NotificationDetailsApiViewModel()
                {
                    notificationID = m.NotificationID,
                    isSeen = m.IsSeen,
                    time = m.NotificationDate.ToString("HH:mm tt"),
                    desc = accLanguage == "ar" ? m.DescAR ?? string.Empty : m.DescEN ?? string.Empty,
                    title = accLanguage == "ar" ? m.TitleAR ?? string.Empty : m.TitleEN ?? string.Empty,
                    orderId = m.OrderVendorID != null ? m.OrderVendorID : 0,
                    rateId = m.OrderRateID != null ? m.OrderRateID : 0,
                    questionId = m.ProdQuestionID != null ? m.ProdQuestionID : 0,
                    notificationType = m.OrderVendorID != null ?
                    (m.OrderVendor.OrderStatusID == (int)OrderStatusEnum.Pending && m.OrderVendor.ApprovalQuantity == (int)ApprovalQuantityEnum.NotAction) ?
                    (int)NotificationTypeEnum.Pending : (m.OrderVendor.OrderStatusID == (int)OrderStatusEnum.Pending && (m.OrderVendor.ApprovalQuantity == (int)ApprovalQuantityEnum.Approve || m.OrderVendor.ApprovalQuantity == (int)ApprovalQuantityEnum.Reject)) ? 0 :
                    m.NotificationTypeID : m.NotificationTypeID,
                }).ToList();
                listret.Add(item);
            }
            var Count = listret.Count();
            listret = listret.Skip(Convert.ToInt32(page - 1) * 10).ToList();
            var DataTake = listret.Take(((Convert.ToInt32(page) * 10)) - (Convert.ToInt32(page - 1) * 10)).ToList();
            bool NextPage = false;
            if (Count > 10)
            {
                NextPage = true;
            }
            var model = new ResponsListNoti();
            model.notifications = DataTake;
            model.isNextPage = NextPage;
            return model;
        }
        #endregion
        #region Invoices
        public List<OrderVendorViewModelInvoice> GetAllTripHistoryQueryResultViewModelForInvoiceGenerate(int? VendorsID, DateTime? FromDate, DateTime? ToDate, string accLanguage)
        {
            var data = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.InvoiceTypeId == (int)OrderInvoiceTypeEnum.Pending_Invoice
                                                  && (VendorsID != null ? x.VendorsID == VendorsID : false)
                                                  && (FromDate != null ? x.CreateDate.Date >= FromDate.Value.Date : true)
                                                  && (ToDate != null ? x.CreateDate.Date <= ToDate.Value.Date : true)
                , "Orders.Customers,Drivers,Vendors,OrderStatus").OrderByDescending(a => a.CreateDate).Select(m => new OrderVendorViewModelInvoice()
                {
                    CreateDate = m.CreateDate.ToString("dd/MM/yyyy hh:mm tt"),
                    CreationDate = m.CreateDate,
                    DriverName = m.DriversID != null ? accLanguage == "ar" ? m.Drivers.NameAr : m.Drivers.NameEn : "----",
                    DriverPhoneNumber = m.DriversID != null ? m.Drivers.PhoneNumber : "----",
                    CustomerName = m.Orders.Customers.FirstName,
                    CustomerMobileNo = m.Orders.Customers.MobileNo,
                    OrderVendorGuid = m.OrderVendorGuid,
                    OrderVendorID = m.OrderVendorID,
                    OrderStatusName = accLanguage == "ar" ? m.OrderStatus.NameAR : m.OrderStatus.NameEN,
                    DriversID = m.DriversID,
                    OrderStatusID = m.OrderStatusID,
                    VendorName = accLanguage == "ar" ? m.Vendors.FirstNameAr : m.Vendors.FirstNameEn,
                    CatainType = accLanguage == "ar" ? (m.CaptainTypeId == (int)CaptainTypeEnum.Home_Made ? "شغل بيت" : m.CaptainTypeId == (int)CaptainTypeEnum.Shipping_Company ? "شركة الشحن" : "كابتن المتجر")
                    : m.CaptainTypeId == (int)CaptainTypeEnum.Home_Made ? "Home Made" : m.CaptainTypeId == (int)CaptainTypeEnum.Shipping_Company ? "Shipping Company" : "Store Captain",
                    TotalBaseItem = m.TotalBaseItems,
                    DeliveryPrice = m.DeliveryPrice - m.DeliveryVatValue,
                    Discount = m.Discount,
                    DeliveryPriceVatValue = m.DeliveryVatValue,
                    VatProduct = m.VatProduct,
                    VatValue = m.VatValue,
                    Total = m.Total,
                    PerStore = m.PerStore,
                    PerHome = m.PerHome,
                }).ToList();
            return data;
        }
        public List<OrderVendorViewModelInvoice> GetAllTripHistoryQueryResultViewModelForInvoiceGenerateBulk(string[] ListVendorID, DateTime? FromDate, DateTime? ToDate, string accLanguage)
        {
            if (ListVendorID != null)
            {
                if (ListVendorID.Count(p => !string.IsNullOrEmpty(p)) == 0)
                {
                    ListVendorID = null;
                }
            }
            var data = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.InvoiceTypeId == (int)OrderInvoiceTypeEnum.Pending_Invoice
                        && (ListVendorID != null ? ListVendorID.Any(y => x.VendorsID.ToString() == y) : true)
                        && (FromDate != null ? x.CreateDate.Date >= FromDate.Value.Date : true)
                                                  && (ToDate != null ? x.CreateDate.Date <= ToDate.Value.Date : true)
                , "Orders.Customers,Drivers,Vendors,OrderStatus").OrderByDescending(a => a.CreateDate).Select(m => new OrderVendorViewModelInvoice()
                {
                    CreateDate = m.CreateDate.ToString("dd/MM/yyyy hh:mm tt"),
                    CreationDate = m.CreateDate,
                    DriverName = m.DriversID != null ? accLanguage == "ar" ? m.Drivers.NameAr : m.Drivers.NameEn : "----",
                    DriverPhoneNumber = m.DriversID != null ? m.Drivers.PhoneNumber : "----",
                    CustomerName = m.Orders.Customers.FirstName,
                    CustomerMobileNo = m.Orders.Customers.MobileNo,
                    OrderVendorGuid = m.OrderVendorGuid,
                    OrderVendorID = m.OrderVendorID,
                    OrderStatusName = accLanguage == "ar" ? m.OrderStatus.NameAR : m.OrderStatus.NameEN,
                    DriversID = m.DriversID,
                    OrderStatusID = m.OrderStatusID,
                    VendorName = accLanguage == "ar" ? m.Vendors.StoreNameAr : m.Vendors.StoreNameEn,
                    CatainType = accLanguage == "ar" ? (m.CaptainTypeId == (int)CaptainTypeEnum.Home_Made ? "شغل بيت" : m.CaptainTypeId == (int)CaptainTypeEnum.Shipping_Company ? "شركة الشحن" : "كابتن المتجر")
                    : m.CaptainTypeId == (int)CaptainTypeEnum.Home_Made ? "Home Made" : m.CaptainTypeId == (int)CaptainTypeEnum.Shipping_Company ? "Shipping Company" : "Store Captain",
                    VendorsID = m.VendorsID,
                    TotalBaseItem = m.TotalBaseItems,
                    DeliveryPrice = m.DeliveryPrice - m.DeliveryVatValue,
                    Discount = m.Discount,
                    DeliveryPriceVatValue = m.DeliveryVatValue,
                    VatProduct = m.VatProduct,
                    VatValue = m.VatValue,
                    Total = m.Total,
                    PerStore = m.PerStore,
                    PerHome = m.PerHome,
                }).ToList();
            return data;
        }
        public List<OrderVendorViewModelInvoice> GetAllTripHistoryQueryResultViewModelForPayment(string[] ListVendorID, DateTime? FromDate, DateTime? ToDate, string accLanguage)
        {
            if (ListVendorID != null)
            {
                if (ListVendorID.Count(p => !string.IsNullOrEmpty(p)) == 0)
                {
                    ListVendorID = null;
                }
            }
            var data = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.TotalBaseItems > 0 &&
             (x.InvoiceTypeId == (int)OrderInvoiceTypeEnum.Pending_Invoice || x.InvoiceTypeId == (int)OrderInvoiceTypeEnum.Invoiced)
                         && (ListVendorID != null ? ListVendorID.Any(y => x.VendorsID.ToString() == y) : true)
                         && (FromDate != null ? x.CreateDate.Date >= FromDate.Value.Date : true)
                                                   && (ToDate != null ? x.CreateDate.Date <= ToDate.Value.Date : true)
                 , "Orders.Customers,Drivers,Vendors,OrderStatus").OrderByDescending(a => a.CreateDate).Select(m => new OrderVendorViewModelInvoice()
                 {
                     CreateDate = m.CreateDate.ToString("dd/MM/yyyy hh:mm tt"),
                     CreationDate = m.CreateDate,
                     DriverName = m.DriversID != null ? accLanguage == "ar" ? m.Drivers.NameAr : m.Drivers.NameEn : "----",
                     DriverPhoneNumber = m.DriversID != null ? m.Drivers.PhoneNumber : "----",
                     CustomerName = m.Orders.Customers.FirstName,
                     CustomerMobileNo = m.Orders.Customers.MobileNo,
                     OrderVendorGuid = m.OrderVendorGuid,
                     OrderVendorID = m.OrderVendorID,
                     OrderStatusName = accLanguage == "ar" ? m.OrderStatus.NameAR : m.OrderStatus.NameEN,
                     DriversID = m.DriversID,
                     OrderStatusID = m.OrderStatusID,
                     VendorName = accLanguage == "ar" ? m.Vendors.StoreNameAr : m.Vendors.StoreNameEn,
                     CatainType = accLanguage == "ar" ? (m.CaptainTypeId == (int)CaptainTypeEnum.Home_Made ? "شغل بيت" : m.CaptainTypeId == (int)CaptainTypeEnum.Shipping_Company ? "شركة الشحن" : "كابتن المتجر")
                    : m.CaptainTypeId == (int)CaptainTypeEnum.Home_Made ? "Home Made" : m.CaptainTypeId == (int)CaptainTypeEnum.Shipping_Company ? "Shipping Company" : "Store Captain",
                     VendorsID = m.VendorsID,
                     TotalBaseItem = m.TotalBaseItems,
                     DeliveryPrice = m.DeliveryPrice - m.DeliveryVatValue,
                     Discount = m.Discount,
                     PackageAmount = m.PackageAmount,
                     PackagePercent = m.PackagePercent,
                     VatValue = m.VatValue,
                     Total = m.Total,
                     PerStore = m.PerStore,
                     PerHome = m.PerHome,
                     PaymentType = accLanguage == "ar" ? (m.Orders.PaymentTypeId == (int)PaymentTypeEnum.Wallet ? "المحفظة" : m.CardType != null ? m.CardType : "البطاقة البنكية") :
                     m.Orders.PaymentTypeId == (int)PaymentTypeEnum.Wallet ? "Wallet" : m.CardType != null ? m.CardType : "Card",
                     PerCompanyPercent = m.PackageAmount > 0 && m.PackagePercent > 0 && m.TotalBaseItems > 0 ? "(" + ((m.PackagePercent / m.TotalBaseItems) * 100).ToString("N2") + "% + " + m.PackageAmount + " ريـال)"
                    : m.PackagePercent > 0 && m.TotalBaseItems > 0 ? "" + ((m.PackagePercent / m.TotalBaseItems) * 100).ToString("N2") + "%" : "0",
                     perStorePercent = m.PackagePercent > 0 && m.TotalBaseItems > 0 ? (100 - ((m.PackagePercent / m.TotalBaseItems) * 100)).ToString("N2") + "%" : "0",
                 }).ToList();
            return data;
        }
        #endregion
        #region Helper
        public class InvoiceTypes
        {
            public int Id { get; set; }
            public string NameAr { get; set; }
            public string NameEn { get; set; }
        }
        public IQueryable<StatusCompany> GetStatusCompany(int ShiipingEnum)
        {
            return Uow.StatusCompany.GetAll(e => e.ShippingCompany.ShippingEnum == ShiipingEnum);
        }
        public List<InvoiceTypes> GetInvoiceStatus()
        {
            var list = new List<InvoiceTypes>();
            list.Add(new InvoiceTypes() { Id = (int)InvoiceTypeEnum.Pending_Operation, NameAr = "انتظار المراجعة", NameEn = "Waiting review" });
            list.Add(new InvoiceTypes() { Id = (int)InvoiceTypeEnum.Pending_Admin, NameAr = "انتظار اعتماد التحويل", NameEn = "Waiting transfer approval" });
            list.Add(new InvoiceTypes() { Id = (int)InvoiceTypeEnum.Pending_Paid, NameAr = "انتظار تأكيد التحويل", NameEn = "Waiting transfer confirmation" });
            list.Add(new InvoiceTypes() { Id = (int)InvoiceTypeEnum.Paid, NameAr = "تم التحويل", NameEn = "Transferred" });
            return list;
        }
        public QuestionListViewModelApi GetQuestionList(string lang, int vendorId, string custoemrPath, string ProdPath, int page)
        {
            var Data = Uow.ProdQuestion.GetAll(s => !s.IsDeleted && s.Product.VendorsID == vendorId,
                       "Product,Customers").Select(m => new ListVendorQuestion()
                       {
                           customerImage = !string.IsNullOrWhiteSpace(m.Customers.ProfilePic) ? custoemrPath + m.Customers.ProfilePic : "",
                           customerName = m.Customers.FirstName + " " + m.Customers.SeconedName,
                           questionDate = m.CreateDate.ToString("dd/MM/yyyy"),
                           productImage = !string.IsNullOrWhiteSpace(m.Product.Logo) ? ProdPath + m.Product.Logo : "",
                           question = m.Question,
                           questionId = m.ProdQuestionID,
                           isRepley = m.IsRepley,
                           answer = m.Answer,
                           price = lang == "ar" ? m.Product.Price + " ريـال" : m.Product.Price + " SAR",
                           productName = lang == "ar" ? m.Product.NameAr : m.Product.NameEn,
                           descripe = lang == "ar" ? m.Product.DescAr : m.Product.DescEn,
                       }).ToList();
            Data = Data.Skip(Convert.ToInt32(page - 1) * 10).ToList();
            var DataTake = Data.Take(((Convert.ToInt32(page) * 10)) - (Convert.ToInt32(page - 1) * 10)).ToList();
            bool NextPage = false;
            if (Data.Count() > 10)
            {
                NextPage = true;
            }
            var model = new QuestionListViewModelApi();
            model.questions = DataTake;
            model.isNextPage = NextPage;
            return model;
        }
        public InvoiceMasterViewModelApi GetInvoiceMastetByVendor(int vendorId, string lang, int page, string serach, string status, DateTime? fromdate, DateTime? todate)
        {
            var Data = Uow.InvoiceMaster.GetAll(s => s.VendorsID == vendorId && !s.IsDeleted).Select(s => new InvoiceVendorList()
            {
                colorHex = s.InvoiceStatusId == (int)InvoiceStatusEnum.Paid ? "#FFF9D9" : "#E5F9ED",
                colorText = s.InvoiceStatusId == (int)InvoiceStatusEnum.Paid ? "#FFBB00" : "#00BE4C",
                invoiceId = s.InvoiceMasterID,
                date = s.CreateDate.ToString("dd/MM/yyyy"),
                invoiceNo = s.InvoiceNo,
                perOrderHomeMade = lang == "ar" ? s.InvoiceDetails.Sum(x => x.OrderVendor.PerHome) + " ريـال" : s.InvoiceDetails.Sum(x => x.OrderVendor.PerHome) + " SAR",
                perOrderStore = lang == "ar" ? s.InvoiceDetails.Sum(x => x.OrderVendor.PerStore) + " ريـال" : s.InvoiceDetails.Sum(x => x.OrderVendor.PerStore) + " SAR",
                perStoreDe = s.InvoiceDetails.Sum(x => x.OrderVendor.PerStore),
                perHomeDe = s.InvoiceDetails.Sum(x => x.OrderVendor.PerHome),
                status = lang == "ar" ? (s.InvoiceTypeId == (int)InvoiceTypeEnum.Pending_Operation ? "انتظار المراجعة" : s.InvoiceTypeId == (int)InvoiceTypeEnum.Pending_Admin ? "انتظار اعتماد التحويل" : s.InvoiceTypeId == (int)InvoiceTypeEnum.Pending_Paid ? "انتظار تأكيد التحويل" : s.InvoiceTypeId == (int)InvoiceTypeEnum.Paid ? "تم التحويل" : "")
                        : (s.InvoiceTypeId == (int)InvoiceTypeEnum.Pending_Operation ? "Waiting review" : s.InvoiceTypeId == (int)InvoiceTypeEnum.Pending_Admin ? "Waiting transfer approval" : s.InvoiceTypeId == (int)InvoiceTypeEnum.Pending_Paid ? "Waiting transfer confirmation" : s.InvoiceTypeId == (int)InvoiceTypeEnum.Paid ? "Transferred" : ""),
                total = lang == "ar" ? s.Total + " ريـال" : s.Total + " SAR",
                statusId = s.InvoiceStatusId,
                createDate = s.CreateDate,
            }).ToList();
            if (!string.IsNullOrWhiteSpace(serach))
            {
                Data = Data.Where(s => s.invoiceNo.ToLower().Contains(serach.ToLower())).ToList();
            }
            if (!string.IsNullOrWhiteSpace(status))
            {
                string[] listdept = status.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                Data = Data.Where(s => listdept.Any(z => z == s.statusId.ToString())).ToList();
            }
            if (fromdate != null)
            {
                Data = Data.Where(s => s.createDate >= fromdate).ToList();
            }
            if (todate != null)
            {
                Data = Data.Where(s => s.createDate <= todate).ToList();
            }
            Data = Data.Skip(Convert.ToInt32(page - 1) * 10).ToList();
            var DataTake = Data.Take(((Convert.ToInt32(page) * 10)) - (Convert.ToInt32(page - 1) * 10)).ToList();
            bool NextPage = false;
            if (Data.Count() > 10)
            {
                NextPage = true;
            }
            var model = new InvoiceMasterViewModelApi();
            model.invoices = DataTake;
            model.isNextPage = NextPage;
            model.lastInvoiceDate = Uow.InvoiceMaster.GetAll(s => s.VendorsID == vendorId).OrderByDescending(s => s.CreateDate).FirstOrDefault() != null ?
                Uow.InvoiceMaster.GetAll(s => s.VendorsID == vendorId).OrderByDescending(s => s.CreateDate).FirstOrDefault().CreateDate.ToString("dd/MM/yyyy hh:mm tt") : DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
            model.perHomeMade = Data.Any() ? Data.Sum(s => s.perHomeDe) : 0;
            model.perStore = Data.Any() ? Data.Sum(s => s.perStoreDe) : 0;
            return model;
        }
        public InvoiceDetailsViewModelApi GetInvoiceDetails(int invoiceId, string lang, int page, string serach)
        {
            var Data = Uow.InvoiceDetails.GetAll(s => s.InvoiceMasterID == invoiceId && !s.IsDeleted, "OrderVendor.Orders.PromoCode").Select(s => new InvoiceOrderList()
            {
                date = s.CreateDate.ToString("dd/MM/yyyy"),
                total = lang == "ar" ? s.OrderVendor.Total + " ريـال" : s.OrderVendor.Total + " SAR",
                deliverycharge = lang == "ar" ? s.OrderVendor.DeliveryPrice + " " : s.OrderVendor.DeliveryPrice + " ",
                discount = lang == "ar" ? s.OrderVendor.Discount + " " : s.OrderVendor.Discount + " ",
                price = lang == "ar" ? s.OrderVendor.Price + " " : s.OrderVendor.Price + " ",
                vat = lang == "ar" ? s.OrderVendor.VatValue + " " : s.OrderVendor.VatValue + " ",
                promocode = s.OrderVendor.Orders.PromoCodeID != null ? s.OrderVendor.Orders.PromoCode.Code : "",
                trackNo = s.OrderVendor.TrackNo,
                orderId = s.OrderVendorID
            }).ToList();
            if (!string.IsNullOrWhiteSpace(serach))
            {
                Data = Data.Where(s => s.trackNo.ToLower().Contains(serach.ToLower())).ToList();
            }
            Data = Data.Skip(Convert.ToInt32(page - 1) * 10).ToList();
            var DataTake = Data.Take(((Convert.ToInt32(page) * 10)) - (Convert.ToInt32(page - 1) * 10)).ToList();
            bool NextPage = false;
            if (Data.Count() > 10)
            {
                NextPage = true;
            }
            var model = new InvoiceDetailsViewModelApi();
            model.invoices = DataTake;
            model.isNextPage = NextPage;
            model.date = Uow.InvoiceMaster.GetAll(s => s.InvoiceMasterID == invoiceId).FirstOrDefault() != null ?
                Uow.InvoiceMaster.GetAll(s => s.InvoiceMasterID == invoiceId).FirstOrDefault().CreateDate.ToString("dd/MM/yyyy hh:mm tt") : DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
            model.invoiceNo = Uow.InvoiceMaster.GetAll(s => s.InvoiceMasterID == invoiceId).FirstOrDefault() != null ?
                Uow.InvoiceMaster.GetAll(s => s.InvoiceMasterID == invoiceId).FirstOrDefault().InvoiceNo : "";
            return model;
        }
        public bool CancelRegister(int vendorId, int UserId)
        {
            var Vendordata = Uow.Vendors.GetAll(s => s.VendorsID == vendorId).FirstOrDefault();
            if (Vendordata != null)
            {
                Vendordata.IsEnable = false;
                Vendordata.UpdateDate = _bLGeneral.GetDateTimeNow();
                Vendordata.UserUpdate = UserId;
                Vendordata.EnableHistory.Add(new EnableHistory()
                {
                    CreateDate = _bLGeneral.GetDateTimeNow(),
                    EnableHistoryGuid = Guid.NewGuid(),
                    IsDeleted = false,
                    IsEnable = true,
                    Status = false,
                    UserId = UserId
                });
                Uow.Vendors.Update(Vendordata);
                Uow.Commit();
            }
            return true;
        }
        public string AddVaction(int vendorId, int UserId, DateTime? StartDate, DateTime? EndData, int Type, string lang)
        {
            var Vendordata = Uow.Vendors.GetAll(s => s.VendorsID == vendorId).FirstOrDefault();
            try
            {
                if (Vendordata != null)
                {
                    if (Type == 1) //اضافه اجازة
                    {
                        if (EndData.HasValue && StartDate.HasValue)
                        {
                            //سيتم تعديلها ليكون ابديت الاجازه علي اساس الجوب
                            Vendordata.IsVac = true;
                            Vendordata.UpdateDate = _bLGeneral.GetDateTimeNow();
                            Vendordata.UserUpdate = UserId;
                            Vendordata.VacHistory.Add(new VacHistory()
                            {
                                CreateDate = _bLGeneral.GetDateTimeNow(),
                                VacHistoryGuid = Guid.NewGuid(),
                                IsDeleted = false,
                                IsEnable = true,
                                UserId = UserId,
                                IsReturn = false,
                                VacFrom = (DateTime)StartDate,
                                VacTo = (DateTime)EndData,
                            });
                            Uow.Vendors.Update(Vendordata);
                            Uow.Commit();
                        }
                        else
                        {
                            return lang == "ar" ? "ادخل تاريخ بدء الاجازة وتاريخ الانتهاء" : "Enter Vacation Start Date and End Date";
                        }
                    }
                    else //عودة اجازة
                    {
                        var VacData = Uow.VacHistory.GetAll(s => s.VendorsID == vendorId).OrderByDescending(s => s.CreateDate).FirstOrDefault();
                        if (VacData != null)
                        {
                            Vendordata.IsVac = false;
                            Vendordata.UpdateDate = _bLGeneral.GetDateTimeNow();
                            Vendordata.UserUpdate = UserId;
                            Uow.Vendors.Update(Vendordata);
                            VacData.IsReturn = true;
                            VacData.UpdateDate = _bLGeneral.GetDateTimeNow();
                            VacData.UserUpdate = UserId;
                            Uow.VacHistory.Update(VacData);
                            Uow.Commit();
                        }
                        else
                        {
                            return lang == "ar" ? "لم يتم تسجيل اجازه لك لايقافها" : "No Vacation has been added";
                        }
                    }
                }
                return "true";
            }
            catch (Exception ex)
            {
                return ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }
        }
        public RateListViewModelApi GetRateList(string lang, int vendorId, string custoemrPath, string ProdPath, int page)
        {
            var Data = Uow.OrderRate.GetAll(s => !s.IsDeleted && s.OrderVendor.VendorsID == vendorId,
                       "OrderVendor.Orders.Customers").Select(m => new ListVendorRate()
                       {
                           customerImage = !string.IsNullOrWhiteSpace(m.OrderVendor.Orders.Customers.ProfilePic) ? custoemrPath + m.OrderVendor.Orders.Customers.ProfilePic : "",
                           customerName = m.OrderVendor.Orders.Customers.FirstName + " " + m.OrderVendor.Orders.Customers.SeconedName,
                           rateDate = m.CreateDate.ToString("dd/MM/yyyy"),
                           rateId = m.OrderRateID,
                           rate = m.RateOrder,
                           isRepley = m.IsRepley,
                           trackNo = m.OrderVendor.TrackNo,
                           repley = "",
                           rateComment = m.CommentOrder,
                           rateTitle = lang == "ar" ? (m.RateOrder <= 1 ? "سيء جدا" : m.RateOrder <= 2 ? "سيء" : m.RateOrder <= 3 ? "مقبول" : m.RateOrder <= 4 ? "ممتاز" : "عالى الجودة") :
                           (m.RateOrder <= 1 ? "Very bad" : m.RateOrder <= 2 ? "Bad" : m.RateOrder <= 3 ? "Acceptable" : m.RateOrder <= 4 ? "Excellent" : "High quality"),
                           listProducts = m.OrderVendor.OrderItems.Where(x => !x.IsDeleted).Select(z => new ViewModel.Vendor.ListProductRate()
                           {
                               image = !string.IsNullOrWhiteSpace(z.ProdImage) ? ProdPath + z.ProdImage : "",
                               name = lang == "ar" ? z.ProdNameAr : z.ProdNameEn,
                               price = lang == "ar" ? z.Price + " ريـال" : z.Price + " SAR"
                           }).ToList()
                       }).ToList();
            Data = Data.Skip(Convert.ToInt32(page - 1) * 10).ToList();
            var DataTake = Data.Take(((Convert.ToInt32(page) * 10)) - (Convert.ToInt32(page - 1) * 10)).ToList();
            bool NextPage = false;
            if (Data.Count() > 10)
            {
                NextPage = true;
            }
            var model = new RateListViewModelApi();
            model.rates = DataTake;
            model.isNextPage = NextPage;
            return model;
        }
        public ListVendorRate RateDetaiils(int rateId, string lang, string custoemrPath, string ProdPath)
        {
            return Uow.OrderRate.GetAll(s => !s.IsDeleted && s.OrderRateID == rateId).Select(m => new ListVendorRate()
            {
                customerImage = !string.IsNullOrWhiteSpace(m.OrderVendor.Orders.Customers.ProfilePic) ? custoemrPath + m.OrderVendor.Orders.Customers.ProfilePic : "",
                customerName = m.OrderVendor.Orders.Customers.FirstName + " " + m.OrderVendor.Orders.Customers.SeconedName,
                rateDate = m.CreateDate.ToString("dd/MM/yyyy"),
                rateId = m.OrderRateID,
                rate = m.RateOrder,
                isRepley = m.IsRepley,
                repley = "",
                rateComment = m.CommentOrder,
                trackNo = m.OrderVendor.TrackNo,
                rateTitle = lang == "ar" ? (m.RateOrder <= 1 ? "سيء جدا" : m.RateOrder <= 2 ? "سيء" : m.RateOrder <= 3 ? "مقبول" : m.RateOrder <= 4 ? "ممتاز" : "عالى الجودة") :
                            (m.RateOrder <= 1 ? "Very bad" : m.RateOrder <= 2 ? "Bad" : m.RateOrder <= 3 ? "Acceptable" : m.RateOrder <= 4 ? "Excellent" : "High quality"),
                listProducts = m.OrderVendor.OrderItems.Where(x => !x.IsDeleted).Select(z => new ViewModel.Vendor.ListProductRate()
                {
                    image = !string.IsNullOrWhiteSpace(z.ProdImage) ? ProdPath + z.ProdImage : "",
                    name = lang == "ar" ? z.ProdNameAr : z.ProdNameEn,
                    price = lang == "ar" ? z.Price + " ريـال" : z.Price + " SAR"
                }).ToList()
            }).FirstOrDefault();
        }
        public ListVendorQuestion QuestionDetaiils(int qeuestionId, string lang, string custoemrPath, string ProdPath)
        {
            return Uow.ProdQuestion.GetAll(s => !s.IsDeleted && s.ProdQuestionID == qeuestionId).Select(m => new ListVendorQuestion()
            {
                customerImage = !string.IsNullOrWhiteSpace(m.Customers.ProfilePic) ? custoemrPath + m.Customers.ProfilePic : "",
                customerName = m.Customers.FirstName + " " + m.Customers.SeconedName,
                questionDate = m.CreateDate.ToString("dd/MM/yyyy"),
                productImage = !string.IsNullOrWhiteSpace(m.Product.Logo) ? ProdPath + m.Product.Logo : "",
                question = m.Question,
                answer = m.Answer,
                price = lang == "ar" ? m.Product.Price + " ريـال" : m.Product.Price + " SAR",
                questionId = m.ProdQuestionID,
                isRepley = m.IsRepley,
                productName = lang == "ar" ? m.Product.NameAr : m.Product.NameEn,
                descripe = lang == "ar" ? m.Product.DescAr : m.Product.DescEn,
            }).FirstOrDefault();
        }
        public ListVendorQuestion UpdateQuestion(int qeuestionId, bool isPublish, string lang, string custoemrPath, string ProdPath, string Relpey, int userid, string FireBaseKey)
        {
            var Data = Uow.ProdQuestion.GetAll(s => !s.IsDeleted && s.ProdQuestionID == qeuestionId, "Product,Customers").FirstOrDefault();
            Data.IsRepley = true;
            Data.Answer = Relpey;
            Data.IsEnable = isPublish;
            Data.UserUpdate = userid;
            Data.UpdateDate = _bLGeneral.GetDateTimeNow();
            Uow.ProdQuestion.Update(Data);
            var Noti = new Notification()
            {
                CreateDate = _bLGeneral.GetDateTimeNow(),
                CustomersID = Data.CustomersID,
                TitleAR = "تم الرد علي سؤالك بخصوص المنتج " + Data.Product.NameAr + "",
                TitleEN = "Vendor has repley to your question on product " + Data.Product.NameEn + "",
                DescEN = Relpey,
                DescAR = Relpey,
                IsDeleted = false,
                IsEnable = true,
                IsSeen = false,
                NotificationGuid = Guid.NewGuid(),
                NotificationTypeID = (int)NotificationTypeEnum.Question,
                UserId = userid,
                NotificationDate = _bLGeneral.GetDateTimeNow(),
                ProdQuestionID = Data.ProdQuestionID
            };
            Uow.Notification.Insert(Noti);
            var _PushListVendor = new PushList()
            {
                orderId = Data.ProdQuestionID,
                lat = 0,
                lng = 0,
                trackNo = Data.ProdQuestionID.ToString(),
                profit = "",
                cusotmerAddress = "",
                vendorAddress = "",
                vendorLogo = "",
                vendorName = "",
                body = "تم الرد علي سؤالك بخصوص المنتج " + Data.Product.NameAr + "",
                sound = "default", //For IOS
                title = " الرد علي سؤالك",
                content_available = "true", //For Android
                priority = "high", //For Android,
                serverKey = FireBaseKey,
                estimation = 20,
                pushType = (int)PushTypeEnum.Question
            };
            var UserDataVendor = _blTokens.GetMobileDataByUserID(Data.Customers.UserId);
            _bLGeneral.SendPush(UserDataVendor.TokenVal, UserDataVendor.DeviceType, _PushListVendor);
            Uow.Commit();
            return Uow.ProdQuestion.GetAll(s => !s.IsDeleted && s.ProdQuestionID == qeuestionId).Select(m => new ListVendorQuestion()
            {
                customerImage = !string.IsNullOrWhiteSpace(m.Customers.ProfilePic) ? custoemrPath + m.Customers.ProfilePic : "",
                customerName = m.Customers.FirstName + " " + m.Customers.SeconedName,
                questionDate = m.CreateDate.ToString("dd/MM/yyyy"),
                productImage = !string.IsNullOrWhiteSpace(m.Product.Logo) ? ProdPath + m.Product.Logo : "",
                question = m.Question,
                answer = m.Answer,
                price = lang == "ar" ? m.Product.Price + " ريـال" : m.Product.Price + " SAR",
                questionId = m.ProdQuestionID,
                isRepley = m.IsRepley,
                productName = lang == "ar" ? m.Product.NameAr : m.Product.NameEn,
                descripe = lang == "ar" ? m.Product.DescAr : m.Product.DescEn,
            }).FirstOrDefault();
        }
        public ListVendorRate UpdateRate(int rateId, string lang, string custoemrPath, string ProdPath, string Relpey, int userid, string FireBaseKey)
        {
            var Data = Uow.OrderRate.GetAll(s => !s.IsDeleted && s.OrderRateID == rateId, "OrderVendor.Orders.Customers").FirstOrDefault();
            Data.IsRepley = true;
            Data.AnswerRate = Relpey;
            Data.UserUpdate = userid;
            Data.UpdateDate = _bLGeneral.GetDateTimeNow();
            Uow.OrderRate.Update(Data);
            var Noti = new Notification()
            {
                CreateDate = _bLGeneral.GetDateTimeNow(),
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
                NotificationDate = _bLGeneral.GetDateTimeNow(),
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
            _bLGeneral.SendPush(UserDataVendor.TokenVal, UserDataVendor.DeviceType, _PushListVendor);
            Uow.Commit();
            return Uow.OrderRate.GetAll(s => !s.IsDeleted && s.OrderRateID == rateId).Select(m => new ListVendorRate()
            {
                customerImage = !string.IsNullOrWhiteSpace(m.OrderVendor.Orders.Customers.ProfilePic) ? custoemrPath + m.OrderVendor.Orders.Customers.ProfilePic : "",
                customerName = m.OrderVendor.Orders.Customers.FirstName + " " + m.OrderVendor.Orders.Customers.SeconedName,
                rateDate = m.CreateDate.ToString("dd/MM/yyyy"),
                rateId = m.OrderRateID,
                rate = m.RateOrder,
                isRepley = m.IsRepley,
                repley = "",
                trackNo = m.OrderVendor.TrackNo,
                rateComment = m.CommentOrder,
                rateTitle = lang == "ar" ? (m.RateOrder <= 1 ? "سيء جدا" : m.RateOrder <= 2 ? "سيء" : m.RateOrder <= 3 ? "مقبول" : m.RateOrder <= 4 ? "ممتاز" : "عالى الجودة") :
                          (m.RateOrder <= 1 ? "Very bad" : m.RateOrder <= 2 ? "Bad" : m.RateOrder <= 3 ? "Acceptable" : m.RateOrder <= 4 ? "Excellent" : "High quality"),
                listProducts = m.OrderVendor.OrderItems.Where(x => !x.IsDeleted).Select(z => new ViewModel.Vendor.ListProductRate()
                {
                    image = !string.IsNullOrWhiteSpace(z.ProdImage) ? ProdPath + z.ProdImage : "",
                    name = lang == "ar" ? z.ProdNameAr : z.ProdNameEn,
                    price = lang == "ar" ? z.Price + " ريـال" : z.Price + " SAR"
                }).ToList()
            }).FirstOrDefault();
        }
        [Obsolete]
        public string ChangeStatus(int ordervedorId, int userid, string lang, int orderStatusID, int? CancelReasonID, string FireBaseKey)
        {
            var Message = "true";
            var OrderData = Uow.OrderVendor.GetAll(s => s.OrderVendorID == ordervedorId && !s.IsDeleted, "OrderStatus,Vendors,Orders.Customers,OrderItems").FirstOrDefault();
            if (OrderData != null)
            {
                try
                {

                    if (orderStatusID != (int)OrderStatusEnum.Shipping)
                    {
                        var OrderStatusData = Uow.OrderStatus.GetAll(s => s.OrderStatusID == orderStatusID).FirstOrDefault();
                        OrderData.OrderStatusID = orderStatusID;
                        OrderData.UpdateDate = _bLGeneral.GetDateTimeNow();
                        OrderData.UserUpdate = userid;
                        OrderData.OrderHistory.Add(new OrderHistory()
                        {
                            UserId = userid,
                            Lat = 0,
                            Lng = 0,
                            OrderStatusID = orderStatusID,
                            OrderHistoryGuid = Guid.NewGuid(),
                            IsEnable = true,
                            IsDeleted = false,
                            CreateDate = _bLGeneral.GetDateTimeNow(),
                        });
                        if (orderStatusID != (int)OrderStatusEnum.Cancel)
                        {
                            OrderData.Notification.Add(new Notification()
                            {
                                CreateDate = _bLGeneral.GetDateTimeNow(),
                                CustomersID = OrderData.Orders.CustomersID,
                                TitleAR = "تم تعديل حالة طلبك رقم " + OrderData.OrderVendorID + " الى " + OrderStatusData.NameAR + "",
                                TitleEN = "your order has been updated to " + OrderStatusData.NameEN + "",
                                DescAR = "تم تعديل حالة الطلب رقم " + OrderData.OrderVendorID + " من خلال " + OrderData.Vendors.StoreNameAr + " في تاريخ " + _bLGeneral.GetDateTimeNow() + " لتكون الحالة " + OrderStatusData.NameAR + "",
                                DescEN = "order No " + OrderData.TrackNo + " has been updated from " + OrderData.Vendors.StoreNameEn + " on date " + _bLGeneral.GetDateTimeNow() + " to be " + OrderStatusData.NameEN + "",
                                IsDeleted = false,
                                IsEnable = true,
                                IsSeen = false,
                                NotificationGuid = Guid.NewGuid(),
                                NotificationTypeID = (int)NotificationTypeEnum.Order,
                                UserId = userid,
                                NotificationDate = _bLGeneral.GetDateTimeNow(),
                                OrderVendorID = OrderData.OrderVendorID,
                            });
                            var _PushListVendor = new PushList()
                            {
                                orderId = OrderData.OrderVendorID,
                                lat = 0,
                                lng = 0,
                                trackNo = OrderData.OrderVendorID.ToString(),
                                profit = "",
                                cusotmerAddress = "",
                                vendorAddress = "",
                                vendorLogo = "",
                                vendorName = "",
                                body = "تم تعديل حالة طلبك رقم " + OrderData.TrackNo + " الى " + OrderStatusData.NameAR + "",
                                sound = "default", //For IOS
                                title = "تغيير حالة الطلب",
                                content_available = "true", //For Android
                                priority = "high", //For Android,
                                serverKey = FireBaseKey,
                                estimation = 20,
                                pushType = (int)PushTypeEnum.Order
                            };
                            var UserDataVendor = _blTokens.GetMobileDataByUserID(OrderData.Orders.Customers.UserId);
                            _bLGeneral.SendPush(UserDataVendor.TokenVal, UserDataVendor.DeviceType, _PushListVendor);
                        }
                        if (orderStatusID == (int)OrderStatusEnum.Delivered || orderStatusID == (int)OrderStatusEnum.Cancel)
                        {
                            var OrderMasterData = Uow.Orders.GetAll(s => s.OrdersID == OrderData.OrdersID).FirstOrDefault();
                            if (orderStatusID == (int)OrderStatusEnum.Cancel)
                            {
                                if (CancelReasonID == (int)CancelReasonEnum.Return_to_Wallet)
                                {
                                    _BlCustomerBalance.InsertBalanceWithOutCommit((int)TypeOperationEnum.CR, OrderData.Total, (int)TransactionTypeEnum.Pay_Order, OrderMasterData.CustomersID
                                    , userid, OrderData.OrderVendorID, _bLGeneral.GetDateTimeNow(), "", "", 0);
                                    OrderData.InvoiceTypeId = (int)OrderInvoiceTypeEnum.Non_Invoice;
                                    #region Noti
                                    OrderData.Notification.Add(new Notification()
                                    {
                                        CreateDate = _bLGeneral.GetDateTimeNow(),
                                        CustomersID = OrderData.Orders.CustomersID,
                                        TitleAR = "تم الغاء طلبك رقم " + OrderData.OrderVendorID + " و إرجاع مبلغ الطلب للمحفظة",
                                        TitleEN = "your order has been cancelled and order amount returned to your wallet",
                                        DescAR = "تم الغاء الطلب رقم " + OrderData.OrderVendorID + " في تاريخ " + _bLGeneral.GetDateTimeNow() + "",
                                        DescEN = "order No " + OrderData.TrackNo + " has been cancelled on date " + _bLGeneral.GetDateTimeNow() + "",
                                        IsDeleted = false,
                                        IsEnable = true,
                                        IsSeen = false,
                                        NotificationGuid = Guid.NewGuid(),
                                        NotificationTypeID = (int)NotificationTypeEnum.Order,
                                        UserId = userid,
                                        NotificationDate = _bLGeneral.GetDateTimeNow(),
                                        OrderVendorID = OrderData.OrderVendorID,
                                    });
                                    var _PushListVendor = new PushList()
                                    {
                                        orderId = OrderData.OrderVendorID,
                                        lat = 0,
                                        lng = 0,
                                        trackNo = OrderData.OrderVendorID.ToString(),
                                        profit = "",
                                        cusotmerAddress = "",
                                        vendorAddress = "",
                                        vendorLogo = "",
                                        vendorName = "",
                                        body = "تم الغاء طلبك رقم " + OrderData.OrderVendorID + " و إرجاع مبلغ الطلب للمحفظة",
                                        sound = "default", //For IOS
                                        title = "الغاء الطلب",
                                        content_available = "true", //For Android
                                        priority = "high", //For Android,
                                        serverKey = FireBaseKey,
                                        estimation = 20,
                                        pushType = (int)PushTypeEnum.Order
                                    };
                                    var UserDataVendor = _blTokens.GetMobileDataByUserID(OrderData.Orders.Customers.UserId);
                                    _bLGeneral.SendPush(UserDataVendor.TokenVal, UserDataVendor.DeviceType, _PushListVendor);
                                    #endregion
                                    #region Daily_Quantity
                                    if (OrderData.OrderStatusID == (int)OrderStatusEnum.Accept || OrderData.OrderStatusID == (int)OrderStatusEnum.Assign || OrderData.OrderStatusID == (int)OrderStatusEnum.Being_Processed
                                        || OrderData.OrderStatusID == (int)OrderStatusEnum.Create || OrderData.OrderStatusID == (int)OrderStatusEnum.OnWay_Store || OrderData.OrderStatusID == (int)OrderStatusEnum.Shipping)
                                    {
                                        foreach (var item in OrderData.OrderItems)
                                        {
                                            var ProdData = Uow.Product.GetAll(s => s.ProductID == item.ProductID).FirstOrDefault();
                                            if (ProdData.IsLimited)
                                            {
                                                ProdData.DailyQuantity = ProdData.DailyQuantity + item.Quantity;
                                                Uow.Product.Update(ProdData);
                                            }
                                        }
                                    }
                                    #endregion
                                }
                                else
                                {
                                    #region Noti
                                    OrderData.Notification.Add(new Notification()
                                    {
                                        CreateDate = _bLGeneral.GetDateTimeNow(),
                                        CustomersID = OrderData.Orders.CustomersID,
                                        TitleAR = "تم الغاء طلبك رقم " + OrderData.OrderVendorID + "",
                                        TitleEN = "your order has been cancelled",
                                        DescAR = "تم الغاء الطلب رقم " + OrderData.OrderVendorID + " في تاريخ " + _bLGeneral.GetDateTimeNow() + "",
                                        DescEN = "order No " + OrderData.OrderVendorID + " has been cancelled on date " + _bLGeneral.GetDateTimeNow() + "",
                                        IsDeleted = false,
                                        IsEnable = true,
                                        IsSeen = false,
                                        NotificationGuid = Guid.NewGuid(),
                                        NotificationTypeID = (int)NotificationTypeEnum.Order,
                                        UserId = userid,
                                        NotificationDate = _bLGeneral.GetDateTimeNow(),
                                        OrderVendorID = OrderData.OrderVendorID,
                                    });
                                    var _PushListVendor = new PushList()
                                    {
                                        orderId = OrderData.OrderVendorID,
                                        lat = 0,
                                        lng = 0,
                                        trackNo = OrderData.OrderVendorID.ToString(),
                                        profit = "",
                                        cusotmerAddress = "",
                                        vendorAddress = "",
                                        vendorLogo = "",
                                        vendorName = "",
                                        body = "تم الغاء طلبك رقم " + OrderData.OrderVendorID + "",
                                        sound = "default", //For IOS
                                        title = "الغاء الطلب",
                                        content_available = "true", //For Android
                                        priority = "high", //For Android,
                                        serverKey = FireBaseKey,
                                        estimation = 20,
                                        pushType = (int)PushTypeEnum.Order
                                    };
                                    var UserDataVendor = _blTokens.GetMobileDataByUserID(OrderData.Orders.Customers.UserId);
                                    _bLGeneral.SendPush(UserDataVendor.TokenVal, UserDataVendor.DeviceType, _PushListVendor);
                                    #endregion
                                    OrderData.InvoiceTypeId = (int)OrderInvoiceTypeEnum.Pending_Invoice;
                                    if (OrderData.DriversID != null)
                                    {
                                        var DeliverySetting = Uow.DeliverySetting.GetAll(s => !s.IsDeleted).FirstOrDefault();
                                        if (DeliverySetting != null)
                                        {
                                            var DriverCommision = DeliverySetting.DriverCommision; 
                                            var Distance = OrderData.DistanceKM != null ? (decimal)OrderData.DistanceKM : 0;
                                            if (Distance > DeliverySetting.MinKM)
                                            {
                                                var OverKm = Math.Round(Distance - DeliverySetting.MinKM, 2);
                                                var OverKmPrice = OverKm * DeliverySetting.OverKmFare;
                                                DriverCommision = Math.Round(DriverCommision + OverKmPrice, 2);
                                            }
                                            _BlDriverBalance.InsertBalanceWithOutCommit((int)TypeOperationEnum.CR, DriverCommision, (int)TransactionTypeEnum.Delivery_Order, (int)OrderData.DriversID, userid, ordervedorId, _bLGeneral.GetDateTimeNow(), "", "");
                                            OrderData.DriverCharge = DriverCommision;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                _BlVendorBalance.InsertBalanceWithOutCommit((int)TypeOperationEnum.CR, OrderData.Total, (int)TransactionTypeEnum.Pay_Order, OrderData.VendorsID
                           , userid, OrderData.OrderVendorID, _bLGeneral.GetDateTimeNow(), "", "");
                                OrderData.InvoiceTypeId = (int)OrderInvoiceTypeEnum.Pending_Invoice;
                            }
                            OrderMasterData.OrderStatusID = orderStatusID;
                            OrderMasterData.UpdateDate = _bLGeneral.GetDateTimeNow();
                            OrderMasterData.UserId = userid;
                            Uow.Orders.Update(OrderMasterData);
                        }
                        Uow.OrderVendor.Update(OrderData);
                        Uow.Commit();
                    }
                    else
                    {
                        if (Uow.ShippingCompany.GetAll(s => s.IsAuto && s.ShippingEnum == (int)ShippingCompanyEnum.Shourq).Any())
                        {
                            _BlOrders.AssignOrderVendorToShrouq(ordervedorId, Uow.ShippingCompany.GetAll(s => s.ShippingEnum == (int)ShippingCompanyEnum.Shourq).FirstOrDefault().ShippingCompanyID
                                , (int)CaptainTypeEnum.Shipping_Company, userid);
                        }
                        else if (Uow.ShippingCompany.GetAll(s => s.IsAuto && s.ShippingEnum == (int)ShippingCompanyEnum.Lavender).Any())
                        {
                            _BlOrders.AssigLavender(ordervedorId, Uow.ShippingCompany.GetAll(s => s.ShippingEnum == (int)ShippingCompanyEnum.Lavender).FirstOrDefault().ShippingCompanyID
                                , (int)CaptainTypeEnum.Shipping_Company, userid);
                        }
                        else if (Uow.ShippingCompany.GetAll(s => s.IsAuto && s.ShippingEnum == (int)ShippingCompanyEnum.Barq).Any())
                        {
                            _BlOrders.AssigBarq(ordervedorId, Uow.ShippingCompany.GetAll(s => s.ShippingEnum == (int)ShippingCompanyEnum.Barq).FirstOrDefault().ShippingCompanyID
                               , (int)CaptainTypeEnum.Shipping_Company, userid);
                        }
                    }
                }
                catch (Exception ex)
                {

                    Message = ex.Message;
                }
            }
            else
            {
                Message = lang == "ar" ? "خطأ في رقم الطلب" : "error in order Id";
            }
            return Message;
        }
        public string ChangeStatusShipping(int ordervedorId, int userid, string lang, int orderStatusID, string FireBaseKey)
        {
            var Message = "true";
            var OrderData = Uow.OrderVendor.GetAll(s => s.OrderVendorID == ordervedorId && !s.IsDeleted, "ShippingCompany,OrderStatus,Vendors,Orders.Customers,OrderItems").FirstOrDefault();
            if (OrderData != null)
            {
                try
                {
                    var OrderStatusData = Uow.OrderStatus.GetAll(s => s.OrderStatusID == orderStatusID).FirstOrDefault();
                    OrderData.OrderStatusID = orderStatusID;
                    OrderData.UpdateDate = _bLGeneral.GetDateTimeNow();
                    OrderData.UserUpdate = userid;
                    OrderData.OrderHistory.Add(new OrderHistory()
                    {
                        UserId = userid,
                        Lat = 0,
                        Lng = 0,
                        OrderStatusID = orderStatusID,
                        OrderHistoryGuid = Guid.NewGuid(),
                        IsEnable = true,
                        IsDeleted = false,
                        CreateDate = _bLGeneral.GetDateTimeNow(),
                    });
                    if (orderStatusID != (int)OrderStatusEnum.Cancel)
                    {
                        OrderData.Notification.Add(new Notification()
                        {
                            CreateDate = _bLGeneral.GetDateTimeNow(),
                            CustomersID = OrderData.Orders.CustomersID,
                            TitleAR = "تم تعديل حالة طلبك رقم " + OrderData.OrderVendorID + " الى " + OrderStatusData.NameAR + "",
                            TitleEN = "your order has been updated to " + OrderStatusData.NameEN + "",
                            DescAR = "تم تعديل حالة الطلب رقم " + OrderData.OrderVendorID + " من خلال " + OrderData.Vendors.StoreNameAr + " في تاريخ " + _bLGeneral.GetDateTimeNow() + " لتكون الحالة " + OrderStatusData.NameAR + "",
                            DescEN = "order No " + OrderData.TrackNo + " has been updated from " + OrderData.Vendors.StoreNameEn + " on date " + _bLGeneral.GetDateTimeNow() + " to be " + OrderStatusData.NameEN + "",
                            IsDeleted = false,
                            IsEnable = true,
                            IsSeen = false,
                            NotificationGuid = Guid.NewGuid(),
                            NotificationTypeID = (int)NotificationTypeEnum.Order,
                            UserId = userid,
                            NotificationDate = _bLGeneral.GetDateTimeNow(),
                            OrderVendorID = OrderData.OrderVendorID,
                        });
                        var _PushListVendor = new PushList()
                        {
                            orderId = OrderData.OrderVendorID,
                            lat = 0,
                            lng = 0,
                            trackNo = OrderData.OrderVendorID.ToString(),
                            profit = "",
                            cusotmerAddress = "",
                            vendorAddress = "",
                            vendorLogo = "",
                            vendorName = "",
                            body = "تم تعديل حالة طلبك رقم " + OrderData.TrackNo + " الى " + OrderStatusData.NameAR + "",
                            sound = "default", //For IOS
                            title = "تغيير حالة الطلب",
                            content_available = "true", //For Android
                            priority = "high", //For Android,
                            serverKey = FireBaseKey,
                            estimation = 20,
                            pushType = (int)PushTypeEnum.Order
                        };
                        var UserDataVendor = _blTokens.GetMobileDataByUserID(OrderData.Orders.Customers.UserId);
                        _bLGeneral.SendPush(UserDataVendor.TokenVal, UserDataVendor.DeviceType, _PushListVendor);
                    }
                    var IsCancel = true;
                    if (OrderData.CaptainTypeId == (int)CaptainTypeEnum.Shipping_Company)
                    {
                        if (OrderData.ShippingCompany.ShippingEnum == (int)ShippingCompanyEnum.Barq)
                        {
                            IsCancel = _BlOrders.CancelBarq(OrderData.OrderVendorID, userid);
                        }
                        else if (OrderData.ShippingCompany.ShippingEnum == (int)ShippingCompanyEnum.Lavender)
                        {
                            IsCancel = _BlOrders.CancelLavender(OrderData.OrderVendorID, userid);
                        }
                        else if (OrderData.ShippingCompany.ShippingEnum == (int)ShippingCompanyEnum.Shourq)
                        {
                            IsCancel = _BlOrders.CancelShoruq(OrderData.OrderVendorID, userid);
                        }
                    }
                    if (IsCancel)
                    {
                        Uow.OrderVendor.Update(OrderData);
                        Uow.Commit();
                    }
                    else
                    {
                        Message = "Can't Cancel from Shipping Compnay";
                    }

                }
                catch (Exception ex)
                {

                    Message = ex.Message;
                }
            }
            else
            {
                Message = lang == "ar" ? "خطأ في رقم الطلب" : "error in order Id";
            }
            return Message;
        }
        public string DeliveredChangeStatus(int ordervedorId, int userid, string lang, int orderStatusID, int DriverId, double lat, double lng, string routeImagefileName, string FireBaseKey)
        {
            var Message = "true";
            var OrderData = Uow.OrderVendor.GetAll(s => s.OrderVendorID == ordervedorId && !s.IsDeleted, "OrderStatus,Vendors,Orders.Customers").FirstOrDefault();
            if (OrderData != null)
            {
                try
                {
                    var OrderStatusData = Uow.OrderStatus.GetAll(s => s.OrderStatusID == orderStatusID).FirstOrDefault();

                    OrderData.OrderStatusID = orderStatusID;
                    OrderData.UpdateDate = _bLGeneral.GetDateTimeNow();
                    OrderData.UserUpdate = userid;
                    if (!string.IsNullOrWhiteSpace(routeImagefileName))
                    {
                        OrderData.InvoiceImage = routeImagefileName;
                    }
                    OrderData.OrderHistory.Add(new OrderHistory()
                    {
                        UserId = userid,
                        Lat = lat,
                        Lng = lng,
                        OrderStatusID = orderStatusID,
                        OrderHistoryGuid = Guid.NewGuid(),
                        IsEnable = true,
                        IsDeleted = false,
                        CreateDate = _bLGeneral.GetDateTimeNow(),
                    });
                    if (orderStatusID == (int)OrderStatusEnum.Delivered)
                    {
                        _BlVendorBalance.InsertBalanceWithOutCommit((int)TypeOperationEnum.CR, OrderData.Total, (int)TransactionTypeEnum.Pay_Order, OrderData.VendorsID
                            , userid, OrderData.OrderVendorID, _bLGeneral.GetDateTimeNow(), "", "");
                        OrderData.InvoiceTypeId = (int)OrderInvoiceTypeEnum.Pending_Invoice;
                        //Customer
                        OrderData.Notification.Add(new Notification()
                        {
                            CreateDate = _bLGeneral.GetDateTimeNow(),
                            CustomersID = OrderData.Orders.CustomersID,
                            TitleAR = "تم تسليمكم الطلب بنجاح ، فضلا يرجى تقييم الخدمة لتحسين مستوى الخدمة.",
                            TitleEN = "Your request has been successfully submitted, please rate the service to improve the level of service.",
                            DescAR = "تم توصيل طلبك رقم " + OrderData.OrderVendorID + " من خلال " + OrderData.Vendors.StoreNameAr + " في تاريخ " + _bLGeneral.GetDateTimeNow() + "",
                            DescEN = "order No " + OrderData.OrderVendorID + " has been delieverd from " + OrderData.Vendors.StoreNameEn + " on date " + _bLGeneral.GetDateTimeNow() + "",
                            IsDeleted = false,
                            IsEnable = true,
                            IsSeen = false,
                            NotificationGuid = Guid.NewGuid(),
                            NotificationTypeID = (int)NotificationTypeEnum.Order,
                            UserId = userid,
                            NotificationDate = _bLGeneral.GetDateTimeNow(),
                        });
                        //Driver
                        OrderData.Notification.Add(new Notification()
                        {
                            CreateDate = _bLGeneral.GetDateTimeNow(),
                            DriversID = DriverId,
                            TitleAR = "تم توصيل الطلب رقم " + OrderData.OrderVendorID + "",
                            TitleEN = "your order has been delieverd",
                            DescAR = "تم توصيل الطلب رقم " + OrderData.OrderVendorID + " إلي المستلم " + OrderData.Orders.Customers.FirstName + " في تاريخ " + _bLGeneral.GetDateTimeNow() + "",
                            DescEN = "order No " + OrderData.TrackNo + " has been delieverd to receiver " + OrderData.Orders.Customers.FirstName + " on date " + _bLGeneral.GetDateTimeNow() + "",
                            IsDeleted = false,
                            IsEnable = true,
                            IsSeen = false,
                            NotificationGuid = Guid.NewGuid(),
                            NotificationTypeID = (int)NotificationTypeEnum.Order,
                            UserId = userid,
                            NotificationDate = _bLGeneral.GetDateTimeNow(),
                        });
                        var _PushListVendor = new PushList()
                        {
                            orderId = OrderData.OrderVendorID,
                            lat = 0,
                            lng = 0,
                            trackNo = OrderData.OrderVendorID.ToString(),
                            profit = "",
                            cusotmerAddress = "",
                            vendorAddress = "",
                            vendorLogo = "",
                            vendorName = "",
                            body = "تم تسليمكم الطلب بنجاح ، فضلا يرجى تقييم الخدمة لتحسين مستوى الخدمة.",
                            sound = "default", //For IOS
                            title = "توصيل الطلب",
                            content_available = "true", //For Android
                            priority = "high", //For Android,
                            serverKey = FireBaseKey,
                            estimation = 20,
                            pushType = (int)PushTypeEnum.Order
                        };




                        var DeliverySetting = Uow.DeliverySetting.GetAll(s => !s.IsDeleted).FirstOrDefault();
                        if (DeliverySetting != null)
                        {
                            var DriverCommision = DeliverySetting.DriverCommision;
                            var Distance = OrderData.DistanceKM != null ? (decimal)OrderData.DistanceKM : 0;
                            if (Distance > DeliverySetting.MinKM)
                            {
                                var OverKm = Math.Round(Distance - DeliverySetting.MinKM, 2);
                                var OverKmPrice = OverKm * DeliverySetting.OverKmFare;
                                DriverCommision = Math.Round(DriverCommision + OverKmPrice, 2);
                            }
                            _BlDriverBalance.InsertBalanceWithOutCommit((int)TypeOperationEnum.CR, DriverCommision, (int)TransactionTypeEnum.Delivery_Order, DriverId, userid, ordervedorId, _bLGeneral.GetDateTimeNow(), "", "");
                            OrderData.DriverCharge = DriverCommision;
                        }

                        try
                        {
                            var _PushList = new PushList()
                            {
                                orderId = OrderData.OrderVendorID,
                                lat = 0,
                                lng = 0,
                                trackNo = OrderData.OrderVendorID.ToString(),
                                profit = "",
                                cusotmerAddress = "",
                                vendorAddress = "",
                                vendorLogo = "",
                                vendorName = "",
                                body = "تم تسليم الطلب إلي المستلم بنجاح.",
                                sound = "default", //For IOS
                                title = "تغيير حالة الطلب",
                                content_available = "true", //For Android
                                priority = "high", //For Android,
                                serverKey = FireBaseKey,
                                estimation = 20,
                                pushType = (int)PushTypeEnum.Order
                            };

                            var UserDataVendor = _blTokens.GetMobileDataByUserID(OrderData.Orders.Customers.UserId);
                            if (UserDataVendor != null)
                            {
                                _bLGeneral.SendPush(UserDataVendor.TokenVal, UserDataVendor.DeviceType, _PushListVendor);
                            }


                            var UserData = _blTokens.GetMobileDataByUserID(OrderData.Vendors.UserId);
                            if (UserData != null)
                            {
                                _bLGeneral.SendPush(UserData.TokenVal, UserData.DeviceType, _PushList);
                            }

                        }
                        catch (Exception w)
                        {
                            Message = w.Message;
                        }
                    }
                    else
                    {
                        OrderData.Notification.Add(new Notification()
                        {
                            CreateDate = _bLGeneral.GetDateTimeNow(),
                            CustomersID = OrderData.Orders.CustomersID,
                            TitleAR = "تم تعديل حالة طلبك رقم " + OrderData.OrderVendorID + " الى " + OrderStatusData.NameAR + "",
                            TitleEN = "your order has been updated to " + OrderStatusData.NameEN + "",
                            DescAR = "تم تعديل حالة الطلب رقم " + OrderData.TrackNo + " من خلال " + OrderData.Vendors.StoreNameAr + " في تاريخ " + _bLGeneral.GetDateTimeNow() + " لتكون الحالة " + OrderStatusData.NameAR + "",
                            DescEN = "order No " + OrderData.TrackNo + " has been updated from " + OrderData.Vendors.StoreNameEn + " on date " + _bLGeneral.GetDateTimeNow() + " to be " + OrderData.OrderStatus.NameEN + "",
                            IsDeleted = false,
                            IsEnable = true,
                            IsSeen = false,
                            NotificationGuid = Guid.NewGuid(),
                            NotificationTypeID = (int)NotificationTypeEnum.Order,
                            UserId = userid,
                            NotificationDate = _bLGeneral.GetDateTimeNow(),
                        });
                        var _PushListVendor = new PushList()
                        {
                            orderId = OrderData.OrderVendorID,
                            lat = 0,
                            lng = 0,
                            trackNo = OrderData.OrderVendorID.ToString(),
                            profit = "",
                            cusotmerAddress = "",
                            vendorAddress = "",
                            vendorLogo = "",
                            vendorName = "",
                            body = "تم تعديل حالة طلبك رقم " + OrderData.OrderVendorID + " الى " + OrderStatusData.NameAR + "",
                            sound = "default", //For IOS
                            title = "تغيير حالة الطلب",
                            content_available = "true", //For Android
                            priority = "high", //For Android,
                            serverKey = FireBaseKey,
                            estimation = 20,
                            pushType = (int)PushTypeEnum.Order
                        };
                        var UserDataVendor = _blTokens.GetMobileDataByUserID(OrderData.Orders.Customers.UserId);
                        if (UserDataVendor != null)
                        {
                            _bLGeneral.SendPush(UserDataVendor.TokenVal, UserDataVendor.DeviceType, _PushListVendor);

                        }
                    }
                    Uow.OrderVendor.Update(OrderData);
                    Uow.Commit();
                }
                catch (Exception ex)
                {

                    Message = ex.Message;
                }
            }
            else
            {
                Message = lang == "ar" ? "خطأ في رقم الطلب" : "error in order Id";
            }
            return Message;
        }
        public VendorHomeViewModelApi GetVendorHome(string lang, int vendorId, string custoemrPath)
        {
            var model = new VendorHomeViewModelApi();
            var vendordata = Uow.Vendors.GetAll(s => !s.IsDeleted && s.VendorsID == vendorId).FirstOrDefault();
            var orders = Uow.OrderVendor.GetAll(s => !s.IsDeleted && s.VendorsID == vendorId);
            model.monthlyTarget = orders.Where(x => x.OrderStatusID == (int)OrderStatusEnum.Delivered && x.CreateDate.Month == _bLGeneral.GetDateTimeNow().Month
            && x.CreateDate.Year == _bLGeneral.GetDateTimeNow().Year).Sum(m => m.PerStore);
            model.monthlyTargetPercent = Math.Round((vendordata.MonthlyTarget != 0 ? ((model.monthlyTarget / vendordata.MonthlyTarget) * 100) : 0), 2);
            model.monthlyTargetSuccess = model.monthlyTargetPercent >= 100 ? true : false;
            model.overTarget = vendordata.MonthlyTarget;
            model.enterMonthlyTarget = vendordata.MonthlyTarget;
            model.cutomers = orders.Select(s => s.Orders.CustomersID).Distinct().Count();
            model.sales = orders.Where(s => s.OrderStatusID == (int)OrderStatusEnum.Delivered).Count();
            model.orders = orders.Count();
            model.paymentCards = orders.Any() ? orders.Where(s => s.InvoiceTypeId == (int)OrderInvoiceTypeEnum.Pending_Invoice).Sum(s => s.PerStore) : 0;
            model.bankTransfer = orders.Any() ? orders.Where(s => s.InvoiceTypeId == (int)OrderInvoiceTypeEnum.Invoiced || s.InvoiceTypeId == (int)OrderInvoiceTypeEnum.Cash_Transfered).Sum(s => s.PerStore) : 0;
            model.cash = orders.Any() ? orders.Where(s => s.InvoiceTypeId != (int)OrderInvoiceTypeEnum.Non_Invoice).Sum(s => s.PerStore) : 0;
            model.orderList = VendorOrders(lang, 1, custoemrPath, vendorId, 1).listOrders;
            return model;
        }
        public VendorOrdersViewModelApi VendorOrders(string lang, int page, string custoemrPath, int vendorId, int type)
        {
            var Data = Uow.OrderVendor.GetAll(s => !s.IsDeleted && s.VendorsID == vendorId &&
            (type == 0 ? s.OrderStatusID == (int)OrderStatusEnum.Cancel :
            type == 1 ? s.OrderStatusID == (int)OrderStatusEnum.Create :
            type == 2 ? s.OrderStatusID == (int)OrderStatusEnum.Accept :
            type == 3 ? s.OrderStatusID == (int)OrderStatusEnum.Being_Processed :
            type == 4 ? (s.OrderStatusID == (int)OrderStatusEnum.Shipping || s.OrderStatusID == (int)OrderStatusEnum.Assign) :
            type == 5 ? s.OrderStatusID == (int)OrderStatusEnum.Being_Delivery : s.OrderStatusID == (int)OrderStatusEnum.Delivered)).Select(m => new ListProductVendor()
            {
                customerImage = !string.IsNullOrWhiteSpace(m.Orders.Customers.ProfilePic) ? custoemrPath + m.Orders.Customers.ProfilePic : "",
                orderId = m.OrderVendorID,
                customerName = m.Orders.Customers.FirstName + " " + m.Orders.Customers.SeconedName,
                customerAddress = m.Orders.CustomerLocation.Address,
                blockName = lang == "ar" ? m.Orders.CustomerLocation.Block.NameAR : m.Orders.CustomerLocation.Block.NameEN,
                paymentType = lang == "ar" ? (m.Orders.PaymentTypeId == (int)PaymentTypeEnum.COD ? "نقد عند الإستلام" : m.Orders.PaymentTypeId == (int)PaymentTypeEnum.Payment_Card ? "بطاقة دفع" :
                m.Orders.PaymentTypeId == (int)PaymentTypeEnum.Bank_Transfer ? "تحويل بنكي" : "المحفظة") :
                (m.Orders.PaymentTypeId == (int)PaymentTypeEnum.COD ? "Cash on Receive" : m.Orders.PaymentTypeId == (int)PaymentTypeEnum.Payment_Card ? "Payment Card" :
                m.Orders.PaymentTypeId == (int)PaymentTypeEnum.Bank_Transfer ? "Bank Transfer" : "Wallet"),
                date = m.CreateDate.ToString("dd/MM/yyyy"),
                time = m.CreateDate.ToString("hh:mm tt"),
                trackNo = m.TrackNo,
                price = lang == "ar" ? m.Price + " ريـال" : m.Price + " SAR",
                paymentStatus = lang == "ar" ? (m.Orders.PaymentTypeId == (int)PaymentTypeEnum.COD ? "لم يتم الدفع" : "تم الدفع") : m.Orders.PaymentTypeId == (int)PaymentTypeEnum.COD ? "Un Paid" : "Paid",
                colorHex = m.Orders.PaymentTypeId == (int)PaymentTypeEnum.COD ? "#FFF9D9" : "#E5F9ED",
                colorText = m.Orders.PaymentTypeId == (int)PaymentTypeEnum.COD ? "#FFBB00" : "#00BE4C",
                createDate = m.CreateDate,
                canceledBy = m.OrderStatusID == (int)OrderStatusEnum.Cancel ?
                m.OrderHistory.FirstOrDefault(s => s.OrderStatusID == (int)OrderStatusEnum.Cancel) != null ?
                (m.OrderHistory.FirstOrDefault(s => s.OrderStatusID == (int)OrderStatusEnum.Cancel).User.UserType == (int)UserTypeEnum.Customer ?
                (lang == "ar" ? "تم الإلغاء بواسطة المستخدم" : "Canceled By Customer") : m.OrderHistory.FirstOrDefault(s => s.OrderStatusID == (int)OrderStatusEnum.Cancel).User.UserType == (int)UserTypeEnum.Vendor ?
                (lang == "ar" ? "تم الإلغاء بواسطة المتجر" : "Canceled By Store") : (lang == "ar" ? "تم الإلغاء بواسطة الأدمن" : "Canceled By Admin"))
                : (lang == "ar" ? "تم الإلغاء بواسطة المتجر" : "Canceled By Store") : "",
                isallawed =
                m.CaptainTypeId == (int)CaptainTypeEnum.Home_Made ?
                (
                m.OrderStatusID == (int)OrderStatusEnum.Create ||
                m.OrderStatusID == (int)OrderStatusEnum.Accept ||
                m.OrderStatusID == (int)OrderStatusEnum.Being_Processed
                ? true : false
                )

                : true,
                orderType = lang == "ar" ? (m.OrderTypeId == (int)OrderTypeEnum.Now ? "طلب فورى" : "طلب مجدول - " + (m.ScheduleDate != null ? m.ScheduleDate.Value.ToString("dd/MM/yyyy") + " " + m.ScheduleTime.Value.ToString("hh:mm tt") : "")) : (m.OrderTypeId == (int)OrderTypeEnum.Now ? "Now Order" : "Schedule Order - " + (m.ScheduleDate != null ? m.ScheduleDate.Value.ToString("dd/MM/yyyy") + " " + m.ScheduleTime.Value.ToString("hh:mm tt") : "")),
                orderVendorID = m.OrderVendorID,

            }).ToList();
            Data = Data.Skip(Convert.ToInt32(page - 1) * 10).ToList();
            var DataTake = Data.Take(((Convert.ToInt32(page) * 10)) - (Convert.ToInt32(page - 1) * 10)).ToList();
            DataTake = DataTake.OrderByDescending(s => s.createDate).ToList();
            bool NextPage = false;
            if (Data.Count() > 10)
            {
                NextPage = true;
            }
            var model = new VendorOrdersViewModelApi();
            model.listOrders = DataTake;
            model.isNextPage = NextPage;
            model.isAllow = true;
            return model;
        }
        public VendorOrdersViewModelApi InvoicesVendorOrders(string lang, int page, string custoemrPath, int vendorId, bool type, DateTime? fromdate, DateTime? todate, string search)
        {
            var Data = Uow.OrderVendor.GetAll(s => !s.IsDeleted && s.VendorsID == vendorId &&
            (type == true ? (s.InvoiceTypeId == (int)OrderInvoiceTypeEnum.Cash_Transfered || s.InvoiceTypeId == (int)OrderInvoiceTypeEnum.Invoiced)
            : s.InvoiceTypeId == (int)OrderInvoiceTypeEnum.Pending_Invoice)).Select(m => new ListProductVendor()
            {
                customerImage = !string.IsNullOrWhiteSpace(m.Orders.Customers.ProfilePic) ? custoemrPath + m.Orders.Customers.ProfilePic : "",
                orderId = m.OrderVendorID,
                customerName = m.Orders.Customers.FirstName + " " + m.Orders.Customers.SeconedName,
                customerAddress = m.Orders.CustomerLocation.Address,
                blockName = lang == "ar" ? m.Orders.CustomerLocation.Block.NameAR : m.Orders.CustomerLocation.Block.NameEN,
                paymentType = lang == "ar" ? (m.Orders.PaymentTypeId == (int)PaymentTypeEnum.COD ? "نقد عند الإستلام" : m.Orders.PaymentTypeId == (int)PaymentTypeEnum.Payment_Card ? "بطاقة دفع" :
                m.Orders.PaymentTypeId == (int)PaymentTypeEnum.Bank_Transfer ? "تحويل بنكي" : "المحفظة") :
                (m.Orders.PaymentTypeId == (int)PaymentTypeEnum.COD ? "Cash on Receive" : m.Orders.PaymentTypeId == (int)PaymentTypeEnum.Payment_Card ? "Payment Card" :
                m.Orders.PaymentTypeId == (int)PaymentTypeEnum.Bank_Transfer ? "Bank Transfer" : "Wallet"),
                date = m.CreateDate.ToString("dd/MM/yyyy"),
                time = m.CreateDate.ToString("hh:mm tt"),
                trackNo = m.TrackNo,
                createDate = m.CreateDate,
                price = lang == "ar" ? m.Price + " ريـال" : m.Price + " SAR",
                paymentStatus = lang == "ar" ? (m.Orders.PaymentTypeId == (int)PaymentTypeEnum.COD ? "لم يتم الدفع" : "تم الدفع") : m.Orders.PaymentTypeId == (int)PaymentTypeEnum.COD ? "Un Paid" : "Paid",
                colorHex = m.Orders.PaymentTypeId == (int)PaymentTypeEnum.COD ? "#FFF9D9" : "#E5F9ED",
                colorText = m.Orders.PaymentTypeId == (int)PaymentTypeEnum.COD ? "#FFBB00" : "#00BE4C",
                canceledBy = m.OrderStatusID == (int)OrderStatusEnum.Cancel ?
                m.OrderHistory.FirstOrDefault(s => s.OrderStatusID == (int)OrderStatusEnum.Cancel) != null ?
                (m.OrderHistory.FirstOrDefault(s => s.OrderStatusID == (int)OrderStatusEnum.Cancel).User.UserType == (int)UserTypeEnum.Customer ?
                (lang == "ar" ? "تم الإلغاء بواسطة المستخدم" : "Canceled By Customer") : (lang == "ar" ? "تم الإلغاء بواسطة المتجر" : "Canceled By Store")) : (lang == "ar" ? "تم الإلغاء بواسطة المتجر" : "Canceled By Store") : "",

            }).ToList();
            if (!string.IsNullOrWhiteSpace(search))
            {
                Data = Data.Where(s => s.trackNo.ToLower().Contains(search.ToLower())).ToList();
            }
            if (fromdate != null)
            {
                Data = Data.Where(s => s.createDate >= fromdate).ToList();
            }
            if (todate != null)
            {
                Data = Data.Where(s => s.createDate <= todate).ToList();
            }
            Data = Data.Skip(Convert.ToInt32(page - 1) * 10).ToList();
            var DataTake = Data.Take(((Convert.ToInt32(page) * 10)) - (Convert.ToInt32(page - 1) * 10)).ToList();
            bool NextPage = false;
            if (Data.Count() > 10)
            {
                NextPage = true;
            }
            var model = new VendorOrdersViewModelApi();
            model.listOrders = DataTake;
            model.isNextPage = NextPage;
            model.isAllow = true;
            return model;
        }
        public decimal GetAmountOrder(int ordervendorId)
        {
            return Uow.OrderVendor.GetAll(s => s.OrderVendorID == ordervendorId).FirstOrDefault().PerStore;
        }
        public Vendors GetById(int id) => Uow.Vendors.GetAll(x => x.VendorsID == id).FirstOrDefault();
        public Vendors GetById(int id, string incluse) => Uow.Vendors.GetAll(x => x.VendorsID == id, incluse).FirstOrDefault();
        public Vendors GetVendorByUserId(int UserId) => Uow.Vendors.GetAll(x => x.UserId == UserId).FirstOrDefault();
        public ProdResonse GetVendorDetails(int VendorID, string vendorpath, string lang, string ProdPath, int page, string search)
        {
            var DeliveryPriceVatPercent = _blConfiguration.GetDeliveryPriceVatPercent();
            var model = new ProdResonse();
            var products = Uow.Product.GetAll(x => !x.IsDeleted && x.VendorsID == VendorID
            && !x.Vendors.IsDeleted && x.Vendors.IsEnable && !x.Vendors.IsVac && !x.IsHidden && x.IsEnable, "Vendors").ToList().Select(s => new ProductList()
            {
                image = !string.IsNullOrWhiteSpace(s.Logo) ? ProdPath + s.Logo : "",
                descripe = lang == "ar" ? s.DescAr : s.DescEn,
                price = Math.Round(_blConfiguration.GetVatForPrice(DeliveryPriceVatPercent, s.Price - _bLGeneral.CalcProductNet(s.Discount, s.StartDiscountDate, s.EndDiscountDate, s.Price)).PriceWithVat, 2),
                discount = s.StartDiscountDate <= DateTime.Now && s.EndDiscountDate >= DateTime.Now ? Math.Round(s.Discount, 2) : 0,
                priceBefor = _blConfiguration.GetVatForPrice(DeliveryPriceVatPercent, s.Price).PriceWithVat,
                discountAmount = _blConfiguration.GetVatForPrice(DeliveryPriceVatPercent, s.Price).PriceWithVat - Math.Round(_blConfiguration.GetVatForPrice(DeliveryPriceVatPercent, s.Price - _bLGeneral.CalcProductNet(s.Discount, s.StartDiscountDate, s.EndDiscountDate, s.Price)).PriceWithVat, 2),
                productId = s.ProductID,
                productName = lang == "ar" ? s.NameAr : s.NameEn,
                vendorId = s.VendorsID,
                vendorName = lang == "ar" ? (s.Vendors.FirstNameAr + " " + s.Vendors.SeconedNameAr) : (s.Vendors.FirstNameEn + " " + s.Vendors.SeconedNameEn),
                isFixed = s.IsFixed,
                isHidden = s.IsHidden,
                vendorLogo = !string.IsNullOrWhiteSpace(s.Vendors.Logo) ? vendorpath + s.Vendors.Logo : "",
                weight = s.Weight,
                quantity = s.DailyQuantity,
                isLimited = s.IsLimited,
                deptName = s.DepartmentsID.ToString(),
                isVendorWorking = IsVendorWorking(s.Vendors.DaysWork, s.Vendors.WorkFrom, s.Vendors.WorkTo, s.Vendors.DaysVac, s.Vendors.VacWorkFrom, s.Vendors.VacWorkTo),
                isVendorWorkingString = lang == "ar" ? (IsVendorWorking(s.Vendors.DaysWork, s.Vendors.WorkFrom, s.Vendors.WorkTo, s.Vendors.DaysVac, s.Vendors.VacWorkFrom, s.Vendors.VacWorkTo) ? "المتجر مفتوح" : "المتجر مغلق") : (IsVendorWorking(s.Vendors.DaysWork, s.Vendors.WorkFrom, s.Vendors.WorkTo, s.Vendors.DaysVac, s.Vendors.VacWorkFrom, s.Vendors.VacWorkTo) ? "Vendor Open" : "Vendor Closed"),

            }
              ).ToList();
            if (!string.IsNullOrWhiteSpace(search))
            {
                products = products.Where(s => s.productName.ToLower().Contains(search.ToLower())).ToList();
            }
            products = products.Skip(Convert.ToInt32(page - 1) * 10).ToList();
            var productsres = products.Take(((Convert.ToInt32(page) * 10)) - (Convert.ToInt32(page - 1) * 10)).ToList();
            bool NextPage = false;
            if (products.Count() > 10)
            {
                NextPage = true;
            }
            model.isNextPage = NextPage;
            model.products = productsres;
            return model;
        }
        public VendorDetailsApi GetVendorDetails(int VendorID, string vendorpath, string lang, string ProdPath, int page)
        {
            var DeliveryPriceVatPercent = _blConfiguration.GetDeliveryPriceVatPercent();
            var Data = Uow.Vendors.GetAll(s => !s.IsDeleted && s.IsEnable && s.VendorsID == VendorID).ToList().Select(s => new VendorDetailsApi()
            {
                banner = !string.IsNullOrWhiteSpace(s.Banner) ? "https://cdn.made-home.com/Home/beet_logo.png" : "https://cdn.made-home.com/Home/beet_logo.png",
                profilePic = !string.IsNullOrWhiteSpace(s.Logo) ? vendorpath + s.Logo : "",
                name = lang == "ar" ? s.StoreNameAr : s.StoreNameEn,
                storeName = lang == "ar" ? s.StoreNameAr : s.StoreNameEn,
                descripe = lang == "ar" ? s.AboutStoreAr : s.AboutStoreEn,
                lat = s.Lat,
                lng = s.Lng,
                address = s.Address,
                rate = 3,
                isVendorWorking = IsVendorWorking(s.DaysWork, s.WorkFrom, s.WorkTo, s.DaysVac, s.VacWorkFrom, s.VacWorkTo),
                isVendorWorkingString = lang == "ar" ? (IsVendorWorking(s.DaysWork, s.WorkFrom, s.WorkTo, s.DaysVac, s.VacWorkFrom, s.VacWorkTo) ? "مفتوح الآن وجاهز لإستقبال الطلبات" : "مغلق الآن") : (IsVendorWorking(s.DaysWork, s.WorkFrom, s.WorkTo, s.DaysVac, s.VacWorkFrom, s.VacWorkTo) ? "Open now and ready to receive orders" : "Closed Now"),

            }).FirstOrDefault();
            var working = GetVendorWorkingTimeByID(VendorID, lang);
            Data.startWork = !string.IsNullOrEmpty(working.DaysWork) ? working.DaysWork : string.Empty;
            Data.vacWork = !string.IsNullOrEmpty(working.DaysVac) ? working.DaysVac : string.Empty;
            var products = Uow.Product.GetAll(x => !x.IsDeleted && !x.Vendors.IsDeleted && x.Vendors.IsEnable && !x.Vendors.IsVac && !x.IsHidden && x.IsEnable && x.VendorsID == VendorID, "Vendors").ToList().Select(s => new ProductList()
            {
                image = !string.IsNullOrWhiteSpace(s.Logo) ? ProdPath + s.Logo : "",
                descripe = lang == "ar" ? s.DescAr : s.DescEn,
                price = Math.Round(_blConfiguration.GetVatForPrice(DeliveryPriceVatPercent, s.Price - _bLGeneral.CalcProductNet(s.Discount, s.StartDiscountDate, s.EndDiscountDate, s.Price)).PriceWithVat, 2),
                discount = s.StartDiscountDate <= DateTime.Now && s.EndDiscountDate >= DateTime.Now ? Math.Round(s.Discount, 2) : 0,
                priceBefor = _blConfiguration.GetVatForPrice(DeliveryPriceVatPercent, s.Price).PriceWithVat,
                discountAmount = _blConfiguration.GetVatForPrice(DeliveryPriceVatPercent, s.Price).PriceWithVat - Math.Round(_blConfiguration.GetVatForPrice(DeliveryPriceVatPercent, s.Price - _bLGeneral.CalcProductNet(s.Discount, s.StartDiscountDate, s.EndDiscountDate, s.Price)).PriceWithVat, 2),
                productId = s.ProductID,
                productName = lang == "ar" ? s.NameAr : s.NameEn,
                vendorId = s.VendorsID,
                vendorName = lang == "ar" ? (s.Vendors.FirstNameAr + " " + s.Vendors.SeconedNameAr) : (s.Vendors.FirstNameEn + " " + s.Vendors.SeconedNameEn),
                isFixed = s.IsFixed,
                isHidden = s.IsHidden,
                vendorLogo = !string.IsNullOrWhiteSpace(s.Vendors.Logo) ? vendorpath + s.Vendors.Logo : "",
                weight = s.Weight,
                quantity = s.DailyQuantity,
                isLimited = s.IsLimited,
                deptName = s.DepartmentsID.ToString(),
                isVendorWorking = IsVendorWorking(s.Vendors.DaysWork, s.Vendors.WorkFrom, s.Vendors.WorkTo, s.Vendors.DaysVac, s.Vendors.VacWorkFrom, s.Vendors.VacWorkTo),
                isVendorWorkingString = lang == "ar" ? (IsVendorWorking(s.Vendors.DaysWork, s.Vendors.WorkFrom, s.Vendors.WorkTo, s.Vendors.DaysVac, s.Vendors.VacWorkFrom, s.Vendors.VacWorkTo) ? "المتجر مفتوح" : "المتجر مغلق") : (IsVendorWorking(s.Vendors.DaysWork, s.Vendors.WorkFrom, s.Vendors.WorkTo, s.Vendors.DaysVac, s.Vendors.VacWorkFrom, s.Vendors.VacWorkTo) ? "Vendor Open" : "Vendor Closed"),

            }
              ).ToList();
            var model = new ProdResonse();
            model.products = products;
            Data.productList = model;
            Data.productList.products = Data.productList.products.Skip(Convert.ToInt32(page - 1) * 10).ToList();
            Data.productList.products = Data.productList.products.Take(((Convert.ToInt32(page) * 10)) - (Convert.ToInt32(page - 1) * 10)).ToList();
            bool NextPage = false;
            if (Data.productList.products.Count() > 10)
            {
                NextPage = true;
            }
            Data.productList.isNextPage = NextPage;
            return Data;
        }
        public Vendors GetVendorsById(int VendorsID)
        {
            return Uow.Vendors.GetAll(x => x.VendorsID == VendorsID && !x.IsDeleted).FirstOrDefault();
        }
        public Vendors GetVendorsByUserId(int UserID)
        {
            return Uow.Vendors.GetAll(x => x.UserId == UserID && !x.IsDeleted).FirstOrDefault();
        }
        public Vendors GetVendorsByMobileNo(string MobileNo, string include)
        {
            return Uow.Vendors.GetAll(x => x.MobileNo == MobileNo && !x.IsDeleted, include).FirstOrDefault();
        }
        public bool IsExistMobile(string Mobile)
        {
            return Uow.Vendors.GetAll(s => s.MobileNo == Mobile && !s.IsDeleted).Any();
        }
        public bool IsExistMobile(string Mobile, int VendorsID)
        {
            return Uow.Vendors.GetAll(s => s.MobileNo == Mobile && s.VendorsID != VendorsID && !s.IsDeleted).Any();
        }
        public bool IsExistEmail(string Email)
        {
            return Uow.Vendors.GetAll(s => s.Email == Email && !s.IsDeleted).Any();
        }
        public bool IsExistEmail(string Email, int VendorsID)
        {
            return Uow.Vendors.GetAll(s => s.Email == Email && s.VendorsID != VendorsID && !s.IsDeleted).Any();
        }
        public bool IsUserNameExists(string username)
        {
            return Uow.User.GetAll(s => s.UserName == username).Count() > 0;
        }
        public List<Banks> GetAllBanks()
        {
            var data = Uow.Banks.GetAll(x => !x.IsDeleted).ToList();
            return data;
        }
        public List<Package> GetAllPackage()
        {
            var data = Uow.Package.GetAll(x => !x.IsDeleted).ToList();
            return data;
        }
        public List<Vendors> GetAllVendors()
        {
            var data = Uow.Vendors.GetAll(x => !x.IsDeleted && x.IsEnable).ToList();
            return data;
        }
        public List<Vendors> GetAllVendorsByInvoicePendingAction()
        {
            var data = Uow.Vendors.GetAll(x => !x.IsDeleted && x.IsEnable
            && x.InvoiceMaster.Any(y => !y.IsDeleted && (y.InvoiceTypeId == (int)InvoiceTypeEnum.Pending_Operation || y.InvoiceTypeId == (int)InvoiceTypeEnum.Pending_Admin))).ToList();
            return data;
        }
        public List<Vendors> GetAllVendorsByInvoiceCollectInvoice()
        {
            var data = Uow.Vendors.GetAll(x => !x.IsDeleted && x.IsEnable
            && x.InvoiceMaster.Any(y => !y.IsDeleted && (y.InvoiceTypeId == (int)InvoiceTypeEnum.Pending_Paid))).ToList();
            return data;
        }
        public class VendorGenerateInvoice
        {
            public string NameAr { get; set; }
            public string NameEn { get; set; }
            public int VendorsID { get; set; }
            public int CountOrders { get; set; }
        }
        public List<VendorGenerateInvoice> GetAllVendors_ToGenerate()
        {
            return Uow.Vendors.GetAll(x => !x.IsDeleted && x.IsEnable && x.OrderVendor.Any(x => !x.IsDeleted && x.InvoiceTypeId == (int)OrderInvoiceTypeEnum.Pending_Invoice)).Select(s => new VendorGenerateInvoice()
            {
                CountOrders = s.OrderVendor.Count(m => m.InvoiceTypeId == (int)OrderInvoiceTypeEnum.Pending_Invoice),
                NameAr = s.StoreNameAr,
                NameEn = s.StoreNameEn,
                VendorsID = s.VendorsID,
            }).ToList();

        }
        public List<Vendors> GetAllVendorsForTrackingOrder()
        {
            var data = Uow.Vendors.GetAll(x => !x.IsDeleted
            && x.IsEnable
            && x.OrderVendor.Any(y => !y.IsDeleted &&
            (y.OrderStatusID == (int)OrderStatusEnum.Assign || y.OrderStatusID == (int)OrderStatusEnum.Being_Delivery || y.OrderStatusID == (int)OrderStatusEnum.OnWay_Store))).ToList();
            return data;
        }
        public List<Vendors> GetAllVendors(string include)
        {
            var data = Uow.Vendors.GetAll(x => !x.IsDeleted && x.IsEnable, include).ToList();
            return data;
        }
        public List<Block> GetAllBlocksByCity(int CityID)
        {
            return Uow.Block.GetAll(x => !x.IsDeleted && x.CityID == CityID && x.IsEnable).ToList();
        }
        public IEnumerable<_Enum> GetGenderEnum(string lang)
        {
            if (lang == "ar")
            {
                List<_Enum> enumData = new List<_Enum>();
                enumData.Add(new _Enum()
                {
                    Value = 0,
                    Text = "ذكر",
                });
                enumData.Add(new _Enum()
                {
                    Value = 1,
                    Text = "انثى",
                });
                enumData.Add(new _Enum()
                {
                    Value = 2,
                    Text = "يفضل عدم التحديد",
                });
                return enumData;
            }
            else
            {
                IEnumerable<_Enum> enumData = from Gender e in Enum.GetValues(typeof(Gender))
                                              select new _Enum
                                              {
                                                  Value = (int)e,
                                                  Text = GetEnumDescription(e)
                                              };
                return enumData;
            }
        }
        public IEnumerable<_Enum> GetDayOfWeekEnum(string lang)
        {
            if (lang == "ar")
            {
                List<_Enum> enumData = new List<_Enum>();
                enumData.Add(new _Enum()
                {
                    Value = 0,
                    Text = "الأحد",
                });
                enumData.Add(new _Enum()
                {
                    Value = 1,
                    Text = "الاثنين",
                });
                enumData.Add(new _Enum()
                {
                    Value = 2,
                    Text = "الثلاثاء",
                });
                enumData.Add(new _Enum()
                {
                    Value = 3,
                    Text = "الأربعاء",
                });
                enumData.Add(new _Enum()
                {
                    Value = 4,
                    Text = "الخميس",
                });
                enumData.Add(new _Enum()
                {
                    Value = 5,
                    Text = "الجمعة",
                });
                enumData.Add(new _Enum()
                {
                    Value = 6,
                    Text = "السبت",
                });
                return enumData;
            }
            else
            {
                IEnumerable<_Enum> enumData = from DayOfWeekEnum e in Enum.GetValues(typeof(DayOfWeekEnum))
                                              select new _Enum
                                              {
                                                  Value = (int)e,
                                                  Text = GetEnumDescription(e)
                                              };
                return enumData;
            }
        }
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                    typeof(DescriptionAttribute),
                    false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
        public VendorViewModel GetVendorViewModelByID(int VendorsID, string lang, string MainPathView)
        {
            var getData = Uow.Vendors.GetAll(x => !x.IsDeleted && x.VendorsID == VendorsID)
            .Select(p => new VendorViewModel()
            {
                CityID = p.CityID,
                RegionID = p.City.RegionID,
                Lat = p.Lat != 0 ? p.Lat : Convert.ToDouble(0.000000),
                Lng = p.Lng != 0 ? p.Lng : Convert.ToDouble(0.000000),
                IsEnabled = p.IsEnabled,
                IsEnable = p.IsEnable,
                MobileNo = p.MobileNo,
                Email = p.Email,
                EnableDate = p.EnableDate,
                IsDeleted = p.IsDeleted,
                UpdateDate = p.UpdateDate,
                UserUpdate = p.UserUpdate,
                CreateDate = p.CreateDate,
                DeleteDate = p.DeleteDate,
                UserId = p.UserId,
                UserDelete = p.UserDelete,
                UserEnable = p.UserEnable,
                NationalityID = p.NationalityID != null ? (int)p.NationalityID : 0,
                FirstName = lang == "ar" ? p.FirstNameAr : p.FirstNameEn,
                SeconedName = lang == "ar" ? p.SeconedNameAr : p.SeconedNameEn,
                FirstNameAr = p.FirstNameAr,
                SeconedNameAr = p.SeconedNameAr,
                FirstNameEn = p.FirstNameEn,
                SeconedNameEn = p.SeconedNameEn,
                VendorsGuid = p.VendorsGuid,
                StoreNameAr = p.StoreNameAr,
                StoreNameEn = p.StoreNameEn,
                AboutStoreAr = p.AboutStoreAr,
                AboutStoreEn = p.AboutStoreEn,
                VendorsID = p.VendorsID,
                AccountNumber = p.AccountNumber,
                Address = p.Address,
                BanksID = p.BanksID,
                Banner = !string.IsNullOrEmpty(p.Banner) ? (MainPathView + p.Banner) : "https://cdn.made-home.com/Home/beet_logo.png",
                ProfilePic = !string.IsNullOrEmpty(p.ProfilePic) ? (MainPathView + p.ProfilePic) : "https://cdn.made-home.com/Home/beet_logo.png",
                CRPic = !string.IsNullOrEmpty(p.CRPic) ? (MainPathView + p.CRPic) : "https://cdn.made-home.com/Home/beet_logo.png",
                Logo = !string.IsNullOrEmpty(p.Logo) ? (MainPathView + p.Logo) : "https://cdn.made-home.com/Home/beet_logo.png",
                CRnumber = p.CRnumber,
                BlockID = p.BlockID,
                IBANNumber = p.IBANNumber,
                PackageID = p.PackageID,
                SwiftCode = p.SwiftCode,
                TaxNumber = p.TaxNumber,
                Gender = p.Gender,
                IsEnableString = lang == "ar" ? (p.IsEnable ? "مفعل" : "غير مفعل") : (p.IsEnable ? "Enabled" : "Disabled"),
                GenderString = lang == "ar" ? (p.Gender == (int)Gender.Male ? "ذكر" : p.Gender == (int)Gender.Female ? "انثى" : "يفضل عدم التحديد") : (p.Gender == (int)Gender.Male ? "Male" : p.Gender == (int)Gender.Female ? "Female" : "Prefer not to specify"),
                CityName = lang == "ar" ? p.City.NameAR : p.City.NameEN,
                RegionName = lang == "ar" ? p.City.Region.NameAR : p.City.Region.NameEN,
                NationalityName = lang == "ar" ? p.Nationality.NameAR : p.Nationality.NameEN,
                BlockName = lang == "ar" ? p.Block.NameAR : p.Block.NameEN,
                PackageName = lang == "ar" ? p.Package.NameAR : p.Package.NameEN,
                BanksName = lang == "ar" ? p.Banks.NameAR : p.Banks.NameEN,
                DeliveryType = p.DeliveryType,
                DeliveryPrice = p.DeliveryPrice.ToString(),
                StoreName = lang == "ar" ? p.StoreNameAr : p.StoreNameEn,
                DeliveryTypeName = lang == "ar" ? (p.DeliveryType == (int)DeliveryTypeEnum.HomeMade ? "الشركة" : p.DeliveryType == (int)DeliveryTypeEnum.Store ? "المتجر" : "") : (p.DeliveryType == (int)DeliveryTypeEnum.HomeMade ? "Company" : p.DeliveryType == (int)DeliveryTypeEnum.Store ? "Store" : ""),
                WorkingTimes = p.WorkingTimes,
                IsShowContact = p.IsShowContact,
                DaysWork = p.DaysWork,
                WorkFrom = p.WorkFrom,
                WorkTo = p.WorkTo,
                WorkFromString = p.WorkFrom != null ? p.WorkFrom.Value.ToString("HH:mm") : string.Empty,
                WorkToString = p.WorkTo != null ? p.WorkTo.Value.ToString("HH:mm") : string.Empty,
                DaysVac = p.DaysVac,
                VacWorkFrom = p.VacWorkFrom,
                VacWorkTo = p.VacWorkTo,
                VacWorkFromString = p.VacWorkFrom != null ? p.VacWorkFrom.Value.ToString("HH:mm") : string.Empty,
                VacWorkToString = p.VacWorkTo != null ? p.VacWorkTo.Value.ToString("HH:mm") : string.Empty,
            }).FirstOrDefault();
            getData.ListDaysWork = !string.IsNullOrEmpty(getData.DaysWork) ? getData.DaysWork.Split(',') : null;
            getData.ListDaysVac = !string.IsNullOrEmpty(getData.DaysVac) ? getData.DaysVac.Split(',') : null;
            return getData;
        }
        public VendorViewModel GetVendorViewModelByGuid(Guid VendorsGuid, string lang, string MainPathView)
        {

            var getData = Uow.Vendors.GetAll(x => !x.IsDeleted && x.VendorsGuid == VendorsGuid)
            .Select(p => new VendorViewModel()
            {
                CityID = p.CityID,
                RegionID = p.City.RegionID,
                Lat = p.Lat != 0 ? p.Lat : Convert.ToDouble(0.000000),
                Lng = p.Lng != 0 ? p.Lng : Convert.ToDouble(0.000000),
                IsEnabled = p.IsEnabled,
                IsEnable = p.IsEnable,
                MobileNo = p.MobileNo,
                Email = p.Email,
                EnableDate = p.EnableDate,
                IsDeleted = p.IsDeleted,
                UpdateDate = p.UpdateDate,
                UserUpdate = p.UserUpdate,
                CreateDate = p.CreateDate,
                DeleteDate = p.DeleteDate,
                UserId = p.UserId,
                UserDelete = p.UserDelete,
                UserEnable = p.UserEnable,
                NationalityID = p.NationalityID != null ? (int)p.NationalityID : 0,
                FirstName = lang == "ar" ? p.FirstNameAr : p.FirstNameEn,
                SeconedName = lang == "ar" ? p.SeconedNameAr : p.SeconedNameEn,
                FirstNameAr = p.FirstNameAr,
                SeconedNameAr = p.SeconedNameAr,
                FirstNameEn = p.FirstNameEn,
                SeconedNameEn = p.SeconedNameEn,
                VendorsGuid = p.VendorsGuid,
                StoreNameAr = p.StoreNameAr,
                StoreNameEn = p.StoreNameEn,
                AboutStoreAr = p.AboutStoreAr,
                AboutStoreEn = p.AboutStoreEn,
                VendorsID = p.VendorsID,
                AccountNumber = p.AccountNumber,
                Address = p.Address,
                BanksID = p.BanksID,
                Banner = !string.IsNullOrEmpty(p.Banner) ? "https://cdn.made-home.com/Home/beet_logo.png" : "https://cdn.made-home.com/Home/beet_logo.png",
                ProfilePic = !string.IsNullOrEmpty(p.ProfilePic) ? (MainPathView + p.ProfilePic) : "https://cdn.made-home.com/Home/beet_logo.png",
                CRPic = !string.IsNullOrEmpty(p.CRPic) ? (MainPathView + p.CRPic) : "https://cdn.made-home.com/Home/beet_logo.png",
                Logo = !string.IsNullOrEmpty(p.Logo) ? (MainPathView + p.Logo) : "https://cdn.made-home.com/Home/beet_logo.png",
                CRnumber = p.CRnumber,
                BlockID = p.BlockID,
                IBANNumber = p.IBANNumber,
                PackageID = p.PackageID,
                SwiftCode = p.SwiftCode,
                TaxNumber = p.TaxNumber,
                Gender = p.Gender,
                IsEnableString = lang == "ar" ? (p.IsEnable ? "مفعل" : "غير مفعل") : (p.IsEnable ? "Enabled" : "Disabled"),
                GenderString = lang == "ar" ? (p.Gender == (int)Gender.Male ? "ذكر" : p.Gender == (int)Gender.Female ? "انثى" : "يفضل عدم التحديد") : (p.Gender == (int)Gender.Male ? "Male" : p.Gender == (int)Gender.Female ? "Female" : "Prefer not to specify"),
                CityName = lang == "ar" ? p.City.NameAR : p.City.NameEN,
                RegionName = lang == "ar" ? p.City.Region.NameAR : p.City.Region.NameEN,
                NationalityName = lang == "ar" ? p.Nationality.NameAR : p.Nationality.NameEN,
                BlockName = lang == "ar" ? p.Block.NameAR : p.Block.NameEN,
                PackageName = lang == "ar" ? p.Package.NameAR : p.Package.NameEN,
                BanksName = lang == "ar" ? p.Banks.NameAR : p.Banks.NameEN,
                DeliveryType = p.DeliveryType,
                DeliveryPrice = p.DeliveryPrice.ToString(),
                StoreName = lang == "ar" ? p.StoreNameAr : p.StoreNameEn,
                DeliveryTypeName = lang == "ar" ? (p.DeliveryType == (int)DeliveryTypeEnum.HomeMade ? "الشركة" : p.DeliveryType == (int)DeliveryTypeEnum.Store ? "المتجر" : "") : (p.DeliveryType == (int)DeliveryTypeEnum.HomeMade ? "Company" : p.DeliveryType == (int)DeliveryTypeEnum.Store ? "Store" : ""),
                WorkingTimes = p.WorkingTimes,
                IsShowContact = p.IsShowContact,
                IsShowContactString = lang == "ar" ? (p.IsShowContact ? "موافق" : "غير موافق") : (p.IsShowContact ? "Ok" : "Not Agree"),
                DaysWork = p.DaysWork,
                WorkFrom = p.WorkFrom,
                WorkTo = p.WorkTo,
                WorkFromString = p.WorkFrom != null ? p.WorkFrom.Value.ToString("HH:mm") : string.Empty,
                WorkToString = p.WorkTo != null ? p.WorkTo.Value.ToString("HH:mm") : string.Empty,
                DaysVac = p.DaysVac,
                VacWorkFrom = p.VacWorkFrom,
                VacWorkTo = p.VacWorkTo,
                VacWorkFromString = p.VacWorkFrom != null ? p.VacWorkFrom.Value.ToString("HH:mm") : string.Empty,
                VacWorkToString = p.VacWorkTo != null ? p.VacWorkTo.Value.ToString("HH:mm") : string.Empty,
            }).FirstOrDefault();
            getData.ListDaysWork = !string.IsNullOrEmpty(getData.DaysWork) ? getData.DaysWork.Split(',') : null;
            getData.ListDaysVac = !string.IsNullOrEmpty(getData.DaysVac) ? getData.DaysVac.Split(',') : null;
            return getData;
        }
        public VendorWorkingTime GetVendorWorkingTimeByID(int VendorsID, string lang)
        {
            VendorWorkingTime workingTime = new VendorWorkingTime();
            workingTime.DaysVac = "";
            workingTime.DaysWork = "";
            workingTime.SiteDaysVac = "";
            workingTime.SiteDaysWork = "";
            workingTime.SiteTimeVac = "";
            workingTime.SiteTimeWork = "";
            var daysEnum = GetDayOfWeekEnum(lang);
            var getData = Uow.Vendors.GetAll(x => !x.IsDeleted && x.VendorsID == VendorsID).FirstOrDefault();
            if (getData.IsDaysWork)
            {
                var ListDaysWork = !string.IsNullOrEmpty(getData.DaysWork) ? getData.DaysWork.Split(',') : null;
                if (ListDaysWork != null)
                {
                    string startday = ListDaysWork.Any(x => x == ((int)DayOfWeekEnum.Saturday).ToString()) ? daysEnum.Where(x => x.Value == (int)DayOfWeekEnum.Saturday).FirstOrDefault().Text : daysEnum.Where(x => x.Value.ToString() == ListDaysWork.Where(x => !string.IsNullOrEmpty(x)).OrderBy(x => Convert.ToInt32(x)).FirstOrDefault()).FirstOrDefault().Text;
                    string Endday = daysEnum.Any(x => x.Value.ToString() == ListDaysWork.Where(x => x != ((int)DayOfWeekEnum.Saturday).ToString() && !string.IsNullOrEmpty(x)).OrderByDescending(x => Convert.ToInt32(x)).FirstOrDefault()) ? daysEnum.Where(x => x.Value.ToString() == ListDaysWork.Where(x => x != ((int)DayOfWeekEnum.Saturday).ToString() && !string.IsNullOrEmpty(x)).OrderByDescending(x => Convert.ToInt32(x)).FirstOrDefault()).FirstOrDefault().Text : "";
                    workingTime.DaysWork = lang == "ar" ? (!string.IsNullOrEmpty(Endday) ? ("اوقات العمل : من   " + startday + " الى  " + Endday) : ("اوقات العمل :   " + startday)) : (!string.IsNullOrEmpty(Endday) ? ("Working : From " + startday + " To " + Endday) : ("Working : " + startday));
                    workingTime.SiteDaysWork = lang == "ar" ? (!string.IsNullOrEmpty(Endday) ? ("اوقات العمل : من   " + startday + " الى  " + Endday) : ("اوقات العمل :   " + startday)) : (!string.IsNullOrEmpty(Endday) ? ("Working : From " + startday + " To " + Endday) : ("Working : " + startday));
                }
                if (getData.WorkFrom != null && getData.WorkTo != null)
                {
                    workingTime.DaysWork += "   " + (lang == "ar" ? (" ( " + getData.WorkFrom?.ToString("hh:mm tt") + " : " + getData.WorkTo?.ToString("hh:mm tt")) + " ) " : (" ( " + getData.WorkFrom?.ToString("hh:mm tt") + " : " + getData.WorkTo?.ToString("hh:mm tt")) + " ) ");
                    workingTime.SiteTimeWork = (lang == "ar" ? (" ( " + getData.WorkFrom?.ToString("hh:mm tt") + " : " + getData.WorkTo?.ToString("hh:mm tt")) + " ) " : (" ( " + getData.WorkFrom?.ToString("hh:mm tt") + " : " + getData.WorkTo?.ToString("hh:mm tt")) + " ) ");
                }
            }
            else
            {
                workingTime.DaysWork = getData.WorkingTimes;
            }
            if (getData.IsDaysVac)
            {
                var ListDaysVac = !string.IsNullOrEmpty(getData.DaysVac) ? getData.DaysVac.Split(',') : null;
                if (ListDaysVac != null)
                {
                    var dayVac = String.Join(" , ", daysEnum.Where(x => ListDaysVac.Any(y => y == x.Value.ToString())).Select(m => m.Text).ToArray());
                    workingTime.DaysVac = lang == "ar" ? (" و " + dayVac) : ("Working in Vacation : " + dayVac);
                    workingTime.SiteDaysVac = lang == "ar" ? (" و " + dayVac) : ("Working in Vacation : " + dayVac);
                }

                if (getData.VacWorkFrom != null && getData.VacWorkTo != null)
                {
                    workingTime.DaysVac += "   " + (lang == "ar" ? (" ( " + getData.VacWorkFrom?.ToString("hh:mm tt") + " : " + getData.VacWorkTo?.ToString("hh:mm tt")) + " ) " : (" ( " + getData.VacWorkFrom?.ToString("hh:mm tt") + " : " + getData.VacWorkTo?.ToString("hh:mm tt")) + " ) ");
                    workingTime.SiteTimeVac = (lang == "ar" ? (" ( " + getData.VacWorkFrom?.ToString("hh:mm tt") + " : " + getData.VacWorkTo?.ToString("hh:mm tt")) + " ) " : (" ( " + getData.VacWorkFrom?.ToString("hh:mm tt") + " : " + getData.VacWorkTo?.ToString("hh:mm tt")) + " ) ");
                }
            }
            return workingTime;
        }
        public bool IsVendorWorking(string DaysWork, DateTime? WorkFrom, DateTime? WorkTo, string DaysVac, DateTime? VacWorkFrom, DateTime? VacWorkTo)
        {
            if (!string.IsNullOrEmpty(DaysWork) || !string.IsNullOrEmpty(DaysVac))
            {
                var datetimenow = _bLGeneral.GetDateTimeNow().TimeOfDay;
                var ListDaysWork = !string.IsNullOrEmpty(DaysWork) ? DaysWork.Split(',') : null;
                var ListDaysVac = !string.IsNullOrEmpty(DaysVac) ? DaysVac.Split(',') : null;
                if (ListDaysWork != null)
                {
                    if (ListDaysWork.Where(x => !string.IsNullOrEmpty(x)).Any(x => Convert.ToInt32(x) == (int)DateTime.Now.DayOfWeek))
                    {

                        TimeSpan WorkFromSpan = (WorkFrom != null ? (DateTime.ParseExact(WorkFrom.Value.ToString("HH:mm"), "HH:mm", System.Globalization.CultureInfo.InvariantCulture)) : new DateTime()).TimeOfDay;
                        TimeSpan WorkToSpan = (WorkTo != null ? (DateTime.ParseExact(WorkTo.Value.ToString("HH:mm"), "HH:mm", System.Globalization.CultureInfo.InvariantCulture)) : new DateTime()).TimeOfDay;

                        if (WorkFromSpan != new TimeSpan() && WorkToSpan != new TimeSpan())
                        {
                            if (WorkFromSpan < WorkToSpan)
                            {
                                if (WorkFromSpan < datetimenow && WorkToSpan > datetimenow)
                                {
                                    return true;
                                }
                            }
                            else
                            {
                                if (WorkFromSpan < datetimenow || WorkToSpan > datetimenow)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
                if (ListDaysVac != null)
                {
                    if (ListDaysVac.Where(x => !string.IsNullOrEmpty(x)).Any(x => Convert.ToInt32(x) == (int)DateTime.Now.DayOfWeek))
                    {

                        TimeSpan VacWorkFromSpan = (VacWorkFrom != null ? (DateTime.ParseExact(VacWorkFrom.Value.ToString("HH:mm"), "HH:mm", System.Globalization.CultureInfo.InvariantCulture)) : new DateTime()).TimeOfDay;
                        TimeSpan VacWorkToSpan = (VacWorkTo != null ? (DateTime.ParseExact(VacWorkTo.Value.ToString("HH:mm"), "HH:mm", System.Globalization.CultureInfo.InvariantCulture)) : new DateTime()).TimeOfDay;

                        if (VacWorkFromSpan != new TimeSpan() && VacWorkToSpan != new TimeSpan())
                        {
                            if (VacWorkFromSpan < VacWorkToSpan)
                            {
                                if (VacWorkFromSpan < datetimenow && VacWorkToSpan > datetimenow)
                                {
                                    return true;
                                }
                            }
                            else
                            {
                                if (VacWorkFromSpan < datetimenow || VacWorkToSpan > datetimenow)
                                {
                                    return true;
                                }
                            }
                        }

                    }
                }
                return false;
            }
            {
                return true;
            }
        }
        public bool IsVendorWorking(int VendorsID)
        {
            var vendor = GetById(VendorsID);
            if (vendor != null)
            {
                if (!string.IsNullOrEmpty(vendor.DaysWork) || !string.IsNullOrEmpty(vendor.DaysVac))
                {
                    var datetimenow = _bLGeneral.GetDateTimeNow().TimeOfDay;
                    var ListDaysWork = !string.IsNullOrEmpty(vendor.DaysWork) ? vendor.DaysWork.Split(',') : null;
                    var ListDaysVac = !string.IsNullOrEmpty(vendor.DaysVac) ? vendor.DaysVac.Split(',') : null;
                    if (ListDaysWork != null)
                    {
                        if (ListDaysWork.Where(x => !string.IsNullOrEmpty(x)).Any(x => Convert.ToInt32(x) == (int)DateTime.Now.DayOfWeek))
                        {

                            TimeSpan WorkFrom = (vendor.WorkFrom != null ? (DateTime.ParseExact(vendor.WorkFrom.Value.ToString("HH:mm"), "HH:mm", System.Globalization.CultureInfo.InvariantCulture)) : new DateTime()).TimeOfDay;
                            TimeSpan WorkTo = (vendor.WorkTo != null ? (DateTime.ParseExact(vendor.WorkTo.Value.ToString("HH:mm"), "HH:mm", System.Globalization.CultureInfo.InvariantCulture)) : new DateTime()).TimeOfDay;

                            if (WorkFrom != new TimeSpan() && WorkTo != new TimeSpan())
                            {
                                if (WorkFrom < WorkTo)
                                {
                                    if (WorkFrom < datetimenow && WorkTo > datetimenow)
                                    {
                                        return true;
                                    }
                                }
                                else
                                {
                                    if (WorkFrom < datetimenow || WorkTo > datetimenow)
                                    {
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                    if (ListDaysVac != null)
                    {
                        if (ListDaysVac.Where(x => !string.IsNullOrEmpty(x)).Any(x => Convert.ToInt32(x) == (int)DateTime.Now.DayOfWeek))
                        {

                            TimeSpan VacWorkFrom = (vendor.VacWorkFrom != null ? (DateTime.ParseExact(vendor.VacWorkFrom.Value.ToString("HH:mm"), "HH:mm", System.Globalization.CultureInfo.InvariantCulture)) : new DateTime()).TimeOfDay;
                            TimeSpan VacWorkTo = (vendor.VacWorkTo != null ? (DateTime.ParseExact(vendor.VacWorkTo.Value.ToString("HH:mm"), "HH:mm", System.Globalization.CultureInfo.InvariantCulture)) : new DateTime()).TimeOfDay;

                            if (VacWorkFrom != new TimeSpan() && VacWorkTo != new TimeSpan())
                            {
                                if (VacWorkFrom < VacWorkTo)
                                {
                                    if (VacWorkFrom < datetimenow && VacWorkTo > datetimenow)
                                    {
                                        return true;
                                    }
                                }
                                else
                                {
                                    if (VacWorkFrom < datetimenow || VacWorkTo > datetimenow)
                                    {
                                        return true;
                                    }
                                }
                            }

                        }
                    }
                }
                {
                    return true;
                }
            }
            return false;
        }
        public VendorViewModelApi GetVendorViewModelApi(int VendorsID, string lang, string MainPathView, int userId)
        {

            var getData = Uow.Vendors.GetAll(x => !x.IsDeleted && x.VendorsID == VendorsID)
            .Select(p => new VendorViewModelApi()
            {
                cityID = p.CityID,
                gender = p.Gender,
                mobileNo = p.MobileNo,
                email = p.Email,
                firstName = lang == "ar" ? p.FirstNameAr : p.FirstNameEn,
                seconedName = lang == "ar" ? p.SeconedNameAr : p.SeconedNameEn,
                nationalityID = p.NationalityID ?? 0,
                address = !string.IsNullOrWhiteSpace(p.Address) ? p.Address : "",
                cityName = lang == "ar" ? p.City.NameAR : p.City.NameEN,
                nationalityName = lang == "ar" ? p.Nationality.NameAR : p.Nationality.NameEN,
                profilePic = !string.IsNullOrWhiteSpace(p.ProfilePic) ? (MainPathView + p.ProfilePic) : "",
                vendorsID = p.VendorsID,
                isVac = p.IsVac,
                isLocation = p.IsLocation,
                notificationsCount = GetUserNotificationNotSeenCount(userId),
            }).FirstOrDefault();
            return getData;
        }
        public CustomersVendor GetCustomerVendor(int VendorsID, string lang, string MainPathView, int page, string search)
        {
            var model = new CustomersVendor();
            var list = new List<CustomerVendorApi>();
            var Data = Uow.OrderVendor.GetAll(e => !e.IsDeleted && e.VendorsID == VendorsID, "Orders.Customers.Nationality,Orders.Customers.City").Select(m => new CustomerVendorApi()
            {
                address = !string.IsNullOrWhiteSpace(m.Orders.Customers.Address) ? m.Orders.Customers.Address : "",
                cityName = lang == "ar" ? m.Orders.Customers.City.NameAR : m.Orders.Customers.City.NameEN,
                nationalityName = lang == "ar" ? m.Orders.Customers.Nationality.NameAR : m.Orders.Customers.Nationality.NameEN,
                email = m.Orders.Customers.Email,
                mobileNo = m.Orders.Customers.MobileNo,
                name = m.Orders.Customers.FirstName + "  " + m.Orders.Customers.SeconedName,
                profilePic = MainPathView + m.Orders.Customers.ProfilePic,
                customersID = m.Orders.CustomersID
            }).ToList();
            foreach (var item in Data)
            {
                if (!list.Any(x => x.customersID == item.customersID))
                {
                    item.coutorders = Uow.OrderVendor.GetAll(e => !e.IsDeleted && e.VendorsID == VendorsID && e.Orders.CustomersID == item.customersID).Count();
                    list.Add(item);
                }
            }
            if (!string.IsNullOrWhiteSpace(search))
            {
                list = list.Where(s => s.name.ToLower().Contains(search.ToLower()) || s.mobileNo.ToLower().Contains(search.ToLower())).ToList();
            }
            Data = Data.Skip(Convert.ToInt32(page - 1) * 10).ToList();
            var productsres = Data.Take(((Convert.ToInt32(page) * 10)) - (Convert.ToInt32(page - 1) * 10)).ToList();
            bool NextPage = false;
            if (Data.Count() > 10)
            {
                NextPage = true;
            }
            model.customerVendor = list;
            model.isNextPage = NextPage;
            return model;
        }
        public IQueryable<VendorViewModel> GetAllVendorViewModel(string lang, string MainPathView)
        {

            var getData = Uow.Vendors.GetAll(x => !x.IsDeleted, "Activity"
                ).OrderByDescending(p => p.CreateDate)
            .Select(p => new VendorViewModel()
            {
                RegType = lang == "ar" ? (p.RegisterType == (int)RegisterType.Created ? "تم الانشاء" : "تم الإكمال") : (p.RegisterType == (int)RegisterType.Created ? "Created" : "Submitted"),
                CityID = p.CityID,
                RegionID = p.City.RegionID,
                Lat = p.Lat,
                Lng = p.Lng,
                IsEnable = p.IsEnable,
                MobileNo = p.MobileNo,
                Email = !string.IsNullOrWhiteSpace(p.Email) ? p.Email : "--",
                EnableDate = p.EnableDate,
                IsDeleted = p.IsDeleted,
                UpdateDate = p.UpdateDate,
                UserUpdate = p.UserUpdate,
                IsEnabled = p.IsEnabled,

                CreateDate = p.CreateDate,
                DeleteDate = p.DeleteDate,
                UserId = p.UserId,
                UserDelete = p.UserDelete,
                UserEnable = p.UserEnable,
                NationalityID = p.NationalityID != null ? (int)p.NationalityID : 0,
                FirstName = lang == "ar" ? p.FirstNameAr : p.FirstNameEn,
                SeconedName = lang == "ar" ? p.SeconedNameAr : p.SeconedNameEn,
                FirstNameAr = p.FirstNameAr,
                SeconedNameAr = p.SeconedNameAr,
                FirstNameEn = p.FirstNameEn,
                SeconedNameEn = p.SeconedNameEn,
                VendorsGuid = p.VendorsGuid,
                StoreNameAr = p.StoreNameAr,
                StoreNameEn = p.StoreNameEn,
                StoreName = lang == "ar" ? !string.IsNullOrWhiteSpace(p.StoreNameAr) ? p.StoreNameAr : "--" :
                !string.IsNullOrWhiteSpace(p.StoreNameEn) ? p.StoreNameEn : "--",
                AboutStoreAr = p.AboutStoreAr,
                AboutStoreEn = p.AboutStoreEn,
                VendorsID = p.VendorsID,
                AccountNumber = p.AccountNumber,
                Address = p.Address,
                BanksID = p.BanksID,
                Banner = !string.IsNullOrEmpty(p.Banner) ? (MainPathView + p.Banner) : "https://cdn.made-home.com/Home/beet_logo.png",
                ProfilePic = !string.IsNullOrEmpty(p.ProfilePic) ? (MainPathView + p.ProfilePic) : "https://cdn.made-home.com/Home/beet_logo.png",
                CRPic = !string.IsNullOrEmpty(p.CRPic) ? (MainPathView + p.CRPic) : "https://cdn.made-home.com/Home/beet_logo.png",
                Logo = !string.IsNullOrEmpty(p.Logo) ? (MainPathView + p.Logo) : "https://cdn.made-home.com/Home/beet_logo.png",
                CRnumber = p.CRnumber,
                BlockID = p.BlockID,
                IBANNumber = p.IBANNumber,
                PackageID = p.PackageID,
                SwiftCode = p.SwiftCode,
                TaxNumber = p.TaxNumber,
                Gender = p.Gender,
                IsEnableString = lang == "ar" ? (p.IsEnable ? "مفعل" : "غير مفعل") : (p.IsEnable ? "Enabled" : "Disabled"),
                GenderString = lang == "ar" ? (p.Gender == (int)Gender.Male ? "ذكر" : p.Gender == (int)Gender.Female ? "انثى" : "يفضل عدم التحديد") : (p.Gender == (int)Gender.Male ? "Male" : p.Gender == (int)Gender.Female ? "Female" : "Prefer not to specify"),
                DeliveryTypeName = lang == "ar" ? (p.DeliveryType == (int)DeliveryTypeEnum.HomeMade ? "الشركة" : p.DeliveryType == (int)DeliveryTypeEnum.Store ? "المتجر" : "") : (p.DeliveryType == (int)DeliveryTypeEnum.HomeMade ? "Company" : p.DeliveryType == (int)DeliveryTypeEnum.Store ? "Store" : ""),
                DeliveryPrice = p.DeliveryPrice.ToString(),
                RegionName = lang == "ar" ? p.City.NameAR : p.City.NameEN,
                IsVacString = lang == "ar" ? (p.IsVac ? "اجازة" : "قيد العمل") : (p.IsVac ? "Vacation" : "On Work"),
                ActivityName = lang == "ar" ? p.Activity.NameAR : p.Activity.NameEN,

            });
            return getData;
        }
        public VendorViewModellistApi GetAllVendorViewModel(string lang, string MainPathView, int page, string keySearch)
        {

            var getData = Uow.Vendors.GetAll(x => !x.IsDeleted && x.IsEnable && !x.IsVac, "Activity,City"
                ).ToList().OrderByDescending(x => x.CreateDate)
                .OrderByDescending(p => IsVendorWorking(p.DaysWork, p.WorkFrom, p.WorkTo, p.DaysVac, p.VacWorkFrom, p.VacWorkTo))
            .Select(p => new VendorViewModel()
            {
                RegType = lang == "ar" ? (p.RegisterType == (int)RegisterType.Created ? "تم الانشاء" : "تم الإكمال") : (p.RegisterType == (int)RegisterType.Created ? "Created" : "Submitted"),
                CityID = p.CityID,
                RegionID = p.City.RegionID,
                Lat = p.Lat,
                Lng = p.Lng,
                IsEnable = p.IsEnable,
                MobileNo = p.MobileNo,
                Email = !string.IsNullOrWhiteSpace(p.Email) ? p.Email : "--",
                IsEnabled = p.IsEnabled,
                EnableDate = p.EnableDate,
                IsDeleted = p.IsDeleted,
                UpdateDate = p.UpdateDate,
                UserUpdate = p.UserUpdate,
                CreateDate = p.CreateDate,
                DeleteDate = p.DeleteDate,
                UserId = p.UserId,
                UserDelete = p.UserDelete,
                UserEnable = p.UserEnable,
                NationalityID = p.NationalityID != null ? (int)p.NationalityID : 0,
                FirstName = lang == "ar" ? p.FirstNameAr : p.FirstNameEn,
                SeconedName = lang == "ar" ? p.SeconedNameAr : p.SeconedNameEn,
                FirstNameAr = p.FirstNameAr,
                SeconedNameAr = p.SeconedNameAr,
                FirstNameEn = p.FirstNameEn,
                SeconedNameEn = p.SeconedNameEn,
                VendorsGuid = p.VendorsGuid,
                StoreNameAr = p.StoreNameAr,
                StoreNameEn = p.StoreNameEn,
                StoreName = lang == "ar" ? !string.IsNullOrWhiteSpace(p.StoreNameAr) ? p.StoreNameAr : "--" :
                !string.IsNullOrWhiteSpace(p.StoreNameEn) ? p.StoreNameEn : "--",
                AboutStoreAr = p.AboutStoreAr,
                AboutStoreEn = p.AboutStoreEn,
                VendorsID = p.VendorsID,
                AccountNumber = p.AccountNumber,
                Address = p.Address,
                BanksID = p.BanksID,
                Banner = !string.IsNullOrEmpty(p.Banner) ? (MainPathView + p.Banner) : "https://cdn.made-home.com/Home/beet_logo.png",
                ProfilePic = !string.IsNullOrEmpty(p.ProfilePic) ? (MainPathView + p.ProfilePic) : "https://cdn.made-home.com/Home/beet_logo.png",
                CRPic = !string.IsNullOrEmpty(p.CRPic) ? (MainPathView + p.CRPic) : "https://cdn.made-home.com/Home/beet_logo.png",
                Logo = !string.IsNullOrEmpty(p.Logo) ? (MainPathView + p.Logo) : "https://cdn.made-home.com/Home/beet_logo.png",
                CRnumber = p.CRnumber,
                BlockID = p.BlockID,
                IBANNumber = p.IBANNumber,
                PackageID = p.PackageID,
                SwiftCode = p.SwiftCode,
                TaxNumber = p.TaxNumber,
                Gender = p.Gender,
                IsVacString = lang == "ar" ? (p.IsVac ? "اجازة" : "قيد العمل") : (p.IsVac ? "Vacation" : "On Work"),
                ActivityName = p.Activity != null ? (lang == "ar" ? p.Activity.NameAR : p.Activity.NameEN) : "",
                WorkingTimes = p.WorkingTimes,
                IsShowContact = p.IsShowContact,
                DaysWork = p.DaysWork,
                DaysVac = p.DaysVac,
                WorkFrom = p.WorkFrom,
                WorkTo = p.WorkTo,
            }).Where(e => e.StoreName.Contains(keySearch ?? "")).ToList();

            getData = getData.Skip(Convert.ToInt32(page - 1) * 10).ToList();
            var DataTake = getData.Take(((Convert.ToInt32(page) * 10)) - (Convert.ToInt32(page - 1) * 10)).ToList();
            bool NextPage = false;
            if (getData.Count() > 10)
            {
                NextPage = true;
            }
            var listTake = new List<VendorViewModel>();
            foreach (var item in DataTake)
            {
                item.IsVendorWorking = IsVendorWorking(item.DaysWork, item.WorkFrom, item.WorkTo, item.DaysVac, item.VacWorkFrom, item.VacWorkTo);
                item.IsVendorWorkingString = lang == "ar" ? (IsVendorWorking(item.DaysWork, item.WorkFrom, item.WorkTo, item.DaysVac, item.VacWorkFrom, item.VacWorkTo) ? "مفتوح" : "مغلق") : (IsVendorWorking(item.DaysWork, item.WorkFrom, item.WorkTo, item.DaysVac, item.VacWorkFrom, item.VacWorkTo) ? "Open" : "Close");

                var working = GetVendorWorkingTimeByID(item.VendorsID, lang);
                item.DaysWork = !string.IsNullOrEmpty(working.DaysWork) ? working.DaysWork : string.Empty;
                item.DaysVac = !string.IsNullOrEmpty(working.DaysVac) ? working.DaysVac : string.Empty;

                listTake.Add(item);
            }

            var model = new VendorViewModellistApi();
            model.vendors = listTake;
            model.isNextPage = NextPage;


            return model;
        }
        public VendorViewModellistApi GetAllVendorViewModelByCustomer(int CustomersID, string lang, string MainPathView, int page)
        {

            var getData = Uow.Vendors.GetAll(x => !x.IsDeleted && x.IsEnable && !x.IsVac && x.OrderVendor.Any(y => y.Orders.CustomersID == CustomersID), "Activity,City"
                ).ToList().OrderByDescending(x => x.CreateDate)
                .OrderByDescending(p => IsVendorWorking(p.DaysWork, p.WorkFrom, p.WorkTo, p.DaysVac, p.VacWorkFrom, p.VacWorkTo))
            .Select(p => new VendorViewModel()
            {
                RegType = lang == "ar" ? (p.RegisterType == (int)RegisterType.Created ? "تم الانشاء" : "تم الإكمال") : (p.RegisterType == (int)RegisterType.Created ? "Created" : "Submitted"),
                CityID = p.CityID,
                RegionID = p.City.RegionID,
                Lat = p.Lat,
                Lng = p.Lng,
                IsEnabled = p.IsEnabled,
                IsEnable = p.IsEnable,
                MobileNo = p.MobileNo,
                Email = !string.IsNullOrWhiteSpace(p.Email) ? p.Email : "--",
                EnableDate = p.EnableDate,
                IsDeleted = p.IsDeleted,
                UpdateDate = p.UpdateDate,
                UserUpdate = p.UserUpdate,
                CreateDate = p.CreateDate,
                DeleteDate = p.DeleteDate,
                UserId = p.UserId,
                UserDelete = p.UserDelete,
                UserEnable = p.UserEnable,
                NationalityID = p.NationalityID != null ? (int)p.NationalityID : 0,
                FirstName = lang == "ar" ? p.FirstNameAr : p.FirstNameEn,
                SeconedName = lang == "ar" ? p.SeconedNameAr : p.SeconedNameEn,
                FirstNameAr = p.FirstNameAr,
                SeconedNameAr = p.SeconedNameAr,
                FirstNameEn = p.FirstNameEn,
                SeconedNameEn = p.SeconedNameEn,
                VendorsGuid = p.VendorsGuid,
                StoreNameAr = p.StoreNameAr,
                StoreNameEn = p.StoreNameEn,
                StoreName = lang == "ar" ? !string.IsNullOrWhiteSpace(p.StoreNameAr) ? p.StoreNameAr : "--" :
                !string.IsNullOrWhiteSpace(p.StoreNameEn) ? p.StoreNameEn : "--",
                AboutStoreAr = p.AboutStoreAr,
                AboutStoreEn = p.AboutStoreEn,
                VendorsID = p.VendorsID,
                AccountNumber = p.AccountNumber,
                Address = p.Address,
                BanksID = p.BanksID,
                Banner = !string.IsNullOrEmpty(p.Banner) ? (MainPathView + p.Banner) : "https://cdn.made-home.com/Home/beet_logo.png",
                ProfilePic = !string.IsNullOrEmpty(p.ProfilePic) ? (MainPathView + p.ProfilePic) : "https://cdn.made-home.com/Home/beet_logo.png",
                CRPic = !string.IsNullOrEmpty(p.CRPic) ? (MainPathView + p.CRPic) : "https://cdn.made-home.com/Home/beet_logo.png",
                Logo = !string.IsNullOrEmpty(p.Logo) ? (MainPathView + p.Logo) : "https://cdn.made-home.com/Home/beet_logo.png",
                CRnumber = p.CRnumber,
                BlockID = p.BlockID,
                IBANNumber = p.IBANNumber,
                PackageID = p.PackageID,
                SwiftCode = p.SwiftCode,
                TaxNumber = p.TaxNumber,
                Gender = p.Gender,
                IsVacString = lang == "ar" ? (p.IsVac ? "اجازة" : "قيد العمل") : (p.IsVac ? "Vacation" : "On Work"),
                ActivityName = p.Activity != null ? (lang == "ar" ? p.Activity.NameAR : p.Activity.NameEN) : "",
                WorkingTimes = p.WorkingTimes,
                IsShowContact = p.IsShowContact,
                DaysWork = p.DaysWork,
                DaysVac = p.DaysVac,
                WorkFrom = p.WorkFrom,
                WorkTo = p.WorkTo,
            }).ToList();

            getData = getData.Skip(Convert.ToInt32(page - 1) * 10).ToList();
            var DataTake = getData.Take(((Convert.ToInt32(page) * 10)) - (Convert.ToInt32(page - 1) * 10)).ToList();
            bool NextPage = false;
            if (getData.Count() > 10)
            {
                NextPage = true;
            }
            var listTake = new List<VendorViewModel>();
            foreach (var item in DataTake)
            {
                item.IsVendorWorking = IsVendorWorking(item.DaysWork, item.WorkFrom, item.WorkTo, item.DaysVac, item.VacWorkFrom, item.VacWorkTo);
                item.IsVendorWorkingString = lang == "ar" ? (IsVendorWorking(item.DaysWork, item.WorkFrom, item.WorkTo, item.DaysVac, item.VacWorkFrom, item.VacWorkTo) ? "مفتوح" : "مغلق") : (IsVendorWorking(item.DaysWork, item.WorkFrom, item.WorkTo, item.DaysVac, item.VacWorkFrom, item.VacWorkTo) ? "Open" : "Close");

                var working = GetVendorWorkingTimeByID(item.VendorsID, lang);
                item.DaysWork = !string.IsNullOrEmpty(working.DaysWork) ? working.DaysWork : string.Empty;
                item.DaysVac = !string.IsNullOrEmpty(working.DaysVac) ? working.DaysVac : string.Empty;

                listTake.Add(item);
            }

            var model = new VendorViewModellistApi();
            model.vendors = listTake;
            model.isNextPage = NextPage;


            return model;
        }
        public IQueryable<VendorViewModel> GetAllVendorViewModelBySearch(string[] ListRegionID, string[] ListCityID, string[] ListBlockID, string[] ListVendorID, string[] ListNationalityID, string[] ListGenderID, string[] ListRegisterTypeId, bool[] ListIsEnabled, bool[] ListIsVac, string lang, string MainPathView)
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
            if (ListNationalityID != null)
            {
                if (ListNationalityID.Count(p => !string.IsNullOrEmpty(p)) == 0)
                {
                    ListNationalityID = null;
                }
            }
            if (ListGenderID != null)
            {
                if (ListGenderID.Count(p => !string.IsNullOrEmpty(p)) == 0)
                {
                    ListGenderID = null;
                }
            }
            if (ListRegisterTypeId != null)
            {
                if (ListRegisterTypeId.Count(p => !string.IsNullOrEmpty(p)) == 0)
                {
                    ListRegisterTypeId = null;
                }
            }
            var getData = Uow.Vendors.GetAll(x => !x.IsDeleted
                                                   && (ListRegionID != null ? ListRegionID.Any(y => x.City.RegionID.ToString() == y) : true)
                                                   && (ListCityID != null ? ListCityID.Any(y => x.CityID.ToString() == y) : true)
                                                   && (ListBlockID != null ? ListBlockID.Any(y => x.BlockID.ToString() == y) : true)
                                                     && (ListVendorID != null ? ListVendorID.Any(y => x.VendorsID.ToString() == y) : true)
                                                       && (ListNationalityID != null ? ListNationalityID.Any(y => x.NationalityID.ToString() == y) : true)
                                                    && (ListGenderID != null ? ListGenderID.Any(y => x.Gender.ToString() == y) : true)
                                                      && (ListRegisterTypeId != null ? ListRegisterTypeId.Any(y => x.RegisterType.ToString() == y) : true)
                                                        && (ListIsEnabled != null ? ListIsEnabled.Any(y => x.IsEnabled == y) : true)
                                                        && (ListIsVac != null ? ListIsVac.Any(y => x.IsVac == y) : true)
                ).OrderBy(p => p.EntryID).ThenByDescending(p => p.CreateDate)
            .Select(p => new VendorViewModel()
            {
                RegType = lang == "ar" ? (p.RegisterType == (int)RegisterType.Created ? "تم الانشاء" : "تم الإكمال") : (p.RegisterType == (int)RegisterType.Created ? "Created" : "Submitted"),
                CityID = p.CityID,
                RegionID = p.City.RegionID,
                Lat = p.Lat,
                Lng = p.Lng,
                IsEnable = p.IsEnable,
                IsEnabled = p.IsEnabled,
                MobileNo = p.MobileNo,
                Email = !string.IsNullOrWhiteSpace(p.Email) ? p.Email : "--",
                EnableDate = p.EnableDate,
                IsDeleted = p.IsDeleted,
                UpdateDate = p.UpdateDate,
                UserUpdate = p.UserUpdate,
                CreateDate = p.CreateDate,
                DeleteDate = p.DeleteDate,
                UserId = p.UserId,
                UserDelete = p.UserDelete,
                UserEnable = p.UserEnable,
                NationalityID = p.NationalityID != null ? (int)p.NationalityID : 0,
                FirstName = lang == "ar" ? p.FirstNameAr : p.FirstNameEn,
                SeconedName = lang == "ar" ? p.SeconedNameAr : p.SeconedNameEn,
                FirstNameAr = p.FirstNameAr,
                SeconedNameAr = p.SeconedNameAr,
                FirstNameEn = p.FirstNameEn,
                SeconedNameEn = p.SeconedNameEn,
                VendorsGuid = p.VendorsGuid,
                StoreNameAr = p.StoreNameAr,
                StoreNameEn = p.StoreNameEn,
                StoreName = lang == "ar" ? !string.IsNullOrWhiteSpace(p.StoreNameAr) ? p.StoreNameAr : "--" :
                !string.IsNullOrWhiteSpace(p.StoreNameEn) ? p.StoreNameEn : "--",
                AboutStoreAr = p.AboutStoreAr,
                AboutStoreEn = p.AboutStoreEn,
                VendorsID = p.VendorsID,
                AccountNumber = p.AccountNumber,
                Address = p.Address,
                BanksID = p.BanksID,
                Banner = !string.IsNullOrEmpty(p.Banner) ? (MainPathView + p.Banner) : "https://cdn.made-home.com/Home/beet_logo.png",
                ProfilePic = !string.IsNullOrEmpty(p.ProfilePic) ? (MainPathView + p.ProfilePic) : "https://cdn.made-home.com/Home/beet_logo.png",
                CRPic = !string.IsNullOrEmpty(p.CRPic) ? (MainPathView + p.CRPic) : "https://cdn.made-home.com/Home/beet_logo.png",
                Logo = !string.IsNullOrEmpty(p.Logo) ? (MainPathView + p.Logo) : "https://cdn.made-home.com/Home/beet_logo.png",
                CRnumber = p.CRnumber,
                BlockID = p.BlockID,
                IBANNumber = p.IBANNumber,
                PackageID = p.PackageID,
                SwiftCode = p.SwiftCode,
                TaxNumber = p.TaxNumber,
                Gender = p.Gender,
                IsEnableString = lang == "ar" ? (p.IsEnable ? "مفعل" : "غير مفعل") : (p.IsEnable ? "Enabled" : "Disabled"),
                GenderString = lang == "ar" ? (p.Gender == (int)Gender.Male ? "ذكر" : p.Gender == (int)Gender.Female ? "انثى" : "يفضل عدم التحديد") : (p.Gender == (int)Gender.Male ? "Male" : p.Gender == (int)Gender.Female ? "Female" : "Prefer not to specify"),
                DeliveryTypeName = lang == "ar" ? (p.DeliveryType == (int)DeliveryTypeEnum.HomeMade ? "الشركة" : p.DeliveryType == (int)DeliveryTypeEnum.Store ? "المتجر" : "") : (p.DeliveryType == (int)DeliveryTypeEnum.HomeMade ? "Company" : p.DeliveryType == (int)DeliveryTypeEnum.Store ? "Store" : ""),
                DeliveryPrice = p.DeliveryPrice.ToString(),
                RegionName = lang == "ar" ? p.City.NameAR : p.City.NameEN,
                IsVacString = lang == "ar" ? (p.IsVac ? "اجازة" : "قيد العمل") : (p.IsVac ? "Vacation" : "On Work"),
                CreateDateString = p.CreateDate.ToString("hh:mm tt"),
                EntryString = lang == "ar" ? (p.EntryID == (int)EntryEnum.Auto ? "اتوماتيك" : "اكسل") : (p.EntryID == (int)EntryEnum.Auto ? "Auto" : "Excel"),
            });
            return getData;
        }
        public IQueryable<VendorViewModel> GetAllVendorViewModelByNotEnable(string lang, string MainPathView)
        {
            var getData = Uow.Vendors.GetAll(x => !x.IsDeleted && !x.IsEnable
                ).OrderByDescending(p => p.CreateDate)
            .Select(p => new VendorViewModel()
            {
                RegType = lang == "ar" ? (p.RegisterType == (int)RegisterType.Created ? "تم الانشاء" : "تم الإكمال") : (p.RegisterType == (int)RegisterType.Created ? "Created" : "Submitted"),
                CityID = p.CityID,
                RegionID = p.City.RegionID,
                Lat = p.Lat,
                Lng = p.Lng,
                IsEnable = p.IsEnable,
                MobileNo = p.MobileNo,
                Email = !string.IsNullOrWhiteSpace(p.Email) ? p.Email : "--",
                EnableDate = p.EnableDate,
                IsDeleted = p.IsDeleted,
                UpdateDate = p.UpdateDate,
                UserUpdate = p.UserUpdate,
                CreateDate = p.CreateDate,
                DeleteDate = p.DeleteDate,
                UserId = p.UserId,
                UserDelete = p.UserDelete,
                IsEnabled = p.IsEnabled,

                UserEnable = p.UserEnable,
                NationalityID = p.NationalityID != null ? (int)p.NationalityID : 0,
                FirstName = lang == "ar" ? p.FirstNameAr : p.FirstNameEn,
                SeconedName = lang == "ar" ? p.SeconedNameAr : p.SeconedNameEn,
                FirstNameAr = p.FirstNameAr,
                SeconedNameAr = p.SeconedNameAr,
                FirstNameEn = p.FirstNameEn,
                SeconedNameEn = p.SeconedNameEn,
                VendorsGuid = p.VendorsGuid,
                StoreNameAr = p.StoreNameAr,
                StoreNameEn = p.StoreNameEn,
                StoreName = lang == "ar" ? !string.IsNullOrWhiteSpace(p.StoreNameAr) ? p.StoreNameAr : "--" :
                !string.IsNullOrWhiteSpace(p.StoreNameEn) ? p.StoreNameEn : "--",
                AboutStoreAr = p.AboutStoreAr,
                AboutStoreEn = p.AboutStoreEn,
                VendorsID = p.VendorsID,
                AccountNumber = p.AccountNumber,
                Address = p.Address,
                BanksID = p.BanksID,
                Banner = !string.IsNullOrEmpty(p.Banner) ? (MainPathView + p.Banner) : "https://cdn.made-home.com/Home/beet_logo.png",
                ProfilePic = !string.IsNullOrEmpty(p.ProfilePic) ? (MainPathView + p.ProfilePic) : "https://cdn.made-home.com/Home/beet_logo.png",
                CRPic = !string.IsNullOrEmpty(p.CRPic) ? (MainPathView + p.CRPic) : "https://cdn.made-home.com/Home/beet_logo.png",
                Logo = !string.IsNullOrEmpty(p.Logo) ? (MainPathView + p.Logo) : "https://cdn.made-home.com/Home/beet_logo.png",
                CRnumber = p.CRnumber,
                BlockID = p.BlockID,
                IBANNumber = p.IBANNumber,
                PackageID = p.PackageID,
                SwiftCode = p.SwiftCode,
                TaxNumber = p.TaxNumber,
                Gender = p.Gender,
                IsEnableString = lang == "ar" ? (p.IsEnable ? "مفعل" : "غير مفعل") : (p.IsEnable ? "Enabled" : "Disabled"),
                GenderString = lang == "ar" ? (p.Gender == (int)Gender.Male ? "ذكر" : p.Gender == (int)Gender.Female ? "انثى" : "يفضل عدم التحديد") : (p.Gender == (int)Gender.Male ? "Male" : p.Gender == (int)Gender.Female ? "Female" : "Prefer not to specify"),
                DeliveryTypeName = lang == "ar" ? (p.DeliveryType == (int)DeliveryTypeEnum.HomeMade ? "الشركة" : p.DeliveryType == (int)DeliveryTypeEnum.Store ? "المتجر" : "") : (p.DeliveryType == (int)DeliveryTypeEnum.HomeMade ? "Company" : p.DeliveryType == (int)DeliveryTypeEnum.Store ? "Store" : ""),
                DeliveryPrice = p.DeliveryPrice.ToString(),
                RegionName = lang == "ar" ? p.City.NameAR : p.City.NameEN,
                IsVacString = lang == "ar" ? (p.IsVac ? "اجازة" : "قيد العمل") : (p.IsVac ? "Vacation" : "On Work"),
                CreateDateString = p.CreateDate.ToString("hh:mm tt"),

            });
            return getData;
        }
        public List<VendorViewModel> GetVendorViewModelByGuid(string lang, string MainPathView)
        {
            var getData = Uow.Vendors.GetAll(x => !x.IsDeleted && x.IsEnable, "Block,City.Region,Nationality,Package,Banks")
            .Select(p => new VendorViewModel()
            {
                CityID = p.CityID,
                RegionID = p.City.RegionID,
                Lat = p.Lat,
                Lng = p.Lng,
                IsEnable = p.IsEnable,
                MobileNo = p.MobileNo,
                Email = p.Email,
                EnableDate = p.EnableDate,
                IsDeleted = p.IsDeleted,
                IsEnabled = p.IsEnabled,

                UpdateDate = p.UpdateDate,
                UserUpdate = p.UserUpdate,
                CreateDate = p.CreateDate,
                DeleteDate = p.DeleteDate,
                UserId = p.UserId,
                UserDelete = p.UserDelete,
                UserEnable = p.UserEnable,
                NationalityID = p.NationalityID != null ? (int)p.NationalityID : 0,
                FirstName = lang == "ar" ? p.FirstNameAr : p.FirstNameEn,
                SeconedName = lang == "ar" ? p.SeconedNameAr : p.SeconedNameEn,
                FirstNameAr = p.FirstNameAr,
                SeconedNameAr = p.SeconedNameAr,
                FirstNameEn = p.FirstNameEn,
                SeconedNameEn = p.SeconedNameEn,
                VendorsGuid = p.VendorsGuid,
                StoreNameAr = p.StoreNameAr,
                StoreNameEn = p.StoreNameEn,
                AboutStoreAr = p.AboutStoreAr,
                AboutStoreEn = p.AboutStoreEn,
                VendorsID = p.VendorsID,
                AccountNumber = p.AccountNumber,
                Address = p.Address,
                BanksID = p.BanksID,
                Banner = !string.IsNullOrEmpty(p.Banner) ? (MainPathView + p.Banner) : p.Banner,
                ProfilePic = !string.IsNullOrEmpty(p.ProfilePic) ? (MainPathView + p.ProfilePic) : "https://cdn.made-home.com/Home/beet_logo.png",
                CRPic = !string.IsNullOrEmpty(p.CRPic) ? (MainPathView + p.CRPic) : MainPathView + "https://cdn.made-home.com/Home/beet_logo.png",
                Logo = !string.IsNullOrEmpty(p.Logo) ? (MainPathView + p.Logo) : MainPathView + "https://cdn.made-home.com/Home/beet_logo.png",
                CRnumber = p.CRnumber,
                BlockID = p.BlockID,
                IBANNumber = p.IBANNumber,
                PackageID = p.PackageID,
                SwiftCode = p.SwiftCode,
                TaxNumber = p.TaxNumber,
                Gender = p.Gender,
                IsEnableString = lang == "ar" ? (p.IsEnable ? "مفعل" : "غير مفعل") : (p.IsEnable ? "Enabled" : "Disabled"),
                GenderString = lang == "ar" ? (p.Gender == (int)Gender.Male ? "ذكر" : p.Gender == (int)Gender.Female ? "انثى" : "يفضل عدم التحديد") : (p.Gender == (int)Gender.Male ? "Male" : p.Gender == (int)Gender.Female ? "Female" : "Prefer not to specify"),
                CityName = lang == "ar" ? p.City.NameAR : p.City.NameEN,
                RegionName = lang == "ar" ? p.City.Region.NameAR : p.City.Region.NameEN,
                NationalityName = p.Nationality != null ? (lang == "ar" ? p.Nationality.NameAR : p.Nationality.NameEN) : " -- ",
                BlockName = p.Block != null ? (lang == "ar" ? p.Block.NameAR : p.Block.NameEN) : " -- ",
                PackageName = p.Package != null ? (lang == "ar" ? p.Package.NameAR : p.Package.NameEN) : " -- ",
                BanksName = p.Banks != null ? (lang == "ar" ? p.Banks.NameAR : p.Banks.NameEN) : " -- ",
                DeliveryType = p.DeliveryType,
                DeliveryPrice = p.DeliveryPrice.ToString(),

            }).Take(10).ToList();
            return getData;
        }
        public IEnumerable<SiteVendorsViewModel> GetAllSiteVendorsViewModelByCustomer(int CustomersID, string lang, string VendorPathView)
        {
            var getData = Uow.Vendors.GetAll(x => !x.IsDeleted && x.IsEnable && x.OrderVendor.Any(y => y.Orders.CustomersID == CustomersID)
                ).OrderByDescending(p => p.CreateDate)
            .Select(p => new SiteVendorsViewModel()
            {
                VendorsID = p.VendorsID,
                VendorsLogo = !string.IsNullOrEmpty(p.Logo) ? (VendorPathView + p.Logo) : "/Images/noImage.png",
                VendorsName = lang == "ar" ? (p.StoreNameAr) : (p.StoreNameEn),
                VendorsGuid = p.VendorsGuid,
                WorkingTimes = !string.IsNullOrEmpty(p.WorkingTimes) ? p.WorkingTimes : "",
                VendorsDaysWork = p.DaysWork,
                VendorsWorkFrom = p.WorkFrom,
                VendorsWorkTo = p.WorkTo,
                VendorsDaysVac = p.DaysVac,
                VendorsVacWorkFrom = p.VacWorkFrom,
                VendorsVacWorkTo = p.VacWorkTo,
            });
            return getData;

        }
        #endregion
        #region Action
        public bool ChangeStatusEnable(int id, int userId)
        {
            var data = GetById(id);
            if (data != null)
            {
                data.UserEnable = userId;
                data.IsEnable = data.IsEnable == Convert.ToBoolean(StatusEnable.Enable) ? Convert.ToBoolean(StatusEnable.Disable) : Convert.ToBoolean(StatusEnable.Enable);
                data.EnableDate = _bLGeneral.GetDateTimeNow();
                data = Uow.Vendors.Update(data);
                Uow.Commit();
                return true;
            }
            return false;
        }
        public async Task<bool> CreateVendor(CreateVendorViewModel model, string profilePhotofileName, int UserId)
        {
            int gender = (int)Gender.Male;
            int nationaltyID = _blNationality.GetFirstNationality().NationalityID;
            Vendors vendors = new Vendors()
            {
                CreateDate = _bLGeneral.GetDateTimeNow(),
                VendorsGuid = Guid.NewGuid(),
                Email = model.email,
                MobileNo = model.mobileNo,
                FirstNameAr = model.firstName,
                FirstNameEn = model.firstName,
                Gender = gender,
                IsDeleted = false,
                SeconedNameAr = " ",
                SeconedNameEn = " ",
                UserId = UserId,
                CityID = model.cityID,
                NationalityID = nationaltyID,
                ProfilePic = profilePhotofileName,
                RegisterType = (int)RegisterType.Created,
                DeliveryType = (int)DeliveryTypeEnum.HomeMade,
                DeliveryPrice = 0,
                Logo = profilePhotofileName,
                BlockID = model.blockID,
                Lat = model.lat,
                Lng = model.lng,
                Address = model.address,

            };
            Uow.Vendors.Insert(vendors);
            var RoleID = 2;
            var user = await _userManager.FindByIdAsync(UserId.ToString());
            if (!_bLPermission.GetIsUserInRole(RoleID, UserId))
            {
                var identityResult = _bLPermission.CreateUserRole(RoleID, UserId);
            }
            var Model = new PermissionViewModel();
            Model.UserId = UserId;
            Model.RoleID = RoleID;
            Model.CheckedItems = _bLPermission.GetAllPermissionControllerActionByRoleFromRoleConfig(RoleID);
            var Res = _bLPermission.EditRolePermissionsNoCommit(Model);
            Uow.Commit();
            return true;
        }
        public bool UpdateVendorApi(ProfileVendorViewModel model, string profilePhotofileName, int VendorID, int UserId)
        {
            var vendors = GetById(VendorID);

            vendors.UpdateDate = _bLGeneral.GetDateTimeNow();
            vendors.Email = model.email;
            vendors.MobileNo = model.mobileNo;
            vendors.FirstNameAr = model.firstName;
            vendors.FirstNameEn = model.firstName;
            vendors.Gender = model.gender;
            vendors.SeconedNameAr = " ";
            vendors.SeconedNameEn = " ";
            vendors.UserUpdate = UserId;
            //vendors.CityID = model.cityID;
            vendors.NationalityID = model.nationaltyID;
            if (!string.IsNullOrWhiteSpace(profilePhotofileName))
            {
                vendors.ProfilePic = profilePhotofileName;
            }
            Uow.Vendors.Update(vendors);
            Uow.Commit();
            return true;
        }
        public bool CreateSiteVendor(VendorSiteRequestVM model, int UserId)
        {
            int gendid = int.Parse(model.gender);
            Vendors vendors = new Vendors();


            vendors.CreateDate = _bLGeneral.GetDateTimeNow();
            vendors.VendorsGuid = Guid.NewGuid();
            vendors.Email = model.Txt_Email;
            vendors.MobileNo = model.Txt_Mobile;
            vendors.FirstNameAr = model.Txt_FnameAr;
            vendors.FirstNameEn = model.Txt_FnameAr;
            vendors.SeconedNameAr = " ";
            vendors.SeconedNameEn = " ";
            vendors.Gender = gendid;
            vendors.IsDeleted = false;
            vendors.UserId = UserId;
            vendors.CityID = model.cities;
            vendors.StoreNameAr = model.Txt_storenameAr;
            vendors.StoreNameEn = model.Txt_storenameEn;
            vendors.AboutStoreAr = model.Txt_MoreaboutservicesAr;
            vendors.AboutStoreEn = model.Txt_MoreaboutservicesEn;
            vendors.CRnumber = model.Txt_CommercialNumber;
            vendors.TaxNumber = model.Txt_TaxNumber;
            vendors.MaarofNumber = model.Txt_knownnumber;
            vendors.AccountNumber = model.Txt_AccountNo;
            vendors.IBANNumber = model.Txt_IBAN;
            vendors.SwiftCode = model.Txt_SwiftCode;
            vendors.Address = model.Txt_Address;
            vendors.ActivityID = model.Activities;
            vendors.BanksID = model.Banks;
            vendors.BlockID = model.blocks != 0 ? model.blocks : null;
            vendors.PackageID = Uow.Package.GetAll(e => e.PackageType == (int)PackageTypeEnum.Default).FirstOrDefault()?.PackageID;
            vendors.Lat = model.lat != null ? double.Parse(model.lat, CultureInfo.InvariantCulture) : 0;
            vendors.Lng = model.lng != null ? double.Parse(model.lng, CultureInfo.InvariantCulture) : 0;
            vendors.UserUpdate = UserId;
            vendors.RegisterType = (int)RegisterType.Submitted;
            vendors.ProfilePic = model.imageownerlogo;
            vendors.Logo = model.imagestorelogo;
            vendors.DeliveryPrice = 0;
            vendors.Banner = string.Empty;
            vendors.NationalityID = model.WizzardNationalityID;
            vendors.CRPic = model.imagecommercialcumber;

            if (!_bLPermission.GetIsUserInRole((int)Rolenum.Vendor, UserId))
            {
                var identityResult = _bLPermission.CreateUserRole((int)Rolenum.Vendor, UserId);
            }
            var Model = new PermissionViewModel();
            Model.UserId = UserId;
            Model.RoleID = (int)Rolenum.Vendor;
            Model.CheckedItems = _bLPermission.GetAllPermissionControllerActionByRoleFromRoleConfig((int)Rolenum.Vendor);

            var Res = _bLPermission.EditRolePermissionsNoCommit(Model);

            Uow.Vendors.Insert(vendors);
            Uow.Commit();
            return true;
        }
        public bool MonthyTargetUpdate(decimal MonthlyT, int VendorID, int UserId)
        {
            var vendors = GetById(VendorID);
            vendors.UpdateDate = _bLGeneral.GetDateTimeNow();
            vendors.UserUpdate = UserId;
            vendors.MonthlyTarget = MonthlyT;
            Uow.Vendors.Update(vendors);
            Uow.Commit();
            return true;
        }
        public async Task<ReturnVendorViewModel> Insert(VendorViewModel model)
        {
            Vendors vendors = new Vendors()
            {
                CreateDate = _bLGeneral.GetDateTimeNow(),
                VendorsGuid = Guid.NewGuid(),
                Email = model.Email,
                MobileNo = model.MobileNo,
                FirstNameAr = model.FirstNameAr,
                FirstNameEn = model.FirstNameAr,
                Gender = model.Gender,
                IsDeleted = false,
                SeconedNameAr = " ",
                SeconedNameEn = " ",
                UserId = model.UserId,
                CityID = model.CityID,
                NationalityID = model.NationalityID,
                RegisterType = (int)RegisterType.Created,
                DeliveryType = model.DeliveryType,
                DeliveryPrice = model.DeliveryPrice != null ? decimal.Parse(model.DeliveryPrice) : 0,
                ProfilePic = model.ProfilePic,
                BlockID = model.BlockID,
                IsShowContact = false, 
                PackageID = model.PackageID,
                DaysWork = model.DaysWork,
                WorkFrom = model.WorkFrom,
                WorkTo = model.WorkTo,
                IsDaysWork = !string.IsNullOrEmpty(model.DaysWork) ? true : false,
                DaysVac = model.DaysVac,
                VacWorkFrom = model.VacWorkFrom,
                VacWorkTo = model.VacWorkTo,
                IsDaysVac = !string.IsNullOrEmpty(model.DaysVac) ? true : false,
            };
            vendors = Uow.Vendors.Insert(vendors);
            string[] rolesstr = model.Roles;
            foreach (var item in rolesstr)
            {
                if (model.UserId != 0 && !string.IsNullOrWhiteSpace(item))
                {
                    var RoleID = int.Parse(item);
                    var user = await _userManager.FindByIdAsync(model.UserId.ToString());
                    if (!_bLPermission.GetIsUserInRole(RoleID, model.UserId))
                    {
                        var identityResult = _bLPermission.CreateUserRole(RoleID, model.UserId);
                    }
                    var Model = new PermissionViewModel();
                    Model.UserId = model.UserId;
                    Model.RoleID = RoleID;
                    Model.CheckedItems = model.CheckedItems;
                    var Res = _bLPermission.EditRolePermissionsNoCommit(Model);
                }
            }
            Uow.Commit();
            var obj = new ReturnVendorViewModel();
            obj.VendorsID = vendors.VendorsID;
            obj.BoolID = true;
            return obj;
        }
        public bool Update(VendorViewModel model, int UserId, string MainPath, string MainPathView)
        {
            var vendors = GetById(model.VendorsID);
            if (vendors != null)
            {
                if (!string.IsNullOrEmpty(model.ProfilePic))
                {
                    if (MainPathView + vendors.ProfilePic != model.ProfilePic)
                    {
                        string FileName = Guid.NewGuid() + ".png";
                        var Photo = FileName;
                        _bLGeneral.SaveImage(new BLGeneral.SaveImageModel
                        {
                            base64 = model.ProfilePic,
                            key = "",
                            fileName = FileName,
                            mainPath = MainPath
                        });
                        vendors.ProfilePic = Photo;
                    }
                }
                vendors.UpdateDate = _bLGeneral.GetDateTimeNow();
                vendors.Email = model.Email;
                vendors.MobileNo = !string.IsNullOrEmpty(model.MobileNo) ? model.MobileNo : vendors.MobileNo;
                vendors.FirstNameAr = model.FirstNameAr;
                vendors.FirstNameEn = model.FirstNameAr;
                vendors.Gender = model.Gender;
                vendors.UserUpdate = UserId;
                vendors.CityID = model.CityID;
                vendors.BlockID = model.BlockID;
                vendors.NationalityID = model.NationalityID;
                vendors.IsShowContact = false;
                vendors.PackageID = model.PackageID;

                vendors.DaysWork = model.DaysWork;
                vendors.WorkFrom = model.WorkFrom;
                vendors.WorkTo = model.WorkTo;
                vendors.IsDaysWork = !string.IsNullOrEmpty(model.DaysWork) ? true : false;
                vendors.DaysVac = model.DaysVac;
                vendors.VacWorkFrom = model.VacWorkFrom;
                vendors.VacWorkTo = model.VacWorkTo;
                vendors.IsDaysVac = !string.IsNullOrEmpty(model.DaysVac) ? true : false;

                vendors = Uow.Vendors.Update(vendors);
                Uow.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool UpdateProfile(int vendorID, ProfileViewModel model)
        {
            var obj = GetById(vendorID);
            if (obj != null)
            {

                obj.UserUpdate = model.UserUpdate;
                obj.UpdateDate = _bLGeneral.GetDateTimeNow();
                obj.FirstNameAr = model.VendorFirstName;
                obj.SeconedNameAr = model.VendorFirstName;
                obj.Email = model.Email;
                obj.MobileNo = model.MobileNo;
                obj.CityID = model.VendorCityId;
                obj.BlockID = model.VendorBlockId;
                obj = Uow.Vendors.Update(obj);
                Uow.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool UpdatePersonalImage(int OperatorId, string domain, string imgPath, ref string FileName, Stream stream, int userId)
        {
            var Vendor = GetById(OperatorId);
            if (Vendor != null)
            {
                Vendor.ProfilePic = /*domain + imgPath +*/ FileName;
                string MainPath = imgPath;

                _bLGeneral.SaveImageWithStream(new BLGeneral.SaveImageModelStream
                {
                    Stream = stream,
                    key = "",
                    fileName = FileName,
                    mainPath = MainPath
                });

                FileName = Vendor.ProfilePic;
                Vendor.UpdateDate = _bLGeneral.GetDateTimeNow();
                Vendor.UserUpdate = userId;
                Vendor = Uow.Vendors.Update(Vendor);
                Uow.Commit();
                return true;
            }
            return false;
        }
        public bool UpdatePayment(PaymentDetails model, int UserId, int vendorsID)
        {
            var vendors = GetVendorsById(vendorsID);
            if (vendors != null)
            {
                vendors.AccountNumber = model.accountNumber;
                vendors.IBANNumber = model.ibanNumber;
                vendors.SwiftCode = model.swiftCode;
                vendors.BanksID = model.bankID;
                vendors.PackageID = model.packageID;
                vendors.UserUpdate = UserId;
                vendors.UpdateDate = _bLGeneral.GetDateTimeNow();
                Uow.Vendors.Update(vendors);
                Uow.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool UpdateLocation(LocationDetails model, int UserId, int vendorsID)
        {
            var vendors = GetVendorsById(vendorsID);
            if (vendors != null)
            {
                vendors.Lat = (double)model.lat;
                vendors.Lng = (double)model.lng;
                vendors.BlockID = model.blockID;
                vendors.CityID = model.cityID;
                vendors.Address = model.address;
                vendors.UserUpdate = UserId;
                vendors.UpdateDate = _bLGeneral.GetDateTimeNow();
                vendors.IsLocation = true;
                Uow.Vendors.Update(vendors);
                Uow.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool UpdateStore(UpdateStoreViewModelApi model, string logofileName, string bannerfileName, string cRPicfileName, int UserId, int vendorsID)
        {
            var vendors = GetVendorsById(vendorsID);
            if (vendors != null)
            {
                if (model.blockID.HasValue)
                {
                    vendors.CityID = Uow.Block.GetAll(s => s.BlockID == model.blockID).FirstOrDefault().CityID;
                }
                vendors.StoreNameEn = model.storeNameEn;
                vendors.StoreNameAr = model.storeNameAr;
                vendors.AboutStoreEn = model.aboutStoreEn;
                vendors.AboutStoreAr = model.aboutStoreAr;
                vendors.CRnumber = model.cRnumber;
                vendors.TaxNumber = model.taxNumber;
                vendors.MaarofNumber = model.maarofNumber;
                vendors.Address = model.address;
                vendors.ActivityID = model.activityID;
                vendors.BlockID = model.blockID;
                vendors.Lat = model.lat != null ? (double)model.lat : 0;
                vendors.Lng = model.lng != null ? (double)model.lng : 0;
                vendors.UserUpdate = UserId;
                vendors.UpdateDate = _bLGeneral.GetDateTimeNow();
                vendors.DaysWork = model.daysWork;
                vendors.WorkFrom = model.workFrom;
                vendors.WorkTo = model.workTo;
                vendors.IsDaysWork = !string.IsNullOrEmpty(model.daysWork) ? true : false;
                vendors.DaysVac = model.daysVac;
                vendors.VacWorkFrom = model.vacWorkFrom;
                vendors.VacWorkTo = model.vacWorkTo;
                vendors.IsDaysVac = !string.IsNullOrEmpty(model.daysVac) ? true : false;

                if (!string.IsNullOrWhiteSpace(logofileName))
                {
                    vendors.Logo = logofileName;
                }
                if (!string.IsNullOrWhiteSpace(bannerfileName))
                {
                    vendors.Banner = bannerfileName;
                }
                if (!string.IsNullOrWhiteSpace(cRPicfileName))
                {
                    vendors.CRPic = cRPicfileName;
                }
                Uow.Vendors.Update(vendors);
                Uow.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }
        public UpdateStoreViewModel GetStoreDetails(int VendorsID, string lang, string vendorpath)
        {
            var data = Uow.Vendors.GetAll(s => s.VendorsID == VendorsID).Select(m => new UpdateStoreViewModel()
            {
                aboutStoreAr = !string.IsNullOrWhiteSpace(m.AboutStoreAr) ? m.AboutStoreAr : "",
                aboutStoreEn = !string.IsNullOrWhiteSpace(m.AboutStoreEn) ? m.AboutStoreEn : "",
                activityID = m.ActivityID != null ? m.ActivityID : 0,
                blockID = m.BlockID != null ? m.BlockID : 0,
                cityID = m.CityID,
                cRnumber = !string.IsNullOrWhiteSpace(m.CRnumber) ? m.CRnumber : "",
                address = !string.IsNullOrWhiteSpace(m.Address) ? m.Address : "",
                storeNameAr = !string.IsNullOrWhiteSpace(m.StoreNameAr) ? m.StoreNameAr : "",
                storeNameEn = !string.IsNullOrWhiteSpace(m.StoreNameEn) ? m.StoreNameEn : "",
                taxNumber = !string.IsNullOrWhiteSpace(m.TaxNumber) ? m.TaxNumber : "",
                banner = !string.IsNullOrWhiteSpace(m.Banner) ? vendorpath + m.Banner : "",
                logo = !string.IsNullOrWhiteSpace(m.Logo) ? vendorpath + m.Logo : "",
                cRPic = !string.IsNullOrWhiteSpace(m.CRPic) ? vendorpath + m.CRPic : "",
                maarofNumber = !string.IsNullOrWhiteSpace(m.MaarofNumber) ? m.MaarofNumber : "",
                lat = m.Lat,
                lng = m.Lng,
                workFrom = m.WorkFrom != null ? m.WorkFrom.Value.ToString("HH:mm") : string.Empty,
                workTo = m.WorkTo != null ? m.WorkTo.Value.ToString("HH:mm") : string.Empty,
                daysVac = m.DaysVac,
                vacWorkFrom = m.VacWorkFrom != null ? m.VacWorkFrom.Value.ToString("HH:mm") : string.Empty,
                vacWorkTo = m.VacWorkTo != null ? m.VacWorkTo.Value.ToString("HH:mm") : string.Empty,
                daysWork = m.DaysWork,
            }).FirstOrDefault();
            if (!string.IsNullOrEmpty(data.daysWork))
            {
                var ListDaysWork = !string.IsNullOrEmpty(data.daysWork) ? data.daysWork.Split(',') : null;
                data.listDaysWork = ListDaysWork.Where(x => !string.IsNullOrEmpty(x)).Select(x => Convert.ToInt32(x)).ToList();
            }
            if (!string.IsNullOrEmpty(data.daysVac))
            {
                var ListDaysVac = !string.IsNullOrEmpty(data.daysVac) ? data.daysVac.Split(',') : null;
                data.listDaysVac = ListDaysVac.Where(x => !string.IsNullOrEmpty(x)).Select(x => Convert.ToInt32(x)).ToList();
            }
            return data;
        }
        public PaymentDetails GetPaymentDetails(int VendorsID)
        {
            return Uow.Vendors.GetAll(s => s.VendorsID == VendorsID).Select(m => new PaymentDetails()
            {
                accountNumber = !string.IsNullOrWhiteSpace(m.AccountNumber) ? m.AccountNumber : "",
                swiftCode = !string.IsNullOrWhiteSpace(m.SwiftCode) ? m.SwiftCode : "",
                bankID = m.BanksID != null ? m.BanksID : 0,
                packageID = m.PackageID != null ? m.PackageID : 0,
                ibanNumber = !string.IsNullOrWhiteSpace(m.IBANNumber) ? m.IBANNumber : "",
            }).FirstOrDefault();
        }
        public bool UpdateStoreData(VendorStoreViewModel model, string logofileName, string bannerfileName, string cRPicfileName, int UserId)
        {
            var vendors = GetVendorsById(model.VendorsID);
            if (vendors != null)
            {
                vendors.StoreNameEn = model.StoreNameEn;
                vendors.StoreNameAr = model.StoreNameAr;
                vendors.AboutStoreEn = model.AboutStoreEn;
                vendors.AboutStoreAr = model.AboutStoreAr;
                vendors.CRnumber = model.CRnumber;
                vendors.DeliveryType = model.DeliveryType;
                vendors.CRnumber = model.CRnumber;
                vendors.UserUpdate = UserId;
                vendors.UpdateDate = _bLGeneral.GetDateTimeNow();
                if (!string.IsNullOrWhiteSpace(logofileName))
                {
                    vendors.Logo = logofileName;
                }
                if (!string.IsNullOrWhiteSpace(bannerfileName))
                {
                    vendors.Banner = bannerfileName;
                }
                if (!string.IsNullOrWhiteSpace(cRPicfileName))
                {
                    vendors.CRPic = cRPicfileName;
                }
                vendors.RegisterType = (int)RegisterType.Submitted;
                Uow.Vendors.Update(vendors);
                Uow.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool UpdateVendorAccount(VendorAccountViewModel model, int UserId)
        {
            var vendors = GetVendorsById(model.VendorsID);
            if (vendors != null)
            {
                vendors.TaxNumber = model.TaxNumber;
                vendors.AccountNumber = model.AccountNumber;
                vendors.IBANNumber = model.IBANNumber;
                vendors.SwiftCode = model.SwiftCode;
                vendors.BanksID = model.BanksID;
                vendors.UserUpdate = UserId;
                vendors.UpdateDate = _bLGeneral.GetDateTimeNow();
                Uow.Vendors.Update(vendors);
                Uow.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool UpdateVendorLocation(VendorLocationViewModel model, int UserId)
        {
            var vendors = GetVendorsById(model.VendorsID);
            if (vendors != null)
            {
                vendors.Address = model.Address;
                vendors.Lat = double.Parse(model.Lat, System.Globalization.CultureInfo.InvariantCulture);
                vendors.Lng = double.Parse(model.Lng, System.Globalization.CultureInfo.InvariantCulture);
                vendors.UserUpdate = UserId;
                vendors.UpdateDate = _bLGeneral.GetDateTimeNow();
                Uow.Vendors.Update(vendors);
                Uow.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Delete(int id, int userId, Guid vendorNewGuid)
        {
            var Vendors = GetById(id);
            if (Vendors != null)
            {
                Vendors.IsDeleted = true;
                Vendors.UserDelete = userId;
                Vendors.DeleteDate = _bLGeneral.GetDateTimeNow();
                Uow.Vendors.Update(Vendors);

                var vendorDataVendor = _blTokens.GetMobileDataByUserID(Vendors.UserId);
                if (vendorDataVendor != null)
                {
                    Uow.Tokens.Delete(vendorDataVendor);
                    _blTokens.UpdateUserJWTToken(Vendors.UserId, string.Empty);
                }

                Uow.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool ChangeStatus(int id, int userId)
        {
            var data = GetById(id);
            if (data != null)
            {
                data.UserEnable = userId;
                data.IsEnable = data.IsEnable == Convert.ToBoolean(StatusEnable.Enable) ? Convert.ToBoolean(StatusEnable.Disable) : Convert.ToBoolean(StatusEnable.Enable);
                data.EnableDate = _bLGeneral.GetDateTimeNow();
                data.EnableHistory.Add(new EnableHistory()
                {
                    CreateDate = _bLGeneral.GetDateTimeNow(),
                    EnableHistoryGuid = Guid.NewGuid(),
                    IsDeleted = false,
                    IsEnable = true,
                    Status = data.IsEnable == Convert.ToBoolean(StatusEnable.Enable) ? Convert.ToBoolean(StatusEnable.Disable) : Convert.ToBoolean(StatusEnable.Enable),
                    UserId = userId,
                });
                data = Uow.Vendors.Update(data);
                if (!data.IsEnable)
                {
                     int num = data.UserId;
                    _blTokens.UpdateTokens(num, 0, string.Empty);
                    _blTokens.UpdateUserJWTToken(num, string.Empty);

                }
                Uow.Commit();
                return true;
            }
            return false;
        }
        public bool CompleteRegister(CompleteRegisterApi model, string logofileName, string bannerfileName, string cRPicfileName, int UserId, int vendorsID)
        {
            var vendors = GetVendorsById(vendorsID);
            if (vendors != null)
            {
                vendors.StoreNameEn = model.storeNameEn;
                vendors.StoreNameAr = model.storeNameAr;
                vendors.AboutStoreEn = model.aboutStoreEn;
                vendors.AboutStoreAr = model.aboutStoreAr;
                vendors.CRnumber = model.cRnumber;
                vendors.TaxNumber = model.taxNumber;
                vendors.MaarofNumber = model.maarofNumber;
                vendors.AccountNumber = model.accountNumber;
                vendors.IBANNumber = model.ibanNumber;
                vendors.SwiftCode = model.swiftCode;
                vendors.ActivityID = model.activityID;
                vendors.BanksID = model.bankID;
                vendors.PackageID = Uow.Package.GetAll(e => e.PackageType == (int)PackageTypeEnum.Default).FirstOrDefault().PackageID;
                vendors.UserUpdate = UserId;
                vendors.UpdateDate = _bLGeneral.GetDateTimeNow();
                vendors.RegisterType = (int)RegisterType.Submitted;
                vendors.IsShowContact = false;
                if (!string.IsNullOrWhiteSpace(logofileName))
                {
                    vendors.Logo = logofileName;
                }
                if (!string.IsNullOrWhiteSpace(bannerfileName))
                {
                    vendors.Banner = bannerfileName;
                }
                if (!string.IsNullOrWhiteSpace(cRPicfileName))
                {
                    vendors.CRPic = cRPicfileName;
                }

                vendors.DaysWork = model.daysWork;
                vendors.WorkFrom = model.workFrom;
                vendors.WorkTo = model.workTo;
                vendors.IsDaysWork = !string.IsNullOrEmpty(model.daysWork) ? true : false;
                vendors.DaysVac = model.daysVac;
                vendors.VacWorkFrom = model.vacWorkFrom;
                vendors.VacWorkTo = model.vacWorkTo;
                vendors.IsDaysVac = !string.IsNullOrEmpty(model.daysVac) ? true : false;

                Uow.Vendors.Update(vendors);
                Uow.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool InsertRequestQuantity(List<RequestQuantityViewModel> model, int vendorsID, int userId)
        {
            try
            {
                var RequestMaster = new QuantitiesRequest()
                {
                    VendorsID = vendorsID,
                    CreateDate = _bLGeneral.GetDateTimeNow(),
                    IsDeleted = false,
                    IsEnable = true,
                    QuantitiesRequestGuid = Guid.NewGuid(),
                    UserId = userId
                };
                foreach (var item in model)
                {
                    var ProdData = Uow.Product.GetAll(s => s.ProductID == item.productID).FirstOrDefault();
                    RequestMaster.QuantitiesRequestProduct.Add(new QuantitiesRequestProduct()
                    {
                        UserId = userId,
                        CreateDate = _bLGeneral.GetDateTimeNow(),
                        IsDeleted = false,
                        IsEnable = true,
                        ProductImage = ProdData.Logo,
                        ProductID = item.productID,
                        ProductName = ProdData.NameEn,
                        ProductPrice = ProdData.Price,
                        QuantitiesRequestProductGuid = Guid.NewGuid(),
                        Quantity = item.quantity,
                        QuantityAllowed = item.quantity,
                        QuantityInventory = item.quantity,
                    });
                }
                Uow.QuantitiesRequest.Insert(RequestMaster);
                Uow.Commit();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion
        #region Site
        public IEnumerable<SiteVendorsViewModel> GetAllSiteVendorsViewModel(string searchVendors, string lang, string VendorPathView, string ProductPathView)
        {
            var getData = Uow.Vendors.GetAll(x => !x.IsDeleted && x.IsEnable
            && (!string.IsNullOrEmpty(searchVendors) ? (x.StoreNameAr.Contains(searchVendors) || x.StoreNameEn.Contains(searchVendors)) : true)
                ).ToList().OrderByDescending(x => x.CreateDate).OrderByDescending(p => IsVendorWorking(p.DaysWork, p.WorkFrom, p.WorkTo, p.DaysVac, p.VacWorkFrom, p.VacWorkTo))
            .Select(p => new SiteVendorsViewModel()
            {
                VendorsID = p.VendorsID,
                VendorsLogo = !string.IsNullOrEmpty(p.Logo) ? (VendorPathView + p.Logo) : "/Images/noImage.png",
                VendorsName = lang == "ar" ? (p.StoreNameAr) : (p.StoreNameEn),
                VendorsGuid = p.VendorsGuid,
                WorkingTimes = !string.IsNullOrEmpty(p.WorkingTimes) ? p.WorkingTimes : "",
                VendorsDaysWork = p.DaysWork,
                VendorsWorkFrom = p.WorkFrom,
                VendorsWorkTo = p.WorkTo,
                VendorsDaysVac = p.DaysVac,
                VendorsVacWorkFrom = p.VacWorkFrom,
                VendorsVacWorkTo = p.VacWorkTo,
            });
            return getData;

        }
        #endregion
        #region Auto_Job
        public string CancelAuto(int ordervedorId, int userid, string lang, int orderStatusID, int? CancelReasonID, string FireBaseKey)
        {
            var Message = "true";
            var OrderData = Uow.OrderVendor.GetAll(s => s.OrderVendorID == ordervedorId && !s.IsDeleted, "OrderStatus,Vendors,Orders.Customers,OrderItems").FirstOrDefault();
            if (OrderData != null)
            {
                try
                {
                    var OrderStatusData = Uow.OrderStatus.GetAll(s => s.OrderStatusID == orderStatusID).FirstOrDefault();
                    OrderData.OrderStatusID = orderStatusID;
                    OrderData.UpdateDate = _bLGeneral.GetDateTimeNow();
                    OrderData.UserUpdate = userid;
                    OrderData.CancelTypeID = (int)CancelTypeEnum.Auto;
                    OrderData.OrderHistory.Add(new OrderHistory()
                    {
                        UserId = userid,
                        Lat = 0,
                        Lng = 0,
                        OrderStatusID = orderStatusID,
                        OrderHistoryGuid = Guid.NewGuid(),
                        IsEnable = true,
                        IsDeleted = false,
                        CreateDate = _bLGeneral.GetDateTimeNow(),
                    });
                    var OrderMasterData = Uow.Orders.GetAll(s => s.OrdersID == OrderData.OrdersID).FirstOrDefault();
                    if (CancelReasonID == (int)CancelReasonEnum.Return_to_Wallet)
                    {
                        _BlCustomerBalance.InsertBalanceWithOutCommit((int)TypeOperationEnum.CR, OrderData.Total, (int)TransactionTypeEnum.Pay_Order, OrderMasterData.CustomersID
                        , userid, OrderData.OrderVendorID, _bLGeneral.GetDateTimeNow(), "", "", 0);
                        OrderData.InvoiceTypeId = (int)OrderInvoiceTypeEnum.Non_Invoice;
                        #region Noti
                        OrderData.Notification.Add(new Notification()
                        {
                            CreateDate = _bLGeneral.GetDateTimeNow(),
                            CustomersID = OrderData.Orders.CustomersID,
                            DescAR = "تم إلغاء الطلب رقم " + OrderData.OrderVendorID + " لعدم قبول التاجر ، وتم ارجاع المبلغ للمحفظة لإعادة الشراء.",
                            DescEN = "your order has been cancelled and order amount returned to your wallet",
                            TitleAR = "إلغاء الطلب لعدم القبول.",
                            TitleEN = "Cancel Order Cause not accept",
                            IsDeleted = false,
                            IsEnable = true,
                            IsSeen = false,
                            NotificationGuid = Guid.NewGuid(),
                            NotificationTypeID = (int)NotificationTypeEnum.Cancel_Auto,
                            UserId = userid,
                            NotificationDate = _bLGeneral.GetDateTimeNow(),
                            OrderVendorID = OrderData.OrderVendorID,
                        });
                        var _PushListVendor = new PushList()
                        {
                            orderId = OrderData.OrderVendorID,
                            lat = 0,
                            lng = 0,
                            trackNo = OrderData.OrderVendorID.ToString(),
                            profit = "",
                            cusotmerAddress = "",
                            vendorAddress = "",
                            vendorLogo = "",
                            vendorName = "",
                            body = "تم إلغاء الطلب رقم " + OrderData.OrderVendorID + " لعدم قبول التاجر ، وتم ارجاع المبلغ للمحفظة لإعادة الشراء.",
                            sound = "default", //For IOS
                            title = "إلغاء الطلب لعدم القبول",
                            content_available = "true", //For Android
                            priority = "high", //For Android,
                            serverKey = FireBaseKey,
                            estimation = 20,
                            pushType = (int)PushTypeEnum.Order
                        };
                        var UserDataVendor = _blTokens.GetMobileDataByUserID(OrderData.Orders.Customers.UserId);
                        if (UserDataVendor != null)
                        {
                            _bLGeneral.SendPush(UserDataVendor.TokenVal, UserDataVendor.DeviceType, _PushListVendor);
                        }
                        #endregion
                    }
                    OrderMasterData.OrderStatusID = orderStatusID;
                    OrderMasterData.UpdateDate = _bLGeneral.GetDateTimeNow();
                    OrderMasterData.UserId = userid;
                    Uow.Orders.Update(OrderMasterData);
                    Uow.OrderVendor.Update(OrderData);
                    #region Daily_Quantity
                    foreach (var item in OrderData.OrderItems)
                    {
                        var ProdData = Uow.Product.GetAll(s => s.ProductID == item.ProductID).FirstOrDefault();
                        if (ProdData.IsLimited)
                        {
                            ProdData.DailyQuantity = ProdData.DailyQuantity + item.Quantity;
                            Uow.Product.Update(ProdData);
                        }
                    }
                    #endregion
                }
                catch (Exception ex)
                {

                    Message = ex.Message;
                }
            }
            else
            {
                Message = lang == "ar" ? "خطأ في رقم الطلب" : "error in order Id";
            }
            return Message;
        }
        public string CheckAllCreatedOrders(string firebaseKey)
        {
            try
            {
                var CountCancel = 0;
                var CountWarning = 0;
                int userId = 1;
                var AllOrders = Uow.OrderVendor.GetAll(s => !s.IsDeleted && s.OrderStatusID == (int)OrderStatusEnum.Create
                , "Vendors");
                foreach (var item in AllOrders)
                {
                    var date = item.CreateDate;
                    if (item.OrderTypeId == (int)OrderTypeEnum.Schedule)
                    {
                        date = item.ScheduleDate.Value.Date;
                        var time = item.ScheduleTime.Value.TimeOfDay;
                        date = date.Add(time);
                    }
                    if (date <= DateTime.Now.AddMinutes(-10)) //حالة ان الطلب مر عليه 10 دقايق نبعت بوش بالالغاء وننفذ اكشن الغاء
                    {
                        CancelAuto(item.OrderVendorID, userId, "ar", (int)OrderStatusEnum.Cancel, (int)CancelReasonEnum.Return_to_Wallet, firebaseKey);
                        item.Notification.Add(new Notification()
                        {
                            CreateDate = _bLGeneral.GetDateTimeNow(),
                            DescAR = "تم إلغاء الطلب رقم " + item.OrderVendorID + " لعدم قبول لمدة عشر دقائق.",
                            DescEN = "Order No " + item.OrderVendorID + " has benn canceled cause it's over 10 mintues",
                            TitleAR = "إلغاء الطلب لعدم القبول",
                            TitleEN = "Cancel Order cause not accept",
                            IsDeleted = false,
                            IsEnable = true,
                            IsSeen = false,
                            NotificationGuid = Guid.NewGuid(),
                            NotificationTypeID = (int)NotificationTypeEnum.Cancel_Auto,
                            UserId = userId,
                            NotificationDate = _bLGeneral.GetDateTimeNow(),
                            OrderVendorID = item.OrderVendorID,
                            VendorsID = item.VendorsID
                        });
                        var _PushListVendor = new PushList()
                        {
                            orderId = item.OrderVendorID,
                            lat = 0,
                            lng = 0,
                            trackNo = item.OrderVendorID.ToString(),
                            profit = "",
                            cusotmerAddress = "",
                            vendorAddress = "",
                            vendorLogo = "",
                            vendorName = "",
                            body = "تم إلغاء الطلب رقم " + item.OrderVendorID + " لعدم قبول لمدة عشر دقائق.",
                            sound = "custom_sound.wav", //For IOS
                            title = "إلغاء الطلب لعدم القبول.",
                            content_available = "true", //For Android
                            priority = "high", //For Android,
                            serverKey = firebaseKey,
                            estimation = 20,
                            pushType = (int)PushTypeEnum.Order
                        };
                        var UserDataVendor = _blTokens.GetMobileDataByUserID(item.Vendors.UserId);
                        if (UserDataVendor != null)
                        {
                            _bLGeneral.SendPush(UserDataVendor.TokenVal, UserDataVendor.DeviceType, _PushListVendor);
                        }
                        CountCancel++;
                    }
                    else if (date <= DateTime.Now.AddMinutes(-5))//حالة ان الطلب مر عليه 5 دقايق نبعت بوش بالتحذير 
                    {
                        var isalreadysend = Uow.Notification.GetAll(s => s.OrderVendorID == item.OrderVendorID && s.NotificationTypeID == (int)NotificationTypeEnum.Warnning_Order).Any();
                        if (!isalreadysend)
                        {
                            CountWarning++;
                            var time = DateTime.Now.Subtract(item.CreateDate).TotalMinutes;
                            item.Notification.Add(new Notification()
                            {
                                CreateDate = _bLGeneral.GetDateTimeNow(),
                                DescAR = "لديك طلب رقم " + item.OrderVendorID + " لم يتم قبوله بعد ، سيتم الغاءه مباشرة بعد خمس دقائق من الآن في حال عدم قبوله.",
                                DescEN = "order no " + item.OrderVendorID + " has been sent from " + time + " minutes if you not accept it will be cancelled auto",
                                TitleAR = "تحذير بالغاء الطلب في حال عدم قبوله",
                                TitleEN = "Cancel Order Warnning",
                                IsDeleted = false,
                                IsEnable = true,
                                IsSeen = false,
                                NotificationGuid = Guid.NewGuid(),
                                NotificationTypeID = (int)NotificationTypeEnum.Warnning_Order,
                                UserId = userId,
                                NotificationDate = _bLGeneral.GetDateTimeNow(),
                                OrderVendorID = item.OrderVendorID,
                                VendorsID = item.VendorsID
                            });
                            var _PushListVendor = new PushList()
                            {
                                orderId = item.OrderVendorID,
                                lat = 0,
                                lng = 0,
                                trackNo = item.OrderVendorID.ToString(),
                                profit = "",
                                cusotmerAddress = "",
                                vendorAddress = "",
                                vendorLogo = "",
                                vendorName = "",
                                body = "لديك طلب رقم " + item.OrderVendorID + " لم يتم قبوله بعد ، سيتم الغاءه مباشرة بعد خمس دقائق من الآن في حال عدم قبوله.",
                                sound = "custom_sound.wav", //For IOS
                                title = "تحذير بالغاء الطلب في حال عدم قبوله",
                                content_available = "true", //For Android
                                priority = "high", //For Android,
                                serverKey = firebaseKey,
                                estimation = 20,
                                pushType = (int)PushTypeEnum.Order
                            };
                            var UserDataVendor = _blTokens.GetMobileDataByUserID(item.Vendors.UserId);
                            if (UserDataVendor != null)
                            {
                                _bLGeneral.SendPush(UserDataVendor.TokenVal, UserDataVendor.DeviceType, _PushListVendor);
                            }
                        }
                    }
                }
                Uow.Commit();
                return "Count Cancelled: " + CountCancel + " ---------- Count warning: " + CountWarning + "";
            }
            catch (Exception ex)
            {

                return ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }
        }
        #endregion
        #region WebHook
        public string UpdateShrouqOrder(int OrderStatusId, string statusname, string ShrouqStatusId, string order_id, string response, string FireBaseKey, string drName, string drPhone)
        {
            try
            {
                var Message = "";
                var OrderVendor = Uow.OrderVendor.GetAll(s => s.IntegrationOrderId == order_id, "Orders.Customers,Vendors,OrderStatus").FirstOrDefault();
                var Shiiping = Uow.ShippingCompany.GetAll(s => !s.IsDeleted && s.ShippingEnum == (int)ShippingCompanyEnum.Shourq).FirstOrDefault(); //شركة الشروق فيما بعد يجب تغيرها باينم
                if (OrderVendor != null && Shiiping != null)
                {
                    Message = "Insert Into Ship Company History Status: " + statusname + "";
                    OrderVendor.ShipCompanyHistory.Add(new ShipCompanyHistory()
                    {
                        ShippingCompanyID = Shiiping.ShippingCompanyID,
                        ShippingStatusId = OrderStatusId,
                        Response = response,
                        CreateDate = _bLGeneral.GetDateTimeNow(),
                        ResponseStatusId = ShrouqStatusId,
                        ShipCompanyHistoryGuid = Guid.NewGuid(),
                        ResponseStatusName = statusname,
                        DriverName = drName,
                        DriverPhone = drPhone,
                    });
                    if (OrderVendor.OrderStatusID != (int)OrderStatusEnum.Delivered && OrderVendor.OrderStatusID != (int)OrderStatusEnum.Cancel)
                    {
                        if (OrderStatusId != 0)
                        {
                            var StatusData = Uow.OrderStatus.GetAll(s => s.OrderStatusID == OrderStatusId).FirstOrDefault();
                            OrderVendor.OrderHistory.Add(new OrderHistory()
                            {
                                IsDeleted = false,
                                CreateDate = _bLGeneral.GetDateTimeNow(),
                                IsEnable = true,
                                Lat = 0,
                                Lng = 0,
                                OrderStatusID = OrderStatusId,
                                UserId = Shiiping.UserId,
                            });
                            OrderVendor.OrderStatusID = OrderStatusId;
                            OrderVendor.UpdateDate = _bLGeneral.GetDateTimeNow();
                            OrderVendor.UserUpdate = Shiiping.UserId;
                            Message += "<br /><br /><br />" +
                                "and Update Order Vendor with Status: " + StatusData.NameEN + "";
                            if (OrderStatusId == (int)OrderStatusEnum.Cancel)
                            {
                                #region Noti
                                OrderVendor.Notification.Add(new Notification()
                                {
                                    CreateDate = _bLGeneral.GetDateTimeNow(),
                                    CustomersID = OrderVendor.Orders.CustomersID,
                                    TitleAR = "تم الغاء طلبك رقم " + OrderVendor.TrackNo + "",
                                    TitleEN = "your order has been cancelled",
                                    DescAR = "تم الغاء الطلب رقم " + OrderVendor.TrackNo + " في تاريخ " + _bLGeneral.GetDateTimeNow() + "",
                                    DescEN = "order No " + OrderVendor.TrackNo + " has been cancelled on date " + _bLGeneral.GetDateTimeNow() + "",
                                    IsDeleted = false,
                                    IsEnable = true,
                                    IsSeen = false,
                                    NotificationGuid = Guid.NewGuid(),
                                    NotificationTypeID = (int)NotificationTypeEnum.Order,
                                    UserId = Shiiping.UserId,
                                    NotificationDate = _bLGeneral.GetDateTimeNow(),
                                    OrderVendorID = OrderVendor.OrderVendorID,
                                });
                                var _PushListVendor = new PushList()
                                {
                                    orderId = OrderVendor.OrderVendorID,
                                    lat = 0,
                                    lng = 0,
                                    trackNo = OrderVendor.OrderVendorID.ToString(),
                                    profit = "",
                                    cusotmerAddress = "",
                                    vendorAddress = "",
                                    vendorLogo = "",
                                    vendorName = "",
                                    body = "تم الغاء طلبك رقم " + OrderVendor.TrackNo + "",
                                    sound = "default", //For IOS
                                    title = "الغاء الطلب",
                                    content_available = "true", //For Android
                                    priority = "high", //For Android,
                                    serverKey = FireBaseKey,
                                    estimation = 20,
                                    pushType = (int)PushTypeEnum.Order
                                };
                                var UserDataVendor = _blTokens.GetMobileDataByUserID(OrderVendor.Orders.Customers.UserId);
                                _bLGeneral.SendPush(UserDataVendor.TokenVal, UserDataVendor.DeviceType, _PushListVendor);
                                #endregion
                                OrderVendor.InvoiceTypeId = (int)OrderInvoiceTypeEnum.Pending_Invoice;

                                OrderVendor.DriverCharge = 0;
                            }
                            else if (OrderStatusId == (int)OrderStatusEnum.Delivered)
                            {
                                _BlVendorBalance.InsertBalanceWithOutCommit((int)TypeOperationEnum.CR, OrderVendor.Total, (int)TransactionTypeEnum.Pay_Order, OrderVendor.VendorsID
                              , Shiiping.UserId, OrderVendor.OrderVendorID, _bLGeneral.GetDateTimeNow(), "", "");
                                OrderVendor.InvoiceTypeId = (int)OrderInvoiceTypeEnum.Pending_Invoice;
                                OrderVendor.DriverCharge = 0;
                                //Customer
                                OrderVendor.Notification.Add(new Notification()
                                {
                                    CreateDate = _bLGeneral.GetDateTimeNow(),
                                    CustomersID = OrderVendor.Orders.CustomersID,
                                    TitleAR = "تم تسليمكم الطلب بنجاح ، فضلا يرجى تقييم الخدمة لتحسين مستوى الخدمة.",
                                    TitleEN = "Your request has been successfully submitted, please rate the service to improve the level of service.",
                                    DescAR = "تم توصيل طلبك رقم " + OrderVendor.TrackNo + " من خلال " + OrderVendor.Vendors.StoreNameAr + " في تاريخ " + _bLGeneral.GetDateTimeNow() + "",
                                    DescEN = "order No " + OrderVendor.TrackNo + " has been delieverd from " + OrderVendor.Vendors.StoreNameEn + " on date " + _bLGeneral.GetDateTimeNow() + "",
                                    IsDeleted = false,
                                    IsEnable = true,
                                    IsSeen = false,
                                    NotificationGuid = Guid.NewGuid(),
                                    NotificationTypeID = (int)NotificationTypeEnum.Order,
                                    UserId = Shiiping.UserId,
                                    NotificationDate = _bLGeneral.GetDateTimeNow(),
                                });
                                var _PushListVendor = new PushList()
                                {
                                    orderId = OrderVendor.OrderVendorID,
                                    lat = 0,
                                    lng = 0,
                                    trackNo = OrderVendor.OrderVendorID.ToString(),
                                    profit = "",
                                    cusotmerAddress = "",
                                    vendorAddress = "",
                                    vendorLogo = "",
                                    vendorName = "",
                                    body = "تم تسليمكم الطلب بنجاح ، فضلا يرجى تقييم الخدمة لتحسين مستوى الخدمة.",
                                    sound = "default", //For IOS
                                    title = "توصيل الطلب",
                                    content_available = "true", //For Android
                                    priority = "high", //For Android,
                                    serverKey = FireBaseKey,
                                    estimation = 20,
                                    pushType = (int)PushTypeEnum.Order
                                };
                                var UserDataVendor = _blTokens.GetMobileDataByUserID(OrderVendor.Orders.Customers.UserId);
                                _bLGeneral.SendPush(UserDataVendor.TokenVal, UserDataVendor.DeviceType, _PushListVendor);
                            }
                            else
                            {
                                OrderVendor.Notification.Add(new Notification()
                                {
                                    CreateDate = _bLGeneral.GetDateTimeNow(),
                                    CustomersID = OrderVendor.Orders.CustomersID,
                                    TitleAR = "تم تعديل حالة طلبك رقم " + OrderVendor.TrackNo + " الى " + StatusData.NameAR + "",
                                    TitleEN = "your order has been updated to " + StatusData.NameEN + "",
                                    DescAR = "تم تعديل حالة الطلب رقم " + OrderVendor.TrackNo + " من خلال " + OrderVendor.Vendors.StoreNameAr + " في تاريخ " + _bLGeneral.GetDateTimeNow() + " لتكون الحالة " + StatusData.NameAR + "",
                                    DescEN = "order No " + OrderVendor.TrackNo + " has been updated from " + OrderVendor.Vendors.StoreNameEn + " on date " + _bLGeneral.GetDateTimeNow() + " to be " + StatusData.NameEN + "",
                                    IsDeleted = false,
                                    IsEnable = true,
                                    IsSeen = false,
                                    NotificationGuid = Guid.NewGuid(),
                                    NotificationTypeID = (int)NotificationTypeEnum.Order,
                                    UserId = Shiiping.UserId,
                                    NotificationDate = _bLGeneral.GetDateTimeNow(),
                                    OrderVendorID = OrderVendor.OrderVendorID,
                                });
                                var _PushListVendor = new PushList()
                                {
                                    orderId = OrderVendor.OrderVendorID,
                                    lat = 0,
                                    lng = 0,
                                    trackNo = OrderVendor.OrderVendorID.ToString(),
                                    profit = "",
                                    cusotmerAddress = "",
                                    vendorAddress = "",
                                    vendorLogo = "",
                                    vendorName = "",
                                    body = "تم تعديل حالة طلبك رقم " + OrderVendor.TrackNo + " الى " + StatusData.NameAR + "",
                                    sound = "default", //For IOS
                                    title = "تغيير حالة الطلب",
                                    content_available = "true", //For Android
                                    priority = "high", //For Android,
                                    serverKey = FireBaseKey,
                                    estimation = 20,
                                    pushType = (int)PushTypeEnum.Order
                                };
                                var UserDataVendor = _blTokens.GetMobileDataByUserID(OrderVendor.Orders.Customers.UserId);
                                _bLGeneral.SendPush(UserDataVendor.TokenVal, UserDataVendor.DeviceType, _PushListVendor);
                            }
                        }
                    }
                    else
                    {
                        Message = "Order ID: " + order_id + "<br /> Status in home made: " + OrderVendor.OrderStatus.NameEN + "<br /> and in shipping compnay need to update to: " + statusname + "";
                    }
                    Uow.OrderVendor.Update(OrderVendor);
                    Uow.Commit();
                }
                else
                {
                    Message = "Order Not Found";
                }
                return Message;
            }
            catch (Exception ex)
            {
                return ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }
        }
        public string CancelShrouqOrder(string order_id, string response, string FireBaseKey)
        {
            try
            {
                var Message = "";
                var OrderVendor = Uow.OrderVendor.GetAll(s => s.IntegrationOrderId == order_id, "Vendors,Orders.Customers").FirstOrDefault();
                var Shiiping = Uow.ShippingCompany.GetAll(s => !s.IsDeleted && s.ShippingEnum == (int)ShippingCompanyEnum.Shourq).FirstOrDefault(); //شركة الشروق فيما بعد يجب تغيرها باينم
                if (OrderVendor != null && Shiiping != null)
                {
                    Message = "Insert Into Ship Company History Status: Cancel";
                    OrderVendor.ShipCompanyHistory.Add(new ShipCompanyHistory()
                    {
                        ShippingCompanyID = Shiiping.ShippingCompanyID,
                        ShippingStatusId = 10,
                        Response = response,
                        CreateDate = _bLGeneral.GetDateTimeNow(),
                        ResponseStatusId = "10",
                        ShipCompanyHistoryGuid = Guid.NewGuid(),
                        ResponseStatusName = "Order cancelled",
                    });
                    OrderVendor.OrderHistory.Add(new OrderHistory()
                    {
                        IsDeleted = false,
                        CreateDate = _bLGeneral.GetDateTimeNow(),
                        IsEnable = true,
                        Lat = 0,
                        Lng = 0,
                        OrderStatusID = (int)OrderStatusEnum.Shipping,
                        UserId = Shiiping.UserId,
                    });
                    OrderVendor.OrderStatusID = (int)OrderStatusEnum.Shipping;
                    OrderVendor.ShippingCompanyID = null;
                    OrderVendor.ShippingCompanyPrice = 0;
                    OrderVendor.CaptainTypeId = (int)CaptainTypeEnum.Home_Made;
                    OrderVendor.UpdateDate = _bLGeneral.GetDateTimeNow();
                    OrderVendor.UserUpdate = Shiiping.UserId;
                    Message += "<br /><br /><br />" +
                        "and Update Order Vendor with Status: Shipping to Re Assign";
                    OrderVendor.Notification.Add(new Notification()
                    {
                        CreateDate = _bLGeneral.GetDateTimeNow(),
                        CustomersID = OrderVendor.Orders.CustomersID,
                        TitleAR = "تم إرجاع حالة طلبك رقم " + OrderVendor.TrackNo + " الى قيد الشحن و سيتم توزيعة مرة اخرى",
                        TitleEN = "your order has been updated to Shipping and we re assign it again",
                        DescAR = "تم إرجاع حالة طلبك رقم " + OrderVendor.TrackNo + " الى قيد الشحن و سيتم توزيعة مرة اخرى",
                        DescEN = "order No " + OrderVendor.TrackNo + " has been updated from " + OrderVendor.Vendors.StoreNameEn + " on date " + _bLGeneral.GetDateTimeNow() + " to be shiiping",
                        IsDeleted = false,
                        IsEnable = true,
                        IsSeen = false,
                        NotificationGuid = Guid.NewGuid(),
                        NotificationTypeID = (int)NotificationTypeEnum.Order,
                        UserId = Shiiping.UserId,
                        NotificationDate = _bLGeneral.GetDateTimeNow(),
                        OrderVendorID = OrderVendor.OrderVendorID,
                    });
                    var _PushListVendor = new PushList()
                    {
                        orderId = OrderVendor.OrderVendorID,
                        lat = 0,
                        lng = 0,
                        trackNo = OrderVendor.OrderVendorID.ToString(),
                        profit = "",
                        cusotmerAddress = "",
                        vendorAddress = "",
                        vendorLogo = "",
                        vendorName = "",
                        body = "تم إرجاع حالة طلبك رقم " + OrderVendor.TrackNo + " الى قيد الشحن و سيتم توزيعة مرة اخرى",
                        sound = "default", //For IOS
                        title = "تغيير حالة الطلب",
                        content_available = "true", //For Android
                        priority = "high", //For Android,
                        serverKey = FireBaseKey,
                        estimation = 20,
                        pushType = (int)PushTypeEnum.Order
                    };
                    var UserDataVendor = _blTokens.GetMobileDataByUserID(OrderVendor.Orders.Customers.UserId);
                    _bLGeneral.SendPush(UserDataVendor.TokenVal, UserDataVendor.DeviceType, _PushListVendor);
                    Uow.OrderVendor.Update(OrderVendor);
                    Uow.Commit();
                }
                return Message;
            }
            catch (Exception ex)
            {
                return ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }
        }
        public string CancelLavenderOrder(string order_id, string response, string FireBaseKey)
        {
            try
            {
                var Message = "";
                var OrderVendor = Uow.OrderVendor.GetAll(s => s.IntegrationOrderId == order_id, "Vendors,Orders.Customers").FirstOrDefault();
                var Shiiping = Uow.ShippingCompany.GetAll(s => !s.IsDeleted && s.ShippingEnum == (int)ShippingCompanyEnum.Lavender).FirstOrDefault(); //شركة الشروق فيما بعد يجب تغيرها باينم
                if (OrderVendor != null && Shiiping != null)
                {
                    OrderVendor.OrderHistory.Add(new OrderHistory()
                    {
                        IsDeleted = false,
                        CreateDate = _bLGeneral.GetDateTimeNow(),
                        IsEnable = true,
                        Lat = 0,
                        Lng = 0,
                        OrderStatusID = (int)OrderStatusEnum.Shipping,
                        UserId = Shiiping.UserId,
                    });
                    OrderVendor.OrderStatusID = (int)OrderStatusEnum.Shipping;
                    OrderVendor.ShippingCompanyID = null;
                    OrderVendor.ShippingCompanyPrice = 0;
                    OrderVendor.CaptainTypeId = (int)CaptainTypeEnum.Home_Made;
                    OrderVendor.UpdateDate = _bLGeneral.GetDateTimeNow();
                    OrderVendor.UserUpdate = Shiiping.UserId;
                    Message += "<br /><br /><br />" +
                        "and Update Order Vendor with Status: Shipping to Re Assign";
                    OrderVendor.Notification.Add(new Notification()
                    {
                        CreateDate = _bLGeneral.GetDateTimeNow(),
                        CustomersID = OrderVendor.Orders.CustomersID,
                        TitleAR = "تم إرجاع حالة طلبك رقم " + OrderVendor.TrackNo + " الى قيد الشحن و سيتم توزيعة مرة اخرى",
                        TitleEN = "your order has been updated to Shipping and we re assign it again",
                        DescAR = "تم إرجاع حالة طلبك رقم " + OrderVendor.TrackNo + " الى قيد الشحن و سيتم توزيعة مرة اخرى",
                        DescEN = "order No " + OrderVendor.TrackNo + " has been updated from " + OrderVendor.Vendors.StoreNameEn + " on date " + _bLGeneral.GetDateTimeNow() + " to be shiiping",
                        IsDeleted = false,
                        IsEnable = true,
                        IsSeen = false,
                        NotificationGuid = Guid.NewGuid(),
                        NotificationTypeID = (int)NotificationTypeEnum.Order,
                        UserId = Shiiping.UserId,
                        NotificationDate = _bLGeneral.GetDateTimeNow(),
                        OrderVendorID = OrderVendor.OrderVendorID,
                    });
                    var _PushListVendor = new PushList()
                    {
                        orderId = OrderVendor.OrderVendorID,
                        lat = 0,
                        lng = 0,
                        trackNo = OrderVendor.OrderVendorID.ToString(),
                        profit = "",
                        cusotmerAddress = "",
                        vendorAddress = "",
                        vendorLogo = "",
                        vendorName = "",
                        body = "تم إرجاع حالة طلبك رقم " + OrderVendor.TrackNo + " الى قيد الشحن و سيتم توزيعة مرة اخرى",
                        sound = "default", //For IOS
                        title = "تغيير حالة الطلب",
                        content_available = "true", //For Android
                        priority = "high", //For Android,
                        serverKey = FireBaseKey,
                        estimation = 20,
                        pushType = (int)PushTypeEnum.Order
                    };
                    var UserDataVendor = _blTokens.GetMobileDataByUserID(OrderVendor.Orders.Customers.UserId);
                    _bLGeneral.SendPush(UserDataVendor.TokenVal, UserDataVendor.DeviceType, _PushListVendor);
                    Uow.OrderVendor.Update(OrderVendor);
                    Uow.Commit();
                }
                return Message;
            }
            catch (Exception ex)
            {
                return ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }
        }
        public string UpdateLavenderOrder(int OrderStatusId, string statusname, int ShrouqStatusId, string order_id, string response, string FireBaseKey, string drName, string drPhone, string reason)
        {
            try
            {
                var Message = "";
                var OrderVendor = Uow.OrderVendor.GetAll(s => s.IntegrationOrderId == order_id, "Orders.Customers,Vendors,OrderStatus").FirstOrDefault();
                var Shiiping = Uow.ShippingCompany.GetAll(s => !s.IsDeleted && s.ShippingEnum == (int)ShippingCompanyEnum.Lavender).FirstOrDefault();
                if (OrderVendor != null && Shiiping != null)
                {
                    Message = "Insert Into Ship Company History Status: " + statusname + "";
                    OrderVendor.ShipCompanyHistory.Add(new ShipCompanyHistory()
                    {
                        ShippingCompanyID = Shiiping.ShippingCompanyID,
                        ShippingStatusId = OrderStatusId,
                        Response = response,
                        CreateDate = _bLGeneral.GetDateTimeNow(),
                        ResponseStatusId = ShrouqStatusId.ToString(),
                        ShipCompanyHistoryGuid = Guid.NewGuid(),
                        ResponseStatusName = statusname,
                        DriverName = drName,
                        DriverPhone = drPhone,
                        CancelReason = reason,
                    });
                    if (OrderVendor.OrderStatusID != (int)OrderStatusEnum.Delivered && OrderVendor.OrderStatusID != (int)OrderStatusEnum.Cancel)
                    {
                        if (OrderStatusId != 0)
                        {
                            var StatusData = Uow.OrderStatus.GetAll(s => s.OrderStatusID == OrderStatusId).FirstOrDefault();
                            OrderVendor.OrderHistory.Add(new OrderHistory()
                            {
                                IsDeleted = false,
                                CreateDate = _bLGeneral.GetDateTimeNow(),
                                IsEnable = true,
                                Lat = 0,
                                Lng = 0,
                                OrderStatusID = OrderStatusId,
                                UserId = Shiiping.UserId,
                            });
                            OrderVendor.OrderStatusID = OrderStatusId;
                            OrderVendor.UpdateDate = _bLGeneral.GetDateTimeNow();
                            OrderVendor.UserUpdate = Shiiping.UserId;
                            Message += "<br /><br /><br />" +
                                "and Update Order Vendor with Status: " + StatusData.NameEN + "";
                            if (OrderStatusId == (int)OrderStatusEnum.Cancel)
                            {
                                CancelLavenderOrder(order_id, response, FireBaseKey);
                            }
                            else if (OrderStatusId == (int)OrderStatusEnum.Delivered)
                            {
                                _BlVendorBalance.InsertBalanceWithOutCommit((int)TypeOperationEnum.CR, OrderVendor.Total, (int)TransactionTypeEnum.Pay_Order, OrderVendor.VendorsID
                              , Shiiping.UserId, OrderVendor.OrderVendorID, _bLGeneral.GetDateTimeNow(), "", "");
                                OrderVendor.InvoiceTypeId = (int)OrderInvoiceTypeEnum.Pending_Invoice;
                                OrderVendor.DriverCharge = 0;
                                //Customer
                                OrderVendor.Notification.Add(new Notification()
                                {
                                    CreateDate = _bLGeneral.GetDateTimeNow(),
                                    CustomersID = OrderVendor.Orders.CustomersID,
                                    TitleAR = "تم تسليمكم الطلب بنجاح ، فضلا يرجى تقييم الخدمة لتحسين مستوى الخدمة.",
                                    TitleEN = "Your request has been successfully submitted, please rate the service to improve the level of service.",
                                    DescAR = "تم توصيل طلبك رقم " + OrderVendor.TrackNo + " من خلال " + OrderVendor.Vendors.StoreNameAr + " في تاريخ " + _bLGeneral.GetDateTimeNow() + "",
                                    DescEN = "order No " + OrderVendor.TrackNo + " has been delieverd from " + OrderVendor.Vendors.StoreNameEn + " on date " + _bLGeneral.GetDateTimeNow() + "",
                                    IsDeleted = false,
                                    IsEnable = true,
                                    IsSeen = false,
                                    NotificationGuid = Guid.NewGuid(),
                                    NotificationTypeID = (int)NotificationTypeEnum.Order,
                                    UserId = Shiiping.UserId,
                                    NotificationDate = _bLGeneral.GetDateTimeNow(),
                                });
                                var _PushListVendor = new PushList()
                                {
                                    orderId = OrderVendor.OrderVendorID,
                                    lat = 0,
                                    lng = 0,
                                    trackNo = OrderVendor.OrderVendorID.ToString(),
                                    profit = "",
                                    cusotmerAddress = "",
                                    vendorAddress = "",
                                    vendorLogo = "",
                                    vendorName = "",
                                    body = "تم تسليمكم الطلب بنجاح ، فضلا يرجى تقييم الخدمة لتحسين مستوى الخدمة.",
                                    sound = "default", //For IOS
                                    title = "توصيل الطلب",
                                    content_available = "true", //For Android
                                    priority = "high", //For Android,
                                    serverKey = FireBaseKey,
                                    estimation = 20,
                                    pushType = (int)PushTypeEnum.Order
                                };
                                var UserDataVendor = _blTokens.GetMobileDataByUserID(OrderVendor.Orders.Customers.UserId);
                                _bLGeneral.SendPush(UserDataVendor.TokenVal, UserDataVendor.DeviceType, _PushListVendor);
                            }
                            else
                            {
                                OrderVendor.Notification.Add(new Notification()
                                {
                                    CreateDate = _bLGeneral.GetDateTimeNow(),
                                    CustomersID = OrderVendor.Orders.CustomersID,
                                    TitleAR = "تم تعديل حالة طلبك رقم " + OrderVendor.TrackNo + " الى " + StatusData.NameAR + "",
                                    TitleEN = "your order has been updated to " + StatusData.NameEN + "",
                                    DescAR = "تم تعديل حالة الطلب رقم " + OrderVendor.TrackNo + " من خلال " + OrderVendor.Vendors.StoreNameAr + " في تاريخ " + _bLGeneral.GetDateTimeNow() + " لتكون الحالة " + StatusData.NameAR + "",
                                    DescEN = "order No " + OrderVendor.TrackNo + " has been updated from " + OrderVendor.Vendors.StoreNameEn + " on date " + _bLGeneral.GetDateTimeNow() + " to be " + StatusData.NameEN + "",
                                    IsDeleted = false,
                                    IsEnable = true,
                                    IsSeen = false,
                                    NotificationGuid = Guid.NewGuid(),
                                    NotificationTypeID = (int)NotificationTypeEnum.Order,
                                    UserId = Shiiping.UserId,
                                    NotificationDate = _bLGeneral.GetDateTimeNow(),
                                    OrderVendorID = OrderVendor.OrderVendorID,
                                });
                                var _PushListVendor = new PushList()
                                {
                                    orderId = OrderVendor.OrderVendorID,
                                    lat = 0,
                                    lng = 0,
                                    trackNo = OrderVendor.OrderVendorID.ToString(),
                                    profit = "",
                                    cusotmerAddress = "",
                                    vendorAddress = "",
                                    vendorLogo = "",
                                    vendorName = "",
                                    body = "تم تعديل حالة طلبك رقم " + OrderVendor.TrackNo + " الى " + StatusData.NameAR + "",
                                    sound = "default", //For IOS
                                    title = "تغيير حالة الطلب",
                                    content_available = "true", //For Android
                                    priority = "high", //For Android,
                                    serverKey = FireBaseKey,
                                    estimation = 20,
                                    pushType = (int)PushTypeEnum.Order
                                };
                                var UserDataVendor = _blTokens.GetMobileDataByUserID(OrderVendor.Orders.Customers.UserId);
                                _bLGeneral.SendPush(UserDataVendor.TokenVal, UserDataVendor.DeviceType, _PushListVendor);
                            }
                        }
                        Uow.OrderVendor.Update(OrderVendor);
                        Uow.Commit();
                    }
                    else
                    {
                        Message = "OrderStatusID can't Update";
                    }
                }
                else
                {
                    Message = "Order Not Found";
                }
                return Message;
            }
            catch (Exception ex)
            {
                return ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }
        }
        public string CancelBarqOrder(string order_id, string response, string FireBaseKey)
        {
            try
            {
                var Message = "";
                var OrderVendor = Uow.OrderVendor.GetAll(s => s.IntegrationOrderId == order_id, "Vendors,Orders.Customers").FirstOrDefault();
                var Shiiping = Uow.ShippingCompany.GetAll(s => !s.IsDeleted && s.ShippingEnum == (int)ShippingCompanyEnum.Barq).FirstOrDefault();
                if (OrderVendor != null && Shiiping != null)
                {
                    OrderVendor.OrderHistory.Add(new OrderHistory()
                    {
                        IsDeleted = false,
                        CreateDate = _bLGeneral.GetDateTimeNow(),
                        IsEnable = true,
                        Lat = 0,
                        Lng = 0,
                        OrderStatusID = (int)OrderStatusEnum.Shipping,
                        UserId = Shiiping.UserId,
                    });
                    OrderVendor.OrderStatusID = (int)OrderStatusEnum.Shipping;
                    OrderVendor.ShippingCompanyID = null;
                    OrderVendor.ShippingCompanyPrice = 0;
                    OrderVendor.CaptainTypeId = (int)CaptainTypeEnum.Home_Made;
                    OrderVendor.UpdateDate = _bLGeneral.GetDateTimeNow();
                    OrderVendor.UserUpdate = Shiiping.UserId;
                    Message += "<br /><br /><br />" +
                        "and Update Order Vendor with Status: Shipping to Re Assign";
                    OrderVendor.Notification.Add(new Notification()
                    {
                        CreateDate = _bLGeneral.GetDateTimeNow(),
                        CustomersID = OrderVendor.Orders.CustomersID,
                        TitleAR = "تم إرجاع حالة طلبك رقم " + OrderVendor.TrackNo + " الى قيد الشحن و سيتم توزيعة مرة اخرى",
                        TitleEN = "your order has been updated to Shipping and we re assign it again",
                        DescAR = "تم إرجاع حالة طلبك رقم " + OrderVendor.TrackNo + " الى قيد الشحن و سيتم توزيعة مرة اخرى",
                        DescEN = "order No " + OrderVendor.TrackNo + " has been updated from " + OrderVendor.Vendors.StoreNameEn + " on date " + _bLGeneral.GetDateTimeNow() + " to be shiiping",
                        IsDeleted = false,
                        IsEnable = true,
                        IsSeen = false,
                        NotificationGuid = Guid.NewGuid(),
                        NotificationTypeID = (int)NotificationTypeEnum.Order,
                        UserId = Shiiping.UserId,
                        NotificationDate = _bLGeneral.GetDateTimeNow(),
                        OrderVendorID = OrderVendor.OrderVendorID,
                    });
                    var _PushListVendor = new PushList()
                    {
                        orderId = OrderVendor.OrderVendorID,
                        lat = 0,
                        lng = 0,
                        trackNo = OrderVendor.OrderVendorID.ToString(),
                        profit = "",
                        cusotmerAddress = "",
                        vendorAddress = "",
                        vendorLogo = "",
                        vendorName = "",
                        body = "تم إرجاع حالة طلبك رقم " + OrderVendor.TrackNo + " الى قيد الشحن و سيتم توزيعة مرة اخرى",
                        sound = "default", //For IOS
                        title = "تغيير حالة الطلب",
                        content_available = "true", //For Android
                        priority = "high", //For Android,
                        serverKey = FireBaseKey,
                        estimation = 20,
                        pushType = (int)PushTypeEnum.Order
                    };
                    var UserDataVendor = _blTokens.GetMobileDataByUserID(OrderVendor.Orders.Customers.UserId);
                    _bLGeneral.SendPush(UserDataVendor.TokenVal, UserDataVendor.DeviceType, _PushListVendor);
                    Uow.OrderVendor.Update(OrderVendor);
                    Uow.Commit();
                }
                return Message;
            }
            catch (Exception ex)
            {
                return ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }
        }
        public string UpdateBarqOrder(int OrderStatusId, string statusname, int ShrouqStatusId, string order_id, string response, string FireBaseKey, string drName, string drPhone)
        {
            try
            {
                var Message = "";
                var OrderVendor = Uow.OrderVendor.GetAll(s => s.IntegrationOrderId == order_id, "Orders.Customers,Vendors,OrderStatus").FirstOrDefault();
                var Shiiping = Uow.ShippingCompany.GetAll(s => !s.IsDeleted && s.ShippingEnum == (int)ShippingCompanyEnum.Barq).FirstOrDefault(); //شركة الشروق فيما بعد يجب تغيرها باينم
                if (OrderVendor != null && Shiiping != null)
                {
                    Message = "Insert Into Ship Company History Status: " + statusname + "";
                    OrderVendor.ShipCompanyHistory.Add(new ShipCompanyHistory()
                    {
                        ShippingCompanyID = Shiiping.ShippingCompanyID,
                        ShippingStatusId = OrderStatusId,
                        Response = response,
                        CreateDate = _bLGeneral.GetDateTimeNow(),
                        ResponseStatusId = ShrouqStatusId.ToString(),
                        ShipCompanyHistoryGuid = Guid.NewGuid(),
                        ResponseStatusName = statusname,
                        DriverName = drName,
                        DriverPhone = drPhone,
                    });
                    if (OrderVendor.OrderStatusID != (int)OrderStatusEnum.Delivered && OrderVendor.OrderStatusID != (int)OrderStatusEnum.Cancel)
                    {
                        if (OrderStatusId != 0)
                        {
                            var StatusData = Uow.OrderStatus.GetAll(s => s.OrderStatusID == OrderStatusId).FirstOrDefault();
                            OrderVendor.OrderHistory.Add(new OrderHistory()
                            {
                                IsDeleted = false,
                                CreateDate = _bLGeneral.GetDateTimeNow(),
                                IsEnable = true,
                                Lat = 0,
                                Lng = 0,
                                OrderStatusID = OrderStatusId,
                                UserId = Shiiping.UserId,
                            });
                            OrderVendor.OrderStatusID = OrderStatusId;
                            OrderVendor.UpdateDate = _bLGeneral.GetDateTimeNow();
                            OrderVendor.UserUpdate = Shiiping.UserId;
                            Message += "<br /><br /><br />" +
                                "and Update Order Vendor with Status: " + StatusData.NameEN + "";
                            if (OrderStatusId == (int)OrderStatusEnum.Cancel)
                            {
                                CancelBarqOrder(order_id, response, FireBaseKey);
                            }
                            else if (OrderStatusId == (int)OrderStatusEnum.Delivered)
                            {
                                _BlVendorBalance.InsertBalanceWithOutCommit((int)TypeOperationEnum.CR, OrderVendor.Total, (int)TransactionTypeEnum.Pay_Order, OrderVendor.VendorsID
                              , Shiiping.UserId, OrderVendor.OrderVendorID, _bLGeneral.GetDateTimeNow(), "", "");
                                OrderVendor.InvoiceTypeId = (int)OrderInvoiceTypeEnum.Pending_Invoice;
                                OrderVendor.DriverCharge = 0;
                                //Customer
                                OrderVendor.Notification.Add(new Notification()
                                {
                                    CreateDate = _bLGeneral.GetDateTimeNow(),
                                    CustomersID = OrderVendor.Orders.CustomersID,
                                    TitleAR = "تم تسليمكم الطلب بنجاح ، فضلا يرجى تقييم الخدمة لتحسين مستوى الخدمة.",
                                    TitleEN = "Your request has been successfully submitted, please rate the service to improve the level of service.",
                                    DescAR = "تم توصيل طلبك رقم " + OrderVendor.TrackNo + " من خلال " + OrderVendor.Vendors.StoreNameAr + " في تاريخ " + _bLGeneral.GetDateTimeNow() + "",
                                    DescEN = "order No " + OrderVendor.TrackNo + " has been delieverd from " + OrderVendor.Vendors.StoreNameEn + " on date " + _bLGeneral.GetDateTimeNow() + "",
                                    IsDeleted = false,
                                    IsEnable = true,
                                    IsSeen = false,
                                    NotificationGuid = Guid.NewGuid(),
                                    NotificationTypeID = (int)NotificationTypeEnum.Order,
                                    UserId = Shiiping.UserId,
                                    NotificationDate = _bLGeneral.GetDateTimeNow(),
                                });
                                var _PushListVendor = new PushList()
                                {
                                    orderId = OrderVendor.OrderVendorID,
                                    lat = 0,
                                    lng = 0,
                                    trackNo = OrderVendor.OrderVendorID.ToString(),
                                    profit = "",
                                    cusotmerAddress = "",
                                    vendorAddress = "",
                                    vendorLogo = "",
                                    vendorName = "",
                                    body = "تم تسليمكم الطلب بنجاح ، فضلا يرجى تقييم الخدمة لتحسين مستوى الخدمة.",
                                    sound = "default", //For IOS
                                    title = "توصيل الطلب",
                                    content_available = "true", //For Android
                                    priority = "high", //For Android,
                                    serverKey = FireBaseKey,
                                    estimation = 20,
                                    pushType = (int)PushTypeEnum.Order
                                };
                                var UserDataVendor = _blTokens.GetMobileDataByUserID(OrderVendor.Orders.Customers.UserId);
                                _bLGeneral.SendPush(UserDataVendor.TokenVal, UserDataVendor.DeviceType, _PushListVendor);
                            }
                            else
                            {
                                OrderVendor.Notification.Add(new Notification()
                                {
                                    CreateDate = _bLGeneral.GetDateTimeNow(),
                                    CustomersID = OrderVendor.Orders.CustomersID,
                                    TitleAR = "تم تعديل حالة طلبك رقم " + OrderVendor.TrackNo + " الى " + StatusData.NameAR + "",
                                    TitleEN = "your order has been updated to " + StatusData.NameEN + "",
                                    DescAR = "تم تعديل حالة الطلب رقم " + OrderVendor.TrackNo + " من خلال " + OrderVendor.Vendors.StoreNameAr + " في تاريخ " + _bLGeneral.GetDateTimeNow() + " لتكون الحالة " + StatusData.NameAR + "",
                                    DescEN = "order No " + OrderVendor.TrackNo + " has been updated from " + OrderVendor.Vendors.StoreNameEn + " on date " + _bLGeneral.GetDateTimeNow() + " to be " + StatusData.NameEN + "",
                                    IsDeleted = false,
                                    IsEnable = true,
                                    IsSeen = false,
                                    NotificationGuid = Guid.NewGuid(),
                                    NotificationTypeID = (int)NotificationTypeEnum.Order,
                                    UserId = Shiiping.UserId,
                                    NotificationDate = _bLGeneral.GetDateTimeNow(),
                                    OrderVendorID = OrderVendor.OrderVendorID,
                                });
                                var _PushListVendor = new PushList()
                                {
                                    orderId = OrderVendor.OrderVendorID,
                                    lat = 0,
                                    lng = 0,
                                    trackNo = OrderVendor.OrderVendorID.ToString(),
                                    profit = "",
                                    cusotmerAddress = "",
                                    vendorAddress = "",
                                    vendorLogo = "",
                                    vendorName = "",
                                    body = "تم تعديل حالة طلبك رقم " + OrderVendor.TrackNo + " الى " + StatusData.NameAR + "",
                                    sound = "default", //For IOS
                                    title = "تغيير حالة الطلب",
                                    content_available = "true", //For Android
                                    priority = "high", //For Android,
                                    serverKey = FireBaseKey,
                                    estimation = 20,
                                    pushType = (int)PushTypeEnum.Order
                                };
                                var UserDataVendor = _blTokens.GetMobileDataByUserID(OrderVendor.Orders.Customers.UserId);
                                _bLGeneral.SendPush(UserDataVendor.TokenVal, UserDataVendor.DeviceType, _PushListVendor);
                            }
                        }
                        Uow.OrderVendor.Update(OrderVendor);
                        Uow.Commit();
                    }
                    else
                    {
                        Message = "OrderStatusID can't Update";
                    }
                }
                else
                {
                    Message = "Order Not Found";
                }
                return Message;
            }
            catch (Exception ex)
            {
                return ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }
        }
        #endregion
        #region Dashboard
        public SummaryHomeCount GetSummaryHomeCountVendor(int VendorsID)
        {
            var data = new SummaryHomeCount();
            #region Orders
            data.CreateCount = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.VendorsID == VendorsID && x.OrderStatusID == (int)OrderStatusEnum.Create).Count();
            data.AcceptCount = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.VendorsID == VendorsID && x.OrderStatusID == (int)OrderStatusEnum.Accept).Count();
            data.BeingProcessedCount = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.VendorsID == VendorsID && x.OrderStatusID == (int)OrderStatusEnum.Being_Processed).Count();
            data.ShippingCount = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.VendorsID == VendorsID && x.OrderStatusID == (int)OrderStatusEnum.Shipping).Count();
            data.DeliveredCount = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.VendorsID == VendorsID && x.OrderStatusID == (int)OrderStatusEnum.Delivered).Count();
            data.BeingDeliveryCount = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.VendorsID == VendorsID && x.OrderStatusID == (int)OrderStatusEnum.Being_Delivery).Count();
            data.CancelCount = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.VendorsID == VendorsID && x.OrderStatusID == (int)OrderStatusEnum.Cancel).Count();
            data.AssignCount = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.VendorsID == VendorsID && x.OrderStatusID == (int)OrderStatusEnum.Assign).Count();
            data.OnWayStoreCount = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.VendorsID == VendorsID && x.OrderStatusID == (int)OrderStatusEnum.OnWay_Store).Count();
            #endregion
            #region Invoices
            data.OrderNotInvoiceCount = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.VendorsID == VendorsID && x.InvoiceTypeId == (int)OrderInvoiceTypeEnum.Pending_Invoice).Count();
            data.OrderNotInvoiceSum = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.VendorsID == VendorsID && x.InvoiceTypeId == (int)OrderInvoiceTypeEnum.Pending_Invoice).Sum(z => z.PerStore);

            data.OrderInvoiceCount = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.VendorsID == VendorsID && x.InvoiceTypeId == (int)OrderInvoiceTypeEnum.Invoiced).Count();
            data.OrderInvoiceSum = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.VendorsID == VendorsID && x.InvoiceTypeId == (int)OrderInvoiceTypeEnum.Invoiced).Sum(z => z.PerStore);

            data.OrderCashTransferedCount = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.VendorsID == VendorsID && x.InvoiceTypeId == (int)OrderInvoiceTypeEnum.Cash_Transfered).Count();
            data.OrderCashTransferedSum = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.VendorsID == VendorsID && x.InvoiceTypeId == (int)OrderInvoiceTypeEnum.Cash_Transfered).Sum(z => z.PerStore);
            #endregion
            return data;
        }
        public void GetAllSummaryChartVendor(int VendorsID, string lang, out int[] ListOrdersCount, out double[] OrdersCount)
        {
            ListOrdersCount = new int[9];
            OrdersCount = new double[3];

            ListOrdersCount[0] = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.VendorsID == VendorsID && x.OrderStatusID == (int)OrderStatusEnum.Create).Count();
            ListOrdersCount[1] = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.VendorsID == VendorsID && x.OrderStatusID == (int)OrderStatusEnum.Accept).Count();
            ListOrdersCount[2] = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.VendorsID == VendorsID && x.OrderStatusID == (int)OrderStatusEnum.Being_Processed).Count();
            ListOrdersCount[3] = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.VendorsID == VendorsID && x.OrderStatusID == (int)OrderStatusEnum.Shipping).Count();
            ListOrdersCount[4] = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.VendorsID == VendorsID && x.OrderStatusID == (int)OrderStatusEnum.Assign).Count();
            ListOrdersCount[5] = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.VendorsID == VendorsID && x.OrderStatusID == (int)OrderStatusEnum.OnWay_Store).Count();
            ListOrdersCount[6] = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.VendorsID == VendorsID && x.OrderStatusID == (int)OrderStatusEnum.Being_Delivery).Count();
            ListOrdersCount[7] = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.VendorsID == VendorsID && x.OrderStatusID == (int)OrderStatusEnum.Delivered).Count();
            ListOrdersCount[8] = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.VendorsID == VendorsID && x.OrderStatusID == (int)OrderStatusEnum.Cancel).Count();

            var DeliveredOrderCount = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.OrderStatusID == (int)OrderStatusEnum.Delivered).Count();
            var CancelOrderCount = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.OrderStatusID == (int)OrderStatusEnum.Cancel).Count();
            var NewOrderCount = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.OrderStatusID != (int)OrderStatusEnum.Delivered && x.OrderStatusID != (int)OrderStatusEnum.Cancel).Count();
            if (DeliveredOrderCount + CancelOrderCount + NewOrderCount > 0)
            {
                OrdersCount[0] = ((double)DeliveredOrderCount / ((double)DeliveredOrderCount + (double)CancelOrderCount + (double)NewOrderCount)) * 100;
                OrdersCount[1] = ((double)CancelOrderCount / ((double)DeliveredOrderCount + (double)CancelOrderCount + (double)NewOrderCount)) * 100;
                OrdersCount[2] = ((double)NewOrderCount / ((double)DeliveredOrderCount + (double)CancelOrderCount + (double)NewOrderCount)) * 100;
            }
        }
        #endregion
        #region Increase Order
        public VendorOrdersViewModelApi VendorIncreaseOrders(string lang, int page, string vendorpath, int VendorsID, int type, string custoemrPath)
        {
            var Data = Uow.OrderVendor.GetAll(s => s.IsDeleted && s.IsEnable && s.VendorsID == VendorsID &&
      (s.OrderStatusID == (int)OrderStatusEnum.Pending) && s.OrderItems.Any(x => !x.IsDeleted && x.IsIncreaseItem) && s.ApprovalQuantity == type,
      "Vendors,Orders.CustomerLocation.Block,OrderStatus").OrderByDescending(s => s.CreateDate).Select(m => new ListProductVendor()
      {
          customerImage = !string.IsNullOrWhiteSpace(m.Orders.Customers.ProfilePic) ? custoemrPath + m.Orders.Customers.ProfilePic : "",
          orderId = m.OrderVendorID,
          customerName = m.Orders.Customers.FirstName + " " + m.Orders.Customers.SeconedName,
          customerAddress = m.Orders.CustomerLocation.Address,
          blockName = lang == "ar" ? m.Orders.CustomerLocation.Block.NameAR : m.Orders.CustomerLocation.Block.NameEN,
          paymentType = lang == "ar" ? (m.Orders.PaymentTypeId == (int)PaymentTypeEnum.COD ? "نقد عند الإستلام" : m.Orders.PaymentTypeId == (int)PaymentTypeEnum.Payment_Card ? "بطاقة دفع" :
                m.Orders.PaymentTypeId == (int)PaymentTypeEnum.Bank_Transfer ? "تحويل بنكي" : "المحفظة") :
                (m.Orders.PaymentTypeId == (int)PaymentTypeEnum.COD ? "Cash on Receive" : m.Orders.PaymentTypeId == (int)PaymentTypeEnum.Payment_Card ? "Payment Card" :
                m.Orders.PaymentTypeId == (int)PaymentTypeEnum.Bank_Transfer ? "Bank Transfer" : "Wallet"),
          date = m.CreateDate.ToString("dd/MM/yyyy"),
          time = m.CreateDate.ToString("hh:mm tt"),
          trackNo = m.TrackNo,
          price = lang == "ar" ? m.Price + " ريـال" : m.Price + " SAR",
          paymentStatus = lang == "ar" ? (m.Orders.PaymentTypeId == (int)PaymentTypeEnum.COD ? "لم يتم الدفع" : "تم الدفع") : m.Orders.PaymentTypeId == (int)PaymentTypeEnum.COD ? "Un Paid" : "Paid",
          colorHex = m.Orders.PaymentTypeId == (int)PaymentTypeEnum.COD ? "#FFF9D9" : "#E5F9ED",
          colorText = m.Orders.PaymentTypeId == (int)PaymentTypeEnum.COD ? "#FFBB00" : "#00BE4C",
          createDate = m.CreateDate,
          canceledBy = m.OrderStatusID == (int)OrderStatusEnum.Cancel ?
                m.OrderHistory.FirstOrDefault(s => s.OrderStatusID == (int)OrderStatusEnum.Cancel) != null ?
                (m.OrderHistory.FirstOrDefault(s => s.OrderStatusID == (int)OrderStatusEnum.Cancel).User.UserType == (int)UserTypeEnum.Customer ?
                (lang == "ar" ? "تم الإلغاء بواسطة المستخدم" : "Canceled By Customer") : m.OrderHistory.FirstOrDefault(s => s.OrderStatusID == (int)OrderStatusEnum.Cancel).User.UserType == (int)UserTypeEnum.Vendor ?
                (lang == "ar" ? "تم الإلغاء بواسطة المتجر" : "Canceled By Store") : (lang == "ar" ? "تم الإلغاء بواسطة الأدمن" : "Canceled By Admin"))
                : (lang == "ar" ? "تم الإلغاء بواسطة المتجر" : "Canceled By Store") : "",
          isallawed =
                m.CaptainTypeId == (int)CaptainTypeEnum.Home_Made ?
                (
                m.OrderStatusID == (int)OrderStatusEnum.Create ||
                m.OrderStatusID == (int)OrderStatusEnum.Accept ||
                m.OrderStatusID == (int)OrderStatusEnum.Being_Processed
                ? true : false
                )

                : true,
          orderType = lang == "ar" ? (m.OrderTypeId == (int)OrderTypeEnum.Now ? "طلب فورى" : "طلب مجدول - " + (m.ScheduleDate != null ? m.ScheduleDate.Value.ToString("dd/MM/yyyy") + " " + m.ScheduleTime.Value.ToString("hh:mm tt") : "")) : (m.OrderTypeId == (int)OrderTypeEnum.Now ? "Now Order" : "Schedule Order - " + (m.ScheduleDate != null ? m.ScheduleDate.Value.ToString("dd/MM/yyyy") + " " + m.ScheduleTime.Value.ToString("hh:mm tt") : "")),
          orderVendorID = m.OrderVendorID,

          approvalQuantity = m.ApprovalQuantity,
          approvalQuantityString = lang == "ar" ? (m.ApprovalQuantity == (int)ApprovalQuantityEnum.Reject ? "تم الرفض" : m.ApprovalQuantity == (int)ApprovalQuantityEnum.Approve ? "تم الموافقة" : m.OrderStatus.NameAR)
              : (m.ApprovalQuantity == (int)ApprovalQuantityEnum.Reject ? "Reject" : m.ApprovalQuantity == (int)ApprovalQuantityEnum.Approve ? "Approve" : m.OrderStatus.NameEN),

      }).ToList();
            Data = Data.Skip(Convert.ToInt32(page - 1) * 10).ToList();
            var DataTake = Data.Take(((Convert.ToInt32(page) * 10)) - (Convert.ToInt32(page - 1) * 10)).ToList();
            bool NextPage = false;
            if (Data.Count() > 10)
            {
                NextPage = true;
            }
            DataTake = DataTake.OrderByDescending(s => s.createDate).ToList();
            var model = new VendorOrdersViewModelApi();
            model.listOrders = DataTake;
            model.isNextPage = NextPage;
            return model;
        }
        #endregion
    }
}
