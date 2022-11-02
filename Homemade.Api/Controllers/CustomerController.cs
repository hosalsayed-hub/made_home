using Homemade.Api.Authentication;
using Homemade.Api.Model;
using Homemade.Api.Models;
using Homemade.BLL;
using Homemade.BLL.Customer;
using Homemade.BLL.General;
using Homemade.BLL.Order;
using Homemade.BLL.Purchase;
using Homemade.BLL.Setting;
using Homemade.BLL.SMS;
using Homemade.BLL.Vendor;
using Homemade.BLL.ViewModel.Customer;
using Homemade.BLL.ViewModel.Driver;
using Homemade.BLL.ViewModel.MyFatoora.FatoraPayment;
using Homemade.BLL.ViewModel.Order;
using Homemade.BLL.ViewModel.Setting;
using Homemade.BLL.ViewModel.Site;
using Homemade.BLL.ViewModel.Vendor;
using Homemade.Model;
using Homemade.Model.BankTransaction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        #region Declarations
        private readonly BLGeneral _blGeneral;
        private UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private FixedResultMessage _Result = new FixedResultMessage();
        private readonly BlCustomer _blCustomer;
        private readonly BlCity _blCity;
        private readonly OTPService _OTPService;
        private readonly BlProduct _BlProduct;
        private readonly BlTokens _blTokens;
        private readonly BlCustomerAddress _BlCustomerAddress;
        private readonly BlVendor _BlVendor;
        private readonly BLTransaction _BLTransaction;
        private readonly BlNationality _blNationality;
        private readonly BlAddressTypes _blAddressTypes;
        private readonly BlBlock _blBlock;
        private readonly BlOrders _BlOrders;
        private readonly BLUser _bLUser;
        private readonly BlCompanyWorkingHours _blCompanyWorkingHours;
        public CustomerController(BLTransaction bLTransaction, BlCustomerAddress BlCustomerAddress, BLGeneral blGeneral, UserManager<User> userManager, IConfiguration configuration, BlCustomer blCustomer, BlCity blCity, OTPService oTPService, BlTokens blTokens, BlProduct BlProduct, BlVendor BlVendor, BlNationality blNationality, BlAddressTypes blAddressTypes, BlBlock blBlock, BlOrders blOrders, BLUser bLUser, BlCompanyWorkingHours blCompanyWorkingHours)
        {
            _blGeneral = blGeneral;
            _userManager = userManager;
            _configuration = configuration;
            _blCustomer = blCustomer;
            _blCity = blCity;
            _OTPService = oTPService;
            _BlProduct = BlProduct;
            _blTokens = blTokens;
            _BlCustomerAddress = BlCustomerAddress;
            _BlVendor = BlVendor;
            _BLTransaction = bLTransaction;
            _blNationality = blNationality;
            _blAddressTypes = blAddressTypes;
            _blBlock = blBlock;
            _BlOrders = blOrders;
            _bLUser = bLUser;
            _blCompanyWorkingHours = blCompanyWorkingHours;
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
                string userIdClient = GetUserIdClient();
                if (userIdClient != "incorrect Authorization Value")
                {
                    int num = int.Parse(userIdClient.ToString());
                    _blTokens.UpdateTokens(num, 0, string.Empty);
                    _blTokens.UpdateUserJWTToken(num, string.Empty);
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
        #region Register Customer
        /// <summary>
        /// Register New Customer
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST / RegisterCustomer
        ///     {
        ///       
        ///     }
        ///
        /// </remarks>
        /// <param name="registerCustomerViewModel"></param>
        /// <returns>
        ///  /// <response code="200">
        /// <remarks>
        /// Sample responce:
        /// {         
        ///  "message": "شكرا لتسجليكم، تم ارسال طلب الاشتراك بنجاح وسيتم التوصيل معكم في أقرب وقت.",
        /// "status": true,
        /// isAuthorize": true,
        /// "data": {
        ///  "firstName": "ali",
        ///  "seconedName": "ali",
        ///  "mobileNo": "581234555",
        ///  "email": "mmm5@mmm.com",
        ///  "gender": "Male",
        ///  "personalPhoto": null,
        ///  "customersID": 4,
        ///  "cityName": "الدمام",
        ///  "nationalityName": "الدمام",
        /// "userId": 9,
        ///  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiNTgxMjM0NTU1IiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvc2lkIjoiOSIsImp0aSI6IjM2ZGQyYzdlLTg3YjgtNDU4ZS1hMjRhLWNlYWM3YzEzYjYzYSIsImV4cCI6MTYzMTEzNDUxNywiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo0NDM5NCIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3Q6NDQzOTQifQ.bjC4tGJR2gLXtLI26TNZA71xMXuDtgAptpGTfb2itFw"
        /// }
        ///           }
        /// </remarks>
        /// </response>
        /// </returns>
        #region Class
        public class MyModelReg
        {
            [FromJson]
            public RegisterCustomerViewModel customerData { get; set; } // <-- JSON will be deserialized to this object
        }
        public class MyModelUpdate
        {
            [FromJson]
            public UpdateCustomerViewModel customerData { get; set; } // <-- JSON will be deserialized to this object
        }
        #endregion
        [HttpPost("RegisterCustomer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Obsolete]
        public async Task<IActionResult> RegisterCustomer([FromForm] MyModelReg createCustomerViewModel)
        {
            var accLanguage = GetUserLang();
            try
            {
                if (!string.IsNullOrWhiteSpace(createCustomerViewModel.customerData.mobileNo))
                {
                    if (!string.IsNullOrWhiteSpace(createCustomerViewModel.customerData.firstName))
                    {
                        createCustomerViewModel.customerData.cityID = _blCity.GetFirstCity().CityID;
                        createCustomerViewModel.customerData.gender = (int)Gender.Male;
                        createCustomerViewModel.customerData.nationaltyID = _blNationality.GetFirstNationality().NationalityID;
                        int countryId = _blCity.GetById(createCustomerViewModel.customerData.cityID, "Region").Region.CountryID;
                        createCustomerViewModel.customerData.mobileNo = MobileFormat(createCustomerViewModel.customerData.mobileNo, countryId);
                        if (!A_Valid_Phone_Number(createCustomerViewModel.customerData.mobileNo, countryId))
                        {
                            _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Invalid_Phone_number : Api.Resources.Response.Invalid_Phone_number;
                            _Result.status = false;
                            _Result.isAuthorize = true;
                            return Ok(_Result);
                        }

                        if (!_blCustomer.IsExistMobile(createCustomerViewModel.customerData.mobileNo))
                        {
                            if (!string.IsNullOrWhiteSpace(createCustomerViewModel.customerData.email))
                            {
                                if (_blCustomer.IsExistEmail(createCustomerViewModel.customerData.email))
                                {
                                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Email_Is_Exist : Api.Resources.Response.Email_Is_Exist;
                                    _Result.status = false;
                                    _Result.isAuthorize = true;
                                    return Ok(_Result);
                                }
                            }
                            if (!string.IsNullOrWhiteSpace(createCustomerViewModel.customerData.password))
                            {
                                if (createCustomerViewModel.customerData.password.Length > 5)
                                {
                                    #region add Customer
                                    #region User
                                    //Insert User
                                    var user = new User
                                    {
                                        UserName = _blGeneral.GenerateToken(200),
                                        Email = _blGeneral.RandomString(10) + "@made-home.com",
                                        PhoneNumber = _blGeneral.RandomNumber(10),
                                        UserType = (int)UserTypeEnum.Customer
                                    };
                                    var identityResult = await _userManager.CreateAsync(user, createCustomerViewModel.customerData.password);
                                    #endregion
                                    if (identityResult.Succeeded)
                                    {
                                        try
                                        {
                                            var IsSuccess = _blCustomer.AddCustomer(createCustomerViewModel.customerData, user.Id, string.Empty);
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
                                                _blTokens.AddTokens(user.Id, 1, token, (int)UserTypeEnum.Customer);
                                                #endregion
                                                #region Update JWT Token
                                                _blTokens.UpdateUserJWTToken(user.Id, new JwtSecurityTokenHandler().WriteToken(Token));
                                                #endregion
                                                #endregion
                                                #endregion
                                                var customerdata = _blCustomer.GetCustomersByMobileNo(createCustomerViewModel.customerData.mobileNo, "City,Nationality");
                                                var SMSStatus = await _OTPService.ThankMessageAfterRegister(createCustomerViewModel.customerData.mobileNo, (int)UserTypeEnum.Customer);
                                                _Result.message = accLanguage == "ar" ? "تهانينا تم تفعيل حسابك بنجاح" : "congratulations, your account active succfully";
                                                _Result.status = true;
                                                _Result.isAuthorize = true;
                                                _Result.data = new
                                                {
                                                    firstName = accLanguage == "ar" ? customerdata.FirstName : customerdata.FirstName,
                                                    seconedName = accLanguage == "ar" ? customerdata.SeconedName : customerdata.SeconedName,
                                                    mobileNo = customerdata.MobileNo,
                                                    email = customerdata.Email,
                                                    gender = accLanguage == "ar" ? (customerdata.Gender == (int)Gender.Male ? "Male" : customerdata.Gender == (int)Gender.Female ? "Female" : "Prefer not to specify") : (customerdata.Gender == (int)Gender.Male ? "ذكر" : customerdata.Gender == (int)Gender.Female ? "انثى" : "تفضل عدم التحديد"),
                                                    personalPhoto = _configuration["CustomerImageView"] + customerdata.ProfilePic,
                                                    customersID = customerdata.CustomersID,
                                                    cityName = accLanguage == "ar" ? customerdata.City.NameAR : customerdata.City.NameEN,
                                                    nationalityName = accLanguage == "ar" ? customerdata.Nationality.NameAR : customerdata.Nationality.NameEN,
                                                    userId = user.Id,
                                                    token = new JwtSecurityTokenHandler().WriteToken(Token),
                                                    wallet = accLanguage == "ar" ? "لديك " + _blCustomer.GetLastBlance(customerdata.CustomersID) + " ريـال في المحفظة" : "you have " + _blCustomer.GetLastBlance(customerdata.CustomersID) + " SR in your wallet",
                                                    name = customerdata.FirstName + " " + customerdata.SeconedName,
                                                    notificationCount = _blCustomer.GetUserNotificationNotSeenCount(user.Id),
                                                    walletAmount = _blCustomer.GetLastBlance(customerdata.CustomersID)
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
                            _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Mobile_No_Is_Exists : Api.Resources.Response.Mobile_No_Is_Exists;
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
            catch (Exception ex)
            {
                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Error + Environment.NewLine + ex.Message.ToString() : Api.Resources.Response.Error + Environment.NewLine + ex.Message.ToString();
                _Result.status = false;
                _Result.isAuthorize = true;
                return Ok(_Result);
            }
        }
        [CustomAuthorization]
        [HttpPost("UpdateCustomer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateCustomer([FromForm] MyModelUpdate createVendorViewModel)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdClient();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var CusotmerData = _blCustomer.GetCustomerByUser(UserId);
                int CustomerID = 0;
                if (CusotmerData != null)
                {
                    CustomerID = CusotmerData.CustomersID;
                }
                if (CustomerID != 0)
                {
                    if (!string.IsNullOrWhiteSpace(createVendorViewModel.customerData.mobileNo))
                    {
                        if (!string.IsNullOrWhiteSpace(createVendorViewModel.customerData.firstName))
                        {
                            int countryId = _blCity.GetById(CusotmerData.CityID, "Region").Region.CountryID;
                            createVendorViewModel.customerData.mobileNo = MobileFormat(createVendorViewModel.customerData.mobileNo, countryId);
                            if (!A_Valid_Phone_Number(createVendorViewModel.customerData.mobileNo, countryId))
                            {
                                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Invalid_Phone_number : Api.Resources.Response.Invalid_Phone_number;
                                _Result.status = false;
                                _Result.isAuthorize = true;
                                return Ok(_Result);
                            }

                            if (!_blCustomer.IsExistMobile(createVendorViewModel.customerData.mobileNo, CustomerID))
                            {
                                if (!string.IsNullOrWhiteSpace(createVendorViewModel.customerData.email))
                                {
                                    if (_blCustomer.IsExistEmail(createVendorViewModel.customerData.email, CustomerID))
                                    {
                                        _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Email_Is_Exist : Api.Resources.Response.Email_Is_Exist;
                                        _Result.status = false;
                                        _Result.isAuthorize = true;
                                        return Ok(_Result);
                                    }
                                }
                                //if (!string.IsNullOrWhiteSpace(createVendorViewModel.customerData.password))
                                //{
                                //    if (createVendorViewModel.customerData.password.Length > 5)
                                //    {
                                //string ImagePath = _configuration["CustomerImageSave"];
                                var profilePhotofileName = string.Empty;
                                //if (createVendorViewModel.image != null)
                                //{
                                //    if (createVendorViewModel.image.Length > 0)
                                //    {
                                //        profilePhotofileName = string.Concat(Guid.NewGuid().ToString()/*.Take(8)*/) +
                                //                (ContentDispositionHeaderValue.Parse(createVendorViewModel.image.ContentDisposition)
                                //                .FileName.Trim('"').Substring(
                                //                    ContentDispositionHeaderValue.Parse(createVendorViewModel.image.ContentDisposition)
                                //                    .FileName.Trim('"').IndexOf(".")));
                                //        var profilePhotofullPath = Path.Combine(ImagePath, profilePhotofileName);
                                //        using (var stream = new FileStream(profilePhotofullPath, FileMode.Create))
                                //        {
                                //            createVendorViewModel.image.CopyTo(stream);
                                //        }
                                //    }
                                //}
                                #region User
                                //Update User
                                if (createVendorViewModel.customerData.mobileNo != CusotmerData.MobileNo)
                                {
                                    if (_blCustomer.IsUserNameExists(createVendorViewModel.customerData.mobileNo))
                                    {
                                        var UserData = await _userManager.FindByIdAsync(CusotmerData.UserId.ToString());
                                        UserData.UserName = createVendorViewModel.customerData.mobileNo;
                                        var identityResult = await _userManager.UpdateAsync(UserData);
                                    }
                                }
                                #endregion
                                var IsSuccess = _blCustomer.UpdateCustomerApi(createVendorViewModel.customerData, profilePhotofileName, CustomerID, UserId);
                                if (IsSuccess)
                                {
                                    var vendordata = _blCustomer.GetCustomersByMobileNo(createVendorViewModel.customerData.mobileNo, "City,Nationality");
                                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Update_Success : Api.Resources.Response.Update_Success;
                                    _Result.status = true;
                                    _Result.isAuthorize = true;
                                    _Result.data = new
                                    {
                                        firstName = accLanguage == "ar" ? vendordata.FirstName : vendordata.FirstName,
                                        seconedName = accLanguage == "ar" ? vendordata.SeconedName : vendordata.SeconedName,
                                        mobileNo = vendordata.MobileNo,
                                        email = vendordata.Email,
                                        gender = accLanguage == "ar" ? (vendordata.Gender == (int)Gender.Male ? "Male" : vendordata.Gender == (int)Gender.Female ? "Female" : "Prefer not to specify") : (vendordata.Gender == (int)Gender.Male ? "ذكر" : vendordata.Gender == (int)Gender.Female ? "انثى" : "تفضل عدم التحديد"),
                                        personalPhoto = _configuration["CustomerImageView"] + vendordata.ProfilePic,
                                        customersID = vendordata.CustomersID,
                                        cityName = accLanguage == "ar" ? vendordata.City.NameAR : vendordata.City.NameEN,
                                        nationalityName = accLanguage == "ar" ? vendordata.Nationality.NameAR : vendordata.Nationality.NameEN,
                                        wallet = accLanguage == "ar" ? "لديك " + _blCustomer.GetLastBlance(vendordata.CustomersID) + " ريـال في المحفظة" : "you have " + _blCustomer.GetLastBlance(vendordata.CustomersID) + " SR in your wallet",
                                        name = vendordata.FirstName + " " + vendordata.SeconedName,
                                        notificationCount = _blCustomer.GetUserNotificationNotSeenCount(vendordata.UserId),
                                        walletAmount = _blCustomer.GetLastBlance(vendordata.CustomersID)
                                    };
                                    return Ok(_Result);
                                }
                                else
                                {
                                    var UserDataError = await _userManager.FindByIdAsync(CusotmerData.UserId.ToString());
                                    UserDataError.UserName = CusotmerData.MobileNo;
                                    var identityResult = await _userManager.UpdateAsync(UserDataError);

                                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Cant_Create : Api.Resources.Response.Cant_Create;
                                    _Result.status = false;
                                    _Result.isAuthorize = true;
                                    return Ok(_Result);
                                }
                                //    }
                                //    else
                                //    {
                                //        _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Password_Not_Valid : Api.Resources.Response.Password_Not_Valid;
                                //        _Result.status = false;
                                //        _Result.isAuthorize = true;
                                //        return Ok(_Result);
                                //    }
                                //}
                                //else
                                //{
                                //    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Please_Insert_Password : Api.Resources.Response.Please_Insert_Password;
                                //    _Result.status = false;
                                //    _Result.isAuthorize = true;
                                //    return Ok(_Result);
                                //}
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
        #region Login Customer
        /// <summary>
        /// Login Customer
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST / LoginCustomer
        ///     {
        ///       "userName":"581234568",
        ///       "password":"12345678",
        ///       "token":"",
        ///       "deviceType":1,
        ///     }
        ///
        /// </remarks>
        /// <param name="loginModel"></param>
        /// <returns>
        /// <response code="200"> 
        /// <remarks>
        /// Sample responce:
        /// {         
        ///  "message": "تم تسجيل الدخول بنجاح",
        /// "status": true,
        /// isAuthorize": true,
        /// "data": {
        ///  "firstName": "ali",
        ///  "seconedName": "ali",
        ///  "mobileNo": "581234555",
        ///  "email": "mmm5@mmm.com",
        ///  "gender": "Male",
        ///  "personalPhoto": null,
        ///  "customersID": 4,
        ///  "cityName": "الدمام",
        ///  "nationalityName": "الدمام",
        /// "userId": 9,
        ///  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiNTgxMjM0NTU1IiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvc2lkIjoiOSIsImp0aSI6IjM2ZGQyYzdlLTg3YjgtNDU4ZS1hMjRhLWNlYWM3YzEzYjYzYSIsImV4cCI6MTYzMTEzNDUxNywiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo0NDM5NCIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3Q6NDQzOTQifQ.bjC4tGJR2gLXtLI26TNZA71xMXuDtgAptpGTfb2itFw"
        /// }
        ///           }
        /// </remarks>
        /// </response>
        /// </returns>
        [AllowAnonymous]
        [Consumes("application/x-www-form-urlencoded")]
        [HttpPost("LoginCustomer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> LoginCustomer([FromForm] LoginModel loginModel)
        {
            var accLanguage = GetUserLang();
            try
            {
                var customerdata = _blCustomer.GetCustomersByMobileNo(loginModel.userName, "City,Nationality");
                if (customerdata != null)
                {
                    if(customerdata.IsDeleted && customerdata?.DeleteDate.Value>=DateTime.Now.AddMonths(1))
                    {
                        _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Sorry_Deleted_Account : Api.Resources.Response.Sorry_Deleted_Account;
                        _Result.status = false;
                        _Result.isAuthorize = true;
                        return Ok(_Result);
                    }
                    var user = await _userManager.FindByIdAsync(customerdata.UserId.ToString());
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
                        #region Check Login Customer 
                        bool IsCodeActivation = false;
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
                            if (customerdata.IsEnable == Convert.ToBoolean(StatusEnable.Enable))
                            {
                                #region Token
                                _blTokens.AddTokens(user.Id, loginModel.deviceType, loginModel.token, (int)UserTypeEnum.Customer);
                                #endregion

                                #region Update JWT Token
                                _blTokens.UpdateUserJWTToken(user.Id, new JwtSecurityTokenHandler().WriteToken(Token));
                                #endregion

                                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Login_Successfuly : Api.Resources.Response.Login_Successfuly;
                                _Result.status = true;
                                _Result.isAuthorize = true;
                                _Result.data = new
                                {
                                    firstName = accLanguage == "ar" ? customerdata.FirstName : customerdata.FirstName,
                                    seconedName = accLanguage == "ar" ? customerdata.SeconedName : customerdata.SeconedName,
                                    mobileNo = customerdata.MobileNo,
                                    email = customerdata.Email,
                                    gender = accLanguage == "ar" ? (customerdata.Gender == (int)Gender.Male ? "Male" : customerdata.Gender == (int)Gender.Female ? "Female" : "Prefer not to specify")
                                    : (customerdata.Gender == (int)Gender.Male ? "ذكر" : customerdata.Gender == (int)Gender.Female ? "انثى" : "تفضل عدم التحديد"),
                                    personalPhoto = !string.IsNullOrWhiteSpace(customerdata.ProfilePic) ? _configuration["CustomerImageView"] + customerdata.ProfilePic : "",
                                    customersID = customerdata.CustomersID,
                                    cityName = accLanguage == "ar" ? customerdata.City.NameAR : customerdata.City.NameEN,
                                    nationalityName = accLanguage == "ar" ? customerdata.Nationality.NameAR : customerdata.Nationality.NameEN,
                                    userId = user.Id,
                                    token = new JwtSecurityTokenHandler().WriteToken(Token),
                                    wallet = accLanguage == "ar" ? "لديك " + _blCustomer.GetLastBlance(customerdata.CustomersID) + " ريـال في المحفظة" : "you have " + _blCustomer.GetLastBlance(customerdata.CustomersID) + " SR in your wallet",
                                    name = customerdata.FirstName + " " + customerdata.SeconedName,
                                    notificationCount = _blCustomer.GetUserNotificationNotSeenCount(user.Id),
                                    walletAmount = _blCustomer.GetLastBlance(customerdata.CustomersID)
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
                            //_Result.message = "نعتذر ، يمكنكم الدخول من خلال الموقع الإلكتروني ، نظرا لوجود بعض الصيانة في التطبيق" + Environment.NewLine + "https://made-home.com";
                            //_Result.status = false;
                            //_Result.isAuthorize = true;
                            //return Ok(_Result);
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
        #region Resend Otp Code
        /// <summary>
        /// Resend Otp Code
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST / Resend Otp Code
        ///     {
        ///       
        ///     }
        ///
        /// </remarks>
        /// <param name="mobileNo"></param>
        /// <param name="countryId"></param>
        ///  <returns>
        /// <response code="200"> 
        /// <remarks>
        /// Sample responce:
        /// {         
        /// "message": "The code was sent successfully",
        /// "status": true,
        /// "isAuthorize": true,
        /// "data": {
        /// "oTPCode": "2613",
        /// "oTPCodeStatus": true
        /// }
        ///           }
        /// </remarks>
        /// </response>
        /// </returns>
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
                    if (!_blCustomer.IsExistMobile(mobileNo))
                    {
                        var IsVisorExists = _blCustomer.GetCustomersByMobileNo(mobileNo, "");
                        if (IsVisorExists == null)
                        {
                            var KeyCode = _blGeneral.RandomNumber(4);
                            var SMSStatus = await _OTPService.SendSMSForVerify(mobileNo, KeyCode, (int)UserTypeEnum.Customer, (int)MessageReasonEnum.Update_Profile);
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
                        _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Mobile_No_Is_Exists : Api.Resources.Response.Mobile_No_Is_Exists;
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
                    var IsVisorExists = _blCustomer.GetCustomersByMobileNo(mobileNo, "");
                    if (IsVisorExists != null)
                    {
                        var KeyCode = _blGeneral.RandomNumber(4);
                        var SMSStatus = await _OTPService.SendSMSForVerify(mobileNo, KeyCode, (int)UserTypeEnum.Customer, (int)MessageReasonEnum.ForgetPassword);
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
                        _Result.message = accLanguage == "ar" ? "الرقم غير مسجل من قبل لحساب زبون" : "It's not customer mobile no";
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
        public async Task<IActionResult> SendRegisterCode(string mobileNo, int countryId, string email, string password)
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
                    if (!_blCustomer.IsExistMobile(mobileNo))
                    {
                        if (!string.IsNullOrWhiteSpace(email))
                        {
                            if (_blCustomer.IsExistEmail(email))
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
                                var IsVisorExists = _blCustomer.GetCustomersByMobileNo(mobileNo, "");
                                if (IsVisorExists == null)
                                {
                                    var KeyCode = _blGeneral.RandomNumber(4);
                                    var SMSStatus = await _OTPService.SendSMSForVerify(mobileNo, KeyCode, (int)UserTypeEnum.Customer, (int)MessageReasonEnum.Registration);
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
                                    _Result.message = accLanguage == "ar" ? "الرقم غير مسجل من قبل لحساب زبون" : "It's not customer mobile no";
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
                        _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Mobile_No_Is_Exists : Api.Resources.Response.Mobile_No_Is_Exists;
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
        #region Reset Password
        /// <summary>
        ///  Reset Password
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST / Reset Password
        ///     {
        ///       
        ///     }
        ///
        /// </remarks>
        /// <param name="resetPasswordViewModel"></param>
        ///  <returns>
        /// <response code="200"> 
        /// <remarks>
        /// Sample responce:
        /// {
        ///  "message": "Save Sucsess",
        ///    "status": true,
        ///    "isAuthorize": true,
        ///   "data": {}
        /// }
        /// </remarks>
        /// </response>
        /// </returns>
        [HttpPost("ResetPassword")]
        [Consumes("application/x-www-form-urlencoded")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordViewModel resetPasswordViewModel)
        {
            var accLanguage = GetUserLang();
            try
            {
                var customerData = _blCustomer.GetCustomersByMobileNo(resetPasswordViewModel.mobileNo, "");
                if (customerData != null)
                {
                    var UserData = await _userManager.FindByIdAsync(customerData.UserId.ToString());
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
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.This_Account_Not_Customer : Api.Resources.Response.This_Account_Not_Customer;
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
        #region Get User Id Client
        private string GetUserIdClient()
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
                        int JWTTokenValid = _blTokens.IsValidUserJWTTokenCustomer(token);
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
                return "incorrect Authorization Value";
            }
        }
        private string GetUserTokenClient()
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
                        return token.ToString();
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
        #endregion
        #region Get User Lang
        private string GetUserLang()
        {
            var languages = Request.GetTypedHeaders()
                                   .AcceptLanguage
                                   ?.OrderByDescending(x => x.Quality ?? 1) // Quality defines priority from 0 to 1, where 1 is the highest.
                                   .Select(x => x.Value.ToString())
                                   .ToArray() ?? Array.Empty<string>();

            var browserLang = Request.Headers["Accept-Language"].ToString().Split(";").FirstOrDefault()?.Split(",").FirstOrDefault();
            return languages.FirstOrDefault();
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
        #region Customer-Homepage
        [AllowAnonymous]
        [HttpGet("CustomerHome")]
        public IActionResult CustomerHome(int page)
        {
            var accLanguage = GetUserLang();
            try
            {
                var NationalityList = _BlProduct.GetCustomerHome(accLanguage, page, _configuration["SliderImageView"], _configuration["DeptImageView"], _configuration["ProductImageView"], _configuration["VendorImageView"]);
                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                _Result.status = true;
                _Result.isAuthorize = true;
                _Result.data = NationalityList;
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
        [AllowAnonymous]
        [HttpGet("ProductDetails")]
        public IActionResult ProductDetails(int productID)
        {
            var accLanguage = GetUserLang();
            try
            {
                var NationalityList = _BlProduct.GetProductDetailsApi(productID, accLanguage, _configuration["ProductImageView"], _configuration["VendorImageView"]);
                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                _Result.status = true;
                _Result.isAuthorize = true;
                _Result.data = NationalityList;
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
        [AllowAnonymous]
        [HttpPost("CustomerFavourite")]
        public IActionResult CustomerFavourite(int productID)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdClient();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var CustomerData = _blCustomer.GetCustomerByUser(UserId);
                if (CustomerData != null)
                {
                    var NationalityList = _blCustomer.SetCustomerFavourite(productID, CustomerData.CustomersID, UserId);
                    if (NationalityList)
                    {
                        _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                        _Result.status = true;
                        _Result.isAuthorize = true;
                        _Result.data = new { };
                        return Ok(_Result);
                    }
                    else
                    {
                        _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                        _Result.status = false;
                        _Result.isAuthorize = false;
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
            catch (Exception e)
            {
                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Error + e.ToString() : Api.Resources.Response.Error + e.ToString();
                _Result.status = false;
                _Result.isAuthorize = true;
                return Ok(_Result);
            }
        }
        [AllowAnonymous]
        [HttpGet("DeptProducts")]
        public IActionResult DeptProducts(int departmentID, int page, string search, string deptList, decimal? from, decimal? to)
        {
            var accLanguage = GetUserLang();
            try
            {
                var NationalityList = _BlProduct.GetProductListWithDept(departmentID, _configuration["ProductImageView"], accLanguage, page, search, _configuration["VendorImageView"],deptList, from, to);
                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                _Result.status = true;
                _Result.isAuthorize = true;
                _Result.data = NationalityList;
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
        [AllowAnonymous]
        [HttpGet("AllProducts")]
        public IActionResult AllProducts(int pagePopular, int pageProduct, string search, string deptList, decimal? from, decimal? to)
        {
            var accLanguage = GetUserLang();
            try
            {
                var NationalityList = _BlProduct.GetAllProducts(pagePopular, pageProduct, search, accLanguage, _configuration["ProductImageView"], _configuration["VendorImageView"],
                    deptList, from, to);
                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                _Result.status = true;
                _Result.isAuthorize = true;
                _Result.data = NationalityList;
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
        #endregion
        #region UpdatePass
        [HttpPost("UpdatePassword")]
        [Consumes("application/x-www-form-urlencoded")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [CustomAuthorization]
        public async Task<IActionResult> UpdatePassword([FromForm] string oldPassword, [FromForm] string newPassword)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdClient();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var VendorData = _blCustomer.GetCustomerByUser(UserId);
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
        #region CustomerProfile
        [HttpPost("UpdateLocation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult UpdateLocation(LocationDetails model)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdClient();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var CustomerData = _blCustomer.GetCustomerByUser(UserId);
                if (CustomerData != null)
                {
                    #region Update Vendor
                    var IsSuccess = _blCustomer.UpdateLocation(model, UserId, CustomerData.CustomersID);
                    if (IsSuccess)
                    {
                        _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Save_Sucsess : Api.Resources.Response.Save_Sucsess;
                        _Result.status = true;
                        _Result.isAuthorize = true;
                        _Result.data = new { };
                        return Ok(_Result);
                    }
                    else
                    {
                        _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Cant_Create : Api.Resources.Response.Cant_Create;
                        _Result.status = false;
                        _Result.isAuthorize = true;
                        return Ok(_Result);

                    }
                    #endregion
                }
                else
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
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
        [CustomAuthorization]
        [HttpGet("CustomerProfile")]
        public IActionResult CustomerProfile()
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdClient();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var CustomerData = _blCustomer.GetCustomerByUser(UserId);
                int CustomerId = 0;
                if (CustomerData != null)
                {
                    CustomerId = CustomerData.CustomersID;
                }
                if (CustomerId != 0)
                {
                    var data = _blCustomer.GetCustomersViewModelApi(CustomerId, accLanguage, _configuration["CustomerImageView"], UserId);
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                    _Result.status = true;
                    _Result.isAuthorize = true;
                    _Result.data = data;
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
        #region Addresses
        [CustomAuthorization]
        [HttpGet("GetAllCustomerAddress")]
        public IActionResult AllCustomerAddress()
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdClient();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var CustomerData = _blCustomer.GetCustomerByUser(UserId);
                int CustomerId = 0;
                if (CustomerData != null)
                {
                    CustomerId = CustomerData.CustomersID;
                }
                if (CustomerId != 0)
                {
                    var data = _BlCustomerAddress.GetAllCutomerAddress(CustomerId, accLanguage);
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                    _Result.status = true;
                    _Result.isAuthorize = true;
                    _Result.data = new { addressdata = data };
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
        [HttpGet("GetAddressById")]
        public IActionResult AddressById(int addressId)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdClient();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var CustomerData = _blCustomer.GetCustomerByUser(UserId);
                int CustomerId = 0;
                if (CustomerData != null)
                {
                    CustomerId = CustomerData.CustomersID;
                }
                if (CustomerId != 0)
                {
                    var data = _BlCustomerAddress.GetOneCutomerAddress(addressId, accLanguage);
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                    _Result.status = true;
                    _Result.isAuthorize = true;
                    _Result.data = new { addressdata = data };
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
        [HttpGet("OrderCustomerAddress")]
        public IActionResult OrderCustomerAddress()
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdClient();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var CustomerData = _blCustomer.GetCustomerByUser(UserId);
                int CustomerId = 0;
                if (CustomerData != null)
                {
                    CustomerId = CustomerData.CustomersID;
                }
                if (CustomerId != 0)
                {
                    var data = _BlCustomerAddress.GetAllCutomerAddressOrder(CustomerId, accLanguage);
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                    _Result.status = true;
                    _Result.isAuthorize = true;
                    _Result.data = new { addressdata = data };
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
        [HttpPost("AddCustomerAddress")]
        public async Task<IActionResult> AddCustomerAddress(CustomerAddressViewModelApi model)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdClient();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var CustomerData = _blCustomer.GetCustomerByUser(UserId);
                int CustomerId = 0;
                if (CustomerData != null)
                {
                    CustomerId = CustomerData.CustomersID;
                }
                if (CustomerId != 0)
                {
                    if (model.cityID != 0)
                    {
                        if (model.blockID != 0)
                        {
                            var IsSuccess = _BlCustomerAddress.AddCustomerAddress(model, CustomerId, UserId);
                            if (IsSuccess)
                            {
                                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                                _Result.status = true;
                                _Result.isAuthorize = true;
                                _Result.data = new { };
                                return Ok(_Result);
                            }
                            else
                            {
                                _Result.message = accLanguage == "ar" ? "حدث خطأ في اضافه العنوان" : "Address Cannot addedd";
                                _Result.status = false;
                                _Result.isAuthorize = true;
                                return Ok(_Result);
                            }
                        }
                        else
                        {
                            _Result.message = accLanguage == "ar" ? "الحي مطلوب" : "Block Not Found";
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
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
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
        [CustomAuthorization]
        [HttpPost("UpdateCustomerAddress")]
        public async Task<IActionResult> UpdateCustomerAddress(UpdateCustomerLocation model)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdClient();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var CustomerData = _blCustomer.GetCustomerByUser(UserId);
                int CustomerId = 0;
                if (CustomerData != null)
                {
                    CustomerId = CustomerData.CustomersID;
                }
                if (CustomerId != 0)
                {
                    if (model.cityID != 0)
                    {
                        if (model.blockID != 0)
                        {
                            var IsSuccess = _BlCustomerAddress.UpdateCustomerAddress(model, CustomerId, UserId);
                            if (IsSuccess)
                            {
                                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                                _Result.status = true;
                                _Result.isAuthorize = true;
                                _Result.data = new { };
                                return Ok(_Result);
                            }
                            else
                            {
                                _Result.message = accLanguage == "ar" ? "حدث خطأ في اضافه العنوان" : "Address Cannot addedd";
                                _Result.status = false;
                                _Result.isAuthorize = true;
                                return Ok(_Result);
                            }
                        }
                        else
                        {
                            _Result.message = accLanguage == "ar" ? "الحي مطلوب" : "Block Not Found";
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
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
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
        [CustomAuthorization]
        [HttpPost("DeleteCustomerAddress")]
        public IActionResult DeleteCustomerAddress(int addressId)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdClient();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var CustomerData = _blCustomer.GetCustomerByUser(UserId);
                int CustomerId = 0;
                if (CustomerData != null)
                {
                    CustomerId = CustomerData.CustomersID;
                }
                if (CustomerId != 0)
                {
                    var data = _BlCustomerAddress.DeleteLocation(addressId, UserId);
                    _Result.message = accLanguage == "ar" ? "تم الحذف بنجاح" : "Deleted Successfully";
                    _Result.status = true;
                    _Result.isAuthorize = true;
                    _Result.data = new { };
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
        #region SendInquiry
        [CustomAuthorization]
        [HttpPost("SendInquiry")]
        public IActionResult SendInquiry(InquiryViewModelApi model)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdClient();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var CustomerData = _blCustomer.GetCustomerByUser(UserId);
                int CustomerId = 0;
                if (CustomerData != null)
                {
                    CustomerId = CustomerData.CustomersID;
                }
                if (CustomerId != 0)
                {
                    var data = _BlCustomerAddress.InsertInquiry(model, UserId);
                    _Result.message = accLanguage == "ar" ? "تم ارسال الاستفسار بنجاح" : "Send Inquiry Successfully";
                    _Result.status = true;
                    _Result.isAuthorize = true;
                    _Result.data = new { };
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
        #region Favourite
        [CustomAuthorization]
        [HttpGet("CustomerFavourite")]
        public IActionResult CustomerFavourite(int page, string search)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdClient();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var CustomerData = _blCustomer.GetCustomerByUser(UserId);
                int CustomerId = 0;
                if (CustomerData != null)
                {
                    CustomerId = CustomerData.CustomersID;
                }
                if (CustomerId != 0)
                {
                    var NationalityList = _BlProduct.GetProductListWithFavourite(_configuration["ProductImageView"], accLanguage, page, search, _configuration["VendorImageView"], CustomerId);
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
        #endregion
        #region Orders
        [CustomAuthorization]
        [HttpGet("CustomerOrders")]
        public IActionResult CustomerOrders(int page, int type)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdClient();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var CustomerData = _blCustomer.GetCustomerByUser(UserId);
                int CustomerId = 0;
                if (CustomerData != null)
                {
                    CustomerId = CustomerData.CustomersID;
                }
                if (CustomerId != 0)
                {
                    var NationalityList = _blCustomer.CustomerOrders(accLanguage, page, _configuration["VendorImageView"], CustomerId, type, _configuration["CustomerImageView"], _configuration["ProductImageView"]);
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
        #endregion
        #region Questions
        [CustomAuthorization]
        [HttpPost("AddQuestion")]
        public IActionResult AddQuestion(QuestionViewModelApi model)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdClient();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var CustomerData = _blCustomer.GetCustomerByUser(UserId);
                int CustomerId = 0;
                if (CustomerData != null)
                {
                    CustomerId = CustomerData.CustomersID;
                }
                if (CustomerId != 0)
                {
                    if (model.productId != 0)
                    {
                        if (!string.IsNullOrEmpty(model.question))
                        {
                            var NationalityList = _blCustomer.AddQuestionProd(model, CustomerId, UserId, _configuration["FireBaseKey"]);
                            _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Save_Sucsess : Api.Resources.Response.Save_Sucsess;
                            _Result.status = true;
                            _Result.isAuthorize = true;
                            _Result.data = new { };
                            return Ok(_Result);
                        }
                        else
                        {
                            _Result.message = accLanguage == "ar" ? "فضلا قم بإدخال السؤال" : "Question not found";
                            _Result.status = false;
                            _Result.isAuthorize = true;
                            return Ok(_Result);
                        }
                    }
                    else
                    {
                        _Result.message = accLanguage == "ar" ? "فضلا قم بتحديد المنتج" : "Product Id not found";
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
        [HttpGet("ProductQuestions")]
        public IActionResult ProductQuestions(int page, int productId)
        {
            var accLanguage = GetUserLang();
            try
            {
                var NationalityList = _blCustomer.ProdQuestions(accLanguage, page, _configuration["VendorImageView"], productId);
                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                _Result.status = true;
                _Result.isAuthorize = true;
                _Result.data = new { productQuestions = NationalityList };
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
        [HttpGet("CustomerQuestions")]
        public IActionResult CustomerQuestions(int page)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdClient();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var CustomerData = _blCustomer.GetCustomerByUser(UserId);
                int CustomerId = 0;
                if (CustomerData != null)
                {
                    CustomerId = CustomerData.CustomersID;
                }
                if (CustomerId != 0)
                {
                    var NationalityList = _blCustomer.CustomerQuestions(accLanguage, page, _configuration["VendorImageView"], _configuration["ProductImageView"], CustomerId);
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                    _Result.status = true;
                    _Result.isAuthorize = true;
                    _Result.data = new { productQuestions = NationalityList };
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
        #region Rates
        [CustomAuthorization]
        [HttpPost("AddRate")]
        public IActionResult AddRate(RateViewModelApi model)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdClient();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var CustomerData = _blCustomer.GetCustomerByUser(UserId);
                int CustomerId = 0;
                if (CustomerData != null)
                {
                    CustomerId = CustomerData.CustomersID;
                }
                if (CustomerId != 0)
                {
                    if (model.orderId != 0)
                    {
                        if (model.rateDelivery != 0 && model.rateOrder != 0)
                        {
                            var NationalityList = _blCustomer.AddRateProd(model, UserId, _configuration["FireBaseKey"]);
                            _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Save_Sucsess : Api.Resources.Response.Save_Sucsess;
                            _Result.status = true;
                            _Result.isAuthorize = true;
                            _Result.data = new { };
                            return Ok(_Result);
                        }
                        else
                        {
                            _Result.message = accLanguage == "ar" ? "فضلا قم بإدخال السؤال" : "Question not found";
                            _Result.status = false;
                            _Result.isAuthorize = true;
                            return Ok(_Result);
                        }
                    }
                    else
                    {
                        _Result.message = accLanguage == "ar" ? "فضلا قم بتحديد المنتج" : "Product Id not found";
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
        [CustomAuthorization]
        [HttpGet("ProductRates")]
        public IActionResult ProductRates(int page)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdClient();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var CustomerData = _blCustomer.GetCustomerByUser(UserId);
                int CustomerId = 0;
                if (CustomerData != null)
                {
                    CustomerId = CustomerData.CustomersID;
                }
                if (CustomerId != 0)
                {
                    var NationalityList = _blCustomer.NewProdRates(accLanguage, page, _configuration["VendorImageView"], CustomerId);
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
        #endregion
        #region Add New Order
        [CustomAuthorization]
        [HttpGet("CheckCode")]
        public IActionResult CheckCode(string code, decimal total, int count)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdClient();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var CustomerData = _blCustomer.GetCustomerByUser(UserId);
                int CustomerId = 0;
                if (CustomerData != null)
                {
                    CustomerId = CustomerData.CustomersID;
                }
                if (CustomerId != 0)
                {
                    if (total != 0)
                    {
                        var NationalityList = _blCustomer.CheckPromoCodeValid(code, CustomerId, total, accLanguage, count);
                        _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                        _Result.status = true;
                        _Result.isAuthorize = true;
                        _Result.data = new { productQuestions = NationalityList };
                        return Ok(_Result);
                    }
                    else
                    {
                        _Result.message = accLanguage == "ar" ? "الإجمالى غير موجود" : "Total Not Found";
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
            catch (Exception e)
            {
                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Error + e.ToString() : Api.Resources.Response.Error + e.ToString();
                _Result.status = false;
                _Result.isAuthorize = true;
                return Ok(_Result);
            }
        }
        [HttpGet("CheckCode_New")]
        public IActionResult CheckCode_New(string code, decimal total, string list_vendor, int address_id)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdClient();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var CustomerData = _blCustomer.GetCustomerByUser(UserId);
                int CustomerId = 0;
                if (CustomerData != null)
                {
                    CustomerId = CustomerData.CustomersID;
                }
                if (CustomerId != 0)
                {
                    if (total != 0)
                    {
                        var NationalityList = _blCustomer.CheckPromoCodeValid(code, CustomerId, total, accLanguage, list_vendor, address_id);
                        _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                        _Result.status = true;
                        _Result.isAuthorize = true;
                        _Result.data = new { productQuestions = NationalityList };
                        return Ok(_Result);
                    }
                    else
                    {
                        _Result.message = accLanguage == "ar" ? "الإجمالى غير موجود" : "Total Not Found";
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
            catch (Exception e)
            {
                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Error + e.ToString() : Api.Resources.Response.Error + e.ToString();
                _Result.status = false;
                _Result.isAuthorize = true;
                return Ok(_Result);
            }
        }
        [CustomAuthorization]
        [HttpPost("AddOrder")]
        public IActionResult AddOrder(AddOrderViewModelApi model)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdClient();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var CustomerData = _blCustomer.GetCustomerByUser(UserId);
                int CustomerId = 0;
                if (CustomerData != null)
                {
                    CustomerId = CustomerData.CustomersID;
                }
                if (CustomerId != 0)
                {
                    if (model.paymentTypeId != 0)
                    {
                        if (model.vendorOrder.Count() > 0)
                        {
                            if (model.vendorOrder.FirstOrDefault().products.Count() > 0)
                            {
                                if (model.addressID != 0)
                                {
                                    var NationalityList = _blCustomer.AddOrder(model, UserId, CustomerId, _configuration["FireBaseKey"], "MOB");
                                    if (NationalityList.message == "true")
                                    {
                                        _Result.message = accLanguage == "ar" ? "تم ارسال الطلب بنجاح" : "Order Send Successfully";
                                        _Result.status = true;
                                        _Result.isAuthorize = true;
                                        _Result.data = new { orderId = NationalityList.orderId };
                                        return Ok(_Result);
                                    }
                                    else
                                    {
                                        _Result.message = NationalityList.message;
                                        _Result.status = false;
                                        _Result.isAuthorize = true;
                                        _Result.data = new { };
                                        return Ok(_Result);
                                    }
                                }
                                else
                                {
                                    _Result.message = accLanguage == "ar" ? "فضلا قم بتحديد العنوان" : "Please choose address";
                                    _Result.status = false;
                                    _Result.isAuthorize = true;
                                    return Ok(_Result);
                                }
                            }
                            else
                            {
                                _Result.message = accLanguage == "ar" ? "فضلا قم بتحديد المنتجات" : "Please choose products";
                                _Result.status = false;
                                _Result.isAuthorize = true;
                                return Ok(_Result);
                            }
                        }
                        else
                        {
                            _Result.message = accLanguage == "ar" ? "فضلا قم بتحديد المنتجات" : "Please choose products";
                            _Result.status = false;
                            _Result.isAuthorize = true;
                            return Ok(_Result);
                        }
                    }
                    else
                    {
                        _Result.message = accLanguage == "ar" ? "فضلا قم بتحديد طريقة الدفع" : "Payment Type not found";
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
                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Error + e.InnerException.Message.ToString() : Api.Resources.Response.Error + e.InnerException.Message.ToString();
                _Result.status = false;
                _Result.isAuthorize = true;
                return Ok(_Result);
            }
        }
        [CustomAuthorization]
        [HttpPost("AddOrder_New")]
        public IActionResult AddOrder_New(AddOrderViewModelApiNew model)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdClient();
                var UserLoginToken = GetUserTokenClient();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var CustomerData = _blCustomer.GetCustomerByUser(UserId);
                int CustomerId = 0;
                if (CustomerData != null)
                {
                    CustomerId = CustomerData.CustomersID;
                }
                if (CustomerId != 0)
                {
                    if (model.paymentTypeId != 0)
                    {
                        if (model.vendorOrder.Count() > 0)
                        {
                            if (model.vendorOrder.FirstOrDefault().products.Count() > 0)
                            {
                                if (model.addressID != 0)
                                {
                                    string[] DateFormat = { "yyyy-MM-dd", "yyyy-MM-dd HH:mm", "yyyy-MM-dd HH:m", "yyyy-MM-dd H:mm", "yyyy-MM-dd H:m" };
                                    if (!string.IsNullOrEmpty(model.scheduleDate))
                                    {
                                        model.scheduleDate = DateTime.ParseExact(model.scheduleDate, DateFormat, System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
                                    }
                                    if (!string.IsNullOrEmpty(model.scheduleTime))
                                    {
                                        model.scheduleTime = DateTime.ParseExact(model.scheduleTime, DateFormat, System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy hh:mm tt");
                                    }
                                    var NationalityList = _blCustomer.AddOrder_New(model, UserId, CustomerId, _configuration["FireBaseKey"], "MOB", UserLoginToken, null, null);
                                    if (NationalityList.message == "true")
                                    {
                                        _Result.message = accLanguage == "ar" ? "تم ارسال الطلب بنجاح" : "Order Send Successfully";
                                        _Result.status = true;
                                        _Result.isAuthorize = true;
                                        _Result.data = new { orderId = NationalityList.orderId };
                                        return Ok(_Result);
                                    }
                                    else
                                    {
                                        _Result.message = NationalityList.message;
                                        _Result.status = false;
                                        _Result.isAuthorize = true;
                                        _Result.data = new { };
                                        return Ok(_Result);
                                    }
                                }
                                else
                                {
                                    _Result.message = accLanguage == "ar" ? "فضلا قم بتحديد العنوان" : "Please choose address";
                                    _Result.status = false;
                                    _Result.isAuthorize = true;
                                    return Ok(_Result);
                                }
                            }
                            else
                            {
                                _Result.message = accLanguage == "ar" ? "فضلا قم بتحديد المنتجات" : "Please choose products";
                                _Result.status = false;
                                _Result.isAuthorize = true;
                                return Ok(_Result);
                            }
                        }
                        else
                        {
                            _Result.message = accLanguage == "ar" ? "فضلا قم بتحديد المنتجات" : "Please choose products";
                            _Result.status = false;
                            _Result.isAuthorize = true;
                            return Ok(_Result);
                        }
                    }
                    else
                    {
                        _Result.message = accLanguage == "ar" ? "فضلا قم بتحديد طريقة الدفع" : "Payment Type not found";
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
                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Error + e.InnerException?.Message.ToString() : Api.Resources.Response.Error + e.InnerException?.Message.ToString();
                _Result.status = false;
                _Result.isAuthorize = true;
                return Ok(_Result);
            }
        }
        public string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz!@#$&()_×";
            var tok = new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
            return tok;
        }
        #region Order New Draft
        [CustomAuthorization]
        [HttpPost("AddOrder_New_Draft")]
        public IActionResult AddOrder_New_Draft(AddOrderViewModelApiNew model)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdClient();
                var UserLoginToken = GetUserTokenClient();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var CustomerData = _blCustomer.GetCustomerByUser(UserId);
                int CustomerId = 0;
                if (CustomerData != null)
                {
                    CustomerId = CustomerData.CustomersID;
                }
                if (CustomerId != 0)
                {
                    if (model.paymentTypeId != 0)
                    {
                        if (model.vendorOrder.Count() > 0)
                        {
                            if (model.vendorOrder.FirstOrDefault().products.Count() > 0)
                            {
                                if (model.addressID != 0)
                                {
                                    if (model.paymentTypeId == (int)PaymentTypeEnum.Wallet)
                                    {
                                        var NationalityList = _blCustomer.AddOrder_New(model, UserId, CustomerId, _configuration["FireBaseKey"], "MOB", UserLoginToken, null, null);
                                        if (NationalityList.message == "true")
                                        {
                                            _Result.message = accLanguage == "ar" ? "تم ارسال الطلب بنجاح" : "Order Send Successfully";
                                            _Result.status = true;
                                            _Result.isAuthorize = true;
                                            _Result.data = new { orderId = NationalityList.orderId };
                                            return Ok(_Result);
                                        }
                                        else
                                        {
                                            _Result.message = NationalityList.message;
                                            _Result.status = false;
                                            _Result.isAuthorize = true;
                                            _Result.data = new { };
                                            return Ok(_Result);
                                        }
                                    }
                                    else
                                    {
                                        string token = RandomString(50);
                                        model.customerReference = token;

                                        var NationalityList = _blCustomer.AddOrder_New_Drafts(model, UserId, CustomerId, _configuration["FireBaseKey"], "MOB", UserLoginToken, null, null);
                                        if (NationalityList.message == "true")
                                        {
                                            var paymentLog = (new TransactionCard()
                                            {
                                                Amount = (double)NationalityList.totalOrderValue,
                                                IsRedirect = false,
                                                IsSMSSentToUser = false,
                                                LastStatusUpdateFrom = (int)LastStatusUpdateFromEnum.Mobile,
                                                Message = "",
                                                PaymentID = string.Concat("order_", NationalityList.orderId),
                                                PaymentUrl = "",
                                                RequestDate = _blGeneral.GetDateTimeNow(),
                                                Status = "(Mobile)New",
                                                TransactionStatus = (int)TransactionEnum.SendPayment_Process_Begin,
                                                OrdersID = NationalityList.orderId,
                                                UpdateDate = null,
                                                CustomersID = CustomerId,
                                                CustomerReference = token

                                            });
                                            paymentLog.TransactionCardLog.Add(new TransactionCardLog()
                                            {
                                                TransactionID = paymentLog.TransactionID,
                                                OrdersID = NationalityList.orderId,
                                                DateAdded = _blGeneral.GetDateTimeNow(),
                                                LastStatusUpdateFrom = (int)LastStatusUpdateFromEnum.Mobile,
                                                Message = "",
                                                PaymentID = string.Concat("order_", NationalityList.orderId),
                                                Status = "(Mobile)New",
                                                TransactionStatus = (int)TransactionEnum.SendPayment_Process_Begin,
                                                CustomersID = CustomerId,
                                                CustomerReference = token

                                            });
                                            _BLTransaction.InsertTransactionCard(paymentLog);
                                            _Result.message = accLanguage == "ar" ? "تم ارسال الطلب بنجاح" : "Order Send Successfully";
                                            _Result.status = true;
                                            _Result.isAuthorize = true;
                                            _Result.data = new { orderId = NationalityList.orderId, CustomerReference = token };
                                            return Ok(_Result);
                                        }
                                        else
                                        {
                                            _Result.message = NationalityList.message;
                                            _Result.status = false;
                                            _Result.isAuthorize = true;
                                            _Result.data = new { };
                                            return Ok(_Result);
                                        }
                                    }
                                }
                                else
                                {
                                    _Result.message = accLanguage == "ar" ? "فضلا قم بتحديد العنوان" : "Please choose address";
                                    _Result.status = false;
                                    _Result.isAuthorize = true;
                                    return Ok(_Result);
                                }
                            }
                            else
                            {
                                _Result.message = accLanguage == "ar" ? "فضلا قم بتحديد المنتجات" : "Please choose products";
                                _Result.status = false;
                                _Result.isAuthorize = true;
                                return Ok(_Result);
                            }
                        }
                        else
                        {
                            _Result.message = accLanguage == "ar" ? "فضلا قم بتحديد المنتجات" : "Please choose products";
                            _Result.status = false;
                            _Result.isAuthorize = true;
                            return Ok(_Result);
                        }
                    }
                    else
                    {
                        _Result.message = accLanguage == "ar" ? "فضلا قم بتحديد طريقة الدفع" : "Payment Type not found";
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
                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Error + e.InnerException?.Message.ToString() : Api.Resources.Response.Error + e.InnerException?.Message.ToString();
                _Result.status = false;
                _Result.isAuthorize = true;
                return Ok(_Result);
            }
        }
        [HttpPost]
        [CustomAuthorization]
        [Route("UpdateTransaction")]
        public async Task<IActionResult> UpdateTransaction()
        {
            var accLanguage = GetUserLang();
            string json = "";
            try
            {
                var UserLogin = GetUserIdClient();
                using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
                {
                    json = await reader.ReadToEndAsync();
                }
                var deserializedDirectError = JsonConvert.DeserializeObject<RooterrorMobile>(json);
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var Driverobj = _blCustomer.GetCustomerByUser(UserId);
                if (Driverobj != null)
                {
                    var paymentLog = Get_TRANSACTION_CARD(deserializedDirectError.invoiceId.ToString(), deserializedDirectError.invoiceId.ToString(), deserializedDirectError.customerReference.ToString());
                    paymentLog.Status = deserializedDirectError.statusCode.ToString();
                    paymentLog.UpdateDate = _blGeneral.GetDateTimeNow();
                    paymentLog.LastStatusUpdateFrom = (int)LastStatusUpdateFromEnum.Mobile;
                    paymentLog.TransactionStatus = (int)TransactionEnum.Execute_Process_Failed;
                    paymentLog.TransactionCardLog.Add(new TransactionCardLog()
                    {
                        TransactionID = paymentLog.TransactionID,
                        CustomersID = Driverobj.CustomersID,
                        OrdersID = 0,
                        DateAdded = _blGeneral.GetDateTimeNow(),
                        LastStatusUpdateFrom = (int)LastStatusUpdateFromEnum.Mobile,
                        Message = deserializedDirectError.message,
                        CustomerReference = deserializedDirectError.customerReference,
                        InvoiceId = deserializedDirectError.invoiceId.ToString(),
                        Status = deserializedDirectError.statusCode.ToString(),
                        TransactionStatus = (int)TransactionEnum.Execute_Process_Completed,
                    });
                    paymentLog = _BLTransaction.UpdateTransactionCard(paymentLog);
                    _Result.message = deserializedDirectError.message;
                    _Result.status = false;
                    _Result.isAuthorize = true;
                    return Ok(_Result);
                }
                else
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Sorry_Not_Allow_Update : Api.Resources.Response.Sorry_Not_Allow_Update;
                    _Result.status = false;
                    _Result.isAuthorize = true;
                    return Ok(_Result);
                }
            }
            catch (System.Exception e)
            {
                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Error + e.Message.ToString() : Api.Resources.Response.Error + e.Message.ToString();
                _Result.status = false;
                _Result.isAuthorize = true;
                return Ok(_Result);
            }
        }
        public TransactionCard Get_TRANSACTION_CARD(string PaymentID, string InvoiceID, string CustomerReference)
        {
            return _BLTransaction.GetTransactionCard(PaymentID, InvoiceID, CustomerReference);
        }
        #endregion
        [CustomAuthorization]
        [HttpPost("ValidationOrder")]
        public IActionResult ValidationOrder(AddOrderViewModelApiNew model)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdClient();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var CustomerData = _blCustomer.GetCustomerByUser(UserId);
                int CustomerId = 0;
                if (CustomerData != null)
                {
                    CustomerId = CustomerData.CustomersID;
                }
                if (CustomerId != 0)
                {
                    if (model.paymentTypeId != 0)
                    {
                        if (model.vendorOrder.Count() > 0)
                        {
                            if (model.vendorOrder.FirstOrDefault().products.Count() > 0)
                            {
                                if (model.addressID != 0)
                                {
                                    var NationalityList = _blCustomer.ValidtaionOrder(model, UserId, CustomerId, _configuration["FireBaseKey"], "MOB");
                                    if (NationalityList.message == "true")
                                    {
                                        _Result.message = accLanguage == "ar" ? "تم ارسال الطلب بنجاح" : "Order Send Successfully";
                                        _Result.status = true;
                                        _Result.isAuthorize = true;
                                        _Result.data = new { orderId = NationalityList.orderId };
                                        return Ok(_Result);
                                    }
                                    else
                                    {
                                        _Result.message = NationalityList.message;
                                        _Result.status = false;
                                        _Result.isAuthorize = true;
                                        _Result.data = new { };
                                        return Ok(_Result);
                                    }
                                }
                                else
                                {
                                    _Result.message = accLanguage == "ar" ? "فضلا قم بتحديد العنوان" : "Please choose address";
                                    _Result.status = false;
                                    _Result.isAuthorize = true;
                                    return Ok(_Result);
                                }
                            }
                            else
                            {
                                _Result.message = accLanguage == "ar" ? "فضلا قم بتحديد المنتجات" : "Please choose products";
                                _Result.status = false;
                                _Result.isAuthorize = true;
                                return Ok(_Result);
                            }
                        }
                        else
                        {
                            _Result.message = accLanguage == "ar" ? "فضلا قم بتحديد المنتجات" : "Please choose products";
                            _Result.status = false;
                            _Result.isAuthorize = true;
                            return Ok(_Result);
                        }
                    }
                    else
                    {
                        _Result.message = accLanguage == "ar" ? "فضلا قم بتحديد طريقة الدفع" : "Payment Type not found";
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
                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Error + e.InnerException.Message.ToString() : Api.Resources.Response.Error + e.InnerException.Message.ToString();
                _Result.status = false;
                _Result.isAuthorize = true;
                return Ok(_Result);
            }
        }
        [CustomAuthorization]
        [HttpPost("CancelOrder")]
        public IActionResult CancelOrder(CancelOrderViewModelApi model)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdClient();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var CustomerData = _blCustomer.GetCustomerByUser(UserId);
                int CustomerId = 0;
                if (CustomerData != null)
                {
                    CustomerId = CustomerData.CustomersID;
                }
                if (CustomerId != 0)
                {
                    if (model.orderId != 0)
                    {
                        if (!string.IsNullOrWhiteSpace(model.reason))
                        {

                            var NationalityList = _blCustomer.CancelOrder(model, UserId, _configuration["FireBaseKey"]);
                            if (NationalityList == "true")
                            {
                                _Result.message = accLanguage == "ar" ? "تم الغاء الطلب بنجاح" : "Order Canceled Successfully";
                                _Result.status = true;
                                _Result.isAuthorize = true;
                                _Result.data = new { };
                                return Ok(_Result);
                            }
                            else
                            {
                                _Result.message = NationalityList;
                                _Result.status = false;
                                _Result.isAuthorize = true;
                                _Result.data = new { };
                                return Ok(_Result);
                            }
                        }
                        else
                        {
                            _Result.message = accLanguage == "ar" ? "فضلا قم بإدخال السبب" : "Please enter cancel reason";
                            _Result.status = false;
                            _Result.isAuthorize = true;
                            return Ok(_Result);
                        }
                    }
                    else
                    {
                        _Result.message = accLanguage == "ar" ? "فضلا قم بتحديد الطلب" : "Order Id not found";
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
                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Error + e.InnerException.Message.ToString() : Api.Resources.Response.Error + e.InnerException.Message.ToString();
                _Result.status = false;
                _Result.isAuthorize = true;
                return Ok(_Result);
            }
        }
        #endregion
        #region Notification
        [CustomAuthorization]
        [HttpGet("GetNotifications")]
        public IActionResult GetNotifications(int page)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdClient();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var Driverobj = _blCustomer.GetCustomerByUser(UserId);
                if (Driverobj != null)
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                    _Result.status = true;
                    _Result.isAuthorize = true;
                    _Result.data = _blCustomer.GetNotificationListApiViewModelByDriver(Driverobj.CustomersID, accLanguage, page);
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
                var UserLogin = GetUserIdClient();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var notification = _blCustomer.GetNotificationById(notificationID);
                if (notification != null)
                {
                    if (notification.IsSeen == false)
                    {
                        var IsSuccess = _blCustomer.UpdateNotificationSeen(notificationID, UserId);
                        if (IsSuccess)
                        {
                            _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Seen_Notification_Successfully : Api.Resources.Response.Seen_Notification_Successfully;
                            _Result.status = true;
                            _Result.isAuthorize = true;
                            _Result.data = new
                            {
                                notificationCount = _blCustomer.GetUserNotificationNotSeenCount(UserId),
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
                var UserLogin = GetUserIdClient();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var IsSuccess = _blCustomer.UpdateNotificationSeen(UserId);
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
        #region Add New Increase Order
        #region DeleteItemFromIncreaseOrder
        [CustomAuthorization]
        [HttpPost("DeleteItemFromIncreaseOrder")]
        public IActionResult DeleteItemFromIncreaseOrder(int productID, int orderId)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdClient();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var CustomerData = _blCustomer.GetCustomerByUser(UserId);
                int CustomerId = 0;
                if (CustomerData != null)
                {
                    CustomerId = CustomerData.CustomersID;
                }
                if (CustomerId != 0)
                {
                    var result = false;
                    int VendorsID = 0;
                    bool IsDeletedVendor = false;
                    var orderItems = _BlOrders.GetOrderItemsByProductAndOrderAndPending(productID, orderId, "OrderVendor");
                    if (orderItems != null)
                    {
                        VendorsID = orderItems.OrderVendor.VendorsID;
                        result = _BlOrders.DeleteItemFromPending(productID, orderId, out IsDeletedVendor);
                    }
                    if (result)
                    {
                        _Result.message = accLanguage == "ar" ? "تم الحذف بنجاح" : "Deleted Success";
                        _Result.status = true;
                        _Result.isAuthorize = true;
                        _Result.data = new { };
                        return Ok(_Result);
                    }
                    else
                    {
                        _Result.message = accLanguage == "ar" ? "خطأ فى الحذف" : "Sorry, wrong With Deleted";
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
        #region AddOrderFromIncrease
        [CustomAuthorization]
        [HttpPost("AddOrderFromIncrease")]
        public IActionResult AddOrderFromIncrease(AddOrderViewModelApiNewPending model)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdClient();
                var UserLoginToken = GetUserTokenClient();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var CustomerData = _blCustomer.GetCustomerByUser(UserId);
                int CustomerId = 0;
                if (CustomerData != null)
                {
                    CustomerId = CustomerData.CustomersID;
                }
                if (CustomerId != 0)
                {
                    if (model.paymentTypeId != 0)
                    {
                        if (model.vendorOrder.Count() > 0)
                        {
                            if (model.vendorOrder.FirstOrDefault().products.Count() > 0)
                            {
                                if (model.addressID != 0)
                                {
                                    var increaseModel = new AddOrderViewModelApiNew()
                                    {
                                        addressID = model.addressID,
                                        customerReference = model.customerReference,
                                        invoiceId = model.invoiceId,
                                        orderTypeId = model.orderTypeId,
                                        paymentTypeId = model.paymentTypeId,
                                        promoCodeID = model.promoCodeID,
                                        scheduleDate = model.scheduleDate,
                                        scheduleTime = model.scheduleTime,
                                        vendorOrder = model.vendorOrder,
                                    };
                                    if (model.paymentTypeId == (int)PaymentTypeEnum.Wallet)
                                    {
                                        var NationalityList = _blCustomer.ChangeOrder_New_Pending_To_Create(increaseModel, model.orderId, UserId, CustomerId, (int)OrderStatusEnum.Create);
                                        if (NationalityList.message == "true")
                                        {
                                            _Result.message = accLanguage == "ar" ? "تم ارسال الطلب بنجاح" : "Order Send Successfully";
                                            _Result.status = true;
                                            _Result.isAuthorize = true;
                                            _Result.data = new { orderId = NationalityList.orderId };
                                            return Ok(_Result);
                                        }
                                        else
                                        {
                                            _Result.message = NationalityList.message;
                                            _Result.status = false;
                                            _Result.isAuthorize = true;
                                            _Result.data = new { };
                                            return Ok(_Result);
                                        }
                                    }
                                    else
                                    {
                                        string token = RandomString(50);
                                        model.customerReference = token;
                                        var NationalityList = _blCustomer.ChangeOrder_New_Pending_To_Create(increaseModel, model.orderId, UserId, CustomerId, (int)OrderStatusEnum.Drafts);
                                        if (NationalityList.message == "true")
                                        {
                                            var paymentLog = (new TransactionCard()
                                            {
                                                Amount = (double)NationalityList.totalOrderValue,
                                                IsRedirect = false,
                                                IsSMSSentToUser = false,
                                                LastStatusUpdateFrom = (int)LastStatusUpdateFromEnum.Mobile,
                                                Message = "",
                                                PaymentID = string.Concat("order_", NationalityList.orderId),
                                                PaymentUrl = "",
                                                RequestDate = _blGeneral.GetDateTimeNow(),
                                                Status = "(Mobile)New",
                                                TransactionStatus = (int)TransactionEnum.SendPayment_Process_Begin,
                                                OrdersID = NationalityList.orderId,
                                                UpdateDate = null,
                                                CustomersID = CustomerId,
                                                CustomerReference = token

                                            });
                                            paymentLog.TransactionCardLog.Add(new TransactionCardLog()
                                            {
                                                TransactionID = paymentLog.TransactionID,
                                                OrdersID = NationalityList.orderId,
                                                DateAdded = _blGeneral.GetDateTimeNow(),
                                                LastStatusUpdateFrom = (int)LastStatusUpdateFromEnum.Mobile,
                                                Message = "",
                                                PaymentID = string.Concat("order_", NationalityList.orderId),
                                                Status = "(Mobile)New",
                                                TransactionStatus = (int)TransactionEnum.SendPayment_Process_Begin,
                                                CustomersID = CustomerId,
                                                CustomerReference = token

                                            });

                                            _BLTransaction.InsertTransactionCard(paymentLog);

                                            _Result.message = accLanguage == "ar" ? "تم ارسال الطلب بنجاح" : "Order Send Successfully";
                                            _Result.status = true;
                                            _Result.isAuthorize = true;
                                            _Result.data = new { orderId = NationalityList.orderId };
                                            return Ok(_Result);
                                        }
                                        else
                                        {
                                            _Result.message = NationalityList.message;
                                            _Result.status = false;
                                            _Result.isAuthorize = true;
                                            _Result.data = new { };
                                            return Ok(_Result);
                                        }
                                    }
                                }
                                else
                                {
                                    _Result.message = accLanguage == "ar" ? "فضلا قم بتحديد العنوان" : "Please choose address";
                                    _Result.status = false;
                                    _Result.isAuthorize = true;
                                    return Ok(_Result);
                                }
                            }
                            else
                            {
                                _Result.message = accLanguage == "ar" ? "فضلا قم بتحديد المنتجات" : "Please choose products";
                                _Result.status = false;
                                _Result.isAuthorize = true;
                                return Ok(_Result);
                            }
                        }
                        else
                        {
                            _Result.message = accLanguage == "ar" ? "فضلا قم بتحديد المنتجات" : "Please choose products";
                            _Result.status = false;
                            _Result.isAuthorize = true;
                            return Ok(_Result);
                        }
                    }
                    else
                    {
                        _Result.message = accLanguage == "ar" ? "فضلا قم بتحديد طريقة الدفع" : "Payment Type not found";
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
                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Error + e.InnerException?.Message.ToString() : Api.Resources.Response.Error + e.InnerException?.Message.ToString();
                _Result.status = false;
                _Result.isAuthorize = true;
                return Ok(_Result);
            }
        }
        #endregion
        [CustomAuthorization]
        [HttpPost("AddIncreaseOrder")]
        public IActionResult AddIncreaseOrder(AddOrderViewModelApiNew model)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdClient();
                var UserLoginToken = GetUserTokenClient();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var CustomerData = _blCustomer.GetCustomerByUser(UserId, "City");
                int CustomerId = 0;
                if (CustomerData != null)
                {
                    CustomerId = CustomerData.CustomersID;
                }
                if (CustomerId != 0)
                {
                    if (model.vendorOrder.Count() > 0)
                    {
                        if (model.vendorOrder.FirstOrDefault().products.Count() > 0)
                        {
                            model.paymentTypeId = (int)PaymentTypeEnum.Wallet;
                            var customerLocation = _blCustomer.GetCustomerLocationByCustomersID(CustomerId);
                            model.addressID = customerLocation != null ? customerLocation.CustomerLocationID : 0;

                            if (model.addressID != 0)
                            {
                                string[] DateFormat = { "yyyy-MM-dd", "yyyy-MM-dd HH:mm", "yyyy-MM-dd HH:m", "yyyy-MM-dd H:mm", "yyyy-MM-dd H:m" };
                                if (!string.IsNullOrEmpty(model.scheduleDate))
                                {
                                    model.scheduleDate = DateTime.ParseExact(model.scheduleDate, DateFormat, System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
                                }
                                if (!string.IsNullOrEmpty(model.scheduleTime))
                                {
                                    model.scheduleTime = DateTime.ParseExact(model.scheduleTime, DateFormat, System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy hh:mm tt");
                                }
                                var NationalityList = _blCustomer.AddOrder_New_Pending(model, UserId, CustomerId, _configuration["FireBaseKey"], "MOB", UserLoginToken, null, null);
                                if (NationalityList.message == "true")
                                {
                                    _Result.message = accLanguage == "ar" ? "تم ارسال طلب الزيادة وبانتظار الموافقة عليه" : "Send Increase Order Successfully and Wating To approve";
                                    _Result.status = true;
                                    _Result.isAuthorize = true;
                                    _Result.data = new { orderId = NationalityList.orderId };
                                    return Ok(_Result);
                                }
                                else
                                {
                                    _Result.message = NationalityList.message;
                                    _Result.status = false;
                                    _Result.isAuthorize = true;
                                    _Result.data = new { };
                                    return Ok(_Result);
                                }
                            }
                            else
                            {
                                _Result.message = accLanguage == "ar" ? "فضلا قم بتحديد العنوان" : "Please choose address";
                                _Result.status = false;
                                _Result.isAuthorize = true;
                                return Ok(_Result);
                            }
                        }
                        else
                        {
                            _Result.message = accLanguage == "ar" ? "فضلا قم بتحديد المنتجات" : "Please choose products";
                            _Result.status = false;
                            _Result.isAuthorize = true;
                            return Ok(_Result);
                        }
                    }
                    else
                    {
                        _Result.message = accLanguage == "ar" ? "فضلا قم بتحديد المنتجات" : "Please choose products";
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
                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Error + e.InnerException?.Message.ToString() : Api.Resources.Response.Error + e.InnerException?.Message.ToString();
                _Result.status = false;
                _Result.isAuthorize = true;
                return Ok(_Result);
            }
        }
        [CustomAuthorization]
        [HttpPost("ValidationIncreaseOrder")]
        public IActionResult ValidationIncreaseOrder(AddOrderViewModelApiNew model)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdClient();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var CustomerData = _blCustomer.GetCustomerByUser(UserId);
                int CustomerId = 0;
                if (CustomerData != null)
                {
                    CustomerId = CustomerData.CustomersID;
                }
                if (CustomerId != 0)
                {
                    if (model.paymentTypeId != 0)
                    {
                        if (model.vendorOrder.Count() > 0)
                        {
                            if (model.vendorOrder.FirstOrDefault().products.Count() > 0)
                            {
                                if (model.addressID != 0)
                                {
                                    var NationalityList = _blCustomer.ValidationIncreaseOrder(model);
                                    if (NationalityList.message == "true")
                                    {
                                        _Result.message = accLanguage == "ar" ? "تم ارسال الطلب بنجاح" : "Order Send Successfully";
                                        _Result.status = true;
                                        _Result.isAuthorize = true;
                                        _Result.data = new { orderId = NationalityList.orderId };
                                        return Ok(_Result);
                                    }
                                    else
                                    {
                                        _Result.message = NationalityList.message;
                                        _Result.status = false;
                                        _Result.isAuthorize = true;
                                        _Result.data = new { };
                                        return Ok(_Result);
                                    }
                                }
                                else
                                {
                                    _Result.message = accLanguage == "ar" ? "فضلا قم بتحديد العنوان" : "Please choose address";
                                    _Result.status = false;
                                    _Result.isAuthorize = true;
                                    return Ok(_Result);
                                }
                            }
                            else
                            {
                                _Result.message = accLanguage == "ar" ? "فضلا قم بتحديد المنتجات" : "Please choose products";
                                _Result.status = false;
                                _Result.isAuthorize = true;
                                return Ok(_Result);
                            }
                        }
                        else
                        {
                            _Result.message = accLanguage == "ar" ? "فضلا قم بتحديد المنتجات" : "Please choose products";
                            _Result.status = false;
                            _Result.isAuthorize = true;
                            return Ok(_Result);
                        }
                    }
                    else
                    {
                        _Result.message = accLanguage == "ar" ? "فضلا قم بتحديد طريقة الدفع" : "Payment Type not found";
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
                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Error + e.InnerException.Message.ToString() : Api.Resources.Response.Error + e.InnerException.Message.ToString();
                _Result.status = false;
                _Result.isAuthorize = true;
                return Ok(_Result);
            }
        }
        #endregion
        #region CustomerIncreaseOrders
        [CustomAuthorization]
        [HttpGet("CustomerIncreaseOrders")]
        public IActionResult CustomerIncreaseOrders(int page, int type)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdClient();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var CustomerData = _blCustomer.GetCustomerByUser(UserId);
                int CustomerId = 0;
                if (CustomerData != null)
                {
                    CustomerId = CustomerData.CustomersID;
                }
                if (CustomerId != 0)
                {
                    var NationalityList = _blCustomer.CustomerIncreaseOrders(accLanguage, page, _configuration["VendorImageView"], CustomerId, type, _configuration["CustomerImageView"], _configuration["ProductImageView"]);
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
        #endregion
        #region ListCart
        [CustomAuthorization]
        [HttpGet("GetCartsItem")]
        public IActionResult GetCartsItem(int orderId)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdClient();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var CustomerData = _blCustomer.GetCustomerByUser(UserId);
                int CustomerId = 0;
                if (CustomerData != null)
                {
                    CustomerId = CustomerData.CustomersID;
                }
                if (CustomerId != 0)
                {
                    var NationalityList = _blCustomer.GeCartDetailsViewModelApiByOrder(orderId, _configuration["VendorImageView"], _configuration["ProductImageView"], accLanguage);
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
        #endregion
        #region ListCustomerStores
        [CustomAuthorization]
        [HttpGet("ListCustomerStores")]
        public IActionResult ListCustomerStores(string keySearch, int page)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdClient();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var CustomerData = _blCustomer.GetCustomerByUser(UserId);
                int CustomerId = 0;
                if (CustomerData != null)
                {
                    CustomerId = CustomerData.CustomersID;
                }
                if (CustomerId != 0)
                {
                    var listvendor = _BlVendor.GetAllVendorViewModelByCustomer(CustomerId, accLanguage, _configuration["VendorImageView"], page);
                    var ss = listvendor.vendors.Count();
                    if (ss > 0)
                    {
                        var data = listvendor.vendors.Select(e => new
                        {
                            vendorsID = e.VendorsID,
                            storeName = e.StoreName,
                            logo = e.Logo,
                            activityName = e.ActivityName,
                            startWork = e.DaysWork,
                            vacWork = e.DaysVac,
                            isShowContact = e.IsShowContact,
                            mobileNo = e.MobileNo,
                            isVendorWorking = e.IsVendorWorking,
                            isVendorWorkingString = e.IsVendorWorkingString,
                        }).Where(e => e.storeName.Contains(keySearch ?? ""));
                        _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                        _Result.status = true;
                        _Result.isAuthorize = true;
                        _Result.data = new { isNextPage = listvendor.isNextPage, storelist = data };
                        return Ok(_Result);
                    }
                    else
                    {
                        _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                        _Result.status = true;
                        _Result.isAuthorize = true;
                        _Result.data = new { isNextPage = listvendor.isNextPage, storelist = new { } };
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
        #region New_Map
        [AllowAnonymous]
        [HttpPost("GetLocationData")]
        public IActionResult GetLocationData(string lat, string lng)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdClient();
                var obj = new GoogleGeoCodeResp().GetReciverData(lat, lng, _configuration["GoogleApiKey"]);
                if (obj != null)
                {
                    if (!string.IsNullOrWhiteSpace(obj.countryName))
                    {
                        int UserId = _bLUser.GetFirstUserId();
                        var Result = _blCustomer.GetLocationbyMap(obj.CityName, obj.areaName, obj.Blok_place_id, obj.Blok_LocationType, accLanguage, UserId);
                        if (!string.IsNullOrWhiteSpace(obj.areaName))
                        {
                            if (Result != null)
                            {
                                Result.Address = obj.Blok_place_id;

                                _Result.message = "";
                                _Result.status = true;
                                _Result.isAuthorize = true;
                                _Result.data = Result;
                                return Ok(_Result);
                            }
                            else
                            {

                                _Result.message = accLanguage == "ar" ? "عفوا , المنطقة المحددة خارج تغطية التوصيل" : "Sorry, this location out of our range";
                                _Result.status = false;
                                _Result.isAuthorize = true;
                                return Ok(_Result);
                            }
                        }
                        else
                        {

                            _Result.message = accLanguage == "ar" ? "عفوا , المنطقة المحددة خارج تغطية التوصيل" : "Sorry, this location out of our range";
                            _Result.status = false;
                            _Result.isAuthorize = true;
                            return Ok(_Result);
                        }
                    }
                    else
                    {
                        _Result.message = accLanguage == "ar" ? "عفوا , لايمكن مشاركة هذا الموقع لانه خارج المملكة العربية السعودية" : "Sorry, this site cannot be shared because it is outside the Kingdom of Saudi Arabia";
                        _Result.status = false;
                        _Result.isAuthorize = true;
                        return Ok(_Result);

                    }
                }
                else
                {
                    _Result.message = accLanguage == "ar" ? "عفوا , لايمكن مشاركة هذا الموقع لانه خارج المملكة العربية السعودية" : "Sorry, this site cannot be shared because it is outside the Kingdom of Saudi Arabia";
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
        #region IsExistAddress
        [CustomAuthorization]
        [HttpGet("IsExistAddress")]
        public IActionResult IsExistAddress()
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdClient();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var CustomerData = _blCustomer.GetCustomerByUser(UserId);
                int CustomerId = 0;
                if (CustomerData != null)
                {
                    CustomerId = CustomerData.CustomersID;
                }
                if (CustomerId != 0)
                {
                    var obj = _blCustomer.GetCustomerLocationByCustomersID(CustomerId);
                    if (obj != null)
                    {

                        _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Address_Found : Api.Resources.Response.Address_Found;
                        _Result.status = true;
                        _Result.isAuthorize = true;
                        _Result.data = new { addressID = obj.CustomerLocationID };
                        return Ok(_Result);
                    }
                    else
                    {
                        _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.No_Address_Found : Api.Resources.Response.No_Address_Found;
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
        #region IsCompanyWorking
        [AllowAnonymous]
        [HttpGet("IsCompanyWorking")]
        public IActionResult IsCompanyWorking()
        {
            var accLanguage = GetUserLang();
            try
            {
                var IsCompanyWorking = _blCompanyWorkingHours.IsCompanyWorking();
                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                _Result.status = true;
                _Result.isAuthorize = true;
                _Result.data = new
                {
                    workingMessage = IsCompanyWorking ? (accLanguage == "ar" ? Api.Resources.Response_ar.The_service_is_now_available : Api.Resources.Response.The_service_is_now_available) : (accLanguage == "ar" ? Api.Resources.Response_ar.Sorry_we_are_out_of_order_now : Api.Resources.Response.Sorry_we_are_out_of_order_now),
                    workingStatus = IsCompanyWorking,
                };
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
        #endregion
        #region List Stores
        [HttpGet("ListStores")]
        public IActionResult ListStores(double lat, double lng, string keySearch, int page)
        {
            var accLanguage = GetUserLang();
            try
            {
                var listvendor = _BlVendor.GetAllVendorViewModel(accLanguage, _configuration["VendorImageView"], page, keySearch);
                var ss = listvendor.vendors.Count();
                if (ss > 0)
                {
                    var data = listvendor.vendors
                        .Select(e => new
                        {
                            vendorsID = e.VendorsID,
                            storeName = e.StoreName,
                            logo = e.Logo,
                            activityName = e.ActivityName,
                            awayYou = accLanguage == "ar" ? "يبعد عنك " + _blGeneral.GetDistance(lat, lng, (double)e.Lat, (double)e.Lng).ToString("0.##") + " كيلو"
                      : "away from you " + _blGeneral.GetDistance(lat, lng, (double)e.Lat, (double)e.Lng).ToString("0.##") + " KM",
                            startWork = e.DaysWork,
                            vacWork = e.DaysVac,
                            distanceKM = _blGeneral.GetDistance(lat, lng, (double)e.Lat, (double)e.Lng).ToString("0.##"),
                            isShowContact = e.IsShowContact,
                            mobileNo = e.MobileNo,
                            isVendorWorking = e.IsVendorWorking,
                            isVendorWorkingString = e.IsVendorWorkingString,
                        });

                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                    _Result.status = true;
                    _Result.isAuthorize = true;
                    _Result.data = new { isNextPage = listvendor.isNextPage, storelist = data.OrderBy(w => w.distanceKM).OrderByDescending(p => p.isVendorWorking) };
                    return Ok(_Result);
                }
                else
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                    _Result.status = true;
                    _Result.isAuthorize = true;
                    _Result.data = new { isNextPage = listvendor.isNextPage, storelist = new { } };
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
