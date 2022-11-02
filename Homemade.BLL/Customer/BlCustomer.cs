using Homemade.BLL.General;
using Homemade.BLL.Setting;
using Homemade.BLL.Vendor;
using Homemade.BLL.ViewModel.Customer;
using Homemade.BLL.ViewModel.Driver;
using Homemade.BLL.ViewModel.Notifications;
using Homemade.BLL.ViewModel.Order;
using Homemade.BLL.ViewModel.Site;
using Homemade.BLL.ViewModel.Vendor;
using Homemade.DAL.Contract;
using Homemade.Model.BankTransaction;
using Homemade.Model.Customer;
using Homemade.Model.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using Homemade.BLL.ViewModel.Employees;
using Microsoft.Extensions.Configuration;


namespace Homemade.BLL.Customer
{
    public class BlCustomer
    {
        #region Declartion
        private readonly IUOW Uow;
        private readonly BLGeneral _bLGeneral;
        private readonly BlTokens _blTokens;
        private readonly BlCustomerBalance _BlCustomerBalance;
        private readonly BlConfiguration _blConfiguration; 
        private readonly BlVendorBalance _BlVendorBalance;
        private readonly IConfiguration _configuration;

        public BlCustomer(BlTokens BlTokens, IUOW _uow, BLGeneral bLGeneral, BlCustomerBalance BlCustomerBalance, BlVendorBalance BlVendorBalance, BlConfiguration blConfiguration, IConfiguration configuration)
        {
            this.Uow = _uow;
            _bLGeneral = bLGeneral;
            _BlCustomerBalance = BlCustomerBalance;
            _BlVendorBalance = BlVendorBalance;
            _blTokens = BlTokens;
            _blConfiguration = blConfiguration;
            _configuration = configuration;
        }
        #endregion
        #region Balance
        public decimal GetLastBlance(int CustomerID)
        {
            var Balance = Uow.CustomerBalance.GetAll(e => e.CustomersID == CustomerID);
            if (Balance.Count() > 0)
            {
                var last =
                    Math.Round(Balance.Where(s => s.TypeOperationID == (int)TypeOperationEnum.CR).Sum(e => e.Amount), 2)
                    -
                    Math.Round(Balance.Where(s => s.TypeOperationID == (int)TypeOperationEnum.DR).Sum(e => e.Amount), 2);
                return Math.Round(last, 2);
            }
            else
            {
                return 0;
            }
        }
        public bool AddCustomerBalance(int id, decimal Amount, string Discripe, int userId)
        {
            var data = GetCustomerById(id);
            if (data != null)
            {
                var before = GetLastBlance(id);

                CustomerBalance customerBalance = new CustomerBalance()
                {
                    CreateDate = _bLGeneral.GetDateTimeNow(),
                    UserId = userId,
                    CustomersID = id,
                    IsDeleted = false,
                    IsEnable = true,
                    CustomerBlanceGuid = Guid.NewGuid(),
                    Amount = Amount,
                    Before = before,
                    After = before + Amount,
                    TransactionTypeID = (int)TransactionTypeEnum.Deposit,
                    TypeOperationID = (int)TypeOperationEnum.CR,
                    DateOperation = _bLGeneral.GetDateTimeNow(),
                    Discripe = Discripe,
                };

                customerBalance = Uow.CustomerBalance.Insert(customerBalance);
                Uow.Commit();
                return true;
            }
            return false;
        }

        #endregion
        #region Notification
        public int GetUserNotificationNotSeenCount(int UserId) => Uow.Notification.GetAll(x => !x.IsDeleted && x.Customers.UserId == UserId && !x.IsSeen).Count();
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
            var notifications = Uow.Notification.GetAll(x => !x.IsDeleted && x.Customers.UserId == userId && !x.IsSeen);
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
        public bool UpdateAllNotificationSeen(int userId)
        {
            var notifications = GetAllNotificationNotSeen(userId);
            if (notifications.Count() > 0)
            {
                foreach (var noti in notifications)
                {
                    var notification = GetNotificationById(noti.NotificationID);
                    notification.UpdateDate = _bLGeneral.GetDateTimeNow();
                    notification.UserUpdate = userId;
                    notification.IsSeen = true;
                    notification = Uow.Notification.Update(notification);
                }
                Uow.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool UpdateAllNotificationSeenByVendors(int userId)
        {
            var notifications = GetAllNotificationNotSeenByVendors(userId);
            if (notifications.Count() > 0)
            {
                foreach (var noti in notifications)
                {
                    var notification = GetNotificationById(noti.NotificationID);
                    notification.UpdateDate = _bLGeneral.GetDateTimeNow();
                    notification.UserUpdate = userId;
                    notification.IsSeen = true;
                    notification = Uow.Notification.Update(notification);
                }
                Uow.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }
        public IQueryable<Notification> GetAllNotificationNotSeen(int UserId) => Uow.Notification.GetAll(x => !x.IsDeleted && !x.IsSeen && x.Customers.UserId == UserId);
        public IQueryable<Notification> GetAllNotificationNotSeenByVendors(int UserId) => Uow.Notification.GetAll(x => !x.IsDeleted && !x.IsSeen && x.Vendors.UserId == UserId);
        public ResponsListNoti GetNotificationListApiViewModelByDriver(int customerId, string accLanguage, int page)
        {
            var listret = new List<NotificationListApiViewModel>();
            var data = Uow.Notification.GetAll(x => !x.IsDeleted && x.CustomersID == customerId, "OrderVendor"
                ).OrderByDescending(e => e.CreateDate).GroupBy(s => s.CreateDate.Date).Select(m => new NotificationListApiViewModel()
            {
                date = accLanguage == "ar" ? (m.Key.Date == DateTime.Now.Date ? "اليوم" : m.Key.Date.AddDays(1) == DateTime.Now.Date ? "امس" : m.Key.ToString("dd-MM-yyyy")) :
                m.Key.Date == DateTime.Now.Date ? "Today" : m.Key.Date.AddDays(1) == DateTime.Now.Date ? "yesterday" : m.Key.ToString("dd-MM-yyyy"),
                notificationCount = m.Count(),
                datekey = m.Key
            }).ToList().OrderByDescending(e => e.datekey);
            foreach (var item in data)
            {
                item.dayNotifications = Uow.Notification.GetAll(x => !x.IsDeleted && x.CreateDate.Date == item.datekey.Date && x.CustomersID == customerId
                ).OrderByDescending(e => e.CreateDate).Select(m => new NotificationDetailsApiViewModel()
                                                                                {
                                                                                    notificationID = m.NotificationID,
                                                                                    isSeen = m.IsSeen,
                                                                                    time = m.NotificationDate.ToString("HH:mm tt"),
                                                                                    desc = accLanguage == "ar" ? m.DescAR ?? string.Empty : m.DescEN ?? string.Empty,
                                                                                    title = accLanguage == "ar" ? m.TitleAR ?? string.Empty : m.TitleEN ?? string.Empty,
                                                                                    orderId = m.OrderVendorID != null ? m.OrderVendor.OrderStatusID == (int)OrderStatusEnum.Pending ? m.OrderVendor.OrdersID : m.OrderVendorID : 0,
                                                                                    rateId = m.OrderRateID != null ? m.OrderRateID : 0,
                                                                                    questionId = m.ProdQuestionID != null ? m.ProdQuestionID : 0,
                                                                                    notificationType = m.OrderVendorID != null ?
                     (m.OrderVendor.OrderStatusID == (int)OrderStatusEnum.Pending && m.OrderVendor.ApprovalQuantity == (int)ApprovalQuantityEnum.Reject) ? (int)NotificationTypeEnum.Pending_Reject
                    : (m.OrderVendor.OrderStatusID == (int)OrderStatusEnum.Pending && m.OrderVendor.ApprovalQuantity == (int)ApprovalQuantityEnum.Approve) ? (int)NotificationTypeEnum.Pending_Approve
                    : m.NotificationTypeID : m.NotificationTypeID,
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
        public List<SiteNotificationViewModel> GetAllNotification(string accLanguage, int UserId)
        {
            return Uow.Notification.GetAll(x => !x.IsDeleted && x.Customers.UserId == UserId, "OrderVendor.Orders").ToList().OrderByDescending(e => e.CreateDate).GroupBy(s => s.CreateDate.Date)
                .Select(m => new SiteNotificationViewModel()
                {
                    NotificationDate = accLanguage == "ar" ? (m.Key.Date == DateTime.Now.Date ? "اليوم" : m.Key.Date.AddDays(1) == DateTime.Now.Date ? "امس" : m.Key.ToString("dd-MM-yyyy")) :
               m.Key.Date == DateTime.Now.Date ? "Today" : m.Key.Date.AddDays(1) == DateTime.Now.Date ? "yesterday" : m.Key.ToString("dd-MM-yyyy"),
                    List = m.Select(m => new SiteListNotificationViewModel()
                    {
                        NotificationID = m.NotificationID,
                        IsSeen = m.IsSeen,
                        NotificationDate = m.NotificationDate == DateTime.Now.Date ? (accLanguage == "ar" ? "اليوم" : "Today") : m.NotificationDate.ToString("dd-MM-yyyy"),
                        NotificationTime = m.NotificationDate.ToString("HH:mm tt"),
                        NotificationDesc = accLanguage == "ar" ? m.DescAR ?? string.Empty : m.DescEN ?? string.Empty,
                        NotificationTitle = accLanguage == "ar" ? m.TitleAR ?? string.Empty : m.TitleEN ?? string.Empty,
                        NotificationTypeID = m.NotificationTypeID,
                        RedirectGuid = m.NotificationTypeID == (int)NotificationTypeEnum.Order ? (m.OrderVendor != null ? m.OrderVendor.Orders.OrdersGuid : null) : null,
                        OrderStatusID = m.OrderVendor != null ? m.OrderVendor.OrderStatusID : null,
                    }).ToList()

                }).ToList();
        }
        public List<NotificationViewModel> GetAllNotificationByVendor(string accLanguage, int UserId)
        {
            return Uow.Notification.GetAll(x => !x.IsDeleted && x.Vendors.UserId == UserId && !x.IsSeen).OrderByDescending(e => e.CreateDate)
                .Select(m => new NotificationViewModel()
                {
                    NotificationID = m.NotificationID,
                    IsSeen = m.IsSeen,
                    NotificationDate = m.NotificationDate == DateTime.Now.Date ? (accLanguage == "ar" ? "اليوم" : "Today") : m.NotificationDate.ToString("dd-MM-yyyy"),
                    NotificationTime = m.NotificationDate.ToString("HH:mm tt"),
                    NotificationDesc = accLanguage == "ar" ? m.DescAR ?? string.Empty : m.DescEN ?? string.Empty,
                    NotificationTitle = accLanguage == "ar" ? m.TitleAR ?? string.Empty : m.TitleEN ?? string.Empty,
                    NotificationTypeID = m.NotificationTypeID,
                    OrderStatusID = m.OrderVendor.OrderStatusID,
                    RedirectGuid = m.NotificationTypeID == (int)NotificationTypeEnum.Order ? m.OrderVendor.OrderVendorGuid : null,

                }).ToList();
        }
        #endregion
        #region Helper
        public SummaryHomeCount GetSummaryHomeCountByCustomer(int CustomersID)
        {
            var data = new SummaryHomeCount();
            #region Orders
            data.CreateCount = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.OrderStatusID == (int)OrderStatusEnum.Create && x.Orders.CustomersID == CustomersID).Count();
            data.AcceptCount = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.OrderStatusID == (int)OrderStatusEnum.Accept && x.Orders.CustomersID == CustomersID).Count();
            data.BeingProcessedCount = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.OrderStatusID == (int)OrderStatusEnum.Being_Processed && x.Orders.CustomersID == CustomersID).Count();
            data.ShippingCount = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.OrderStatusID == (int)OrderStatusEnum.Shipping && x.Orders.CustomersID == CustomersID).Count();
            data.DeliveredCount = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.OrderStatusID == (int)OrderStatusEnum.Delivered && x.Orders.CustomersID == CustomersID).Count();
            data.BeingDeliveryCount = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.OrderStatusID == (int)OrderStatusEnum.Being_Delivery && x.Orders.CustomersID == CustomersID).Count();
            data.CancelCount = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.OrderStatusID == (int)OrderStatusEnum.Cancel && x.CancelTypeID == (int)CancelTypeEnum.Default && x.Orders.CustomersID == CustomersID).Count();
            data.PendingCancelCount = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.OrderStatusID == (int)OrderStatusEnum.Cancel && x.CancelTypeID == (int)CancelTypeEnum.Auto && x.Orders.CustomersID == CustomersID).Count();
            data.AssignCount = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.OrderStatusID == (int)OrderStatusEnum.Assign && x.Orders.CustomersID == CustomersID).Count();
            data.OnWayStoreCount = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.OrderStatusID == (int)OrderStatusEnum.OnWay_Store && x.Orders.CustomersID == CustomersID).Count();
            #endregion
            return data;
        }
        public Customers GetCustomerByUser(int userId)
        {
            return Uow.Customers.GetAll(e => e.UserId == userId).FirstOrDefault();
        }
        public Customers GetCustomerByUser(int userId, string include)
        {
            return Uow.Customers.GetAll(e => e.UserId == userId, include).FirstOrDefault();
        }
        public Customers GetCustomerById(int CustomersID)
        {
            return Uow.Customers.GetAll(e => e.CustomersID == CustomersID, "User").FirstOrDefault();
        }
        public Orders GetOrdersById(int OrdersID)
        {
            return Uow.Orders.GetAll(e => e.OrdersID == OrdersID).FirstOrDefault();
        }
        public bool IsExistMobile(string Mobile)
        {
            return Uow.Customers.GetAll(s => s.MobileNo == Mobile && !s.IsDeleted).Any();
        }
        public bool IsExistEmail(string Email)
        {
            return Uow.Customers.GetAll(s => s.Email == Email && !s.IsDeleted).Any();
        }
        public Customers GetCustomersByMobileNo(string MobileNo, string include)
        {
            return Uow.Customers.GetAll(x => x.MobileNo == MobileNo && !x.IsDeleted, include).FirstOrDefault();
        }
        public CustomerLocation GetCustomerLocationByID(int CustomerLocationID)
        {
            return Uow.CustomerLocation.GetAll(e => e.CustomerLocationID == CustomerLocationID).FirstOrDefault();
        }
        public CustomerLocation GetCustomerLocationByCustomersID(int CustomersID)
        {
            return Uow.CustomerLocation.GetAll(e => !e.IsDeleted && e.CustomersID == CustomersID).OrderByDescending(x => x.CreateDate).FirstOrDefault();
        }
        #endregion
        #region Action
        public bool AddCustomer(RegisterCustomerViewModel model, int UserId, string logofileName)
        {
            Customers customers = new Customers()
            {
                CreateDate = _bLGeneral.GetDateTimeNow(),
                CustomersGuid = Guid.NewGuid(),
                Email = model.email,
                MobileNo = model.mobileNo,
                FirstName = model.firstName,
                Gender = model.gender,
                IsDeleted = false,
                SeconedName = "",
                UserId = UserId,
                CityID = model.cityID,
                NationalityID = model.nationaltyID,
                ProfilePic = logofileName,
                IsEnable = true,
                Address = string.Empty,
            };
            Uow.Customers.Insert(customers);
            Uow.Commit();
            return true;
        }
        public bool AddCustomer(NewUserViewModel model, int UserId)
        {
            Customers customers = new Customers()
            {
                CreateDate = _bLGeneral.GetDateTimeNow(),
                CustomersGuid = Guid.NewGuid(),
                Email = model.Email,
                MobileNo = model.MobileNo,
                FirstName = model.UserName,
                SeconedName = "",
                Gender = model.Gender,
                IsDeleted = false,
                UserId = UserId,
                CityID = model.CityID,
                NationalityID = model.NationalityID,
                IsEnable = true,
                ProfilePic = "0971b9ed-0261-45d2-af5c-d103db5765ce.png"
            };
            Uow.Customers.Insert(customers);
            Uow.Commit();
            return true;
        }
        public bool InsertCustomerLocation(SiteCustomerLocationViewModel model, int UserId)
        {
            CustomerLocation customerLocation = new CustomerLocation()
            {
                CreateDate = _bLGeneral.GetDateTimeNow(),
                CustomerLocationGuid = Guid.NewGuid(),
                IsDeleted = false,
                UserId = UserId,
                AddressTypesID = model.AddressTypesID,
                BuildingNumber = model.BuildingNumber,
                CustomersID = model.CustomersID,
                IsEnable = true,
                IsLocation = true,
                BlockID = model.BlockID,
                Address = model.Address,
                MobileNo = model.MobileNo,
                Name = model.Name,
                StreetNo = model.StreetNo,
                UniqueSign = model.UniqueSign,
                Lat = model.Lat,
                Lng = model.Lng,
            };
            Uow.CustomerLocation.Insert(customerLocation);
            Uow.Commit();
            return true;
        }
        public bool UpdateCustomerLocation(SiteCustomerLocationViewModel model, int UserId)
        {
            var customerLocation = GetCustomerLocationByID(model.CustomerLocationID);
            if (customerLocation != null)
            {
                customerLocation.UpdateDate = _bLGeneral.GetDateTimeNow();
                customerLocation.UserUpdate = UserId;
                customerLocation.AddressTypesID = model.AddressTypesID;
                customerLocation.BuildingNumber = model.BuildingNumber;
                customerLocation.CustomersID = model.CustomersID;
                customerLocation.BlockID = model.BlockID;
                customerLocation.Address = model.Address;
                customerLocation.MobileNo = model.MobileNo;
                customerLocation.Name = model.Name;
                customerLocation.StreetNo = model.StreetNo;
                customerLocation.UniqueSign = model.UniqueSign;
                customerLocation.Lat = model.Lat;
                customerLocation.Lng = model.Lng;
                Uow.CustomerLocation.Update(customerLocation);
                Uow.Commit();
                return true;
            }
            else
            {
                return false;
            }

        }
        public bool DeleteCustomerLocation(int id, int userId)
        {
            var customerLocation = GetCustomerLocationByID(id);
            if (customerLocation != null)
            {
                customerLocation.IsDeleted = true;
                customerLocation.UserDelete = userId;
                customerLocation.DeleteDate = _bLGeneral.GetDateTimeNow();
                Uow.CustomerLocation.Update(customerLocation);
                Uow.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
        #region SMS
        public bool IsSmsSend()
        {
            return Uow.Configuration.GetAll().FirstOrDefault().IsSmsSend;
        }
        public decimal LastSmsBalance()
        {
            return Uow.Configuration.GetAll().FirstOrDefault().LastSmsBalance;
        }
        public bool UpdateLastSmsBalance(decimal Balance)
        {
            var Data = Uow.Configuration.GetAll().FirstOrDefault();
            Data.LastSmsBalance = Balance;
            Uow.Configuration.Update(Data);
            Uow.Commit();
            return true;
        }
        #endregion
        #region Customer Mobile
        public CustomerOrdersViewModelApi CustomerOrders(string lang, int page, string vendorpath, int CustoemrID, int type, string customerPath, string productPath)
        {
            var Data = Uow.OrderVendor.GetAll(s => !s.IsDeleted && s.IsEnable && s.Orders.CustomersID == CustoemrID &&
            (type == 1 ? (s.OrderStatusID != (int)OrderStatusEnum.Delivered && s.OrderStatusID != (int)OrderStatusEnum.Cancel) : (s.OrderStatusID == (int)OrderStatusEnum.Delivered || s.OrderStatusID == (int)OrderStatusEnum.Cancel)),
            "Vendors,Orders.CustomerLocation.Block,OrderStatus").OrderByDescending(s => s.CreateDate).Select(m => new ListProduct()
            {
                customerName = m.Orders.Customers.FirstName + " " + m.Orders.Customers.SeconedName,
                customerLogo = !string.IsNullOrWhiteSpace(m.Orders.Customers.ProfilePic) ? customerPath + m.Orders.Customers.ProfilePic : "",
                customerLat = m.Orders.CustomerLocation.Lat,
                customerLng = m.Orders.CustomerLocation.Lng,
                vendorLat = m.Vendors.Lat,
                vendorLng = m.Vendors.Lng,
                vendorImage = !string.IsNullOrWhiteSpace(m.Vendors.Logo) ? vendorpath + m.Vendors.Logo : "",
                vendorId = m.VendorsID,
                trackNo = m.TrackNo,
                orderId = m.OrderVendorID,
                customerMobile = "0" + m.Orders.Customers.MobileNo,
                customerWhats = "+966" + m.Orders.Customers.MobileNo,
                vendorMobile = "0" + m.Vendors.MobileNo,
                vendorWhats = "+966" + m.Vendors.MobileNo,
                orderStatusId = m.OrderStatusID,
                vendorName = lang == "ar" ? m.Vendors.StoreNameAr : m.Vendors.StoreNameEn,
                paymentTypestr = lang == "ar" ? (m.Orders.PaymentTypeId == (int)PaymentTypeEnum.COD ? "نقد عند الإستلام" : m.Orders.PaymentTypeId == (int)PaymentTypeEnum.Payment_Card ? "بطاقة دفع" :
                  m.Orders.PaymentTypeId == (int)PaymentTypeEnum.Bank_Transfer ? "تحويل بنكي" : "المحفظة") :
                  (m.Orders.PaymentTypeId == (int)PaymentTypeEnum.COD ? "Cash on Receive" : m.Orders.PaymentTypeId == (int)PaymentTypeEnum.Payment_Card ? "Payment Card" :
                  m.Orders.PaymentTypeId == (int)PaymentTypeEnum.Bank_Transfer ? "Bank Transfer" : "Wallet"),
                date = m.CreateDate.ToString("dd-MM-yyyy hh:mm tt"),
                address = m.Orders.CustomerLocation.Address,
                status = lang == "ar" ? m.OrderStatus.NameAR : m.OrderStatus.NameEN,
                price = lang == "ar" ? m.Total + " ريـال" : m.Total + " SAR",
                blockName = lang == "ar" ? m.Orders.CustomerLocation.Block.NameAR : m.Orders.CustomerLocation.Block.NameEN,
                isCancel = m.OrderStatusID == (int)OrderStatusEnum.Create ? true : false,
                listItems = m.OrderItems.Where(s => !s.IsDeleted && s.IsEnable).Select(z => new ListItems()
                {
                    productName = lang == "ar" ? z.ProdNameAr : z.ProdNameEn,
                    price = lang == "ar" ? z.Price + " ريـال" : z.Price + " SAR",
                    quantity = z.Quantity,
                    productImage = !string.IsNullOrEmpty(z.ProdImage) ? (productPath + z.ProdImage) : "https://cdn.made-home.com/Home/beet_logo.png",
                }).ToList(),
                countItems = m.OrderItems.Count(),
                colorHex = (type == 1 ? ((m.Orders.PaymentTypeId == (int)PaymentTypeEnum.Payment_Card || m.Orders.PaymentTypeId == (int)PaymentTypeEnum.Bank_Transfer || m.Orders.PaymentTypeId == (int)PaymentTypeEnum.Wallet ? "#964B00" : "#32CD32"))
                  : (m.OrderStatusID == (int)OrderStatusEnum.Delivered ? "#32CD32" : "#964B00")),
                createDate = m.CreateDate,
                orderType = lang == "ar" ? (m.OrderTypeId == (int)OrderTypeEnum.Now ? "طلب فورى" : "طلب مجدول : " + (m.ScheduleDate != null ? m.ScheduleDate.Value.ToString("dd-MM-yyyy")
                + "   " + m.ScheduleTime.Value.ToString("hh:mm tt") : "")) : (m.OrderTypeId == (int)OrderTypeEnum.Now ? "Now Order" : "Schedule Order : " + (m.ScheduleDate != null ? m.ScheduleDate.Value.ToString("dd-MM-yyyy") + "   " + m.ScheduleTime.Value.ToString("hh:mm tt") : "")),
            }).ToList();
            Data = Data.Skip(Convert.ToInt32(page - 1) * 10).ToList();
            var DataTake = Data.Take(((Convert.ToInt32(page) * 10)) - (Convert.ToInt32(page - 1) * 10)).ToList();
            bool NextPage = false;
            if (Data.Count() > 10)
            {
                NextPage = true;
            }
            DataTake = DataTake.OrderByDescending(s => s.createDate).ToList();
            var model = new CustomerOrdersViewModelApi();
            model.listProduct = DataTake;
            model.isNextPage = NextPage;
            return model;
        }
        public CustomerOrdersViewModelApi CustomerIncreaseOrders(string lang, int page, string vendorpath, int CustoemrID, int type, string customerPath, string productPath)
        {
            var Data = Uow.OrderVendor.GetAll(s => s.IsDeleted && s.IsEnable && s.Orders.CustomersID == CustoemrID &&
      (s.OrderStatusID == (int)OrderStatusEnum.Pending) && s.ApprovalQuantity == type,
      "Vendors,Orders.CustomerLocation.Block,OrderStatus").OrderByDescending(s => s.CreateDate).Select(m => new ListProduct()
      {
          customerName = m.Orders.Customers.FirstName + " " + m.Orders.Customers.SeconedName,
          customerLogo = !string.IsNullOrWhiteSpace(m.Orders.Customers.ProfilePic) ? customerPath + m.Orders.Customers.ProfilePic : "",
          customerLat = m.Orders.CustomerLocation.Lat,
          customerLng = m.Orders.CustomerLocation.Lng,
          vendorLat = m.Vendors.Lat,
          vendorLng = m.Vendors.Lng,
          vendorImage = !string.IsNullOrWhiteSpace(m.Vendors.Logo) ? vendorpath + m.Vendors.Logo : "https://cdn.made-home.com/Home/beet_logo.png",
          vendorId = m.VendorsID,
          trackNo = m.TrackNo,
          orderId = m.OrdersID,
          orderVendorID = m.OrderVendorID,
          customerMobile = "0" + m.Orders.Customers.MobileNo,
          customerWhats = "+966" + m.Orders.Customers.MobileNo,
          vendorMobile = "0" + m.Vendors.MobileNo,
          vendorWhats = "+966" + m.Vendors.MobileNo,
          orderStatusId = m.OrderStatusID,
          vendorName = lang == "ar" ? m.Vendors.StoreNameAr : m.Vendors.StoreNameEn,
          paymentTypestr = lang == "ar" ? (m.Orders.PaymentTypeId == (int)PaymentTypeEnum.COD ? "نقد عند الإستلام" : m.Orders.PaymentTypeId == (int)PaymentTypeEnum.Payment_Card ? "بطاقة دفع" :
                  m.Orders.PaymentTypeId == (int)PaymentTypeEnum.Bank_Transfer ? "تحويل بنكي" : "المحفظة") :
                  (m.Orders.PaymentTypeId == (int)PaymentTypeEnum.COD ? "Cash on Receive" : m.Orders.PaymentTypeId == (int)PaymentTypeEnum.Payment_Card ? "Payment Card" :
                  m.Orders.PaymentTypeId == (int)PaymentTypeEnum.Bank_Transfer ? "Bank Transfer" : "Wallet"),
          date = m.CreateDate.ToString("dd-MM-yyyy hh:mm tt"),
          address = m.Orders.CustomerLocation.Address,
          status = lang == "ar" ? m.OrderStatus.NameAR : m.OrderStatus.NameEN,
          price = lang == "ar" ? m.Total + " ريـال" : m.Total + " SAR",
          blockName = lang == "ar" ? m.Orders.CustomerLocation.Block.NameAR : m.Orders.CustomerLocation.Block.NameEN,
          isCancel = m.OrderStatusID == (int)OrderStatusEnum.Create ? true : false,
          listItems = m.OrderItems.Where(s => !s.IsDeleted && s.IsEnable).Select(z => new ListItems()
          {
              productName = lang == "ar" ? z.ProdNameAr : z.ProdNameEn,
              price = lang == "ar" ? z.Price + " ريـال" : z.Price + " SAR",
              quantity = z.Quantity,
              productImage = !string.IsNullOrEmpty(z.ProdImage) ? (productPath + z.ProdImage) : "https://cdn.made-home.com/Home/beet_logo.png",
          }).ToList(),
          countItems = m.OrderItems.Count(),
          colorHex = (type == 1 ? ((m.Orders.PaymentTypeId == (int)PaymentTypeEnum.Payment_Card || m.Orders.PaymentTypeId == (int)PaymentTypeEnum.Bank_Transfer || m.Orders.PaymentTypeId == (int)PaymentTypeEnum.Wallet ? "#964B00" : "#32CD32"))
                  : (m.OrderStatusID == (int)OrderStatusEnum.Delivered ? "#32CD32" : "#964B00")),
          createDate = m.CreateDate,
          orderType = lang == "ar" ? (m.OrderTypeId == (int)OrderTypeEnum.Now ? "طلب فورى" : "طلب مجدول : " + (m.ScheduleDate != null ? m.ScheduleDate.Value.ToString("dd-MM-yyyy")
                + "   " + m.ScheduleTime.Value.ToString("hh:mm tt") : "")) : (m.OrderTypeId == (int)OrderTypeEnum.Now ? "Now Order" : "Schedule Order : " + (m.ScheduleDate != null ? m.ScheduleDate.Value.ToString("dd-MM-yyyy") + "   " + m.ScheduleTime.Value.ToString("hh:mm tt") : "")),
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
            var model = new CustomerOrdersViewModelApi();
            model.listProduct = DataTake;
            model.isNextPage = NextPage;
            return model;
        }
        public ProdQuestionsViewModelApi ProdQuestions(string lang, int page, string vendorpath, int ProdId)
        {
            var Data = Uow.ProdQuestion.GetAll(s => !s.IsDeleted && s.IsEnable && s.ProductID == ProdId,
            "Product.Vendors").Select(m => new ListProductQuestion()
            {
                vendorImage = !string.IsNullOrWhiteSpace(m.Product.Vendors.Logo) ? vendorpath + m.Product.Vendors.Logo : "",
                vendorName = lang == "ar" ? m.Product.Vendors.StoreNameAr : m.Product.Vendors.StoreNameEn,
                questionDate = m.CreateDate.ToString("dd-MM-yyyy"),
                answerDate = m.UpdateDate != null ? m.UpdateDate.Value.ToString("dd-MM-yyyy") : "",
                answer = m.Answer,
                question = m.Question,
                isRepley = m.IsRepley,
            }).ToList();
            Data = Data.Skip(Convert.ToInt32(page - 1) * 10).ToList();
            var DataTake = Data.Take(((Convert.ToInt32(page) * 10)) - (Convert.ToInt32(page - 1) * 10)).ToList();
            bool NextPage = false;
            if (Data.Count() > 10)
            {
                NextPage = true;
            }
            var model = new ProdQuestionsViewModelApi();
            model.questions = DataTake;
            model.isNextPage = NextPage;
            return model;
        }
        public ProdQuestionsViewModelApi CustomerQuestions(string lang, int page, string vendorpath, string productPath, int CustoemrID)
        {
            var Data = Uow.ProdQuestion.GetAll(s => !s.IsDeleted && s.IsEnable && s.CustomersID == CustoemrID,
            "Product.Vendors").Select(m => new ListProductQuestion()
            {
                vendorImage = !string.IsNullOrWhiteSpace(m.Product.Vendors.Logo) ? vendorpath + m.Product.Vendors.Logo : "",
                vendorName = lang == "ar" ? m.Product.Vendors.StoreNameAr : m.Product.Vendors.StoreNameEn,
                questionDate = m.CreateDate.ToString("dd-MM-yyyy"),
                answerDate = m.UpdateDate != null ? m.UpdateDate.Value.ToString("dd-MM-yyyy") : "",
                answer = m.Answer,
                question = m.Question,
                isRepley = m.IsRepley,
                productName = lang == "ar" ? m.Product.NameAr : m.Product.NameEn,
                productImage = !string.IsNullOrEmpty(m.Product.Logo) ? (productPath + m.Product.Logo) : "",
            }).ToList();
            Data = Data.Skip(Convert.ToInt32(page - 1) * 10).ToList();
            var DataTake = Data.Take(((Convert.ToInt32(page) * 10)) - (Convert.ToInt32(page - 1) * 10)).ToList();
            bool NextPage = false;
            if (Data.Count() > 10)
            {
                NextPage = true;
            }
            var model = new ProdQuestionsViewModelApi();
            model.questions = DataTake;
            model.isNextPage = NextPage;
            return model;
        }
        public ProdRatesViewModelApi NewProdRates(string lang, int page, string vendorpath, int CustomersID)
        {
            var maxprod = Uow.OrderItems.GetAll(x => !x.IsDeleted).GroupBy(x => x.ProductID).OrderByDescending(x => x.Count()).Select(x => x.Count()).FirstOrDefault();
            var Data = Uow.OrderItems.GetAll(s => !s.IsDeleted && s.IsEnable && s.OrderVendor.Orders.CustomersID == CustomersID,
            "OrderVendor.Vendors").ToList().GroupBy(x => x.ProductID).Select(m => new ViewModel.Customer.ListProductRate()
            {
                vendorImage = !string.IsNullOrWhiteSpace(m.FirstOrDefault().OrderVendor.Vendors.Logo) ? vendorpath + m.FirstOrDefault().OrderVendor.Vendors.Logo : "",
                vendorName = lang == "ar" ? m.FirstOrDefault().OrderVendor.Vendors.StoreNameAr : m.FirstOrDefault().OrderVendor.Vendors.StoreNameEn,
                rateDate = m.FirstOrDefault().CreateDate.ToString("dd-MM-yyyy"),
                answerDate = m.FirstOrDefault().UpdateDate != null ? m.FirstOrDefault().UpdateDate.Value.ToString("dd-MM-yyyy") : "",
                answer = string.Empty,
                trackNo = m.FirstOrDefault().OrderVendor.TrackNo,
                rateComment = string.Empty,
                isRepley = false,
                rate = 5 * ((decimal)m.Count() / (decimal)maxprod),
                rateTitle = lang == "ar" ? ((5 * ((decimal)m.Count() / (decimal)maxprod)) <= 1 ? "سيء جدا" : (5 * ((decimal)m.Count() / (decimal)maxprod)) <= 2 ? "سيء" : (5 * ((decimal)m.Count() / (decimal)maxprod)) <= 3 ? "مقبول" : (5 * ((decimal)m.Count() / (decimal)maxprod)) <= 4 ? "ممتاز" : "عالى الجودة") :
                           ((5 * ((decimal)m.Count() / (decimal)maxprod)) <= 1 ? "Very bad" : (5 * ((decimal)m.Count() / (decimal)maxprod)) <= 2 ? "Bad" : (5 * ((decimal)m.Count() / (decimal)maxprod)) <= 3 ? "Acceptable" : (5 * ((decimal)m.Count() / (decimal)maxprod)) <= 4 ? "Excellent" : "High quality"),
            }).ToList();
            Data = Data.Skip(Convert.ToInt32(page - 1) * 10).ToList();
            var DataTake = Data.Take(((Convert.ToInt32(page) * 10)) - (Convert.ToInt32(page - 1) * 10)).ToList();
            bool NextPage = false;
            if (Data.Count() > 10)
            {
                NextPage = true;
            }
            var model = new ProdRatesViewModelApi();
            model.rates = DataTake;
            model.isNextPage = NextPage;
            return model;
        }
        public bool AddQuestionProd(QuestionViewModelApi model, int CustomerID, int UserId, string FireBaseKey)
        {
            var Data = new ProdQuestion()
            {
                IsDeleted = false,
                IsEnable = false,
                IsRepley = false,
                CreateDate = _bLGeneral.GetDateTimeNow(),
                CustomersID = CustomerID,
                ProductID = model.productId,
                Question = model.question,
                ProdQuestionGuid = Guid.NewGuid(),
                Answer = string.Empty,
                UserId = UserId
            };
            Uow.ProdQuestion.Insert(Data);
            Uow.Commit();
            var ratedate = Uow.ProdQuestion.GetAll(s => s.ProdQuestionID == Data.ProdQuestionID, "Product.Vendors").FirstOrDefault();
            var Noti = new Notification()
            {
                CreateDate = _bLGeneral.GetDateTimeNow(),
                TitleAR = "تم إرسال استفسار علي المنتج " + ratedate.Product.NameAr + "",
                TitleEN = "customer has Inquiry on product " + ratedate.Product.NameEn + "",
                DescEN = model.question,
                DescAR = model.question,
                IsDeleted = false,
                IsEnable = true,
                IsSeen = false,
                NotificationGuid = Guid.NewGuid(),
                NotificationTypeID = (int)NotificationTypeEnum.Question,
                UserId = UserId,
                NotificationDate = _bLGeneral.GetDateTimeNow(),
                ProdQuestionID = ratedate.ProdQuestionID,
                VendorsID = ratedate.Product.VendorsID,
            };
            Uow.Notification.Insert(Noti);
            var _PushListVendor = new PushList()
            {
                orderId = ratedate.ProdQuestionID,
                lat = 0,
                lng = 0,
                trackNo = "",
                profit = "",
                cusotmerAddress = "",
                vendorAddress = "",
                vendorLogo = "",
                vendorName = "",
                body = "تم إرسال استفسار علي المنتج " + ratedate.Product.NameAr + "",
                sound = "custom_sound.wav", //For IOS
                title = "استفسار علي منتج",
                content_available = "true", //For Android
                priority = "high", //For Android,
                serverKey = FireBaseKey,
                estimation = 20,
                pushType = (int)PushTypeEnum.Question
            };
            var UserDataVendor = _blTokens.GetMobileDataByUserID(ratedate.Product.Vendors.UserId);
            if (UserDataVendor != null)
            {
                var res = _bLGeneral.SendPush(UserDataVendor.TokenVal, UserDataVendor.DeviceType, _PushListVendor);
            }

            Uow.Commit();
            return true;
        }
        public bool AddRateProd(RateViewModelApi model, int UserId, string FireBaseKey)
        {
            var Data = new OrderRate()
            {
                IsDeleted = false,
                IsEnable = true,
                IsRepley = false,
                CreateDate = _bLGeneral.GetDateTimeNow(),
                OrderVendorID = model.orderId,
                RateOrder = model.rateOrder,
                OrderRateGuid = Guid.NewGuid(),
                AnswerRate = string.Empty,
                CommentDelivery = model.commentDelivery,
                CommentOrder = model.commentOrder,
                RateDelivery = model.rateDelivery,
                UserId = UserId
            };
            Uow.OrderRate.Insert(Data);
            Uow.Commit();
            var ratedate = Uow.OrderRate.GetAll(s => s.OrderRateID == Data.OrderRateID, "OrderVendor.Vendors").FirstOrDefault();
            var Noti = new Notification()
            {
                CreateDate = _bLGeneral.GetDateTimeNow(),
                TitleAR = "تم إرسال تقييم على الطلب رقم " + ratedate.OrderVendor.TrackNo + "",
                TitleEN = "customer ratted order " + ratedate.OrderVendor.TrackNo + "",
                DescEN = model.commentOrder,
                DescAR = model.commentOrder,
                IsDeleted = false,
                IsEnable = true,
                IsSeen = false,
                NotificationGuid = Guid.NewGuid(),
                NotificationTypeID = (int)NotificationTypeEnum.Rate,
                UserId = UserId,
                NotificationDate = _bLGeneral.GetDateTimeNow(),
                OrderRateID = ratedate.OrderRateID,
                VendorsID = ratedate.OrderVendor.VendorsID,
            };
            var _PushListVendor = new PushList()
            {
                orderId = ratedate.OrderRateID,
                lat = 0,
                lng = 0,
                trackNo = ratedate.OrderRateID.ToString(),
                profit = "",
                cusotmerAddress = "",
                vendorAddress = "",
                vendorLogo = "",
                vendorName = "",
                body = "تم إرسال تقييم على الطلب رقم " + ratedate.OrderVendor.TrackNo + "",
                sound = "custom_sound.wav", //For IOS
                title = "تقييم الطلب",
                content_available = "true", //For Android
                priority = "high", //For Android,
                serverKey = FireBaseKey,
                estimation = 20,
                pushType = (int)PushTypeEnum.Rate
            };
            var UserDataVendor = _blTokens.GetMobileDataByUserID(ratedate.OrderVendor.Vendors.UserId);
            if (UserDataVendor != null)
            {
                _bLGeneral.SendPush(UserDataVendor.TokenVal, UserDataVendor.DeviceType, _PushListVendor);
            }
            Uow.Notification.Insert(Noti);
            Uow.Commit();
            return true;
        }
        public bool SetCustomerFavourite(int ProdId, int CustomerID, int userid)
        {
            try
            {
                var IsCustomerFav = Uow.CustomerFavourites.GetAll(s => s.CustomersID == CustomerID && s.ProductID == ProdId && !s.IsDeleted).FirstOrDefault();
                if (IsCustomerFav != null)
                {
                    IsCustomerFav.IsDeleted = true;
                    IsCustomerFav.UserDelete = userid;
                    IsCustomerFav.DeleteDate = _bLGeneral.GetDateTimeNow();
                    Uow.CustomerFavourites.Update(IsCustomerFav);
                    Uow.Commit();
                }
                else
                {
                    IsCustomerFav = Uow.CustomerFavourites.GetAll(s => s.CustomersID == CustomerID && s.ProductID == ProdId).FirstOrDefault();
                    if (IsCustomerFav != null)
                    {
                        IsCustomerFav.IsDeleted = false;
                        IsCustomerFav.UserUpdate = userid;
                        IsCustomerFav.UpdateDate = _bLGeneral.GetDateTimeNow();
                        Uow.CustomerFavourites.Update(IsCustomerFav);
                        Uow.Commit();
                    }
                    else
                    {
                        var CusFav = new CustomerFavourites() { IsDeleted = false, IsEnable = true, ProductID = ProdId, CustomersID = CustomerID, UserId = userid, CustomerFavouritesGuid = Guid.NewGuid() };
                        Uow.CustomerFavourites.Insert(CusFav);
                        Uow.Commit();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        public CustomerViewModel GetCustomersViewModelByGuid(Guid EntityCustomerGuid, string lang, string MainPathView, string productPath)
        {
            var getData = Uow.Customers.GetAll(x => !x.IsDeleted
                                                   && x.CustomersGuid == EntityCustomerGuid
                , "City.Region,Nationality,CustomerLocation.Block.City,CustomerFavourites.Product").OrderByDescending(p => p.CreateDate)
            .Select(p => new CustomerViewModel()
            {
                CityID = p.CityID,
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
                NationalityID = p.NationalityID,
                Address = p.Address,
                CityName = lang == "ar" ? p.City.NameAR : p.City.NameEN,
                NationalityName = lang == "ar" ? p.Nationality.NameAR : p.Nationality.NameEN,
                FirstName = p.FirstName,
                SeconedName = p.SeconedName,
                Gender = p.Gender,
                ProfilePic = !string.IsNullOrEmpty(p.ProfilePic) ? (MainPathView + p.ProfilePic) : p.ProfilePic,
                CustomersID = p.CustomersID,
                CustomersGuid = p.CustomersGuid,
                CustomerLocations = p.CustomerLocation.Select(L => new UpdateCustomerLocation()
                {
                    name = L.Name,
                    mobileNo = L.MobileNo,
                    address = L.Address,
                    cityName = lang == "ar" ? L.Block.City.NameAR : L.Block.City.NameEN,
                    blockName = lang == "ar" ? L.Block.NameAR : L.Block.NameEN,
                    streetNo = L.StreetNo,
                    uniqueSign = L.UniqueSign,
                    buildingNumber = L.BuildingNumber
                }).ToList(),
                CustomerFavourites = p.CustomerFavourites.Select(CustomerFavourite => new CustomerFavourritesViewModel()
                {
                    ProductName = lang == "ar" ? CustomerFavourite.Product.NameAr : CustomerFavourite.Product.NameEn,
                    Logo = productPath + CustomerFavourite.Product.Logo,
                }).ToList()
            }).FirstOrDefault();
            return getData;
        }
        public bool IsUserNameExists(string username)
        {
            return Uow.User.GetAll(s => s.UserName == username).Count() > 0;
        }
        public bool UpdateCustomerApi(UpdateCustomerViewModel model, string profilePhotofileName, int CustomerId, int UserId)
        {
            var Customers = Uow.Customers.GetAll(s => s.CustomersID == CustomerId).FirstOrDefault();
            Customers.UpdateDate = _bLGeneral.GetDateTimeNow();
            Customers.Email = model.email;
            Customers.MobileNo = model.mobileNo;
            Customers.FirstName = model.firstName;
            // Customers.Gender = model.gender;
            Customers.SeconedName = model.seconedName;
            Customers.UserUpdate = UserId;
            // Customers.CityID = model.cityID;
            // Customers.NationalityID = model.nationaltyID;
            //if (!string.IsNullOrWhiteSpace(profilePhotofileName))
            //{
            //    Customers.ProfilePic = profilePhotofileName;
            //}
            Uow.Customers.Update(Customers);
            Uow.Commit();
            return true;
        }
        public bool UpdatePersonalData(PersonalDataViewModel model, int UserId)
        {
            var Customers = Uow.Customers.GetAll(s => s.UserId == UserId).FirstOrDefault();
            Customers.UpdateDate = _bLGeneral.GetDateTimeNow();
            Customers.Email = model.Email;
            Customers.MobileNo = model.MobileNo;
            Customers.FirstName = model.Name;
            //Customers.SeconedName = model.LastName;
            Customers.Gender = model.GenderID;
            Customers.UserUpdate = UserId;
            Customers.NationalityID = model.NationalityID;
            Customers.CityID = model.CityID;
            Uow.Customers.Update(Customers);
            Uow.Commit();
            return true;
        }
        public bool UpdateLocation(LocationDetails model, int UserId, int customerID)
        {
            var vendors = Uow.CustomerLocation.GetAll(s => s.CustomersID == customerID && !s.IsDeleted && !s.IsLocation).FirstOrDefault();
            if (vendors != null)
            {
                vendors.Lat = (double)model.lat;
                vendors.Lng = (double)model.lng;
                vendors.BlockID = (int)model.blockID;
                vendors.Address = model.address;
                vendors.UserUpdate = UserId;
                vendors.UpdateDate = _bLGeneral.GetDateTimeNow();
                vendors.IsLocation = true;
                //vendors.RegisterType = (int)RegisterType.Submitted;
                Uow.CustomerLocation.Update(vendors);
                Uow.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }
        public CustomerViewModelApi GetCustomersViewModelApi(int CustomerId, string lang, string MainPathView, int userId)
        {

            var getData = Uow.Customers.GetAll(x => !x.IsDeleted
                                                   && x.CustomersID == CustomerId
                , "").OrderByDescending(p => p.CreateDate)
            .Select(p => new CustomerViewModelApi()
            {
                cityID = p.CityID,
                gender = p.Gender,
                mobileNo = p.MobileNo,
                email = p.Email,
                firstName = p.FirstName,
                seconedName = p.SeconedName,
                nationalityID = p.NationalityID,
                address = !string.IsNullOrWhiteSpace(p.Address) ? p.Address : "",
                cityName = lang == "ar" ? p.City.NameAR : p.City.NameEN,
                nationalityName = lang == "ar" ? p.Nationality.NameAR : p.Nationality.NameEN,
                profilePic = !string.IsNullOrWhiteSpace(p.ProfilePic) ? (MainPathView + p.ProfilePic) : "",
                customerID = p.CustomersID,
                isLocation = p.CustomerLocation.FirstOrDefault(s => !s.IsDeleted) != null ? p.CustomerLocation.FirstOrDefault(s => !s.IsDeleted).IsLocation : true,
                notificationCount = GetUserNotificationNotSeenCount(userId),
                walletAmount = GetLastBlance(CustomerId),
                wallet = lang == "ar" ? "لديك " + GetLastBlance(CustomerId) + " ريـال في المحفظة" : "you have " + GetLastBlance(CustomerId) + " SR in your wallet",
            }).FirstOrDefault();
            return getData;
        }
        public IQueryable<CustomerViewModel> GetAllCustomersViewModelBySearch(string[] ListCityID, string Name, string Mobile, string lang)
        {
            if (ListCityID != null)
            {
                if (ListCityID.Count(p => !string.IsNullOrEmpty(p)) == 0)
                {
                    ListCityID = null;
                }
            }

            var getData = Uow.Customers.GetAll(x => !x.IsDeleted
             && true
                                                   && (ListCityID != null ? ListCityID.Any(y => x.CityID.ToString() == y) : true)
                                                   && (!string.IsNullOrEmpty(Name) ? (x.FirstName == Name) : true)

              && (!string.IsNullOrEmpty(Mobile) ? x.MobileNo == Mobile : true)
                , "City.Region,Nationality,Orders.OrderVendor").OrderByDescending(p => p.CreateDate).ToList()
            .Select(p => new CustomerViewModel()
            {
                CityID = p.CityID,
                IsEnable = p.IsEnable,
                MobileNo = p.MobileNo,
                Email = !string.IsNullOrWhiteSpace(p.Email) ? p.Email : "---",
                Address = !string.IsNullOrWhiteSpace(p.Address) ? p.Address : "---",
                EnableDate = p.EnableDate,
                IsDeleted = p.IsDeleted,
                UpdateDate = p.UpdateDate,
                UserUpdate = p.UserUpdate,
                CreateDate = p.CreateDate,
                DeleteDate = p.DeleteDate,
                UserId = p.UserId,
                UserDelete = p.UserDelete,
                UserEnable = p.UserEnable,
                NationalityID = p.NationalityID,
                CityName = lang == "ar" ? p.City.NameAR : p.City.NameEN,
                NationalityName = lang == "ar" ? p.Nationality.NameAR : p.Nationality.NameEN,
                FirstName = p.FirstName,
                SeconedName = p.SeconedName,
                Gender = p.Gender,
                CustomersID = p.CustomersID,
                CustomersGuid = p.CustomersGuid,
                IsEnableString = lang == "ar" ? (p.IsEnable ? "مفعل" : "غير مفعل") : (p.IsEnable ? "Enabled" : "Disabled"),
                CreateDateString = p.CreateDate.ToString("hh:mm tt"),
                DeliveredCount = p.Orders.Where(x => !x.IsDeleted && x.OrderVendor.Any(y => !y.IsDeleted)).Sum(s => s.OrderVendor.Count(x => !x.IsDeleted && x.OrderStatusID == (int)OrderStatusEnum.Delivered)),
            }).AsQueryable();
            return getData;
        }
        public IQueryable<CustomerViewModel> GetAllCustomersViewModel(string lang)
        {

            var getData = Uow.Customers.GetAll(x => !x.IsDeleted
                , "City.Region,Nationality,Orders.OrderVendor,CustomerBalance").OrderByDescending(p => p.CreateDate).ToList()
            .Select(p => new CustomerViewModel()
            {
                CityID = p.CityID,
                IsEnable = p.IsEnable,
                MobileNo = p.MobileNo,
                Email = !string.IsNullOrWhiteSpace(p.Email) ? p.Email : "---",
                Address = !string.IsNullOrWhiteSpace(p.Address) ? p.Address : "---",
                EnableDate = p.EnableDate,
                IsDeleted = p.IsDeleted,
                UpdateDate = p.UpdateDate,
                UserUpdate = p.UserUpdate,
                CreateDate = p.CreateDate,
                DeleteDate = p.DeleteDate,
                UserId = p.UserId,
                UserDelete = p.UserDelete,
                UserEnable = p.UserEnable,
                NationalityID = p.NationalityID,
                CityName = lang == "ar" ? p.City.NameAR : p.City.NameEN,
                NationalityName = lang == "ar" ? p.Nationality.NameAR : p.Nationality.NameEN,
                FirstName = p.FirstName,
                SeconedName = p.SeconedName,
                Gender = p.Gender,
                CustomersID = p.CustomersID,
                CustomersGuid = p.CustomersGuid,
                IsEnableString = lang == "ar" ? (p.IsEnable ? "مفعل" : "غير مفعل") : (p.IsEnable ? "Enabled" : "Disabled"),
                CreateDateString = p.CreateDate.ToString("hh:mm tt"),
                DeliveredCount = p.Orders.Where(x => !x.IsDeleted && x.OrderVendor.Any(y => !y.IsDeleted)).Sum(s => s.OrderVendor.Count(x => !x.IsDeleted && x.OrderStatusID == (int)OrderStatusEnum.Delivered)),
                Balance = Math.Round(p.CustomerBalance.Where(s => s.TypeOperationID == (int)TypeOperationEnum.CR).Sum(e => e.Amount) - p.CustomerBalance.Where(s => s.TypeOperationID == (int)TypeOperationEnum.DR).Sum(e => e.Amount), 2),
            }).AsQueryable();
            return getData;
        }
        public IQueryable<CustomerViewModel> GetAllCustomersViewModelByVendorAndSearch(int VendorsID, string[] ListCityID, string Name, string Mobile, string lang)
        {
            if (ListCityID != null)
            {
                if (ListCityID.Count(p => !string.IsNullOrEmpty(p)) == 0)
                {
                    ListCityID = null;
                }
            }

            var getData = Uow.Customers.GetAll(x => !x.IsDeleted
            && x.Orders.Any(y => y.OrderVendor.Any(z => z.VendorsID == VendorsID))
                                                   && (ListCityID != null ? ListCityID.Any(y => x.CityID.ToString() == y) : true)
                                                   && (!string.IsNullOrEmpty(Name) ? (x.FirstName == Name) : true)

              && (!string.IsNullOrEmpty(Mobile) ? x.MobileNo == Mobile : true)
                , "City.Region,Nationality").OrderByDescending(p => p.CreateDate)
            .Select(p => new CustomerViewModel()
            {
                CityID = p.CityID,
                IsEnable = p.IsEnable,
                MobileNo = p.MobileNo,
                Email = !string.IsNullOrWhiteSpace(p.Email) ? p.Email : "---",
                Address = !string.IsNullOrWhiteSpace(p.Address) ? p.Address : "---",
                EnableDate = p.EnableDate,
                IsDeleted = p.IsDeleted,
                UpdateDate = p.UpdateDate,
                UserUpdate = p.UserUpdate,
                CreateDate = p.CreateDate,
                DeleteDate = p.DeleteDate,
                UserId = p.UserId,
                UserDelete = p.UserDelete,
                UserEnable = p.UserEnable,
                NationalityID = p.NationalityID,
                CityName = lang == "ar" ? p.City.NameAR : p.City.NameEN,
                NationalityName = lang == "ar" ? p.Nationality.NameAR : p.Nationality.NameEN,
                FirstName = p.FirstName,
                SeconedName = p.SeconedName,
                Gender = p.Gender,
                CustomersID = p.CustomersID,
                CustomersGuid = p.CustomersGuid,
                IsEnableString = lang == "ar" ? (p.IsEnable ? "مفعل" : "غير مفعل") : (p.IsEnable ? "Enabled" : "Disabled"),
            });
            return getData;
        }
        public bool IsExistEmail(string Email, int Id) =>
          Uow.Customers.GetAll().Any(query => query.CustomersID != Id && query.Email.Trim() == Email.Trim() && !query.IsDeleted);
        public bool IsExistMobile(string MobileNo, int Id) =>
           Uow.Customers.GetAll().Any(query => query.CustomersID != Id && query.MobileNo.Trim() == MobileNo.Trim() && !query.IsDeleted);
        public SiteCustomersViewModel GetSiteCustomersViewModel(int UserId, string lang, string MainPathView)
        {

            var getData = Uow.Customers.GetAll(x => !x.IsDeleted
                                                   && x.UserId == UserId
                , "").OrderByDescending(p => p.CreateDate)
            .Select(p => new SiteCustomersViewModel()
            {
                ProfilePic = !string.IsNullOrWhiteSpace(p.ProfilePic) ? (MainPathView + p.ProfilePic) : "/assets/homeMadeSite/img/logo.svg",
                CustomerName = p.FirstName + " " + p.SeconedName,
                CustomersID = p.CustomersID
            }).FirstOrDefault();
            return getData;
        }
        public PersonalDataViewModel GetPersonalDataViewModelByUser(int UserId)
        {
            var getData = Uow.Customers.GetAll(x => !x.IsDeleted
                                                   && x.UserId == UserId
                , "").OrderByDescending(p => p.CreateDate)
            .Select(p => new PersonalDataViewModel()
            {
                Name = p.FirstName,
                LastName = p.SeconedName,
                CityID = p.CityID,
                Email = p.Email,
                GenderID = p.Gender,
                MobileNo = p.MobileNo,
                NationalityID = p.NationalityID,
                CustomersID = p.CustomersID
            }).FirstOrDefault();
            return getData;
        }
        public IEnumerable<SiteCustomerLocationViewModel> GetAllSiteCustomerLocationViewModelByUser(int UserId, string lang)
        {
            var CustomerLocations = Uow.CustomerLocation.GetAll(x => !x.IsDeleted && x.Customers.UserId == UserId).Select(p => new SiteCustomerLocationViewModel()
            {
                Name = p.Name,
                MobileNo = p.MobileNo,
                Address = p.Address,
                CityName = lang == "ar" ? p.Block.City.NameAR : p.Block.City.NameEN,
                BlockName = lang == "ar" ? p.Block.NameAR : p.Block.NameEN,
                StreetNo = p.StreetNo,
                UniqueSign = p.UniqueSign,
                BuildingNumber = p.BuildingNumber,
                Lat = p.Lat,
                Lng = p.Lng,
                AddressTypesID = p.AddressTypesID,
                BlockID = p.BlockID,
                CustomerLocationID = p.CustomerLocationID,
                CustomersID = p.CustomersID,
                IsVerfiy = p.IsVerfiy,
                AddressTypesName = lang == "ar" ? p.AddressTypes.NameAR : p.AddressTypes.NameEN,
            });
            return CustomerLocations;
        }
        public SiteCustomerLocationViewModel GetSiteCustomerLocationViewModelByCustomerLocation(int CustomerLocationID, string lang)
        {
            var CustomerLocations = Uow.CustomerLocation.GetAll(x => !x.IsDeleted && x.CustomerLocationID == CustomerLocationID, "Block.City.Region,AddressTypes").Select(p => new SiteCustomerLocationViewModel()
            {
                Name = p.Name,
                MobileNo = p.MobileNo,
                Address = p.Address,
                CityName = lang == "ar" ? p.Block.City.NameAR : p.Block.City.NameEN,
                BlockName = lang == "ar" ? p.Block.NameAR : p.Block.NameEN,
                StreetNo = p.StreetNo,
                UniqueSign = p.UniqueSign,
                BuildingNumber = p.BuildingNumber,
                Lat = p.Lat,
                Lng = p.Lng,
                AddressTypesID = p.AddressTypesID,
                BlockID = p.BlockID,
                CustomerLocationID = p.CustomerLocationID,
                CustomersID = p.CustomersID,
                IsVerfiy = p.IsVerfiy,
                AddressTypesName = lang == "ar" ? p.AddressTypes.NameAR : p.AddressTypes.NameEN,
                RegionID = p.Block.City.RegionID,
                RegionName = lang == "ar" ? p.Block.City.Region.NameAR : p.Block.City.Region.NameEN,
                CityID = p.Block.CityID,
            }).FirstOrDefault();
            return CustomerLocations;
        }
        public SiteReviewQeustionsViewModel GetSiteReviewQeustionsViewModelByUserCustomer(int UserId, string lang, string VendorPathView)
        {
            var data = new SiteReviewQeustionsViewModel();
            data.ListQuestions = Uow.ProdQuestion.GetAll(s => !s.IsDeleted && s.Customers.UserId == UserId).Select(m => new SiteCustomerQuestionViewModel()
            {
                QuestionDate = m.CreateDate.ToString("dd yyyy MMMM"),
                Question = m.Question,
                QuestionId = m.ProdQuestionID,
                IsRepley = m.IsRepley,
                Answer = m.Answer,
            });
            data.ListOrderRate = Uow.OrderRate.GetAll(s => !s.IsDeleted && s.OrderVendor.Orders.Customers.UserId == UserId).Select(m => new SiteOrderRateViewModel()
            {
                OrdersID = m.OrderVendor.OrdersID,
                RateDate = m.CreateDate.ToString("dd yyyy MMMM"),
                RateTime = m.CreateDate.ToString("hh:mm:ss tt"),
                CommentDelivery = m.CommentDelivery,
                AnswerRate = m.AnswerRate,
                RateOrder = m.RateOrder,
                RateDelivery = m.RateDelivery,
                IsRepley = m.IsRepley,
                VendorsName = lang == "ar" ? (m.OrderVendor.Vendors.StoreNameAr) : (m.OrderVendor.Vendors.StoreNameEn),
                VendorsLogo = !string.IsNullOrEmpty(m.OrderVendor.Vendors.Logo) ? (VendorPathView + m.OrderVendor.Vendors.Logo) : string.Empty,
                CommentOrder = m.CommentOrder,
                VendorsGuid = m.OrderVendor.Vendors.VendorsGuid,
                AnswerDate = m.UpdateDate != null ? m.UpdateDate.Value.ToString("dd yyyy MMMM") : "",
                AnswerTime = m.UpdateDate != null ? m.UpdateDate.Value.ToString("hh:mm:ss tt") : "",
                TrackNo = m.OrderVendor.TrackNo,
                RateTitle = lang == "ar" ? (m.RateOrder <= 1 ? "سيء جدا" : m.RateOrder <= 2 ? "سيء" : m.RateOrder <= 3 ? "مقبول" : m.RateOrder <= 4 ? "ممتاز" : "عالى الجودة") :
                           (m.RateOrder <= 1 ? "Very bad" : m.RateOrder <= 2 ? "Bad" : m.RateOrder <= 3 ? "Acceptable" : m.RateOrder <= 4 ? "Excellent" : "High quality"),
            });

            return data;
        }
        public bool Delete(int id, int userId, Guid customerNewGuid)
        {
            var customer = GetCustomerById(id);
            if (customer != null)
            {
                customer.IsDeleted = !  customer.IsDeleted;
                customer.UserDelete = userId;
                customer.DeleteDate = _bLGeneral.GetDateTimeNow();
                Uow.Customers.Update(customer);

                var vendorDataVendor = _blTokens.GetMobileDataByUserID(customer.UserId);
                if (vendorDataVendor != null)
                {
                    Uow.Tokens.Delete(vendorDataVendor);
                    _blTokens.UpdateUserJWTToken(customer.UserId, string.Empty);
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
            var data = GetCustomerById(id);
            if (data != null)
            {
                data.UserEnable = userId;
                data.IsEnable = data.IsEnable == Convert.ToBoolean(StatusEnable.Enable) ? Convert.ToBoolean(StatusEnable.Disable) : Convert.ToBoolean(StatusEnable.Enable);
                data.EnableDate = _bLGeneral.GetDateTimeNow();
                data = Uow.Customers.Update(data);
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
        #endregion
        #region Order Validation
        public PromoCodeViewModelApi CheckPromoCodeValid(string code, int customerId, decimal total, string accLanguage, int countVendors)
        {
            var CheckPromo = new PromoCodeViewModelApi();
            CheckPromo.deliveryPrice = Uow.Configuration.GetAll(s => !s.IsDeleted).FirstOrDefault() != null ? Uow.Configuration.GetAll(s => !s.IsDeleted).FirstOrDefault().DeliveryPrice : 0;
            CheckPromo.deliveryPrice = CheckPromo.deliveryPrice * countVendors;
            CheckPromo.price = Math.Round(total, 2);
            CheckPromo.discount = 0;
            CheckPromo.promoCodeID = 0;
            var vatpercent = _blConfiguration.GetDeliveryPriceVatPercent();
            CheckPromo.total = total / (1 + (vatpercent / 100));
            var vatvalue = (vatpercent * CheckPromo.total) / 100;
            CheckPromo.total = Math.Round(CheckPromo.total + vatvalue, 2);
            CheckPromo.message = "";
            CheckPromo.status = true;
            CheckPromo.vatValue = vatvalue;
            CheckPromo.vat = vatpercent;
            CheckPromo.totalWithDelivery = CheckPromo.total + CheckPromo.deliveryPrice;
            //CheckPromo.messageAr = "";

            if (!string.IsNullOrWhiteSpace(code))
            {
                var promoData = Uow.PromoCode.GetAll(p => !p.IsDeleted && p.Code == code).FirstOrDefault();
                if (promoData != null)
                {
                    var DateCheck = Uow.PromoCode.GetAll().Where(p => (DateTime.Now >= p.StartDate && DateTime.Now <= p.ExpiryDate) && p.PromoCodeID == promoData.PromoCodeID).FirstOrDefault();
                    if (DateCheck != null)
                    {
                        if (promoData.OrderPromo.Count(e => !e.IsDeleted) < promoData.LimitCount)
                        {
                            var IsForPassenger = Uow.OrderPromo.GetAll(e => e.Orders.CustomersID == customerId && e.PromoCodeID == promoData.PromoCodeID && e.Orders.OrderStatusID != (int)OrderStatusEnum.Drafts).Any();
                            if (IsForPassenger)
                            {
                                CheckPromo.message = accLanguage == "ar" ? "رمز ترويجي مستخدم من قبل" : "Promo Code Is Already Used";
                                CheckPromo.status = false;
                            }
                            else
                            {
                                decimal discount = decimal.Parse(promoData.DiscountValue.ToString());
                                if (promoData.DiscountType == (int)DiscountTypeEnum.Percent)
                                {
                                    discount = Math.Round((total * decimal.Parse(promoData.DiscountValue.ToString())) / 100, 2);
                                }
                                total = CheckPromo.total - discount;
                                CheckPromo.promoCodeID = promoData.PromoCodeID;
                                CheckPromo.discount = Math.Round(discount, 2);
                                CheckPromo.total = Math.Round(total, 2);
                                CheckPromo.totalWithDelivery = CheckPromo.totalWithDelivery - discount;
                                CheckPromo.message = accLanguage == "ar" ? "رمز ترويجي صالح" : "Valid promotional code";
                                CheckPromo.status = true;
                            }
                        }
                        else
                        {
                            CheckPromo.message = accLanguage == "ar" ? "تم انتهاء العدد الخاص بالرمز الترويجي" : "Maxumim Limt Count";
                            CheckPromo.status = false;
                        }
                    }
                    else
                    {
                        CheckPromo.message = accLanguage == "ar" ? "تم انتهاء مده الرمز الترويجي" : "Promo Code is Expired";
                        CheckPromo.status = false;
                    }
                }
                else
                {
                    CheckPromo.message = accLanguage == "ar" ? "رمز ترويجي غير صالح" : "Not Valid promotional code";
                    CheckPromo.status = false;
                }
            }
            return CheckPromo;
        }
        public PromoCodeViewModelApi_new CheckPromoCodeValid(string code, int customerId, decimal total, string accLanguage, string list_vendor, int address_id)
        {
            var CheckPromo = new PromoCodeViewModelApi_new();
            CheckPromo.listvendor = new List<listvendor>();
            CheckPromo.price = Math.Round(total, 2);
            CheckPromo.discount = 0;
            CheckPromo.promoCodeID = 0;
            var vatpercent = _blConfiguration.GetDeliveryPriceVatPercent();
            CheckPromo.total = total;
            CheckPromo.message = "";
            CheckPromo.status = true;
            CheckPromo.vat = vatpercent;
            string[] list_vendorIds = null;
            if (!string.IsNullOrEmpty(list_vendor) && list_vendor != "null")
            {
                list_vendorIds = list_vendor.Split(',');
            }

            foreach (var itemlist_vendorIds in list_vendorIds)
            {
                if (!string.IsNullOrWhiteSpace(itemlist_vendorIds))
                {
                    var listVendors = Uow.Vendors.GetAll(e => e.VendorsID == int.Parse(itemlist_vendorIds)).FirstOrDefault();
                    if (listVendors.DeliveryType == (int)DeliveryTypeEnum.Store) // فى حاله ان المتجر هو اللى هيوصل
                    {
                        CheckPromo.listvendor.Add(new listvendor { vendorId = int.Parse(itemlist_vendorIds), deliveryPrice = (decimal)listVendors.DeliveryPrice, distanceKM = 0 });
                        CheckPromo.deliveryPrice += (decimal)listVendors.DeliveryPrice;
                    }
                    else
                    {
                        // ارجاع الكونفجريشن الخاص بالشركه
                        var companyConfigration = Uow.Configuration.GetAll().FirstOrDefault();


                        // الحصول على عنوان العميل المختار من الموبايل
                        var CustomerLocation = Uow.CustomerLocation.GetAll(e => e.CustomerLocationID == address_id).FirstOrDefault();

                        // الحصول على المسافه بين العميل والاسره المنتجة
                        double distanceBTvendorKM = 0;
                        if (CustomerLocation != null)
                        {
                            distanceBTvendorKM = _bLGeneral.GetDistance((double)CustomerLocation.Lat, (double)CustomerLocation.Lng, (double)listVendors.Lat, (double)listVendors.Lng);
                        }

                        // الرجوع بأعلي مسافه لسعر التوصيل المحدد
                        var maxKm = companyConfigration.MinKM;

                        // الرجوع بسعر التوصيل بالقيمه المضافه
                        var deliveryPrice = companyConfigration.DeliveryPrice;

                        // التحقق من سعر التوصيل بالمسافه 
                        if ((decimal)distanceBTvendorKM <= maxKm)
                        {
                            CheckPromo.listvendor.Add(new listvendor { vendorId = int.Parse(itemlist_vendorIds), deliveryPrice = deliveryPrice, distanceKM = Convert.ToDecimal(distanceBTvendorKM.ToString("#.00")) });

                            CheckPromo.deliveryPrice += deliveryPrice;

                        }
                        else
                        {
                            // الرجوع بأعلي مسافه لسعر التوصيل المحدد
                            var priceOverKM = companyConfigration.OverKmFare;

                            // حساب الفارق بين العميل والاسره المنتجه
                            var distanceDifference = (decimal)distanceBTvendorKM - maxKm;
                            // ارجاع اجمالى سعر التوصيل 
                            var totalDeliveryPrice = deliveryPrice + (distanceDifference * priceOverKM);

                            CheckPromo.listvendor.Add(new listvendor { vendorId = int.Parse(itemlist_vendorIds), deliveryPrice = Convert.ToDecimal(totalDeliveryPrice.ToString("#.00")), distanceKM = Convert.ToDecimal(distanceBTvendorKM.ToString("#.00")) });

                            CheckPromo.deliveryPrice += totalDeliveryPrice;

                        }
                    }
                }
            }

            CheckPromo.totalWithDelivery = CheckPromo.total + CheckPromo.deliveryPrice;


            //CheckPromo.messageAr = "";

            if (!string.IsNullOrWhiteSpace(code))
            {
                var promoData = Uow.PromoCode.GetAll(p => !p.IsDeleted && p.Code == code).FirstOrDefault();
                if (promoData != null)
                {
                    var DateCheck = Uow.PromoCode.GetAll().Where(p => (DateTime.Now >= p.StartDate && DateTime.Now <= p.ExpiryDate) && p.PromoCodeID == promoData.PromoCodeID).FirstOrDefault();
                    if (DateCheck != null)
                    {
                        if (promoData.OrderPromo.Count(e => !e.IsDeleted) < promoData.LimitCount)
                        {
                            var IsForPassenger = Uow.OrderPromo.GetAll(e => e.Orders.CustomersID == customerId && e.PromoCodeID == promoData.PromoCodeID).Any();
                            if (IsForPassenger)
                            {
                                CheckPromo.message = accLanguage == "ar" ? "رمز ترويجي مستخدم من قبل" : "Promo Code Is Already Used";
                                CheckPromo.status = false;
                            }
                            else
                            {
                                decimal discount = decimal.Parse(promoData.DiscountValue.ToString());
                                if (promoData.DiscountType == (int)DiscountTypeEnum.Percent)
                                {
                                    discount = Math.Round((CheckPromo.totalWithDelivery * decimal.Parse(promoData.DiscountValue.ToString())) / 100, 2);
                                }
                                total = CheckPromo.total - discount;
                                CheckPromo.promoCodeID = promoData.PromoCodeID;
                                CheckPromo.discount = Math.Round(discount, 2);
                                CheckPromo.total = Math.Round(total, 2);
                                CheckPromo.totalWithDelivery = CheckPromo.totalWithDelivery - discount;
                                CheckPromo.message = accLanguage == "ar" ? "رمز ترويجي صالح" : "Valid promotional code";
                                CheckPromo.status = true;
                            }
                        }
                        else
                        {
                            CheckPromo.message = accLanguage == "ar" ? "تم انتهاء العدد الخاص بالرمز الترويجي" : "Maxumim Limt Count";
                            CheckPromo.status = false;
                        }
                    }
                    else
                    {
                        CheckPromo.message = accLanguage == "ar" ? "تم انتهاء مده الرمز الترويجي" : "Promo Code is Expired";
                        CheckPromo.status = false;
                    }
                }
                else
                {
                    CheckPromo.message = accLanguage == "ar" ? "رمز ترويجي غير صالح" : "Not Valid promotional code";
                    CheckPromo.status = false;
                }
            }


            CheckPromo.deliveryPrice = Convert.ToDecimal(CheckPromo.deliveryPrice.ToString("#.00"));
            CheckPromo.totalWithDelivery = Convert.ToDecimal(CheckPromo.totalWithDelivery.ToString("#.00"));

            CheckPromo.vatValue = Math.Round(CheckPromo.totalWithDelivery - (CheckPromo.totalWithDelivery / (1 + (_blConfiguration.GetDeliveryPriceVatPercent() / 100))), 2);
            CheckPromo.vatTotal = Math.Round(CheckPromo.totalWithDelivery - (CheckPromo.totalWithDelivery / (1 + (_blConfiguration.GetDeliveryPriceVatPercent() / 100))), 2);

            return CheckPromo;
        }
        public PromoCodeViewModelApi_new CheckPromoCodeValid_Web(string code, int customerId, decimal total, string accLanguage, List<int> list_vendor, int address_id, int _sessionID)
        {
            if (address_id == 0)
            {
                var customerLocation = Uow.CustomerLocation.GetAll(e => e.CustomersID == customerId).FirstOrDefault();
                if (customerLocation != null)
                {
                    address_id = customerLocation.CustomerLocationID;
                }
            }

            var CheckPromo = new PromoCodeViewModelApi_new();
            CheckPromo.listvendor = new List<listvendor>();
            CheckPromo.price = Math.Round(total, 2);
            CheckPromo.discount = 0;
            CheckPromo.promoCodeID = 0;
            var vatpercent = _blConfiguration.GetDeliveryPriceVatPercent();
            CheckPromo.total = total;
            CheckPromo.message = "";
            CheckPromo.status = true;
            CheckPromo.vat = vatpercent;

            bool isCartMasterChanged = false;
            foreach (var itemlist_vendorIds in list_vendor)
            {
                decimal vendordeliveryPrice = 0;
                decimal vendorDistanceKM = 0;
                if (!string.IsNullOrWhiteSpace(itemlist_vendorIds.ToString()))
                {
                    var listVendors = Uow.Vendors.GetAll(e => e.VendorsID == itemlist_vendorIds).FirstOrDefault();
                    if (listVendors.DeliveryType == (int)DeliveryTypeEnum.Store) // فى حاله ان المتجر هو اللى هيوصل
                    {
                        CheckPromo.listvendor.Add(new listvendor { vendorId = itemlist_vendorIds, deliveryPrice = (decimal)listVendors.DeliveryPrice, distanceKM = 0 });
                        CheckPromo.deliveryPrice += (decimal)listVendors.DeliveryPrice;
                        vendordeliveryPrice = (decimal)listVendors.DeliveryPrice;
                        vendorDistanceKM = 0;
                    }
                    else
                    {
                        // ارجاع الكونفجريشن الخاص بالشركه
                        var companyConfigration = Uow.Configuration.GetAll().FirstOrDefault();


                        // الحصول على عنوان العميل المختار من الموبايل
                        var CustomerLocation = Uow.CustomerLocation.GetAll(e => e.CustomerLocationID == address_id).FirstOrDefault();

                        // الحصول على المسافه بين العميل والاسره المنتجة
                        double distanceBTvendorKM = 0;
                        if (CustomerLocation != null)
                        {
                            distanceBTvendorKM = _bLGeneral.GetDistance((double)CustomerLocation.Lat, (double)CustomerLocation.Lng, (double)listVendors.Lat, (double)listVendors.Lng);
                        }

                        // الرجوع بأعلي مسافه لسعر التوصيل المحدد
                        var maxKm = companyConfigration.MinKM;

                        // الرجوع بسعر التوصيل بالقيمه المضافه
                        var deliveryPrice = companyConfigration.DeliveryPrice;

                        // التحقق من سعر التوصيل بالمسافه 
                        if ((decimal)distanceBTvendorKM <= maxKm)
                        {
                            CheckPromo.listvendor.Add(new listvendor { vendorId = itemlist_vendorIds, deliveryPrice = deliveryPrice, distanceKM = Convert.ToDecimal(distanceBTvendorKM.ToString("#.00")) });

                            CheckPromo.deliveryPrice += deliveryPrice;
                            vendordeliveryPrice = deliveryPrice;
                            vendorDistanceKM = Convert.ToDecimal(distanceBTvendorKM.ToString("#.00"));


                        }
                        else
                        {
                            // الرجوع بأعلي مسافه لسعر التوصيل المحدد
                            var priceOverKM = companyConfigration.OverKmFare;

                            // حساب الفارق بين العميل والاسره المنتجه
                            var distanceDifference = (decimal)distanceBTvendorKM - maxKm;

                            // ارجاع اجمالى سعر التوصيل 
                            var totalDeliveryPrice = deliveryPrice + (distanceDifference * priceOverKM);

                            CheckPromo.listvendor.Add(new listvendor { vendorId = itemlist_vendorIds, deliveryPrice = Convert.ToDecimal(totalDeliveryPrice.ToString("#.00")), distanceKM = Convert.ToDecimal(distanceBTvendorKM.ToString("#.00")) });

                            CheckPromo.deliveryPrice += totalDeliveryPrice;
                            vendordeliveryPrice = totalDeliveryPrice;
                            vendorDistanceKM = Convert.ToDecimal(distanceBTvendorKM.ToString("#.00"));


                        }
                    }

                    var cartmasterdata = Uow.CartDetails.GetAll(e => e.CartMasterID == _sessionID && e.Product.Vendors.VendorsID == itemlist_vendorIds);
                    foreach (var item in cartmasterdata)
                    {
                        item.distanceKM = vendorDistanceKM;
                        item.deliveryprice = vendordeliveryPrice;
                        Uow.CartDetails.Update(item);
                    }

                    isCartMasterChanged = true;
                }
            }
            if (isCartMasterChanged)
                Uow.Commit();

            CheckPromo.totalWithDelivery = CheckPromo.total + CheckPromo.deliveryPrice;

            //CheckPromo.messageAr = "";
            if (!string.IsNullOrWhiteSpace(code))
            {
                var promoData = Uow.PromoCode.GetAll(p => !p.IsDeleted && p.Code == code).FirstOrDefault();
                if (promoData != null)
                {
                    var DateCheck = Uow.PromoCode.GetAll().Where(p => (DateTime.Now >= p.StartDate && DateTime.Now <= p.ExpiryDate) && p.PromoCodeID == promoData.PromoCodeID).FirstOrDefault();
                    if (DateCheck != null)
                    {
                        if (promoData.OrderPromo.Count(e => !e.IsDeleted) < promoData.LimitCount)
                        {
                            var IsForPassenger = Uow.OrderPromo.GetAll(e => e.Orders.CustomersID == customerId && e.PromoCodeID == promoData.PromoCodeID && e.Orders.OrderStatusID != (int)OrderStatusEnum.Drafts, "Orders").Any();
                            if (IsForPassenger)
                            {
                                CheckPromo.message = accLanguage == "ar" ? "رمز ترويجي مستخدم من قبل" : "Promo Code Is Already Used";
                                CheckPromo.status = false;
                            }
                            else
                            {
                                decimal discount = decimal.Parse(promoData.DiscountValue.ToString());
                                if (promoData.DiscountType == (int)DiscountTypeEnum.Percent)
                                {
                                    discount = Math.Round((CheckPromo.totalWithDelivery * decimal.Parse(promoData.DiscountValue.ToString())) / 100, 2);
                                }
                                total = CheckPromo.total - discount;
                                CheckPromo.promoCodeID = promoData.PromoCodeID;
                                CheckPromo.discount = Math.Round(discount, 2);
                                CheckPromo.total = Math.Round(total, 2);
                                CheckPromo.totalWithDelivery = CheckPromo.totalWithDelivery - discount;
                                CheckPromo.message = accLanguage == "ar" ? "رمز ترويجي صالح" : "Valid promotional code";
                                CheckPromo.status = true;
                            }
                        }
                        else
                        {
                            CheckPromo.message = accLanguage == "ar" ? "تم انتهاء العدد الخاص بالرمز الترويجي" : "Maxumim Limt Count";
                            CheckPromo.status = false;
                        }
                    }
                    else
                    {
                        CheckPromo.message = accLanguage == "ar" ? "تم انتهاء مده الرمز الترويجي" : "Promo Code is Expired";
                        CheckPromo.status = false;
                    }
                }
                else
                {
                    CheckPromo.message = accLanguage == "ar" ? "رمز ترويجي غير صالح" : "Not Valid promotional code";
                    CheckPromo.status = false;
                }
            }
            CheckPromo.deliveryPrice = Convert.ToDecimal(CheckPromo.deliveryPrice.ToString("#.00"));
            CheckPromo.totalWithDelivery = Convert.ToDecimal(CheckPromo.totalWithDelivery.ToString("#.00"));
            CheckPromo.vatValue = Math.Round(CheckPromo.totalWithDelivery - (CheckPromo.totalWithDelivery / (1 + (_blConfiguration.GetDeliveryPriceVatPercent() / 100))), 2);
            CheckPromo.vatTotal = Math.Round(CheckPromo.totalWithDelivery - (CheckPromo.totalWithDelivery / (1 + (_blConfiguration.GetDeliveryPriceVatPercent() / 100))), 2);
            return CheckPromo;
        }
        public PromoCodeViewModelApi_new CheckPromoCodeValid_WebByOrder(string code, int customerId, decimal total, string accLanguage, List<int> list_vendor, int address_id, int OrdersID)
        {
            if (address_id == 0)
            {
                var customerLocation = Uow.CustomerLocation.GetAll(e => e.CustomersID == customerId).FirstOrDefault();
                if (customerLocation != null)
                {
                    address_id = customerLocation.CustomerLocationID;
                }
            }

            var CheckPromo = new PromoCodeViewModelApi_new();
            CheckPromo.listvendor = new List<listvendor>();
            CheckPromo.price = Math.Round(total, 2);
            CheckPromo.discount = 0;
            CheckPromo.promoCodeID = 0;
            var vatpercent = _blConfiguration.GetDeliveryPriceVatPercent();
            CheckPromo.total = total / (1 + (vatpercent / 100));
            var vatvalue = (vatpercent * CheckPromo.total) / 100;
            CheckPromo.total = Math.Round(CheckPromo.total + vatvalue, 2);
            CheckPromo.message = "";
            CheckPromo.status = true;
            CheckPromo.vatValue = vatvalue;
            CheckPromo.vat = vatpercent;
            bool isCartMasterChanged = false;
            foreach (var itemlist_vendorIds in list_vendor)
            {
                decimal vendordeliveryPrice = 0;
                decimal vendorDistanceKM = 0;
                if (!string.IsNullOrWhiteSpace(itemlist_vendorIds.ToString()))
                {
                    var listVendors = Uow.Vendors.GetAll(e => e.VendorsID == itemlist_vendorIds).FirstOrDefault();
                    if (listVendors.DeliveryType == (int)DeliveryTypeEnum.Store) // فى حاله ان المتجر هو اللى هيوصل
                    {
                        CheckPromo.listvendor.Add(new listvendor { vendorId = itemlist_vendorIds, deliveryPrice = (decimal)listVendors.DeliveryPrice, distanceKM = 0 });
                        CheckPromo.deliveryPrice += (decimal)listVendors.DeliveryPrice;
                        vendordeliveryPrice = (decimal)listVendors.DeliveryPrice;
                        vendorDistanceKM = 0;
                    }
                    else
                    {
                        // ارجاع الكونفجريشن الخاص بالشركه
                        var companyConfigration = Uow.Configuration.GetAll().FirstOrDefault();


                        // الحصول على عنوان العميل المختار من الموبايل
                        var CustomerLocation = Uow.CustomerLocation.GetAll(e => e.CustomerLocationID == address_id).FirstOrDefault();

                        // الحصول على المسافه بين العميل والاسره المنتجة
                        double distanceBTvendorKM = 0;
                        if (CustomerLocation != null)
                        {
                            distanceBTvendorKM = _bLGeneral.GetDistance((double)CustomerLocation.Lat, (double)CustomerLocation.Lng, (double)listVendors.Lat, (double)listVendors.Lng);
                        }

                        // الرجوع بأعلي مسافه لسعر التوصيل المحدد
                        var maxKm = companyConfigration.MinKM;

                        // الرجوع بسعر التوصيل بالقيمه المضافه
                        var deliveryPrice = companyConfigration.DeliveryPrice;

                        // التحقق من سعر التوصيل بالمسافه 
                        if ((decimal)distanceBTvendorKM <= maxKm)
                        {
                            CheckPromo.listvendor.Add(new listvendor { vendorId = itemlist_vendorIds, deliveryPrice = deliveryPrice, distanceKM = Convert.ToDecimal(distanceBTvendorKM.ToString("#.00")) });

                            CheckPromo.deliveryPrice += deliveryPrice;
                            vendordeliveryPrice = deliveryPrice;
                            vendorDistanceKM = Convert.ToDecimal(distanceBTvendorKM.ToString("#.00"));


                        }
                        else
                        {
                            // الرجوع بأعلي مسافه لسعر التوصيل المحدد
                            var priceOverKM = companyConfigration.OverKmFare;

                            // حساب الفارق بين العميل والاسره المنتجه
                            var distanceDifference = (decimal)distanceBTvendorKM - maxKm;

                            // ارجاع اجمالى سعر التوصيل 
                            var totalDeliveryPrice = deliveryPrice + (distanceDifference * priceOverKM);

                            CheckPromo.listvendor.Add(new listvendor { vendorId = itemlist_vendorIds, deliveryPrice = Convert.ToDecimal(totalDeliveryPrice.ToString("#.00")), distanceKM = Convert.ToDecimal(distanceBTvendorKM.ToString("#.00")) });

                            CheckPromo.deliveryPrice += totalDeliveryPrice;
                            vendordeliveryPrice = totalDeliveryPrice;
                            vendorDistanceKM = Convert.ToDecimal(distanceBTvendorKM.ToString("#.00"));


                        }
                    }

                    var cartmasterdata = Uow.OrderVendor.GetAll(e => e.OrdersID == OrdersID && e.VendorsID == itemlist_vendorIds && e.OrderStatusID == (int)OrderStatusEnum.Pending
                    && e.IsIncreaseQuantity == true);
                    if (cartmasterdata.Any())
                    {
                        foreach (var item in cartmasterdata)
                        {
                            item.DistanceKM = vendorDistanceKM;
                            item.DeliveryPrice = vendordeliveryPrice;
                            Uow.OrderVendor.Update(item);
                            isCartMasterChanged = true;
                        }

                    }
                }
            }
            if (isCartMasterChanged)
                Uow.Commit();

            CheckPromo.totalWithDelivery = CheckPromo.total + CheckPromo.deliveryPrice;

            //CheckPromo.messageAr = "";
            if (!string.IsNullOrWhiteSpace(code))
            {
                var promoData = Uow.PromoCode.GetAll(p => !p.IsDeleted && p.Code == code).FirstOrDefault();
                if (promoData != null)
                {
                    var DateCheck = Uow.PromoCode.GetAll().Where(p => (DateTime.Now >= p.StartDate && DateTime.Now <= p.ExpiryDate) && p.PromoCodeID == promoData.PromoCodeID).FirstOrDefault();
                    if (DateCheck != null)
                    {
                        if (promoData.OrderPromo.Count(e => !e.IsDeleted) < promoData.LimitCount)
                        {
                            var IsForPassenger = Uow.OrderPromo.GetAll(e => e.Orders.CustomersID == customerId && e.PromoCodeID == promoData.PromoCodeID && e.Orders.OrderStatusID != (int)OrderStatusEnum.Drafts, "Orders").Any();
                            if (IsForPassenger)
                            {
                                CheckPromo.message = accLanguage == "ar" ? "رمز ترويجي مستخدم من قبل" : "Promo Code Is Already Used";
                                CheckPromo.status = false;
                            }
                            else
                            {
                                decimal discount = decimal.Parse(promoData.DiscountValue.ToString());
                                if (promoData.DiscountType == (int)DiscountTypeEnum.Percent)
                                {
                                    discount = Math.Round((CheckPromo.totalWithDelivery * decimal.Parse(promoData.DiscountValue.ToString())) / 100, 2);
                                }
                                total = CheckPromo.total - discount;
                                CheckPromo.promoCodeID = promoData.PromoCodeID;
                                CheckPromo.discount = Math.Round(discount, 2);
                                CheckPromo.total = Math.Round(total, 2);
                                CheckPromo.totalWithDelivery = CheckPromo.totalWithDelivery - discount;
                                CheckPromo.message = accLanguage == "ar" ? "رمز ترويجي صالح" : "Valid promotional code";
                                CheckPromo.status = true;
                            }
                        }
                        else
                        {
                            CheckPromo.message = accLanguage == "ar" ? "تم انتهاء العدد الخاص بالرمز الترويجي" : "Maxumim Limt Count";
                            CheckPromo.status = false;
                        }
                    }
                    else
                    {
                        CheckPromo.message = accLanguage == "ar" ? "تم انتهاء مده الرمز الترويجي" : "Promo Code is Expired";
                        CheckPromo.status = false;
                    }
                }
                else
                {
                    CheckPromo.message = accLanguage == "ar" ? "رمز ترويجي غير صالح" : "Not Valid promotional code";
                    CheckPromo.status = false;
                }
            }


            CheckPromo.deliveryPrice = Convert.ToDecimal(CheckPromo.deliveryPrice.ToString("#.00"));
            CheckPromo.totalWithDelivery = Convert.ToDecimal(CheckPromo.totalWithDelivery.ToString("#.00"));

            CheckPromo.vatValue = Math.Round(CheckPromo.totalWithDelivery - (CheckPromo.totalWithDelivery / (1 + (_blConfiguration.GetDeliveryPriceVatPercent() / 100))), 2);
            //(vatpercent * CheckPromo.totalWithDelivery) / 100;
            CheckPromo.vatTotal = Math.Round(CheckPromo.totalWithDelivery - (CheckPromo.totalWithDelivery / (1 + (_blConfiguration.GetDeliveryPriceVatPercent() / 100))), 2);
            // (vatpercent * CheckPromo.totalWithDelivery) / 100;
            return CheckPromo;
        }
        public PromoCodeViewModelApi GetVatValue_Web(decimal total)
        {
            var CheckPromo = new PromoCodeViewModelApi();
            CheckPromo.deliveryPrice = Uow.Configuration.GetAll(s => !s.IsDeleted).FirstOrDefault() != null ? Uow.Configuration.GetAll(s => !s.IsDeleted).FirstOrDefault().DeliveryPrice : 0;
            CheckPromo.deliveryPrice = CheckPromo.deliveryPrice * 1;
            var vatpercent = _blConfiguration.GetDeliveryPriceVatPercent();
            CheckPromo.total = total / (1 + (vatpercent / 100));
            var vatvalue = (vatpercent * CheckPromo.total) / 100;
            CheckPromo.total = Math.Round(CheckPromo.total + vatvalue, 2);
            CheckPromo.message = "";
            CheckPromo.status = true;
            CheckPromo.vatValue = vatvalue;
            CheckPromo.vat = vatpercent;
            CheckPromo.totalWithDelivery = CheckPromo.total + CheckPromo.deliveryPrice;
            return CheckPromo;
        }
        public int? GetPromoCodeId(string code)
        {

            if (!string.IsNullOrWhiteSpace(code))
            {
                var promoData = Uow.PromoCode.GetAll(p => !p.IsDeleted && p.Code == code).FirstOrDefault();
                if (promoData != null)
                {
                    return promoData.PromoCodeID;
                }
                else
                {
                    return null;

                }

            }
            return null;
        }
        public AddOrderResponse ValidtaionOrder(AddOrderViewModelApiNew model, int UserId, int CustomersID, string FireBaseKey, string addedFrom)
        {
            var res = new AddOrderResponse();
            try
            {
                if (model.vendorOrder.Count() > 0)
                {

                    foreach (var item in model.vendorOrder)
                    {
                        var VendCheck = Uow.Vendors.GetAll(s => s.VendorsID == item.vendorId).FirstOrDefault();

                        if (item.products.Count() > 0)
                        {
                            foreach (var prod in item.products)
                            {
                                var ProdData = Uow.Product.GetAll(s => s.ProductID == prod.productId).FirstOrDefault();
                                if (ProdData != null)
                                {
                                    if (ProdData.IsLimited)
                                    {
                                        if (ProdData.DailyQuantity == 0)
                                        {
                                            res.orderId = 0;
                                            res.message = "عفوا لقد نفذت الكمية اليومية المتاحة من المنتج " + ProdData.NameAr + "";
                                            return res;
                                        }
                                        else if (ProdData.DailyQuantity < prod.quantity)
                                        {
                                            res.orderId = 0;
                                            res.message = "الكمية المتوفرة من منتج " + ProdData.NameAr + " : " + ProdData.DailyQuantity + "";
                                            return res;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            res.orderId = 0;
                            res.message = "لا يوجد طلبات مختارة من المتجر " + VendCheck.StoreNameAr + "";
                            return res;
                        }

                        #region Working_Times
                        if (VendCheck.IsDaysWork)//يجب ان يقوم المتجر بتحديد اوقات العمل
                        {
                            var date = DateTime.Now; //اذا كان الطلب فوري
                            if (model.orderTypeId == (int)OrderTypeEnum.Schedule) //اذا كان الطلب مجدول
                            {
                                string[] DateFormat = { "yyyy-MM-dd", "yyyy-MM-dd HH:mm", "yyyy-MM-dd HH:m", "yyyy-MM-dd H:mm", "yyyy-MM-dd H:m" };
                                DateTime? ddScheduleDate = !string.IsNullOrWhiteSpace(model.scheduleDate) ? DateTime.ParseExact(model.scheduleDate, DateFormat, System.Globalization.CultureInfo.InvariantCulture) : null;
                                DateTime? ScheddduleTime = !string.IsNullOrWhiteSpace(model.scheduleDate) ? DateTime.ParseExact(model.scheduleDate, DateFormat, System.Globalization.CultureInfo.InvariantCulture) : null;
                                date = ddScheduleDate.Value.Date;
                                var time = ScheddduleTime.Value.TimeOfDay;
                                date = date.Add(time);
                            }

                            var DaysWorking = VendCheck.DaysWork.Split(","); // ايام العمل الرسمية
                            if (DaysWorking.Any(s => int.Parse(s) == (int)date.DayOfWeek)) //اذا كان الطلب في يوم من ايام العمل الرسمية
                            {
                                if (VendCheck.WorkFrom.HasValue && VendCheck.WorkTo.HasValue)// اذا تم تحديد وقت معين للعمل في ايام العمل الرسمية
                                {
                                    if (VendCheck.WorkFrom.Value.TimeOfDay < VendCheck.WorkTo.Value.TimeOfDay)
                                    {
                                        if (VendCheck.WorkFrom.Value.TimeOfDay < date.TimeOfDay && VendCheck.WorkTo.Value.TimeOfDay > date.TimeOfDay)
                                        {
                                            res.message = "المتجر مفتوح";
                                        }
                                        else
                                        {
                                            res.orderId = 0;
                                            res.message = "عفوا المتجر " + VendCheck.StoreNameAr + " تم انتهاء وقت العمل الخاص به اليوم";
                                            return res;
                                        }
                                    }
                                    else
                                    {
                                        if (VendCheck.WorkFrom.Value.TimeOfDay < date.TimeOfDay || VendCheck.WorkTo.Value.TimeOfDay > date.TimeOfDay)
                                        {
                                            res.message = "المتجر مفتوح";
                                        }
                                        else
                                        {
                                            res.orderId = 0;
                                            res.message = "عفوا المتجر " + VendCheck.StoreNameAr + " تم انتهاء وقت العمل الخاص به اليوم";
                                            return res;
                                        }
                                    }
                                }
                            }
                            else if (VendCheck.IsDaysVac) //اذا اضاف المتجر اوقات عمل في ايام الاجازة
                            {
                                var DaysVac = VendCheck.DaysVac.Split(",");//ايام الاجازة التى تم تحديدها
                                if (DaysVac.Any(s => int.Parse(s) == (int)date.DayOfWeek))//اذا تم تحديد اوقات للعمل في يوم الاجازة
                                {
                                    if (VendCheck.VacWorkFrom.HasValue && VendCheck.VacWorkTo.HasValue)//اذا تم تحديد وقت معين للعمل في يوم الاجازة
                                    {
                                        if (VendCheck.VacWorkFrom.Value.TimeOfDay < VendCheck.VacWorkTo.Value.TimeOfDay)
                                        {
                                            if (VendCheck.VacWorkFrom.Value.TimeOfDay < date.TimeOfDay && VendCheck.VacWorkTo.Value.TimeOfDay > date.TimeOfDay)
                                            {
                                                res.message = "المتجر مفتوح";
                                            }
                                            else
                                            {
                                                res.orderId = 0;
                                                res.message = "عفوا المتجر " + VendCheck.StoreNameAr + " تم انتهاء وقت العمل الخاص به اليوم";
                                                return res;
                                            }
                                        }
                                        else
                                        {
                                            if (VendCheck.VacWorkFrom.Value.TimeOfDay < date.TimeOfDay || VendCheck.VacWorkTo.Value.TimeOfDay > date.TimeOfDay)
                                            {
                                                res.message = "المتجر مفتوح";
                                            }
                                            else
                                            {
                                                res.orderId = 0;
                                                res.message = "عفوا المتجر " + VendCheck.StoreNameAr + " تم انتهاء وقت العمل الخاص به اليوم";
                                                return res;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    res.orderId = 0;
                                    res.message = "عفوا المتجر " + VendCheck.StoreNameAr + " غير متاح اليوم";
                                    return res;
                                }
                            }
                            else
                            {
                                res.orderId = 0;
                                res.message = "عفوا المتجر " + VendCheck.StoreNameAr + " غير متاح لتجهيز طلباتكم الان";
                                return res;
                            }
                        }
                        else
                        {
                            res.orderId = 0;
                            res.message = "لا يمكن الطلب من المتجر " + VendCheck.StoreNameAr + " لأنه لم يحدد اوقات العمل";
                            return res;
                        }
                        #endregion

                    }
                    res.message = "true";
                    res.orderId = 1;
                }
                else
                {
                    res.orderId = 0;
                    res.message = "لا يوجد طلبات من اى مزود خدمة";
                }
            }
            catch (Exception ex)
            {
                res.orderId = 0;
                res.message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }
            return res;
        }
        public AddOrderResponse ValidationIncreaseOrder(AddOrderViewModelApiNew model)
        {
            var res = new AddOrderResponse();
            try
            {
                if (model.vendorOrder.Count() > 0)
                {

                    foreach (var item in model.vendorOrder)
                    {
                        var VendCheck = Uow.Vendors.GetAll(s => s.VendorsID == item.vendorId).FirstOrDefault();

                        if (item.products.Count() == 0)
                        {
                            res.orderId = 0;
                            res.message = "لا يوجد طلبات مختارة من المتجر " + VendCheck.StoreNameAr + "";
                            return res;
                        }

                        #region Working_Times
                        if (VendCheck.IsDaysWork)//يجب ان يقوم المتجر بتحديد اوقات العمل
                        {
                            var date = DateTime.Now; //اذا كان الطلب فوري
                            if (model.orderTypeId == (int)OrderTypeEnum.Schedule) //اذا كان الطلب مجدول
                            {
                                string[] DateFormat = { "yyyy-MM-dd", "yyyy-MM-dd HH:mm", "yyyy-MM-dd HH:m", "yyyy-MM-dd H:mm", "yyyy-MM-dd H:m" };
                                DateTime? ddScheduleDate = !string.IsNullOrWhiteSpace(model.scheduleDate) ? DateTime.ParseExact(model.scheduleDate, DateFormat, System.Globalization.CultureInfo.InvariantCulture) : null;
                                DateTime? ScheddduleTime = !string.IsNullOrWhiteSpace(model.scheduleDate) ? DateTime.ParseExact(model.scheduleDate, DateFormat, System.Globalization.CultureInfo.InvariantCulture) : null;
                                date = ddScheduleDate.Value.Date;
                                var time = ScheddduleTime.Value.TimeOfDay;
                                date = date.Add(time);
                            }

                            var DaysWorking = VendCheck.DaysWork.Split(","); // ايام العمل الرسمية
                            if (DaysWorking.Any(s => int.Parse(s) == (int)date.DayOfWeek)) //اذا كان الطلب في يوم من ايام العمل الرسمية
                            {
                                if (VendCheck.WorkFrom.HasValue && VendCheck.WorkTo.HasValue)// اذا تم تحديد وقت معين للعمل في ايام العمل الرسمية
                                {
                                    if (VendCheck.WorkFrom.Value.TimeOfDay < VendCheck.WorkTo.Value.TimeOfDay)
                                    {
                                        if (VendCheck.WorkFrom.Value.TimeOfDay < date.TimeOfDay && VendCheck.WorkTo.Value.TimeOfDay > date.TimeOfDay)
                                        {
                                            res.message = "المتجر مفتوح";
                                        }
                                        else
                                        {
                                            res.orderId = 0;
                                            res.message = "عفوا المتجر " + VendCheck.StoreNameAr + " تم انتهاء وقت العمل الخاص به اليوم";
                                            return res;
                                        }
                                    }
                                    else
                                    {
                                        if (VendCheck.WorkFrom.Value.TimeOfDay < date.TimeOfDay || VendCheck.WorkTo.Value.TimeOfDay > date.TimeOfDay)
                                        {
                                            res.message = "المتجر مفتوح";
                                        }
                                        else
                                        {
                                            res.orderId = 0;
                                            res.message = "عفوا المتجر " + VendCheck.StoreNameAr + " تم انتهاء وقت العمل الخاص به اليوم";
                                            return res;
                                        }
                                    }
                                }
                            }
                            else if (VendCheck.IsDaysVac) //اذا اضاف المتجر اوقات عمل في ايام الاجازة
                            {
                                var DaysVac = VendCheck.DaysVac.Split(",");//ايام الاجازة التى تم تحديدها
                                if (DaysVac.Any(s => int.Parse(s) == (int)date.DayOfWeek))//اذا تم تحديد اوقات للعمل في يوم الاجازة
                                {
                                    if (VendCheck.VacWorkFrom.HasValue && VendCheck.VacWorkTo.HasValue)//اذا تم تحديد وقت معين للعمل في يوم الاجازة
                                    {
                                        if (VendCheck.VacWorkFrom.Value.TimeOfDay < VendCheck.VacWorkTo.Value.TimeOfDay)
                                        {
                                            if (VendCheck.VacWorkFrom.Value.TimeOfDay < date.TimeOfDay && VendCheck.VacWorkTo.Value.TimeOfDay > date.TimeOfDay)
                                            {
                                                res.message = "المتجر مفتوح";
                                            }
                                            else
                                            {
                                                res.orderId = 0;
                                                res.message = "عفوا المتجر " + VendCheck.StoreNameAr + " تم انتهاء وقت العمل الخاص به اليوم";
                                                return res;
                                            }
                                        }
                                        else
                                        {
                                            if (VendCheck.VacWorkFrom.Value.TimeOfDay < date.TimeOfDay || VendCheck.VacWorkTo.Value.TimeOfDay > date.TimeOfDay)
                                            {
                                                res.message = "المتجر مفتوح";
                                            }
                                            else
                                            {
                                                res.orderId = 0;
                                                res.message = "عفوا المتجر " + VendCheck.StoreNameAr + " تم انتهاء وقت العمل الخاص به اليوم";
                                                return res;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    res.orderId = 0;
                                    res.message = "عفوا المتجر " + VendCheck.StoreNameAr + " غير متاح اليوم";
                                    return res;
                                }
                            }
                            else
                            {
                                res.orderId = 0;
                                res.message = "عفوا المتجر " + VendCheck.StoreNameAr + " غير متاح لتجهيز طلباتكم الان";
                                return res;
                            }
                        }
                        else
                        {
                            res.orderId = 0;
                            res.message = "لا يمكن الطلب من المتجر " + VendCheck.StoreNameAr + " لأنه لم يحدد اوقات العمل";
                            return res;
                        }
                        #endregion

                    }
                    res.message = "true";
                    res.orderId = 1;
                }
                else
                {
                    res.orderId = 0;
                    res.message = "لا يوجد طلبات من اى مزود خدمة";
                }
            }
            catch (Exception ex)
            {
                res.orderId = 0;
                res.message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }
            return res;
        }
        #endregion
        #region Add new order
        public string CancelOrder(CancelOrderViewModelApi model, int UserId, string FireBaseKey)
        {
            try
            {
                var OrderData = Uow.OrderVendor.GetAll(s => s.OrderVendorID == model.orderId, "Orders.Customers,OrderStatus,Vendors,OrderItems").FirstOrDefault();
                OrderData.OrderStatusID = (int)OrderStatusEnum.Cancel;
                OrderData.UpdateDate = _bLGeneral.GetDateTimeNow();
                OrderData.UserId = UserId;
                OrderData.InvoiceTypeId = (int)OrderInvoiceTypeEnum.Non_Invoice;
                OrderData.OrderHistory.Add(new OrderHistory()
                {
                    OrderStatusID = (int)OrderStatusEnum.Cancel,
                    CancelReason = model.reason,
                    CreateDate = _bLGeneral.GetDateTimeNow(),
                    IsDeleted = false,
                    IsEnable = true,
                    Lat = 0,
                    Lng = 0,
                    OrderHistoryGuid = Guid.NewGuid(),
                    UserId = UserId
                });
                var OrderMasterData = Uow.Orders.GetAll(s => s.OrdersID == OrderData.OrdersID, "Customers,OrderStatus").FirstOrDefault();
                OrderMasterData.OrderStatusID = (int)OrderStatusEnum.Cancel;
                OrderMasterData.UpdateDate = _bLGeneral.GetDateTimeNow();
                OrderMasterData.UserId = UserId;
                Uow.Orders.Update(OrderMasterData);
                #region Daily_Quantity
                //if (_bLGeneral.GetDateTimeNow().Date == OrderData.CreateDate.Date) //لو الغاء الطلب في نفس اليوم اللى تم فيه انشاء الطلب
                // {
                foreach (var item in OrderData.OrderItems)
                {
                    var ProdData = Uow.Product.GetAll(s => s.ProductID == item.ProductID).FirstOrDefault();
                    if (ProdData.IsLimited)
                    {
                        ProdData.DailyQuantity = ProdData.DailyQuantity + item.Quantity;
                        Uow.Product.Update(ProdData);
                    }
                }
                //}
                #endregion
                OrderData.Notification.Add(new Notification()
                {
                    CreateDate = _bLGeneral.GetDateTimeNow(),
                    TitleAR = "تم الغاء الطلب رقم " + OrderData.OrderVendorID + " من خلال " + OrderMasterData.Customers.FirstName + "",
                    TitleEN = "your order has been canceled from " + OrderData.Orders.Customers.FirstName + "",
                    DescAR = "تم الغاء الطلب رقم " + OrderData.OrderVendorID + " من خلال " + OrderMasterData.Customers.FirstName + " في تاريخ " + _bLGeneral.GetDateTimeNow() + "",
                    DescEN = "order No " + OrderData.OrderVendorID + " has been canceled from " + OrderMasterData.Customers.FirstName + " on date " + _bLGeneral.GetDateTimeNow() + "",
                    IsDeleted = false,
                    IsEnable = true,
                    IsSeen = false,
                    NotificationGuid = Guid.NewGuid(),
                    NotificationTypeID = (int)NotificationTypeEnum.Order,
                    UserId = UserId,
                    NotificationDate = _bLGeneral.GetDateTimeNow(),
                    OrderVendorID = OrderData.OrderVendorID,
                    VendorsID = OrderData.VendorsID,
                });
                Uow.OrderVendor.Update(OrderData);

                _BlCustomerBalance.InsertBalanceWithOutCommit((int)TypeOperationEnum.CR, OrderData.Total, (int)TransactionTypeEnum.Pay_Order, OrderMasterData.CustomersID
                    , UserId, OrderData.OrderVendorID, _bLGeneral.GetDateTimeNow(), "", "", 0);
                Uow.Commit();


                var _PushListCusotmer = new PushList()
                {
                    orderId = OrderData.OrdersID,
                    lat = 0,
                    lng = 0,
                    trackNo = OrderData.OrdersID.ToString(),
                    profit = "",
                    cusotmerAddress = "",
                    vendorAddress = "",
                    vendorLogo = "",
                    vendorName = "",
                    body = "تم الغاء طلبك من متجر " + OrderData.Vendors.StoreNameAr + " اضغط هنا لتفاصيل الطلب",//For IOS
                    sound = "default", //For IOS
                    title = "الغاء الطلب",
                    content_available = "true", //For Android
                    priority = "high", //For Android,
                    serverKey = FireBaseKey,
                    estimation = 20,
                    pushType = (int)PushTypeEnum.Order
                };
                var UserData = _blTokens.GetMobileDataByUserID(UserId);
                if (UserData != null)
                {
                    _bLGeneral.SendPush(UserData.TokenVal, UserData.DeviceType, _PushListCusotmer);
                }
                var _PushListVendor = new PushList()
                {
                    orderId = OrderData.OrdersID,
                    lat = 0,
                    lng = 0,
                    trackNo = OrderData.OrdersID.ToString(),
                    profit = "",
                    cusotmerAddress = "",
                    vendorAddress = "",
                    vendorLogo = "",
                    vendorName = "",
                    body = "الغاء الطلب من الزبون " + OrderData.Orders.Customers.FirstName + " اضغط هنا لتفاصيل الطلب",//For IOS
                    sound = "custom_sound.wav", //For IOS
                    title = "الغاء الطلب",
                    content_available = "true", //For Android
                    priority = "high", //For Android,
                    serverKey = FireBaseKey,
                    estimation = 20,
                    pushType = (int)PushTypeEnum.Order
                };
                var UserDataVendor = _blTokens.GetMobileDataByUserID(OrderData.Vendors.UserId);
                if (UserDataVendor != null)
                {
                    _bLGeneral.SendPush(UserDataVendor.TokenVal, UserDataVendor.DeviceType, _PushListVendor);
                }

                return "true";
            }
            catch (Exception ex)
            {
                return ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }
        }
        public class AddOrderResponse
        {
            public int orderId { get; set; }
            public string message { get; set; }
            public decimal totalOrderValue { get; set; }
            public List<int> listVendorsID { get; set; }
        }
        public AddOrderResponse AddOrder(AddOrderViewModelApi model, int UserId, int CustomersID, string FireBaseKey, string addedFrom)
        {
            var res = new AddOrderResponse();
            decimal deliveryvatper = 0;
            decimal deliveryvatval = 0;
            try
            {
                var VatSettingPercent = _blConfiguration.GetDeliveryPriceVatPercent();

                var DeliveryVatSettingPercent = _blConfiguration.GetDeliveryPriceVatPercent();


                if (model.vendorOrder.Count() > 0)
                {

                    decimal TotalPrice = 0;
                    decimal discount = 0;
                    decimal DeliveryPricePerOne = Uow.Configuration.GetAll(s => !s.IsDeleted).FirstOrDefault() != null ? Uow.Configuration.GetAll(s => !s.IsDeleted).FirstOrDefault().DeliveryPrice : 0;
                    foreach (var item in model.vendorOrder)
                    {
                        if (item.products.Count() > 0)
                        {
                            foreach (var prod in item.products)
                            {
                                var ProdData = Uow.Product.GetAll(s => s.ProductID == prod.productId).FirstOrDefault();
                                if (ProdData != null)
                                {
                                    TotalPrice = TotalPrice + ((ProdData.Price - _bLGeneral.CalcProductNet(ProdData.Discount, ProdData.StartDiscountDate, ProdData.EndDiscountDate, ProdData.Price)) * prod.quantity);
                                }
                            }
                        }
                        else
                        {
                            var vendor = Uow.Vendors.GetAll(s => s.VendorsID == item.vendorId).FirstOrDefault();
                            res.orderId = 0;
                            res.message = "لا يوجد طلبات مختارة من المتجر " + vendor.StoreNameAr + "";
                            return res;
                        }
                    }
                    if (model.promoCodeID != null)
                    {
                        var PromoData = Uow.PromoCode.GetAll(s => s.PromoCodeID == model.promoCodeID).FirstOrDefault();
                        if (PromoData != null)
                        {
                            var Pro = CheckPromoCodeValid(PromoData.Code, CustomersID, TotalPrice, "", 1);
                            discount = Pro.discount;
                        }
                    }
                    var totmaster = TotalPrice;
                    var vatvalue = (VatSettingPercent * totmaster) / 100;
                    totmaster = totmaster + vatvalue;
                    totmaster = totmaster - discount;
                    if (model.paymentTypeId == (int)PaymentTypeEnum.Wallet)
                    {
                        var CustomerBalance = _BlCustomerBalance.GetLastBlance(CustomersID);
                        if (totmaster > CustomerBalance)
                        {
                            res.orderId = 0;
                            res.message = "عفوا المبلغ المتواجد في المحفظة اقل من اجمالى الطلبات";
                            return res;
                        }
                    }
                    var OrderData = new Orders()
                    {
                        CreateDate = _bLGeneral.GetDateTimeNow(),
                        CustomerLocationID = model.addressID,
                        CustomersID = CustomersID,
                        DeliveryPrice = (DeliveryPricePerOne * model.vendorOrder.Count()),
                        Discount = discount,
                        IsDeleted = false,
                        IsEnable = true,
                        Notes = string.Empty,
                        OrdersGuid = Guid.NewGuid(),
                        OrderStatusID = (int)OrderStatusEnum.Create,
                        PaymentTypeId = model.paymentTypeId,
                        Price = TotalPrice,
                        Total = totmaster,
                        Vat = vatvalue,
                        UserId = UserId,
                        PromoCodeID = model.promoCodeID,
                        ReferenceNo = model.customerReference,
                        InvoiceId = model.invoiceId
                    };
                    OrderData = Uow.Orders.Insert(OrderData);
                    Uow.Commit();
                    var ordervendordata = new OrderVendor();
                    if (discount > 0)
                    {
                        var PromoData = Uow.PromoCode.GetAll(s => s.PromoCodeID == model.promoCodeID).FirstOrDefault();
                        if (PromoData != null)
                        {
                            var PromoOrder = new OrderPromo()
                            {
                                CreateDate = _bLGeneral.GetDateTimeNow(),
                                DiscountType = PromoData.DiscountType,
                                DiscountValue = PromoData.DiscountValue,
                                IsDeleted = false,
                                IsEnable = true,
                                OrderPromoGuid = Guid.NewGuid(),
                                PromoCodeID = PromoData.PromoCodeID,
                                OrdersID = OrderData.OrdersID,
                                UserId = UserId,
                            };
                            Uow.OrderPromo.Insert(PromoOrder);
                        }
                    }
                    foreach (var item in model.vendorOrder)
                    {
                        decimal VatProduct = 0;
                        var vaendordata = Uow.Vendors.GetAll(s => s.VendorsID == item.vendorId).FirstOrDefault();
                        decimal PriceOrderVendor = 0;
                        decimal DicOrder = discount > 0 ? (discount / model.vendorOrder.Count()) : 0;
                        var TrackNo = _bLGeneral.KeyGeneratorNumbersOnly(12);
                        while (Uow.OrderVendor.GetAll(s => s.TrackNo == TrackNo).Count() > 0)
                        {
                            TrackNo = _bLGeneral.KeyGeneratorNumbersOnly(12);
                        }
                        foreach (var prod in item.products)
                        {
                            var ProdData = Uow.Product.GetAll(s => s.ProductID == prod.productId).FirstOrDefault();
                            if (ProdData != null)
                            {
                                PriceOrderVendor = PriceOrderVendor + ((ProdData.Price - _bLGeneral.CalcProductNet(ProdData.Discount, ProdData.StartDiscountDate, ProdData.EndDiscountDate, ProdData.Price)) * prod.quantity);
                            }
                        }

                        var PriceOrderVendorss = PriceOrderVendor;
                        decimal vatorder = (PriceOrderVendorss * VatSettingPercent) / 100;
                        PriceOrderVendorss = (PriceOrderVendorss + vatorder) - DicOrder;
                        int CaptainType = (int)CaptainTypeEnum.Home_Made;
                        var pacakedata = Uow.Package.GetAll(s => !s.IsDeleted).FirstOrDefault();
                        if (vaendordata.PackageID.HasValue)
                        {
                            pacakedata = Uow.Package.GetAll(s => !s.IsDeleted && s.PackageID == vaendordata.PackageID.Value).FirstOrDefault();
                        }
                        decimal pacadgeamount = pacakedata.Price;
                        decimal percentvalue = (pacakedata.Percent * PriceOrderVendorss) / 100;
                        var totalpacaamount = pacadgeamount + percentvalue;
                        decimal perhome = totalpacaamount;
                        decimal perstore = PriceOrderVendorss - totalpacaamount;


                        if (Uow.Vendors.GetAll(s => s.VendorsID == item.vendorId && s.DeliveryType == (int)DeliveryTypeEnum.Store).Any())
                        {
                            CaptainType = (int)CaptainTypeEnum.Store;
                            var oldDeliveryPricePerOne = DeliveryPricePerOne;
                            DeliveryPricePerOne = Uow.Vendors.GetAll(s => s.VendorsID == item.vendorId).FirstOrDefault().DeliveryPrice != null ?
                                (decimal)Uow.Vendors.GetAll(s => s.VendorsID == item.vendorId).FirstOrDefault().DeliveryPrice : oldDeliveryPricePerOne;
                            //لو المتجر عنده سواق هيزيد في حقه سعر التوصيل بتاعه
                            perstore = perstore + DeliveryPricePerOne;
                            deliveryvatper = 0;
                            deliveryvatval = 0;
                        }
                        else
                        {
                            perhome = perhome + DeliveryPricePerOne;
                            deliveryvatper = DeliveryVatSettingPercent;
                            deliveryvatval = (DeliveryVatSettingPercent * DeliveryPricePerOne) / 100;
                        }
                        PriceOrderVendorss = PriceOrderVendorss + DeliveryPricePerOne;
                        ordervendordata = new OrderVendor()
                        {
                            VatValue = deliveryvatval + VatProduct,
                            VendorsID = item.vendorId,
                            CreateDate = _bLGeneral.GetDateTimeNow(),
                            DeliveryPrice = DeliveryPricePerOne,
                            Discount = DicOrder,
                            InvoiceImage = string.Empty,
                            IsDeleted = false,
                            IsEnable = true,
                            Notes = item.notes,
                            OrderStatusID = (int)OrderStatusEnum.Create,
                            OrderVendorGuid = Guid.NewGuid(),
                            TrackNo = TrackNo,
                            Price = PriceOrderVendor,
                            Total = PriceOrderVendorss,
                            OrdersID = OrderData.OrdersID,
                            UserId = UserId,
                            CaptainPaidID = (int)PaidStatusEnum.Created,
                            DriverCharge = 0,
                            CaptainTypeId = CaptainType,
                            InvoiceTypeId = (int)OrderInvoiceTypeEnum.Created,
                            PerStore = perstore,
                            PerHome = perhome,
                            PackageAmount = pacadgeamount,
                            PackageID = pacakedata.PackageID,
                            PackagePercent = percentvalue,
                            VatPercent = deliveryvatper,
                            DeliveryVatValue = deliveryvatval,
                            AddedFrom = addedFrom,
                            VatProduct = VatProduct,
                        };

                        Uow.Commit();

                        foreach (var prodiem in item.products)
                        {
                            decimal Dicitem = DicOrder != 0 ? (DicOrder / item.products.Count()) : 0;
                            var ProdData = Uow.Product.GetAll(s => s.ProductID == prodiem.productId).FirstOrDefault();
                            if (ProdData != null)
                            {
                                var vatPriceViewModel = _blConfiguration.GetVatForPrice(ProdData.Price - _bLGeneral.CalcProductNet(ProdData.Discount, ProdData.StartDiscountDate, ProdData.EndDiscountDate, ProdData.Price));
                                VatProduct += (vatPriceViewModel.VatValue * prodiem.quantity);
                                ordervendordata.OrderItems.Add(new OrderItems()
                                {
                                    CreateDate = _bLGeneral.GetDateTimeNow(),
                                    Discount = Dicitem,
                                    IsDeleted = false,
                                    IsEnable = true,
                                    OrderItemsGuid = Guid.NewGuid(),
                                    PriceBeforeVat = ProdData.Price,
                                    VatValue = vatPriceViewModel.VatValue,
                                    Price = vatPriceViewModel.PriceWithVat,
                                    ProdImage = ProdData.Logo,
                                    ProdNameAr = ProdData.NameAr,
                                    ProdNameEn = ProdData.NameEn,
                                    ProductID = ProdData.ProductID,
                                    UserId = UserId,
                                    Quantity = prodiem.quantity,
                                });
                                ProdData.Quantity = ProdData.Quantity - prodiem.quantity;
                                Uow.Product.Update(ProdData);
                            }
                        }
                        ordervendordata.OrderHistory.Add(new OrderHistory()
                        {
                            CreateDate = _bLGeneral.GetDateTimeNow(),
                            UserId = UserId,
                            OrderStatusID = (int)OrderStatusEnum.Create,
                            IsDeleted = false,
                            IsEnable = true,
                            Lat = 0,
                            Lng = 0,
                            OrderHistoryGuid = Guid.NewGuid(),
                        });
                        ordervendordata.Notification.Add(new Notification()
                        {
                            CreateDate = _bLGeneral.GetDateTimeNow(),
                            TitleAR = "تم إرسال الطلب رقم " + ordervendordata.OrderVendorID + " من خلال " + OrderData.Customers.FirstName + "",
                            TitleEN = "your order has been send from " + OrderData.Customers.FirstName + "",
                            DescAR = "تم إرسال الطلب رقم " + ordervendordata.OrderVendorID + " من خلال " + OrderData.Customers.FirstName + " في تاريخ " + _bLGeneral.GetDateTimeNow() + "",
                            DescEN = "order No " + ordervendordata.OrderVendorID + " has been send from " + OrderData.Customers.FirstName + " on date " + _bLGeneral.GetDateTimeNow() + "",
                            IsDeleted = false,
                            IsEnable = true,
                            IsSeen = false,
                            NotificationGuid = Guid.NewGuid(),
                            NotificationTypeID = (int)NotificationTypeEnum.Order,
                            UserId = UserId,
                            NotificationDate = _bLGeneral.GetDateTimeNow(),
                            OrderVendorID = ordervendordata.OrderVendorID,
                            VendorsID = ordervendordata.VendorsID
                        });
                        Uow.OrderVendor.Insert(ordervendordata);
                    }
                    Uow.Commit();
                    if (model.paymentTypeId == (int)PaymentTypeEnum.Wallet)
                    {
                        decimal currentAmountwallet = 0;

                        foreach (var item in OrderData.OrderVendor)
                        {
                            var amountwallet = item.Total;

                            _BlCustomerBalance.InsertBalanceWithOutCommit((int)TypeOperationEnum.DR, amountwallet, (int)TransactionTypeEnum.Pay_Order, CustomersID, UserId, item.OrderVendorID, _bLGeneral.GetDateTimeNow(), "", "", currentAmountwallet);

                            currentAmountwallet += amountwallet;

                        }
                    }
                    Uow.Commit();
                    foreach (var item in OrderData.OrderVendor)
                    {
                        #region Push
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
                            body = "طلب جديد من الزبون " + OrderData.Customers.FirstName + " فضلا قم بتجهيز الطلب الان",//For IOS
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

                    res.message = "true";
                    res.orderId = OrderData.OrdersID;
                    SendEmailAfterSendOrder(OrderData.OrdersID);
                }
                else
                {
                    res.orderId = 0;
                    res.message = "لا يوجد طلبات من اى مزود خدمة";
                }
            }
            catch (Exception ex)
            {
                res.orderId = 0;
                res.message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }
            return res;
        }
        public void SendEmailAfterSendOrder(int OrdersID)
        {
            var orderdata = Uow.Orders.GetAll(e => e.OrdersID == OrdersID, "OrderVendor,Customers").FirstOrDefault();
            if (orderdata != null)
            {
                string Message = "<div style='text-align:right; float: right;font-size:14px;direction: rtl;'>";
                Message += "<b> رقم الطلب: </b>" + string.Join(" - ", orderdata.OrderVendor.Select(m => "(" + m.Vendors.VendorsID + "#)" + " " + m.Vendors.StoreNameAr).ToArray());
                Message += "<br />";
                Message += "<b> تاريخ الطلب: </b>" + _bLGeneral.GetDateTimeNow().ToString("yyyy-MM-dd hh:mm tt");
                Message += "<br />";
                Message += "<b> اسم العميل: </b>" + orderdata.Customers.FirstName + " " + orderdata.Customers.SeconedName;
                Message += "<br />";
                Message += "<b> نوع الطلب: </b>" + (orderdata.OrderVendor.FirstOrDefault().OrderTypeId == (int)OrderTypeEnum.Now ? "فورى" : "مجدول");
                if (orderdata.OrderVendor.FirstOrDefault().OrderTypeId == (int)OrderTypeEnum.Schedule && orderdata.OrderVendor.FirstOrDefault().ScheduleDate != null)
                {
                    Message += "<br />";
                    Message += "<b> تاريخ الجدولة: </b>" + orderdata.OrderVendor.FirstOrDefault().ScheduleDate.Value.ToString("yyyy-MM-dd");
                }
                Message += "<br />";
                Message += "<br />";
                Message += "</div>";
                try
                {
                    _bLGeneral.SendEmail(_configuration["OrderMail"], "طلب جديد رقم #" + OrdersID, Message);
                }
                catch (Exception e)
                {
                }

            }
        }
        public AddOrderResponse AddOrder_New(AddOrderViewModelApiNew model, int UserId, int CustomersID, string FireBaseKey, string addedFrom, string UserLoginToken, DateTime? ScheduleDate, DateTime? ScheduleTime)
        {
            var res = new AddOrderResponse();
            decimal deliveryvatper = 0;
            decimal deliveryvatval = 0;
            decimal totalItemPrice = 0;
            decimal promoCodeDiscount = 0;
            decimal totalItemsAfterDicount = 0;

            try
            {
                // ارجاع قيمه الضريبه للتوصيل
                var DeliveryVatSettingPercent = _blConfiguration.GetDeliveryPriceVatPercent();

                if (model.vendorOrder.Count() > 0)
                {
                    // حساب قيمه المنتجات
                    foreach (var item in model.vendorOrder)
                    {
                        var VendCheck = Uow.Vendors.GetAll(s => s.VendorsID == item.vendorId).FirstOrDefault();

                        if (item.products.Count() > 0)
                        {
                            foreach (var prod in item.products)
                            {
                                var ProdData = Uow.Product.GetAll(s => s.ProductID == prod.productId).FirstOrDefault();
                                if (ProdData != null)
                                {
                                    if (ProdData.IsLimited)
                                    {
                                        if (ProdData.DailyQuantity == 0)
                                        {
                                            res.orderId = 0;
                                            res.message = "عفوا لقد نفذت الكمية اليومية المتاحة من المنتج " + ProdData.NameAr + "";
                                            return res;
                                        }
                                        else if (ProdData.DailyQuantity < prod.quantity)
                                        {
                                            res.orderId = 0;
                                            res.message = "الكمية المتوفرة من منتج " + ProdData.NameAr + " : " + ProdData.DailyQuantity + "";
                                            return res;
                                        }
                                    }
                                    totalItemPrice = totalItemPrice + ((ProdData.Price - _bLGeneral.CalcProductNet(ProdData.Discount, ProdData.StartDiscountDate, ProdData.EndDiscountDate, ProdData.Price)) * prod.quantity);
                                }
                            }
                        }
                        else
                        {
                            res.orderId = 0;
                            res.message = "لا يوجد طلبات مختارة من المتجر " + VendCheck.StoreNameAr + "";
                            return res;
                        }

                        #region Working_Times
                        if (VendCheck.IsDaysWork)//يجب ان يقوم المتجر بتحديد اوقات العمل
                        {
                            var date = DateTime.Now; //اذا كان الطلب فوري
                            if (model.orderTypeId == (int)OrderTypeEnum.Schedule) //اذا كان الطلب مجدول
                            {
                                var ddScheduleDate = (addedFrom == "WEB" ? ScheduleDate : (model.scheduleDate != null ? (model.scheduleDate != "" ?
                                DateTime.ParseExact(model.scheduleDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)
                                : null) : null));
                                var ScheddduleTime = (addedFrom == "WEB" ? ScheduleTime : (model.scheduleTime != null ? (model.scheduleTime != "" ?
                                DateTime.ParseExact(model.scheduleTime, "dd/MM/yyyy hh:mm tt", System.Globalization.CultureInfo.InvariantCulture)
                                : null) : null));
                                date = ddScheduleDate.Value.Date;
                                var time = ScheddduleTime.Value.TimeOfDay;
                                date = date.Add(time);
                            }

                            var DaysWorking = VendCheck.DaysWork.Split(","); // ايام العمل الرسمية
                            if (DaysWorking.Any(s => int.Parse(s) == (int)date.DayOfWeek)) //اذا كان الطلب في يوم من ايام العمل الرسمية
                            {
                                if (VendCheck.WorkFrom.HasValue && VendCheck.WorkTo.HasValue)// اذا تم تحديد وقت معين للعمل في ايام العمل الرسمية
                                {
                                    if (VendCheck.WorkFrom.Value.TimeOfDay < VendCheck.WorkTo.Value.TimeOfDay)
                                    {
                                        if (VendCheck.WorkFrom.Value.TimeOfDay < date.TimeOfDay && VendCheck.WorkTo.Value.TimeOfDay > date.TimeOfDay)
                                        {
                                            res.message = "المتجر مفتوح";
                                        }
                                        else
                                        {
                                            res.orderId = 0;
                                            res.message = "عفوا المتجر " + VendCheck.StoreNameAr + " تم انتهاء وقت العمل الخاص به اليوم";
                                            return res;
                                        }
                                    }
                                    else
                                    {
                                        if (VendCheck.WorkFrom.Value.TimeOfDay < date.TimeOfDay || VendCheck.WorkTo.Value.TimeOfDay > date.TimeOfDay)
                                        {
                                            res.message = "المتجر مفتوح";
                                        }
                                        else
                                        {
                                            res.orderId = 0;
                                            res.message = "عفوا المتجر " + VendCheck.StoreNameAr + " تم انتهاء وقت العمل الخاص به اليوم";
                                            return res;
                                        }
                                    }
                                }
                            }
                            else if (VendCheck.IsDaysVac) //اذا اضاف المتجر اوقات عمل في ايام الاجازة
                            {
                                var DaysVac = VendCheck.DaysVac.Split(",");//ايام الاجازة التى تم تحديدها
                                if (DaysVac.Any(s => int.Parse(s) == (int)date.DayOfWeek))//اذا تم تحديد اوقات للعمل في يوم الاجازة
                                {
                                    if (VendCheck.VacWorkFrom.HasValue && VendCheck.VacWorkTo.HasValue)//اذا تم تحديد وقت معين للعمل في يوم الاجازة
                                    {
                                        if (VendCheck.VacWorkFrom.Value.TimeOfDay < VendCheck.VacWorkTo.Value.TimeOfDay)
                                        {
                                            if (VendCheck.VacWorkFrom.Value.TimeOfDay < date.TimeOfDay && VendCheck.VacWorkTo.Value.TimeOfDay > date.TimeOfDay)
                                            {
                                                res.message = "المتجر مفتوح";
                                            }
                                            else
                                            {
                                                res.orderId = 0;
                                                res.message = "عفوا المتجر " + VendCheck.StoreNameAr + " تم انتهاء وقت العمل الخاص به اليوم";
                                                return res;
                                            }
                                        }
                                        else
                                        {
                                            if (VendCheck.VacWorkFrom.Value.TimeOfDay < date.TimeOfDay || VendCheck.VacWorkTo.Value.TimeOfDay > date.TimeOfDay)
                                            {
                                                res.message = "المتجر مفتوح";
                                            }
                                            else
                                            {
                                                res.orderId = 0;
                                                res.message = "عفوا المتجر " + VendCheck.StoreNameAr + " تم انتهاء وقت العمل الخاص به اليوم";
                                                return res;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    res.orderId = 0;
                                    res.message = "عفوا المتجر " + VendCheck.StoreNameAr + " غير متاح اليوم";
                                    return res;
                                }
                            }
                            else
                            {
                                res.orderId = 0;
                                res.message = "عفوا المتجر " + VendCheck.StoreNameAr + " غير متاح لتجهيز طلباتكم الان";
                                return res;
                            }
                        }
                        else
                        {
                            res.orderId = 0;
                            res.message = "لا يمكن الطلب من المتجر " + VendCheck.StoreNameAr + " لأنه لم يحدد اوقات العمل";
                            return res;
                        }
                        #endregion
                    }


                    // حساب اجمالى التوصيل
                    var totalOrderValue = totalItemPrice;

                    // حساب ضريبه القيمه المضافه للمنتجات
                    var vatvalue = (DeliveryVatSettingPercent * totalOrderValue) / 100;

                    // حساب اجمالى الطلب مع ضريبه القيمه المضافه للمنتجات
                    totalOrderValue = totalOrderValue + model.vendorOrder.Sum(e => e.deliveryprice) + vatvalue;
                    // ألتحقق من برومو كود الخصم وارجاع قيمته

                    if (model.promoCodeID != null)
                    {
                        var PromoData = Uow.PromoCode.GetAll(s => s.PromoCodeID == model.promoCodeID).FirstOrDefault();
                        if (PromoData != null)
                        {
                            var Pro = CheckPromoCodeValid(PromoData.Code, CustomersID, totalOrderValue, "", 1);
                            promoCodeDiscount = Pro.discount;
                        }
                    }
                    // حساب اجمالى الطلب مع الخصم
                    totalOrderValue = totalOrderValue - promoCodeDiscount;

                    // التحقق
                    // التحقق من طريقه الدفع والتحقق من رصيد المحفظه
                    if (model.paymentTypeId == (int)PaymentTypeEnum.Wallet)
                    {
                        var CustomerBalance = _BlCustomerBalance.GetLastBlance(CustomersID);
                        if (totalOrderValue > CustomerBalance)
                        {
                            res.orderId = 0;
                            res.message = "عفوا المبلغ المتواجد في المحفظة اقل من اجمالى الطلبات";
                            return res;
                        }
                    }
                    // انشاء ماستر الطلب
                    var OrderData = Uow.Orders.Insert(new Orders()
                    {
                        CreateDate = _bLGeneral.GetDateTimeNow(),
                        CustomerLocationID = model.addressID,
                        CustomersID = CustomersID,
                        DeliveryPrice = model.vendorOrder.Sum(e => e.deliveryprice),
                        Discount = promoCodeDiscount,
                        IsDeleted = false,
                        IsEnable = true,
                        Notes = string.Empty,
                        OrdersGuid = Guid.NewGuid(),
                        OrderStatusID = (int)OrderStatusEnum.Create,
                        PaymentTypeId = model.paymentTypeId,
                        Price = totalItemPrice,
                        Total = totalOrderValue,
                        Vat = vatvalue,
                        UserId = UserId,
                        PromoCodeID = model.promoCodeID,
                        ReferenceNo = model.customerReference,
                        InvoiceId = model.invoiceId
                    });


                    // التحقق من استخدام البرومو كود وتسجيل استخدامه
                    if (promoCodeDiscount > 0)
                    {
                        var PromoData = Uow.PromoCode.GetAll(s => s.PromoCodeID == model.promoCodeID).FirstOrDefault();
                        if (PromoData != null)
                        {
                            OrderData.OrderPromo.Add(
                              new OrderPromo()
                              {
                                  CreateDate = _bLGeneral.GetDateTimeNow(),
                                  DiscountType = PromoData.DiscountType,
                                  DiscountValue = PromoData.DiscountValue,
                                  IsDeleted = false,
                                  IsEnable = true,
                                  OrderPromoGuid = Guid.NewGuid(),
                                  PromoCodeID = PromoData.PromoCodeID,
                                  UserId = UserId,
                              });
                        }
                    }


                    decimal currentAmountwallet = 0;

                    // بدء تسجيل الطلبات على حدى
                    foreach (var item in model.vendorOrder)
                    {
                        decimal VatProduct = 0;
                        List<OrderItems> orderItems = new List<OrderItems>();


                        // نوع كابتن الطلب
                        int CaptainType = 0;

                        // اجمالى المنتجات لمقدم الخدمه
                        decimal priceItemOrderVendor = 0;

                        // انشاء رقم الشحنه
                        var TrackNo = _bLGeneral.KeyGeneratorNumbersOnly(12);


                        // سعر التوصيل للطلب
                        decimal DeliveryPricePerOne = item.deliveryprice;

                        // ارجاع بيانات مقدم الخدمه
                        var vaendordata = Uow.Vendors.GetAll(s => s.VendorsID == item.vendorId).FirstOrDefault();

                        // قيمه الخصم للطلب الواحد ويتم حسابتها كا الاتى
                        // يتم تقسيم قيمه الخصم على عدد الطلبات
                        decimal discountOrder = 0;
                        // حساب اجمالى الطلب لمقدم الخدمه المحدد
                        foreach (var prod in item.products)
                        {
                            var ProdData = Uow.Product.GetAll(s => s.ProductID == prod.productId).FirstOrDefault();
                            if (ProdData != null)
                            {
                                priceItemOrderVendor = priceItemOrderVendor + ((ProdData.Price - _bLGeneral.CalcProductNet(ProdData.Discount, ProdData.StartDiscountDate, ProdData.EndDiscountDate, ProdData.Price)) * prod.quantity);
                            }
                        }
                        // حساب اجممالى الطلب شامل الضريبه
                        var priceOrderVendor = priceItemOrderVendor;
                        // التحقق من الباكدج الخاصه بمقدم الخدمه والرجوع بقيمتها
                        var pacakedata = Uow.Package.GetAll(s => !s.IsDeleted && s.PackageType == (int)PackageTypeEnum.Default).FirstOrDefault();
                        if (vaendordata.PackageID.HasValue)
                        {
                            pacakedata = Uow.Package.GetAll(s => !s.IsDeleted && s.PackageID == vaendordata.PackageID.Value).FirstOrDefault();
                        }

                        // احتساب سعر الباكدج 
                        decimal PackageAmount = pacakedata.Price;
                        decimal PackagePercentValue = (pacakedata.Percent * priceOrderVendor) / 100;
                        var totalPackageAmount = PackageAmount + PackagePercentValue;
                        decimal perhome = totalPackageAmount;
                        decimal perstore = (priceOrderVendor - totalPackageAmount);
                        // التحقق من سواق التوصيل هل متجر او هوم ميد
                        if (Uow.Vendors.GetAll(s => s.VendorsID == item.vendorId && s.DeliveryType == (int)DeliveryTypeEnum.Store).Any())
                        {
                            CaptainType = (int)CaptainTypeEnum.Store;

                            // حساب نسبه المتجر من الطلب 
                            //لو المتجر عنده سواق هيزيد في حقه سعر التوصيل بتاعه
                            perstore = perstore + DeliveryPricePerOne;
                            deliveryvatper = 0;
                            deliveryvatval = 0;
                        }
                        else
                        {
                            var disval = promoCodeDiscount > 0 ? (promoCodeDiscount / model.vendorOrder.Count()) : 0;
                            discountOrder = disval;
                            perhome = perhome - disval;
                            deliveryvatper = DeliveryVatSettingPercent;
                            deliveryvatval = DeliveryPricePerOne - (DeliveryPricePerOne / (1 + (_blConfiguration.GetDeliveryPriceVatPercent() / 100)));
                            CaptainType = (int)CaptainTypeEnum.Home_Made;
                        }
                        decimal vatorder = (priceOrderVendor * DeliveryVatSettingPercent) / 100; //vatvalue > 0 ? (vatvalue / model.vendorOrder.Count()) : 0;
                        // حساب اجمالى الطلب شامل الضريبه والخصم
                        priceOrderVendor = priceOrderVendor + vatorder;
                        // اجمالى الطلب بالاضافه لسعر التوصيل
                        priceItemOrderVendor = priceOrderVendor + DeliveryPricePerOne - discountOrder;
                        foreach (var prodiem in item.products)
                        {
                            decimal Dicitem = discountOrder != 0 ? (discountOrder / item.products.Count()) : 0;
                            var ProdData = Uow.Product.GetAll(s => s.ProductID == prodiem.productId).FirstOrDefault();
                            if (ProdData != null)
                            {
                                var vatPriceViewModel = _blConfiguration.GetVatForPrice(ProdData.Price - _bLGeneral.CalcProductNet(ProdData.Discount, ProdData.StartDiscountDate, ProdData.EndDiscountDate, ProdData.Price));
                                totalItemsAfterDicount += ((ProdData.Price - _bLGeneral.CalcProductNet(ProdData.Discount, ProdData.StartDiscountDate, ProdData.EndDiscountDate, ProdData.Price)) * prodiem.quantity);
                                VatProduct += (vatPriceViewModel.VatValue * prodiem.quantity);
                                orderItems.Add(new OrderItems()
                                {
                                    CreateDate = _bLGeneral.GetDateTimeNow(),
                                    Discount = Dicitem,
                                    IsDeleted = false,
                                    IsEnable = true,
                                    OrderItemsGuid = Guid.NewGuid(),
                                    PriceBeforeVat = ProdData.Price,
                                    VatValue = vatPriceViewModel.VatValue,
                                    Price = vatPriceViewModel.PriceWithVat,
                                    ProdImage = ProdData.Logo,
                                    ProdNameAr = ProdData.NameAr,
                                    ProdNameEn = ProdData.NameEn,
                                    ProductID = ProdData.ProductID,
                                    UserId = UserId,
                                    Quantity = prodiem.quantity,
                                });
                                if (ProdData.IsLimited)
                                {
                                    ProdData.DailyQuantity = (ProdData.DailyQuantity - prodiem.quantity) < 0 ? 0 : (ProdData.DailyQuantity - prodiem.quantity);
                                    Uow.Product.Update(ProdData);
                                }
                            }
                        }
                        var customerData = Uow.Customers.GetAll(e => e.CustomersID == CustomersID).FirstOrDefault();
                        var listCustomerBalance_Obj = new List<CustomerBalance>();
                        CustomerBalance CustomerBalance_Obj = new CustomerBalance();
                        // التحقق من ان كانت طريقه الدفع هى المحفظه
                        if (model.paymentTypeId == (int)PaymentTypeEnum.Wallet)
                        {
                            var CustomerBalance = _BlCustomerBalance.GetLastBlance(CustomersID);
                            if (totalOrderValue > CustomerBalance)
                            {
                                res.orderId = 0;
                                res.message = "عفوا المبلغ المتواجد في المحفظة اقل من اجمالى الطلبات";
                                return res;
                            }

                            var disval = promoCodeDiscount > 0 ? (promoCodeDiscount / model.vendorOrder.Count()) : 0;

                            var amountwallet = priceItemOrderVendor;
                            amountwallet = amountwallet <= 0 ? 0 : amountwallet;
                            var Before = CustomerBalance - currentAmountwallet;
                            CustomerBalance_Obj = (new CustomerBalance()
                            {
                                Amount = amountwallet,
                                Before = Before,
                                CreateDate = _bLGeneral.GetDateTimeNow(),
                                CustomersID = CustomersID,
                                IsDeleted = false,
                                TransactionTypeID = (int)TransactionTypeEnum.Pay_Order,
                                TypeOperationID = (int)TypeOperationEnum.DR,
                                UserId = UserId,
                                After = ((Before - amountwallet)),
                                CustomerBlanceGuid = Guid.NewGuid(),
                                DateOperation = _bLGeneral.GetDateTimeNow(),
                                Attachment = "",
                                Discripe = "الطلب مجانى لان قيمه الخصم اكبر من قيمه الطلب رقم البرومو كود " + model.promoCodeID + "  وقيمه الخصم " + promoCodeDiscount + ""
                            });
                            currentAmountwallet += amountwallet;
                            listCustomerBalance_Obj.Add(CustomerBalance_Obj);

                        }
                        else
                        {
                            CustomerBalance_Obj = null;
                            listCustomerBalance_Obj = null;
                        }



                        OrderData.OrderVendor.Add(new OrderVendor()
                        {
                            TotalBaseItems = totalItemsAfterDicount, //orderItems.Sum(x => x.PriceBeforeVat * x.Quantity),
                            VendorsID = item.vendorId,
                            CreateDate = _bLGeneral.GetDateTimeNow(),
                            DeliveryPrice = DeliveryPricePerOne,
                            Discount = (promoCodeDiscount > 0 ? (promoCodeDiscount / model.vendorOrder.Count()) : 0),
                            InvoiceImage = string.Empty,
                            IsDeleted = false,
                            IsEnable = true,
                            Notes = item.notes,
                            OrderStatusID = (int)OrderStatusEnum.Create,
                            OrderVendorGuid = Guid.NewGuid(),
                            TrackNo = TrackNo,
                            Price = priceOrderVendor,
                            Total = priceItemOrderVendor,
                            UserId = UserId,
                            CaptainPaidID = (int)PaidStatusEnum.Created,
                            DriverCharge = 0,
                            CaptainTypeId = CaptainType,
                            InvoiceTypeId = (int)OrderInvoiceTypeEnum.Created,
                            PerStore = perstore,
                            PerHome = perhome,
                            PackageAmount = PackageAmount,
                            PackageID = pacakedata.PackageID,
                            PackagePercent = PackagePercentValue,
                            DistanceKM = item.distanceKM,
                            AddedFrom = addedFrom,
                            OrderTypeId = model.orderTypeId,
                            DeliveryVatValue = deliveryvatval,
                            VatValue = deliveryvatval + VatProduct,
                            VatPercent = deliveryvatper,
                            VatProduct = VatProduct,
                            ScheduleDate = (addedFrom == "WEB" ? ScheduleDate : (model.scheduleDate != null ? (model.scheduleDate != "" ?
                            DateTime.ParseExact(model.scheduleDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)
                            : null) : null)),

                            ScheduleTime = (addedFrom == "WEB" ? ScheduleTime : (model.scheduleTime != null ? (model.scheduleTime != "" ?
                            DateTime.ParseExact(model.scheduleTime, "dd/MM/yyyy hh:mm tt", System.Globalization.CultureInfo.InvariantCulture)
                            : null) : null)),
                            OrderHistory = new List<OrderHistory>() {
                                new OrderHistory()
                                {
                                    CreateDate = _bLGeneral.GetDateTimeNow(),
                                    UserId = UserId,
                                    OrderStatusID = (int)OrderStatusEnum.Create,
                                    IsDeleted = false,
                                    IsEnable = true,
                                    Lat = 0,
                                    Lng = 0,
                                    OrderHistoryGuid = Guid.NewGuid(),
                                }
                            },
                            OrderItems = orderItems,
                            CustomerBalance = listCustomerBalance_Obj,


                        });
                    }
                    Uow.Commit();
                    var customerDataNot = Uow.Customers.GetAll(e => e.CustomersID == CustomersID).FirstOrDefault();
                    foreach (var item in OrderData.OrderVendor)
                    {
                        #region Push
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
                            body = "طلب جديد من الزبون " + OrderData.Customers.FirstName + " فضلا قم بتجهيز الطلب الان",//For IOS
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

                        Uow.Notification.Insert(new Notification()
                        {
                            CreateDate = _bLGeneral.GetDateTimeNow(),
                            TitleAR = "تم إرسال الطلب رقم " + item.OrderVendorID + " من خلال " + customerDataNot.FirstName + "",
                            TitleEN = "your order has been send from " + customerDataNot.FirstName + "",
                            DescAR = "تم إرسال الطلب رقم " + item.OrderVendorID + " من خلال " + customerDataNot.FirstName + " في تاريخ " + _bLGeneral.GetDateTimeNow() + "",
                            DescEN = "order No " + item.OrderVendorID + " has been send from " + customerDataNot.FirstName + " on date " + _bLGeneral.GetDateTimeNow() + "",
                            IsDeleted = false,
                            IsEnable = true,
                            IsSeen = false,
                            NotificationGuid = Guid.NewGuid(),
                            NotificationTypeID = (int)NotificationTypeEnum.Order,
                            UserId = UserId,
                            NotificationDate = _bLGeneral.GetDateTimeNow(),
                            OrderVendorID = item.OrderVendorID,
                            VendorsID = item.VendorsID
                        });
                        Uow.Commit();
                    }

                    res.message = "true";
                    res.orderId = OrderData.OrdersID;
                }
                else
                {
                    res.orderId = 0;
                    res.message = "لا يوجد طلبات من اى مزود خدمة";
                }
            }
            catch (Exception ex)
            {
                res.orderId = 0;
                res.message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }
            return res;
        }
        public AddOrderResponse AddOrder_New_Drafts(AddOrderViewModelApiNew model, int UserId, int CustomersID, string FireBaseKey, string addedFrom, string UserLoginToken, DateTime? ScheduleDate, DateTime? ScheduleTime)
        {
            var res = new AddOrderResponse();
            decimal deliveryvatper = 0;
            decimal deliveryvatval = 0;
            decimal totalItemPrice = 0;
            decimal promoCodeDiscount = 0;
            decimal totalItemsAfterDicount = 0;


            try
            {
                // ارجاع قيمه الضريبه للتوصيل
                var DeliveryVatSettingPercent = _blConfiguration.GetDeliveryPriceVatPercent();

                if (model.vendorOrder.Count() > 0)
                {
                    // حساب قيمه المنتجات
                    foreach (var item in model.vendorOrder)
                    {
                        var VendCheck = Uow.Vendors.GetAll(s => s.VendorsID == item.vendorId).FirstOrDefault();

                        if (item.products.Count() > 0)
                        {
                            foreach (var prod in item.products)
                            {
                                var ProdData = Uow.Product.GetAll(s => s.ProductID == prod.productId).FirstOrDefault();
                                if (ProdData != null)
                                {
                                    if (ProdData.IsLimited)
                                    {
                                        if (ProdData.DailyQuantity == 0)
                                        {
                                            res.orderId = 0;
                                            res.message = "عفوا لقد نفذت الكمية اليومية المتاحة من المنتج " + ProdData.NameAr + "";
                                            return res;
                                        }
                                        else if (ProdData.DailyQuantity < prod.quantity)
                                        {
                                            res.orderId = 0;
                                            res.message = "الكمية المتوفرة من منتج " + ProdData.NameAr + " : " + ProdData.DailyQuantity + "";
                                            return res;
                                        }
                                    }
                                    totalItemPrice = totalItemPrice + ((ProdData.Price - _bLGeneral.CalcProductNet(ProdData.Discount, ProdData.StartDiscountDate, ProdData.EndDiscountDate, ProdData.Price)) * prod.quantity);
                                }
                            }

                        }
                        else
                        {
                            res.orderId = 0;
                            res.message = "لا يوجد طلبات مختارة من المتجر " + VendCheck.StoreNameAr + "";
                            return res;
                        }
                    }

                    // حساب اجمالى التوصيل
                    var totalOrderValue = totalItemPrice;

                    // حساب ضريبه القيمه المضافه للمنتجات
                    var vatvalue = (DeliveryVatSettingPercent * totalOrderValue) / 100;

                    // حساب اجمالى الطلب مع ضريبه القيمه المضافه للمنتجات
                    totalOrderValue = totalOrderValue + model.vendorOrder.Sum(e => e.deliveryprice) + vatvalue;

                    // ألتحقق من برومو كود الخصم وارجاع قيمته

                    if (model.promoCodeID != null)
                    {
                        var PromoData = Uow.PromoCode.GetAll(s => s.PromoCodeID == model.promoCodeID).FirstOrDefault();
                        if (PromoData != null)
                        {
                            var Pro = CheckPromoCodeValid(PromoData.Code, CustomersID, totalOrderValue, "", 1);
                            promoCodeDiscount = Pro.discount;
                        }
                    }

                    // حساب اجمالى الطلب مع الخصم
                    totalOrderValue = totalOrderValue - promoCodeDiscount;

                    // التحقق

                    // انشاء ماستر الطلب
                    var OrderData = Uow.Orders.Insert(new Orders()
                    {
                        CreateDate = _bLGeneral.GetDateTimeNow(),
                        CustomerLocationID = model.addressID,
                        CustomersID = CustomersID,
                        DeliveryPrice = model.vendorOrder.Sum(e => e.deliveryprice),
                        Discount = promoCodeDiscount,
                        IsDeleted = false,
                        IsEnable = true,
                        Notes = string.Empty,
                        OrdersGuid = Guid.NewGuid(),
                        OrderStatusID = (int)OrderStatusEnum.Drafts,
                        PaymentTypeId = model.paymentTypeId,
                        Price = totalItemPrice,
                        Total = totalOrderValue,
                        Vat = vatvalue,
                        UserId = UserId,
                        PromoCodeID = model.promoCodeID,
                        ReferenceNo = model.customerReference,
                        InvoiceId = model.invoiceId,

                    });


                    // التحقق من استخدام البرومو كود وتسجيل استخدامه
                    if (promoCodeDiscount > 0)
                    {
                        var PromoData = Uow.PromoCode.GetAll(s => s.PromoCodeID == model.promoCodeID).FirstOrDefault();
                        if (PromoData != null)
                        {
                            OrderData.OrderPromo.Add(
                              new OrderPromo()
                              {
                                  CreateDate = _bLGeneral.GetDateTimeNow(),
                                  DiscountType = PromoData.DiscountType,
                                  DiscountValue = PromoData.DiscountValue,
                                  IsDeleted = false,
                                  IsEnable = true,
                                  OrderPromoGuid = Guid.NewGuid(),
                                  PromoCodeID = PromoData.PromoCodeID,
                                  UserId = UserId,
                              });
                        }
                    }


                    decimal currentAmountwallet = 0;

                    // بدء تسجيل الطلبات على حدى
                    foreach (var item in model.vendorOrder)
                    {
                        decimal VatProduct = 0;
                        List<OrderItems> orderItems = new List<OrderItems>();


                        // نوع كابتن الطلب
                        int CaptainType = 0;

                        // اجمالى المنتجات لمقدم الخدمه
                        decimal priceItemOrderVendor = 0;

                        // انشاء رقم الشحنه
                        var TrackNo = _bLGeneral.KeyGeneratorNumbersOnly(12);


                        // سعر التوصيل للطلب
                        decimal DeliveryPricePerOne = item.deliveryprice;

                        // ارجاع بيانات مقدم الخدمه
                        var vaendordata = Uow.Vendors.GetAll(s => s.VendorsID == item.vendorId).FirstOrDefault();

                        // قيمه الخصم للطلب الواحد ويتم حسابتها كا الاتى
                        // يتم تقسيم قيمه الخصم على عدد الطلبات
                        decimal discountOrder = 0;
                        // حساب اجمالى الطلب لمقدم الخدمه المحدد
                        foreach (var prod in item.products)
                        {
                            var ProdData = Uow.Product.GetAll(s => s.ProductID == prod.productId).FirstOrDefault();
                            if (ProdData != null)
                            {
                                priceItemOrderVendor = priceItemOrderVendor + ((ProdData.Price - _bLGeneral.CalcProductNet(ProdData.Discount, ProdData.StartDiscountDate, ProdData.EndDiscountDate, ProdData.Price)) * prod.quantity);
                            }
                        }

                        // حساب اجممالى الطلب شامل الضريبه
                        var priceOrderVendor = priceItemOrderVendor;

                        // التحقق من الباكدج الخاصه بمقدم الخدمه والرجوع بقيمتها
                        var pacakedata = Uow.Package.GetAll(s => !s.IsDeleted && s.PackageType == (int)PackageTypeEnum.Default).FirstOrDefault();
                        if (vaendordata.PackageID.HasValue)
                        {
                            pacakedata = Uow.Package.GetAll(s => !s.IsDeleted && s.PackageID == vaendordata.PackageID.Value).FirstOrDefault();
                        }

                        // احتساب سعر الباكدج 
                        decimal PackageAmount = pacakedata.Price;
                        decimal PackagePercentValue = (pacakedata.Percent * priceOrderVendor) / 100;
                        var totalPackageAmount = PackageAmount + PackagePercentValue;
                        decimal perhome = totalPackageAmount;
                        decimal perstore = (priceOrderVendor - totalPackageAmount);
                        // التحقق من سواق التوصيل هل متجر او هوم ميد
                        if (Uow.Vendors.GetAll(s => s.VendorsID == item.vendorId && s.DeliveryType == (int)DeliveryTypeEnum.Store).Any())
                        {
                            CaptainType = (int)CaptainTypeEnum.Store;

                            // حساب نسبه المتجر من الطلب 
                            //لو المتجر عنده سواق هيزيد في حقه سعر التوصيل بتاعه
                            perstore = perstore + DeliveryPricePerOne;
                            deliveryvatper = 0;
                            deliveryvatval = 0;
                        }
                        else
                        {
                            var disval = promoCodeDiscount > 0 ? (promoCodeDiscount / model.vendorOrder.Count()) : 0;
                            discountOrder = disval;
                            // حساب نسبه هوم ميد من الطلب
                            perhome = perhome - disval;
                            deliveryvatper = DeliveryVatSettingPercent;
                            deliveryvatval = DeliveryPricePerOne - (DeliveryPricePerOne / (1 + (_blConfiguration.GetDeliveryPriceVatPercent() / 100)));

                            CaptainType = (int)CaptainTypeEnum.Home_Made;
                        }
                        // حساب قيمه الضريبه المضافه للمنتجات
                        decimal vatorder = (priceOrderVendor * DeliveryVatSettingPercent) / 100;
                        // حساب اجمالى الطلب شامل الضريبه والخصم
                        priceOrderVendor = priceOrderVendor + vatorder;
                        // اجمالى الطلب بالاضافه لسعر التوصيل
                        priceItemOrderVendor = priceOrderVendor + DeliveryPricePerOne - discountOrder;


                        foreach (var prodiem in item.products)
                        {
                            decimal Dicitem = discountOrder != 0 ? (discountOrder / item.products.Count()) : 0;
                            var ProdData = Uow.Product.GetAll(s => s.ProductID == prodiem.productId).FirstOrDefault();
                            if (ProdData != null)
                            {
                                var vatPriceViewModel = _blConfiguration.GetVatForPrice(ProdData.Price - _bLGeneral.CalcProductNet(ProdData.Discount, ProdData.StartDiscountDate, ProdData.EndDiscountDate, ProdData.Price));
                                totalItemsAfterDicount += ((ProdData.Price - _bLGeneral.CalcProductNet(ProdData.Discount, ProdData.StartDiscountDate, ProdData.EndDiscountDate, ProdData.Price)) * prodiem.quantity);
                                VatProduct += (vatPriceViewModel.VatValue * prodiem.quantity);
                                orderItems.Add(new OrderItems()
                                {
                                    CreateDate = _bLGeneral.GetDateTimeNow(),
                                    Discount = Dicitem,
                                    IsDeleted = false,
                                    IsEnable = true,
                                    OrderItemsGuid = Guid.NewGuid(),
                                    PriceBeforeVat = ProdData.Price,
                                    VatValue = vatPriceViewModel.VatValue,
                                    Price = vatPriceViewModel.PriceWithVat,
                                    ProdImage = ProdData.Logo,
                                    ProdNameAr = ProdData.NameAr,
                                    ProdNameEn = ProdData.NameEn,
                                    ProductID = ProdData.ProductID,
                                    UserId = UserId,
                                    Quantity = prodiem.quantity,
                                });
                                if (ProdData.IsLimited)
                                {
                                    ProdData.DailyQuantity = (ProdData.DailyQuantity - prodiem.quantity) < 0 ? 0 : (ProdData.DailyQuantity - prodiem.quantity);
                                    Uow.Product.Update(ProdData);
                                }
                            }
                        }


                        var customerData = Uow.Customers.GetAll(e => e.CustomersID == CustomersID).FirstOrDefault();
                        var listCustomerBalance_Obj = new List<CustomerBalance>();
                        CustomerBalance CustomerBalance_Obj = new CustomerBalance();
                        // التحقق من ان كانت طريقه الدفع هى المحفظه

                        CustomerBalance_Obj = null;
                        listCustomerBalance_Obj = null;




                        OrderData.OrderVendor.Add(new OrderVendor()
                        {
                            TotalBaseItems = totalItemsAfterDicount,
                            VatValue = deliveryvatval + VatProduct,
                            VendorsID = item.vendorId,
                            CreateDate = _bLGeneral.GetDateTimeNow(),
                            DeliveryPrice = DeliveryPricePerOne,
                            Discount = (promoCodeDiscount > 0 ? (promoCodeDiscount / model.vendorOrder.Count()) : 0),
                            InvoiceImage = string.Empty,
                            IsDeleted = true,
                            IsEnable = true,
                            Notes = item.notes,
                            OrderStatusID = (int)OrderStatusEnum.Drafts,
                            OrderVendorGuid = Guid.NewGuid(),
                            TrackNo = TrackNo,
                            Price = priceOrderVendor,
                            Total = priceItemOrderVendor,
                            UserId = UserId,
                            CaptainPaidID = (int)PaidStatusEnum.Created,
                            DriverCharge = 0,
                            CaptainTypeId = CaptainType,
                            InvoiceTypeId = (int)OrderInvoiceTypeEnum.Created,
                            PerStore = perstore,
                            PerHome = perhome,
                            PackageAmount = PackageAmount,
                            PackageID = pacakedata.PackageID,
                            PackagePercent = PackagePercentValue,
                            VatPercent = deliveryvatper,
                            DeliveryVatValue = deliveryvatval,
                            DistanceKM = item.distanceKM,
                            AddedFrom = addedFrom,
                            OrderTypeId = model.orderTypeId,
                            VatProduct = VatProduct,
                            ScheduleDate = (addedFrom == "WEB" ? ScheduleDate : (model.scheduleDate != null ? (model.scheduleDate != "" ?
                            DateTime.ParseExact(model.scheduleDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)
                            : null) : null)),

                            ScheduleTime = (addedFrom == "WEB" ? ScheduleTime : (model.scheduleTime != null ? (model.scheduleTime != "" ?
                            DateTime.ParseExact(model.scheduleDate, "dd/MM/yyyy hh:mm tt", System.Globalization.CultureInfo.InvariantCulture)
                            : null) : null)),
                            OrderHistory = new List<OrderHistory>() {
                                new OrderHistory()
                                {
                                    CreateDate = _bLGeneral.GetDateTimeNow(),
                                    UserId = UserId,
                                    OrderStatusID = (int)OrderStatusEnum.Drafts,
                                    IsDeleted = false,
                                    IsEnable = true,
                                    Lat = 0,
                                    Lng = 0,
                                    OrderHistoryGuid = Guid.NewGuid(),
                                }
                            },

                            OrderItems = orderItems,
                            CustomerBalance = listCustomerBalance_Obj,

                        });
                    }

                    Uow.Commit();


                    res.message = "true";
                    res.orderId = OrderData.OrdersID;

                    res.totalOrderValue = totalOrderValue;
                }
                else
                {
                    res.orderId = 0;
                    res.message = "لا يوجد طلبات من اى مزود خدمة";
                }
            }
            catch (Exception ex)
            {
                res.orderId = 0;
                res.message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }
            return res;
        }
        public AddOrderResponse AddOrder_New_Pending(AddOrderViewModelApiNew model, int UserId, int CustomersID, string FireBaseKey, string addedFrom, string UserLoginToken, DateTime? ScheduleDate, DateTime? ScheduleTime)
        {
            var res = new AddOrderResponse();
            res.listVendorsID = new List<int>();
            decimal deliveryvatper = 0;
            decimal deliveryvatval = 0;
            decimal totalItemPrice = 0;
            decimal promoCodeDiscount = 0;
            decimal totalItemsAfterDicount = 0;

            try
            {
                // ارجاع قيمه الضريبه للتوصيل
                var DeliveryVatSettingPercent = _blConfiguration.GetDeliveryPriceVatPercent();

                if (model.vendorOrder.Count() > 0)
                {
                    // حساب قيمه المنتجات
                    foreach (var item in model.vendorOrder)
                    {
                        var VendCheck = Uow.Vendors.GetAll(s => s.VendorsID == item.vendorId).FirstOrDefault();

                        if (item.products.Count() > 0)
                        {
                            foreach (var prod in item.products)
                            {
                                var ProdData = Uow.Product.GetAll(s => s.ProductID == prod.productId).FirstOrDefault();
                                if (ProdData != null)
                                {
                                    totalItemPrice = totalItemPrice + ((ProdData.Price - _bLGeneral.CalcProductNet(ProdData.Discount, ProdData.StartDiscountDate, ProdData.EndDiscountDate, ProdData.Price)) * prod.quantity);
                                }
                            }

                        }
                        else
                        {
                            res.orderId = 0;
                            res.message = "لا يوجد طلبات مختارة من المتجر " + VendCheck.StoreNameAr + "";
                            return res;
                        }

                        #region Working_Times
                        if (VendCheck.IsDaysWork)//يجب ان يقوم المتجر بتحديد اوقات العمل
                        {
                            var date = DateTime.Now; //اذا كان الطلب فوري
                            var DaysWorking = VendCheck.DaysWork.Split(","); // ايام العمل الرسمية
                            if (DaysWorking.Any(s => int.Parse(s) == (int)date.DayOfWeek)) //اذا كان الطلب في يوم من ايام العمل الرسمية
                            {
                                if (VendCheck.WorkFrom.HasValue && VendCheck.WorkTo.HasValue)// اذا تم تحديد وقت معين للعمل في ايام العمل الرسمية
                                {
                                    if (VendCheck.WorkFrom.Value.TimeOfDay < VendCheck.WorkTo.Value.TimeOfDay)
                                    {
                                        if (VendCheck.WorkFrom.Value.TimeOfDay < date.TimeOfDay && VendCheck.WorkTo.Value.TimeOfDay > date.TimeOfDay)
                                        {
                                            res.message = "المتجر مفتوح";
                                        }
                                        else
                                        {
                                            res.orderId = 0;
                                            res.message = "عفوا المتجر " + VendCheck.StoreNameAr + " تم انتهاء وقت العمل الخاص به اليوم";
                                            return res;
                                        }
                                    }
                                    else
                                    {
                                        if (VendCheck.WorkFrom.Value.TimeOfDay < date.TimeOfDay || VendCheck.WorkTo.Value.TimeOfDay > date.TimeOfDay)
                                        {
                                            res.message = "المتجر مفتوح";
                                        }
                                        else
                                        {
                                            res.orderId = 0;
                                            res.message = "عفوا المتجر " + VendCheck.StoreNameAr + " تم انتهاء وقت العمل الخاص به اليوم";
                                            return res;
                                        }
                                    }
                                }
                            }
                            else if (VendCheck.IsDaysVac) //اذا اضاف المتجر اوقات عمل في ايام الاجازة
                            {
                                var DaysVac = VendCheck.DaysVac.Split(",");//ايام الاجازة التى تم تحديدها
                                if (DaysVac.Any(s => int.Parse(s) == (int)date.DayOfWeek))//اذا تم تحديد اوقات للعمل في يوم الاجازة
                                {
                                    if (VendCheck.VacWorkFrom.HasValue && VendCheck.VacWorkTo.HasValue)//اذا تم تحديد وقت معين للعمل في يوم الاجازة
                                    {
                                        if (VendCheck.VacWorkFrom.Value.TimeOfDay < VendCheck.VacWorkTo.Value.TimeOfDay)
                                        {
                                            if (VendCheck.VacWorkFrom.Value.TimeOfDay < date.TimeOfDay && VendCheck.VacWorkTo.Value.TimeOfDay > date.TimeOfDay)
                                            {
                                                res.message = "المتجر مفتوح";
                                            }
                                            else
                                            {
                                                res.orderId = 0;
                                                res.message = "عفوا المتجر " + VendCheck.StoreNameAr + " تم انتهاء وقت العمل الخاص به اليوم";
                                                return res;
                                            }
                                        }
                                        else
                                        {
                                            if (VendCheck.VacWorkFrom.Value.TimeOfDay < date.TimeOfDay || VendCheck.VacWorkTo.Value.TimeOfDay > date.TimeOfDay)
                                            {
                                                res.message = "المتجر مفتوح";
                                            }
                                            else
                                            {
                                                res.orderId = 0;
                                                res.message = "عفوا المتجر " + VendCheck.StoreNameAr + " تم انتهاء وقت العمل الخاص به اليوم";
                                                return res;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    res.orderId = 0;
                                    res.message = "عفوا المتجر " + VendCheck.StoreNameAr + " غير متاح اليوم";
                                    return res;
                                }
                            }
                            else
                            {
                                res.orderId = 0;
                                res.message = "عفوا المتجر " + VendCheck.StoreNameAr + " غير متاح لتجهيز طلباتكم الان";
                                return res;
                            }
                        }
                        else
                        {
                            res.orderId = 0;
                            res.message = "لا يمكن الطلب من المتجر " + VendCheck.StoreNameAr + " لأنه لم يحدد اوقات العمل";
                            return res;
                        }
                        #endregion

                    }

                    // ألتحقق من برومو كود الخصم وارجاع قيمته
                    // حساب اجمالى التوصيل
                    var totalOrderValue = totalItemPrice;

                    // حساب ضريبه القيمه المضافه للمنتجات
                    var vatvalue = (DeliveryVatSettingPercent * totalOrderValue) / 100;

                    // حساب اجمالى الطلب مع ضريبه القيمه المضافه للمنتجات
                    totalOrderValue = totalOrderValue + model.vendorOrder.Sum(e => e.deliveryprice) + vatvalue;


                    // التحقق

                    // انشاء ماستر الطلب
                    var OrderData = Uow.Orders.Insert(new Orders()
                    {
                        CreateDate = _bLGeneral.GetDateTimeNow(),
                        CustomerLocationID = model.addressID,
                        CustomersID = CustomersID,
                        DeliveryPrice = model.vendorOrder.Sum(e => e.deliveryprice),
                        Discount = promoCodeDiscount,
                        IsDeleted = false,
                        IsEnable = true,
                        Notes = string.Empty,
                        OrdersGuid = Guid.NewGuid(),
                        OrderStatusID = (int)OrderStatusEnum.Pending,
                        PaymentTypeId = model.paymentTypeId,
                        Price = totalItemPrice,
                        Total = totalOrderValue,
                        Vat = vatvalue,
                        UserId = UserId,
                        PromoCodeID = model.promoCodeID,
                        ReferenceNo = model.customerReference,
                        InvoiceId = model.invoiceId,

                    });


                    // التحقق من استخدام البرومو كود وتسجيل استخدامه
                    if (promoCodeDiscount > 0)
                    {
                        var PromoData = Uow.PromoCode.GetAll(s => s.PromoCodeID == model.promoCodeID).FirstOrDefault();
                        if (PromoData != null)
                        {
                            OrderData.OrderPromo.Add(
                              new OrderPromo()
                              {
                                  CreateDate = _bLGeneral.GetDateTimeNow(),
                                  DiscountType = PromoData.DiscountType,
                                  DiscountValue = PromoData.DiscountValue,
                                  IsDeleted = false,
                                  IsEnable = true,
                                  OrderPromoGuid = Guid.NewGuid(),
                                  PromoCodeID = PromoData.PromoCodeID,
                                  UserId = UserId,
                              });
                        }
                    }


                    decimal currentAmountwallet = 0;

                    // بدء تسجيل الطلبات على حدى
                    foreach (var item in model.vendorOrder)
                    {
                        decimal VatProduct = 0;
                        bool IsIncreaseQuantity = false;

                        List<OrderItems> orderItems = new List<OrderItems>();


                        // نوع كابتن الطلب
                        int CaptainType = 0;

                        // اجمالى المنتجات لمقدم الخدمه
                        decimal priceItemOrderVendor = 0;

                        // انشاء رقم الشحنه
                        var TrackNo = _bLGeneral.KeyGeneratorNumbersOnly(12);


                        // سعر التوصيل للطلب
                        decimal DeliveryPricePerOne = item.deliveryprice;

                        // ارجاع بيانات مقدم الخدمه
                        var vaendordata = Uow.Vendors.GetAll(s => s.VendorsID == item.vendorId).FirstOrDefault();

                        // قيمه الخصم للطلب الواحد ويتم حسابتها كا الاتى
                        // يتم تقسيم قيمه الخصم على عدد الطلبات
                        decimal discountOrder = 0;//promoCodeDiscount > 0 ? (promoCodeDiscount / model.vendorOrder.Count()) : 0;
                        // حساب اجمالى الطلب لمقدم الخدمه المحدد
                        foreach (var prod in item.products)
                        {
                            var ProdData = Uow.Product.GetAll(s => s.ProductID == prod.productId).FirstOrDefault();
                            if (ProdData != null)
                            {
                                if (ProdData.IsLimited)
                                {
                                    if (ProdData.DailyQuantity < prod.quantity)
                                    {
                                        IsIncreaseQuantity = true;
                                    }
                                }
                                priceItemOrderVendor = priceItemOrderVendor + ((ProdData.Price - _bLGeneral.CalcProductNet(ProdData.Discount, ProdData.StartDiscountDate, ProdData.EndDiscountDate, ProdData.Price)) * prod.quantity);
                            }
                        }

                        // حساب اجمالى الطلب شامل الضريبه
                        var priceOrderVendor = priceItemOrderVendor;


                        // التحقق من الباكدج الخاصه بمقدم الخدمه والرجوع بقيمتها
                        var pacakedata = Uow.Package.GetAll(s => !s.IsDeleted && s.PackageType == (int)PackageTypeEnum.Default).FirstOrDefault();
                        if (vaendordata.PackageID.HasValue)
                        {
                            pacakedata = Uow.Package.GetAll(s => !s.IsDeleted && s.PackageID == vaendordata.PackageID.Value).FirstOrDefault();
                        }

                        // احتساب سعر الباكدج 
                        decimal PackageAmount = pacakedata.Price;
                        decimal PackagePercentValue = (pacakedata.Percent * priceOrderVendor) / 100;
                        var totalPackageAmount = PackageAmount + PackagePercentValue;
                        decimal perhome = totalPackageAmount;
                        decimal perstore = (priceOrderVendor - totalPackageAmount);
                        // التحقق من سواق التوصيل هل متجر او هوم ميد
                        if (Uow.Vendors.GetAll(s => s.VendorsID == item.vendorId && s.DeliveryType == (int)DeliveryTypeEnum.Store).Any())
                        {
                            CaptainType = (int)CaptainTypeEnum.Store;

                            // حساب نسبه المتجر من الطلب 
                            //لو المتجر عنده سواق هيزيد في حقه سعر التوصيل بتاعه
                            perstore = perstore + DeliveryPricePerOne;
                            deliveryvatper = 0;
                            deliveryvatval = 0;
                        }
                        else
                        {
                            var disval = promoCodeDiscount > 0 ? (promoCodeDiscount / model.vendorOrder.Count()) : 0;
                            discountOrder = disval;
                            // حساب نسبه هوم ميد من الطلب
                            perhome = perhome - disval;
                            deliveryvatper = DeliveryVatSettingPercent;
                            deliveryvatval = DeliveryPricePerOne - (DeliveryPricePerOne / (1 + (_blConfiguration.GetDeliveryPriceVatPercent() / 100)));

                            CaptainType = (int)CaptainTypeEnum.Home_Made;
                        }

                        // حساب قيمه الضريبه المضافه للمنتجات
                        decimal vatorder = (priceOrderVendor * DeliveryVatSettingPercent) / 100; 
                                                                                                 // حساب اجمالى الطلب شامل الضريبه والخصم
                        priceOrderVendor = priceOrderVendor + vatorder;
                        // اجمالى الطلب بالاضافه لسعر التوصيل
                        priceItemOrderVendor = priceOrderVendor + DeliveryPricePerOne - discountOrder;

                        bool IsIncrease = false;
                        bool IsIncreaseItem = false;
                        foreach (var prodiem in item.products)
                        {
                            decimal Dicitem = discountOrder != 0 ? (discountOrder / item.products.Count()) : 0;
                            var ProdData = Uow.Product.GetAll(s => s.ProductID == prodiem.productId).FirstOrDefault();
                            if (ProdData != null)
                            {
                                var vatPriceViewModel = _blConfiguration.GetVatForPrice(ProdData.Price - _bLGeneral.CalcProductNet(ProdData.Discount, ProdData.StartDiscountDate, ProdData.EndDiscountDate, ProdData.Price));
                                totalItemsAfterDicount += ((ProdData.Price - _bLGeneral.CalcProductNet(ProdData.Discount, ProdData.StartDiscountDate, ProdData.EndDiscountDate, ProdData.Price)) * prodiem.quantity);
                                VatProduct += (vatPriceViewModel.VatValue * prodiem.quantity);

                                IsIncreaseItem = false;
                                if (prodiem.quantity > ProdData.DailyQuantity && ProdData.IsLimited)
                                {
                                    IsIncreaseItem = true;
                                }
                                orderItems.Add(new OrderItems()
                                {
                                    CreateDate = _bLGeneral.GetDateTimeNow(),
                                    Discount = Dicitem,
                                    IsDeleted = false,
                                    IsEnable = true,
                                    OrderItemsGuid = Guid.NewGuid(),
                                    PriceBeforeVat = ProdData.Price,
                                    VatValue = vatPriceViewModel.VatValue,
                                    Price = vatPriceViewModel.PriceWithVat,
                                    ProdImage = ProdData.Logo,
                                    ProdNameAr = ProdData.NameAr,
                                    ProdNameEn = ProdData.NameEn,
                                    ProductID = ProdData.ProductID,
                                    UserId = UserId,
                                    Quantity = prodiem.quantity,
                                    IsIncreaseItem = IsIncreaseItem,
                                });
                                if (ProdData.IsLimited)
                                {
                                    int quantity = Convert.ToInt32(ProdData.DailyQuantity);
                                    if (decimal.ToInt32(prodiem.quantity) > quantity)
                                    {
                                        IsIncrease = true;
                                    }
                                }

                            }
                        }


                        var customerData = Uow.Customers.GetAll(e => e.CustomersID == CustomersID).FirstOrDefault();
                        var listCustomerBalance_Obj = new List<CustomerBalance>();
                        CustomerBalance CustomerBalance_Obj = new CustomerBalance();
                        // التحقق من ان كانت طريقه الدفع هى المحفظه

                        CustomerBalance_Obj = null;
                        listCustomerBalance_Obj = null;

                        OrderData.OrderVendor.Add(new OrderVendor()
                        {
                            TotalBaseItems = totalItemsAfterDicount, 
                            VatValue = deliveryvatval + VatProduct,
                            VendorsID = item.vendorId,
                            CreateDate = _bLGeneral.GetDateTimeNow(),
                            DeliveryPrice = DeliveryPricePerOne,
                            Discount = (promoCodeDiscount > 0 ? (promoCodeDiscount / model.vendorOrder.Count()) : 0),
                            InvoiceImage = string.Empty,
                            IsDeleted = true,
                            IsEnable = true,
                            Notes = item.notes,
                            OrderStatusID = (int)OrderStatusEnum.Pending,
                            OrderVendorGuid = Guid.NewGuid(),
                            TrackNo = TrackNo,
                            Price = priceOrderVendor,
                            Total = priceItemOrderVendor,
                            UserId = UserId,
                            CaptainPaidID = (int)PaidStatusEnum.Created,
                            DriverCharge = 0,
                            CaptainTypeId = CaptainType,
                            InvoiceTypeId = (int)OrderInvoiceTypeEnum.Created,
                            PerStore = perstore,
                            PerHome = perhome,
                            PackageAmount = PackageAmount,
                            PackageID = pacakedata.PackageID,
                            PackagePercent = PackagePercentValue,
                            VatPercent = deliveryvatper,
                            DeliveryVatValue = deliveryvatval,
                            DistanceKM = item.distanceKM,
                            AddedFrom = addedFrom,
                            OrderTypeId = model.orderTypeId,
                            ApprovalQuantity = (int)ApprovalQuantityEnum.NotAction,
                            IsIncreaseQuantity = IsIncreaseQuantity,
                            VatProduct = VatProduct,
                            ScheduleDate = (addedFrom == "WEB" ? ScheduleDate : (model.scheduleDate != null ? (model.scheduleDate != "" ?
                            DateTime.ParseExact(model.scheduleDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)
                            : null) : null)),

                            ScheduleTime = (addedFrom == "WEB" ? ScheduleTime : (model.scheduleTime != null ? (model.scheduleTime != "" ?
                            DateTime.ParseExact(model.scheduleDate, "dd/MM/yyyy hh:mm tt", System.Globalization.CultureInfo.InvariantCulture)
                            : null) : null)),
                            OrderHistory = new List<OrderHistory>() {
                                new OrderHistory()
                                {
                                    CreateDate = _bLGeneral.GetDateTimeNow(),
                                    UserId = UserId,
                                    OrderStatusID = (int)OrderStatusEnum.Pending,
                                    IsDeleted = false,
                                    IsEnable = true,
                                    Lat = 0,
                                    Lng = 0,
                                    OrderHistoryGuid = Guid.NewGuid(),
                                }
                            },

                            OrderItems = orderItems,
                            CustomerBalance = listCustomerBalance_Obj,

                        });
                        if (IsIncrease)
                        {
                            res.listVendorsID.Add(item.vendorId);
                        }
                    }

                    Uow.Commit();

                    if (res.listVendorsID != null)
                    {
                        if (res.listVendorsID.Any())
                        {
                            var customerData = Uow.Customers.GetAll(e => e.CustomersID == CustomersID).FirstOrDefault();
                            foreach (var vendorsID in res.listVendorsID)
                            {
                                var vendor = Uow.Vendors.GetById(vendorsID);
                                if (vendor != null)
                                {
                                    var orderVendor = Uow.OrderVendor.GetAll(p => p.IsDeleted && p.OrdersID == OrderData.OrdersID && p.VendorsID == vendorsID, "Vendors").FirstOrDefault();
                                    if (orderVendor != null)
                                    {
                                        Uow.Notification.Insert(new Notification()
                                        {

                                            CreateDate = _bLGeneral.GetDateTimeNow(),
                                            TitleAR = "تم إرسال طلب زيادة كمية رقم " + orderVendor.OrderVendorID + " من خلال " + customerData.FirstName + "",
                                            TitleEN = "your order Increase Quantity has been send from " + customerData.FirstName + "",
                                            DescAR = "تم إرسال طلب زيادة الكمية رقم " + orderVendor.OrderVendorID + " من خلال " + customerData.FirstName + " في تاريخ " + _bLGeneral.GetDateTimeNow() + "",
                                            DescEN = "order Increase Quantity No " + orderVendor.OrderVendorID + " has been send from " + customerData.FirstName + " on date " + _bLGeneral.GetDateTimeNow() + "",
                                            IsDeleted = false,
                                            IsEnable = true,
                                            IsSeen = false,
                                            NotificationGuid = Guid.NewGuid(),
                                            NotificationTypeID = (int)NotificationTypeEnum.Order,
                                            UserId = UserId,
                                            NotificationDate = _bLGeneral.GetDateTimeNow(),
                                            OrderVendorID = orderVendor.OrderVendorID,
                                            VendorsID = vendor.VendorsID
                                        });
                                        var _PushListVendor = new PushList()
                                        {
                                            orderId = orderVendor.OrderVendorID,
                                            lat = 0,
                                            lng = 0,
                                            trackNo = "",
                                            profit = "",
                                            cusotmerAddress = "",
                                            vendorAddress = "",
                                            vendorLogo = "",
                                            vendorName = "",
                                            body = "تم إرسال طلب زيادة كمية رقم " + orderVendor.OrderVendorID + " من خلال " + customerData.FirstName + "",
                                            sound = "custom_sound.wav", //For IOS
                                            title = "طلب زيادة كمية",
                                            content_available = "true", //For Android
                                            priority = "high", //For Android,
                                            serverKey = FireBaseKey,
                                            estimation = 20,
                                            pushType = (int)PushTypeEnum.Pending
                                        };
                                        var UserDataVendor = _blTokens.GetMobileDataByUserID(orderVendor.Vendors.UserId);
                                        if (UserDataVendor != null)
                                        {
                                            _bLGeneral.SendPush(UserDataVendor.TokenVal, UserDataVendor.DeviceType, _PushListVendor);
                                        }
                                    }
                                }
                            }
                            Uow.Commit();
                        }
                    }


                    res.message = "true";
                    res.orderId = OrderData.OrdersID;

                    res.totalOrderValue = totalOrderValue;
                }
                else
                {
                    res.orderId = 0;
                    res.message = "لا يوجد طلبات من اى مزود خدمة";
                }
            }
            catch (Exception ex)
            {
                res.orderId = 0;
                res.message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }
            return res;
        }
        public AddOrderResponse ChangeOrder_New_Pending_To_Create(AddOrderViewModelApiNew model, int OrdersID, int UserId, int CustomersID, int status)
        {
            var res = new AddOrderResponse();
            decimal deliveryvatper = 0;
            decimal deliveryvatval = 0;
            decimal totalItemPrice = 0;
            decimal promoCodeDiscount = 0;
            decimal totalItemsAfterDicount = 0;

            try
            {
                // ارجاع قيمه الضريبه للتوصيل
                var DeliveryVatSettingPercent = _blConfiguration.GetDeliveryPriceVatPercent();

                if (model.vendorOrder.Count() > 0)
                {
                    // حساب قيمه المنتجات
                    foreach (var item in model.vendorOrder)
                    {
                        var VendCheck = Uow.Vendors.GetAll(s => s.VendorsID == item.vendorId).FirstOrDefault();

                        if (item.products.Count() > 0)
                        {
                            foreach (var prod in item.products)
                            {
                                var ProdData = Uow.Product.GetAll(s => s.ProductID == prod.productId).FirstOrDefault();
                                if (ProdData != null)
                                {
                                    totalItemPrice = totalItemPrice + ((ProdData.Price - _bLGeneral.CalcProductNet(ProdData.Discount, ProdData.StartDiscountDate, ProdData.EndDiscountDate, ProdData.Price)) * prod.quantity);
                                }
                            }

                        }
                        else
                        {
                            res.orderId = 0;
                            res.message = "لا يوجد طلبات مختارة من المتجر " + VendCheck.StoreNameAr + "";
                            return res;
                        }

                        #region Working_Times
                        if (VendCheck.IsDaysWork)//يجب ان يقوم المتجر بتحديد اوقات العمل
                        {
                            var date = DateTime.Now; //اذا كان الطلب فوري

                            var DaysWorking = VendCheck.DaysWork.Split(","); // ايام العمل الرسمية
                            if (DaysWorking.Any(s => int.Parse(s) == (int)date.DayOfWeek)) //اذا كان الطلب في يوم من ايام العمل الرسمية
                            {
                                if (VendCheck.WorkFrom.HasValue && VendCheck.WorkTo.HasValue)// اذا تم تحديد وقت معين للعمل في ايام العمل الرسمية
                                {
                                    if (VendCheck.WorkFrom.Value.TimeOfDay < VendCheck.WorkTo.Value.TimeOfDay)
                                    {
                                        if (VendCheck.WorkFrom.Value.TimeOfDay < date.TimeOfDay && VendCheck.WorkTo.Value.TimeOfDay > date.TimeOfDay)
                                        {
                                            res.message = "المتجر مفتوح";
                                        }
                                        else
                                        {
                                            res.orderId = 0;
                                            res.message = "عفوا المتجر " + VendCheck.StoreNameAr + " تم انتهاء وقت العمل الخاص به اليوم";
                                            return res;
                                        }
                                    }
                                    else
                                    {
                                        if (VendCheck.WorkFrom.Value.TimeOfDay < date.TimeOfDay || VendCheck.WorkTo.Value.TimeOfDay > date.TimeOfDay)
                                        {
                                            res.message = "المتجر مفتوح";
                                        }
                                        else
                                        {
                                            res.orderId = 0;
                                            res.message = "عفوا المتجر " + VendCheck.StoreNameAr + " تم انتهاء وقت العمل الخاص به اليوم";
                                            return res;
                                        }
                                    }
                                }
                            }
                            else if (VendCheck.IsDaysVac) //اذا اضاف المتجر اوقات عمل في ايام الاجازة
                            {
                                var DaysVac = VendCheck.DaysVac.Split(",");//ايام الاجازة التى تم تحديدها
                                if (DaysVac.Any(s => int.Parse(s) == (int)date.DayOfWeek))//اذا تم تحديد اوقات للعمل في يوم الاجازة
                                {
                                    if (VendCheck.VacWorkFrom.HasValue && VendCheck.VacWorkTo.HasValue)//اذا تم تحديد وقت معين للعمل في يوم الاجازة
                                    {
                                        if (VendCheck.VacWorkFrom.Value.TimeOfDay < VendCheck.VacWorkTo.Value.TimeOfDay)
                                        {
                                            if (VendCheck.VacWorkFrom.Value.TimeOfDay < date.TimeOfDay && VendCheck.VacWorkTo.Value.TimeOfDay > date.TimeOfDay)
                                            {
                                                res.message = "المتجر مفتوح";
                                            }
                                            else
                                            {
                                                res.orderId = 0;
                                                res.message = "عفوا المتجر " + VendCheck.StoreNameAr + " تم انتهاء وقت العمل الخاص به اليوم";
                                                return res;
                                            }
                                        }
                                        else
                                        {
                                            if (VendCheck.VacWorkFrom.Value.TimeOfDay < date.TimeOfDay || VendCheck.VacWorkTo.Value.TimeOfDay > date.TimeOfDay)
                                            {
                                                res.message = "المتجر مفتوح";
                                            }
                                            else
                                            {
                                                res.orderId = 0;
                                                res.message = "عفوا المتجر " + VendCheck.StoreNameAr + " تم انتهاء وقت العمل الخاص به اليوم";
                                                return res;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    res.orderId = 0;
                                    res.message = "عفوا المتجر " + VendCheck.StoreNameAr + " غير متاح اليوم";
                                    return res;
                                }
                            }
                            else
                            {
                                res.orderId = 0;
                                res.message = "عفوا المتجر " + VendCheck.StoreNameAr + " غير متاح لتجهيز طلباتكم الان";
                                return res;
                            }
                        }
                        else
                        {
                            res.orderId = 0;
                            res.message = "لا يمكن الطلب من المتجر " + VendCheck.StoreNameAr + " لأنه لم يحدد اوقات العمل";
                            return res;
                        }
                        #endregion

                    }



                    // حساب اجمالى التوصيل
                    var totalOrderValue = totalItemPrice;

                    // حساب ضريبه القيمه المضافه للمنتجات
                    var vatvalue = (DeliveryVatSettingPercent * totalOrderValue) / 100;

                    // حساب اجمالى الطلب مع ضريبه القيمه المضافه للمنتجات
                    totalOrderValue = totalOrderValue + model.vendorOrder.Sum(e => e.deliveryprice) + vatvalue;

                    // ألتحقق من برومو كود الخصم وارجاع قيمته

                    if (model.promoCodeID != null)
                    {
                        var PromoData = Uow.PromoCode.GetAll(s => s.PromoCodeID == model.promoCodeID).FirstOrDefault();
                        if (PromoData != null)
                        {
                            var Pro = CheckPromoCodeValid(PromoData.Code, CustomersID, totalOrderValue, "", 1);
                            promoCodeDiscount = Pro.discount;
                        }
                    }

                    // حساب اجمالى الطلب مع الخصم
                    totalOrderValue = totalOrderValue - promoCodeDiscount;

                    // التحقق

                    // انشاء ماستر الطلب
                    var orders = GetOrdersById(OrdersID);
                    if (orders != null)
                    {
                        orders.OrderStatusID = status;
                        orders.PaymentTypeId = model.paymentTypeId;
                        orders.UserId = UserId;
                        orders.Total = totalOrderValue;
                        orders.Price = totalItemPrice;
                        orders.DeliveryPrice = model.vendorOrder.Sum(e => e.deliveryprice);
                        orders.Discount = promoCodeDiscount;
                        orders.UpdateDate = _bLGeneral.GetDateTimeNow();
                        orders.InvoiceId = model.invoiceId;
                        Uow.Orders.Update(orders);
                    }


                    decimal currentAmountwallet = 0;

                    var orderVendors = Uow.OrderVendor.GetAll(x => x.OrdersID == OrdersID);
                    if (model.vendorOrder.Count() > 0)
                    {
                        foreach (var item in orderVendors)
                        {
                            decimal VatProduct = 0;
                            // نوع كابتن الطلب
                            int CaptainType = 0;

                            // اجمالى المنتجات لمقدم الخدمه
                            decimal priceItemOrderVendor = 0;

                            // انشاء رقم الشحنه
                            var TrackNo = _bLGeneral.KeyGeneratorNumbersOnly(12);


                            // سعر التوصيل للطلب
                            decimal DeliveryPricePerOne = model.vendorOrder.Where(x => x.vendorId == item.VendorsID).FirstOrDefault().deliveryprice;  //model.vendorOrder.Where(x => x.vendorId == item.VendorsID).Any() ? model.vendorOrder.Where(x => x.vendorId == item.VendorsID).FirstOrDefault().deliveryprice : 0;

                            // ارجاع بيانات مقدم الخدمه
                            var vaendordata = Uow.Vendors.GetAll(s => s.VendorsID == item.VendorsID).FirstOrDefault();

                            // قيمه الخصم للطلب الواحد ويتم حسابتها كا الاتى
                            // يتم تقسيم قيمه الخصم على عدد الطلبات
                            decimal discountOrder = 0;//promoCodeDiscount > 0 ? (promoCodeDiscount / model.vendorOrder.Count()) : 0;
                                                      // حساب اجمالى الطلب لمقدم الخدمه المحدد
                            var products = model.vendorOrder.Where(x => x.vendorId == item.VendorsID).Any() ? model.vendorOrder.Where(x => x.vendorId == item.VendorsID).FirstOrDefault().products : null;
                            foreach (var prod in products)
                            {
                                var ProdData = Uow.Product.GetAll(s => s.ProductID == prod.productId).FirstOrDefault();
                                if (ProdData != null)
                                {
                                    priceItemOrderVendor = priceItemOrderVendor + ((ProdData.Price - _bLGeneral.CalcProductNet(ProdData.Discount, ProdData.StartDiscountDate, ProdData.EndDiscountDate, ProdData.Price)) * prod.quantity);
                                }
                            }

                            // حساب اجممالى الطلب شامل الضريبه
                            var priceOrderVendor = priceItemOrderVendor;

                            // التحقق من الباكدج الخاصه بمقدم الخدمه والرجوع بقيمتها
                            var pacakedata = Uow.Package.GetAll(s => !s.IsDeleted && s.PackageType == (int)PackageTypeEnum.Default).FirstOrDefault();
                            if (vaendordata.PackageID.HasValue)
                            {
                                pacakedata = Uow.Package.GetAll(s => !s.IsDeleted && s.PackageID == vaendordata.PackageID.Value).FirstOrDefault();
                            }
                            // احتساب سعر الباكدج 
                            decimal PackageAmount = pacakedata.Price;
                            decimal PackagePercentValue = (pacakedata.Percent * priceOrderVendor) / 100;
                            var totalPackageAmount = PackageAmount + PackagePercentValue;
                            decimal perhome = totalPackageAmount;
                            decimal perstore = (priceOrderVendor - totalPackageAmount);
                            // التحقق من سواق التوصيل هل متجر او هوم ميد
                            if (Uow.Vendors.GetAll(s => s.VendorsID == item.VendorsID && s.DeliveryType == (int)DeliveryTypeEnum.Store).Any())
                            {
                                CaptainType = (int)CaptainTypeEnum.Store;

                                // حساب نسبه المتجر من الطلب 
                                //لو المتجر عنده سواق هيزيد في حقه سعر التوصيل بتاعه
                                perstore = perstore + DeliveryPricePerOne;
                                deliveryvatper = 0;
                                deliveryvatval = 0;
                            }
                            else
                            {
                                var disval = promoCodeDiscount > 0 ? (promoCodeDiscount / model.vendorOrder.Count()) : 0;
                                discountOrder = disval;
                                // حساب نسبه هوم ميد من الطلب
                                perhome = perhome - disval;
                                deliveryvatper = DeliveryVatSettingPercent;
                                deliveryvatval = DeliveryPricePerOne - (DeliveryPricePerOne / (1 + (_blConfiguration.GetDeliveryPriceVatPercent() / 100)));
                                CaptainType = (int)CaptainTypeEnum.Home_Made;

                            }

                            // حساب قيمه الضريبه المضافه للمنتجات
                            decimal vatorder = (priceOrderVendor * DeliveryVatSettingPercent) / 100;
                            // حساب اجمالى الطلب شامل الضريبه والخصم
                            priceOrderVendor = priceOrderVendor + vatorder;
                            // اجمالى الطلب بالاضافه لسعر التوصيل
                            priceItemOrderVendor = priceOrderVendor + DeliveryPricePerOne - discountOrder;

                            Uow.OrderHistory.Insert(new OrderHistory()
                            {
                                CreateDate = _bLGeneral.GetDateTimeNow(),
                                UserId = UserId,
                                OrderStatusID = status,
                                IsDeleted = false,
                                IsEnable = true,
                                Lat = 0,
                                Lng = 0,
                                OrderHistoryGuid = Guid.NewGuid(),
                                OrderVendorID = item.OrderVendorID,
                            });
                            var orderItems = Uow.OrderItems.GetAll(x => !x.IsDeleted && x.OrderVendorID == item.OrderVendorID);
                            foreach (var prodiem in orderItems)
                            {
                                decimal Dicitem = discountOrder != 0 ? (discountOrder / orderItems.Count()) : 0;

                                var ProdData = Uow.Product.GetAll(s => s.ProductID == prodiem.ProductID).FirstOrDefault();
                                if (ProdData != null)
                                {
                                    var vatPriceViewModel = _blConfiguration.GetVatForPrice(ProdData.Price - _bLGeneral.CalcProductNet(ProdData.Discount, ProdData.StartDiscountDate, ProdData.EndDiscountDate, ProdData.Price));
                                    totalItemsAfterDicount +=
                                        ((ProdData.Price -
                                        _bLGeneral.CalcProductNet(ProdData.Discount, ProdData.StartDiscountDate, ProdData.EndDiscountDate, ProdData.Price))
                                        * prodiem.Quantity);
                                    VatProduct += (vatPriceViewModel.VatValue * prodiem.Quantity);
                                    if (ProdData.IsLimited)
                                    {

                                        ProdData.DailyQuantity = (ProdData.DailyQuantity - prodiem.Quantity) < 0 ? 0 : (ProdData.DailyQuantity - prodiem.Quantity);
                                        ProdData.Discount = Dicitem;
                                        Uow.Product.Update(ProdData);
                                    }
                                }
                            }

                            var customerData = Uow.Customers.GetAll(e => e.CustomersID == CustomersID).FirstOrDefault();
                            var listCustomerBalance_Obj = new List<CustomerBalance>();
                            CustomerBalance CustomerBalance_Obj = new CustomerBalance();
                            // التحقق من ان كانت طريقه الدفع هى المحفظه
                            if (model.paymentTypeId == (int)PaymentTypeEnum.Wallet)
                            {
                                var CustomerBalance = _BlCustomerBalance.GetLastBlance(CustomersID);
                                if (totalOrderValue > CustomerBalance)
                                {
                                    res.orderId = 0;
                                    res.message = "عفوا المبلغ المتواجد في المحفظة اقل من اجمالى الطلبات";
                                    return res;
                                }

                                var disval = promoCodeDiscount > 0 ? (promoCodeDiscount / model.vendorOrder.Count()) : 0;

                                var amountwallet = priceItemOrderVendor;
                                amountwallet = amountwallet <= 0 ? 0 : amountwallet;


                                var Before = CustomerBalance - currentAmountwallet;
                                CustomerBalance_Obj = (new CustomerBalance()
                                {
                                    Amount = amountwallet,
                                    Before = Before,
                                    CreateDate = _bLGeneral.GetDateTimeNow(),
                                    CustomersID = CustomersID,
                                    IsDeleted = false,
                                    TransactionTypeID = (int)TransactionTypeEnum.Pay_Order,
                                    TypeOperationID = (int)TypeOperationEnum.DR,
                                    UserId = UserId,
                                    After = ((Before - amountwallet)),
                                    CustomerBlanceGuid = Guid.NewGuid(),
                                    DateOperation = _bLGeneral.GetDateTimeNow(),
                                    Attachment = "",
                                    Discripe = "الطلب مجانى لان قيمه الخصم اكبر من قيمه الطلب رقم البرومو كود " + model.promoCodeID + "  وقيمه الخصم " + promoCodeDiscount + ""
                                });
                                currentAmountwallet += amountwallet;
                                listCustomerBalance_Obj.Add(CustomerBalance_Obj);

                            }
                            else
                            {
                                CustomerBalance_Obj = null;
                                listCustomerBalance_Obj = null;
                            }
                            item.CustomerBalance = listCustomerBalance_Obj;
                            item.OrderStatusID = status;
                            item.UserId = UserId;
                            item.UpdateDate = _bLGeneral.GetDateTimeNow();
                            item.IsDeleted = false;
                            item.DeliveryPrice = DeliveryPricePerOne;
                            item.Discount = (promoCodeDiscount > 0 ? (promoCodeDiscount / model.vendorOrder.Count()) : 0);
                            item.Price = priceOrderVendor;
                            item.Total = priceItemOrderVendor;
                            item.DeliveryVatValue = deliveryvatval;
                            item.VatValue = deliveryvatval + VatProduct;
                            item.DistanceKM = model.vendorOrder.Where(x => x.vendorId == item.VendorsID).FirstOrDefault().distanceKM;
                            item.TotalBaseItems = totalItemsAfterDicount;
                            item.PerStore = perstore;
                            item.PerHome = perhome;
                            item.PackageAmount = PackageAmount;
                            item.PackagePercent = PackagePercentValue;
                            Uow.OrderVendor.Update(item);
                            if (status == (int)OrderStatusEnum.Create)
                            {
                                Uow.Notification.Insert(new Notification()
                                {
                                    CreateDate = _bLGeneral.GetDateTimeNow(),
                                    TitleAR = "تم إرسال الطلب رقم " + item.OrderVendorID + " من خلال " + customerData.FirstName + "",
                                    TitleEN = "your order has been send from " + customerData.FirstName + "",
                                    DescAR = "تم إرسال الطلب رقم " + item.OrderVendorID + " من خلال " + customerData.FirstName + " في تاريخ " + _bLGeneral.GetDateTimeNow() + "",
                                    DescEN = "order No " + item.OrderVendorID + " has been send from " + customerData.FirstName + " on date " + _bLGeneral.GetDateTimeNow() + "",
                                    IsDeleted = false,
                                    IsEnable = true,
                                    IsSeen = false,
                                    NotificationGuid = Guid.NewGuid(),
                                    NotificationTypeID = (int)NotificationTypeEnum.Order,
                                    UserId = UserId,
                                    NotificationDate = _bLGeneral.GetDateTimeNow(),
                                    OrderVendorID = item.OrderVendorID,
                                    VendorsID = item.VendorsID
                                });
                            }

                        }
                    }
                    Uow.Commit();


                    res.message = "true";
                    res.orderId = OrdersID;
                }
                else
                {
                    res.orderId = 0;
                    res.message = "لا يوجد طلبات من اى مزود خدمة";
                }
            }
            catch (Exception ex)
            {
                res.orderId = 0;
                res.message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }
            return res;
        }

        #endregion
        #region Paid Online
        public bool IsOrderExistByInvoiceId(string invoiceId)
        {
            return Uow.Orders.GetAll(e => e.InvoiceId == invoiceId).Any();

        }
        #endregion     
        #region New_Map
        public class LocationMapResponse
        {
            public int RegionId { get; set; }
            public int BlockId { get; set; }
            public int CityId { get; set; }
            public string Address { get; set; }
            public string CityName { get; set; }
            public string BlockName { get; set; }
            public string RegionName { get; set; }
        }
        public LocationMapResponse GetLocationbyMap(string CityName, string areaName, string Blok_place_id, string BlockName, string Lang, int userId)
        {
            CityName = CityName.Replace("Al ", "").Replace("Ash ", "").Replace("Ad ", "").Replace("al ", "").Replace("ad ", "").Replace("ash ", "").Replace("ال", "");
            var objCityCheck = Uow.City.GetAll().Where(e => (e.NameAR == CityName || e.NameEN == CityName)).FirstOrDefault();
            areaName = areaName.Replace("al ", "").Replace("Al ", "").Replace("as ", "").Replace("As ", "").Replace("ar ", "").Replace("Ar ", "").Replace("ad ", "").Replace("Ad ", "").Replace("at ", "").Replace("At ", "").Replace("ash ", "").Replace("Ash ", "").Replace("an ", "").Replace("An ", "").Replace("az ", "").Replace("Az ", "").Replace("حي ", "");
            BlockName = BlockName.Replace("al ", "").Replace("Al ", "").Replace("as ", "").Replace("As ", "").Replace("ar ", "").Replace("Ar ", "").Replace("ad ", "").Replace("Ad ", "").Replace("at ", "").Replace("At ", "").Replace("ash ", "").Replace("Ash ", "").Replace("an ", "").Replace("An ", "").Replace("az ", "").Replace("Az ", "").Replace("حي ", "").Replace("'", "").Replace("ال", "");
            var objCity = Uow.City.GetAll(e => !e.IsDeleted, "Region,CitiesCovered").Where(e => (e.NameAR == CityName || e.NameEN == CityName || e.NameEN.Replace("ال", "") == CityName ||
              e.NameEN == CityName.Replace("ال", "") || e.NameAR.Replace("ال", "") == CityName || e.NameAR == CityName.Replace("ال", ""))).FirstOrDefault();
            if (objCity == null)
            {
                return null;
            }
            else
            {
                if (objCity.CitiesCovered.Any(s => !s.IsDeleted/* && s.IsEnable*/))
                {
                    var BlockData = Uow.Block.GetAll().Where(e => (e.NameAR.Equals(BlockName) || e.NameAR.Replace("ال", "") == BlockName || e.NameAR == BlockName.Replace("'", "") ||
                    e.NameEN == BlockName.Replace("ال", "") || e.NameEN == BlockName.Replace("ال", "")
                    || e.NameAR == BlockName.Replace("ال", "") || e.NameEN.Equals(BlockName)) && e.CityID == objCity.CityID).FirstOrDefault();
                    if (BlockData == null)
                    {
                        BlockData = new Model.Setting.Block()
                        {
                            BlockGuid = Guid.NewGuid(),
                            NameEN = areaName,
                            NameAR = areaName,
                            CityID = objCity.CityID,
                            Code = Blok_place_id,
                            CreateDate = _bLGeneral.GetDateTimeNow(),
                            EnableDate = _bLGeneral.GetDateTimeNow(),
                            IsEnable = true,
                            IsDeleted = false,
                            UserId = userId
                        };
                        BlockData = Uow.Block.Insert(BlockData);
                        Uow.Commit();
                    }
                    var response = new LocationMapResponse();
                    response.CityId = objCity.CityID;
                    response.RegionId = objCity.RegionID;
                    response.BlockId = BlockData.BlockID;
                    response.CityName = Lang == "ar" ? objCity.NameAR : objCity.NameEN;
                    response.RegionName = Lang == "ar" ? objCity.Region.NameAR : objCity.Region.NameEN;
                    response.BlockName = Lang == "ar" ? BlockData.NameAR : BlockData.NameEN;
                    return response;
                }
                else
                {
                    return null;
                }
            }
        }
        public CartDetailsViewModelApi GeCartDetailsViewModelApiByOrder(int OrdersID, string MainPathView, string productPath, string lang)
        {
            CartDetailsViewModelApi cartDetailsViewModelApi = new CartDetailsViewModelApi();
            cartDetailsViewModelApi.listVendors = Uow.OrderVendor.GetAll(e => e.OrdersID == OrdersID && e.IsDeleted, "Vendors,OrderItems").Select(p => new CartVendorViewModel()
            {
                vendorId = p.VendorsID,
                vendorName = lang == "ar" ? !string.IsNullOrWhiteSpace(p.Vendors.StoreNameAr) ? p.Vendors.StoreNameAr : "--" :
                !string.IsNullOrWhiteSpace(p.Vendors.StoreNameEn) ? p.Vendors.StoreNameEn : "--",
                vendorImage = !string.IsNullOrEmpty(p.Vendors.Logo) ? (MainPathView + p.Vendors.Logo) : "https://cdn.made-home.com/Home/beet_logo.png",
                products = p.OrderItems.Where(e => !e.IsDeleted).Select(e => new CartItemsViewModel()
                {
                    productId = e.ProductID,
                    name = lang == "ar" ? e.ProdNameAr : e.ProdNameEn,
                    price = e.Price,
                    count = e.Quantity,
                    availableQuantity = e.Product.DailyQuantity,
                    isIncreaseItem = e.IsIncreaseItem,
                    productImage = !string.IsNullOrEmpty(e.ProdImage) ? (productPath + e.ProdImage) : "https://cdn.made-home.com/Home/beet_logo.png",
                }).ToList(),
                approvalQuantity = p.ApprovalQuantity,

            }).ToList();
            return cartDetailsViewModelApi;
        }

        #endregion
    }
}
