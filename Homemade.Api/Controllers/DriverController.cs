using Homemade.Api.Authentication;
using Homemade.Api.Model;
using Homemade.Api.Models;
using Homemade.BLL;
using Homemade.BLL.Driver;
using Homemade.BLL.General;
using Homemade.BLL.Setting;
using Homemade.BLL.SMS;
using Homemade.BLL.Vendor;
using Homemade.BLL.ViewModel.Driver;
using Homemade.BLL.ViewModel.Order;
using Homemade.BLL.ViewModel.Setting;
using Homemade.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        #region Declarations
        private readonly BlTokens _blTokens;
        private readonly BlDriver _BlDriver;
        private readonly BLGeneral _blGeneral;
        private UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private FixedResultMessage _Result = new FixedResultMessage();
        private readonly BlCity _blCity;
        private readonly BlVendor _BlVendor;
        private readonly BlHelpQuestions _blHelpQuestions;
        private readonly BlDriverSupport _BlDriverSupport;
        private readonly OTPService _OTPService;
        private readonly BlDriverBalance _BlDriverBalance;

        public DriverController(BlDriverBalance BlDriverBalance,OTPService OTPService,BlDriverSupport BlDriverSupport, BlHelpQuestions blHelpQuestions, BlCity BlCity, BlDriver BlDriver, BlTokens blTokens, BLGeneral blGeneral, UserManager<User> userManager, IConfiguration configuration, BlVendor blVendor)
        {
            _blTokens = blTokens;
            _blGeneral = blGeneral;
            _userManager = userManager;
            _configuration = configuration;
            _BlDriver = BlDriver;
            _blHelpQuestions = blHelpQuestions;
            _blCity = BlCity;
            _BlVendor = blVendor;
            _BlDriverSupport = BlDriverSupport;
            _OTPService = OTPService;
            _BlDriverBalance = BlDriverBalance;
        }
        #endregion

        #region Get Token / Login
        [AllowAnonymous]
        [Consumes("application/x-www-form-urlencoded")]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromForm] LoginModel loginModel)
        {
            var accLanguage = GetUserLang();
            try
            {
                var Driverdata = _BlDriver.GetDriverByIDNO(loginModel.userName, "City,Nationality");
                if (Driverdata != null)
                {
                    var user = await _userManager.FindByIdAsync(Driverdata.UserId.ToString());
                    if (await _userManager.CheckPasswordAsync(user, loginModel.password))
                    {
                        #region Identity
                        var userRoles = await _userManager.GetRolesAsync(user);
                        var authClaims = new List<Claim> { new Claim(ClaimTypes.Name, user.UserName), new Claim(ClaimTypes.Sid, user.Id.ToString()), new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), };
                        foreach (var userRole in userRoles)
                        {
                            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                        }
                        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                        var Token = new JwtSecurityToken(
                        issuer: _configuration["JWT:ValidIssuer"],
                        audience: _configuration["JWT:ValidAudience"],
                        expires: _blGeneral.GetDateTimeNow().AddSeconds(20),
                        claims: authClaims,
                        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                        );
                        #endregion
                        #region Check Login Driver 
                        #region Login Driver
                        if (user == null)
                        {
                            _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Wrong_username_or_password : Api.Resources.Response.Wrong_username_or_password;
                            _Result.status = false;
                            _Result.isAuthorize = true;
                            return Ok(_Result);
                        }
                        else
                        {
                            if (Driverdata.IsEnable == Convert.ToBoolean(StatusEnable.Enable))
                            {
                                #region Token
                                _blTokens.AddTokens(user.Id, loginModel.deviceType, loginModel.token, (int)UserTypeEnum.Driver);
                                #endregion

                                #region Update JWT Token
                                _blTokens.UpdateUserJWTToken(user.Id, new JwtSecurityTokenHandler().WriteToken(Token));
                                #endregion

                                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Login_Successfuly : Api.Resources.Response.Login_Successfuly;
                                _Result.status = true;
                                _Result.isAuthorize = true;
                                _Result.data = new
                                {
                                    name = accLanguage == "ar" ? Driverdata.NameAr : Driverdata.NameEn,
                                    mobileNo = Driverdata.PhoneNumber,
                                    iDNo = Driverdata.IDNo,
                                    email = Driverdata.Email,
                                    gender = accLanguage == "ar" ? (Driverdata.Gender == (int)Gender.Male ? "Male" : Driverdata.Gender == (int)Gender.Female ? "Female" : "Prefer not to specify")
                                    : (Driverdata.Gender == (int)Gender.Male ? "ذكر" : Driverdata.Gender == (int)Gender.Female ? "انثى" : "تفضل عدم التحديد"),
                                    personalPhoto = !string.IsNullOrWhiteSpace(Driverdata.PersonalPicture) ? _configuration["DriverImageView"] + Driverdata.PersonalPicture : "",
                                    driversID = Driverdata.DriversID,
                                    cityName = accLanguage == "ar" ? Driverdata.City.NameAR : Driverdata.City.NameEN,
                                    nationalityName = accLanguage == "ar" ? Driverdata.Nationality.NameAR : Driverdata.Nationality.NameEN,
                                    userId = user.Id,
                                    token = new JwtSecurityTokenHandler().WriteToken(Token),
                                    wallet = accLanguage == "ar" ? "حصلت علي " + _BlDriver.GetAmountPaid(Driverdata.DriversID).Paid + " ريـال" :
                                    "you Collected " + _BlDriver.GetAmountPaid(Driverdata.DriversID).Paid + " SR",
                                    pendingPay = _BlDriver.GetAmountPaid(Driverdata.DriversID).UnPaid,
                                    paid = _BlDriver.GetAmountPaid(Driverdata.DriversID).Paid,
                                    isOnline = Driverdata.IsOnline
                                };
                                return Ok(_Result);
                            }
                            else
                            {
                                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Sorry_You_Not_Enable : Api.Resources.Response.Sorry_You_Not_Enable;
                                _Result.status = false;
                                _Result.isAuthorize = true;
                                return Ok(_Result);
                            }


                        }
                        #endregion
                        #endregion

                    }
                    else
                    {
                        _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Wrong_username_or_password : Api.Resources.Response.Wrong_username_or_password;
                        _Result.status = false;
                        _Result.isAuthorize = true;
                        return Ok(_Result);
                    }
                }
                else
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Wrong_username_or_password : Api.Resources.Response.Wrong_username_or_password;
                    _Result.status = false;
                    _Result.isAuthorize = true;
                    return Ok(_Result);
                }
            }
            catch (Exception ex)
            {
                _Result.message = accLanguage == "ar" ? ex.ToString() : ex.ToString();
                _Result.status = false;
                _Result.isAuthorize = true;
                return Ok(_Result);
            }
        }
        #endregion
        #region Logout
        [CustomAuthorization]
        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            string message;
            string userLang = GetUserLang();
            try
            {
                string userIdClient = GetUserIdDriver();
                if (userIdClient != "incorrect Authorization Value")
                {
                    int num = int.Parse(userIdClient.ToString());
                    _blTokens.UpdateTokens(num, 0, string.Empty);
                    _blTokens.UpdateUserJWTToken(num, string.Empty);
                     var IsSuccess = _BlDriver.UpdateOffline(num);
                    _Result.message = (userLang == "ar" ? Resources.Response_ar.Logout_Successfully : Resources.Response.Logout_Successfully);
                    _Result.status = true;
                    _Result.isAuthorize = true;
                    _Result.data = new { };
                    return Ok(_Result);
                }
                else
                {
                    _Result.message = (userLang == "ar" ? Resources.Response_ar.incorrect_Authorization_Value : Resources.Response.incorrect_Authorization_Value);
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
            }
            catch (Exception exception1)
            {
                _Result.message = (userLang == "ar" ? exception1.ToString() : exception1.ToString());
                _Result.status = false;
                _Result.isAuthorize = true;
                return Ok(_Result);
            }
        }
        #endregion
        #region Class
        public class MyModelReg
        {
            public IFormFile personalPic { get; set; }
            public IFormFile carPic { get; set; }
            public IFormFile iDPic { get; set; }
            public IFormFile licensePic { get; set; }
            public IFormFile carLicensePic { get; set; }
            [FromJson]
            public RegisterDriverViewModel driverData { get; set; } // <-- JSON will be deserialized to this object
        }
        public class MyModelUpdate
        {
            public IFormFile personalPic { get; set; }
            public IFormFile carPic { get; set; }
            public IFormFile iDPic { get; set; }
            public IFormFile licensePic { get; set; }
            public IFormFile carLicensePic { get; set; }
            [FromJson]
            public UpdateDriverViewModel driverData { get; set; } // <-- JSON will be deserialized to this object
        }
        #endregion
        #region Register
        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Obsolete]
        public async Task<IActionResult> Register([FromForm] MyModelReg createDriverViewModel)
        {
            var accLanguage = GetUserLang();
            try
            {
                if (createDriverViewModel.driverData.cityID != 0)
                {
                    if (!string.IsNullOrWhiteSpace(createDriverViewModel.driverData.mobileNo))
                    {
                        if (!string.IsNullOrWhiteSpace(createDriverViewModel.driverData.name))
                        {
                            if (createDriverViewModel.driverData.nationaltyID != 0)
                            {

                                int countryId = _blCity.GetById(createDriverViewModel.driverData.cityID, "Region").Region.CountryID;
                                createDriverViewModel.driverData.mobileNo = MobileFormat(createDriverViewModel.driverData.mobileNo, countryId);
                                if (!A_Valid_Phone_Number(createDriverViewModel.driverData.mobileNo, countryId))
                                {
                                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Invalid_Phone_number : Api.Resources.Response.Invalid_Phone_number;
                                    _Result.status = false;
                                    _Result.isAuthorize = true;
                                    return Ok(_Result);
                                }

                                if (!_BlDriver.IsExistMobile(createDriverViewModel.driverData.mobileNo))
                                {
                                    if (!_BlDriver.IsExistIDNo(createDriverViewModel.driverData.iDNo))
                                    {
                                        if (!string.IsNullOrWhiteSpace(createDriverViewModel.driverData.email))
                                        {
                                            if (_BlDriver.IsExistEmail(createDriverViewModel.driverData.email))
                                            {
                                                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Email_Is_Exist : Api.Resources.Response.Email_Is_Exist;
                                                _Result.status = false;
                                                _Result.isAuthorize = true;
                                                return Ok(_Result);
                                            }
                                        }
                                        if (!string.IsNullOrWhiteSpace(createDriverViewModel.driverData.password))
                                        {
                                            if (createDriverViewModel.driverData.password.Length > 5)
                                            {
                                                #region add Driver
                                                #region User
                                                //Insert User
                                                var user = new User
                                                {
                                                    UserName = _blGeneral.GenerateToken(200),
                                                    Email = _blGeneral.RandomString(10) + "@made-home.com",
                                                    PhoneNumber = _blGeneral.RandomNumber(10),
                                                    UserType = (int)UserTypeEnum.Driver
                                                };
                                                var identityResult = await _userManager.CreateAsync(user, createDriverViewModel.driverData.password);
                                                #endregion
                                                if (identityResult.Succeeded)
                                                {
                                                    try
                                                    {
                                                        #region Image
                                                        string ImagePath = _configuration["DriverImageSave"];
                                                        var logofileName = string.Empty;
                                                        if (createDriverViewModel.personalPic != null)
                                                        {
                                                            if (createDriverViewModel.personalPic.Length > 0)
                                                            {
                                                                logofileName = string.Concat(Guid.NewGuid().ToString()/*.Take(8)*/) +
                                                                        (ContentDispositionHeaderValue.Parse(createDriverViewModel.personalPic.ContentDisposition)
                                                                        .FileName.Trim('"').Substring(
                                                                            ContentDispositionHeaderValue.Parse(createDriverViewModel.personalPic.ContentDisposition)
                                                                            .FileName.Trim('"').IndexOf(".")));
                                                                var logofullPath = Path.Combine(ImagePath, logofileName);
                                                                using (var stream = new FileStream(logofullPath, FileMode.Create))
                                                                {
                                                                    createDriverViewModel.personalPic.CopyTo(stream);
                                                                }
                                                            }
                                                        }
                                                        var lecince = string.Empty;
                                                        if (createDriverViewModel.licensePic != null)
                                                        {
                                                            if (createDriverViewModel.licensePic.Length > 0)
                                                            {
                                                                lecince = string.Concat(Guid.NewGuid().ToString()/*.Take(8)*/) +
                                                                        (ContentDispositionHeaderValue.Parse(createDriverViewModel.licensePic.ContentDisposition)
                                                                        .FileName.Trim('"').Substring(
                                                                            ContentDispositionHeaderValue.Parse(createDriverViewModel.licensePic.ContentDisposition)
                                                                            .FileName.Trim('"').IndexOf(".")));
                                                                var lecincefullPath = Path.Combine(ImagePath, lecince);
                                                                using (var stream = new FileStream(lecincefullPath, FileMode.Create))
                                                                {
                                                                    createDriverViewModel.licensePic.CopyTo(stream);
                                                                }
                                                            }
                                                        }
                                                        var carpic = string.Empty;
                                                        if (createDriverViewModel.carPic != null)
                                                        {
                                                            if (createDriverViewModel.carPic.Length > 0)
                                                            {
                                                                carpic = string.Concat(Guid.NewGuid().ToString()/*.Take(8)*/) +
                                                                        (ContentDispositionHeaderValue.Parse(createDriverViewModel.carPic.ContentDisposition)
                                                                        .FileName.Trim('"').Substring(
                                                                            ContentDispositionHeaderValue.Parse(createDriverViewModel.carPic.ContentDisposition)
                                                                            .FileName.Trim('"').IndexOf(".")));
                                                                var carpicfullPath = Path.Combine(ImagePath, carpic);
                                                                using (var stream = new FileStream(carpicfullPath, FileMode.Create))
                                                                {
                                                                    createDriverViewModel.carPic.CopyTo(stream);
                                                                }
                                                            }
                                                        }
                                                        var idpic = string.Empty;
                                                        if (createDriverViewModel.iDPic != null)
                                                        {
                                                            if (createDriverViewModel.iDPic.Length > 0)
                                                            {
                                                                idpic = string.Concat(Guid.NewGuid().ToString()/*.Take(8)*/) +
                                                                        (ContentDispositionHeaderValue.Parse(createDriverViewModel.iDPic.ContentDisposition)
                                                                        .FileName.Trim('"').Substring(
                                                                            ContentDispositionHeaderValue.Parse(createDriverViewModel.iDPic.ContentDisposition)
                                                                            .FileName.Trim('"').IndexOf(".")));
                                                                var cidpicfullPath = Path.Combine(ImagePath, idpic);
                                                                using (var stream = new FileStream(cidpicfullPath, FileMode.Create))
                                                                {
                                                                    createDriverViewModel.iDPic.CopyTo(stream);
                                                                }
                                                            }
                                                        }
                                                        var carlecince = string.Empty;
                                                        if (createDriverViewModel.carLicensePic != null)
                                                        {
                                                            if (createDriverViewModel.carLicensePic.Length > 0)
                                                            {
                                                                carlecince = string.Concat(Guid.NewGuid().ToString()/*.Take(8)*/) +
                                                                        (ContentDispositionHeaderValue.Parse(createDriverViewModel.carLicensePic.ContentDisposition)
                                                                        .FileName.Trim('"').Substring(
                                                                            ContentDispositionHeaderValue.Parse(createDriverViewModel.carLicensePic.ContentDisposition)
                                                                            .FileName.Trim('"').IndexOf(".")));
                                                                var cidpicfullPaths = Path.Combine(ImagePath, carlecince);
                                                                using (var stream = new FileStream(cidpicfullPaths, FileMode.Create))
                                                                {
                                                                    createDriverViewModel.carLicensePic.CopyTo(stream);
                                                                }
                                                            }
                                                        }
                                                        #endregion
                                                        var IsSuccess = _BlDriver.AddDriver(createDriverViewModel.driverData, user.Id, logofileName, lecince, carpic, idpic, carlecince);
                                                        if (IsSuccess)
                                                        {
                                                            #region Identity
                                                            var userRoles = await _userManager.GetRolesAsync(user);
                                                            var authClaims = new List<Claim> { new Claim(ClaimTypes.Name, user.UserName), new Claim(ClaimTypes.Sid, user.Id.ToString()), new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), };
                                                            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                                                            var Token = new JwtSecurityToken(
                                                            issuer: _configuration["JWT:ValidIssuer"],
                                                            audience: _configuration["JWT:ValidAudience"],
                                                            expires: _blGeneral.GetDateTimeNow().AddSeconds(20),
                                                            claims: authClaims,
                                                            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                                                            );
                                                            #region Token
                                                            #region Create JWT Token
                                                            var token = string.Empty;
                                                            _blTokens.AddTokens(user.Id, 1, token, (int)UserTypeEnum.Driver);
                                                            #endregion

                                                            #region Update JWT Token
                                                            _blTokens.UpdateUserJWTToken(user.Id, new JwtSecurityTokenHandler().WriteToken(Token));
                                                            #endregion
                                                            #endregion
                                                            #endregion
                                                            var SMSStatus = await _OTPService.ThankMessageAfterRegister(createDriverViewModel.driverData.mobileNo, (int)UserTypeEnum.Driver);
                                                            _Result.message = accLanguage == "ar" ? "تهانينا تم ارسال طلب اشتراكك بنجاح" : "congratulations, your request has been sent succfully";
                                                            _Result.status = true;
                                                            _Result.isAuthorize = true;
                                                            _Result.data = new
                                                            {
                                                            };
                                                            return Ok(_Result);
                                                        }

                                                        else
                                                        {
                                                            await _userManager.DeleteAsync(user);
                                                            _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Cant_Create : Api.Resources.Response.Cant_Create;
                                                            _Result.status = false;
                                                            _Result.isAuthorize = true;
                                                            return Ok(_Result);

                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        await _userManager.DeleteAsync(user);
                                                        _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Cant_Create + ex.Message.ToString() : Api.Resources.Response.Cant_Create + ex.Message.ToString();
                                                        _Result.status = false;
                                                        _Result.isAuthorize = true;
                                                        return Ok(_Result);
                                                    }
                                                }
                                                else
                                                {
                                                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Error_when_Create_User_Login_Data_Check_the_Email_And_Mobile_And_Try_Again : Api.Resources.Response.Error_when_Create_User_Login_Data_Check_the_Email_And_Mobile_And_Try_Again;
                                                    _Result.status = false;
                                                    _Result.isAuthorize = true;
                                                    return Ok(_Result);
                                                }
                                                #endregion
                                            }
                                            else
                                            {
                                                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Password_Not_Valid : Api.Resources.Response.Password_Not_Valid;
                                                _Result.status = false;
                                                _Result.isAuthorize = true;
                                                return Ok(_Result);
                                            }
                                        }
                                        else
                                        {
                                            _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Please_Insert_Password : Api.Resources.Response.Please_Insert_Password;
                                            _Result.status = false;
                                            _Result.isAuthorize = true;
                                            return Ok(_Result);
                                        }
                                    }
                                    else
                                    {
                                        _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Oops_this_IdNo_is_found_Before_to_a_driver_account : Api.Resources.Response.Oops_this_IdNo_is_found_Before_to_a_driver_account;
                                        _Result.status = false;
                                        _Result.isAuthorize = true;
                                        return Ok(_Result);
                                    }
                                }
                                else
                                {
                                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Mobile_No_Is_Exists : Api.Resources.Response.Mobile_No_Is_Exists;
                                    _Result.status = false;
                                    _Result.isAuthorize = true;
                                    return Ok(_Result);
                                }

                            }
                            else
                            {
                                _Result.message = accLanguage == "ar" ? Resources.Response_ar.NationaltIsreq : Resources.Response.NationaltIsreq;
                                _Result.status = false;
                                _Result.isAuthorize = true;
                                return Ok(_Result);
                            }

                        }
                        else
                        {
                            _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Name_not_Found : Api.Resources.Response.Name_not_Found;
                            _Result.status = false;
                            _Result.isAuthorize = true;
                            return Ok(_Result);
                        }

                    }
                    else
                    {
                        _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Mobile_No_not_Found : Api.Resources.Response.Mobile_No_not_Found;
                        _Result.status = false;
                        _Result.isAuthorize = true;
                        return Ok(_Result);
                    }

                }
                else
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.city_ID_not_Found : Api.Resources.Response.city_ID_not_Found;
                    _Result.status = false;
                    _Result.isAuthorize = true;
                    return Ok(_Result);

                }
            }
            catch (Exception ex)
            {
                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Error + Environment.NewLine + ex.Message.ToString() : Api.Resources.Response.Error + Environment.NewLine + ex.Message.ToString();
                _Result.status = false;
                _Result.isAuthorize = true;
                return Ok(_Result);
            }
        }
        #endregion
        #region Update Driver
        [CustomAuthorization]
        [HttpPost("UpdateDriver")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateDriver([FromForm] MyModelUpdate createVendorViewModel)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdDriver();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var CusotmerData = _BlDriver.GetDriverByUserId(UserId);
                int DriverID = 0;
                if (CusotmerData != null)
                {
                    DriverID = CusotmerData.DriversID;
                }
                if (DriverID != 0)
                {
                    if (createVendorViewModel.driverData.cityID != 0)
                    {
                        if (!string.IsNullOrWhiteSpace(createVendorViewModel.driverData.mobileNo))
                        {
                            if (!string.IsNullOrWhiteSpace(createVendorViewModel.driverData.name))
                            {
                                if (createVendorViewModel.driverData.nationaltyID != 0)
                                {
                                    int countryId = _blCity.GetById(createVendorViewModel.driverData.cityID, "Region").Region.CountryID;
                                    createVendorViewModel.driverData.mobileNo = MobileFormat(createVendorViewModel.driverData.mobileNo, countryId);
                                    if (!A_Valid_Phone_Number(createVendorViewModel.driverData.mobileNo, countryId))
                                    {
                                        _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Invalid_Phone_number : Api.Resources.Response.Invalid_Phone_number;
                                        _Result.status = false;
                                        _Result.isAuthorize = true;
                                        return Ok(_Result);
                                    }

                                    if (!_BlDriver.IsExistMobile(createVendorViewModel.driverData.mobileNo, DriverID))
                                    {
                                        if (!_BlDriver.IsExistIDNO(createVendorViewModel.driverData.iDNo, DriverID))
                                        {
                                            if (!string.IsNullOrWhiteSpace(createVendorViewModel.driverData.email))
                                            {
                                                if (_BlDriver.IsExistEmail(createVendorViewModel.driverData.email, DriverID))
                                                {
                                                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Email_Is_Exist : Api.Resources.Response.Email_Is_Exist;
                                                    _Result.status = false;
                                                    _Result.isAuthorize = true;
                                                    return Ok(_Result);
                                                }
                                            }
                                            #region Image
                                            string ImagePath = _configuration["DriverImageSave"];
                                            var logofileName = string.Empty;
                                            if (createVendorViewModel.personalPic != null)
                                            {
                                                if (createVendorViewModel.personalPic.Length > 0)
                                                {
                                                    logofileName = string.Concat(Guid.NewGuid().ToString()/*.Take(8)*/) +
                                                            (ContentDispositionHeaderValue.Parse(createVendorViewModel.personalPic.ContentDisposition)
                                                            .FileName.Trim('"').Substring(
                                                                ContentDispositionHeaderValue.Parse(createVendorViewModel.personalPic.ContentDisposition)
                                                                .FileName.Trim('"').IndexOf(".")));
                                                    var logofullPath = Path.Combine(ImagePath, logofileName);
                                                    using (var stream = new FileStream(logofullPath, FileMode.Create))
                                                    {
                                                        createVendorViewModel.personalPic.CopyTo(stream);
                                                    }
                                                }
                                            }
                                            var lecince = string.Empty;
                                            if (createVendorViewModel.licensePic != null)
                                            {
                                                if (createVendorViewModel.licensePic.Length > 0)
                                                {
                                                    lecince = string.Concat(Guid.NewGuid().ToString()/*.Take(8)*/) +
                                                            (ContentDispositionHeaderValue.Parse(createVendorViewModel.licensePic.ContentDisposition)
                                                            .FileName.Trim('"').Substring(
                                                                ContentDispositionHeaderValue.Parse(createVendorViewModel.licensePic.ContentDisposition)
                                                                .FileName.Trim('"').IndexOf(".")));
                                                    var lecincefullPath = Path.Combine(ImagePath, lecince);
                                                    using (var stream = new FileStream(lecincefullPath, FileMode.Create))
                                                    {
                                                        createVendorViewModel.licensePic.CopyTo(stream);
                                                    }
                                                }
                                            }
                                            var carpic = string.Empty;
                                            if (createVendorViewModel.carPic != null)
                                            {
                                                if (createVendorViewModel.carPic.Length > 0)
                                                {
                                                    carpic = string.Concat(Guid.NewGuid().ToString()/*.Take(8)*/) +
                                                            (ContentDispositionHeaderValue.Parse(createVendorViewModel.carPic.ContentDisposition)
                                                            .FileName.Trim('"').Substring(
                                                                ContentDispositionHeaderValue.Parse(createVendorViewModel.carPic.ContentDisposition)
                                                                .FileName.Trim('"').IndexOf(".")));
                                                    var carpicfullPath = Path.Combine(ImagePath, carpic);
                                                    using (var stream = new FileStream(carpicfullPath, FileMode.Create))
                                                    {
                                                        createVendorViewModel.carPic.CopyTo(stream);
                                                    }
                                                }
                                            }
                                            var idpic = string.Empty;
                                            if (createVendorViewModel.iDPic != null)
                                            {
                                                if (createVendorViewModel.iDPic.Length > 0)
                                                {
                                                    idpic = string.Concat(Guid.NewGuid().ToString()/*.Take(8)*/) +
                                                            (ContentDispositionHeaderValue.Parse(createVendorViewModel.iDPic.ContentDisposition)
                                                            .FileName.Trim('"').Substring(
                                                                ContentDispositionHeaderValue.Parse(createVendorViewModel.iDPic.ContentDisposition)
                                                                .FileName.Trim('"').IndexOf(".")));
                                                    var cidpicfullPath = Path.Combine(ImagePath, idpic);
                                                    using (var stream = new FileStream(cidpicfullPath, FileMode.Create))
                                                    {
                                                        createVendorViewModel.iDPic.CopyTo(stream);
                                                    }
                                                }
                                            }
                                            var carlecince = string.Empty;
                                            if (createVendorViewModel.carLicensePic != null)
                                            {
                                                if (createVendorViewModel.carLicensePic.Length > 0)
                                                {
                                                    carlecince = string.Concat(Guid.NewGuid().ToString()/*.Take(8)*/) +
                                                            (ContentDispositionHeaderValue.Parse(createVendorViewModel.carLicensePic.ContentDisposition)
                                                            .FileName.Trim('"').Substring(
                                                                ContentDispositionHeaderValue.Parse(createVendorViewModel.carLicensePic.ContentDisposition)
                                                                .FileName.Trim('"').IndexOf(".")));
                                                    var cidpicfullPaths = Path.Combine(ImagePath, carlecince);
                                                    using (var stream = new FileStream(cidpicfullPaths, FileMode.Create))
                                                    {
                                                        createVendorViewModel.carLicensePic.CopyTo(stream);
                                                    }
                                                }
                                            }
                                            #endregion

                                            #region User
                                            //Update User
                                            if (createVendorViewModel.driverData.iDNo != CusotmerData.IDNo)
                                            {
                                                if (_BlDriver.IsUserNameExists(createVendorViewModel.driverData.iDNo))
                                                {
                                                    var UserData = await _userManager.FindByIdAsync(CusotmerData.UserId.ToString());
                                                    UserData.UserName = createVendorViewModel.driverData.iDNo;
                                                    var identityResult = await _userManager.UpdateAsync(UserData);
                                                }
                                            }
                                            #endregion
                                            var IsSuccess = _BlDriver.UpdateDriverApi(createVendorViewModel.driverData, logofileName, DriverID, UserId, lecince, carpic, idpic, carlecince);
                                            if (IsSuccess)
                                            {
                                                var Driverdata = _BlDriver.GetDriverByPhoneNumber(createVendorViewModel.driverData.mobileNo, "City,Nationality");
                                                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Update_Success : Api.Resources.Response.Update_Success;
                                                _Result.status = true;
                                                _Result.isAuthorize = true;
                                                _Result.data = new
                                                {
                                                    name = accLanguage == "ar" ? Driverdata.NameAr : Driverdata.NameEn,
                                                    mobileNo = Driverdata.PhoneNumber,
                                                    email = Driverdata.Email,
                                                    gender = accLanguage == "ar" ? (Driverdata.Gender == (int)Gender.Male ? "Male" : Driverdata.Gender == (int)Gender.Female ? "Female" : "Prefer not to specify"): (Driverdata.Gender == (int)Gender.Male ? "ذكر" : Driverdata.Gender == (int)Gender.Female ? "انثى" : "تفضل عدم التحديد"),
                                                    personalPhoto = !string.IsNullOrWhiteSpace(Driverdata.PersonalPicture) ? _configuration["DriverImageView"] + Driverdata.PersonalPicture : "",
                                                    driversID = Driverdata.DriversID,
                                                    cityName = accLanguage == "ar" ? Driverdata.City.NameAR : Driverdata.City.NameEN,
                                                    nationalityName = accLanguage == "ar" ? Driverdata.Nationality.NameAR : Driverdata.Nationality.NameEN,
                                                    userId = Driverdata.UserId,
                                                    wallet = accLanguage == "ar" ? "حصلت علي " + _BlDriver.GetLastBlance(Driverdata.DriversID) + " ريـال" : "you Collected " + _BlDriver.GetLastBlance(Driverdata.DriversID) + " SR",
                                                    pendingPay = _BlDriver.GetAmountPaid(Driverdata.DriversID).UnPaid,
                                                    paid = _BlDriver.GetAmountPaid(Driverdata.DriversID).Paid,
                                                };
                                                return Ok(_Result);
                                            }
                                            else
                                            {
                                                var UserDataError = await _userManager.FindByIdAsync(CusotmerData.UserId.ToString());
                                                UserDataError.UserName = CusotmerData.IDNo;
                                                var identityResult = await _userManager.UpdateAsync(UserDataError);
                                                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Cant_Create : Api.Resources.Response.Cant_Create;
                                                _Result.status = false;
                                                _Result.isAuthorize = true;
                                                return Ok(_Result);
                                            }
                                        }
                                        else
                                        {
                                            _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Oops_this_IdNo_is_found_Before_to_a_driver_account : Api.Resources.Response.Oops_this_IdNo_is_found_Before_to_a_driver_account;
                                            _Result.status = false;
                                            _Result.isAuthorize = true;
                                            return Ok(_Result);
                                        }
                                    }
                                    else
                                    {
                                        _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Mobile_No_Is_Exists : Api.Resources.Response.Mobile_No_Is_Exists;
                                        _Result.status = false;
                                        _Result.isAuthorize = true;
                                        return Ok(_Result);
                                    }

                                }
                                else
                                {
                                    _Result.message = accLanguage == "ar" ? Resources.Response_ar.NationaltIsreq : Resources.Response.NationaltIsreq;
                                    _Result.status = false;
                                    _Result.isAuthorize = true;
                                    return Ok(_Result);
                                }

                            }
                            else
                            {
                                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Name_not_Found : Api.Resources.Response.Name_not_Found;
                                _Result.status = false;
                                _Result.isAuthorize = true;
                                return Ok(_Result);
                            }

                        }
                        else
                        {
                            _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Mobile_No_not_Found : Api.Resources.Response.Mobile_No_not_Found;
                            _Result.status = false;
                            _Result.isAuthorize = true;
                            return Ok(_Result);
                        }

                    }
                    else
                    {
                        _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.city_ID_not_Found : Api.Resources.Response.city_ID_not_Found;
                        _Result.status = false;
                        _Result.isAuthorize = true;
                        return Ok(_Result);

                    }
                }
                else
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Error_in_the_drivers_username : Api.Resources.Response.Error_in_the_drivers_username;
                    _Result.status = false;
                    _Result.isAuthorize = true;
                    return Ok(_Result);
                }
            }
            catch (Exception e)
            {
                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Error + e.ToString() : Api.Resources.Response.Error + e.ToString();
                _Result.status = false;
                _Result.isAuthorize = true;
                return Ok(_Result);
            }
        }
        #endregion
        #region Send Code
        [AllowAnonymous]
        [HttpPost("ResendOtpCode")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ResendOtpCode(string mobileNo, int countryId)
        {
            var accLanguage = GetUserLang();
            try
            {
                mobileNo = MobileFormat(mobileNo, countryId);
                if (!A_Valid_Phone_Number(mobileNo, countryId))
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Invalid_Phone_number : Api.Resources.Response.Invalid_Phone_number;
                    _Result.isAuthorize = true;
                    _Result.status = false;
                    return Ok(_Result);
                }
                else
                {
                    if (!_BlDriver.IsExistMobile(mobileNo))
                    {
                        var IsVisorExists = _BlDriver.GetDriverByPhoneNumber(mobileNo, "");
                        if (IsVisorExists == null)
                        {
                            var KeyCode = _blGeneral.RandomNumber(4);
                            var SMSStatus = await _OTPService.SendSMSForVerify(mobileNo, KeyCode, (int)UserTypeEnum.Driver, (int)MessageReasonEnum.Registration);
                            #region Send Otp code
                            _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.The_code_was_sent_successfully : Api.Resources.Response.The_code_was_sent_successfully;
                            _Result.isAuthorize = true;
                            _Result.status = true;
                            _Result.data = new
                            {
                                oTPCode = KeyCode,
                                oTPCodeStatus = true,
                            };
                            return Ok(_Result);
                            #endregion
                        }
                        else
                        {
                            _Result.message = accLanguage == "ar" ? "الحساب مسجل من قبل" : "this mobile is already registered";
                            _Result.isAuthorize = true;
                            _Result.status = false;
                            return Ok(_Result);
                        }
                    }
                    else
                    {
                        _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Password_Not_Valid : Api.Resources.Response.Password_Not_Valid;
                        _Result.status = false;
                        _Result.isAuthorize = true;
                        return Ok(_Result);
                    }
                }
            }
            catch (Exception ex)
            {
                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Oops_this_number_is_not_registered_Before_to_a_driver_account : Api.Resources.Response.Oops_this_number_is_not_registered_Before_to_a_driver_account;
                _Result.isAuthorize = true;
                _Result.status = false;
                return Ok(_Result);
            }
        }
        [AllowAnonymous]
        [HttpPost("SendOtpCode")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> SendOtpCode(string mobileNo, int countryId)
        {
            var accLanguage = GetUserLang();
            try
            {
                mobileNo = MobileFormat(mobileNo, countryId);
                if (!A_Valid_Phone_Number(mobileNo, countryId))
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Invalid_Phone_number : Api.Resources.Response.Invalid_Phone_number;
                    _Result.isAuthorize = true;
                    _Result.status = false;
                    return Ok(_Result);
                }
                else
                {
                    var IsVisorExists = _BlDriver.GetDriverByPhoneNumber(mobileNo, "");
                    if (IsVisorExists != null)
                    {
                        var KeyCode = _blGeneral.RandomNumber(4);
                        var SMSStatus = await _OTPService.SendSMSForVerify(mobileNo, KeyCode, (int)UserTypeEnum.Driver, (int)MessageReasonEnum.ForgetPassword);
                        #region Send Otp code
                        _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.The_code_was_sent_successfully : Api.Resources.Response.The_code_was_sent_successfully;
                        _Result.isAuthorize = true;
                        _Result.status = true;
                        _Result.data = new
                        {
                            oTPCode = KeyCode,
                            oTPCodeStatus = true,
                        };
                        return Ok(_Result);
                        #endregion
                    }
                    else
                    {
                        _Result.message = accLanguage == "ar" ? "الرقم غير مسجل من قبل لحساب سائق" : "It's not Driver mobile no";
                        _Result.isAuthorize = true;
                        _Result.status = false;
                        return Ok(_Result);
                    }
                }
            }
            catch (Exception ex)
            {
                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Oops_this_number_is_not_registered_Before_to_a_driver_account : Api.Resources.Response.Oops_this_number_is_not_registered_Before_to_a_driver_account;
                _Result.isAuthorize = true;
                _Result.status = false;
                return Ok(_Result);
            }
        }
        [HttpPost("SendRegisterCode")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> SendRegisterCode(string mobileNo, int countryId, string email, string password, string idNo)
        {
            var accLanguage = GetUserLang();
            try
            {
                mobileNo = MobileFormat(mobileNo, countryId);
                if (!A_Valid_Phone_Number(mobileNo, countryId))
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Invalid_Phone_number : Api.Resources.Response.Invalid_Phone_number;
                    _Result.isAuthorize = true;
                    _Result.status = false;
                    return Ok(_Result);
                }
                else
                {
                    if (!_BlDriver.IsExistIDNO(idNo))
                    {
                        if (!_BlDriver.IsExistMobile(mobileNo))
                        {
                            if (!string.IsNullOrWhiteSpace(email))
                            {
                                if (_BlDriver.IsExistEmail(email))
                                {
                                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Email_Is_Exist : Api.Resources.Response.Email_Is_Exist;
                                    _Result.status = false;
                                    _Result.isAuthorize = true;
                                    return Ok(_Result);
                                }
                            }
                            if (!string.IsNullOrWhiteSpace(password))
                            {
                                if (password.Length > 5)
                                {
                                    var IsVisorExists = _BlDriver.GetDriverByPhoneNumber(mobileNo, "");
                                    if (IsVisorExists == null)
                                    {
                                        var KeyCode = _blGeneral.RandomNumber(4);
                                        var SMSStatus = await _OTPService.SendSMSForVerify(mobileNo, KeyCode, (int)UserTypeEnum.Driver, (int)MessageReasonEnum.Registration);
                                        #region Send Otp code
                                        _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.The_code_was_sent_successfully : Api.Resources.Response.The_code_was_sent_successfully;
                                        _Result.isAuthorize = true;
                                        _Result.status = true;
                                        _Result.data = new
                                        {
                                            oTPCode = KeyCode,
                                            oTPCodeStatus = true,
                                        };
                                        return Ok(_Result);
                                        #endregion
                                    }
                                    else
                                    {
                                        _Result.message = accLanguage == "ar" ? "الرقم غير مسجل من قبل لحساب سائق" : "It's not Driver mobile no";
                                        _Result.isAuthorize = true;
                                        _Result.status = false;
                                        return Ok(_Result);
                                    }
                                }
                                else
                                {
                                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Password_Not_Valid : Api.Resources.Response.Password_Not_Valid;
                                    _Result.status = false;
                                    _Result.isAuthorize = true;
                                    return Ok(_Result);
                                }
                            }
                            else
                            {
                                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Password_Not_Valid : Api.Resources.Response.Password_Not_Valid;
                                _Result.status = false;
                                _Result.isAuthorize = true;
                                return Ok(_Result);
                            }
                        }
                        else
                        {
                            _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Password_Not_Valid : Api.Resources.Response.Password_Not_Valid;
                            _Result.status = false;
                            _Result.isAuthorize = true;
                            return Ok(_Result);
                        }
                    }
                    else
                    {
                        _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Oops_this_IdNo_is_found_Before_to_a_driver_account : Api.Resources.Response.Oops_this_IdNo_is_found_Before_to_a_driver_account;
                        _Result.status = false;
                        _Result.isAuthorize = true;
                        return Ok(_Result);
                    }
                }
            }
            catch (Exception ex)
            {
                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Oops_this_number_is_not_registered_Before_to_a_driver_account : Api.Resources.Response.Oops_this_number_is_not_registered_Before_to_a_driver_account;
                _Result.isAuthorize = true;
                _Result.status = false;
                return Ok(_Result);
            }
        }
        #endregion
        #region Update Password
        [HttpPost("ResetPassword")]
        [Consumes("application/x-www-form-urlencoded")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordViewModel resetPasswordViewModel)
        {
            var accLanguage = GetUserLang();
            try
            {
                var DriverData = _BlDriver.GetDriverByPhoneNumber(resetPasswordViewModel.mobileNo, "");
                if (DriverData != null)
                {
                    var UserData = await _userManager.FindByIdAsync(DriverData.UserId.ToString());
                    UserData.PasswordHash = _userManager.PasswordHasher.HashPassword(UserData, resetPasswordViewModel.password);
                    var identityResult = await _userManager.UpdateAsync(UserData);
                    if (identityResult.Succeeded)
                    {
                        _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Password_Updated_Successfully : Api.Resources.Response.Password_Updated_Successfully;
                        _Result.status = true;
                        _Result.isAuthorize = true;
                        _Result.data = new
                        {

                        };
                        return Ok(_Result);
                    }
                    else
                    {
                        _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Password_Updated_Failed : Api.Resources.Response.Password_Updated_Failed;
                        _Result.status = false;
                        _Result.isAuthorize = true;
                        return Ok(_Result);
                    }
                }
                else
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.This_Account_Not_Driver : Api.Resources.Response.This_Account_Not_Driver;
                    _Result.status = false;
                    _Result.isAuthorize = true;
                    return Ok(_Result);
                }
            }
            catch (Exception e)
            {
                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Error + e.ToString() : Api.Resources.Response.Error + e.ToString();
                _Result.status = false;
                _Result.isAuthorize = true;
                return Ok(_Result);
            }
        }
        [HttpPost("UpdatePassword")]
        [Consumes("application/x-www-form-urlencoded")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [CustomAuthorization]
        public async Task<IActionResult> UpdatePassword([FromForm] string oldPassword, [FromForm] string newPassword)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdDriver();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var VendorData = _BlDriver.GetDriverByUserId(UserId);
                if (VendorData != null)
                {

                    var UserData = await _userManager.FindByIdAsync(VendorData.UserId.ToString());
                    var checkoldpassword = await _userManager.CheckPasswordAsync(UserData, oldPassword);
                    if (checkoldpassword)
                    {
                        UserData.PasswordHash = _userManager.PasswordHasher.HashPassword(UserData, newPassword);
                        var identityResult = await _userManager.UpdateAsync(UserData);
                        if (identityResult.Succeeded)
                        {
                            _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Password_Updated_Successfully : Api.Resources.Response.Password_Updated_Successfully;
                            _Result.status = true;
                            _Result.isAuthorize = true;
                            _Result.data = new
                            {

                            };
                            return Ok(_Result);
                        }
                        else
                        {
                            _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Password_Updated_Failed : Api.Resources.Response.Password_Updated_Failed;
                            _Result.status = false;
                            _Result.isAuthorize = true;
                            return Ok(_Result);
                        }
                    }
                    else
                    {
                        _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Wrong_Old_password : Api.Resources.Response.Wrong_Old_password;
                        _Result.status = false;
                        _Result.isAuthorize = true;
                        return Ok(_Result);
                    }
                }
                else
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.This_Account_Not_Driver : Api.Resources.Response.This_Account_Not_Driver;
                    _Result.status = false;
                    _Result.isAuthorize = true;
                    return Ok(_Result);
                }
            }
            catch (Exception e)
            {
                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Error + e.ToString() : Api.Resources.Response.Error + e.ToString();
                _Result.status = false;
                _Result.isAuthorize = true;
                return Ok(_Result);
            }
        }
        #endregion
        #region Profile
        [CustomAuthorization]
        [HttpGet("DriverProfile")]
        public IActionResult DriverProfile()
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdDriver();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var DriverData = _BlDriver.GetDriverByUserId(UserId);
                int DriverId = 0;
                if (DriverData != null)
                {
                    DriverId = DriverData.DriversID;
                }
                if (DriverId != 0)
                {
                    var data = _BlDriver.GetDriversViewModelApi(DriverId, accLanguage, _configuration["DriverImageView"], UserId);
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                    _Result.status = true;
                    _Result.isAuthorize = true;
                    _Result.data = new { driverData = data };
                    return Ok(_Result);
                }
                else
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Error_in_the_drivers_username : Api.Resources.Response.Error_in_the_drivers_username;
                    _Result.status = false;
                    _Result.isAuthorize = true;
                    return Ok(_Result);
                }
            }
            catch (Exception e)
            {
                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Error + e.ToString() : Api.Resources.Response.Error + e.ToString();
                _Result.status = false;
                _Result.isAuthorize = true;
                return Ok(_Result);
            }
        }
        #endregion
        #region Helpers
        private bool A_Valid_Email(string email)
        {
            bool address;
            try
            {
                address = (new MailAddress(email)).Address == email;
            }
            catch
            {
                address = false;
            }
            return address;
        }
        private bool A_Valid_Phone_Number(string number, int countryId)
        {
            if (countryId == 2)
            {
                if (number.StartsWith("1") && number.Length == 10)
                {
                    return true;
                }
                if (number.StartsWith("01") && number.Length == 11)
                {
                    return true;
                }
                return false;
            }
            if (number.StartsWith("5") && number.Length == 9)
            {
                return true;
            }
            if (number.StartsWith("05") && number.Length == 10)
            {
                return true;
            }
            return false;
        }
        #region Get User Id Driver
        private string GetUserIdDriver()
        {
            //var identity = (ClaimsIdentity)User.Identity;
            //var userclime = identity.Claims.Count() > 0 ? identity.Claims.Where(e => e.Type.Contains("sid")).FirstOrDefault().Value.ToString() : "";
            try
            {
                var browserLang = Request.Headers["Authorization"].ToString().Split(";").FirstOrDefault()?.Split(",").FirstOrDefault();

                var headers = Request.Headers;

                if (headers.ContainsKey("Authorization"))
                {
                    string token = Request.Headers["Authorization"].FirstOrDefault().ToString().Replace("bearer ", "").Replace("Bearer ", "");

                    if (!string.IsNullOrWhiteSpace(token))
                    {
                        #region Update JWT Token
                        int JWTTokenValid = _blTokens.IsValidUserJWTTokenDriver(token);
                        #endregion
                        if (JWTTokenValid != 0)
                        {
                            return JWTTokenValid.ToString();
                        }
                        else // JWT Token Not Valid
                        {
                            return "incorrect Authorization Value";
                        }
                    }
                    else // JWT Token Not Valid
                    {
                        return "incorrect Authorization Value";
                    }
                }
                else
                {
                    return "incorrect Authorization Value";
                }
            }
            catch (Exception ex)
            {
                //_blErrorLog.Insert("API", "API", "Trip", "GetUserIdClient", ex.Message, ex.InnerException?.Message);
                return "incorrect Authorization Value";
            }
        }
        #endregion
        #region Get User Lang
        private string GetUserLang()
        {
            // In your action method.
            var languages = Request.GetTypedHeaders()
                                   .AcceptLanguage
                                   ?.OrderByDescending(x => x.Quality ?? 1) // Quality defines priority from 0 to 1, where 1 is the highest.
                                   .Select(x => x.Value.ToString())
                                   .ToArray() ?? Array.Empty<string>();

            var browserLang = Request.Headers["Accept-Language"].ToString().Split(";").FirstOrDefault()?.Split(",").FirstOrDefault();
            return languages.FirstOrDefault();
        }
        #endregion
        #region HashMD5
        private static string HashMD5(string text)
        {
            string result = string.Empty;
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(text));
                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                result = sBuilder.ToString();
            }
            return result;
        }
        #endregion
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
        #endregion
        #region Orders
        [CustomAuthorization]
        [HttpGet("DriverOrders")]
        public IActionResult DriverOrders(int page, int type)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdDriver();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var CustomerData = _BlDriver.GetDriverByUserId(UserId);
                int CustomerId = 0;
                if (CustomerData != null)
                {
                    CustomerId = CustomerData.DriversID;
                }
                if (CustomerId != 0)
                {
                    var NationalityList = _BlDriver.DriverOrdersHome(accLanguage, page, _configuration["VendorImageView"], CustomerId, type, null, _configuration["CustomerImageView"]);
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                    _Result.status = true;
                    _Result.isAuthorize = true;
                    _Result.data = NationalityList;
                    return Ok(_Result);
                }
                else
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Error_in_the_drivers_username : Api.Resources.Response.Error_in_the_drivers_username;
                    _Result.status = false;
                    _Result.isAuthorize = true;
                    return Ok(_Result);
                }
            }
            catch (Exception e)
            {
                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Error + e.ToString() : Api.Resources.Response.Error + e.ToString();
                _Result.status = false;
                _Result.isAuthorize = true;
                return Ok(_Result);
            }
        }
        [CustomAuthorization]
        [HttpGet("DriverHistory")]
        public IActionResult DriverHistory(int page, DateTime date)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdDriver();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var CustomerData = _BlDriver.GetDriverByUserId(UserId);
                int CustomerId = 0;
                if (CustomerData != null)
                {
                    CustomerId = CustomerData.DriversID;
                }
                if (CustomerId != 0)
                {
                    int type = 2;
                    var NationalityList = _BlDriver.DriverOrders(accLanguage, page, _configuration["VendorImageView"], CustomerId, type, date, _configuration["CustomerImageView"]);
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                    _Result.status = true;
                    _Result.isAuthorize = true;
                    _Result.data = NationalityList;
                    return Ok(_Result);
                }
                else
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Error_in_the_drivers_username : Api.Resources.Response.Error_in_the_drivers_username;
                    _Result.status = false;
                    _Result.isAuthorize = true;
                    return Ok(_Result);
                }
            }
            catch (Exception e)
            {
                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Error + e.ToString() : Api.Resources.Response.Error + e.ToString();
                _Result.status = false;
                _Result.isAuthorize = true;
                return Ok(_Result);
            }
        }
        [CustomAuthorization]
        [HttpPost("DeliveredOrder")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeliveredOrder(int orderId, double lat, double lng, int orderstatusId)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdDriver();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var DriverData = _BlDriver.GetDriverByUserId(UserId);
                int DriverId = 0;
                if (DriverData != null)
                {
                    DriverId = DriverData.DriversID;
                }
                if (DriverId != 0)
                {
                    string routeImagefileName = string.Empty;
                    var IsSuccess = _BlVendor.DeliveredChangeStatus(orderId, UserId, accLanguage, orderstatusId, DriverId, lat, lng, routeImagefileName, _configuration["FireBaseKey"]);
                    if (IsSuccess == "true")
                    {
                        _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Update_Success : Api.Resources.Response.Update_Success;
                        _Result.status = true;
                        _Result.isAuthorize = true;
                        _Result.data = new
                        {
                        };
                        return Ok(_Result);
                    }
                    else
                    {
                        _Result.message = IsSuccess;
                        _Result.status = false;
                        _Result.isAuthorize = true;
                        return Ok(_Result);
                    }
                }
                else
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Error_in_the_drivers_username : Api.Resources.Response.Error_in_the_drivers_username;
                    _Result.status = false;
                    _Result.isAuthorize = true;
                    return Ok(_Result);
                }
            }
            catch (Exception e)
            {
                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Error + e.ToString() : Api.Resources.Response.Error + e.ToString();
                _Result.status = false;
                _Result.isAuthorize = true;
                return Ok(_Result);
            }
        }
        #endregion
        #region Wallet
        [CustomAuthorization]
        [HttpGet("DriverWallet")]
        public IActionResult DriverWallet(int page, bool type, DateTime? date)
        {
            var accLanguage = GetUserLang();

            try
            {
                var UserID = GetUserIdDriver();
                if (UserID == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserID.ToString());
                var Driverobj = _BlDriver.GetDriverByUserId(UserId);
                int DriverID = 0;
                if (Driverobj != null)
                {
                    DriverID = Driverobj.DriversID;
                }
                if (DriverID != 0)
                {
                    if (page != 0)
                    {
                        var Responce = _BlDriver.GetFinancialbyDriver(DriverID, page, accLanguage, _configuration["VendorImageView"], type, date, _configuration["CustomerImageView"]);
                        _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                        _Result.status = true;
                        _Result.isAuthorize = true;
                        _Result.data = Responce;
                        return Ok(_Result);
                    }
                    else
                    {
                        _Result.message = accLanguage == "ar" ? "الصفحة غير موجودة" : "error in page";
                        _Result.status = false;
                        _Result.isAuthorize = true;
                        return Ok(_Result);
                    }
                }
                else
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
            }
            catch (System.Exception e)
            {
                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                _Result.status = false;
                _Result.isAuthorize = true;
                return Ok(_Result);
            }
        }
        #endregion
        #region Driver Help
        [HttpGet("HelpQuestionsList")]
        public IActionResult HelpQuestionsList(bool isOrder)
        {
            var accLanguage = GetUserLang();
            try
            {
                var HelpQuestionsList = _blHelpQuestions.GetAllHelpQuestionsViewModelApiByIsForTrip(isOrder, accLanguage, (int)HelpUserTypeEnum.Driver);
                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                _Result.status = true;
                _Result.isAuthorize = true;
                _Result.data = new { helpData = HelpQuestionsList };
                return Ok(_Result);
            }
            catch (Exception e)
            {
                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Error + e.ToString() : Api.Resources.Response.Error + e.ToString();
                _Result.status = false;
                _Result.isAuthorize = true;
                return Ok(_Result);
            }
        }
        [CustomAuthorization]
        [HttpPost("DriverHelp")]
        public async Task<IActionResult> DriverHelp(int helpQuestionsID, int? orderId, string descripe, IFormFile image)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdDriver();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var helpQuestions = _blHelpQuestions.GetById(helpQuestionsID);
                if (helpQuestions != null)
                {

                    var Driverobj = _BlDriver.GetDriverByUserId(UserId, "");
                    if (Driverobj != null)
                    {
                        #region Upload Images
                        string ImagePath = _configuration["HelpImageSave"];
                        string routeImagefileName = string.Empty;
                        if (image != null)
                        {
                            if (image.Length > 0)
                            {
                                routeImagefileName = string.Concat(Guid.NewGuid().ToString()) +
                                    (ContentDispositionHeaderValue.Parse(image.ContentDisposition).FileName.Trim('"').Substring(ContentDispositionHeaderValue.Parse(image.ContentDisposition).FileName.Trim('"').IndexOf(".")));
                                var IDPicturefullPath = Path.Combine(ImagePath, routeImagefileName);
                                using (var stream = new FileStream(IDPicturefullPath, FileMode.Create))
                                {
                                    image.CopyTo(stream);
                                }
                            }
                        }
                        #endregion
                        var IsSave = _BlDriverSupport.InsertDriversHelp(Driverobj.DriversID, helpQuestionsID, orderId, UserId, descripe, routeImagefileName);
                        if (IsSave)
                        {
                            _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Save_Sucsess : Api.Resources.Response.Save_Sucsess;
                            _Result.status = true;
                            _Result.isAuthorize = true;
                            _Result.data = new
                            {

                            };
                            return Ok(_Result);
                        }
                        else
                        {
                            _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Error_In_Save_Data : Api.Resources.Response.Error_In_Save_Data;
                            _Result.status = false;
                            _Result.isAuthorize = true;
                            return Ok(_Result);
                        }
                    }
                    else
                    {
                        _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.This_Account_Not_Driver : Api.Resources.Response.This_Account_Not_Driver;
                        _Result.status = false;
                        _Result.isAuthorize = true;
                        return Ok(_Result);
                    }

                }
                else
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Oops_Error_In_Data : Api.Resources.Response.Oops_Error_In_Data;
                    _Result.status = false;
                    _Result.isAuthorize = true;
                    return Ok(_Result);
                }

            }
            catch (Exception e)
            {
                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Error + e.ToString() : Api.Resources.Response.Error + e.ToString();
                _Result.status = false;
                _Result.isAuthorize = true;
                return Ok(_Result);
            }
        }
        [HttpPost("DriverOnline")]
        public async Task<IActionResult> DriverOnline(bool acccountStatus)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = "513";
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var VendorData = _BlDriver.GetDriverByUserId(UserId);
                if (VendorData != null)
                {
                    var IsSuccess = _BlDriver.UpdateOnline(VendorData.DriversID, UserId, acccountStatus);
                    if (IsSuccess)
                    {
                        _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Update_Success : Api.Resources.Response.Update_Success;
                        _Result.status = true;
                        _Result.isAuthorize = true;
                        _Result.data = new
                        {

                        };
                        return Ok(_Result);
                    }
                    else
                    {
                        _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Failed : Api.Resources.Response.Return_Data_Failed;
                        _Result.status = false;
                        _Result.isAuthorize = true;
                        return Ok(_Result);
                    }
                }
                else
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.This_Account_Not_Driver : Api.Resources.Response.This_Account_Not_Driver;
                    _Result.status = false;
                    _Result.isAuthorize = true;
                    return Ok(_Result);
                }
            }
            catch (Exception e)
            {
                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Error + e.ToString() : Api.Resources.Response.Error + e.ToString();
                _Result.status = false;
                _Result.isAuthorize = true;
                return Ok(_Result);
            }
        }
        #endregion
        #region STC_DirectPayment
        [CustomAuthorization]
        [HttpPost]
        public IActionResult VerifyStc(string mobileNoStc)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdDriver();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var Driverobj = _BlDriver.GetDriverByUserId(UserId, "City");
                if (Driverobj != null && !string.IsNullOrWhiteSpace(mobileNoStc))
                {
                    var IsSave = _BlDriver.UpdatePersonalStc(Driverobj.DriversID, UserId, mobileNoStc);
                    var driverobj = _BlDriver.GetById(Driverobj.DriversID);
                    if (IsSave)
                    {
                        _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Save_Sucsess : Api.Resources.Response.Save_Sucsess;
                        _Result.status = true;
                        _Result.isAuthorize = true;
                        _Result.data = new
                        {
                            driverName = accLanguage == "ar" ? driverobj.NameAr : driverobj.NameEn,
                            driverID = driverobj.DriversID,
                            mobileNo = driverobj.PhoneNumber,
                            email = driverobj.Email,
                            address = driverobj.Address,
                            birthDate = driverobj.BirthDate != null ? driverobj.BirthDate.Value.ToString("MM/dd/yyyy") : string.Empty,
                            personalPicture = _configuration["DriverImageView"] + driverobj.PersonalPicture,
                        };
                        return Ok(_Result);
                    }
                    else
                    {
                        _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Error_In_Save_Data : Api.Resources.Response.Error_In_Save_Data;
                        _Result.status = false;
                        _Result.isAuthorize = true;
                        return Ok(_Result);
                    }
                }
                else
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.This_Account_Not_Driver : Api.Resources.Response.This_Account_Not_Driver;
                    _Result.status = false;
                    _Result.isAuthorize = true;
                    return Ok(_Result);
                }
            }
            catch (System.Exception e)
            {
                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Error + e.ToString() : Api.Resources.Response.Error + e.ToString();
                _Result.status = false;
                _Result.isAuthorize = true;
                return Ok(_Result);
            }
        }
        #endregion
        #region STC_Pay
        public string GetSecretId()
        {
            try
            {
                var browserLang = Request.Headers["Authorization"].ToString().Split(";").FirstOrDefault()?.Split(",").FirstOrDefault();

                var headers = Request.Headers;

                if (headers.ContainsKey("Authorization"))
                {
                    string token = Request.Headers["Authorization"].FirstOrDefault().ToString().Replace("bearer ", "").Replace("Bearer ", "");

                    if (!string.IsNullOrWhiteSpace(token))
                    {
                        #region Update JWT Token
                        string SecertToken = _configuration["SecertToken"];
                        var JWTTokenValid = token == SecertToken ? true : false;
                        #endregion
                        if (JWTTokenValid)
                        {
                            return "true";
                        }
                        else // JWT Token Not Valid
                        {
                            return "incorrect Authorization Value";
                        }
                    }
                    else // JWT Token Not Valid
                    {
                        return "incorrect Authorization Value";
                    }
                }
                else
                {
                    return "incorrect Authorization Value";
                }
            }
            catch (Exception ex)
            {
                return "incorrect Authorization Value";
            }
        }

        [CustomAuthorization]
        [HttpPost("STCPay")]
        public IActionResult STCPay(DriverSTCPay model)
        {
            try
            {
                var UserLogin = GetSecretId();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                var issave = _BlDriverBalance.PayToDrivers(model.drivers, model.userId, _configuration["PaySTC"], _configuration["STCMID"], _configuration["InqyuirySTC"], _configuration["PathCert"]);
                _Result.message = "Paid To Drivers";
                _Result.status = true;
                _Result.isAuthorize = true;
                return Ok(_Result);
            }
            catch (Exception ex)
            {
                _Result.message = Api.Resources.Response.Oops_this_number_is_not_registered_Before_to_a_driver_account;
                _Result.status = false;
                _Result.isAuthorize = true;
                return Ok(_Result);
            }
        }
        [CustomAuthorization]
        [HttpPost("STCRePayLink")]
        public IActionResult STCRePayLink(TranactionList model)
        {
            try
            {
                var UserLogin = GetSecretId();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                var issave = _BlDriverBalance.RePayToDrivers(model.tranactions, model.userId, _configuration["STCMID"], _configuration["InqyuirySTC"], _configuration["PathCert"], _configuration["PaySTC"]);
                _Result.message = "Re Paid To Drivers";
                _Result.status = true;
                _Result.isAuthorize = true;
                return Ok(_Result);
            }
            catch (Exception ex)
            {
                _Result.message = Api.Resources.Response.Oops_this_number_is_not_registered_Before_to_a_driver_account;
                _Result.status = false;
                _Result.isAuthorize = true;
                return Ok(_Result);
            }
        }
        [CustomAuthorization]
        [HttpPost("InqyuirySTC")]
        public IActionResult InqyuirySTC()
        {
            try
            {
                var issave = _BlDriverBalance.AutoRepayDrivers(_configuration["STCMID"], _configuration["InqyuirySTC"], _configuration["PathCert"], _configuration["PaySTC"]);
                _Result.message = "InqyuirySTC To Drivers";
                _Result.status = true;
                _Result.isAuthorize = true;
                return Ok(_Result);
            }
            catch (Exception ex)
            {
                _Result.message = Api.Resources.Response.Oops_this_number_is_not_registered_Before_to_a_driver_account;
                _Result.status = false;
                _Result.isAuthorize = true;
                return Ok(_Result);
            }
        }
        #endregion
        #region Notifications
        [CustomAuthorization]
        [HttpGet("GetNotifications")]
        public IActionResult GetNotifications(int page)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdDriver();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var Driverobj = _BlDriver.GetDriverByUserId(UserId);
                if (Driverobj != null)
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                    _Result.status = true;
                    _Result.isAuthorize = true;
                    _Result.data = _BlDriver.GetNotificationListApiViewModelByDriver(Driverobj.DriversID, accLanguage, page);
                    return Ok(_Result);
                }
                else
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Error_in_the_drivers_username : Api.Resources.Response.Error_in_the_drivers_username;
                    _Result.status = false;
                    _Result.isAuthorize = true;
                    return Ok(_Result);
                }
            }
            catch (Exception e)
            {
                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Error + e.ToString() : Api.Resources.Response.Error + e.ToString();
                _Result.status = false;
                _Result.isAuthorize = true;
                return Ok(_Result);
            }
        }
        [CustomAuthorization]
        [HttpPost("SeenNotification")]
        public IActionResult SeenNotification(int notificationID)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdDriver();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var notification = _BlDriver.GetNotificationById(notificationID);
                if (notification != null)
                {
                    if (notification.IsSeen == false)
                    {
                        var IsSuccess = _BlDriver.UpdateNotificationSeen(notificationID, UserId);
                        if (IsSuccess)
                        {
                            _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Seen_Notification_Successfully : Api.Resources.Response.Seen_Notification_Successfully;
                            _Result.status = true;
                            _Result.isAuthorize = true;
                            _Result.data = new
                            {
                                notificationCount = _BlDriver.GetUserNotificationNotSeenCount(UserId),
                            };
                            return Ok(_Result);
                        }
                        else
                        {
                            _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Error_In_Seen_Notification : Api.Resources.Response.Error_In_Seen_Notification;
                            _Result.status = false;
                            _Result.isAuthorize = true;
                            return Ok(_Result);
                        }
                    }
                    else
                    {
                        _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Oops_Seen_Notification_before : Api.Resources.Response.Oops_Seen_Notification_before;
                        _Result.status = false;
                        _Result.isAuthorize = true;
                        return Ok(_Result);
                    }
                }
                else
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Error_In_Seen_Notification : Api.Resources.Response.Error_In_Seen_Notification;
                    _Result.status = false;
                    _Result.isAuthorize = true;
                    return Ok(_Result);
                }

            }
            catch (Exception e)
            {
                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Error + e.ToString() : Api.Resources.Response.Error + e.ToString();
                _Result.status = false;
                _Result.isAuthorize = true;
                return Ok(_Result);
            }
        }
        [CustomAuthorization]
        [HttpPost("SeenAllNotification")]
        public IActionResult SeenAllNotification()
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdDriver();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var IsSuccess = _BlDriver.UpdateNotificationSeen(UserId);
                if (IsSuccess)
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Seen_Notification_Successfully : Api.Resources.Response.Seen_Notification_Successfully;
                    _Result.status = true;
                    _Result.isAuthorize = true;
                    _Result.data = new
                    {
                        notificationCount = _BlVendor.GetUserNotificationNotSeenCount(UserId),
                    };
                    return Ok(_Result);
                }
                else
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Error_In_Seen_Notification : Api.Resources.Response.Error_In_Seen_Notification;
                    _Result.status = false;
                    _Result.isAuthorize = true;
                    return Ok(_Result);
                }

            }
            catch (Exception e)
            {
                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Error + e.ToString() : Api.Resources.Response.Error + e.ToString();
                _Result.status = false;
                _Result.isAuthorize = true;
                return Ok(_Result);
            }
        }
        #endregion
    }
}
