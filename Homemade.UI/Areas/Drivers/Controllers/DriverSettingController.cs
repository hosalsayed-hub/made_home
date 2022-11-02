using Homemade.BLL;
using Homemade.BLL.Driver;
using Homemade.BLL.General;
using Homemade.BLL.SMS;
using Homemade.BLL.ViewModel.Driver;
using Homemade.Model;
using Homemade.UI.Attributes;
using Homemade.UI.InfraStructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Homemade.UI.Areas.Drivers.Controllers
{
    public class DriverSettingController : Controller
    {
        #region Declerations
        private readonly ResultMessage result;
         private readonly BlDriver blDriver;
         private readonly BLGeneral _bLGeneral;
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly OTPService _OTPService;

        #endregion
        #region Constructor
        public DriverSettingController(BlDriver blDriver, ResultMessage result, IConfiguration configuration, BLGeneral bLGeneral, UserManager<User> userManager, OTPService oTPService)
        {
            this.blDriver = blDriver;
            
            this.result = result;
          
            _configuration = configuration;
            _bLGeneral = bLGeneral;
            this._userManager = userManager;
            _OTPService = oTPService;

        }
        #endregion
        #region Views
        [CustomeAuthoriz((int)ControllerEnum.Driver_New_Requests, (int)ActionEnum.View)]
        public IActionResult Index()
        {
            return View();
        }
        [CustomeAuthoriz((int)ControllerEnum.Driver_New_Requests, (int)ActionEnum.View)]
        public IActionResult NewRequests()
        {
            return View();
        }
        [CustomeAuthoriz((int)ControllerEnum.Driver_Under_Requests, (int)ActionEnum.View)]
        public IActionResult UnderScrutiny()
        {
            return View();
        }
        [CustomeAuthoriz((int)ControllerEnum.Driver_Rejected_Requests, (int)ActionEnum.View)]
        public IActionResult RejectedRequests()
        {
            return View();
        }
        [CustomeAuthoriz((int)ControllerEnum.Driver_Waiting_Activation, (int)ActionEnum.View)]
        public IActionResult WaitingActivation()
        {
            return View();
        }
        [CustomeAuthoriz((int)ControllerEnum.AssignDriver, (int)ActionEnum.Insert)]
        public IActionResult Create()
        {
            AssignDriverVewModel model = new AssignDriverVewModel();
            return View(model);
        }
        [CustomeAuthoriz((int)ControllerEnum.AssignDriver, (int)ActionEnum.Update)]
        public IActionResult Edit(Guid? id)
        {
            if (id.HasValue)
            {
                var tbl = blDriver.GetAssignByID(id.Value, InfraStructure.Utility.CurrentLanguageCode, _configuration["DriverImageView"]);
                return View(tbl);
            }
            return Redirect("/Drivers/DriverSetting/Index");
        }
        [CustomeAuthoriz((int)ControllerEnum.AssignDriver, (int)ActionEnum.View)]
        public IActionResult Details(Guid? id)
        {
            if (id.HasValue)
            {
                var tbl = blDriver.GetAssignByID(id.Value, InfraStructure.Utility.CurrentLanguageCode, _configuration["DriverImageView"]);
                return View(tbl);
            }
            return Redirect("/Drivers/DriverSetting/Index");
        }
        #endregion
        #region Action
        [HttpPost]
        public async Task<JsonResult> Insert(AssignDriverVewModel assignDriverVewModel)
        {
            #region validations
            assignDriverVewModel.PhoneNumberStc = MobileFormat(assignDriverVewModel.PhoneNumberStc);

            if (string.IsNullOrWhiteSpace(assignDriverVewModel.CarNumber))
            {
                result.Message = Resources.Homemade.TaxiNumberRequired;
                result.ResultType = ResultMessageType.error.ToString();
                return Json(result);

            }
            if (string.IsNullOrWhiteSpace(assignDriverVewModel.NameAr))
            {
                result.Message = Resources.Homemade.DriverNameArRequired;
                result.ResultType = ResultMessageType.error.ToString();
                return Json(result);

            }
            if (string.IsNullOrWhiteSpace(assignDriverVewModel.IDNo))
            {
                result.Message = Resources.Homemade.IdNoRequired;
                result.ResultType = ResultMessageType.error.ToString();
                return Json(result);

            }
            if (string.IsNullOrWhiteSpace(assignDriverVewModel.NationalityID.ToString()) || assignDriverVewModel.NationalityID == 0)
            {
                result.Message = Resources.Homemade.Nationality;
                result.ResultType = ResultMessageType.error.ToString();
                return Json(result);
            }
            if (string.IsNullOrWhiteSpace(assignDriverVewModel.PhoneNumberStc))
            {
                result.Message = Resources.Homemade.MobileNumberRequired;
                result.ResultType = ResultMessageType.error.ToString();
                return Json(result);

            }

            if (blDriver.DriverNameIsExists(assignDriverVewModel.DriverGuid, assignDriverVewModel.NameAr))
            {
                result.Message = Resources.Homemade.DriverNameArIsExists;
                result.ResultType = ResultMessageType.error.ToString();
                return Json(result);

            }
            if (blDriver.IDNoIsExists(assignDriverVewModel.DriverGuid, assignDriverVewModel.IDNo))
            {
                result.Message = Resources.Homemade.IDNoIsExists;
                result.ResultType = ResultMessageType.error.ToString();
                return Json(result);

            }
            if (blDriver.CarNumberIsExists(assignDriverVewModel.DriverGuid, assignDriverVewModel.CarNumber))
            {
                result.Message = Resources.Homemade.TaxiNumberIsExists;
                result.ResultType = ResultMessageType.error.ToString();
                return Json(result);
            }

            #endregion

            #region Image
            if (assignDriverVewModel.fupBankAccountPicture != null)
            {
                if (assignDriverVewModel.fupBankAccountPicture.Length > 0)
                {
                    assignDriverVewModel.BankAccountPicture = string.Concat(Guid.NewGuid().ToString()) + (ContentDispositionHeaderValue.Parse(assignDriverVewModel.fupBankAccountPicture.ContentDisposition).FileName.Trim('"').Substring(ContentDispositionHeaderValue.Parse(assignDriverVewModel.fupBankAccountPicture.ContentDisposition).FileName.Trim('"').IndexOf(".")));
                    var FilePath = Path.Combine(_configuration["DriverImageSave"], assignDriverVewModel.BankAccountPicture);

                    _bLGeneral.SaveIFromFile(new BLGeneral.SaveImageFormFileModel
                    {
                        file = assignDriverVewModel.fupBankAccountPicture,
                        filename = assignDriverVewModel.BankAccountPicture,
                        key = "",
                        mainPath = FilePath
                    });
                }
            }
            if (assignDriverVewModel.fupCarLicensePicture != null)
            {
                if (assignDriverVewModel.fupCarLicensePicture.Length > 0)
                {
                    assignDriverVewModel.CarLicensePicture = string.Concat(Guid.NewGuid().ToString()) + (ContentDispositionHeaderValue.Parse(assignDriverVewModel.fupCarLicensePicture.ContentDisposition).FileName.Trim('"').Substring(ContentDispositionHeaderValue.Parse(assignDriverVewModel.fupCarLicensePicture.ContentDisposition).FileName.Trim('"').IndexOf(".")));
                    var FilePath = Path.Combine(_configuration["DriverImageSave"], assignDriverVewModel.CarLicensePicture);

                    _bLGeneral.SaveIFromFile(new BLGeneral.SaveImageFormFileModel
                    {
                        file = assignDriverVewModel.fupCarLicensePicture,
                        filename = assignDriverVewModel.CarLicensePicture,
                        key = "",
                        mainPath = FilePath
                    });
                }
            }
            if (assignDriverVewModel.fupCarPictrue != null)
            {
                if (assignDriverVewModel.fupCarPictrue.Length > 0)
                {
                    assignDriverVewModel.CarPictrue = string.Concat(Guid.NewGuid().ToString()) + (ContentDispositionHeaderValue.Parse(assignDriverVewModel.fupCarPictrue.ContentDisposition).FileName.Trim('"').Substring(ContentDispositionHeaderValue.Parse(assignDriverVewModel.fupCarPictrue.ContentDisposition).FileName.Trim('"').IndexOf(".")));
                    var FilePath = Path.Combine(_configuration["DriverImageSave"], assignDriverVewModel.CarPictrue);

                    _bLGeneral.SaveIFromFile(new BLGeneral.SaveImageFormFileModel
                    {
                        file = assignDriverVewModel.fupCarPictrue,
                        filename = assignDriverVewModel.CarPictrue,
                        key = "",
                        mainPath = FilePath
                    });
                }
            }
            if (assignDriverVewModel.fupPersonalPicture != null)
            {
                if (assignDriverVewModel.fupPersonalPicture.Length > 0)
                {
                    assignDriverVewModel.PersonalPicture = string.Concat(Guid.NewGuid().ToString()) + (ContentDispositionHeaderValue.Parse(assignDriverVewModel.fupPersonalPicture.ContentDisposition).FileName.Trim('"').Substring(ContentDispositionHeaderValue.Parse(assignDriverVewModel.fupPersonalPicture.ContentDisposition).FileName.Trim('"').IndexOf(".")));
                    var FilePath = Path.Combine(_configuration["DriverImageSave"], assignDriverVewModel.PersonalPicture);

                    _bLGeneral.SaveIFromFile(new BLGeneral.SaveImageFormFileModel
                    {
                        file = assignDriverVewModel.fupPersonalPicture,
                        filename = assignDriverVewModel.PersonalPicture,
                        key = "",
                        mainPath = FilePath
                    });
                }
            }
            if (assignDriverVewModel.fupLicensePicture != null)
            {
                if (assignDriverVewModel.fupLicensePicture.Length > 0)
                {
                    assignDriverVewModel.LicensePicture = string.Concat(Guid.NewGuid().ToString()) + (ContentDispositionHeaderValue.Parse(assignDriverVewModel.fupLicensePicture.ContentDisposition).FileName.Trim('"').Substring(ContentDispositionHeaderValue.Parse(assignDriverVewModel.fupLicensePicture.ContentDisposition).FileName.Trim('"').IndexOf(".")));
                    var FilePath = Path.Combine(_configuration["DriverImageSave"], assignDriverVewModel.LicensePicture);

                    _bLGeneral.SaveIFromFile(new BLGeneral.SaveImageFormFileModel
                    {
                        file = assignDriverVewModel.fupLicensePicture,
                        filename = assignDriverVewModel.LicensePicture,
                        key = "",
                        mainPath = FilePath
                    });
                }
            }
            if (assignDriverVewModel.fupIDPicture != null)
            {
                if (assignDriverVewModel.fupIDPicture.Length > 0)
                {
                    assignDriverVewModel.IDPicture = string.Concat(Guid.NewGuid().ToString()) + (ContentDispositionHeaderValue.Parse(assignDriverVewModel.fupIDPicture.ContentDisposition).FileName.Trim('"').Substring(ContentDispositionHeaderValue.Parse(assignDriverVewModel.fupIDPicture.ContentDisposition).FileName.Trim('"').IndexOf(".")));
                    var FilePath = Path.Combine(_configuration["DriverImageSave"], assignDriverVewModel.IDPicture);

                    _bLGeneral.SaveIFromFile(new BLGeneral.SaveImageFormFileModel
                    {
                        file = assignDriverVewModel.fupIDPicture,
                        filename = assignDriverVewModel.IDPicture,
                        key = "",
                        mainPath = FilePath
                    });
                }
            }
            #endregion
            #endregion
            #region SaveData

            try
            {
                assignDriverVewModel.PhoneNumberStc = MobileFormat(assignDriverVewModel.PhoneNumberStc);
                User user = new User
                {
                    UserName = _bLGeneral.GenerateToken(200),
                    Email = _bLGeneral.RandomString(10) + "@made-home.com",
                    PhoneNumber = _bLGeneral.RandomNumber(10),
                    UserType = (int)UserTypeEnum.Driver
                };
                var Userresult = await _userManager.CreateAsync(user, _bLGeneral.GeneratePassword(8));
                if (Userresult.Succeeded)
                {
                    var IsSuccess = blDriver.InsertDriver(assignDriverVewModel, user.Id);
                    if (IsSuccess)
                    {
                        result.Message = Resources.Homemade.SuccessSaveMessage;
                        result.ResultType = ResultMessageType.success.ToString();
                    }
                    else
                    {
                        await _userManager.DeleteAsync(user);
                        result.Message = Resources.Homemade.FailSaveMessage;
                        result.ResultType = ResultMessageType.error.ToString();
                    }
                }
                else
                {
                    result.Message = Resources.Homemade.FailSaveMessage;
                    result.ResultType = ResultMessageType.error.ToString();
                }
            }
            catch (Exception e)
            {
                result.Message = Resources.Homemade.FailSaveMessage;
                result.ResultType = ResultMessageType.error.ToString();
            }
            return Json(result);
        }
        [HttpPost]
        public async Task<JsonResult> Edit(AssignDriverVewModel assignDriverVewModel)
        {
            #region validations
            assignDriverVewModel.PhoneNumberStc = MobileFormat(assignDriverVewModel.PhoneNumberStc);

            if (string.IsNullOrWhiteSpace(assignDriverVewModel.CarNumber))
            {
                result.Message = Resources.Homemade.TaxiNumberRequired;
                result.ResultType = ResultMessageType.error.ToString();
                return Json(result);

            }
            if (string.IsNullOrWhiteSpace(assignDriverVewModel.NameAr))
            {
                result.Message = Resources.Homemade.DriverNameArRequired;
                result.ResultType = ResultMessageType.error.ToString();
                return Json(result);

            }
            if (string.IsNullOrWhiteSpace(assignDriverVewModel.IDNo))
            {
                result.Message = Resources.Homemade.IdNoRequired;
                result.ResultType = ResultMessageType.error.ToString();
                return Json(result);

            }
            if (string.IsNullOrWhiteSpace(assignDriverVewModel.NationalityID.ToString()) || assignDriverVewModel.NationalityID == 0)
            {
                result.Message = Resources.Homemade.Nationality;
                result.ResultType = ResultMessageType.error.ToString();
                return Json(result);
            }
            if (string.IsNullOrWhiteSpace(assignDriverVewModel.PhoneNumberStc))
            {
                result.Message = Resources.Homemade.MobileNumberRequired;
                result.ResultType = ResultMessageType.error.ToString();
                return Json(result);

            }

            if (blDriver.DriverNameIsExists(assignDriverVewModel.DriverGuid, assignDriverVewModel.NameAr))
            {
                result.Message = Resources.Homemade.DriverNameArIsExists;
                result.ResultType = ResultMessageType.error.ToString();
                return Json(result);

            }
            if (blDriver.IDNoIsExists(assignDriverVewModel.DriverGuid, assignDriverVewModel.IDNo))
            {
                result.Message = Resources.Homemade.IDNoIsExists;
                result.ResultType = ResultMessageType.error.ToString();
                return Json(result);

            }
            if (blDriver.CarNumberIsExists(assignDriverVewModel.DriverGuid, assignDriverVewModel.CarNumber))
            {
                result.Message = Resources.Homemade.TaxiNumberIsExists;
                result.ResultType = ResultMessageType.error.ToString();
                return Json(result);
            }
            #endregion
            #region Image
            if (assignDriverVewModel.fupBankAccountPicture != null)
            {
                if (assignDriverVewModel.fupBankAccountPicture.Length > 0)
                {
                    assignDriverVewModel.BankAccountPicture = string.Concat(Guid.NewGuid().ToString()) + (ContentDispositionHeaderValue.Parse(assignDriverVewModel.fupBankAccountPicture.ContentDisposition).FileName.Trim('"').Substring(ContentDispositionHeaderValue.Parse(assignDriverVewModel.fupBankAccountPicture.ContentDisposition).FileName.Trim('"').IndexOf(".")));
                    var FilePath = Path.Combine(_configuration["DriverImageSave"], assignDriverVewModel.BankAccountPicture);

                    _bLGeneral.SaveIFromFile(new BLGeneral.SaveImageFormFileModel
                    {
                        file = assignDriverVewModel.fupBankAccountPicture,
                        filename = assignDriverVewModel.BankAccountPicture,
                        key = "",
                        mainPath = FilePath
                    });
                }
            }
            if (assignDriverVewModel.fupCarLicensePicture != null)
            {
                if (assignDriverVewModel.fupCarLicensePicture.Length > 0)
                {
                    assignDriverVewModel.CarLicensePicture = string.Concat(Guid.NewGuid().ToString()) + (ContentDispositionHeaderValue.Parse(assignDriverVewModel.fupCarLicensePicture.ContentDisposition).FileName.Trim('"').Substring(ContentDispositionHeaderValue.Parse(assignDriverVewModel.fupCarLicensePicture.ContentDisposition).FileName.Trim('"').IndexOf(".")));
                    var FilePath = Path.Combine(_configuration["DriverImageSave"], assignDriverVewModel.CarLicensePicture);

                    _bLGeneral.SaveIFromFile(new BLGeneral.SaveImageFormFileModel
                    {
                        file = assignDriverVewModel.fupCarLicensePicture,
                        filename = assignDriverVewModel.CarLicensePicture,
                        key = "",
                        mainPath = FilePath
                    });
                }
            }
            if (assignDriverVewModel.fupCarPictrue != null)
            {
                if (assignDriverVewModel.fupCarPictrue.Length > 0)
                {
                    assignDriverVewModel.CarPictrue = string.Concat(Guid.NewGuid().ToString()) + (ContentDispositionHeaderValue.Parse(assignDriverVewModel.fupCarPictrue.ContentDisposition).FileName.Trim('"').Substring(ContentDispositionHeaderValue.Parse(assignDriverVewModel.fupCarPictrue.ContentDisposition).FileName.Trim('"').IndexOf(".")));
                    var FilePath = Path.Combine(_configuration["DriverImageSave"], assignDriverVewModel.CarPictrue);

                    _bLGeneral.SaveIFromFile(new BLGeneral.SaveImageFormFileModel
                    {
                        file = assignDriverVewModel.fupCarPictrue,
                        filename = assignDriverVewModel.CarPictrue,
                        key = "",
                        mainPath = FilePath
                    });
                }
            }
            if (assignDriverVewModel.fupPersonalPicture != null)
            {
                if (assignDriverVewModel.fupPersonalPicture.Length > 0)
                {
                    assignDriverVewModel.PersonalPicture = string.Concat(Guid.NewGuid().ToString()) + (ContentDispositionHeaderValue.Parse(assignDriverVewModel.fupPersonalPicture.ContentDisposition).FileName.Trim('"').Substring(ContentDispositionHeaderValue.Parse(assignDriverVewModel.fupPersonalPicture.ContentDisposition).FileName.Trim('"').IndexOf(".")));
                    var FilePath = Path.Combine(_configuration["DriverImageSave"], assignDriverVewModel.PersonalPicture);

                    _bLGeneral.SaveIFromFile(new BLGeneral.SaveImageFormFileModel
                    {
                        file = assignDriverVewModel.fupPersonalPicture,
                        filename = assignDriverVewModel.PersonalPicture,
                        key = "",
                        mainPath = FilePath
                    });
                }
            }
            if (assignDriverVewModel.fupLicensePicture != null)
            {
                if (assignDriverVewModel.fupLicensePicture.Length > 0)
                {
                    assignDriverVewModel.LicensePicture = string.Concat(Guid.NewGuid().ToString()) + (ContentDispositionHeaderValue.Parse(assignDriverVewModel.fupLicensePicture.ContentDisposition).FileName.Trim('"').Substring(ContentDispositionHeaderValue.Parse(assignDriverVewModel.fupLicensePicture.ContentDisposition).FileName.Trim('"').IndexOf(".")));
                    var FilePath = Path.Combine(_configuration["DriverImageSave"], assignDriverVewModel.LicensePicture);

                    _bLGeneral.SaveIFromFile(new BLGeneral.SaveImageFormFileModel
                    {
                        file = assignDriverVewModel.fupLicensePicture,
                        filename = assignDriverVewModel.LicensePicture,
                        key = "",
                        mainPath = FilePath
                    });
                }
            }
            if (assignDriverVewModel.fupIDPicture != null)
            {
                if (assignDriverVewModel.fupIDPicture.Length > 0)
                {
                    assignDriverVewModel.IDPicture = string.Concat(Guid.NewGuid().ToString()) + (ContentDispositionHeaderValue.Parse(assignDriverVewModel.fupIDPicture.ContentDisposition).FileName.Trim('"').Substring(ContentDispositionHeaderValue.Parse(assignDriverVewModel.fupIDPicture.ContentDisposition).FileName.Trim('"').IndexOf(".")));
                    var FilePath = Path.Combine(_configuration["DriverImageSave"], assignDriverVewModel.IDPicture);

                    _bLGeneral.SaveIFromFile(new BLGeneral.SaveImageFormFileModel
                    {
                        file = assignDriverVewModel.fupIDPicture,
                        filename = assignDriverVewModel.IDPicture,
                        key = "",
                        mainPath = FilePath
                    });
                }
            }
            #endregion
            #endregion
            #region SaveData

            try
            {
               
                var IsSuccess = blDriver.UpdateDriver(assignDriverVewModel, User.GetUserIdInt());
                if (IsSuccess)
                {
                    result.Message = Resources.Homemade.SuccessSaveMessage;
                    result.ResultType = ResultMessageType.success.ToString();
                }
                else
                {
                    result.Message = Resources.Homemade.FailSaveMessage;
                    result.ResultType = ResultMessageType.error.ToString();
                }
            }
            catch (Exception e)
            {
                result.Message = Resources.Homemade.FailSaveMessage;
                result.ResultType = ResultMessageType.error.ToString();
            }
            return Json(result);
        }
        #endregion
        #region MyRegion
        [HttpPost]
        //عرض البيانات من جدول الاساين
        public JsonResult LoadTable(string listCityID, string mobileno, int? driverID, DateTime? fromDate, DateTime? toDate)
        {
            listCityID = listCityID?.Replace("nullNaN", "null");
            listCityID = listCityID?.Replace("NaN", "null");

            #region DataTableParameters
            int displayLength = int.Parse(Request.Form["iDisplayLength"]);
            int displayStart = int.Parse(Request.Form["iDisplayStart"]);
            var isfirst = false;
            if (displayStart == 0)
            {
                displayStart = displayLength;
                isfirst = true;
            }
            int pageno = displayStart / displayLength;
            if (!isfirst)
            {
                pageno = pageno + 1;
            }
            //int skip = (displayStart);
            //int take = (skip + 1) * displayLength;
            #endregion
            string[] city = null;
            if (!string.IsNullOrEmpty(listCityID) && listCityID != "null")
            {
                city = listCityID.Split(',');
            }
            var data = blDriver.GetAllDriverData(mobileno, city, driverID, fromDate, toDate, Utility.CurrentLanguageCode, _configuration["DriverImageView"]).ToList();
            if (Request.Form.ContainsKey("sSearch"))
            {
                string search = Request.Form["sSearch"];
                if (!string.IsNullOrEmpty(search))
                {
                    data = data.Where(s => s.DriverName.ToLower().Contains(search.ToLower()) || s.PhoneNumber.ToLower().Contains(search.ToLower()) || s.IDNo.ToLower().Contains(search.ToLower())).ToList();
                }
            }
            var Responce = data.AsQueryable();
            Responce = Responce.Skip(Convert.ToInt32(pageno - 1) * displayLength);
            var DataResponce = Responce.Take(((Convert.ToInt32(pageno) * displayLength)) - (Convert.ToInt32(pageno - 1) * displayLength));
            int totalCount = data.Count();
            return Json(new
            {
                iTotalRecords = totalCount,
                iTotalDisplayRecords = totalCount,
                aaData = DataResponce
            });

        }

        [HttpPost]
        //عرض البيانات من جدول الاساين
        public JsonResult LoadTableNewRequest(string listCountryID, string listAgentID, string listCityID, int? driverID,  DateTime? fromDate, DateTime? toDate)
        {
            listCityID = listCityID?.Replace("NaN", "");

            #region DataTableParameters
            int displayLength = int.Parse(Request.Form["iDisplayLength"]);
            int displayStart = int.Parse(Request.Form["iDisplayStart"]);
            var isfirst = false;
            if (displayStart == 0)
            {
                displayStart = displayLength;
                isfirst = true;
            }
            int pageno = displayStart / displayLength;
            if (!isfirst)
            {
                pageno = pageno + 1;
            }
            //int skip = (displayStart);
            //int take = (skip + 1) * displayLength;
            #endregion
            string[] country = null;
            if (!string.IsNullOrEmpty(listCountryID) && listCountryID != "null" && listCountryID != "undefined")
            {
                country = listCountryID.Split(',');
            }
            string[] agent = null;
            if (!string.IsNullOrEmpty(listAgentID) && listAgentID != "null" && listCountryID != "undefined")
            {
                agent = listAgentID.Split(',');
            }
            string[] city = null;
            if (!string.IsNullOrEmpty(listCityID) && listCityID != "null" && listCountryID != "undefined")
            {
                city = listCityID.Split(',');
            }
            var data = blDriver.GetAllDriver_New(country, agent, city, driverID,  fromDate, toDate, Utility.CurrentLanguageCode, _configuration["DriverImageView"]).ToList();
            if (Request.Form.ContainsKey("sSearch"))
            {
                string search = Request.Form["sSearch"];
                if (!string.IsNullOrEmpty(search))
                {
                    data = data.Where(x => x.DriverName.Contains(search) ||  x.PhoneNumber.Contains(search)
                    || x.IDNo.Contains(search)).ToList();
                }
            }
            var Responce = data.AsQueryable();
            Responce = Responce.Skip(Convert.ToInt32(pageno - 1) * displayLength);
            var DataResponce = Responce.Take(((Convert.ToInt32(pageno) * displayLength)) - (Convert.ToInt32(pageno - 1) * displayLength));
            int totalCount = data.Count();
            return Json(new
            {
                iTotalRecords = totalCount,
                iTotalDisplayRecords = totalCount,
                aaData = DataResponce
            });

        }

        [HttpPost]
        //عرض البيانات من جدول الاساين
        public JsonResult LoadTableUnderRequest(string listCountryID, string listAgentID, string listCityID, int? driverID,  DateTime? fromDate, DateTime? toDate)
        {
            listCityID = listCityID?.Replace("NaN", "");

            #region DataTableParameters
            int displayLength = int.Parse(Request.Form["iDisplayLength"]);
            int displayStart = int.Parse(Request.Form["iDisplayStart"]);
            var isfirst = false;
            if (displayStart == 0)
            {
                displayStart = displayLength;
                isfirst = true;
            }
            int pageno = displayStart / displayLength;
            if (!isfirst)
            {
                pageno = pageno + 1;
            }
            //int skip = (displayStart);
            //int take = (skip + 1) * displayLength;
            #endregion
            string[] country = null;
            if (!string.IsNullOrEmpty(listCountryID) && listCountryID != "null" && listCountryID != "undefined")
            {
                country = listCountryID.Split(',');
            }
            string[] agent = null;
            if (!string.IsNullOrEmpty(listAgentID) && listAgentID != "null" && listCountryID != "undefined")
            {
                agent = listAgentID.Split(',');
            }
            string[] city = null;
            if (!string.IsNullOrEmpty(listCityID) && listCityID != "null" && listCountryID != "undefined")
            {
                city = listCityID.Split(',');
            }
            var data = blDriver.GetAllDriver_Under_Scrutiny(country, agent, city, driverID, fromDate, toDate, Utility.CurrentLanguageCode, _configuration["DriverImageView"]).ToList();
            if (Request.Form.ContainsKey("sSearch"))
            {
                string search = Request.Form["sSearch"];
                if (!string.IsNullOrEmpty(search))
                {
                    data = data.Where(x => x.DriverName.Contains(search) || x.PhoneNumber.Contains(search)
                    || x.IDNo.Contains(search)).ToList();
                }
            }
            var Responce = data.AsQueryable();
            Responce = Responce.Skip(Convert.ToInt32(pageno - 1) * displayLength);
            var DataResponce = Responce.Take(((Convert.ToInt32(pageno) * displayLength)) - (Convert.ToInt32(pageno - 1) * displayLength));
            int totalCount = data.Count();
            return Json(new
            {
                iTotalRecords = totalCount,
                iTotalDisplayRecords = totalCount,
                aaData = DataResponce
            });

        }

        [HttpPost]
        //عرض البيانات من جدول الاساين
        public JsonResult LoadTableAcceptRequest(string listCountryID, string listAgentID, string listCityID, int? driverID,  DateTime? fromDate, DateTime? toDate)
        {
            listCityID = listCityID?.Replace("NaN", "");

            #region DataTableParameters
            int displayLength = int.Parse(Request.Form["iDisplayLength"]);
            int displayStart = int.Parse(Request.Form["iDisplayStart"]);
            var isfirst = false;
            if (displayStart == 0)
            {
                displayStart = displayLength;
                isfirst = true;
            }
            int pageno = displayStart / displayLength;
            if (!isfirst)
            {
                pageno = pageno + 1;
            }
            //int skip = (displayStart);
            //int take = (skip + 1) * displayLength;
            #endregion
            string[] country = null;
            if (!string.IsNullOrEmpty(listCountryID) && listCountryID != "null" && listCountryID != "undefined")
            {
                country = listCountryID.Split(',');
            }
            string[] agent = null;
            if (!string.IsNullOrEmpty(listAgentID) && listAgentID != "null" && listCountryID != "undefined")
            {
                agent = listAgentID.Split(',');
            }
            string[] city = null;
            if (!string.IsNullOrEmpty(listCityID) && listCityID != "null" && listCountryID != "undefined")
            {
                city = listCityID.Split(',');
            }
            var data = blDriver.GetAllDriver_Approved(country, agent, city, driverID, fromDate, toDate, Utility.CurrentLanguageCode, _configuration["DriverImageView"]).ToList();
            if (Request.Form.ContainsKey("sSearch"))
            {
                string search = Request.Form["sSearch"];
                if (!string.IsNullOrEmpty(search))
                {
                    data = data.Where(x => x.DriverName.Contains(search) || x.PhoneNumber.Contains(search)
                    || x.IDNo.Contains(search)).ToList();
                }
            }
            var Responce = data.AsQueryable();
            Responce = Responce.Skip(Convert.ToInt32(pageno - 1) * displayLength);
            var DataResponce = Responce.Take(((Convert.ToInt32(pageno) * displayLength)) - (Convert.ToInt32(pageno - 1) * displayLength));
            int totalCount = data.Count();
            return Json(new
            {
                iTotalRecords = totalCount,
                iTotalDisplayRecords = totalCount,
                aaData = DataResponce
            });

        }

        [HttpPost]
        //عرض البيانات من جدول الاساين
        public JsonResult LoadTableRejectRequest(string listCountryID, string listAgentID, string listCityID, int? driverID, DateTime? fromDate, DateTime? toDate)
        {
            listCityID = listCityID?.Replace("NaN", "");

            #region DataTableParameters
            int displayLength = int.Parse(Request.Form["iDisplayLength"]);
            int displayStart = int.Parse(Request.Form["iDisplayStart"]);
            var isfirst = false;
            if (displayStart == 0)
            {
                displayStart = displayLength;
                isfirst = true;
            }
            int pageno = displayStart / displayLength;
            if (!isfirst)
            {
                pageno = pageno + 1;
            }
            //int skip = (displayStart);
            //int take = (skip + 1) * displayLength;
            #endregion
            string[] country = null;
            if (!string.IsNullOrEmpty(listCountryID) && listCountryID != "null" && listCountryID != "undefined")
            {
                country = listCountryID.Split(',');
            }
            string[] agent = null;
            if (!string.IsNullOrEmpty(listAgentID) && listAgentID != "null" && listCountryID != "undefined")
            {
                agent = listAgentID.Split(',');
            }
            string[] city = null;
            if (!string.IsNullOrEmpty(listCityID) && listCityID != "null" && listCountryID != "undefined")
            {
                city = listCityID.Split(',');
            }
            var data = blDriver.GetAllDriver_Rejected(country, agent, city, driverID, fromDate, toDate, Utility.CurrentLanguageCode, _configuration["DriverImageView"]).ToList();
            if (Request.Form.ContainsKey("sSearch"))
            {
                string search = Request.Form["sSearch"];
                if (!string.IsNullOrEmpty(search))
                {
                    data = data.Where(x => x.DriverName.Contains(search) || x.PhoneNumber.Contains(search)
                    || x.IDNo.Contains(search)).ToList();
                }
            }
            var Responce = data.AsQueryable();
            Responce = Responce.Skip(Convert.ToInt32(pageno - 1) * displayLength);
            var DataResponce = Responce.Take(((Convert.ToInt32(pageno) * displayLength)) - (Convert.ToInt32(pageno - 1) * displayLength));
            int totalCount = data.Count();
            return Json(new
            {
                iTotalRecords = totalCount,
                iTotalDisplayRecords = totalCount,
                aaData = DataResponce
            });

        }

        [HttpPost]
        public JsonResult ConfirmReceived(Guid id, int status, string notes)
        {
            try
            {
                bool Success = blDriver.ConfirmReceived(id, User.GetUserIdInt(), status, notes);
                if (Success)
                {
                    result.Message = Resources.Homemade.SuccessSaveMessage;
                    result.ResultType = ResultMessageType.success.ToString();
                    return Json(result);

                }
                else
                {
                    result.Message = Resources.Homemade.FailSaveMessage;
                    result.ResultType = ResultMessageType.error.ToString();
                    return Json(result);
                }

            }
            catch (Exception e)
            {
                result.Message = Resources.Homemade.FailSaveMessage;
                result.ResultType = ResultMessageType.error.ToString();
                return Json(result);
            }
        }

        [HttpPost]
        public JsonResult ConfirmReceivedAll(string id, int status, string notes)
        {
            try
            {
                //string strPassword = Guid.NewGuid().ToString("N").Substring(0, 24);
                bool Success = blDriver.ConfirmReceivedAll(id, User.GetUserIdInt(), status, notes);
                if (Success)
                {
                    result.Message =  Resources.Homemade.SuccessSaveMessage;
                    result.ResultType = ResultMessageType.success.ToString();
                    return Json(result);

                }
                else
                {
                    result.Message = Resources.Homemade.FailSaveMessage;
                    result.ResultType = ResultMessageType.error.ToString();
                    return Json(result);
                }

            }
            catch (Exception e)
            {
                result.Message = Resources.Homemade.FailSaveMessage;
                result.ResultType = ResultMessageType.error.ToString();
                return Json(result);
            }
        }

        [HttpPost]
        //تغير حاله الاساين
        public async Task<JsonResult> ChangeStatue(int id)
        {
            try
            {
                bool Success = blDriver.ChangeStatue(id);
                if (Success)
                {
                    var driverTaxi = blDriver.GetDriverTaxiByIdAndActive(id, "User");
                    if (driverTaxi != null)
                    {
                        if (!driverTaxi.IsActive)
                        {
                            var Password = _bLGeneral.RandomNumber(6);
                            var UserData = await _userManager.FindByIdAsync(driverTaxi.UserId.ToString());
                            UserData.PasswordHash = _userManager.PasswordHasher.HashPassword(UserData, Password);
                            await _userManager.UpdateAsync(UserData);
                            var smsMessage = "";
                            var link = "https://apps.apple.com/us/app/%D8%B4%D8%BA%D9%84-%D8%A8%D9%8A%D8%AA-%D8%A7%D9%84%D9%83%D8%A7%D8%A8%D8%AA%D9%86/id1589602972";
                            var andrilink = "https://play.google.com/store/apps/details?id=app.tech_time.home_made_captain";
                            if (Utility.CurrentLanguageCode == "ar")
                            {
                                smsMessage = "تم تفعيل حسابكم بنجاح" + Environment.NewLine +
                                "اسم المستخدم : " + driverTaxi.PhoneNumber + "" + Environment.NewLine +
                                "كلمة المرور : " + Password + "" + Environment.NewLine +
                                "رابط تطبيق الأندرويد : " + andrilink + Environment.NewLine +
                                "رابط تطبيق الأيفون : " + link + "";
                            }
                            else
                            {
                                smsMessage = "Your account has been activated" + Environment.NewLine +
                                   "Username : " + driverTaxi.PhoneNumber + Environment.NewLine +
                                   "Password : " + Password + Environment.NewLine +
                                   "Android App link : " + andrilink + Environment.NewLine +
                                   "IOS App link : " + link;
                            }
                        }
                    }
                    result.Message = Resources.Homemade.SuccessSaveMessage;
                    result.ResultType = ResultMessageType.success.ToString();
                    return Json(result);

                }
                else
                {
                    result.Message = Resources.Homemade.FailSaveMessage;
                    result.ResultType = ResultMessageType.error.ToString();
                    return Json(result);
                }

            }
            catch (Exception e)
            {
                result.Message = Resources.Homemade.FailSaveMessage;
                result.ResultType = ResultMessageType.error.ToString();
                return Json(result);
            }
        }

        [HttpPost]
        //تغير حاله الاساين
        public async Task<JsonResult> ChangeStatueAll(string id)
        {
            try
            {
                bool AllSuccess = false;
                string[] listids = id.Split(",");
                foreach (var item in listids)
                {
                    if (!string.IsNullOrWhiteSpace(item))
                    {
                        bool Success = blDriver.ChangeStatue(int.Parse(item));
                    }
                }
                if (AllSuccess)
                {
                    result.Message = Resources.Homemade.SuccessSaveMessage;
                    result.ResultType = ResultMessageType.success.ToString();
                    return Json(result);
                }
                else
                {
                    result.Message = Resources.Homemade.FailSaveMessage;
                    result.ResultType = ResultMessageType.error.ToString();
                    return Json(result);
                }
            }
            catch (Exception e)
            {
                result.Message = Resources.Homemade.FailSaveMessage;
                result.ResultType = ResultMessageType.error.ToString();
                return Json(result);
            }
        }
        [HttpPost]
        public JsonResult ChangeEnable(int Id)
        {
            try
            {
                if (Id != 0)
                {
                    Model.Driver.Drivers data = blDriver.GetById(Id);
                    if (data != null)
                    {
                        blDriver.ChangeEnable(Id, User.GetUserIdInt());
                        return Json(new ResultMessage()
                        {
                            Message = Resources.Homemade.SuccessChangeMessage,
                            ResultType = ResultMessageType.success.ToString(),
                            Status = true
                        });
                    }
                }
                return Json(new ResultMessage()
                {
                    Message = Resources.Homemade.FailSaveMessage,
                    ResultType = ResultMessageType.error.ToString(),
                    Status = false
                });
            }
            catch (Exception e)
            {
                result.Message = Resources.Homemade.FailSaveMessage;
                result.ResultType = ResultMessageType.error.ToString();
                return Json(result);
            }
        }
        [CustomeAuthoriz((int)ControllerEnum.AssignDriver, (int)ActionEnum.Delete)]
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            try
            {
                if (Id != 0)
                {
                    Model.Driver.Drivers data = blDriver.GetById(Id);
                    if (data != null)
                    {
                        blDriver.Delete(Id, User.GetUserIdInt());

                        return Json(new ResultMessage()
                        {
                            Message = Resources.Homemade.SuccessDeleteMessage,
                            ResultType = ResultMessageType.success.ToString(),
                            Status = true
                        });
                    }
                }
                return Json(new ResultMessage()
                {
                    Message = Resources.Homemade.FailDeleteMessage,
                    ResultType = ResultMessageType.error.ToString(),
                    Status = false
                });
            }
            catch (Exception e)
            {
                result.Message = Resources.Homemade.FailDeleteMessage;
                result.ResultType = ResultMessageType.error.ToString();
                return Json(result);
            }
        }
        [HttpPost]
        public JsonResult DeleteAll(string Id)
        {
            try
            {
                string[] listids = Id.Split(",");
                foreach (var item in listids)
                {
                    if (!string.IsNullOrWhiteSpace(item))
                    {
                        if (int.Parse(item) != 0)
                        {
                            Model.Driver.Drivers data = blDriver.GetById(int.Parse(item));
                            if (data != null)
                            {
                                blDriver.Delete(int.Parse(item), User.GetUserIdInt());


                            }
                        }
                    }
                }
                return Json(new ResultMessage()
                {
                    Message = Resources.Homemade.SuccessDeleteMessage,
                    ResultType = ResultMessageType.success.ToString(),
                    Status = true
                });
            }
            catch (Exception e)
            {
                result.Message = Resources.Homemade.FailDeleteMessage;
                result.ResultType = ResultMessageType.error.ToString();
                return Json(result);
            }
        }

        #endregion
        #region Mobileno
        public string MobileFormat(string mobileNo)
        {
            var m_no = mobileNo.Substring(0, 2);
            if (m_no == "05")
            {
                m_no = mobileNo.Substring(1);
            }
            else
            {
                m_no = mobileNo;
            }
            return m_no;
        }
        #endregion
    }
}
