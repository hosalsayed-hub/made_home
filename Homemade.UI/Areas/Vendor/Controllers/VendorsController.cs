using Homemade.BLL;
using Homemade.BLL.General;
using Homemade.BLL.Setting;
using Homemade.BLL.Vendor;
using Homemade.BLL.ViewModel;
using Homemade.BLL.ViewModel.Vendor;
using Homemade.Model;
using Homemade.UI.Attributes;
using Homemade.UI.InfraStructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Homemade.UI.Areas.Vendor.Controllers
{
    public class VendorsController : Controller
    {
        #region Declarations
        private readonly ResultMessage result;
        private readonly BlVendor _blVendor;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly BLPermission _bLPermission;
        private readonly BLGeneral _BLGeneral;
        private readonly BlConfiguration _blConfiguration;
        private readonly BLUser _bLUser;

        public VendorsController(BLGeneral BLGeneral, ResultMessage result, BlVendor blVendor, IConfiguration configuration, UserManager<User> userManager, BLPermission bLPermission, BlConfiguration blConfiguration, BLUser bLUser)
        {
            this.result = result;
            _blConfiguration = blConfiguration;
            _blVendor = blVendor;
            _configuration = configuration;
            _userManager = userManager;
            _bLPermission = bLPermission;
            _BLGeneral = BLGeneral;
            _bLUser = bLUser;
        }
        #endregion
        #region View
        [CustomeAuthoriz((int)ControllerEnum.Vendors, (int)ActionEnum.View)]
        public IActionResult Index()
        {
            return View();
        }
        [CustomeAuthoriz((int)ControllerEnum.Vendors, (int)ActionEnum.View)]
        public IActionResult Direction()
        {
            return View();
        }
        [CustomeAuthoriz((int)ControllerEnum.Vendors, (int)ActionEnum.View)]
        public IActionResult Create()
        {
            return View();
        }
        [CustomeAuthoriz((int)ControllerEnum.Vendors, (int)ActionEnum.View)]
        public IActionResult Details(Guid? id)
        {

            if (id.HasValue)
            {
                VendorViewModel VendorViewModel = _blVendor.GetVendorViewModelByGuid(id.Value, InfraStructure.Utility.CurrentLanguageCode, _configuration["VendorImageView"]);
                if (VendorViewModel != null)
                {
                    return View(VendorViewModel);
                }
            }
            return Redirect(Url.Action("Index", "Vendors", new { Area = "Vendor" }));

        }
        [CustomeAuthoriz((int)ControllerEnum.Vendors, (int)ActionEnum.Update)]
        public IActionResult Edit(Guid? id)
        {
            if (id.HasValue)
            {
                VendorViewModel VendorViewModel = _blVendor.GetVendorViewModelByGuid(id.Value, InfraStructure.Utility.CurrentLanguageCode, _configuration["VendorImageView"]);
                if (VendorViewModel != null)
                {
                    return View(VendorViewModel);
                }
            }
            return Redirect(Url.Action("Index", "Vendors", new { Area = "Vendor" }));
        }
        [CustomeAuthoriz((int)ControllerEnum.VendorOrders, (int)ActionEnum.Update)]
        public IActionResult EditVendor()
        {
            var user = _bLUser.GetUserInfo(User.GetUserIdInt());
            int UserTypeId = user.UserType;
            int VendorsID = 0;
            if (UserTypeId == (int)UserTypeEnum.Vendor)
            {
                var vednor = _blVendor.GetVendorByUserId(User.GetUserIdInt());
                if (vednor != null)
                {
                    VendorsID = vednor.VendorsID;
                }
            }
            VendorViewModel VendorViewModel = _blVendor.GetVendorViewModelByID(VendorsID, InfraStructure.Utility.CurrentLanguageCode, _configuration["VendorImageView"]);
            if (VendorViewModel != null)
            {
                return View(VendorViewModel);
            }
            return Redirect("/AccessDenied/Unauthorized");
        }
        #endregion
        #region Action
        [CustomeAuthoriz((int)ControllerEnum.Vendors, (int)ActionEnum.Insert)]
        [HttpPost, ValidateAntiForgeryToken]
        [Obsolete]
        public async Task<JsonResult> Create(VendorViewModel viewModel)
        {
            try
            {
                #region Validation

                JsonResult ValidateData = ValidateModel(viewModel.VendorsID, viewModel.CityID, viewModel.MobileNo, viewModel.Email, viewModel.DeliveryPrice);

                if (ValidateData != null) return ValidateData;
                if (viewModel.ListDaysWork == null || !viewModel.ListDaysWork.Any())
                {
                    result.Message = Homemade.UI.Resources.Homemade.DaysWorkRequired;
                    result.ResultType = ResultMessageType.error.ToString();
                    result.Status = false;
                    return Json(result);
                }
                #endregion
                if (viewModel.VendorsID == 0)
                {
                    #region Adding
                    string Password = _BLGeneral.GeneratePassword(8);
                    User user = new User
                    {
                        UserName = _BLGeneral.GenerateToken(200),
                        Email = _BLGeneral.RandomString(10) + "@made-home.com",
                        PhoneNumber = _BLGeneral.RandomNumber(10),
                        UserType = (int)UserTypeEnum.Vendor,
                    };
                    var resultsss = await _userManager.CreateAsync(user, Password);
                    if (user.Id != 0)
                    {
                        viewModel.UserId = user.Id;
                        var Photo = "";
                        if (!string.IsNullOrEmpty(viewModel.ProfilePic))
                        {
                            string FileName = Guid.NewGuid() + ".png";
                            Photo = FileName;
                            string MainPath = _configuration["VendorImageSave"];
                            _BLGeneral.SaveImage(new BLGeneral.SaveImageModel
                            {
                                base64 = viewModel.ProfilePic,
                                key = "",
                                fileName = FileName,
                                mainPath = MainPath
                            });
                            viewModel.ProfilePic = Photo;
                        }

                        viewModel.DaysWork = String.Join(",", viewModel.ListDaysWork);
                        if (viewModel.ListDaysVac != null && viewModel.ListDaysVac.Any())
                        {
                            viewModel.DaysVac = String.Join(",", viewModel.ListDaysVac.Where(x => !viewModel.ListDaysWork.Any(y => y == x)));
                        }
                        var vendorResult = await _blVendor.Insert(viewModel);
                        if (vendorResult.BoolID)
                        {
                            result.Message = Homemade.UI.Resources.Homemade.SuccessSaveMessage;
                            result.ResultType = ResultMessageType.success.ToString();
                            result.Status = true;
                            result.ID = vendorResult.VendorsID;
                            return Json(result);
                        }
                        else
                        {
                            result.Message = Homemade.UI.Resources.Homemade.FailSaveMessage;
                            result.ResultType = ResultMessageType.error.ToString();
                            result.Status = false;
                            return Json(result);
                        }
                    }
                    else
                    {
                        await _userManager.DeleteAsync(user);
                        result.Message = Resources.Homemade.FailSaveMessage;
                        result.ResultType = ResultMessageType.error.ToString();
                        return Json(result);
                    }
                }
                else
                {
                    var VendorData = _blVendor.GetById(viewModel.VendorsID);
                    #region Update
                    if (!_blVendor.IsExistMobile(viewModel.MobileNo, viewModel.VendorsID))
                    {
                        if (!string.IsNullOrWhiteSpace(viewModel.Email))
                        {
                            if (_blVendor.IsExistEmail(viewModel.Email, viewModel.VendorsID))
                            {
                                result.Message = Homemade.UI.Resources.Homemade.EmailExists;
                                result.ResultType = ResultMessageType.error.ToString();
                                result.Status = false;
                                return Json(result);
                            }
                        }
                        if (viewModel.MobileNo != VendorData.MobileNo)
                        {
                            if (_blVendor.IsUserNameExists(viewModel.MobileNo))
                            {
                                var UserData = await _userManager.FindByIdAsync(VendorData.UserId.ToString());
                                UserData.UserName = viewModel.MobileNo;
                                var identityResult = await _userManager.UpdateAsync(UserData);
                            }
                        }
                        int UserId = User.GetUserIdInt();
                        if (_blVendor.Update(viewModel, UserId, _configuration["VendorImageSave"], _configuration["VendorImageView"]))
                        {
                            result.Message = Homemade.UI.Resources.Homemade.SuccessSaveMessage;
                            result.ResultType = ResultMessageType.success.ToString();
                            result.Status = true;
                            return Json(result);
                        }
                        else
                        {
                            var UserDataError = await _userManager.FindByIdAsync(VendorData.UserId.ToString());
                            UserDataError.UserName = VendorData.MobileNo;
                            var identityResult = await _userManager.UpdateAsync(UserDataError);
                            result.Message = Homemade.UI.Resources.Homemade.FailSaveMessage;
                            result.ResultType = ResultMessageType.error.ToString();
                            result.Status = false;
                            return Json(result);
                        }
                    }
                    else
                    {
                        result.Message = Homemade.UI.Resources.Homemade.MobileIsExists;
                        result.ResultType = ResultMessageType.error.ToString();
                        result.Status = false;
                        return Json(result);
                    }
                    #endregion
                }

                #endregion

            }
            catch (System.Exception ex)
            {
                result.ResultType = ResultMessageType.error.ToString();
                result.Message = ex.Message.ToString();
                result.Status = false;
                return Json(result);
            }
        }
        [HttpPost, ValidateAntiForgeryToken]
        [Obsolete]
        public async Task<JsonResult> Edit(VendorViewModel viewModel)
        {
            try
            {
                #region Validation
                JsonResult ValidateData = ValidateModel(viewModel.VendorsID, viewModel.CityID, viewModel.MobileNo, viewModel.Email, viewModel.DeliveryPrice);
                if (ValidateData != null) return ValidateData;
                #endregion
                var VendorData = _blVendor.GetById(viewModel.VendorsID);
                #region Update
                if (!_blVendor.IsExistMobile(viewModel.MobileNo, viewModel.VendorsID))
                {
                    if (!string.IsNullOrWhiteSpace(viewModel.Email))
                    {
                        if (_blVendor.IsExistEmail(viewModel.Email, viewModel.VendorsID))
                        {
                            result.Message = Homemade.UI.Resources.Homemade.EmailExists;
                            result.ResultType = ResultMessageType.error.ToString();
                            result.Status = false;
                            return Json(result);
                        }
                    }
                    if (viewModel.MobileNo != VendorData.MobileNo)
                    {
                        if (_blVendor.IsUserNameExists(viewModel.MobileNo))
                        {
                            var UserData = await _userManager.FindByIdAsync(VendorData.UserId.ToString());
                            UserData.UserName = viewModel.MobileNo;
                            var identityResult = await _userManager.UpdateAsync(UserData);
                        }
                    }
                    if (viewModel.ListDaysWork == null || !viewModel.ListDaysWork.Any())
                    {
                        result.Message = Homemade.UI.Resources.Homemade.DaysWorkRequired;
                        result.ResultType = ResultMessageType.error.ToString();
                        result.Status = false;
                        return Json(result);
                    }
                    viewModel.DaysWork = String.Join(",", viewModel.ListDaysWork);
                    viewModel.DaysVac = string.Empty;
                    if (viewModel.ListDaysVac != null && viewModel.ListDaysVac.Any())
                    {
                        viewModel.DaysVac = String.Join(",", viewModel.ListDaysVac.Where(x => !viewModel.ListDaysWork.Any(y => y == x)));
                    }
                    int UserId = User.GetUserIdInt();
                    if (_blVendor.Update(viewModel, UserId, _configuration["VendorImageSave"], _configuration["VendorImageView"]))
                    {
                        result.Message = Homemade.UI.Resources.Homemade.SuccessSaveMessage;
                        result.ResultType = ResultMessageType.success.ToString();
                        result.Status = true;
                        return Json(result);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(VendorData.MobileNo))
                        {
                            var UserDataError = await _userManager.FindByIdAsync(VendorData.UserId.ToString());
                            UserDataError.UserName = VendorData.MobileNo;
                            var identityResult = await _userManager.UpdateAsync(UserDataError);
                        }
                        result.Message = Homemade.UI.Resources.Homemade.FailSaveMessage;
                        result.ResultType = ResultMessageType.error.ToString();
                        result.Status = false;
                        return Json(result);
                    }
                }
                else
                {
                    result.Message = Homemade.UI.Resources.Homemade.MobileIsExists;
                    result.ResultType = ResultMessageType.error.ToString();
                    result.Status = false;
                    return Json(result);
                }
                #endregion

            }
            catch (System.Exception ex)
            {
                result.ResultType = ResultMessageType.error.ToString();
                result.Message = ex.Message.ToString();
                result.Status = false;
                return Json(result);
            }
        }
        [HttpPost]
        public async Task<JsonResult> AddStoreData(VendorStoreViewModel model)
        {
            try
            {
                #region Adding
                if (model.VendorsID != 0)
                {
                    var LogoFileName = "";
                    var BannerFileName = "";
                    var CRPicFileName = "";
                    if (model.LogoFile != null)
                    {
                        if (model.LogoFile.Length > 0)
                        {
                            LogoFileName = string.Concat(Guid.NewGuid().ToString()) + (ContentDispositionHeaderValue.Parse(model.LogoFile.ContentDisposition)
                                .FileName.Trim('"').Substring(ContentDispositionHeaderValue.Parse(model.LogoFile.ContentDisposition).FileName.Trim('"').IndexOf(".")));
                            var FilePath = Path.Combine(_configuration["VendorImageSaveFile"], LogoFileName);

                            _BLGeneral.SaveIFromFile(new BLGeneral.SaveImageFormFileModel
                            {
                                file = model.LogoFile,
                                filename = LogoFileName,
                                key = "",
                                mainPath = FilePath
                            });
                        }
                    }
                    if (model.BannerFile != null)
                    {
                        if (model.BannerFile.Length > 0)
                        {
                            BannerFileName = string.Concat(Guid.NewGuid().ToString()) +
                                (ContentDispositionHeaderValue.Parse(model.BannerFile.ContentDisposition).FileName.Trim('"')
                                .Substring(ContentDispositionHeaderValue.Parse(model.BannerFile.ContentDisposition).FileName.Trim('"').IndexOf(".")));
                            var FilePath = Path.Combine(_configuration["VendorImageSaveFile"], BannerFileName);

                            _BLGeneral.SaveIFromFile(new BLGeneral.SaveImageFormFileModel
                            {
                                file = model.BannerFile,
                                filename = BannerFileName,
                                key = "",
                                mainPath = FilePath
                            });
                        }
                    }
                    if (model.CRPicFile != null)
                    {
                        if (model.CRPicFile.Length > 0)
                        {
                            CRPicFileName = string.Concat(Guid.NewGuid().ToString()) +
                                (ContentDispositionHeaderValue.Parse(model.CRPicFile.ContentDisposition).FileName.Trim('"')
                                .Substring(ContentDispositionHeaderValue.Parse(model.CRPicFile.ContentDisposition).FileName.Trim('"')
                                .IndexOf(".")));
                            var FilePath = Path.Combine(_configuration["VendorImageSaveFile"], CRPicFileName);

                            _BLGeneral.SaveIFromFile(new BLGeneral.SaveImageFormFileModel
                            {
                                file = model.CRPicFile,
                                filename = CRPicFileName,
                                key = "",
                                mainPath = FilePath
                            });
                        }
                    }
                    if (model.DeliveryType == (int)DeliveryTypeEnum.Store)
                    {
                        if (model.DeliveryPrice != null)
                        {
                            var Config = _blConfiguration.GetConfigurations().FirstOrDefault();
                            if (decimal.Parse(model.DeliveryPrice, CultureInfo.InvariantCulture) < decimal.Parse(Config.MinDeliveryPrice, CultureInfo.InvariantCulture) || decimal.Parse(model.DeliveryPrice, CultureInfo.InvariantCulture) > decimal.Parse(Config.MaxDeliveryPrice, CultureInfo.InvariantCulture))
                            {
                                result.Message = BLL.Resources.HomemadeErrorMessages.DeliveryPriceMinMaxValidation + " " + Config.MinDeliveryPrice + " " + BLL.Resources.HomemadeErrorMessages.DeliveryPriceMinMaxValidationTo + " " + Config.MaxDeliveryPrice + " " + BLL.Resources.HomemadeErrorMessages.DeliveryPriceMinMaxValidationCurrency;
                                result.ResultType = ResultMessageType.error.ToString();
                                return Json(result);
                            }
                        }
                    }
                    if (string.IsNullOrWhiteSpace(model.StoreNameAr))
                    {
                        result.Message = "يجب ادخال اسم المتجر عربي";
                        result.ResultType = ResultMessageType.error.ToString();
                        return Json(result);
                    }
                    int UserId = User.GetUserIdInt();
                    if (_blVendor.UpdateStoreData(model, LogoFileName, BannerFileName, CRPicFileName, UserId))
                    {
                        result.Message = Homemade.UI.Resources.Homemade.SuccessSaveMessage;
                        result.ResultType = ResultMessageType.success.ToString();
                        result.Status = true;
                        return Json(result);
                    }
                    else
                    {
                        result.Message = Homemade.UI.Resources.Homemade.FailSaveMessage;
                        result.ResultType = ResultMessageType.error.ToString();
                        result.Status = false;
                        return Json(result);
                    }
                }
                else
                {
                    result.Message = Homemade.UI.Resources.Homemade.InsertValidDataMessage;
                    result.ResultType = ResultMessageType.error.ToString();
                    result.Status = false;
                    return Json(result);
                }
                #endregion

            }
            catch (System.Exception ex)
            {
                result.ResultType = ResultMessageType.error.ToString();
                result.Message = ex.Message.ToString();
                result.Status = false;
                return Json(result);
            }
        }
        [HttpPost]
        public JsonResult AddVendorAccount(VendorAccountViewModel model)
        {
            try
            {
                #region Adding
                if (model.VendorsID != 0)
                {

                    int UserId = User.GetUserIdInt();
                    if (_blVendor.UpdateVendorAccount(model, UserId))
                    {
                        result.Message = Homemade.UI.Resources.Homemade.SuccessSaveMessage;
                        result.ResultType = ResultMessageType.success.ToString();
                        result.Status = true;
                        return Json(result);
                    }
                    else
                    {
                        result.Message = Homemade.UI.Resources.Homemade.FailSaveMessage;
                        result.ResultType = ResultMessageType.error.ToString();
                        result.Status = false;
                        return Json(result);
                    }
                }
                else
                {
                    result.Message = Homemade.UI.Resources.Homemade.InsertValidDataMessage;
                    result.ResultType = ResultMessageType.error.ToString();
                    result.Status = false;
                    return Json(result);
                }
                #endregion

            }
            catch (System.Exception ex)
            {
                result.ResultType = ResultMessageType.error.ToString();
                result.Message = ex.Message.ToString();
                result.Status = false;
                return Json(result);
            }
        }
        [HttpPost]
        public JsonResult AddVendorLocation(VendorLocationViewModel model)
        {
            try
            {
                #region Adding
                if (model.VendorsID != 0)
                {
                    int UserId = User.GetUserIdInt();
                    if (_blVendor.UpdateVendorLocation(model, UserId))
                    {
                        result.Message = Homemade.UI.Resources.Homemade.SuccessSaveMessage;
                        result.ResultType = ResultMessageType.success.ToString();
                        result.Status = true;
                        return Json(result);
                    }
                    else
                    {
                        result.Message = Homemade.UI.Resources.Homemade.FailSaveMessage;
                        result.ResultType = ResultMessageType.error.ToString();
                        result.Status = false;
                        return Json(result);
                    }
                }
                else
                {
                    result.Message = Homemade.UI.Resources.Homemade.InsertValidDataMessage;
                    result.ResultType = ResultMessageType.error.ToString();
                    result.Status = false;
                    return Json(result);
                }
                #endregion

            }
            catch (System.Exception ex)
            {
                result.ResultType = ResultMessageType.error.ToString();
                result.Message = ex.Message.ToString();
                result.Status = false;
                return Json(result);
            }
        }
        private JsonResult ValidateModel(int VendorsID, int CityID, string Mobile, string Email, string deliveryPrice)
        {

            if (VendorsID == 0)
            {
                if (_blVendor.IsExistMobile(Mobile))
                {
                    result.Message = Homemade.UI.Resources.Homemade.MobileIsExists;
                    result.ResultType = ResultMessageType.error.ToString();
                    return Json(result);
                }
                if (!string.IsNullOrWhiteSpace(Email))
                {
                    if (_blVendor.IsExistEmail(Email))
                    {
                        result.Message = Homemade.UI.Resources.Homemade.EmailExists;
                        result.ResultType = ResultMessageType.error.ToString();
                        return Json(result);
                    }
                }
            }
            else
            {
                if (_blVendor.IsExistMobile(Mobile, VendorsID))
                {
                    result.Message = Homemade.UI.Resources.Homemade.MobileIsExists;
                    result.ResultType = ResultMessageType.error.ToString();
                    return Json(result);
                }
                if (!string.IsNullOrWhiteSpace(Email))
                {
                    if (_blVendor.IsExistEmail(Email, VendorsID))
                    {
                        result.Message = Homemade.UI.Resources.Homemade.EmailExists;
                        result.ResultType = ResultMessageType.error.ToString();
                        return Json(result);
                    }
                }
            }

            if (CityID == 0)
            {
                result.Message = BLL.Resources.HomemadeErrorMessages.CityRequierd;
                result.ResultType = ResultMessageType.error.ToString();
                return Json(result);
            }

            return null;
        }
        [CustomeAuthoriz((int)ControllerEnum.Vendors, (int)ActionEnum.Delete)]
        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            var result = false;
            try
            {
                Guid vendorNewGuid = Guid.NewGuid();
                result = _blVendor.Delete(id, User.GetUserIdInt(), vendorNewGuid);
                if (result)
                {
                    var vendor = _blVendor.GetById(id);
                    if (vendor != null)
                    {
                        User user = _bLUser.GetUserInfo(vendor.UserId);
                        if (user != null)
                        {
                            user.UserName = vendorNewGuid.ToString();
                            user.NormalizedUserName = vendorNewGuid.ToString();
                            await _userManager.UpdateAsync(user);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
            }
            ResultMessage rResultMessage = result ?
                new ResultMessage { Message = Resources.Homemade.SuccessDeleteMessage, ResultType = ResultMessageType.success.ToString() }
                :
                new ResultMessage { Message = Resources.Homemade.FailDeleteMessage, ResultType = ResultMessageType.error.ToString() };
            return Json(rResultMessage);
        }
        [CustomeAuthoriz((int)ControllerEnum.Vendors, (int)ActionEnum.Update)]
        [HttpPost]
        public JsonResult ChangeStatue(int id)
        {
            var result = false;
            try
            {
                result = _blVendor.ChangeStatus(id, User.GetUserIdInt());
            }
            catch (Exception ex)
            {
            }
            ResultMessage rResultMessage = result ?
                new ResultMessage { Message = Resources.Homemade.SuccessChangeMessage, ResultType = ResultMessageType.success.ToString() }
                :
                new ResultMessage { Message = Resources.Homemade.FailChangeStatueMessage, ResultType = ResultMessageType.error.ToString() };
            return Json(rResultMessage);

        }
        [CustomeAuthoriz((int)ControllerEnum.Vendors, (int)ActionEnum.Update)]
        [HttpPost]
        public JsonResult ChangeStatueEnable(int id)
        {
            var result = false;
            try
            {
                result = _blVendor.ChangeStatusEnable(id, User.GetUserIdInt());
            }
            catch (Exception ex)
            {
            }
            ResultMessage rResultMessage = result ?
                new ResultMessage { Message = Resources.Homemade.SuccessChangeMessage, ResultType = ResultMessageType.success.ToString() }
                :
                new ResultMessage { Message = Resources.Homemade.FailChangeStatueMessage, ResultType = ResultMessageType.error.ToString() };
            return Json(rResultMessage);

        }
        #endregion
        #region Helper
        [HttpPost]
        public JsonResult LoadTable(string listRegionID, string listCityID,
           string listBlockID, string listVendorID, string listNationalityID, string listGenderID, string listRegisterTypeId, string listIsEnabled, string listIsVac)
        {
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
            #endregion
            string[] city = null;
            if (!string.IsNullOrEmpty(listCityID) && listCityID != "null")
            {
                city = listCityID.Split(',');
            }
            string[] region = null;
            if (!string.IsNullOrEmpty(listRegionID) && listRegionID != "null")
            {
                region = listRegionID.Split(',');
            }
            string[] block = null;
            if (!string.IsNullOrEmpty(listBlockID) && listBlockID != "null" && listBlockID != "undefined")
            {
                block = listBlockID.Split(',');
            }
            string[] vendor = null;
            if (!string.IsNullOrEmpty(listVendorID) && listVendorID != "null")
            {
                vendor = listVendorID.Split(',');
            }
            string[] nationality = null;
            if (!string.IsNullOrEmpty(listNationalityID) && listNationalityID != "null" && listNationalityID != "undefined")
            {
                nationality = listNationalityID.Split(',');
            }
            string[] gender = null;
            if (!string.IsNullOrEmpty(listGenderID) && listGenderID != "null" && listGenderID != "undefined")
            {
                gender = listGenderID.Split(',');
            }
            string[] registerType = null;
            if (!string.IsNullOrEmpty(listRegisterTypeId) && listRegisterTypeId != "null" && listRegisterTypeId != "undefined")
            {
                registerType = listRegisterTypeId.Split(',');
            }
            bool[] isEnabled = null;
            if (!string.IsNullOrEmpty(listIsEnabled) && listIsEnabled != "null" && listIsEnabled != "undefined")
            {
                isEnabled = listIsEnabled.Split(',').Where(m => !string.IsNullOrEmpty(m)).Select(m => Convert.ToBoolean(Convert.ToInt32(m))).ToArray();
            }
            bool[] isVac = null;
            if (!string.IsNullOrEmpty(listIsVac) && listIsVac != "null" && listIsVac != "undefined")
            {
                isVac = listIsVac.Split(',').Where(m => !string.IsNullOrEmpty(m)).Select(m => Convert.ToBoolean(Convert.ToInt32(m))).ToArray();
            }
            var data = _blVendor.GetAllVendorViewModelBySearch(region, city, block, vendor, nationality, gender, registerType, isEnabled, isVac, InfraStructure.Utility.CurrentLanguageCode, _configuration["VendorImageView"]);
            if (Request.Form.ContainsKey("sSearch"))
            {
                string search = Request.Form["sSearch"];
                if (!string.IsNullOrEmpty(search))
                {
                    data = data.Where(x =>
                    x.FirstName.ToLower().Contains(search.ToLower()) || x.SeconedName.ToLower().Contains(search.ToLower()) || x.StoreNameAr.ToLower().Contains(search.ToLower()) ||
                    x.StoreNameEn.ToLower().Contains(search.ToLower()) || x.MobileNo.ToLower().Contains(search.ToLower()) || x.GenderString.ToLower().Contains(search.ToLower())
                    || x.Email.ToLower().Contains(search.ToLower()) || x.IsEnableString.ToLower().Contains(search.ToLower()));
                }
            }
            var Responce = data.AsQueryable();
            Responce = Responce.Skip(Convert.ToInt32(pageno - 1) * displayLength);
            var takenData = Responce.Take(((Convert.ToInt32(pageno) * displayLength)) - (Convert.ToInt32(pageno - 1) * displayLength));
            int totalCount = data.Count();
            return Json(new
            {
                iTotalRecords = totalCount,
                iTotalDisplayRecords = totalCount,
                aaData = takenData
            });

        }
        public JsonResult GetAllBanks()
        {
            var obj = _blVendor.GetAllBanks().Select(p => new { p.BanksID, BanksName = Homemade.UI.InfraStructure.Utility.CurrentLanguageCode == "ar" ? p.NameAR : p.NameEN }).ToList();
            return Json(obj);
        }
        public JsonResult GetAllPackage()
        {
            var obj = _blVendor.GetAllPackage().Select(p => new { p.PackageID, PackageName = Homemade.UI.InfraStructure.Utility.CurrentLanguageCode == "ar" ? p.NameAR : p.NameEN }).ToList();
            return Json(obj);
        }
        public JsonResult GetAllBlockByCity(int cityID)
        {
            var listOfCities = _blVendor.GetAllBlocksByCity(cityID).Select(p => new { p.BlockID, BlockName = Utility.CurrentLanguageCode == "ar" ? p.NameAR : p.NameEN });
            return Json(listOfCities);
        }
        public JsonResult GetAllVendors()
        {
            var obj = _blVendor.GetAllVendors().Select(p => new { p.VendorsID, VendorsName = Utility.CurrentLanguageCode == "ar" ? p.StoreNameAr : p.StoreNameEn }).ToList();
            return Json(obj);
        }
        public JsonResult GetAllGender()
        {
            var obj = _blVendor.GetGenderEnum(Utility.CurrentLanguageCode).Select(p => new { GenderID = p.Value, GenderName = p.Text }).ToList();
            return Json(obj);
        }
        public JsonResult GetAllDayOfWeek()
        {
            var obj = _blVendor.GetDayOfWeekEnum(Utility.CurrentLanguageCode).Select(p => new { DayID = p.Value, DayName = p.Text }).ToList();
            return Json(obj);
        }
        #endregion
        #region Permession
        public JsonResult LoadTableRolePermissions(string Roles, int? userId)
        {
            List<PermissionControllerActionViewModel> controller = new List<PermissionControllerActionViewModel>();
            if (!string.IsNullOrWhiteSpace(Roles))
            {
                controller = _bLPermission.GetAllPermissionForComapny(Roles, userId, Request.Cookies.IsArabic());
            }
            var data = controller;
            int totalCount = data.Count();

            if (Request.Form.ContainsKey("sSearch"))
            {
                string s = Request.Form["sSearch"];
                if (!string.IsNullOrEmpty(s))
                {
                    data = data.Where(x => x.ControllerName.ToLower().Contains(s.ToLower())).ToList();
                }
            }

            return Json(new
            {
                iTotalRecords = totalCount,
                iTotalDisplayRecords = totalCount,
                aaData = data
            });
        }
        [CustomeAuthoriz((int)ControllerEnum.Vendors, (int)ActionEnum.Update)]
        [HttpPost]
        public ActionResult UpdateVendorPermissions(int VendorsID, string[] Roles, string CheckedItems)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (VendorsID != 0)
                    {
                        if (CheckedItems != null)
                        {
                            var vendor = _blVendor.GetById(VendorsID);
                            var isSuccess = false;
                            if (vendor != null)
                            {
                                foreach (var item in Roles)
                                {
                                    PermissionViewModel model = new PermissionViewModel();
                                    model.RoleID = Convert.ToInt32(item);
                                    model.CheckedItems = CheckedItems;
                                    model.UserId = vendor.UserId;
                                    isSuccess = _bLPermission.EditRolePermissions(model);
                                }
                                if (isSuccess)
                                {
                                    result.ResultType = ResultMessageType.success.ToString();
                                    result.Message = Resources.Homemade.SuccessSaveMessage;
                                }
                                else
                                {
                                    result.ResultType = ResultMessageType.error.ToString();
                                    result.Message = Resources.Homemade.FailSaveMessage;
                                }
                            }
                            else
                            {
                                result.ResultType = ResultMessageType.error.ToString();
                                result.Message = Resources.Homemade.FailSaveMessage;
                            }
                        }
                        else
                        {
                            result.ResultType = ResultMessageType.error.ToString();
                            result.Message = Resources.Homemade.FailSaveMessage;
                        }
                    }
                    else
                    {
                        result.ResultType = ResultMessageType.error.ToString();
                        result.Message = Resources.Homemade.FailSaveMessage;
                    }
                }
                else
                {
                    result.ResultType = ResultMessageType.error.ToString();
                    result.Message = Resources.Homemade.FailSaveMessage;
                }
            }
            catch (Exception ex)
            {
            }
            return Json(result);
        }
        #endregion
        #region Excel
        #region Basic Excel
        public static byte[] ExportExcel(DataTable dataTable, string heading = "", bool showSrNo = false, params string[] columnsToTake)
        {
            byte[] result = null;
            using (ExcelPackage package = new ExcelPackage())
            {

                ExcelWorksheet workSheet = package.Workbook.Worksheets.Add(String.Format("{0} Data", heading));
                int startRowFrom = String.IsNullOrEmpty(heading) ? 1 : 3;

                if (showSrNo)
                {
                    DataColumn dataColumn = dataTable.Columns.Add("SR No", typeof(int));
                    dataColumn.SetOrdinal(0);
                    int index = 1;
                    foreach (DataRow item in dataTable.Rows)
                    {
                        item[0] = index;
                        index++;
                    }
                }
                // add the content into the Excel file  
                workSheet.Cells["A" + startRowFrom].LoadFromDataTable(dataTable, true);
                // autofit width of cells with small content  
                int columnIndex = 1;
                foreach (DataColumn column in dataTable.Columns)
                {
                    ExcelRange columnCells = workSheet.Cells[workSheet.Dimension.Start.Row, columnIndex, workSheet.Dimension.End.Row, columnIndex];

                    if (!string.IsNullOrWhiteSpace(columnCells.Max(cell => cell.Value.ToString())))
                    {
                        int maxLength = columnCells.Max(cell => cell.Value.ToString().Count());
                        if (maxLength < 150)
                        {
                            workSheet.Column(columnIndex).AutoFit();

                        }
                    }

                    columnIndex++;
                }
                // format header - bold, yellow on black  
                using (ExcelRange r = workSheet.Cells[startRowFrom, 1, startRowFrom, dataTable.Columns.Count])
                {
                    r.Style.Font.Color.SetColor(System.Drawing.Color.White);
                    r.Style.Font.Bold = true;
                    r.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    r.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#1fb5ad"));
                }
                // format cells - add borders  
                using (ExcelRange r = workSheet.Cells[startRowFrom + 1, 1, startRowFrom + dataTable.Rows.Count, dataTable.Columns.Count])
                {
                    r.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    r.Style.Border.Top.Color.SetColor(System.Drawing.Color.Black);
                    r.Style.Border.Bottom.Color.SetColor(System.Drawing.Color.Black);
                    r.Style.Border.Left.Color.SetColor(System.Drawing.Color.Black);
                    r.Style.Border.Right.Color.SetColor(System.Drawing.Color.Black);
                }

                if (!String.IsNullOrEmpty(heading))
                {
                    workSheet.Cells["A1"].Value = heading;
                    workSheet.Cells["A1"].Style.Font.Size = 20;

                    workSheet.InsertColumn(1, 1);
                    workSheet.InsertRow(1, 1);
                    workSheet.Column(1).Width = 5;
                }

                result = package.GetAsByteArray();
            }
            return result;
        }
        public static byte[] ExportExcel<T>(List<T> data, string Heading = "", bool showSlno = false, params string[] ColumnsToTake)
        {
            return ExportExcel(ListToDataTable<T>(data), Heading, showSlno, ColumnsToTake);
        }
        public static DataTable ListToDataTable<T>(List<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable dataTable = new DataTable();

            for (int i = 0; i < properties.Count; i++)
            {
                PropertyDescriptor property = properties[i];
                dataTable.Columns.Add(property.Name, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType);
            }

            object[] values = new object[properties.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = properties[i].GetValue(item);
                }

                dataTable.Rows.Add(values);
            }

            return dataTable;
        }
        public static string ExcelContentType
        {
            get
            { return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"; }
        }
        #endregion
        public FileContentResult ExcelVendors(string listRegionID, string listCityID, string listBlockID, string listVendorID, string listNationalityID, string listGenderID, string listRegisterTypeId, string listIsEnabled, string listIsVac)
        {

            byte[] filecontent = null;
            string downloadFileName = "";
            try
            {
                string[] city = null;
                if (!string.IsNullOrEmpty(listCityID) && listCityID != "null")
                {
                    city = listCityID.Split(',');
                }
                string[] region = null;
                if (!string.IsNullOrEmpty(listRegionID) && listRegionID != "null")
                {
                    region = listRegionID.Split(',');
                }
                string[] block = null;
                if (!string.IsNullOrEmpty(listBlockID) && listBlockID != "null" && listBlockID != "undefined")
                {
                    block = listBlockID.Split(',');
                }
                string[] vendor = null;
                if (!string.IsNullOrEmpty(listVendorID) && listVendorID != "null")
                {
                    vendor = listVendorID.Split(',');
                }
                string[] nationality = null;
                if (!string.IsNullOrEmpty(listNationalityID) && listNationalityID != "null" && listNationalityID != "undefined")
                {
                    nationality = listNationalityID.Split(',');
                }
                string[] gender = null;
                if (!string.IsNullOrEmpty(listGenderID) && listGenderID != "null" && listGenderID != "undefined")
                {
                    gender = listGenderID.Split(',');
                }
                string[] registerType = null;
                if (!string.IsNullOrEmpty(listRegisterTypeId) && listRegisterTypeId != "null" && listRegisterTypeId != "undefined")
                {
                    registerType = listRegisterTypeId.Split(',');
                }
                bool[] isEnabled = null;
                if (!string.IsNullOrEmpty(listIsEnabled) && listIsEnabled != "null" && listIsEnabled != "undefined")
                {
                    isEnabled = listIsEnabled.Split(',').Where(m => !string.IsNullOrEmpty(m)).Select(m => Convert.ToBoolean(Convert.ToInt32(m))).ToArray();
                }
                bool[] isVac = null;
                if (!string.IsNullOrEmpty(listIsVac) && listIsVac != "null" && listIsVac != "undefined")
                {
                    isVac = listIsVac.Split(',').Where(m => !string.IsNullOrEmpty(m)).Select(m => Convert.ToBoolean(Convert.ToInt32(m))).ToArray();
                }
                var data = _blVendor.GetAllVendorViewModelBySearch(region, city, block, vendor, nationality, gender, registerType, isEnabled, isVac, InfraStructure.Utility.CurrentLanguageCode, _configuration["VendorImageView"]);


                if (data != null)
                {
                    int totalCount = data.Count();
                    var listexcel = data.Select(x => new
                    {
                        store = x.StoreName,
                        region = x.RegionName,
                        mobileNo = x.MobileNo,
                        completed = x.RegType,
                        createDate = x.CreateDate.ToString("dd-MM-yyyy hh:mm tt"),
                        vacctionStatus = x.IsVacString,
                        entry = x.EntryString,
                        status = x.IsEnableString,
                    }).ToList();
                    string[] column = { "Store", "Region", "Mobile No", "Completed", "Create Date", "Vacction Status", "Entry", "Status" };
                    filecontent = ExportExcel(listexcel, "Selected count : " + totalCount, true, column);
                    downloadFileName = "VendorsList_" + _BLGeneral.GetDateTimeNow() + ".xlsx";
                    return File(filecontent, ExcelContentType, downloadFileName);
                }
                else
                {
                    string[] columns = { "Store", "Region", "Mobile No", "Completed", "Create Date", "Vacction Status", "Entry", "Status" };
                    filecontent = ExportExcel(data.ToList(), "Vendors List ", true, columns);
                    downloadFileName = "VendorsList_" + _BLGeneral.GetDateTimeNow() + ".xlsx";
                    return File(filecontent, ExcelContentType, downloadFileName);
                }
            }
            catch (Exception e)
            {
                string[] columns = { "Store", "Region", "Mobile No", "Completed", "Create Date", "Vacction Status", "Entry", "Status" };
                filecontent = ExportExcel(null, "Vendors List ", true, columns);
                downloadFileName = "VendorsList_" + _BLGeneral.GetDateTimeNow() + ".xlsx";
                return File(filecontent, ExcelContentType, downloadFileName);
            }
        }
        #endregion
    }
}
