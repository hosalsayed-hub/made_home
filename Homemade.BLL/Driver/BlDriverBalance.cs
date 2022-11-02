using Homemade.BLL.General;
using Homemade.BLL.ViewModel.Driver;
using Homemade.BLL.ViewModel.Driver.STC;
using Homemade.DAL.Contract;
using Homemade.Model.Driver;
using Homemade.Model.Order;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Homemade.BLL.Driver
{
   public class BlDriverBalance 
    {
        #region Decleration
        private readonly IUOW Uow;
        private readonly BlDriver _blDriver;
        private readonly BLGeneral _bLGeneral;
        private readonly BlTransactionType _blTransactionType;

        public BlDriverBalance(IUOW _uow, BlDriver blDriver, BLGeneral bLGeneral, BlTransactionType blTransactionType)
        {
            _bLGeneral = bLGeneral;
            _blDriver = blDriver;
            _blTransactionType = blTransactionType;
            this.Uow = _uow;
        }
        #endregion
        #region Actions
        //اخر مبلغ للسائق
        public decimal GetLastBlance(int DriverID)
        {
            var Balance = Uow.DriverBlance.GetAll(e => e.DriversID == DriverID);
            if (Balance.Count() > 0)
            {
                var last =
                    Math.Round(Balance.Where(s => s.TypeOperationID == (int)TypeOperationEnum.CR/* && s.IsPaid*/).Sum(e => e.Amount), 2)
                    -
                    Math.Round(Balance.Where(s => s.TypeOperationID == (int)TypeOperationEnum.DR/* && s.IsPaid*/).Sum(e => e.Amount), 2);
                return Math.Round(last, 2);
            }
            else
            {
                return 0;
            }
        }
        public DriverBlanceViewModel GetDriverBlanceViewModelForIndex()
        {
            DriverBlanceViewModel driverBlanceViewModel = new DriverBlanceViewModel
            {
                LstDrivers = _blDriver.GetAllDriverByAgentOperator().ToList(),
            };
            return driverBlanceViewModel;
        }
        public DriverBlanceViewModel GetDriverBlanceViewModelForCreate()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            DriverBlanceViewModel driverBlanceViewModel = new DriverBlanceViewModel
            {
                LstDrivers = _blDriver.GetAllDriverByAgentOperator().ToList(),
                LstTransactionType = _blTransactionType.GetAllTransactionType().Where(x => x.TransactionTypeID == (int)TransactionTypeEnum.Deposit ||
                x.TransactionTypeID == (int)TransactionTypeEnum.Withdrawal || x.TransactionTypeID == (int)TransactionTypeEnum.Bouns ||
                x.TransactionTypeID == (int)TransactionTypeEnum.Punishment).ToList(),
            };
            return driverBlanceViewModel;
        }
        public IEnumerable<TotalBalanceViewModel> GetAllBalances()
        {
            var TotalBalance = Uow.DriverBlance.GetAll(x => !x.IsDeleted, "Drivers").OrderByDescending(a => a.CreateDate).ToList().GroupBy(x => x.DriversID).Select(m => new TotalBalanceViewModel()
            {
                DriverID = m.Key,
                DriverNameAr = m.FirstOrDefault().Drivers.NameAr,
                DriverNameEn = m.FirstOrDefault().Drivers.NameEn,
                TotalCredit = m.Where(x => x.TypeOperationID == (int)TypeOperationEnum.CR).Sum(y => y.Amount),
                TotalDebit = m.Where(x => x.TypeOperationID == (int)TypeOperationEnum.DR).Sum(y => y.Amount),
                NetBalance = (m.Where(x => x.TypeOperationID == (int)TypeOperationEnum.CR).Sum(y => y.Amount)) - (m.Where(x => x.TypeOperationID == (int)TypeOperationEnum.DR).Sum(y => y.Amount)),
            });
            return TotalBalance;
        }
        public decimal GetLastTripsCharge(int DriverID)
        {
            var Trips = Uow.OrderVendor.GetAll(e => e.CaptainPaidID == (int)PaidStatusEnum.Created && e.DriversID == DriverID && e.OrderStatusID == (int)OrderStatusEnum.Delivered);
            if (Trips.Any())
            {
                return Trips.Sum(s => s.DriverCharge);
            }
            return 0;
        }
        public decimal GettripsCharge(List<int> trips)
        {
            if (trips.Any())
            {
                return Uow.OrderVendor.GetAll(e => trips.Any(s => s == e.OrderVendorID)).Sum(x => x.DriverCharge);
            }
            return 0;
        }
        public List<int> GetListTrips(int DriverID)
        {
            var Trips = Uow.OrderVendor.GetAll(e => e.CaptainPaidID == (int)PaidStatusEnum.Created && e.DriverCharge > 0 && e.DriversID == DriverID && e.OrderStatusID == (int)OrderStatusEnum.Delivered);
            if (Trips.Any())
            {
                return Trips.Select(s => s.OrderVendorID).ToList();
            }
            return null;
        }
        //إضافة الماليات للسائق
        public DriverBlance InsertBalanceWithOutCommit(int oPerationType, decimal amount, int transactionType, int driverId, int userId, int? tripId, DateTime DateOperation, string Descripe, string Attacment)
        {
            var Before = GetLastBlance(driverId);
            var DriverBlance = new DriverBlance()
            {
                Amount = amount,
                Before = Before,
                CreateDate = _bLGeneral.GetDateTimeNow(),
                DriversID = driverId,
                IsDeleted = false,
                TransactionTypeID = transactionType,
                TypeOperationID = oPerationType,
                UserId = userId,
                After = oPerationType == (int)TypeOperationEnum.CR ? (amount + Before) : (Before - amount),
                DriverBlanceGuid = Guid.NewGuid(),
                OrderVendorID = tripId,
                DateOperation = DateOperation,
                Attachment = Attacment,
                Discripe = Descripe
            };
            DriverBlance = Uow.DriverBlance.Insert(DriverBlance);
            return DriverBlance;
        }
        public DriverBlance InsertBalance(int oPerationType, decimal amount, int transactionType, int driverId, int userId, int? tripId, DateTime DateOperation, string Descripe, string Attacment)
        {
            var Before = GetLastBlance(driverId);
            var DriverBlance = new DriverBlance()
            {
                Amount = amount,
                Before = Before,
                CreateDate = _bLGeneral.GetDateTimeNow(),
                DriversID = driverId,
                IsDeleted = false,
                TransactionTypeID = transactionType,
                TypeOperationID = oPerationType,
                UserId = userId,
                After = oPerationType == (int)TypeOperationEnum.CR ? (amount + Before) : (Before - amount),
                DriverBlanceGuid = Guid.NewGuid(),
                OrderVendorID = tripId,
                DateOperation = DateOperation,
                Attachment = Attacment,
                Discripe = Descripe
            };
            DriverBlance = Uow.DriverBlance.Insert(DriverBlance);
            Uow.Commit();
            return DriverBlance;
        }
        public IQueryable<DriverBlanceViewModel> GetAllDriverBlanceViewModel(string[] ListCountryID, string[] ListAgentID, string[] ListCityID, int? DriverID, string TaxiNumber, DateTime? FromDate, DateTime? ToDate, int? ModelID, int? BrandID, int? IsEnable, bool? IsActive, string accLanguage, string fileView, string driverMobile, string tripNo)
        {
            if (ListCountryID != null)
            {
                if (ListCountryID.Count(p => !string.IsNullOrEmpty(p)) == 0)
                {
                    ListCountryID = null;
                }
            }
            if (ListAgentID != null)
            {
                if (ListAgentID.Count(p => !string.IsNullOrEmpty(p)) == 0)
                {
                    ListAgentID = null;
                }
            }
            if (ListCityID != null)
            {
                if (ListCityID.Count(p => !string.IsNullOrEmpty(p)) == 0)
                {
                    ListCityID = null;
                }
            }
            var data = Uow.DriverBlance.GetAll(x => !x.IsDeleted
                  && (ListCountryID != null ? ListCountryID.Any(y => y == x.Drivers.City.Region.Country.ToString()) : true)
              && (ListCityID != null ? ListCityID.Any(y => y == x.Drivers.CityID.ToString()) : true)
                && (IsActive != null ? x.Drivers.IsActive : true)
            && (!string.IsNullOrEmpty(TaxiNumber) ? x.Drivers.CarNumber.Contains(TaxiNumber) : true)
             && (DriverID != null ? DriverID == x.DriversID : true)
             && (FromDate != null ? FromDate.Value.Date <= x.DateOperation.Date : true)
             && (ToDate != null ? ToDate.Value.Date >= x.DateOperation.Date : true)
             && (driverMobile != null ? driverMobile == x.Drivers.PhoneNumber : true)
             && (tripNo != null ? tripNo == x.OrderVendorID.ToString() : true)
            ).OrderByDescending(a => a.CreateDate).Select(m => new DriverBlanceViewModel()
            {
                After = m.After,
                Amount = m.Amount,
                Attachment = !String.IsNullOrEmpty(m.Attachment) ? fileView + m.Attachment : string.Empty,
                Before = m.Before,
                DateOperation = m.DateOperation,
                Discripe = m.Discripe,
                DriverBlanceGuid = m.DriverBlanceGuid,
                DriverBlanceID = m.DriverBlanceID,
                DriverID = m.DriversID,
                TransactionTypeID = m.TransactionTypeID,
                TypeOperationID = m.TypeOperationID,
                DriverName = accLanguage == "ar" ? m.Drivers.NameAr : m.Drivers.NameEn,
                TransactionTypeName = accLanguage == "ar" ? m.TransactionType.NameAR : m.TransactionType.NameEN,
                OrderVendorID = m.OrderVendorID,
                OrderVendorGuid = m.OrderVendorID != null ? m.OrderVendor.OrderVendorGuid : Guid.Empty,
                OrderNumber = m.OrderVendorID != null ? m.OrderVendor.TrackNo : "--"
            });
            return data;
        }
        public List<DriverBlanceViewModel> GetAllDriverBlanceViewModelPay(string[] ListCountryID, string[] ListAgentID, string[] ListCityID, int? DriverID, string TaxiNumber, DateTime? FromDate, DateTime? ToDate, int? ModelID, int? BrandID, int? IsEnable, bool? IsActive, string accLanguage, string fileView, string driverMobile, string tripNo, decimal loast)
        {
            var lis = new List<DriverBlanceViewModel>();
            if (ListCountryID != null)
            {
                if (ListCountryID.Count(p => !string.IsNullOrEmpty(p)) == 0)
                {
                    ListCountryID = null;
                }
            }
            if (ListAgentID != null)
            {
                if (ListAgentID.Count(p => !string.IsNullOrEmpty(p)) == 0)
                {
                    ListAgentID = null;
                }
            }
            if (ListCityID != null)
            {
                if (ListCityID.Count(p => !string.IsNullOrEmpty(p)) == 0)
                {
                    ListCityID = null;
                }
            }
            var data = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.CaptainPaidID == (int)PaidStatusEnum.Created && (x.OrderStatusID == (int)OrderStatusEnum.Delivered || x.OrderStatusID == (int)OrderStatusEnum.Cancel) &&  x.DriverCharge > 0 
                && (IsActive != null ? (IsActive == true ? x.Drivers.VerifyStc == (int)VerifyStcEnum.Verified : x.Drivers.VerifyStc != (int)VerifyStcEnum.Verified) : true)
            && (!string.IsNullOrEmpty(TaxiNumber) ? x.Drivers.CarNumber.Contains(TaxiNumber) : true)
             && (DriverID != null ? DriverID == x.DriversID : true)
             && (driverMobile != null ? driverMobile == x.Drivers.PhoneNumber : true)
           , "Drivers").OrderByDescending(a => a.CreateDate).Select(m => new DriverBlanceViewModel()
           {
               After = 0,
               Amount = Math.Round(m.DriverCharge, 2),
               Before = 0,
               DateOperation = m.CreateDate,
               DriverID = m.DriversID.Value,
               DriverName = accLanguage == "ar" ? m.Drivers.NameAr : m.Drivers.NameEn,
               MobileNo = m.Drivers.PhoneNumber,
               OrderVendorID = m.OrderVendorID,
               OrderVendorGuid = m.OrderVendorGuid,
               OrderNumber = m.TrackNo,
               VerifyStc = m.Drivers.VerifyStc,
               OpenTransaction = m.Drivers.OpenTransaction,
           });
            foreach (var item in data.Where(e => e.Amount > 0))
            {
                item.After = GetLastTripsCharge(item.DriverID);
                lis.Add(item);
            }
            return lis.Where(e => e.After >= loast).ToList();
        }
        public Notification InsertNotification(int userId,int? driverID, string notificationTitleEN, string notificationTitleAR,string notificationDescEN, string notificationDescAR, int? TripID, int? DriverBlanceID, int Type)
        {
            try
            {
                var notification = new Notification()
                {
                    CreateDate = _bLGeneral.GetDateTimeNow(),
                    NotificationDate = _bLGeneral.GetDateTimeNow(),
                    NotificationGuid = Guid.NewGuid(),
                    IsDeleted = false,
                    UserId = userId,
                    IsSeen = false,
                    TitleAR = notificationTitleAR,
                    TitleEN = notificationTitleEN,
                    DescEN = notificationDescEN,
                    DescAR = notificationDescAR,
                    DriversID = driverID,
                    NotificationTypeID = Type,
                    OrderVendorID = TripID,
                    DriverBlanceID = DriverBlanceID,
                };
                notification = Uow.Notification.Insert(notification);
                Uow.Commit();
                return notification;
            }
            catch (Exception)
            {
                return new Notification();
            }
        }
        #region STC_Pay
        public bool UpdateOpenTransaction(List<DriverWithTrips> listDrivers)
        {
            var driverlist = listDrivers.Select(s => s.driverId).ToList();
            var driversdata = Uow.Drivers.GetAll(e => driverlist.Any(x => x == e.DriversID) && !e.IsDeleted);
            foreach (var item in driversdata)
            {
                item.OpenTransaction = true;
            }
            Uow.Drivers.UpdateRang(driversdata);
            Uow.Commit();
            return true;
        }
        public bool UpdateOpenTransaction(List<int> listtranactions)
        {
            var driverlist = Uow.TransactionSTCPay.GetAll(e => listtranactions.Any(x => x == e.TransactionSTCPayID)).Select(s => s.DriversID).ToList();
            var driversdata = Uow.Drivers.GetAll(e => driverlist.Any(x => x == e.DriversID) && !e.IsDeleted);
            foreach (var item in driversdata)
            {
                item.OpenTransaction = true;
            }
            Uow.Drivers.UpdateRang(driversdata);
            Uow.Commit();
            return true;
        }
        public class LogiSTCResult
        {
            public bool status { get; set; }
            public string message { get; set; }
        }
        [Obsolete]
        public LogiSTCResult ReSendDiversToSTCPAY(string token, List<int> listtranactions, string Link, int UserID, int type)
        {
            var ResponseNode = new LogiSTCResult();
            if (type == 1)
            {
                UpdateOpenTransaction(listtranactions);
                var RequestResend = new TranactionList();
                var Token = "Bearer " + token + "";
                RequestResend.userId = UserID;
                RequestResend.tranactions = listtranactions;
                try
                {
                    var client = new RestClient(Link);
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Content-Type", "application/json;charset=utf-8");
                    request.AddHeader("Authorization", "" + Token + "");
                    request.RequestFormat = DataFormat.Json;
                    request.AddJsonBody(RequestResend);
                    client.ExecuteTaskAsync(request);
                    ResponseNode.message = "";
                    ResponseNode.status = true;
                    return ResponseNode;
                }
                catch (Exception ex)
                {
                    ResponseNode.message = ex.Message;
                    ResponseNode.status = false;
                }
            }
            else
            {
                try
                {
                    if (listtranactions.Any())
                    {
                        foreach (var tranactionid in listtranactions)
                        {
                            var tranactiondata = Uow.TransactionSTCPay.GetAll(r => r.TransactionSTCPayID == tranactionid).FirstOrDefault();
                            if (tranactiondata != null)
                            {
                                var drivertrips = Uow.TranLogSTCPay.GetAll(r => r.TransactionSTCPayID == tranactionid).Select(x => x.OrderVendorID).ToList();
                                if (drivertrips != null)
                                {
                                    var tripdata = Uow.OrderVendor.GetAll(e => drivertrips.Any(x=> x== e.OrderVendorID));
                                    foreach (var item in tripdata)
                                    {
                                        if (item != null)
                                        {
                                            item.CaptainPaidID = (int)PaidStatusEnum.Cash_Transfered;
                                            item.UserUpdate = UserID;
                                            item.UpdateDate = _bLGeneral.GetDateTimeNow();
                                        }
                                    }
                                    Uow.OrderVendor.UpdateRang(tripdata);
                                    var data = InsertBalanceWithOutCommit((int)TypeOperationEnum.DR, GettripsCharge(drivertrips), (int)TransactionTypeEnum.Cash_Transfered, tranactiondata.DriversID
                                        , UserID, null, _bLGeneral.GetDateTimeNow(), "", "");
                                    tranactiondata.STCStatusId = (int)STCStatusEnum.Cancelled;
                                    tranactiondata.UpdateDate = _bLGeneral.GetDateTimeNow();
                                    tranactiondata.UserUpdate = UserID;
                                    Uow.TransactionSTCPay.Update(tranactiondata);
                                }
                            }
                        }
                        Uow.Commit();
                    }
                    ResponseNode.message = "Update Successfully";
                    ResponseNode.status = true;
                }
                catch (Exception ex)
                {

                    ResponseNode.message = ex.Message;
                    ResponseNode.status = false;
                }

            }
            return ResponseNode;

        }

        [Obsolete]
        public LogiSTCResult SendDiversToSTCPAY(string token, List<DriverWithTrips> listDrivers,string Link, int UserID,int type)
        {               
            var ResponseNode = new LogiSTCResult();
            if (type == 1)
            {
                UpdateOpenTransaction(listDrivers);
                var RequestResend = new DriverSTCPay();
                var Token = "Bearer " + token + "";
                RequestResend.userId = UserID;
                RequestResend.drivers = listDrivers;
                try
                {
                    var client = new RestClient(Link);
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Content-Type", "application/json;charset=utf-8");
                    request.AddHeader("Authorization", "" + Token + "");
                    request.RequestFormat = DataFormat.Json;
                    request.AddJsonBody(RequestResend);
                    client.ExecuteTaskAsync(request);
                    ResponseNode.message = "";
                    ResponseNode.status = true;
                    return ResponseNode;
                }
                catch (Exception ex)
                {
                    ResponseNode.message = ex.Message;
                    ResponseNode.status = false;
                }
            }
            else
            {
                try
                {
                    if (listDrivers.Any())
                    {
                        foreach (var driver in listDrivers)
                        {
                            var drivertrips = driver.trips;
                            if (drivertrips != null)
                            {
                                foreach (var item in drivertrips)
                                {
                                    var tripdata = Uow.OrderVendor.GetAll(e => e.OrderVendorID == item).FirstOrDefault();
                                    if (tripdata != null)
                                    {
                                        tripdata.CaptainPaidID = (int)PaidStatusEnum.Cash_Transfered;
                                        tripdata.UserUpdate = UserID;
                                        tripdata.UpdateDate = _bLGeneral.GetDateTimeNow();
                                        Uow.OrderVendor.Update(tripdata);
                                    }
                                }
                            }
                            var data = InsertBalanceWithOutCommit((int)TypeOperationEnum.DR, GettripsCharge(driver.trips), (int)TransactionTypeEnum.Cash_Transfered, driver.driverId, UserID, null, _bLGeneral.GetDateTimeNow(), "", "");
                        }
                        Uow.Commit();
                    }
                    ResponseNode.message = "Update Successfully";
                    ResponseNode.status = true;
                }
                catch (Exception ex)
                {

                    ResponseNode.message = ex.Message;
                    ResponseNode.status = false;
                }

            }
            return ResponseNode;

        }
        [Obsolete]
        public List<STCResultMessage> PayToDrivers(List<DriverWithTrips> listDrivers,int UserID, string Link, string STCMID, string inqurilink, string GetAppMainLink)
        {
            var RetResult = new STCResultMessage();
            var ListRetResult = new List<STCResultMessage>();
            if (listDrivers.Any())
            {
                foreach (var item in listDrivers)
                {
                    var DriverData = Uow.Drivers.GetAll(e => e.DriversID == item.driverId).FirstOrDefault();
                    if (DriverData != null)
                    {
                        var GetLastBalance = GettripsCharge(item.trips);
                        if (GetLastBalance > 0)
                        {
                            var StcRsu = STC(GetLastBalance,item,UserID,Link,STCMID,inqurilink,GetAppMainLink);
                            ListRetResult.Add(StcRsu);
                        }
                        else
                        {
                            RetResult.Message = "Driver " + item.ToString() + "has no balance";
                            RetResult.StatusCode = (int)TransactionStatusEnum.Error;
                            ListRetResult.Add(RetResult);
                        }
                    }
                    else
                    {
                        RetResult.Message = "Driver "+item.ToString()+" not found";
                        RetResult.StatusCode = (int)TransactionStatusEnum.Error;
                        ListRetResult.Add(RetResult);
                    }
                }
            }
            else
            {
                RetResult.Message = "No Drivers Found";
                RetResult.StatusCode = (int)TransactionStatusEnum.Error;
                ListRetResult.Add(RetResult);
            }
            return ListRetResult;
        }
        [Obsolete]
        public List<STCResultMessage> RePayToDrivers(List<int> listtranactions, int UserID,string STCMID, string inqurilink, string GetAppMainLink,string StcPayLink)
        {
            var RetResult = new STCResultMessage();
            var ListRetResult = new List<STCResultMessage>();
            if (listtranactions.Any())
            {
                foreach (var item in listtranactions)
                {
                    STCInquiry(item, UserID, STCMID, inqurilink, GetAppMainLink, StcPayLink);
                }
            }
            else
            {
                RetResult.Message = "No Drivers Found";
                RetResult.StatusCode = (int)TransactionStatusEnum.Error;
                ListRetResult.Add(RetResult);
            }
            return ListRetResult;
        }
        [Obsolete]
        public List<STCResultMessage> AutoRepayDrivers(string STCMID, string inqurilink, string GetAppMainLink,string StcPayLink)
        {
            var RetResult = new STCResultMessage();
            var ListRetResult = new List<STCResultMessage>();
            var InProgress = Uow.TransactionSTCPay.GetAll(e => !e.IsDeleted && (e.STCStatusId == (int)STCStatusEnum.CustomerInformed || e.STCStatusId == (int)STCStatusEnum.Created || e.STCStatusId == (int)STCStatusEnum.ProcessingPayment || e.STCStatusId == (int)STCStatusEnum.PaymentFailed), "").FirstOrDefault();
            if (InProgress != null)
            {
                    STCInquiry(InProgress.TransactionSTCPayID, InProgress.UserId, STCMID, inqurilink, GetAppMainLink, StcPayLink);
            }
            else
            {
                RetResult.Message = "No Drivers Found";
                RetResult.StatusCode = (int)TransactionStatusEnum.Error;
                ListRetResult.Add(RetResult);
            }
            return ListRetResult;
        }
        [Obsolete]
        public STCResultMessage STC(decimal Amount, DriverWithTrips model, int UserId,string Link, string STCMID,string inqurilink,string GetAppMainLink)
        {
            #region Ref
            var result = new STCResultMessage();
            var resultInquiry = new STCResultMessage();
            var PaymentOrderRequestMessage = new PaymentOrderRequestMessage();
            var lPayments = new List<Payments>();
            var Payments = new Payments();
            var TestSTC = new STCPayment();
            InquirySTC InquirySTC = new InquirySTC();
            PaymentOrderInquiryRequestMessage PaymentOrderInquiryRequestMessage = new PaymentOrderInquiryRequestMessage();
            #endregion
            var DriversData = Uow.Drivers.GetAll(e => e.DriversID == model.driverId).FirstOrDefault();
            if (DriversData != null)
            {
                #region Request
                try
                {
                    #region Payment Request Data
                    //Generate Customer Refrence
                    var Ref = _bLGeneral.KeyGeneratorNumbersOnly(10);
                    if (Ref == null || Ref.Length != 10)
                    {
                        Ref = _bLGeneral.KeyGeneratorNumbersOnly(10);
                    }
                    var TrackExists = Uow.TransactionSTCPay.GetAll().Where(e => e.CustomerRefrence == "Home_" + Ref).Any();
                    while (TrackExists)
                    {
                        Ref = _bLGeneral.KeyGeneratorNumbersOnly(10);
                        TrackExists = Uow.TransactionSTCPay.GetAll().Where(e => e.CustomerRefrence == "Home_" + Ref).Any();
                    }
                    //Generate Item Refrence
                    var ItemRef = Amount.ToString() + " _ " + DriversData.DriverGuid.ToString();
                    PaymentOrderRequestMessage.CustomerReference = "Home_" + Ref;
                    PaymentOrderRequestMessage.Description = "Home Made";
                    PaymentOrderRequestMessage.ValueDate = _bLGeneral.GetDateTimeNow().ToString();
                    Payments.ItemReference = ItemRef;
                    //For Test
                    //Payments.Amount = 1;
                    //Payments.MobileNumber = "966557877988";
                    //For Prod Test
                    //Payments.Amount = 1;
                    //Payments.MobileNumber = "0595489308";
                    //For Production
                    Payments.Amount = Amount;
                    Payments.MobileNumber = "0" + DriversData.PhoneNumberStc;
                    lPayments.Add(Payments);
                    PaymentOrderRequestMessage.Payments = lPayments;
                    TestSTC.PaymentOrderRequestMessage = PaymentOrderRequestMessage;
                    #endregion
                    #region Send Payment Request
                    var client = new RestClient(Link);
                    string Certificate = GetAppMainLink;
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.DefaultConnectionLimit = 9999;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => true;
                    X509Certificate2 certificate = new X509Certificate2(Certificate, "Jana@2020");
                    client.ClientCertificates = new X509CertificateCollection() { certificate };
                    client.Proxy = new WebProxy();
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Content-Type", "application/json;charset=utf-8");
                    request.AddHeader("X-ClientCode", STCMID);
                    request.RequestFormat = DataFormat.Json;
                    request.AddBody(TestSTC);
                    IRestResponse response = client.Execute(request);
                    result.Content = response.Content.ToString();
                    #endregion
                    Thread.Sleep(5000);
                    if (response.StatusCode == HttpStatusCode.OK && !string.IsNullOrEmpty(result.Content))
                    {
                        #region Payment Refrence
                        //Get PaymentOrderReference
                        STCRes jsons_success = JsonConvert.DeserializeObject<STCRes>(result.Content);
                        result.Message = jsons_success.PaymentOrderResponseMessage.PaymentOrderReference;
                        #endregion
                        //#region TranLogSTCPay
                        //var TranLogSTCPay22 = new TransactionSTCPay();
                        //TranLogSTCPay22.TransactionStatusId = (int)TransactionStatusEnum.OK;
                        //TranLogSTCPay22.Amount = Amount;
                        //TranLogSTCPay22.STCStatusId = (int)PaidStatusEnum.Created;
                        //TranLogSTCPay22.InquriyContent = "";
                        //TranLogSTCPay22.CreateDate = _bLGeneral.GetDateTimeNow();
                        //TranLogSTCPay22.IsDeleted = false;
                        //TranLogSTCPay22.MobileNo = DriversData.PhoneNumberStc;
                        ////TranLogSTCPay22.DriverBlanceID = OrderPayData.DriverBlanceID;
                        //TranLogSTCPay22.UserId = UserId;
                        //TranLogSTCPay22.PaymentOrderReference = result.Message;
                        //TranLogSTCPay22.CustomerRefrence = "Home_" + Ref;
                        //TranLogSTCPay22.ItemRefrence = ItemRef;
                        //TranLogSTCPay22.ResponseMessage = response.Content;
                        //Uow.TransactionSTCPay.Insert(TranLogSTCPay22);
                        //#endregion
                        //Uow.Commit();
                        #region Body
                        PaymentOrderInquiryRequestMessage.CustomerReference = "Home_" + Ref;
                        PaymentOrderInquiryRequestMessage.ItemReference = ItemRef;
                        PaymentOrderInquiryRequestMessage.PaymentReference = result.Message;
                        InquirySTC.PaymentOrderInquiryRequestMessage = PaymentOrderInquiryRequestMessage;
                        #endregion
                        #region Inquiry Request
                        var clientInquiry = new RestClient(inqurilink);
                        ServicePointManager.Expect100Continue = true;
                        ServicePointManager.DefaultConnectionLimit = 9999;
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => true;
                        clientInquiry.ClientCertificates = new X509CertificateCollection() { certificate };
                        clientInquiry.Proxy = new WebProxy();
                        var requestInquiry = new RestRequest(Method.POST);
                        requestInquiry.AddHeader("Content-Type", "application/json;charset=utf-8");
                        requestInquiry.AddHeader("X-ClientCode", STCMID);
                        requestInquiry.RequestFormat = DataFormat.Json;
                        requestInquiry.AddBody(InquirySTC);
                        IRestResponse responseInq = clientInquiry.Execute(requestInquiry);
                        resultInquiry.Content = responseInq.Content.ToString();
                        #endregion
                        #region Content From Inquiry
                        //Get Inquiry Data
                        if (!string.IsNullOrWhiteSpace(resultInquiry.Content))
                        {

                            var jsons_success_Inquiry = JsonConvert.DeserializeObject<STCInquiryRes>(resultInquiry.Content);
                            var PaymentOrderInquiryResponseMessage = new PaymentOrderInquiryResponseMessage();
                            PaymentOrderInquiryResponseMessage = jsons_success_Inquiry.PaymentOrderInquiryResponseMessage;
                            resultInquiry.Message = PaymentOrderInquiryResponseMessage.Payments.FirstOrDefault().Status;
                        }
                        #endregion
                        int PaidStatusId = 0;
                        result.StatusCode = (int)TransactionStatusEnum.OK;
                        if (!string.IsNullOrWhiteSpace(resultInquiry.Message))
                        {
                            PaidStatusId = int.Parse(resultInquiry.Message);
                            result.PaidStatus = int.Parse(resultInquiry.Message);
                        }
                        #region TranLogSTCPay
                        var TranLogSTCPay = new TransactionSTCPay();
                        TranLogSTCPay.TransactionStatusId = (int)TransactionStatusEnum.OK;
                        TranLogSTCPay.Amount = Amount;
                        TranLogSTCPay.STCStatusId = PaidStatusId;
                        TranLogSTCPay.InquriyContent = resultInquiry.Content;
                        TranLogSTCPay.CreateDate = _bLGeneral.GetDateTimeNow();
                        TranLogSTCPay.IsDeleted = false;
                        TranLogSTCPay.MobileNo = DriversData.PhoneNumberStc;
                        TranLogSTCPay.UserId = UserId;
                        TranLogSTCPay.DriversID = model.driverId;
                        TranLogSTCPay.PaymentOrderReference = result.Message;
                        TranLogSTCPay.CustomerRefrence = PaymentOrderRequestMessage.CustomerReference;
                        TranLogSTCPay.ItemRefrence = ItemRef;
                        TranLogSTCPay.ResponseMessage = response.Content;
                        var drivertrips = model.trips;
                        if (drivertrips != null)
                        {
                            //foreach (var item in drivertrips)
                            //{
                            //    var tripdata = Uow.TripMaster.GetAll(e => e.TripMasterID == item).FirstOrDefault();
                            //    if (tripdata != null)
                            //    {
                            //        tripdata.CaptainPaidID = PaidStatusId == (int)STCStatusEnum.Paid ? (int)PaidStatusEnum.STC_Transfered :
                            //           PaidStatusId == (int)STCStatusEnum.Created ? (int)PaidStatusEnum.Created_STC :
                            //        PaidStatusId == (int)STCStatusEnum.ProcessingPayment ? (int)PaidStatusEnum.ProcessingPayment_STC
                            //        : PaidStatusId == (int)STCStatusEnum.CustomerInformed ? (int)PaidStatusEnum.CustomerInformed_STC : (int)PaidStatusEnum.PaymentFailed_STC;
                            //        tripdata.UserUpdate = UserId;
                            //        tripdata.UpdateDate = _bLGeneral.GetDateTimeNow();
                            //        Uow.TripMaster.Update(tripdata);
                            //    }
                            //}
                            var tripdata = Uow.OrderVendor.GetAll(e => drivertrips.Any(x=> x == e.OrderVendorID));
                            foreach (var item in tripdata)
                            {
                                if (item != null)
                                {
                                    item.CaptainPaidID = PaidStatusId == (int)STCStatusEnum.Paid ? (int)PaidStatusEnum.STC_Transfered :
                                       PaidStatusId == (int)STCStatusEnum.Created ? (int)PaidStatusEnum.Created_STC :
                                    PaidStatusId == (int)STCStatusEnum.ProcessingPayment ? (int)PaidStatusEnum.ProcessingPayment_STC
                                    : PaidStatusId == (int)STCStatusEnum.CustomerInformed ? (int)PaidStatusEnum.CustomerInformed_STC : (int)PaidStatusEnum.PaymentFailed_STC;
                                    item.UserUpdate = UserId;
                                    item.UpdateDate = _bLGeneral.GetDateTimeNow();
                                }
                                TranLogSTCPay.TranLogSTCPay.Add(new TranLogSTCPay() { IsDeleted = false, CreateDate = _bLGeneral.GetDateTimeNow(), TranLogSTCPayGUID = Guid.NewGuid(), OrderVendorID = item.OrderVendorID
                                    , UserId = UserId });
                            }
                            Uow.OrderVendor.UpdateRang(tripdata);
                        }
                        if (PaidStatusId == (int)STCStatusEnum.Paid)
                        {

                            var data = InsertBalanceSTC(Amount, (int)model.driverId, UserId);
                            TranLogSTCPay.DriverBlanceID = data.DriverBlanceID;
                        }
                        Uow.TransactionSTCPay.Insert(TranLogSTCPay);
                        DriversData.OpenTransaction = false;
                        Uow.Drivers.Update(DriversData);
                        Uow.Commit();
                        #endregion
                        return result;
                    }
                    else
                    {
                        TransactionSTCPay TranLogSTCPay = new TransactionSTCPay();
                        TranLogSTCPay.TransactionStatusId = (int)TransactionStatusEnum.Error;
                        if (response.StatusCode == HttpStatusCode.Forbidden)
                        {
                            TranLogSTCPay.TransactionStatusId = (int)TransactionStatusEnum.Forbidden;
                        }
                        TranLogSTCPay.Amount = Amount;
                        TranLogSTCPay.CreateDate = _bLGeneral.GetDateTimeNow();
                        TranLogSTCPay.IsDeleted = false;
                        TranLogSTCPay.MobileNo = DriversData.PhoneNumberStc;
                        //TranLogSTCPay.DriverBlanceID = OrderPayData.DriverBlanceID;
                        TranLogSTCPay.UserId = UserId;
                        TranLogSTCPay.PaymentOrderReference = string.Empty;
                        TranLogSTCPay.CustomerRefrence = PaymentOrderRequestMessage.CustomerReference;
                        TranLogSTCPay.ItemRefrence = ItemRef;
                        TranLogSTCPay.STCStatusId = (int)STCStatusEnum.PaymentFailed;
                        TranLogSTCPay.ResponseMessage = response.Content;
                        TranLogSTCPay.DriversID = model.driverId;
                        Uow.TransactionSTCPay.Insert(TranLogSTCPay);
                        Uow.Commit();
                        result.StatusCode = (int)TransactionStatusEnum.Error;
                        result.Message = response.Content;
                        return result;
                    }
                }
                catch (Exception e1)
                {
                    result.StatusCode = (int)TransactionStatusEnum.Error;
                    result.Message = e1.ToString();
                    DriversData.OpenTransaction = false;
                    Uow.Drivers.Update(DriversData);
                    Uow.Commit();
                    return result;
                }
                #endregion
            }
            else
            {
                result.StatusCode = (int)TransactionStatusEnum.Error;
                result.Message = "Driver Not Found";
                return result;
            }
        }
        public List<DriverBlanceViewModel> GetAllDriverBlanceViewModelInquiry(string[] ListCountryID, string[] ListAgentID, string[] ListCityID, int? DriverID, string TaxiNumber, DateTime? FromDate, DateTime? ToDate, int? ModelID, int? BrandID, int? IsEnable, bool? IsActive, string accLanguage, string fileView, string driverMobile, string tripNo, decimal loast)
        {
            var lis = new List<DriverBlanceViewModel>();
            if (ListCountryID != null)
            {
                if (ListCountryID.Count(p => !string.IsNullOrEmpty(p)) == 0)
                {
                    ListCountryID = null;
                }
            }
            if (ListAgentID != null)
            {
                if (ListAgentID.Count(p => !string.IsNullOrEmpty(p)) == 0)
                {
                    ListAgentID = null;
                }
            }
            if (ListCityID != null)
            {
                if (ListCityID.Count(p => !string.IsNullOrEmpty(p)) == 0)
                {
                    ListCityID = null;
                }
            }
            var data = Uow.TransactionSTCPay.GetAll(x => !x.IsDeleted &&
            (x.STCStatusId == (int)STCStatusEnum.PaymentFailed || x.STCStatusId == (int)STCStatusEnum.ProcessingPayment
            || x.STCStatusId == (int)STCStatusEnum.CustomerInformed || x.STCStatusId == (int)STCStatusEnum.Ready || x.STCStatusId == (int)STCStatusEnum.Created)
                && (IsActive != null ? (IsActive == true ? x.Drivers.VerifyStc == (int)VerifyStcEnum.Verified : x.Drivers.VerifyStc != (int)VerifyStcEnum.Verified) : true)
            && (!string.IsNullOrEmpty(TaxiNumber) ? x.Drivers.CarNumber.Contains(TaxiNumber) : true)
             && (DriverID != null ? DriverID == x.DriversID : true)
             && (FromDate != null ? FromDate.Value.Date <= x.CreateDate.Date : true)
             && (ToDate != null ? ToDate.Value.Date >= x.CreateDate.Date : true)
             && (driverMobile != null ? driverMobile == x.Drivers.PhoneNumber : true)
            , "Drivers").OrderByDescending(a => a.CreateDate).Select(m => new DriverBlanceViewModel()
            {
                After = 0,
                Amount = Math.Round(m.Amount, 2),
                Before = 0,
                DateOperation = m.CreateDate,
                DriverID = m.DriversID,
                DriverName = accLanguage == "ar" ? m.Drivers.NameAr : m.Drivers.NameEn,
                MobileNo = m.Drivers.PhoneNumber,
                OrderVendorID = m.TransactionSTCPayID,
                OrderVendorGuid = m.TransactionSTCPayGUID,
                OrderNumber = m.TransactionSTCPayID.ToString(),
                TransactionTypeName = accLanguage == "ar" ? (m.STCStatusId == (int)STCStatusEnum.ProcessingPayment ? "جارى الدفع" :
                m.STCStatusId == (int)STCStatusEnum.Created ? "تم الإنشاء" : m.STCStatusId == (int)STCStatusEnum.PaymentFailed ? "فشل الدفع" : "العميل غير مفعل") :
                (m.STCStatusId == (int)STCStatusEnum.ProcessingPayment ? "Processing Payment" :
                m.STCStatusId == (int)STCStatusEnum.Created ? "Created" : m.STCStatusId == (int)STCStatusEnum.PaymentFailed ? "Payment Failed" : "Customer Informed"),
                VerifyStc = m.Drivers.VerifyStc,
                Attachment = m.CustomerRefrence,
                OpenTransaction = m.Drivers.OpenTransaction,
            });
            foreach (var item in data.Where(e => e.Amount > 0))
            {
                item.After = GetLastTripsCharge(item.DriverID);
                lis.Add(item);
            }
            return lis.Where(e => e.After >= loast).ToList();
        }
        public List<DriverBlanceViewModel> GetAllDriverBlanceViewModelHistory(string[] ListCountryID, string[] ListAgentID, string[] ListCityID, int? DriverID, string TaxiNumber, DateTime? FromDate, DateTime? ToDate, int? ModelID, int? BrandID, int? IsEnable, bool? IsActive, string accLanguage, string fileView, string driverMobile, string tripNo, decimal loast)
        {
            var lis = new List<DriverBlanceViewModel>();
            if (ListCountryID != null)
            {
                if (ListCountryID.Count(p => !string.IsNullOrEmpty(p)) == 0)
                {
                    ListCountryID = null;
                }
            }
            if (ListAgentID != null)
            {
                if (ListAgentID.Count(p => !string.IsNullOrEmpty(p)) == 0)
                {
                    ListAgentID = null;
                }
            }
            if (ListCityID != null)
            {
                if (ListCityID.Count(p => !string.IsNullOrEmpty(p)) == 0)
                {
                    ListCityID = null;
                }
            }
            var data = Uow.OrderVendor.GetAll(x => !x.IsDeleted &&
            (x.CaptainPaidID == (int)PaidStatusEnum.Cash_Transfered || x.CaptainPaidID == (int)PaidStatusEnum.STC_Transfered)
            && (x.OrderStatusID == (int)OrderStatusEnum.Delivered || x.OrderStatusID == (int)OrderStatusEnum.Cancel)
                && (IsActive != null ? (IsActive == true ? x.Drivers.VerifyStc == (int)VerifyStcEnum.Verified : x.Drivers.VerifyStc != (int)VerifyStcEnum.Verified) : true)
            && (!string.IsNullOrEmpty(TaxiNumber) ? x.Drivers.CarNumber.Contains(TaxiNumber) : true)
             && (DriverID != null ? DriverID == x.DriversID : true)
             && (driverMobile != null ? driverMobile == x.Drivers.PhoneNumber : true)
            , "Drivers").OrderByDescending(a => a.CreateDate).Select(m => new DriverBlanceViewModel()
            {
                After = 0,
                Amount =Math.Round(m.DriverCharge, 2),
                Before = 0,
                DateOperation = m.CreateDate,
                DriverID = m.DriversID.Value,
                DriverName = accLanguage == "ar" ? m.Drivers.NameAr : m.Drivers.NameEn,
                MobileNo = m.Drivers.PhoneNumber,
                OrderVendorID = m.OrderVendorID,
                OrderVendorGuid = m.OrderVendorGuid,
                OrderNumber = m.TrackNo,
                TransactionTypeName = accLanguage == "ar" ? (m.CaptainPaidID == (int)PaidStatusEnum.ProcessingPayment_STC ? "جارى الدفع" :
                m.CaptainPaidID == (int)PaidStatusEnum.Cash_Transfered ? "دفع نقدي" : "دفع اس تي سي") :
                (m.CaptainPaidID == (int)PaidStatusEnum.Cash_Transfered ? "Cash" : "STC"),
                VerifyStc = m.Drivers.VerifyStc,
                OpenTransaction = m.Drivers.OpenTransaction,
            });
            foreach (var item in data.Where(e => e.Amount > 0))
            {
                item.After = GetLastTripsCharge(item.DriverID);
                lis.Add(item);
            }
            return lis.Where(e => e.After >= loast).ToList();
        }
        [Obsolete]
        public STCResultMessage STCInquiry(int TranactionID,int UserID, string STCMID,string paymentOrderInquiry, string PhysicalApplicationPath,string StcPayLink)
        {
            #region Ref
            var resultInquiry = new STCResultMessage();
            var InquirySTC = new InquirySTC();
            var PaymentOrderInquiryRequestMessage = new PaymentOrderInquiryRequestMessage();
            #endregion
            var item = Uow.TransactionSTCPay.GetAll(e => !e.IsDeleted && e.TransactionSTCPayID == TranactionID).FirstOrDefault();
            try
            {
                
                #region Body
                PaymentOrderInquiryRequestMessage.CustomerReference = item.CustomerRefrence;
                PaymentOrderInquiryRequestMessage.ItemReference = item.ItemRefrence;
                PaymentOrderInquiryRequestMessage.PaymentReference = item.PaymentOrderReference;
                InquirySTC.PaymentOrderInquiryRequestMessage = PaymentOrderInquiryRequestMessage;
                #endregion
                #region Inquiry Request
                var clientInquiry = new RestClient(paymentOrderInquiry);
                string Certificate = PhysicalApplicationPath;
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.DefaultConnectionLimit = 9999;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => true;
                X509Certificate2 certificate = new X509Certificate2(Certificate, "Jana@2020");
                clientInquiry.ClientCertificates = new X509CertificateCollection() { certificate };
                clientInquiry.Proxy = new WebProxy();
                var requestInquiry = new RestRequest(Method.POST);
                requestInquiry.AddHeader("Content-Type", "application/json;charset=utf-8");
                requestInquiry.AddHeader("X-ClientCode", STCMID);
                requestInquiry.RequestFormat = DataFormat.Json;
                requestInquiry.AddBody(InquirySTC);
                IRestResponse responseInq = clientInquiry.Execute(requestInquiry);
                resultInquiry.Content = responseInq.Content.ToString();
                #endregion              
                //Get Inquiry Data
                if (!string.IsNullOrWhiteSpace(resultInquiry.Content))
                {
                    var jsons_success_Inquiry = JsonConvert.DeserializeObject<STCInquiryRes>(resultInquiry.Content);
                    PaymentOrderInquiryResponseMessage PaymentOrderInquiryResponseMessage = new PaymentOrderInquiryResponseMessage();
                    PaymentOrderInquiryResponseMessage = jsons_success_Inquiry.PaymentOrderInquiryResponseMessage;
                    resultInquiry.Message = PaymentOrderInquiryResponseMessage.Payments.FirstOrDefault().Status;
                    int PaidStatusId = 0;
                    if (!string.IsNullOrWhiteSpace(resultInquiry.Message))
                    {
                        PaidStatusId = int.Parse(resultInquiry.Message);
                    }
                    if (item.STCStatusId == (int)STCStatusEnum.CustomerInformed || item.STCStatusId == (int)STCStatusEnum.Created || item.STCStatusId == (int)STCStatusEnum.Ready
                        || item.STCStatusId == (int)STCStatusEnum.ProcessingPayment || item.STCStatusId == (int)STCStatusEnum.PaymentFailed)
                    {
                        if (PaidStatusId != item.STCStatusId)
                        {
                            #region TranLogSTCPay
                            item.UpdateDate = _bLGeneral.GetDateTimeNow();
                            item.UserUpdate = UserID;
                            item.ResponseMessage = resultInquiry.Content;
                            item.STCStatusId = PaidStatusId;
                            var drivertrips = Uow.TranLogSTCPay.GetAll(e=>e.TransactionSTCPayID == item.TransactionSTCPayID).Select(s=>s.OrderVendorID).ToList();
                            if (drivertrips != null)
                            {
                                var tripdata = Uow.OrderVendor.GetAll(e => drivertrips.Any(x => x == e.OrderVendorID));
                                foreach (var trip in tripdata)
                                {
                                    if (trip != null)
                                    {
                                        trip.CaptainPaidID = PaidStatusId == (int)STCStatusEnum.Paid ? (int)PaidStatusEnum.STC_Transfered :
                                           PaidStatusId == (int)STCStatusEnum.Created ? (int)PaidStatusEnum.Created_STC :
                                        PaidStatusId == (int)STCStatusEnum.ProcessingPayment ? (int)PaidStatusEnum.ProcessingPayment_STC
                                        : PaidStatusId == (int)STCStatusEnum.CustomerInformed ? (int)PaidStatusEnum.CustomerInformed_STC : (int)PaidStatusEnum.PaymentFailed_STC;
                                        trip.UserUpdate = UserID;
                                        trip.UpdateDate = _bLGeneral.GetDateTimeNow();
                                    }
                                }
                                Uow.OrderVendor.UpdateRang(tripdata);
                            }
                            if (PaidStatusId == (int)STCStatusEnum.Paid)
                            {

                                var data = InsertBalanceSTC(item.Amount, (int)item.DriversID, UserID);
                                item.DriverBlanceID = data.DriverBlanceID;
                            }
                            #endregion
                            var DriversData = Uow.Drivers.GetAll(s => s.DriversID == item.DriversID).FirstOrDefault();
                            DriversData.OpenTransaction = false;
                            Uow.Drivers.Update(DriversData);
                            Uow.TransactionSTCPay.Update(item);
                            Uow.Commit();
                        }
                        else if (item.STCStatusId == (int)STCStatusEnum.PaymentFailed)
                        {
                            var model = new DriverWithTrips();
                            model.driverId = item.DriversID;
                            model.trips = Uow.TranLogSTCPay.GetAll(e => e.TransactionSTCPayID == item.TransactionSTCPayID).Select(s => s.OrderVendorID).ToList();
                            item.STCStatusId = (int)STCStatusEnum.Cancelled;
                            item.UpdateDate = _bLGeneral.GetDateTimeNow();
                            item.UserUpdate = UserID;
                            item.ResponseMessage = resultInquiry.Content;
                            Uow.TransactionSTCPay.Update(item);
                            STC(item.Amount, model, UserID,StcPayLink, STCMID, paymentOrderInquiry, PhysicalApplicationPath);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                resultInquiry.Message = ex.Message;
                resultInquiry.StatusCode = (int)TransactionStatusEnum.Error;
            }
            return resultInquiry;
        }
        #endregion
        public DriverBlance InsertBalanceSTC(decimal amount, int driverId, int userId)
        {
            var Before = GetLastBlance(driverId);
            var DriverBlance = new DriverBlance()
            {
                Amount = amount,
                Before = Before,
                CreateDate = _bLGeneral.GetDateTimeNow(),
                DriversID = driverId,
                IsDeleted = false,
                TransactionTypeID = (int)TransactionTypeEnum.Pay_to_Captain_STC,
                TypeOperationID = (int)TypeOperationEnum.DR,
                UserId = userId,
                After = Before - amount,
                DriverBlanceGuid = Guid.NewGuid(),
                DateOperation = _bLGeneral.GetDateTimeNow(),
                //IsPaid = false,
            };
            DriverBlance = Uow.DriverBlance.Insert(DriverBlance);
            Uow.Commit();
            return DriverBlance;
        }
        #endregion
    }
}
