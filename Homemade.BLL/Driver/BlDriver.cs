using Homemade.BLL.General;
using Homemade.BLL.Setting;
using Homemade.BLL.ViewModel.Customer;
using Homemade.BLL.ViewModel.Driver;
using Homemade.BLL.ViewModel.General;
using Homemade.BLL.ViewModel.Notifications;
using Homemade.BLL.ViewModel.Setting;
using Homemade.DAL.Contract;
using Homemade.Model;
using Homemade.Model.Driver;
using Homemade.Model.Order;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;

namespace Homemade.BLL.Driver
{
    public class BlDriver : BaseEntity
    {
        #region Decleration
        private readonly IUOW Uow;
        private readonly BLGeneral _blGeneral;
        private readonly BlTokens _blTokens;
        public BlDriver(IUOW _uow, BLGeneral blGeneral, BlTokens blTokens)
        {
            this.Uow = _uow;
            _blGeneral = blGeneral;
            _blTokens = blTokens;
        }
        #endregion
        #region Helpers
        /// <summary>
        /// Check Phone Number if exist by driverGuid
        /// </summary>
        /// <param name="driverNameAr"></param>
        /// <returns></returns>
        public bool DriverNameIsExists(Guid driverGuid, string driverNameAr)
        {
            return Uow.Drivers.GetAll(p => p.NameAr.Trim() == driverNameAr && p.DriverGuid != driverGuid).Any();
        }
        /// <summary>
        /// Check ID No if exist by driverGuid
        /// </summary>
        /// <param name="idNo"></param>
        /// <returns></returns>
        public bool IDNoIsExists(Guid driverGuid, string idNo)
        {
            return Uow.Drivers.GetAll(p => p.IDNo.Trim() == idNo && p.DriverGuid != driverGuid).Any();

        }
        /// <summary>
        /// Check Car Number if exist 
        /// </summary>
        /// <param name="CarNumber"></param>
        /// <returns></returns>
        public bool CarNumberIsExists(Guid DriverGuid, string CarNumber)
        {
            return Uow.Drivers.GetAll(p => p.CarNumber.Trim() == CarNumber && p.DriverGuid != DriverGuid).Any();

        }
        #endregion
        #region Notifictions
        public int GetUserNotificationNotSeenCount(int UserId) => Uow.Notification.GetAll(x => !x.IsDeleted && x.Drivers.UserId == UserId && !x.IsSeen).Count();
        public Notification GetNotificationById(int NotificationID) => Uow.Notification.GetAll(x => !x.IsDeleted && x.NotificationID == NotificationID).FirstOrDefault();
        public bool UpdateNotificationSeen(int notificationID, int userId)
        {
            var notification = GetNotificationById(notificationID);
            if (notification != null)
            {
                notification.UpdateDate = _blGeneral.GetDateTimeNow();
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
            var notifications = Uow.Notification.GetAll(x => !x.IsDeleted && x.Drivers.UserId == userId && !x.IsSeen);
            if (notifications != null)
            {
                foreach (var notification in notifications)
                {
                    notification.UpdateDate = _blGeneral.GetDateTimeNow();
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
        public ResponsListNoti GetNotificationListApiViewModelByDriver(int DriversID, string accLanguage, int page)
        {
            var listret = new List<NotificationListApiViewModel>();
            var data = Uow.Notification.GetAll(x => !x.IsDeleted && x.DriversID == DriversID).OrderByDescending(e => e.CreateDate).GroupBy(s => s.CreateDate.Date).Select(m => new NotificationListApiViewModel()
            {
                date = accLanguage == "ar" ? (m.Key.Date == DateTime.Now.Date ? "اليوم" : m.Key.Date.AddDays(1) == DateTime.Now.Date ? "امس" : m.Key.ToString("dd-MM-yyyy")) :
                 m.Key.Date == DateTime.Now.Date ? "Today" : m.Key.Date.AddDays(1) == DateTime.Now.Date ? "yesterday" : m.Key.ToString("dd-MM-yyyy"),
                notificationCount = m.Count(),
                datekey = m.Key
            });
            foreach (var item in data)
            {
                item.dayNotifications = Uow.Notification.GetAll(x => !x.IsDeleted && x.CreateDate.Date == item.datekey.Date && x.DriversID == DriversID).OrderByDescending(e => e.CreateDate).Select(m => new NotificationDetailsApiViewModel()
                {
                    notificationID = m.NotificationID,
                    isSeen = m.IsSeen,
                    time = m.NotificationDate.ToString("HH:mm tt"),
                    desc = accLanguage == "ar" ? m.DescAR ?? string.Empty : m.DescEN ?? string.Empty,
                    title = accLanguage == "ar" ? m.TitleAR ?? string.Empty : m.TitleEN ?? string.Empty,
                    orderId = m.OrderVendorID != null ? m.OrderVendorID : 0,
                    rateId = m.OrderRateID != null ? m.OrderRateID : 0,
                    questionId = m.ProdQuestionID != null ? m.ProdQuestionID : 0,
                    notificationType = m.NotificationTypeID,
                }).ToList();
                listret.Add(item);
            }
            listret = listret.Skip(Convert.ToInt32(page - 1) * 10).ToList();
            var DataTake = listret.Take(((Convert.ToInt32(page) * 10)) - (Convert.ToInt32(page - 1) * 10)).ToList();
            bool NextPage = false;
            if (listret.Count() > 10)
            {
                NextPage = true;
            }
            var model = new ResponsListNoti();
            model.notifications = DataTake;
            model.isNextPage = NextPage;
            return model;
        }
        #endregion
        #region Helpers
        public bool IsUserNameExists(string username)
        {
            return Uow.User.GetAll(s => s.UserName == username).Count() > 0;
        }
        public bool IsExistMobile(string Mobile)
        {
            return Uow.Drivers.GetAll(s => s.PhoneNumber == Mobile && !s.IsDeleted).Any();
        }
        public bool IsExistIDNO(string Mobile)
        {
            return Uow.Drivers.GetAll(s => s.IDNo == Mobile && !s.IsDeleted).Any();
        }
        public bool IsExistIDNO(string Mobile, int driverId)
        {
            return Uow.Drivers.GetAll(s => s.IDNo == Mobile && !s.IsDeleted && s.DriversID != driverId).Any();
        }
        public bool IsExistMobile(string Mobile, int driverId)
        {
            return Uow.Drivers.GetAll(s => s.PhoneNumber == Mobile && s.DriversID != driverId && !s.IsDeleted).Any();
        }
        public bool IsExistEmail(string Mobile)
        {
            return Uow.Drivers.GetAll(s => s.Email == Mobile && !s.IsDeleted).Any();
        }
        public bool IsExistEmail(string Mobile, int driverId)
        {
            return Uow.Drivers.GetAll(s => s.Email == Mobile && !s.IsDeleted && s.DriversID != driverId).Any();
        }
        /// <summary>
        /// Check If Drivers IDNo Is Used Before Or Not اختبار هل رقم الهوية موجود من قبل
        /// </summary>
        /// <param name="IDNo"></param>
        /// <returns></returns>
        public bool IsExistIDNo(string name)
        {
            return Uow.Drivers.GetAll(s => s.IDNo == name && !s.IsDeleted).Any();
        }
        #endregion
        #region Actions
        public bool ConfirmReceived(Guid id, int UserId, int Status, string notes)
        {
            var AssData = Uow.Drivers.GetAll(e => e.DriverGuid == id).FirstOrDefault();
            if (AssData != null)
            {
                AssData.RequestStatusId = Status;
                AssData.UpdateDate = _blGeneral.GetDateTimeNow();
                AssData.UserUpdate = UserId;
                AssData.RequestNotes = notes;
                Uow.Drivers.Update(AssData);
                Uow.Commit();
                return true;
            }
            return false;
        }
        public bool ConfirmReceivedAll(string id, int UserId, int Status, string notes)
        {
            string[] listids = id.Split(",");
            foreach (var item in listids)
            {
                if (!string.IsNullOrWhiteSpace(item))
                {
                    var AssData = Uow.Drivers.GetAll(e => e.DriversID == int.Parse(item)).FirstOrDefault();
                    if (AssData != null)
                    {
                        AssData.RequestStatusId = Status;
                        AssData.UpdateDate = _blGeneral.GetDateTimeNow();
                        AssData.UserUpdate = UserId;
                        AssData.RequestNotes = notes;
                        Uow.Drivers.Update(AssData);
                    }
                }
            }
            try
            {
                Uow.Commit();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool ChangeStatue(int id)
        {
            var driverTaxi = GetById(id);
            if (driverTaxi != null)
            {
                var driverTaxiObj = GetById(id);
                if (driverTaxiObj.RequestStatusId == (int)RequestStatusEnum.Approved)
                {
                    driverTaxiObj.RequestStatusId = (int)RequestStatusEnum.Activated;
                    driverTaxiObj.IsEnable = true;
                    driverTaxiObj.IsActive = true;
                }
                Uow.Drivers.Update(driverTaxiObj);
                Uow.Commit();
                return true;
            }
            return false;
        }
        public bool ChangeEnable(int Id, int userId)
        {
            var driverTaxi = Uow.Drivers.GetAll(e => e.DriversID == Id).FirstOrDefault();
            if (driverTaxi != null)
            {
                driverTaxi.IsEnable = true;
                driverTaxi.UserUpdate = userId;
                driverTaxi.RequestStatusId = (int)RequestStatusEnum.Activated;
                driverTaxi.IsActive = true;
                driverTaxi.UpdateDate = _blGeneral.GetDateTimeNow();
                Uow.Drivers.Update(driverTaxi);
                if (!driverTaxi.IsEnable)
                {
                    int num = GetById(Id).UserId;
                    _blTokens.UpdateTokens(num, 0, string.Empty);
                    _blTokens.UpdateUserJWTToken(num, string.Empty);

                    //var userid = GetById(Id).UserId;
                    //var tokendata = Uow.User.GetAll(e => e.Id == userid).FirstOrDefault();
                    //if (tokendata != null)
                    //{
                    //    tokendata.UserJWTToken = "";
                    //    Uow.User.Update(tokendata);
                    //}
                }
                Uow.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Delete(int Id, int userId)
        {
            var driverTaxi = GetById(Id);
            if (driverTaxi != null)
            {
                driverTaxi.IsDeleted = true;
                driverTaxi.IsActive = false;
                driverTaxi.UserUpdate = userId;
                driverTaxi.DeleteDate = _blGeneral.GetDateTimeNow();
                Uow.Drivers.Update(driverTaxi);

                var vendorDataVendor = _blTokens.GetMobileDataByUserID(driverTaxi.UserId);
                if (vendorDataVendor != null)
                {
                    Uow.Tokens.Delete(vendorDataVendor);
                    _blTokens.UpdateUserJWTToken(driverTaxi.UserId, string.Empty);
                }

                Uow.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool InsertDriver(AssignDriverVewModel model, int UserId)
        {
            Drivers driver = new Drivers()
            {
                CreateDate = _blGeneral.GetDateTimeNow(),
                DriverGuid = Guid.NewGuid(),
                Email = model.Email,
                PhoneNumberStc = model.PhoneNumberStc,
                NameAr = model.NameAr,
                NameEn = model.NameEn,
                Gender = (byte)model.Gender,
                UserUpdate = UserId,
                CityID = model.CityID,
                NationalityID = model.NationalityID,
                Address = model.Address,
                BirthDateType = model.BirthDateType,
                CarNumber = model.CarNumber,
                FileNumber = model.FileNumber,
                IBANNumber = model.IBANNumber,
                HijiriInsuranceEndDate = model.HijiriInsuranceEndDate,
                IDNo = model.IDNo,
                NickName = model.NickName,
                VerifyStc = model.VerifyStc,
                PhoneType = model.PhoneType,
                CarSerialNumber = model.CarSerialNumber,
                InsuranceEndDateType = model.DriverInsuranceEndDateType,
                PCOEndDateType = model.PCOEndDateType,
                PrivateLicenseTypeEndDate = model.PrivateLicenseTypeEndDate,
                PrivateLicenseNumber = model.PrivateLicenseNumber,
                InsuranceNumber = model.InsuranceNumber,
                HijriBirthDate = model.HijriBirthDate,
                PrivateHijriLicenseEndDate = model.PrivateHijriLicenseEndDate,
                PhoneNumber = model.PhoneNumberStc,
                IsDeleted = false,
                UserId = UserId,
                IsEnable = false,
                RequestStatusId = (int)RequestStatusEnum.Under_Scrutiny,
                OpenTransaction = false,
                IsActive = true,
                BankAccountPicture = model.BankAccountPicture,
                PersonalPicture = model.PersonalPicture,
                LicensePicture = model.LicensePicture,
                IDPicture = model.IDPicture,
                CarPictrue = model.CarPictrue,
                CarLicensePicture = model.CarLicensePicture,
                RegionCityID = model.RegionCityID,
            };
            if (!string.IsNullOrEmpty(model.BirthDate))
            {
                driver.BirthDate = DateTime.ParseExact(model.BirthDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            }
            if (!string.IsNullOrEmpty(model.InsuranceEndDate))
            {
                driver.InsuranceEndDate = DateTime.ParseExact(model.InsuranceEndDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            }
            if (!string.IsNullOrEmpty(model.PrivateLicenseEndDate))
            {
                driver.PrivateLicenseEndDate = DateTime.ParseExact(model.PrivateLicenseEndDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            }
            Uow.Drivers.Insert(driver);
            Uow.Commit();
            return true;

        }
        public bool UpdateDriver(AssignDriverVewModel model, int UserId)
        {
            var driver = GetById(model.DriverID);
            if (driver != null)
            {

                driver.UpdateDate = _blGeneral.GetDateTimeNow();
                driver.Email = model.Email;
                driver.PhoneNumberStc = model.PhoneNumberStc;
                driver.NameAr = model.NameAr;
                driver.NameEn = model.NameEn;
                driver.Gender = (byte)model.Gender;
                driver.UserUpdate = UserId;
                driver.CityID = model.CityID;
                driver.NationalityID = model.NationalityID;
                driver.Address = model.Address;
                driver.BirthDateType = model.BirthDateType;
                driver.CarNumber = model.CarNumber;
                driver.FileNumber = model.FileNumber;
                driver.IBANNumber = model.IBANNumber;
                driver.HijiriInsuranceEndDate = model.HijiriInsuranceEndDate;
                driver.IDNo = model.IDNo;
                driver.NickName = model.NickName;
                driver.VerifyStc = model.VerifyStc;
                driver.PhoneType = model.PhoneType;
                driver.CarSerialNumber = model.CarSerialNumber;
                driver.InsuranceEndDateType = model.InsuranceEndDateType;
                driver.PCOEndDateType = model.PCOEndDateType;
                driver.PrivateLicenseTypeEndDate = model.PrivateLicenseTypeEndDate;
                driver.PrivateLicenseNumber = model.PrivateLicenseNumber;
                driver.InsuranceNumber = model.InsuranceNumber;
                driver.HijriBirthDate = model.HijriBirthDate;
                driver.PrivateHijriLicenseEndDate = model.PrivateHijriLicenseEndDate;
                driver.PrivateLicenseTypeEndDate = model.PrivateLicenseTypeEndDate;
                driver.RegionCityID = model.RegionCityID;
                if (!string.IsNullOrEmpty(model.BirthDate))
                {
                    driver.BirthDate = DateTime.ParseExact(model.BirthDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                }
                if (!string.IsNullOrEmpty(model.InsuranceEndDate))
                {
                    driver.InsuranceEndDate = DateTime.ParseExact(model.InsuranceEndDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                }
                if (!string.IsNullOrEmpty(model.PrivateLicenseEndDate))
                {
                    driver.PrivateLicenseEndDate = DateTime.ParseExact(model.PrivateLicenseEndDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                }

                if (!string.IsNullOrEmpty(model.BankAccountPicture) && model.BankAccountPicture != driver.BankAccountPicture)
                {
                    driver.BankAccountPicture = model.BankAccountPicture;
                }
                if (!string.IsNullOrEmpty(model.PersonalPicture) && model.PersonalPicture != driver.PersonalPicture)
                {
                    driver.PersonalPicture = model.PersonalPicture;
                }
                if (!string.IsNullOrEmpty(model.LicensePicture) && model.LicensePicture != driver.LicensePicture)
                {
                    driver.LicensePicture = model.LicensePicture;
                }
                if (!string.IsNullOrEmpty(model.IDPicture) && model.IDPicture != driver.IDPicture)
                {
                    driver.IDPicture = model.IDPicture;
                }
                if (!string.IsNullOrEmpty(model.CarPictrue) && model.CarPictrue != driver.CarPictrue)
                {
                    driver.CarPictrue = model.CarPictrue;
                }
                if (!string.IsNullOrEmpty(model.CarLicensePicture) && model.CarLicensePicture != driver.CarLicensePicture)
                {
                    driver.CarLicensePicture = model.CarLicensePicture;
                }
                Uow.Drivers.Update(driver);
                Uow.Commit();
                return true;
            }
            else
            {
                return false;
            }

        }
        /// Update Driver   تعديل  بيانات سائق موجود
        public bool UpdateOnline(int DriverID, int userId,bool acccountStatus)
        {
            var driverobj = GetById(DriverID);
            if (driverobj != null)
            {
                driverobj.UserUpdate = userId;
                driverobj.UpdateDate = _blGeneral.GetDateTimeNow();
                driverobj.IsOnline = acccountStatus;
                driverobj = Uow.Drivers.Update(driverobj);
                Uow.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool UpdateOffline(int userId)
        {
            var driverobj = GetDriverByUserId(userId);
            if (driverobj != null)
            {
                driverobj.UserUpdate = userId;
                driverobj.UpdateDate = _blGeneral.GetDateTimeNow();
                driverobj.IsOnline = false;
                driverobj = Uow.Drivers.Update(driverobj);
                Uow.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool UpdateDriverApi(UpdateDriverViewModel model, string profilePhotofileName, int driversid, int UserId, string lecine, string carpic, string idpic, string carlicence)
        {
            var Customers = Uow.Drivers.GetAll(s => s.DriversID == driversid).FirstOrDefault();
            Customers.UpdateDate = _blGeneral.GetDateTimeNow();
            Customers.Email = model.email;
            Customers.PhoneNumber = model.mobileNo;
            Customers.NameAr = model.name;
            Customers.NameEn = model.name;
            Customers.IDNo = model.iDNo;
            Customers.Gender = (byte)model.gender;
            Customers.UserUpdate = UserId;
            Customers.CityID = model.cityID;
            Customers.NationalityID = model.nationaltyID;
            if (!string.IsNullOrWhiteSpace(profilePhotofileName))
            {
                Customers.PersonalPicture = profilePhotofileName;
            }
            if (!string.IsNullOrWhiteSpace(lecine))
            {
                Customers.LicensePicture = lecine;
            }
            if (!string.IsNullOrWhiteSpace(carpic))
            {
                Customers.CarPictrue = carpic;
            }
            if (!string.IsNullOrWhiteSpace(idpic))
            {
                Customers.IDPicture = idpic;
            }
            if (!string.IsNullOrWhiteSpace(carlicence))
            {
                Customers.CarLicensePicture = carlicence;
            }
            Uow.Drivers.Update(Customers);
            Uow.Commit();
            return true;
        }
        public DriverViewModelApi GetDriversViewModelApi(int driversId, string lang, string MainPathView, int userId)
        {
            var getData = Uow.Drivers.GetAll(x => !x.IsDeleted
                                                   && x.DriversID == driversId
                , "").OrderByDescending(p => p.CreateDate)
            .Select(p => new DriverViewModelApi()
            {
                cityID = p.CityID,
                gender = p.Gender,
                mobileNo = p.PhoneNumber,
                email = p.Email,
                iDNo = p.IDNo,
                name = lang == "ar" ? p.NameAr : p.NameEn,
                nationalityID = p.NationalityID != null ? (int)p.NationalityID : 0,
                address = !string.IsNullOrWhiteSpace(p.Address) ? p.Address : "",
                cityName = lang == "ar" ? p.City.NameAR : p.City.NameEN,
                nationalityName = lang == "ar" ? p.Nationality.NameAR : p.Nationality.NameEN,
                licensePic = !string.IsNullOrWhiteSpace(p.LicensePicture) ? (MainPathView + p.LicensePicture) : "",
                driverID = p.DriversID,
                carPic = !string.IsNullOrWhiteSpace(p.CarPictrue) ? (MainPathView + p.CarPictrue) : "",
                iDPic = !string.IsNullOrWhiteSpace(p.IDPicture) ? (MainPathView + p.IDPicture) : "",
                carLicensePicture = !string.IsNullOrWhiteSpace(p.CarLicensePicture) ? (MainPathView + p.CarLicensePicture) : "",
                personalPic = !string.IsNullOrWhiteSpace(p.PersonalPicture) ? (MainPathView + p.PersonalPicture) : "",
                wallet = lang == "ar" ? "حصلت علي " + GetLastBlance(driversId) + " ريـال" : "you Collected " + GetLastBlance(driversId) + " SR",
                notificationsCount = GetUserNotificationNotSeenCount(userId),
                pendingPay = GetAmountPaid(driversId).UnPaid,
                paid = GetAmountPaid(driversId).Paid,
            }).FirstOrDefault();
            return getData;
        }
        public bool AddDriver(RegisterDriverViewModel model, int UserId, string profile, string lecine, string carpic, string idpic, string carlicence)
        {
            Drivers customers = new Drivers()
            {
                CreateDate = _blGeneral.GetDateTimeNow(),
                DriverGuid = Guid.NewGuid(),
                Email = model.email,
                PhoneNumber = model.mobileNo,
                NameAr = model.name,
                NameEn = model.name,
                Gender = (byte)model.gender,
                IsDeleted = false,
                UserId = UserId,
                CityID = model.cityID,
                NationalityID = model.nationaltyID,
                PersonalPicture = !string.IsNullOrEmpty(profile) ? profile : "logo.svg",
                IsEnable = false,
                Address = string.Empty,
                BankAccountPicture = string.Empty,
                BirthDateType = 0,
                CarLicensePicture = carlicence,
                CarNumber = string.Empty,
                CarPictrue = carpic,
                FileNumber = string.Empty,
                IBANNumber = string.Empty,
                HijiriInsuranceEndDate = string.Empty,
                IDNo = model.iDNo,
                NickName = model.name,
                LicensePicture = lecine,
                IDPicture = idpic,
                VerifyStc = (int)VerifyStcEnum.Not_Verified,
                PhoneNumberStc = model.mobileNo,
                PhoneType = 0,
                CarSerialNumber = string.Empty,
                RequestStatusId = (int)RequestStatusEnum.New,
                InsuranceEndDateType = 0,
                OpenTransaction = false,
                PCOEndDateType = 0,
                PrivateLicenseTypeEndDate = 0,
                IsActive = false,
                RegionCityID = model.regionCityID,
            };
            Uow.Drivers.Insert(customers);
            Uow.Commit();
            return true;
        }
        /// Update Driver   تعديل  بيانات سائق موجود
        public bool UpdatePersonalStc(int DriverID, int userId, string mobileNoStc)
        {
            string current_culture = CultureInfo.CurrentCulture.Name;
            var driverobj = GetById(DriverID);
            if (driverobj != null)
            {
                driverobj.UserUpdate = userId;
                driverobj.UpdateDate = _blGeneral.GetDateTimeNow();
                driverobj.VerifyStcDate = _blGeneral.GetDateTimeNow();
                driverobj.PhoneNumberStc = mobileNoStc;
                driverobj.VerifyStc = (int)VerifyStcEnum.Verified;
                driverobj = Uow.Drivers.Update(driverobj);
                Uow.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }
        public Drivers GetByMobileNo(string MobileNo)
        {
            return Uow.Drivers.GetAll(query => !query.IsDeleted && query.PhoneNumber == MobileNo, "City.Region").FirstOrDefault();
        }
        //public bool InsertCaptain(CaptianViewModel viewModel, string conString, int userId)
        //{
        //    #region Stored
        //    StoredResultViewModel _res = new StoredResultViewModel();

        //    string HijriIDDate = "";
        //    var DateId = _blGeneral.GetDateTimeNow();
        //    if (viewModel.IDDate != null)
        //    {
        //        DateId = DateTime.Parse(viewModel.IDDate);
        //        string current_culture = CultureInfo.CurrentCulture.Name;
        //        HijriIDDate = DateTime.Parse(DateId.ToString(), new CultureInfo(current_culture)).ToString("yyyy/MM/dd" + " " + (DateId).ToString("HH:mm:ss.fff"), new CultureInfo("ar-sa"));
        //    }
        //    else
        //    {
        //        HijriIDDate = viewModel.idExpirydate;
        //    }
        //    var FileNumber = _blGeneral.RandomNumber(5);
        //    while (IsExistFileNumber(FileNumber))
        //    {
        //        FileNumber = _blGeneral.RandomNumber(5);
        //    }
        //    using (SqlConnection sqlcon = new SqlConnection(conString))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("proc_AssignDriverService", sqlcon))
        //        {
        //            // add the table-valued-parameter. 
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@MobileNo", viewModel.PhoneNumber);
        //            cmd.Parameters.AddWithValue("@IDNo", viewModel.IDNumber);
        //            cmd.Parameters.AddWithValue("@Name", viewModel.Name);
        //            cmd.Parameters.AddWithValue("@ModelID", 0);
        //            cmd.Parameters.AddWithValue("@UserId", userId);
        //            cmd.Parameters.AddWithValue("@TaxiNumber", viewModel.TaxiNo);
        //            cmd.Parameters.AddWithValue("@IDDate", DateId);
        //            cmd.Parameters.AddWithValue("@HijriIDDate", HijriIDDate);
        //            cmd.Parameters.AddWithValue("@IDPicture", viewModel.IDPicture);
        //            cmd.Parameters.AddWithValue("@LicensePicture", viewModel.LicensePicture);
        //            cmd.Parameters.AddWithValue("@PersonalPicture", viewModel.PersonalPicture);
        //            cmd.Parameters.AddWithValue("@YearID", 0);
        //            cmd.Parameters.AddWithValue("@ColorID", 0);
        //            cmd.Parameters.AddWithValue("@CityID", viewModel.CityID);
        //            cmd.Parameters.AddWithValue("@CarPicture", viewModel.CarPicture);
        //            cmd.Parameters.AddWithValue("@NationaltyID", viewModel.NationaltyID);
        //            cmd.Parameters.AddWithValue("@BackPicture", viewModel.BackPicture);
        //            cmd.Parameters.AddWithValue("@RightSidePicture", viewModel.RightSidePicture);
        //            cmd.Parameters.AddWithValue("@LeftSidePicture", viewModel.LeftSidePicture);
        //            cmd.Parameters.AddWithValue("@InsurancePicture", viewModel.InsurancePicture);
        //            cmd.Parameters.AddWithValue("@CarLicensePicture", viewModel.CarLicensePicture);
        //            cmd.Parameters.AddWithValue("@FrontMattressesPicture", "");
        //            cmd.Parameters.AddWithValue("@PhoneNumberStc", viewModel.PhoneNumberStc);
        //            cmd.Parameters.AddWithValue("@TaxiSerialNumber", viewModel.TaxiNo);
        //            cmd.Parameters.AddWithValue("@Email", viewModel.PhoneNumber + "@LogisticsDriver.com");
        //            cmd.Parameters.AddWithValue("@LicenseNumber", "12345678");
        //            cmd.Parameters.AddWithValue("@FileNumber", FileNumber);
        //            cmd.Parameters.AddWithValue("@PhoneType", 0);
        //            cmd.Parameters.AddWithValue("@gender", (int)Gender.Male);
        //            cmd.Parameters.AddWithValue("@DateNow", _blGeneral.GetDateTimeNow());

        //            // execute
        //            sqlcon.Open();
        //            SqlDataReader ResultsMeeting = cmd.ExecuteReader();
        //            DataTable dtMeeting = new DataTable();
        //            dtMeeting.Load(ResultsMeeting);
        //            if (dtMeeting.Rows.Count > 0)
        //            {
        //                _res.ErrorNumber = Convert.ToString(dtMeeting.Rows[0]["ErrorNumber"]);
        //                _res.ErrorSeverity = Convert.ToString(dtMeeting.Rows[0]["ErrorSeverity"]);
        //                _res.ErrorState = Convert.ToString(dtMeeting.Rows[0]["ErrorState"]);
        //                _res.ErrorProcedure = Convert.ToString(dtMeeting.Rows[0]["ErrorProcedure"]);
        //                _res.ErrorLine = Convert.ToString(dtMeeting.Rows[0]["ErrorLine"]);
        //                _res.ErrorMessage = Convert.ToString(dtMeeting.Rows[0]["ErrorMessage"]);
        //            }
        //            sqlcon.Close();
        //        }
        //    }
        //    if (_res.ErrorLine != null)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        return true;
        //    }
        //    #endregion
        //}
        #endregion
        #region GetDriveData
        /// Get All Driver   عرض كل  بيانات السائقين
        public IQueryable<Drivers> GetAllDriver()
        {
            return Uow.Drivers.GetAll(query => !query.IsDeleted);
        }
        public IQueryable<Drivers> GetAllDriverActive()
        {
            return Uow.Drivers.GetAll(query => !query.IsDeleted && query.IsActive);
        }
        public Drivers GetDriverByUserId(int UserId)
        {
            return Uow.Drivers.GetAll(query => !query.IsDeleted && query.UserId == UserId).FirstOrDefault(); ;
        }
        /// Get All Driver   عرض كل  بيانات السائقين مع الانكلود
        /// <summary>
        /// جلب رقم هاتف السائق بناءا على اليوزر اي دي
        /// </summary>
        /// <param name="Userid"></param>
        /// <returns></returns>
        /// <summary>
        /// Get Driver By UserId
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="include"></param>
        /// <returns></returns>
        public Drivers GetDriverByUserId(int UserId, string include)
        {
            return Uow.Drivers.GetAll(x => x.UserId == UserId && !x.IsDeleted, include).FirstOrDefault();
        }
        public Drivers GetDriverByPhoneNumber(string PhoneNumber, string include)
        {
            return Uow.Drivers.GetAll(x => x.PhoneNumber == PhoneNumber && !x.IsDeleted, include).FirstOrDefault();
        }
        public Drivers GetDriverByIDNO(string IDNO, string include)
        {
            return Uow.Drivers.GetAll(x => x.IDNo == IDNO && !x.IsDeleted, include).FirstOrDefault();
        }
        public Drivers GetById(int id)
        {
            return Uow.Drivers.GetAll(x => x.DriversID == id && !x.IsDeleted).FirstOrDefault();
        }

        /// <summary>
        /// get Driver By id and lang
        /// </summary>
        /// <param name="DriverID"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        public FinancailViewModel GetFinancialbyDriver(int DriversID, int page, string lang, string vendorpath, bool isPaid, DateTime? date, string customerPath)
        {
            var Data = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.DriversID == DriversID && x.DriverCharge > 0 &&
            (isPaid == true ? (x.CaptainPaidID == (int)PaidStatusEnum.Cash_Transfered || x.CaptainPaidID == (int)PaidStatusEnum.STC_Transfered) : (x.CaptainPaidID == (int)PaidStatusEnum.Created))
            && (x.OrderStatusID == (int)OrderStatusEnum.Delivered || x.OrderStatusID == (int)OrderStatusEnum.Cancel)
            && (date != null ? date.Value.Date == x.CreateDate.Date : true),
            "Vendors,Orders.CustomerLocation,OrderStatus").OrderByDescending(s => s.CreateDate).Select(m => new ListProductWallet()
            {
                deliveryPrice = m.DriverBlance.FirstOrDefault(s => s.TypeOperationID == (int)TypeOperationEnum.CR) != null ? m.DriverBlance.FirstOrDefault(s => s.TypeOperationID == (int)TypeOperationEnum.CR).Amount : 0,
                vendorImage = !string.IsNullOrWhiteSpace(m.Vendors.Logo) ? vendorpath + m.Vendors.Logo : "",
                vendorId = m.VendorsID,
                trackNo = m.TrackNo,
                orderId = m.OrderVendorID,
                orderStatusId = m.OrderStatusID,
                customerLogo = !string.IsNullOrWhiteSpace(m.Orders.Customers.ProfilePic) ? customerPath + m.Orders.Customers.ProfilePic : "",
                customerName = m.Orders.Customers.FirstName + " " + m.Orders.Customers.SeconedName,
                vendorName = lang == "ar" ? m.Vendors.StoreNameAr : m.Vendors.StoreNameEn,
                date = m.CreateDate.ToString("dd-MM-yyyy hh:mm tt"),
                address = m.Orders.CustomerLocation.Address,
                status = lang == "ar" ? m.OrderStatus.NameAR : m.OrderStatus.NameEN,
                colorHex = (m.CaptainPaidID == (int)PaidStatusEnum.Cash_Transfered || m.CaptainPaidID == (int)PaidStatusEnum.STC_Transfered) ? "#FFF9D9" : "#E5F9ED",
                colorText = (m.CaptainPaidID == (int)PaidStatusEnum.Cash_Transfered || m.CaptainPaidID == (int)PaidStatusEnum.STC_Transfered) ? "#FFBB00" : "#00BE4C",
            }).ToList();
            Data = Data.Skip(Convert.ToInt32(page - 1) * 10).ToList();
            var DataTake = Data.Take(((Convert.ToInt32(page) * 10)) - (Convert.ToInt32(page - 1) * 10)).ToList();
            bool NextPage = false;
            if (Data.Count() > 10)
            {
                NextPage = true;
            }
            var model = new FinancailViewModel();
            model.listOrders = DataTake;
            model.isNextPage = NextPage;
            model.countPaid = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.DriversID == DriversID && (x.CaptainPaidID == (int)PaidStatusEnum.Cash_Transfered || x.CaptainPaidID == (int)PaidStatusEnum.STC_Transfered)
            && (x.OrderStatusID == (int)OrderStatusEnum.Delivered)).Count();
            model.countUnPaid = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.DriversID == DriversID && (x.CaptainPaidID == (int)PaidStatusEnum.Created)
            && (x.OrderStatusID == (int)OrderStatusEnum.Delivered)).Count();
            model.amountPaid = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.DriversID == DriversID && (x.CaptainPaidID == (int)PaidStatusEnum.Cash_Transfered || x.CaptainPaidID == (int)PaidStatusEnum.STC_Transfered)
            && (x.OrderStatusID == (int)OrderStatusEnum.Delivered)).Sum(x => x.DriverCharge);
            model.amountUnPaid = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.DriversID == DriversID && (x.CaptainPaidID == (int)PaidStatusEnum.Created)
            && (x.OrderStatusID == (int)OrderStatusEnum.Delivered)).Sum(x => x.DriverCharge);
            return model;
        }
        public class DriverAmountPaid
        {
            public decimal Paid { get; set; }
            public decimal UnPaid { get; set; }
        }
        public DriverAmountPaid GetAmountPaid(int DriversID)
        {
            var model = new DriverAmountPaid();
            model.Paid = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.DriversID == DriversID && (x.CaptainPaidID == (int)PaidStatusEnum.Cash_Transfered || x.CaptainPaidID == (int)PaidStatusEnum.STC_Transfered)
            && (x.OrderStatusID == (int)OrderStatusEnum.Delivered)).Sum(x => x.DriverCharge);
            model.UnPaid = Uow.OrderVendor.GetAll(x => !x.IsDeleted && x.DriversID == DriversID && (x.CaptainPaidID == (int)PaidStatusEnum.Created)
            && (x.OrderStatusID == (int)OrderStatusEnum.Delivered)).Sum(x => x.DriverCharge);
            return model;
        }
        public CustomerOrdersViewModelApi DriverOrders(string lang, int page, string vendorpath, int DriverId, int type, DateTime? date, string customerPath)
        {
            var Data = Uow.OrderVendor.GetAll(s => !s.IsDeleted && s.IsEnable && s.DriversID == DriverId &&
            (type == 1 ? (s.OrderStatusID == (int)OrderStatusEnum.Being_Delivery) : (s.OrderStatusID == (int)OrderStatusEnum.Delivered || s.OrderStatusID == (int)OrderStatusEnum.Cancel))
            && (date != null ? s.CreateDate.Date == date.Value.Date : true),
            "Vendors,Orders.CustomerLocation,OrderStatus").OrderByDescending(s => s.CreateDate).Select(m => new ListProduct()
            {
                customerLat = m.Orders.CustomerLocation.Lat,
                customerLng = m.Orders.CustomerLocation.Lng,
                vendorLat = m.Vendors.Lat,
                vendorLng = m.Vendors.Lng,
                customerName = m.Orders.Customers.FirstName + " " + m.Orders.Customers.SeconedName,
                customerLogo = !string.IsNullOrWhiteSpace(m.Orders.Customers.ProfilePic) ? customerPath + m.Orders.Customers.ProfilePic : "",
                vendorImage = !string.IsNullOrWhiteSpace(m.Vendors.Logo) ? vendorpath + m.Vendors.Logo : "",
                vendorId = m.VendorsID,
                customerMobile = "0" + m.Orders.Customers.MobileNo,
                customerWhats = "+966" + m.Orders.Customers.MobileNo,
                vendorMobile = "0" + m.Vendors.MobileNo,
                vendorWhats = "+966" + m.Vendors.MobileNo,
                trackNo = m.TrackNo,
                orderId = m.OrderVendorID,
                orderStatusId = m.OrderStatusID,
                vendorName = lang == "ar" ? m.Vendors.StoreNameAr : m.Vendors.StoreNameEn,
                paymentTypestr = lang == "ar" ? (m.Orders.PaymentTypeId == (int)PaymentTypeEnum.COD ? "نقد عند الإستلام" : m.Orders.PaymentTypeId == (int)PaymentTypeEnum.Payment_Card ? "بطاقة دفع" :
                  m.Orders.PaymentTypeId == (int)PaymentTypeEnum.Bank_Transfer ? "تحويل بنكي" : "المحفظة") :
                  (m.Orders.PaymentTypeId == (int)PaymentTypeEnum.COD ? "Cash on Receive" : m.Orders.PaymentTypeId == (int)PaymentTypeEnum.Payment_Card ? "Payment Card" :
                  m.Orders.PaymentTypeId == (int)PaymentTypeEnum.Bank_Transfer ? "Bank Transfer" : "Wallet"),
                date = m.CreateDate.ToString("hh:mm tt"),
                address = m.Orders.CustomerLocation.Address,
                status = lang == "ar" ? m.OrderStatus.NameAR : m.OrderStatus.NameEN,
                price = lang == "ar" ? m.Price + " ريـال" : m.Price + " SAR",
                isCancel = m.OrderStatusID == (int)OrderStatusEnum.Create ? true : false,
                listItems = m.OrderItems.Where(s => !s.IsDeleted && s.IsEnable).Select(z => new ListItems()
                {
                    productName = lang == "ar" ? z.ProdNameAr : z.ProdNameEn,
                    price = lang == "ar" ? z.Price + " ريـال" : z.Price + " SAR",
                    quantity = z.Quantity,
                }).ToList(),
                countItems = m.OrderItems.Count(),
                colorHex = (type == 1 ? ((m.Orders.PaymentTypeId != (int)PaymentTypeEnum.COD ? "#B2E0E7" : "#32CD32"))
                  : (m.OrderStatusID == (int)OrderStatusEnum.Delivered ? "#B2E0E7" : "#32CD32"))
            }).ToList();
            Data = Data.Skip(Convert.ToInt32(page - 1) * 10).ToList();
            var DataTake = Data.Take(((Convert.ToInt32(page) * 10)) - (Convert.ToInt32(page - 1) * 10)).ToList();
            bool NextPage = false;
            if (Data.Count() > 10)
            {
                NextPage = true;
            }
            //if (type == 1)
            //{
            //    DataTake = DataTake.OrderByDescending(s => s.orderStatusId).ToList();
            //}
            var model = new CustomerOrdersViewModelApi();
            model.listProduct = DataTake;
            model.isNextPage = NextPage;
            return model;
        }
        public CustomerOrdersViewModelApi DriverOrdersHome(string lang, int page, string vendorpath, int DriverId, int type, DateTime? date, string customerPath)
        {
            var Data = Uow.OrderVendor.GetAll(s => !s.IsDeleted && s.IsEnable && s.DriversID == DriverId &&
            (type == 1 ? (s.OrderStatusID == (int)OrderStatusEnum.Assign) : type == 2 ? (s.OrderStatusID == (int)OrderStatusEnum.OnWay_Store) : (s.OrderStatusID == (int)OrderStatusEnum.Being_Delivery))
            && (date != null ? s.CreateDate.Date == date.Value.Date : true),
            "Vendors,Orders.CustomerLocation.Block,OrderStatus").OrderByDescending(s => s.CreateDate).Select(m => new ListProduct()
            {
                customerLat = m.Orders.CustomerLocation.Lat,
                customerLng = m.Orders.CustomerLocation.Lng,
                vendorLat = m.Vendors.Lat,
                customerMobile = "0" + m.Orders.Customers.MobileNo,
                customerWhats = "+966" + m.Orders.Customers.MobileNo,
                vendorMobile = "0" + m.Vendors.MobileNo,
                vendorWhats = "+966" + m.Vendors.MobileNo,
                vendorLng = m.Vendors.Lng,
                customerName = m.Orders.Customers.FirstName + " " + m.Orders.Customers.SeconedName,
                customerLogo = !string.IsNullOrWhiteSpace(m.Orders.Customers.ProfilePic) ? customerPath + m.Orders.Customers.ProfilePic : "",
                vendorImage = !string.IsNullOrWhiteSpace(m.Vendors.Logo) ? vendorpath + m.Vendors.Logo : "",
                vendorId = m.VendorsID,
                trackNo = m.TrackNo,
                orderId = m.OrderVendorID,
                orderStatusId = m.OrderStatusID,
                vendorName = lang == "ar" ? m.Vendors.StoreNameAr : m.Vendors.StoreNameEn,
                paymentTypestr = lang == "ar" ? (m.Orders.PaymentTypeId == (int)PaymentTypeEnum.COD ? "نقد عند الإستلام" : m.Orders.PaymentTypeId == (int)PaymentTypeEnum.Payment_Card ? "بطاقة دفع" :
                  m.Orders.PaymentTypeId == (int)PaymentTypeEnum.Bank_Transfer ? "تحويل بنكي" : "المحفظة") :
                  (m.Orders.PaymentTypeId == (int)PaymentTypeEnum.COD ? "Cash on Receive" : m.Orders.PaymentTypeId == (int)PaymentTypeEnum.Payment_Card ? "Payment Card" :
                  m.Orders.PaymentTypeId == (int)PaymentTypeEnum.Bank_Transfer ? "Bank Transfer" : "Wallet"),
                date = m.CreateDate.ToString("dd-MM-yyyy hh:mm tt"),
                address = m.Orders.CustomerLocation.Address,
                blockName = lang == "ar" ? m.Orders.CustomerLocation.Block.NameAR : m.Orders.CustomerLocation.Block.NameEN,
                status = lang == "ar" ? m.OrderStatus.NameAR : m.OrderStatus.NameEN,
                price = lang == "ar" ? m.Price + " ريـال" : m.Price + " SAR",
                isCancel = m.OrderStatusID == (int)OrderStatusEnum.Create ? true : false,
                listItems = m.OrderItems.Where(s => !s.IsDeleted && s.IsEnable).Select(z => new ListItems()
                {
                    productName = lang == "ar" ? z.ProdNameAr : z.ProdNameEn,
                    price = lang == "ar" ? z.Price + " ريـال" : z.Price + " SAR",
                    quantity = z.Quantity,
                }).ToList(),
                countItems = m.OrderItems.Count(),
                colorHex = (type == 1 ? ((m.Orders.PaymentTypeId == (int)PaymentTypeEnum.Payment_Card || m.Orders.PaymentTypeId == (int)PaymentTypeEnum.Bank_Transfer || m.Orders.PaymentTypeId == (int)PaymentTypeEnum.Wallet ? "#964B00" : "#32CD32"))
                  : (m.OrderStatusID == (int)OrderStatusEnum.Delivered ? "#32CD32" : "#964B00"))
            }).ToList();
            Data = Data.Skip(Convert.ToInt32(page - 1) * 10).ToList();
            var DataTake = Data.Take(((Convert.ToInt32(page) * 10)) - (Convert.ToInt32(page - 1) * 10)).ToList();
            bool NextPage = false;
            if (Data.Count() > 10)
            {
                NextPage = true;
            }
            //if (type == 1)
            //{
            //    DataTake = DataTake.OrderByDescending(s => s.orderStatusId).ToList();
            //}
            var model = new CustomerOrdersViewModelApi();
            model.listProduct = DataTake;
            model.isNextPage = NextPage;
            return model;
        }
        public decimal GetLastBlance(int DriverID)
        {
            var Balance = Uow.DriverBlance.GetAll(e => e.DriversID == DriverID);
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
        public decimal GetDriverEvaluate(int DriverId)
        {
            decimal Rate = 0;
            var TripList = Uow.OrderVendor.GetAll(e => e.DriversID == DriverId && e.OrderRate.Any(w => !w.IsDeleted && w.RateDelivery != null)).Select(x => x.OrderVendorID).ToList();
            if (TripList.Any())
            {
                var CountTrips = Uow.OrderRate.GetAll(e => TripList.Any(s => s == e.OrderVendorID) && e.RateDelivery != null).Count();
                decimal TotalRates = Uow.OrderRate.GetAll(e => TripList.Any(s => s == e.OrderVendorID) && e.RateDelivery != null).Any() ?
                    Uow.OrderRate.GetAll(e => TripList.Any(s => s == e.OrderVendorID) && e.RateDelivery != null).Sum(e => e.RateDelivery) : 0;
                decimal MaxRate = CountTrips * 100;
                var DriverRate = TotalRates;
                var RateBefore = (DriverRate / MaxRate);
                Rate = Math.Round((RateBefore * 100), 2);
            }
            return Rate;
        }
        public IQueryable<Drivers> GetAllDriverByAgentOperator()
        {
            return Uow.Drivers.GetAll(query => !query.IsDeleted);
        }
        public IQueryable<DriverViewModel> GetAllDriverData(string mobile, string[] ListCityID, int? DriverID, DateTime? FromDate, DateTime? ToDate, string accLanguage, string DriverImageView)
        {
            var list = new List<DriverViewModel>();
            if (ListCityID != null)
            {
                if (ListCityID.Count(p => !string.IsNullOrEmpty(p)) == 0)
                {
                    ListCityID = null;
                }
            }
            var data = Uow.Drivers.GetAll(query => !query.IsDeleted && query.IsActive
                && (mobile != null ? mobile == query.PhoneNumber : true)
             && (ListCityID != null ? ListCityID.Any(y => y == query.CityID.ToString()) : true)
            && (DriverID != null ? query.DriversID == DriverID : true)
            && (FromDate != null ? query.CreateDate.Date >= FromDate.Value.Date : true)
           && (ToDate != null ? query.CreateDate.Date <= ToDate.Value.Date : true)
            ).OrderByDescending(a => a.CreateDate).Select(m => new DriverViewModel()
            {
                DriversID = m.DriversID,
                DriverGuid = m.DriverGuid,
                DriverName = accLanguage == "ar" ? m.NameAr : m.NameEn,
                CityName = accLanguage == "ar" ? m.City.NameAR : m.City.NameEN,
                CountryName = accLanguage == "ar" ? m.City.Region.Country.NameAR : m.City.Region.Country.NameEN,
                Address = m.Address,
                CityID = m.CityID,
                Email = m.Email ?? "---",
                Gender = m.Gender,
                BirthDateType = m.BirthDateType,
                HijiriInsuranceEndDate = m.HijiriInsuranceEndDate,
                HijriBirthDate = m.HijriBirthDate,
                HijriIDDate = m.HijriIDDate,
                IDNo = !string.IsNullOrWhiteSpace(m.IDNo) ? m.IDNo : "",
                InsuranceEndDateType = m.InsuranceEndDateType,
                InsuranceNumber = m.InsuranceNumber,
                PCOEndDateType = m.PCOEndDateType,
                PCOHijriEndDate = m.PCOHijriEndDate,
                PCONumber = m.PCONumber,
                PhoneNumber = m.PhoneNumber,
                PrivateHijriLicenseEndDate = m.PrivateHijriLicenseEndDate,
                PrivateLicenseNumber = m.PrivateLicenseNumber,
                PrivateLicenseTypeEndDate = m.PrivateLicenseTypeEndDate,
                GenderName = accLanguage == "ar" ? (m.Gender == (byte)Gender.Male ? "Male" : m.Gender == (byte)Gender.Female ? "Female"
                : m.Gender == (byte)Gender.Prefer_not_to_specify ? "Not to specify" : string.Empty) :
                (m.Gender == (byte)Gender.Male ? "ذكر" : m.Gender == (byte)Gender.Female ? "أنثي" : m.Gender == (byte)Gender.Prefer_not_to_specify ?
                "غير محدد" : string.Empty),
                BirthDate = m.BirthDate != null ? m.BirthDate.Value.ToString("yyyy-MM-dd") : string.Empty,
                InsuranceEndDate = m.InsuranceEndDate != null ? m.InsuranceEndDate.Value.ToString("yyyy-MM-dd") : string.Empty,
                IDPicture = !string.IsNullOrEmpty(m.IDPicture) ? (DriverImageView + m.IDPicture) : string.Empty,
                LicensePicture = !string.IsNullOrEmpty(m.LicensePicture) ? (DriverImageView + m.LicensePicture) : string.Empty,
                PCOEndDate = m.PCOEndDate != null ? m.PCOEndDate.Value.ToString("yyyy-MM-dd") : string.Empty,
                PersonalPicture = !string.IsNullOrEmpty(m.PersonalPicture) ? (DriverImageView + m.PersonalPicture) : string.Empty,
                PrivateLicenseEndDate = m.PrivateLicenseEndDate != null ? m.PrivateLicenseEndDate.Value.ToString("yyyy-MM-dd") : string.Empty,
                IDDate = m.IDDate != null ? m.IDDate.Value.ToString("yyyy-MM-dd") : string.Empty,
                CreateDateString = m.CreateDate.ToString("yyyy-MM-dd"),
                RequestStatusName = accLanguage == "ar" ? (m.RequestStatusId == (int)RequestStatusEnum.New ? "جديد" : m.RequestStatusId == (int)RequestStatusEnum.Hanging ? "معلق"
                : m.RequestStatusId == (int)RequestStatusEnum.Rejected ? "مرفوض" : m.RequestStatusId == (int)RequestStatusEnum.Activated ? "تم التفعيل" : "تحت التدقيق") :
                (m.RequestStatusId == (int)RequestStatusEnum.New ? "New" : m.RequestStatusId == (int)RequestStatusEnum.Hanging ? "Hanging"
                : m.RequestStatusId == (int)RequestStatusEnum.Rejected ? "Rejected" : m.RequestStatusId == (int)RequestStatusEnum.Activated ? "Activated" : "Under Scrutiny"),

                RegionName = accLanguage == "ar" ? m.City.Region.Country.NameAR : m.City.Region.Country.NameEN,
                NationalityName = m.Nationality != null ? accLanguage == "ar" ? m.Nationality.NameAR : m.Nationality.NameEN : " --- ",
                RequestStatusId = m.RequestStatusId,
                IsEnable = m.IsEnable,
            });
            foreach (var item in data)
            {
                item.Balance = GetLastBlance(item.DriversID);
                item.Rate = GetDriverEvaluate(item.DriversID);
                list.Add(item);
            }
            return list.AsQueryable();
        }
        public IQueryable<CaptainViewModel> GetAllCaptainViewModelByCity(string[] ListCityID, string[] ListRegionCityID, string accLanguage)
        {
            if (ListCityID != null)
            {
                if (ListCityID.Count(p => !string.IsNullOrEmpty(p)) == 0)
                {
                    ListCityID = null;
                }
            }
            if (ListRegionCityID != null)
            {
                if (ListRegionCityID.Count(p => !string.IsNullOrEmpty(p)) == 0)
                {
                    ListRegionCityID = null;
                }
            }
            var data = Uow.Drivers.GetAll(query => !query.IsDeleted && query.IsActive
             && (ListCityID != null ? ListCityID.Any(y => y == query.CityID.ToString()) : true)
             && (ListRegionCityID != null ? ListRegionCityID.Any(y => y == query.RegionCityID.ToString()) : true)
            ).OrderByDescending(a => a.CreateDate).Select(m => new CaptainViewModel()
            {
                DriversID = m.DriversID,
                DriverName = accLanguage == "ar" ? m.NameAr : m.NameEn,
                CityName = accLanguage == "ar" ? m.City.NameAR : m.City.NameEN,
                CityID = m.CityID,
                PhoneNumber = m.PhoneNumber,

            });
            return data;
        }
        public IQueryable<DriverViewModel> GetAllDriver_New(string[] ListCountryID, string[] ListAgentID, string[] ListCityID, int? DriverID, DateTime? FromDate, DateTime? ToDate, string accLanguage, string DriverImageView)
        {
            var list = new List<DriverViewModel>();
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
            var data = Uow.Drivers.GetAll(query => !query.IsDeleted
            && (query.RequestStatusId == (int)RequestStatusEnum.New)
                && (ListCountryID != null ? ListCountryID.Any(y => y == query.City.Region.CountryID.ToString()) : true)
             && (ListCityID != null ? ListCityID.Any(y => y == query.CityID.ToString()) : true)
            && (DriverID != null ? query.DriversID == DriverID : true)
            && (FromDate != null ? query.CreateDate.Date >= FromDate.Value.Date : true)
           && (ToDate != null ? query.CreateDate.Date <= ToDate.Value.Date : true)
            ).OrderByDescending(a => a.CreateDate).Select(m => new DriverViewModel()
            {
                DriversID = m.DriversID,
                DriverGuid = m.DriverGuid,
                DriverName = accLanguage == "ar" ? m.NameAr : m.NameEn,
                CityName = accLanguage == "ar" ? m.City.NameAR : m.City.NameEN,
                CountryName = accLanguage == "ar" ? m.City.Region.Country.NameAR : m.City.Region.Country.NameEN,
                Address = m.Address,
                CityID = m.CityID,
                Email = m.Email ?? "---",
                Gender = m.Gender,
                BirthDateType = m.BirthDateType,
                HijiriInsuranceEndDate = m.HijiriInsuranceEndDate,
                HijriBirthDate = m.HijriBirthDate,
                HijriIDDate = m.HijriIDDate,
                IDNo = m.IDNo,
                InsuranceEndDateType = m.InsuranceEndDateType,
                InsuranceNumber = m.InsuranceNumber,
                PCOEndDateType = m.PCOEndDateType,
                PCOHijriEndDate = m.PCOHijriEndDate,
                PCONumber = m.PCONumber,
                PhoneNumber = m.PhoneNumber,
                PrivateHijriLicenseEndDate = m.PrivateHijriLicenseEndDate,
                PrivateLicenseNumber = m.PrivateLicenseNumber,
                PrivateLicenseTypeEndDate = m.PrivateLicenseTypeEndDate,
                GenderName = accLanguage == "ar" ? (m.Gender == (byte)Gender.Male ? "Male" : m.Gender == (byte)Gender.Female ? "Female"
                : m.Gender == (byte)Gender.Prefer_not_to_specify ? "Not to specify" : string.Empty) :
                (m.Gender == (byte)Gender.Male ? "ذكر" : m.Gender == (byte)Gender.Female ? "أنثي" : m.Gender == (byte)Gender.Prefer_not_to_specify ?
                "غير محدد" : string.Empty),
                BirthDate = m.BirthDate != null ? m.BirthDate.Value.ToString("yyyy-MM-dd") : string.Empty,
                InsuranceEndDate = m.InsuranceEndDate != null ? m.InsuranceEndDate.Value.ToString("yyyy-MM-dd") : string.Empty,
                IDPicture = !string.IsNullOrEmpty(m.IDPicture) ? (DriverImageView + m.IDPicture) : string.Empty,
                LicensePicture = !string.IsNullOrEmpty(m.LicensePicture) ? (DriverImageView + m.LicensePicture) : string.Empty,
                PCOEndDate = m.PCOEndDate != null ? m.PCOEndDate.Value.ToString("yyyy-MM-dd") : string.Empty,
                PersonalPicture = !string.IsNullOrEmpty(m.PersonalPicture) ? (DriverImageView + m.PersonalPicture) : string.Empty,
                PrivateLicenseEndDate = m.PrivateLicenseEndDate != null ? m.PrivateLicenseEndDate.Value.ToString("yyyy-MM-dd") : string.Empty,
                IDDate = m.IDDate != null ? m.IDDate.Value.ToString("yyyy-MM-dd") : string.Empty,
                CreateDateString = m.CreateDate.ToString("yyyy-MM-dd"),
                RequestStatusName = accLanguage == "ar" ? (m.RequestStatusId == (int)RequestStatusEnum.New ? "جديد" : m.RequestStatusId == (int)RequestStatusEnum.Hanging ? "معلق"
                : m.RequestStatusId == (int)RequestStatusEnum.Rejected ? "مرفوض" : m.RequestStatusId == (int)RequestStatusEnum.Activated ? "تم التفعيل" : "تحت التدقيق") :
                (m.RequestStatusId == (int)RequestStatusEnum.New ? "New" : m.RequestStatusId == (int)RequestStatusEnum.Hanging ? "Hanging"
                : m.RequestStatusId == (int)RequestStatusEnum.Rejected ? "Rejected" : m.RequestStatusId == (int)RequestStatusEnum.Activated ? "Activated" : "Under Scrutiny"),

                RegionName = accLanguage == "ar" ? m.City.Region.Country.NameAR : m.City.Region.Country.NameEN,
                NationalityName = m.Nationality != null ? accLanguage == "ar" ? m.Nationality.NameAR : m.Nationality.NameEN : " --- ",
                RequestStatusId = m.RequestStatusId
            });
            foreach (var item in data)
            {
                item.Balance = GetLastBlance(item.DriversID);
                item.Rate = GetDriverEvaluate(item.DriversID);
                list.Add(item);
            }
            return list.AsQueryable();
        }
        public IQueryable<DriverViewModel> GetAllDriver_Under_Scrutiny(string[] ListCountryID, string[] ListAgentID, string[] ListCityID, int? DriverID, DateTime? FromDate, DateTime? ToDate, string accLanguage, string DriverImageView)
        {
            var list = new List<DriverViewModel>();
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
            var data = Uow.Drivers.GetAll(query => !query.IsDeleted
            && (query.RequestStatusId == (int)RequestStatusEnum.Under_Scrutiny)
                && (ListCountryID != null ? ListCountryID.Any(y => y == query.City.Region.CountryID.ToString()) : true)
             && (ListCityID != null ? ListCityID.Any(y => y == query.CityID.ToString()) : true)
            && (DriverID != null ? query.DriversID == DriverID : true)
            && (FromDate != null ? query.CreateDate.Date >= FromDate.Value.Date : true)
           && (ToDate != null ? query.CreateDate.Date <= ToDate.Value.Date : true)
            ).OrderByDescending(a => a.CreateDate).Select(m => new DriverViewModel()
            {
                DriversID = m.DriversID,
                DriverGuid = m.DriverGuid,
                DriverName = accLanguage == "ar" ? m.NameAr : m.NameEn,
                CityName = accLanguage == "ar" ? m.City.NameAR : m.City.NameEN,
                CountryName = accLanguage == "ar" ? m.City.Region.Country.NameAR : m.City.Region.Country.NameEN,
                Address = m.Address,
                CityID = m.CityID,
                Email = m.Email ?? "---",
                Gender = m.Gender,
                BirthDateType = m.BirthDateType,
                HijiriInsuranceEndDate = m.HijiriInsuranceEndDate,
                HijriBirthDate = m.HijriBirthDate,
                HijriIDDate = m.HijriIDDate,
                IDNo = m.IDNo,
                InsuranceEndDateType = m.InsuranceEndDateType,
                InsuranceNumber = m.InsuranceNumber,
                PCOEndDateType = m.PCOEndDateType,
                PCOHijriEndDate = m.PCOHijriEndDate,
                PCONumber = m.PCONumber,
                PhoneNumber = m.PhoneNumber,
                PrivateHijriLicenseEndDate = m.PrivateHijriLicenseEndDate,
                PrivateLicenseNumber = m.PrivateLicenseNumber,
                PrivateLicenseTypeEndDate = m.PrivateLicenseTypeEndDate,
                GenderName = accLanguage == "ar" ? (m.Gender == (byte)Gender.Male ? "Male" : m.Gender == (byte)Gender.Female ? "Female"
                : m.Gender == (byte)Gender.Prefer_not_to_specify ? "Not to specify" : string.Empty) :
                (m.Gender == (byte)Gender.Male ? "ذكر" : m.Gender == (byte)Gender.Female ? "أنثي" : m.Gender == (byte)Gender.Prefer_not_to_specify ?
                "غير محدد" : string.Empty),
                BirthDate = m.BirthDate != null ? m.BirthDate.Value.ToString("yyyy-MM-dd") : string.Empty,
                InsuranceEndDate = m.InsuranceEndDate != null ? m.InsuranceEndDate.Value.ToString("yyyy-MM-dd") : string.Empty,
                IDPicture = !string.IsNullOrEmpty(m.IDPicture) ? (DriverImageView + m.IDPicture) : string.Empty,
                LicensePicture = !string.IsNullOrEmpty(m.LicensePicture) ? (DriverImageView + m.LicensePicture) : string.Empty,
                PCOEndDate = m.PCOEndDate != null ? m.PCOEndDate.Value.ToString("yyyy-MM-dd") : string.Empty,
                PersonalPicture = !string.IsNullOrEmpty(m.PersonalPicture) ? (DriverImageView + m.PersonalPicture) : string.Empty,
                PrivateLicenseEndDate = m.PrivateLicenseEndDate != null ? m.PrivateLicenseEndDate.Value.ToString("yyyy-MM-dd") : string.Empty,
                IDDate = m.IDDate != null ? m.IDDate.Value.ToString("yyyy-MM-dd") : string.Empty,
                CreateDateString = m.CreateDate.ToString("yyyy-MM-dd"),
                RequestStatusName = accLanguage == "ar" ? (m.RequestStatusId == (int)RequestStatusEnum.New ? "جديد" : m.RequestStatusId == (int)RequestStatusEnum.Hanging ? "معلق"
                : m.RequestStatusId == (int)RequestStatusEnum.Rejected ? "مرفوض" : m.RequestStatusId == (int)RequestStatusEnum.Activated ? "تم التفعيل" : "تحت التدقيق") :
                (m.RequestStatusId == (int)RequestStatusEnum.New ? "New" : m.RequestStatusId == (int)RequestStatusEnum.Hanging ? "Hanging"
                : m.RequestStatusId == (int)RequestStatusEnum.Rejected ? "Rejected" : m.RequestStatusId == (int)RequestStatusEnum.Activated ? "Activated" : "Under Scrutiny"),
                RegionName = accLanguage == "ar" ? m.City.Region.Country.NameAR : m.City.Region.Country.NameEN,
                NationalityName = m.Nationality != null ? accLanguage == "ar" ? m.Nationality.NameAR : m.Nationality.NameEN : " --- ",
                RequestStatusId = m.RequestStatusId

            });
            foreach (var item in data)
            {
                item.Balance = GetLastBlance(item.DriversID);
                item.Rate = GetDriverEvaluate(item.DriversID);
                list.Add(item);
            }
            return list.AsQueryable();
        }
        public IQueryable<DriverViewModel> GetAllDriver_Approved(string[] ListCountryID, string[] ListAgentID, string[] ListCityID, int? DriverID, DateTime? FromDate, DateTime? ToDate, string accLanguage, string DriverImageView)
        {
            var list = new List<DriverViewModel>();
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
            var data = Uow.Drivers.GetAll(query => !query.IsDeleted
            && (query.RequestStatusId == (int)RequestStatusEnum.Approved)
                && (ListCountryID != null ? ListCountryID.Any(y => y == query.City.Region.CountryID.ToString()) : true)
             && (ListCityID != null ? ListCityID.Any(y => y == query.CityID.ToString()) : true)
            && (DriverID != null ? query.DriversID == DriverID : true)
            && (FromDate != null ? query.CreateDate.Date >= FromDate.Value.Date : true)
           && (ToDate != null ? query.CreateDate.Date <= ToDate.Value.Date : true)
            ).OrderByDescending(a => a.CreateDate).Select(m => new DriverViewModel()
            {
                DriversID = m.DriversID,
                DriverGuid = m.DriverGuid,
                DriverName = accLanguage == "ar" ? m.NameAr : m.NameEn,
                CityName = accLanguage == "ar" ? m.City.NameAR : m.City.NameEN,
                CountryName = accLanguage == "ar" ? m.City.Region.Country.NameAR : m.City.Region.Country.NameEN,
                Address = m.Address,
                CityID = m.CityID,
                Email = m.Email ?? "---",
                Gender = m.Gender,
                BirthDateType = m.BirthDateType,
                HijiriInsuranceEndDate = m.HijiriInsuranceEndDate,
                HijriBirthDate = m.HijriBirthDate,
                HijriIDDate = m.HijriIDDate,
                IDNo = m.IDNo,
                InsuranceEndDateType = m.InsuranceEndDateType,
                InsuranceNumber = m.InsuranceNumber,
                PCOEndDateType = m.PCOEndDateType,
                PCOHijriEndDate = m.PCOHijriEndDate,
                PCONumber = m.PCONumber,
                PhoneNumber = m.PhoneNumber,
                PrivateHijriLicenseEndDate = m.PrivateHijriLicenseEndDate,
                PrivateLicenseNumber = m.PrivateLicenseNumber,
                PrivateLicenseTypeEndDate = m.PrivateLicenseTypeEndDate,
                GenderName = accLanguage == "ar" ? (m.Gender == (byte)Gender.Male ? "Male" : m.Gender == (byte)Gender.Female ? "Female"
                : m.Gender == (byte)Gender.Prefer_not_to_specify ? "Not to specify" : string.Empty) :
                (m.Gender == (byte)Gender.Male ? "ذكر" : m.Gender == (byte)Gender.Female ? "أنثي" : m.Gender == (byte)Gender.Prefer_not_to_specify ?
                "غير محدد" : string.Empty),
                BirthDate = m.BirthDate != null ? m.BirthDate.Value.ToString("yyyy-MM-dd") : string.Empty,
                InsuranceEndDate = m.InsuranceEndDate != null ? m.InsuranceEndDate.Value.ToString("yyyy-MM-dd") : string.Empty,
                IDPicture = !string.IsNullOrEmpty(m.IDPicture) ? (DriverImageView + m.IDPicture) : string.Empty,
                LicensePicture = !string.IsNullOrEmpty(m.LicensePicture) ? (DriverImageView + m.LicensePicture) : string.Empty,
                PCOEndDate = m.PCOEndDate != null ? m.PCOEndDate.Value.ToString("yyyy-MM-dd") : string.Empty,
                PersonalPicture = !string.IsNullOrEmpty(m.PersonalPicture) ? (DriverImageView + m.PersonalPicture) : string.Empty,
                PrivateLicenseEndDate = m.PrivateLicenseEndDate != null ? m.PrivateLicenseEndDate.Value.ToString("yyyy-MM-dd") : string.Empty,
                IDDate = m.IDDate != null ? m.IDDate.Value.ToString("yyyy-MM-dd") : string.Empty,
                CreateDateString = m.CreateDate.ToString("yyyy-MM-dd"),
                RequestStatusName = accLanguage == "ar" ? (m.RequestStatusId == (int)RequestStatusEnum.New ? "جديد" : m.RequestStatusId == (int)RequestStatusEnum.Hanging ? "معلق"
                : m.RequestStatusId == (int)RequestStatusEnum.Rejected ? "مرفوض" : m.RequestStatusId == (int)RequestStatusEnum.Activated ? "تم التفعيل" : "تحت التدقيق") :
                (m.RequestStatusId == (int)RequestStatusEnum.New ? "New" : m.RequestStatusId == (int)RequestStatusEnum.Hanging ? "Hanging"
                : m.RequestStatusId == (int)RequestStatusEnum.Rejected ? "Rejected" : m.RequestStatusId == (int)RequestStatusEnum.Activated ? "Activated" : "Under Scrutiny"),
                RegionName = accLanguage == "ar" ? m.City.Region.Country.NameAR : m.City.Region.Country.NameEN,
                NationalityName = m.Nationality != null ? accLanguage == "ar" ? m.Nationality.NameAR : m.Nationality.NameEN : " --- ",
                RequestStatusId = m.RequestStatusId
            });
            foreach (var item in data)
            {
                item.Balance = GetLastBlance(item.DriversID);
                item.Rate = GetDriverEvaluate(item.DriversID);
                list.Add(item);
            }
            return list.AsQueryable();
        }
        public IQueryable<DriverViewModel> GetAllDriver_Rejected(string[] ListCountryID, string[] ListAgentID, string[] ListCityID, int? DriverID, DateTime? FromDate, DateTime? ToDate, string accLanguage, string DriverImageView)
        {
            var list = new List<DriverViewModel>();
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
            var data = Uow.Drivers.GetAll(query => !query.IsDeleted
            && (query.RequestStatusId == (int)RequestStatusEnum.Rejected)
                && (ListCountryID != null ? ListCountryID.Any(y => y == query.City.Region.CountryID.ToString()) : true)
             && (ListCityID != null ? ListCityID.Any(y => y == query.CityID.ToString()) : true)
            && (DriverID != null ? query.DriversID == DriverID : true)
            && (FromDate != null ? query.CreateDate.Date >= FromDate.Value.Date : true)
           && (ToDate != null ? query.CreateDate.Date <= ToDate.Value.Date : true)
            ).OrderByDescending(a => a.CreateDate).Select(m => new DriverViewModel()
            {
                DriversID = m.DriversID,
                DriverGuid = m.DriverGuid,
                DriverName = accLanguage == "ar" ? m.NameAr : m.NameEn,
                CityName = accLanguage == "ar" ? m.City.NameAR : m.City.NameEN,
                CountryName = accLanguage == "ar" ? m.City.Region.Country.NameAR : m.City.Region.Country.NameEN,
                Address = m.Address,
                CityID = m.CityID,
                Email = m.Email ?? "---",
                Gender = m.Gender,
                BirthDateType = m.BirthDateType,
                HijiriInsuranceEndDate = m.HijiriInsuranceEndDate,
                HijriBirthDate = m.HijriBirthDate,
                HijriIDDate = m.HijriIDDate,
                IDNo = m.IDNo,
                InsuranceEndDateType = m.InsuranceEndDateType,
                InsuranceNumber = m.InsuranceNumber,
                PCOEndDateType = m.PCOEndDateType,
                PCOHijriEndDate = m.PCOHijriEndDate,
                PCONumber = m.PCONumber,
                PhoneNumber = m.PhoneNumber,
                PrivateHijriLicenseEndDate = m.PrivateHijriLicenseEndDate,
                PrivateLicenseNumber = m.PrivateLicenseNumber,
                PrivateLicenseTypeEndDate = m.PrivateLicenseTypeEndDate,
                GenderName = accLanguage == "ar" ? (m.Gender == (byte)Gender.Male ? "Male" : m.Gender == (byte)Gender.Female ? "Female"
                : m.Gender == (byte)Gender.Prefer_not_to_specify ? "Not to specify" : string.Empty) :
                (m.Gender == (byte)Gender.Male ? "ذكر" : m.Gender == (byte)Gender.Female ? "أنثي" : m.Gender == (byte)Gender.Prefer_not_to_specify ?
                "غير محدد" : string.Empty),
                BirthDate = m.BirthDate != null ? m.BirthDate.Value.ToString("yyyy-MM-dd") : string.Empty,
                InsuranceEndDate = m.InsuranceEndDate != null ? m.InsuranceEndDate.Value.ToString("yyyy-MM-dd") : string.Empty,
                IDPicture = !string.IsNullOrEmpty(m.IDPicture) ? (DriverImageView + m.IDPicture) : string.Empty,
                LicensePicture = !string.IsNullOrEmpty(m.LicensePicture) ? (DriverImageView + m.LicensePicture) : string.Empty,
                PCOEndDate = m.PCOEndDate != null ? m.PCOEndDate.Value.ToString("yyyy-MM-dd") : string.Empty,
                PersonalPicture = !string.IsNullOrEmpty(m.PersonalPicture) ? (DriverImageView + m.PersonalPicture) : string.Empty,
                PrivateLicenseEndDate = m.PrivateLicenseEndDate != null ? m.PrivateLicenseEndDate.Value.ToString("yyyy-MM-dd") : string.Empty,
                IDDate = m.IDDate != null ? m.IDDate.Value.ToString("yyyy-MM-dd") : string.Empty,
                CreateDateString = m.CreateDate.ToString("yyyy-MM-dd"),
                RequestStatusName = accLanguage == "ar" ? (m.RequestStatusId == (int)RequestStatusEnum.New ? "جديد" : m.RequestStatusId == (int)RequestStatusEnum.Hanging ? "معلق"
                : m.RequestStatusId == (int)RequestStatusEnum.Rejected ? "مرفوض" : m.RequestStatusId == (int)RequestStatusEnum.Activated ? "تم التفعيل" : "تحت التدقيق") :
                (m.RequestStatusId == (int)RequestStatusEnum.New ? "New" : m.RequestStatusId == (int)RequestStatusEnum.Hanging ? "Hanging"
                : m.RequestStatusId == (int)RequestStatusEnum.Rejected ? "Rejected" : m.RequestStatusId == (int)RequestStatusEnum.Activated ? "Activated" : "Under Scrutiny"),
                RegionName = accLanguage == "ar" ? m.City.Region.Country.NameAR : m.City.Region.Country.NameEN,
                NationalityName = m.Nationality != null ? accLanguage == "ar" ? m.Nationality.NameAR : m.Nationality.NameEN : " --- ",
                RequestStatusId = m.RequestStatusId
            });
            foreach (var item in data)
            {
                item.Balance = GetLastBlance(item.DriversID);
                item.Rate = GetDriverEvaluate(item.DriversID);
                list.Add(item);
            }
            return list.AsQueryable();
        }
        public AssignDriverVewModel GetAssignByID(Guid id, string lang, string DriverImageView)
        {

            var assignDriverVewModel =
                 Uow.Drivers.GetAll(p => p.DriverGuid == id, "City.Region.Country,Nationality").ToList()
                 .Select(p => new AssignDriverVewModel()
                 {
                     Address = p.Address,
                     BirthDate = p.BirthDate?.ToString("yyyy-MM-dd"),
                     BirthDateType = p.BirthDateType,
                     CarPictrue = !string.IsNullOrEmpty(p.CarPictrue) ? (DriverImageView + p.CarPictrue) : string.Empty,
                     CityID = p.CityID,
                     CountryID = p.City.Region.Country.CountryID,
                     DriverHijiriInsuranceEndDate = p.HijiriInsuranceEndDate,
                     HijiriInsuranceEndDate = p.HijiriInsuranceEndDate,
                     DriverInsuranceEndDate = p.InsuranceEndDate?.ToString("yyyy-MM-dd"),
                     DriverInsuranceEndDateType = p.InsuranceEndDateType,
                     InsuranceEndDate = p.InsuranceEndDate?.ToString("yyyy-MM-dd"),
                     DriverInsuranceNumber = p.InsuranceNumber,
                     IsActive = p.IsActive,
                     IDNo = p.IDNo,
                     Email = p.Email,
                     IDPicture = !string.IsNullOrEmpty(p.IDPicture) ? (DriverImageView + p.IDPicture) : string.Empty,
                     InsuranceNumber = p.InsuranceNumber,
                     Gender = p.Gender,
                     HijriBirthDate = p.HijriBirthDate,
                     PrivateHijriLicenseEndDate = p.PrivateHijriLicenseEndDate,
                     InsuranceEndDateType = p.InsuranceEndDateType,
                     PrivateLicenseEndDate = p.PrivateLicenseEndDate?.ToString("yyyy-MM-dd"),
                     PrivateLicenseNumber = p.PrivateLicenseNumber,
                     LicensePicture = !string.IsNullOrEmpty(p.LicensePicture) ? (DriverImageView + p.LicensePicture) : string.Empty,
                     PrivateLicenseTypeEndDate = p.PrivateLicenseTypeEndDate,
                     NameAr = p.NameAr,
                     NameEn = p.NameEn,
                     PCOEndDate = p.PCOEndDate?.ToString("yyyy-MM-dd"),
                     PCOEndDateType = p.PCOEndDateType,
                     PCOHijriEndDate = p.PCOHijriEndDate,
                     PCONumber = p.PCONumber,
                     PersonalPicture = !string.IsNullOrEmpty(p.PersonalPicture) ? (DriverImageView + p.PersonalPicture) : string.Empty,
                     PhoneNumber = p.PhoneNumber,
                     CarNumber = p.CarNumber,
                     CountryName = lang == "ar" ? p.City?.Region.Country.NameAR : p.City.Region.Country?.NameEN,
                     CityName = lang == "ar" ? p.City?.NameAR : p.City?.NameEN,
                     DriverGuid = p.DriverGuid,
                     CarLicensePicture = !string.IsNullOrEmpty(p.LicensePicture) ? (DriverImageView + p.LicensePicture) : string.Empty,
                     BankAccountPicture = !string.IsNullOrEmpty(p.BankAccountPicture) ? (DriverImageView + p.BankAccountPicture) : string.Empty,
                     NationalityID = p.NationalityID != null ? (int)p.NationalityID : 0,
                     PhoneNumberStc = p.PhoneNumberStc,
                     IBANNumber = p.IBANNumber,
                     PhoneType = p.PhoneType,
                     CarSerialNumber = p.CarSerialNumber,
                     FileNumber = p.FileNumber,
                     NickName = p.NickName,
                     VerifyStc = p.VerifyStc,
                     NationalityName = lang == "ar" ? p.Nationality?.NameAR : p.Nationality?.NameEN,
                     DriverID = p.DriversID,
                     RegionCityID = p.RegionCityID,
                     RegionCityName = lang == "ar" ? p.RegionCity?.NameAR : p.RegionCity?.NameEN,

                 }).FirstOrDefault();
            return assignDriverVewModel;
        }
        public Drivers GetDriverTaxiByIdAndActive(int DriversID, string Include)
        {
            return Uow.Drivers.GetAll(x => !x.IsDeleted && x.DriversID == DriversID, Include).FirstOrDefault();
        }
        #endregion
    }
}
