using Homemade.BLL.ViewModel.User;
using Homemade.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using Microsoft.AspNetCore.Hosting;
using Homemade.UI.InfraStructure;
using Homemade.BLL;
using System.Linq;
using Homemade.BLL.ViewModel.Identity;
using System.Security;
using Homemade.BLL.General;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using Homemade.UI.Attributes;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Homemade.BLL.ViewModel.Employees;
using Homemade.BLL.Emp;
using Homemade.BLL.Vendor;
using Homemade.BLL.SMS;

namespace Homemade.UI.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class AccountController : Homemade.UI.InfraStructure.Controller.Controller
    {

        #region Declarations

        internal const string SessionResetName = "Code";

        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;
        private readonly ResultMessage _resultMessage;
        private readonly BLGeneral _bLGeneral;
        private readonly BLPermission _BLPermission;
        private readonly BLUser blUser;
        private readonly BlEmployee _BlEmployee;
        private readonly BlVendor _BlVendor;
        private readonly OTPService _OTPService;
        public AccountController(BLPermission bLPermission, SignInManager<User> signInManager, UserManager<User> userManager, IWebHostEnvironment webHostEnvironment, IConfiguration configuration, ResultMessage resultMessage, BLGeneral bLGeneral, BLUser blUser, BlEmployee blEmployee, BlVendor blVendor, OTPService oTPService)
        {
            _BlVendor = blVendor;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _resultMessage = resultMessage;
            _bLGeneral = bLGeneral;
            _BLPermission = bLPermission;
            this.blUser = blUser;
            this._BlEmployee = blEmployee;
            _OTPService = oTPService;
        }
        #endregion
        #region Views
        /// <summary>
        /// دالة خاصة بفتح صفحة تسجيل الدخول
        /// </summary>
        /// <returns></returns>
        public ActionResult AutoVerifyOperation(Guid id, string code)
        {
            AutoVerifyViewModel rr = new AutoVerifyViewModel();
            rr.Id = id;
            rr.Type = 1;
            return View(rr);
        }
        public IActionResult LogIn()
        {
            return View();
        }
        public IActionResult LogInVendor()
        {
            return View();
        }

        public IActionResult CompanyLogIn()
        {
            return View();
        }

        /// <summary>
        /// عرض الملف الشخصي للمستخدم
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public IActionResult Profile()
        {
            var user = User.GetUserIdInt();
            var UserType = blUser.GetUserInfo(User.GetUserIdInt()).UserType;
            var EmpOrVendor = _BlEmployee.GetProfileViewModelByUser(user, Homemade.UI.InfraStructure.Utility.CurrentLanguageCode, (UserType == (int)UserTypeEnum.Employee || UserType == (int)UserTypeEnum.Admin) ?
                _configuration["EmployeeImageView"] : UserType == (int)UserTypeEnum.Vendor ? _configuration["VendorImageView"] : "");
            if (EmpOrVendor != null && (UserType == (int)UserTypeEnum.Employee || UserType == (int)UserTypeEnum.Admin))
            {
                return View(EmpOrVendor);
            }
            else if (EmpOrVendor != null && UserType == (int)UserTypeEnum.Vendor)
            {
                return View("VendorProfile", EmpOrVendor);
            }
            return RedirectToAction(nameof(LogIn));
        }

        /// <summary>
        /// اعاده تعيين كلمه المرور
        /// </summary>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public IActionResult ForgetPassword()
        {
            return View();
        }
        /// <summary>
        /// اعاده تعيين كلمه المرور
        /// </summary>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public IActionResult ForgetPasswordVendor()
        {
            return View();
        }

        /// <summary>
        /// تاكيد الكود وحفظ كلمه المرور الجديده
        /// </summary>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public IActionResult ResetPassword()
        {
            if (!HttpContext.Session.Keys.Contains("EmployeeVerfiedCodeViewModel"))
            {
                return RedirectToAction(nameof(ForgetPassword));
            }

            return View();
        }
        /// <summary>
        /// تاكيد الكود وحفظ كلمه المرور الجديده
        /// </summary>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public IActionResult ResetPasswordVendor()
        {
            if (!HttpContext.Session.Keys.Contains("UserVerfiedCodeViewModel"))
            {
                return RedirectToAction(nameof(ForgetPasswordVendor));
            }

            return View();
        }

        #endregion
        #region Actions
        /// <summary>
        /// دالة تسجيل الدخول حيث تقوم اختبار بيانات اليوزر
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(LoginModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    #region Check User Data
                    var userOpertaor = _BlEmployee.GetByMobile(model.Mobile);
                    if (userOpertaor != null)
                    {
                        // To enable password failures to trigger account lockout, set lockoutOnFailure: false
                        var Userdata = await _userManager.FindByIdAsync(userOpertaor.UserId.ToString());

                        Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(Userdata, model.Password, true, lockoutOnFailure: false);

                        if (result.Succeeded)
                        {
                            return LocalRedirect("/Home/Index");
                            // logz in success
                        }

                        if (result.RequiresTwoFactor)
                        {
                            return RedirectToAction("LoginWith2fa", new { ReturnUrl = "", RememberMe = true });
                        }

                        if (result.IsLockedOut)
                        {
                            return RedirectToPage("./Lockout");
                        }
                        else
                        {
                            ModelState.AddModelError("Error", Homemade.UI.Resources.Homemade.The_login_information_is_incorrect);
                            return View();
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Error", Homemade.UI.Resources.Homemade.You_do_not_have_access);
                    }
                    #endregion

                    ModelState.AddModelError("Error", Homemade.UI.Resources.Homemade.User_Not_Found);
                }
                // If we got this far, something failed, redisplay form
                return View(model);

            }
            catch (Exception e)
            {
                return View(model);
            }
        }

        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public async Task<IActionResult> LogInVendor(LoginModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    #region Check User Data
                    model.Mobile = MobileFormat(model.Mobile, (int)CountryEnum.SA);
                    var vendordata = _BlVendor.GetVendorsByMobileNo(model.Mobile, "City,Nationality");
                    if (vendordata != null)
                    {
                        // To enable password failures to trigger account lockout, set lockoutOnFailure: false
                        var Userdata = await _userManager.FindByIdAsync(vendordata.UserId.ToString());
                        var Vendor = _BlVendor.GetVendorsByUserId(Userdata.Id);
                        if (Vendor != null)
                        {
                            if (vendordata.IsEnable == Convert.ToBoolean(StatusEnable.Enable))
                            {

                                Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(Userdata, model.Password, true, lockoutOnFailure: false);

                                if (result.Succeeded)
                                {
                                    return LocalRedirect("/Home/IndexVendor");
                                }

                                if (result.RequiresTwoFactor)
                                {
                                    return RedirectToAction("LoginWith2fa", new { ReturnUrl = "", RememberMe = true });
                                }

                                if (result.IsLockedOut)
                                {
                                    return RedirectToPage("./Lockout");
                                }
                                else
                                {
                                    ModelState.AddModelError("Error", Homemade.UI.Resources.Homemade.The_login_information_is_incorrect);
                                    return View();
                                }
                            }
                            else
                            {
                                ModelState.AddModelError("Error", Homemade.UI.Resources.Homemade.Sorry_You_Not_Enable);
                                return View();
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("Error", Homemade.UI.Resources.Homemade.This_Account_NotA_Vendor);
                            return View();
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Error", Homemade.UI.Resources.Homemade.Mobile_Not_Valid);
                    }
                    #endregion
                    ModelState.AddModelError("Error", Homemade.UI.Resources.Homemade.User_Not_Found);
                }
                // If we got this far, something failed, redisplay form
                return View(model);

            }
            catch (Exception e)
            {

                return View(model);
            }
        }
        [HttpPost]
        public async Task<JsonResult> VerifyCode(string code)
        {
            try
            {
                if (string.IsNullOrEmpty(code))
                {
                    _resultMessage.Status = (false);
                    _resultMessage.ResultType = (((ResultMessageType)1).ToString() ?? "");
                    _resultMessage.Message = (Homemade.UI.Resources.Homemade.CodeRequired);
                    return Json(_resultMessage);
                }
                else
                {
                    CompanyAccountViewModel companyAccountViewModel = new CompanyAccountViewModel();
                    companyAccountViewModel = this.HttpContext.Session.GetObject<CompanyAccountViewModel>("VerifyCompanyEmail");
                    if (companyAccountViewModel.Code != code)
                    {
                        _resultMessage.Status = (false);
                        _resultMessage.ResultType = ResultMessageType.error.ToString();
                        _resultMessage.Message = (Homemade.UI.Resources.Homemade.WrongCode);
                        return Json(_resultMessage);
                    }
                    else
                    {
                        var Userdata = await _userManager.FindByIdAsync(companyAccountViewModel.UserID.ToString());
                        Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(Userdata.UserName, companyAccountViewModel.Password, true, lockoutOnFailure: false);

                        if (result.Succeeded)
                        {
                            await _userManager.UpdateAsync(Userdata);

                            _resultMessage.Status = true;
                            _resultMessage.ResultType = ResultMessageType.success.ToString();
                            _resultMessage.Message = "Login Success";
                            return Json(_resultMessage);
                        }
                        else
                        {
                            _resultMessage.Status = (false);
                            _resultMessage.ResultType = ResultMessageType.error.ToString();
                            _resultMessage.Message = (Homemade.UI.Resources.Homemade.FailSaveMessage);
                            return Json(_resultMessage);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _resultMessage.Status = false;
                _resultMessage.ResultType = (((ResultMessageType)1).ToString() ?? "");
                _resultMessage.Message = Homemade.UI.Resources.Homemade.FailSaveMessage;
                return Json(_resultMessage);
            }

        }
        /// <summary>
        /// تسجيل الخروج
        /// </summary>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> LogOut()
        {
            try
            {
                var Userdata = await _userManager.FindByIdAsync(User.GetUserIdInt().ToString());
                if (Userdata.UserType == (int)UserTypeEnum.Vendor)
                {
                    await _signInManager.SignOutAsync();
                    return Redirect(Url.Action("loginvendor", "Account", new { Area = "Identity" }));
                }
                else
                {
                    await _signInManager.SignOutAsync();
                    return Redirect(Url.Action("login", "Account", new { Area = "Identity" }));
                }
            }
            catch (Exception)
            {
                return Redirect(Url.Action("Index", "Home", new { Area = "Site" }));
            }
        }

        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> LogOutSite()
        {
            await _signInManager.SignOutAsync();
            return Redirect(Url.Action("login", "Home", new { Area = "Site" }));

        }
        [HttpPost, Authorize, ValidateAntiForgeryToken]
        public JsonResult ChangeProfileImage(int Id, IFormFile fupPersonalPhoto)
        {
            ResultMessage resultMessage = new ResultMessage();
            try
            {
                if (Request.Form != null && Request.Form.Files?.Count() > 0)
                {
                    resultMessage.Message = "";
                    resultMessage.ResultType = ResultMessageType.success.ToString();
                    resultMessage.Status = true;
                    using System.IO.Stream st = Request.Form.Files[0].OpenReadStream();
                    string fileName = Guid.NewGuid() + ".png";

                    var UserType = blUser.GetUserInfo(User.GetUserIdInt()).UserType;
                    if (UserType == (int)UserTypeEnum.Employee)
                    {
                        _BlEmployee.UpdatePersonalImage(Id, AppConst.DomainUrl, _configuration["EmployeeImageSave"], ref fileName, st, User.GetUserIdInt());
                    }
                    else if (UserType == (int)UserTypeEnum.Vendor)
                    {
                        _BlVendor.UpdatePersonalImage(Id, AppConst.DomainUrl, _configuration["VendorImageSave"], ref fileName, st, User.GetUserIdInt());
                    }

                    return Json(new { resultMessage, Data = fileName });
                }
                resultMessage.Message = Homemade.UI.Resources.Homemade.PersonalFileUpload;
                resultMessage.ResultType = ResultMessageType.error.ToString();
                resultMessage.Status = false;
            }
            catch (Exception ex)
            {
                resultMessage.Message = Homemade.UI.Resources.Homemade.PersonalFileUpload;
                resultMessage.ResultType = ResultMessageType.error.ToString();
                resultMessage.Status = false;
            }
            return Json(new { resultMessage });
        }

        [HttpPost, Authorize, ValidateAntiForgeryToken]
        public async Task<JsonResult> ChangePassword(ChangePasswordViewModel mdl)
        {
            ResultMessage resultMessage = new ResultMessage
            {
                ResultType = ResultMessageType.error.ToString(),
                Status = false,
                Message = ""
            };
            try
            {
                if (mdl != null && !IsStringEmpty(mdl.ConfirmNewPassword) && !IsStringEmpty(mdl.OldPassword) && !IsStringEmpty(mdl.NewPassword))
                {
                    if (mdl.NewPassword != mdl.ConfirmNewPassword)
                    {
                        resultMessage.Message = Resources.Homemade.PasswordNotMatched;
                    }
                    else
                    {
                        var user = await _userManager.GetUserAsync(User);
                        if (user != null)
                        {
                            var identityResult = await _userManager.CheckPasswordAsync(user, mdl.OldPassword);
                            if (identityResult)
                            {
                                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, mdl.NewPassword);
                                var isss = await _userManager.UpdateAsync(user);
                                if (isss.Succeeded)
                                {
                                    resultMessage.ResultType = ResultMessageType.success.ToString();
                                    resultMessage.Status = true;
                                    resultMessage.Message = Resources.Homemade.SuccessSaveMessage;
                                    return Json(resultMessage);
                                }
                                IdentityError errors = isss.Errors.ToList().FirstOrDefault();
                                if (errors != null)
                                {
                                    resultMessage.Message = errors.Code != "PasswordMismatch" ?
                                        Resources.Homemade.Password + " " + Resources.Homemade.AspIdentityPassVali
                                        : Homemade.UI.Resources.Homemade.InvalidPassword;
                                }
                            }
                            else
                            {
                                resultMessage.Message = Homemade.UI.Resources.Homemade.InvalidPassword;

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultMessage.Message = Homemade.UI.Resources.Homemade.InvalidPassword;

            }
            return Json(resultMessage);
        }


        [HttpPost, Authorize, ValidateAntiForgeryToken]
        public async Task<JsonResult> ChangeMainData(ProfileViewModel model)
        {
            ResultMessage resultMessage = new ResultMessage
            {
                ResultType = ResultMessageType.error.ToString(),
                Status = false,
                Message = ""
            };
            try
            {
                var UserType = blUser.GetUserInfo(User.GetUserIdInt()).UserType;

                if (ModelState.IsValid && model != null && UserType == (int)UserTypeEnum.Employee)
                {
                    var Employee = _BlEmployee.GetByUserId(User.GetUserIdInt());
                    model.UserUpdate = User.GetUserIdInt();
                    if (Employee != null)
                    {
                        bool IsMobileChanged = false;
                        if (Employee.MobileNo != model.MobileNo)
                        {
                            IsMobileChanged = true;
                            if (!IsStringEmpty(model.MobileNo))
                            {
                                if (_BlEmployee.IsExistMobile(model.MobileNo, Employee.EntityEmpID))
                                {
                                    resultMessage.Message = Resources.Homemade.PhoneIsExists;
                                    return Json(resultMessage);
                                }
                            }
                            else
                            {
                                resultMessage.Message = Resources.Homemade.MobileNumberRequired;
                                return Json(resultMessage);
                            }
                        }

                        if (_BlEmployee.IsExistEmail(model.Email, Employee.EntityEmpID))
                        {
                            resultMessage.Message = Resources.Homemade.EmailExists;
                            return Json(resultMessage);
                        }
                        if (_BlEmployee.Update(Employee.EntityEmpID, model))
                        {
                            resultMessage.ResultType = ResultMessageType.success.ToString();
                            resultMessage.Status = true;
                            resultMessage.Message = Resources.Homemade.SuccessSaveMessage;
                            return Json(resultMessage);
                        }
                    }
                }
                else if (ModelState.IsValid && model != null && UserType == (int)UserTypeEnum.Vendor)
                {
                    var Vendor = _BlVendor.GetVendorsByUserId(User.GetUserIdInt());
                    model.UserUpdate = User.GetUserIdInt();
                    if (Vendor != null)
                    {
                        bool IsMobileChanged = false;
                        if (Vendor.MobileNo != model.MobileNo)
                        {
                            IsMobileChanged = true;
                            if (!IsStringEmpty(model.MobileNo))
                            {
                                if (_BlVendor.IsExistMobile(model.MobileNo, Vendor.VendorsID))
                                {
                                    resultMessage.Message = Resources.Homemade.PhoneIsExists;
                                    return Json(resultMessage);
                                }
                            }
                            else
                            {
                                resultMessage.Message = Resources.Homemade.MobileNumberRequired;
                                return Json(resultMessage);
                            }
                        }

                        if (_BlVendor.IsExistEmail(model.Email, Vendor.VendorsID))
                        {
                            resultMessage.Message = Resources.Homemade.EmailExists;
                            return Json(resultMessage);
                        }
                        if (_BlVendor.UpdateProfile(model.VendorID.Value, model))
                        {
                            resultMessage.ResultType = ResultMessageType.success.ToString();
                            resultMessage.Status = true;
                            resultMessage.Message = Resources.Homemade.SuccessSaveMessage;
                            return Json(resultMessage);
                        }
                    }
                }
                resultMessage.Message = Resources.Homemade.InsertValidDataMessage;
            }
            catch (Exception ex)
            {
                resultMessage.Message = Resources.Homemade.InsertValidDataMessage;
            }
            return Json(resultMessage);
        }
        #region Forget Password Site
        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> ForgetPasswordVendor(string username)
        {
            try
            {
                if (IsStringEmpty(username))
                {
                    _resultMessage.Status = false;
                    _resultMessage.ResultType = ResultMessageType.error.ToString();
                    _resultMessage.Message = Resources.Homemade.UserName + " " + BLL.Resources.HomemadeErrorMessages.Required;
                    return Json(_resultMessage);
                }
                #region Check User Data
                var vendordata = _BlVendor.GetVendorsByMobileNo(username, "City,Nationality");
                if (vendordata != null)
                {
                    var KeyCode = _bLGeneral.RandomNumber(4);
                    var SMSStatus = await _OTPService.SendSMSForVerify(vendordata.MobileNo
                        , KeyCode, (int)UserTypeEnum.Vendor, (int)MessageReasonEnum.ForgetPassword);

                    HttpContext.Session.SetObject("UserVerfiedCodeViewModel", new ResetPasswordViewModel() { Code = KeyCode, UserName = vendordata.MobileNo });

                    _resultMessage.Status = true;
                    _resultMessage.ResultType = ResultMessageType.success.ToString();
                    _resultMessage.Message = Homemade.UI.Resources.Homemade.SuccessSendCode;
                    return Json(_resultMessage);
                }
                else
                {
                    _resultMessage.Status = false;
                    _resultMessage.ResultType = ResultMessageType.error.ToString();
                    _resultMessage.Message = Homemade.UI.Resources.Homemade.Mobile_Not_Valid;
                    return Json(_resultMessage);
                }
                #endregion
            }
            catch (Exception ex)
            {
                _resultMessage.Message = string.Empty;
                _resultMessage.ResultType = ResultMessageType.error.ToString();
                _resultMessage.Status = false;
                return Json(_resultMessage);
            }
        }


        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public async Task<JsonResult> ResendCodeVendor()
        {
            try
            {
                if (!HttpContext.Session.Keys.Contains("UserVerfiedCodeViewModel")) return Json(false);

                ResetPasswordViewModel model = HttpContext.Session.GetObject<ResetPasswordViewModel>("UserVerfiedCodeViewModel");

                if (model == null) return Json(false);

                var KeyCode = _bLGeneral.RandomNumber(4);
                var SMSStatus = await _OTPService.SendSMSForVerify(model.UserName
                    , KeyCode, (int)UserTypeEnum.Customer, (int)MessageReasonEnum.ForgetPassword);

                HttpContext.Session.SetObject("UserVerfiedCodeViewModel", new ResetPasswordViewModel() { Code = KeyCode, UserName = model.UserName });
            }
            catch (Exception ex)
            {

                var esx = ex.ToString();
            }
            return Json(true);
        }

        /// <summary>
        /// التاكيد ان الكود الذي تم ارساله هو الموجود 
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public JsonResult ConfirmCodeVendor(string Code)
        {
            ResultMessage resultMessage = new ResultMessage();
            try
            {
                if (IsStringEmpty(Code))
                {
                    resultMessage.Message =
                        Resources.Homemade.Code + " " + BLL.Resources.HomemadeErrorMessages.Required;
                    resultMessage.ResultType = ResultMessageType.error.ToString();
                    resultMessage.Status = false;

                    return Json(resultMessage);
                }

                var SavedCode = HttpContext.Session.GetObject<ResetPasswordViewModel>("UserVerfiedCodeViewModel");

                if (SavedCode == null || IsStringEmpty(SavedCode.Code))
                {
                    return Json(new { Status = false, Reload = true });
                }

                if (SavedCode.Code != Code)
                {
                    resultMessage.Message = Resources.Homemade.InvalidVerifiedCode;
                    resultMessage.ResultType = ResultMessageType.error.ToString();
                    resultMessage.Status = false;

                    return Json(resultMessage);
                }

                resultMessage.Message = "";
                resultMessage.ResultType = ResultMessageType.success.ToString();
                resultMessage.Status = true;

                return Json(resultMessage);
            }
            catch (Exception ex)
            {
                resultMessage.Message = string.Empty;
                resultMessage.ResultType = ResultMessageType.error.ToString();
                resultMessage.Status = false;
                return Json(resultMessage);
            }
        }

        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public async Task<JsonResult> ResetPasswordVendor(string Code, string Password)
        {
            ResultMessage resultMessage = new ResultMessage
            {
                ResultType = ResultMessageType.error.ToString(),
                Status = false
            };
            try
            {
                if (IsStringEmpty(Code))
                {
                    resultMessage.Message = Resources.Homemade.Code + " " + BLL.Resources.HomemadeErrorMessages.Required;
                    return Json(resultMessage);
                }

                if (IsStringEmpty(Password))
                {
                    resultMessage.Message = Resources.Homemade.Password + " " + BLL.Resources.HomemadeErrorMessages.Required;
                    return Json(resultMessage);
                }

                var SavedCode = HttpContext.Session.GetObject<ResetPasswordViewModel>("UserVerfiedCodeViewModel");

                if (SavedCode == null || IsStringEmpty(SavedCode.Code)) return Json(new { Status = false, Reload = true });

                if (SavedCode.Code != Code)
                {
                    resultMessage.Message = Resources.Homemade.InvalidCode;
                    return Json(resultMessage);
                }
                var vendordata = _BlVendor.GetVendorsByMobileNo(SavedCode.UserName, "User");
                if (vendordata != null)
                {
                    var user = await _userManager.FindByNameAsync(vendordata.User.UserName);

                    if (user == null) return Json(new { Status = false, Reload = true });
                    string token = await _userManager.GeneratePasswordResetTokenAsync(user);


                    IdentityResult result = await _userManager.ResetPasswordAsync(user, token, Password);

                    if (!result.Succeeded)
                    {
                        resultMessage.Message = (Resources.Homemade.Password + " - " + Resources.Homemade.AspIdentityPassVali).Replace("-", "<br />");
                        return Json(resultMessage);
                    }
                    await _signInManager.PasswordSignInAsync(user, Password, true, lockoutOnFailure: false);
                    resultMessage.ResultType = ResultMessageType.success.ToString();
                    resultMessage.Status = true;
                    resultMessage.Message = "";
                }
            }
            catch (Exception ex)
            {
                resultMessage.Message = string.Empty;
            }
            return Json(resultMessage);
        }
        #endregion
        #region Forget Password Site
        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> ForgetPassword(string username)
        {
            try
            {
                if (IsStringEmpty(username))
                {
                    _resultMessage.Status = false;
                    _resultMessage.ResultType = ResultMessageType.error.ToString();
                    _resultMessage.Message = Resources.Homemade.UserName + " " + BLL.Resources.HomemadeErrorMessages.Required;
                    return Json(_resultMessage);
                }
                #region Check User Data
                var employee = _BlEmployee.GetByMobile(username,"User");
                if (employee != null)
                {
                    var KeyCode = _bLGeneral.RandomNumber(4);
                    var SMSStatus = await _OTPService.SendSMSForVerify(employee.MobileNo, KeyCode, employee.User.UserType, (int)MessageReasonEnum.ForgetPassword);
                    HttpContext.Session.SetObject("EmployeeVerfiedCodeViewModel", new SiteResetPasswordViewModel() { Code = KeyCode, Email = employee.Email, UserName = employee.User.UserName });
                    _resultMessage.Status = true;
                    _resultMessage.ResultType = ResultMessageType.success.ToString();
                    _resultMessage.Message = Homemade.UI.Resources.Homemade.SuccessSendCode;
                    return Json(_resultMessage);
                }
                else
                {
                    _resultMessage.Status = false;
                    _resultMessage.ResultType = ResultMessageType.error.ToString();
                    _resultMessage.Message = Homemade.UI.Resources.Homemade.Email_Not_Valid;
                    return Json(_resultMessage);
                }
                #endregion

            }
            catch (Exception ex)
            {
                _resultMessage.Message = string.Empty;
                _resultMessage.ResultType = ResultMessageType.error.ToString();
                _resultMessage.Status = false;
                return Json(_resultMessage);
            }
        }


        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public async Task<JsonResult>  ResendCode()
        {
            if (!HttpContext.Session.Keys.Contains("EmployeeVerfiedCodeViewModel")) return Json(false);
            SiteResetPasswordViewModel model = HttpContext.Session.GetObject<SiteResetPasswordViewModel>("EmployeeVerfiedCodeViewModel");
            if (model == null) return Json(false);
            var SMSStatus = await _OTPService.SendSMSForVerify(model.UserName, model.Code, (int)UserTypeEnum.Employee, (int)MessageReasonEnum.ForgetPassword);
            return Json(true);
        }

        /// <summary>
        /// التاكيد ان الكود الذي تم ارساله هو الموجود 
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public JsonResult ConfirmCode(string Code)
        {
            ResultMessage resultMessage = new ResultMessage();
            try
            {
                if (IsStringEmpty(Code))
                {
                    resultMessage.Message =
                        Resources.Homemade.Code + " " + BLL.Resources.HomemadeErrorMessages.Required;
                    resultMessage.ResultType = ResultMessageType.error.ToString();
                    resultMessage.Status = false;

                    return Json(resultMessage);
                }

                var SavedCode = HttpContext.Session.GetObject<SiteResetPasswordViewModel>("EmployeeVerfiedCodeViewModel");

                if (SavedCode == null || IsStringEmpty(SavedCode.Code))
                {
                    return Json(new { Status = false, Reload = true });
                }

                if (SavedCode.Code != Code)
                {
                    resultMessage.Message = Resources.Homemade.InvalidVerifiedCode;
                    resultMessage.ResultType = ResultMessageType.error.ToString();
                    resultMessage.Status = false;

                    return Json(resultMessage);
                }

                resultMessage.Message = "";
                resultMessage.ResultType = ResultMessageType.success.ToString();
                resultMessage.Status = true;

                return Json(resultMessage);
            }
            catch (Exception ex)
            {
                resultMessage.Message = string.Empty;
                resultMessage.ResultType = ResultMessageType.error.ToString();
                resultMessage.Status = false;
                return Json(resultMessage);
            }
        }

        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public async Task<JsonResult> ResetPassword(string Code, string Password)
        {
            ResultMessage resultMessage = new ResultMessage
            {
                ResultType = ResultMessageType.error.ToString(),
                Status = false
            };
            try
            {
                if (IsStringEmpty(Code))
                {
                    resultMessage.Message = Resources.Homemade.Code + " " + BLL.Resources.HomemadeErrorMessages.Required;
                    return Json(resultMessage);
                }

                if (IsStringEmpty(Password))
                {
                    resultMessage.Message = Resources.Homemade.Password + " " + BLL.Resources.HomemadeErrorMessages.Required;
                    return Json(resultMessage);
                }

                var SavedCode = HttpContext.Session.GetObject<SiteResetPasswordViewModel>("EmployeeVerfiedCodeViewModel");

                if (SavedCode == null || IsStringEmpty(SavedCode.Code)) return Json(new { Status = false, Reload = true });

                if (SavedCode.Code != Code)
                {
                    resultMessage.Message = Resources.Homemade.InvalidCode;
                    return Json(resultMessage);
                }
                var employee = _BlEmployee.GetByMobile(SavedCode.UserName, "User");
                if (employee != null)
                {
                    var user = await _userManager.FindByNameAsync(employee.User.UserName);

                    if (user == null) return Json(new { Status = false, Reload = true });
                    string token = await _userManager.GeneratePasswordResetTokenAsync(user);


                    IdentityResult result = await _userManager.ResetPasswordAsync(user, token, Password);

                    if (!result.Succeeded)
                    {
                        resultMessage.Message = (Resources.Homemade.Password + " - " + Resources.Homemade.AspIdentityPassVali).Replace("-", "<br />");
                        return Json(resultMessage);
                    }

                    await _signInManager.PasswordSignInAsync(user, Password, true, lockoutOnFailure: false);

                    resultMessage.ResultType = ResultMessageType.success.ToString();
                    resultMessage.Status = true;
                    resultMessage.Message = "";
                }
            }
            catch (Exception ex)
            {
                resultMessage.Message = string.Empty;
            }
            return Json(resultMessage);
        }
        #endregion
        #endregion
        #region Helper
        private string MobileFormat(string mobileNo, int countryId)
        {
            if (countryId == 2)
            {
                string str = mobileNo.Substring(0, 2);
                str = (str != "01" ? mobileNo : mobileNo.Substring(1));
                return str;
            }
            string str1 = mobileNo.Substring(0, 2);
            str1 = (str1 != "05" ? mobileNo : mobileNo.Substring(1));
            return str1;
        }
        private bool IsStringEmpty(string s) => string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s);

        private string GetResetPassTemp(string te)
        {
            string templateString = "";

            if (!System.IO.File.Exists(_webHostEnvironment.WebRootPath + AppConst.ResetPasswordTemplatePath)) return templateString;

            templateString = System.IO.File.ReadAllText(_webHostEnvironment.WebRootPath + AppConst.ResetPasswordTemplatePath);
            var sss = Request.GetAppMainLink();
            templateString = templateString.Replace("@VerifyLink", Request.GetAppMainLink())
                      .Replace("@dir", Utility.CurrentLanguageCode == "ar" ? "rtl" : "ltr").Replace("@ImgLogo", "https://cdn.made-home.com/Shared/Images/logo_f.png").Replace("@Code", te);

            return Utility.CurrentLanguageCode switch
            {
                "ar" => templateString.Replace("@YourStyle", ".en{display:none !important;} "),
                _ => templateString.Replace("@YourStyle", ".ar{display:none !important;}"),
            };
        }
        public string GetConfirmPassTemp()
        {
            string templateString = "تم تغيير الرقم السرى الخاص بكم بنجاح <br /> فريق ماجور  <br /> في حالة طلب المساعدة يمكنك الاتصال على الرقم التالي او الواتساب ٠١٣٨٣٢٦٥٨٠ <br /> Your Password has been successfully changed <br />   Homemade Team <br />  In the event that assistance is requested, you can call the following number or WhatsApp 0138326580 ";
            return templateString;
        }
        #endregion      
        #region Partial Views
        public IActionResult ShowProfileImage() => PartialView("_ProfileImage", "");
        public IActionResult ShowProfile() => PartialView("_ShowProfile", "");
        #endregion
    }
}
