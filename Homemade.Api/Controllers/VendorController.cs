using Homemade.Api.Authentication;
using Homemade.Api.Model;
using Homemade.Api.Models;
using Homemade.BLL;
using Homemade.BLL.General;
using Homemade.BLL.Order;
using Homemade.BLL.Setting;
using Homemade.BLL.SMS;
using Homemade.BLL.Vendor;
using Homemade.BLL.ViewModel.Driver;
using Homemade.BLL.ViewModel.Order;
using Homemade.BLL.ViewModel.Setting;
using Homemade.BLL.ViewModel.Vendor;
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
using System.Text;
using System.Threading.Tasks;

namespace Homemade.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        #region Declarations
        private readonly BLGeneral _blGeneral;
        private UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private FixedResultMessage _Result = new FixedResultMessage();
        private readonly BlProduct _BlProduct;
        private readonly BlVendor _BlVendor;
        private readonly BlTokens _blTokens;
        private readonly BlCity _blCity;
        private readonly BlOrders _BlOrders;
        private readonly BlHelpQuestions _blHelpQuestions;
        private readonly BlVendorSupport _BlVendorSupport;
        private readonly OTPService _OTPService;
        private readonly BlDepartments _blDepartments;

        public VendorController(OTPService OTPService, BlVendorSupport BlVendorSupport, BlHelpQuestions BlHelpQuestions, BlOrders BlOrders, BLGeneral blGeneral, UserManager<User> userManager, IConfiguration configuration, BlVendor BlVendor, BlProduct BlProduct, BlTokens blTokens, BlCity blCity, BlDepartments blDepartments)
        {
            _blGeneral = blGeneral;
            _userManager = userManager;
            _configuration = configuration;
            _BlProduct = BlProduct;
            _BlVendor = BlVendor;
            _blTokens = blTokens;
            _blCity = blCity;
            _BlOrders = BlOrders;
            _blHelpQuestions = BlHelpQuestions;
            _BlVendorSupport = BlVendorSupport;
            _OTPService = OTPService;
            _blDepartments = blDepartments;
        }
        #endregion
        #region Get User Id Vendor
        private string GetUserIdVendor()
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
                        int JWTTokenValid = _blTokens.IsValidUserJWTTokenVendor(token);
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
        #region Logout
        [CustomAuthorization]
        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            string message;
            string userLang = GetUserLang();
            try
            {
                string userIdClient = GetUserIdVendor();
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
        #region Vendor_Producs
        [AllowAnonymous]
        [HttpPost("DeleteProduct")]
        public IActionResult DeleteProduct(int productID)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdVendor();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var VendorData = _BlVendor.GetVendorsByUserId(UserId);
                if (VendorData != null)
                {
                    var NationalityList = _BlProduct.DeleteProduct(productID, UserId);
                    _Result.message = accLanguage == "ar" ? "تم حذف المنتج بنجاح" : "Delete Product Successfully";
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
            catch (Exception e)
            {
                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Error + e.ToString() : Api.Resources.Response.Error + e.ToString();
                _Result.status = false;
                _Result.isAuthorize = true;
                return Ok(_Result);
            }
        }
        [AllowAnonymous]
        [HttpPost("UpdateFixedProduct")]
        public IActionResult UpdateFixedProduct(int productID,int type)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdVendor();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var VendorData = _BlVendor.GetVendorsByUserId(UserId);
                if (VendorData != null)
                {
                    var NationalityList = _BlProduct.UpdateProdFixed(productID, type, UserId);
                    _Result.message = accLanguage == "ar" ? "تم تعديل المنتج بنجاح" : "Update Product Successfully";
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
            catch (Exception e)
            {
                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Error + e.ToString() : Api.Resources.Response.Error + e.ToString();
                _Result.status = false;
                _Result.isAuthorize = true;
                return Ok(_Result);
            }
        }
        [AllowAnonymous]
        [HttpPost("DeleteAllProduct")]
        public IActionResult DeleteAllProduct()
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdVendor();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var VendorData = _BlVendor.GetVendorsByUserId(UserId);
                if (VendorData != null)
                {
                    var NationalityList = _BlProduct.DeleteAllProduct(VendorData.VendorsID, UserId);
                    _Result.message = accLanguage == "ar" ? "تم حذف المنتجات بنجاح" : "Delete All Products Successfully";
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
                var NationalityList = _BlProduct.GetProductDetailsUpdateApi(productID, accLanguage, _configuration["ProductImageView"], _configuration["VendorImageView"]);
                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                _Result.status = true;
                _Result.isAuthorize = true;
                _Result.data = new {product = NationalityList};
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
        public class ModelUpdateProd
        {
            public IFormFile logo { get; set; }
            public IList<IFormFile> productImage { get; set; }
            [FromJson]
            public UpdateProductViewModel productData { get; set; } // <-- JSON will be deserialized to this object
        }
        [CustomAuthorization]
        [HttpPost("UpdateProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult UpdateProduct([FromForm] ModelUpdateProd addProductViewModel)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdVendor();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var VendorData = _BlVendor.GetVendorsByUserId(UserId);
                if (VendorData != null)
                {
                    #region Images
                    string ImagePath = _configuration["ProductImageSave"];
                    var logofileName = string.Empty;
                    List<string> productImage = new List<string>();
                    if (addProductViewModel.logo != null)
                    {
                        if (addProductViewModel.logo.Length > 0)
                        {
                            logofileName = string.Concat(Guid.NewGuid().ToString()/*.Take(8)*/) +
                                    (ContentDispositionHeaderValue.Parse(addProductViewModel.logo.ContentDisposition)
                                    .FileName.Trim('"').Substring(
                                        ContentDispositionHeaderValue.Parse(addProductViewModel.logo.ContentDisposition)
                                        .FileName.Trim('"').IndexOf(".")));
                            var logofullPath = Path.Combine(ImagePath, logofileName);
                            using (var stream = new FileStream(logofullPath, FileMode.Create))
                            {
                                addProductViewModel.logo.CopyTo(stream);
                            }
                        }
                    }
                    if (addProductViewModel.productImage != null)
                    {
                        if (addProductViewModel.productImage.Count > 0)
                        {
                            foreach (var item in addProductViewModel.productImage)
                            {
                                if (item != null)
                                {
                                    if (item.Length > 0)
                                    {
                                        var fileName = string.Concat(Guid.NewGuid().ToString()/*.Take(8)*/) +
                                                (ContentDispositionHeaderValue.Parse(item.ContentDisposition)
                                                .FileName.Trim('"').Substring(
                                                    ContentDispositionHeaderValue.Parse(item.ContentDisposition)
                                                    .FileName.Trim('"').IndexOf(".")));
                                        var logofullPath = Path.Combine(ImagePath, fileName);
                                        using (var stream = new FileStream(logofullPath, FileMode.Create))
                                        {
                                            item.CopyTo(stream);
                                        }
                                        productImage.Add(fileName);
                                    }
                                }
                            }
                        }

                    }
                    #endregion
                    #region Update product
                    var IsSuccess = _BlProduct.UpdateProduct(addProductViewModel.productData, logofileName, productImage, UserId, VendorData.VendorsID);
                    if (IsSuccess)
                    {
                        _Result.message = accLanguage == "ar" ? "تم تعديل المنتج بنجاح" : "Update Product Successfully";
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
        [AllowAnonymous]
        [HttpGet("VendorProducts")]
        public IActionResult VendorProducts(int page,string search)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdVendor();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var VendorData = _BlVendor.GetVendorsByUserId(UserId);
                if (VendorData != null)
                {
                    var NationalityList = _BlVendor.GetVendorDetails(VendorData.VendorsID, _configuration["VendorImageView"], accLanguage, _configuration["ProductImageView"], page, search);
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                    _Result.status = true;
                    _Result.isAuthorize = true;
                    _Result.data = new {products = NationalityList};
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
            catch (Exception e)
            {
                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Error + e.ToString() : Api.Resources.Response.Error + e.ToString();
                _Result.status = false;
                _Result.isAuthorize = true;
                return Ok(_Result);
            }
        }
        #endregion
        #region Store
        [CustomAuthorization]
        [HttpGet("StoreDetails")]
        public IActionResult StoreDetails()
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdVendor();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var VendorData = _BlVendor.GetVendorsByUserId(UserId);
                if (VendorData != null)
                {
                    var NationalityList = _BlVendor.GetStoreDetails(VendorData.VendorsID, accLanguage, _configuration["VendorImageView"]);
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                    _Result.status = true;
                    _Result.isAuthorize = true;
                    _Result.data = new { storeDetails = NationalityList };
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
            catch (Exception e)
            {
                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Error + e.ToString() : Api.Resources.Response.Error + e.ToString();
                _Result.status = false;
                _Result.isAuthorize = true;
                return Ok(_Result);
            }
        }
        [CustomAuthorization]
        [HttpGet("PaymentDetails")]
        public IActionResult PaymentDetails()
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdVendor();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var VendorData = _BlVendor.GetVendorsByUserId(UserId);
                if (VendorData != null)
                {
                    var NationalityList = _BlVendor.GetPaymentDetails(VendorData.VendorsID);
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                    _Result.status = true;
                    _Result.isAuthorize = true;
                    _Result.data = new { paymentDetails = NationalityList };
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
            catch (Exception e)
            {
                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Error + e.ToString() : Api.Resources.Response.Error + e.ToString();
                _Result.status = false;
                _Result.isAuthorize = true;
                return Ok(_Result);
            }
        }
        public class StoreUpdate
        {
            public IFormFile logo { get; set; }
            public IFormFile banner { get; set; }
            public IFormFile cRPic { get; set; } //صورة السجل التجارى
            [FromJson]
            public UpdateStoreViewModelApi storeData { get; set; } // <-- JSON will be deserialized to this object
        }
        [CustomAuthorization]
        [HttpPost("UpdateStore")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult UpdateStore([FromForm] StoreUpdate updateVendorViewModel)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdVendor();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var VendorData = _BlVendor.GetVendorsByUserId(UserId);
                if (VendorData != null)
                {
                    #region Images
                    string ImagePath = _configuration["VendorImageSave"];
                    var logofileName = string.Empty;
                    var bannerfileName = string.Empty;
                    var cRPicfileName = string.Empty;
                    if (updateVendorViewModel.logo != null)
                    {
                        if (updateVendorViewModel.logo.Length > 0)
                        {
                            logofileName = string.Concat(Guid.NewGuid().ToString()/*.Take(8)*/) +
                                    (ContentDispositionHeaderValue.Parse(updateVendorViewModel.logo.ContentDisposition)
                                    .FileName.Trim('"').Substring(
                                        ContentDispositionHeaderValue.Parse(updateVendorViewModel.logo.ContentDisposition)
                                        .FileName.Trim('"').IndexOf(".")));
                            var logofullPath = Path.Combine(ImagePath, logofileName);
                            using (var stream = new FileStream(logofullPath, FileMode.Create))
                            {
                                updateVendorViewModel.logo.CopyTo(stream);
                            }
                        }
                    }
                    if (updateVendorViewModel.banner != null)
                    {
                        if (updateVendorViewModel.banner.Length > 0)
                        {
                            bannerfileName = string.Concat(Guid.NewGuid().ToString()/*.Take(8)*/) +
                                    (ContentDispositionHeaderValue.Parse(updateVendorViewModel.banner.ContentDisposition)
                                    .FileName.Trim('"').Substring(
                                        ContentDispositionHeaderValue.Parse(updateVendorViewModel.banner.ContentDisposition)
                                        .FileName.Trim('"').IndexOf(".")));
                            var bannerfullPath = Path.Combine(ImagePath, bannerfileName);
                            using (var stream = new FileStream(bannerfullPath, FileMode.Create))
                            {
                                updateVendorViewModel.banner.CopyTo(stream);
                            }
                        }
                    }
                    if (updateVendorViewModel.cRPic != null)
                    {
                        if (updateVendorViewModel.cRPic.Length > 0)
                        {
                            cRPicfileName = string.Concat(Guid.NewGuid().ToString()/*.Take(8)*/) +
                                    (ContentDispositionHeaderValue.Parse(updateVendorViewModel.cRPic.ContentDisposition)
                                    .FileName.Trim('"').Substring(
                                        ContentDispositionHeaderValue.Parse(updateVendorViewModel.cRPic.ContentDisposition)
                                        .FileName.Trim('"').IndexOf(".")));
                            var cRPicfullPath = Path.Combine(ImagePath, cRPicfileName);
                            using (var stream = new FileStream(cRPicfullPath, FileMode.Create))
                            {
                                updateVendorViewModel.cRPic.CopyTo(stream);
                            }
                        }
                    }

                    #endregion
                    #region Update Vendor
                    if (string.IsNullOrEmpty(updateVendorViewModel.storeData.daysWork))
                    {
                        _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Please_Choose_Days_Work : Api.Resources.Response.Please_Choose_Days_Work;
                        _Result.status = false;
                        _Result.isAuthorize = true;
                        return Ok(_Result);
                    }
                    

                    var IsSuccess = _BlVendor.UpdateStore(updateVendorViewModel.storeData, logofileName, bannerfileName, cRPicfileName, UserId, VendorData.VendorsID);
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
        [HttpPost("UpdatePayment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult UpdatePayment(PaymentDetails model)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdVendor();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var VendorData = _BlVendor.GetVendorsByUserId(UserId);
                if (VendorData != null)
                {
                    #region Update Vendor
                    var IsSuccess = _BlVendor.UpdatePayment(model, UserId, VendorData.VendorsID);
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
        [HttpPost("UpdateLocation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult UpdateLocation(LocationDetails model)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdVendor();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var VendorData = _BlVendor.GetVendorsByUserId(UserId);
                if (VendorData != null)
                {
                    #region Update Vendor
                    var IsSuccess = _BlVendor.UpdateLocation(model, UserId, VendorData.VendorsID);
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
        #endregion
        #region VendorDetails
        [AllowAnonymous]
        [HttpGet("VendorDetails")]
        public IActionResult VendorDetails(int vendorId, int page)
        {
            var accLanguage = GetUserLang();
            try
            {
                var NationalityList = _BlVendor.GetVendorDetails(vendorId, _configuration["VendorImageView"], accLanguage, _configuration["ProductImageView"], page);
                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                _Result.status = true;
                _Result.isAuthorize = true;
                _Result.data = new { vendorlist = NationalityList };
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
        #region Create Vendor
        /// <summary>
        /// Create New Vendor
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST / CreateVendor
        ///     {
        ///       
        ///     }
        ///
        /// </remarks>
        /// <param name="createVendorViewModel"></param>
        /// <returns>
        /// <response code="200"> 
        /// <remarks>
        /// Sample responce:
        /// {         
        ///  "message": "Thanks for Registration,Your request has been send and we will contact with you soon.",
        /// "status": true,
        /// "isAuthorize": true,
        /// "data": {
        ///    "firstName": "islam",
        ///   "seconedName": "islam",
        ///   "mobileNo": "567895233",
        ///   "email": "mmmm@m.com",
        ///   "gender": "انثى",
        ///   "personalPhoto": "https://cdn.made-home.com/Shared/Home/Vendor/",
        ///  "vendorsID": 4,
        ///  "cityName": "DMM",
        ///  "nationalityName": "DMM",
        ///  "registerTypeId": 1,
        ///  "userId": 10,
        ///  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiNTY3ODk1MjMzIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvc2lkIjoiMTAiLCJqdGkiOiIzNGI4ODMwMC0yZjNkLTQ0YTUtODU0YS01Yjk4ZWM3ZWI3M2EiLCJleHAiOjE2MzExNDMwNDUsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NDQzOTQiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjQ0Mzk0In0.uRZaBOvPg68AaOdJk0T4YD2TrIJtjVWfjxzxYwY0wfg"        /// }
        ///           }
        /// </remarks>
        /// </response>
        /// </returns>
        public class VendorCreate
        {
            [FromJson]
            public CreateVendorViewModel vendorData { get; set; } // <-- JSON will be deserialized to this object
        }
        public class VendorUpdateProfile
        {
            public IFormFile profilePhoto { get; set; }
            [FromJson]
            public ProfileVendorViewModel vendorData { get; set; } // <-- JSON will be deserialized to this object
        }
        [HttpPost("CreateVendor")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Obsolete]
        public async Task<IActionResult> CreateVendor([FromForm] VendorCreate createVendorViewModel)
        {
            var accLanguage = GetUserLang();
            try
            {
                if (createVendorViewModel.vendorData.cityID != 0)
                {
                    if (!string.IsNullOrWhiteSpace(createVendorViewModel.vendorData.mobileNo))
                    {
                        if (!string.IsNullOrWhiteSpace(createVendorViewModel.vendorData.firstName))
                        {
                                int countryId = _blCity.GetById(createVendorViewModel.vendorData.cityID, "Region").Region.CountryID;
                                createVendorViewModel.vendorData.mobileNo = MobileFormat(createVendorViewModel.vendorData.mobileNo, countryId);
                                if (!A_Valid_Phone_Number(createVendorViewModel.vendorData.mobileNo, countryId))
                                {
                                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Invalid_Phone_number : Api.Resources.Response.Invalid_Phone_number;
                                    _Result.status = false;
                                    _Result.isAuthorize = true;
                                    return Ok(_Result);
                                }

                                if (!_BlVendor.IsExistMobile(createVendorViewModel.vendorData.mobileNo))
                                {
                                    if (!string.IsNullOrWhiteSpace(createVendorViewModel.vendorData.email))
                                    {
                                        if (_BlVendor.IsExistEmail(createVendorViewModel.vendorData.email))
                                        {
                                            _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Email_Is_Exist : Api.Resources.Response.Email_Is_Exist;
                                            _Result.status = false;
                                            _Result.isAuthorize = true;
                                            return Ok(_Result);
                                        }
                                    }
                                    if (!string.IsNullOrWhiteSpace(createVendorViewModel.vendorData.password))
                                    {
                                        if (createVendorViewModel.vendorData.password.Length > 5)
                                        {
                                            #region add Vendor
                                            #region User
                                            //Insert User
                                            var user = new User { UserName = _blGeneral.GenerateToken(200),
                                                Email = _blGeneral.RandomString(10) + "@made-home.com",
                                                PhoneNumber = _blGeneral.RandomNumber(10),
                                                UserType = (int)UserTypeEnum.Vendor };
                                            var identityResult = await _userManager.CreateAsync(user, createVendorViewModel.vendorData.password);
                                            #endregion
                                            if (identityResult.Succeeded)
                                            {
                                                try
                                                {
                                                    var IsSuccess = await _BlVendor.CreateVendor(createVendorViewModel.vendorData, string.Empty, user.Id);
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
                                                        _blTokens.AddTokens(user.Id, 1, token, (int)UserTypeEnum.Vendor);
                                                        #endregion
                                                        #region Update JWT Token
                                                        _blTokens.UpdateUserJWTToken(user.Id, new JwtSecurityTokenHandler().WriteToken(Token));
                                                        #endregion
                                                        #endregion
                                                        #endregion
                                                        var vendordata = _BlVendor.GetVendorsByMobileNo(createVendorViewModel.vendorData.mobileNo, "City,Nationality");
                                                        var SMSStatus = await _OTPService.ThankMessageAfterRegister(createVendorViewModel.vendorData.mobileNo, (int)UserTypeEnum.Vendor);
                                                        _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Thanks_for_Registration_Your_request_has_been_send_and_we_will_contact_with_you_soon : Api.Resources.Response.Thanks_for_Registration_Your_request_has_been_send_and_we_will_contact_with_you_soon;
                                                        _Result.status = true;
                                                        _Result.isAuthorize = true;
                                                        _Result.data = new
                                                        {
                                                            firstName = accLanguage == "ar" ? vendordata.FirstNameAr : vendordata.FirstNameEn,
                                                            seconedName = accLanguage == "ar" ? vendordata.SeconedNameAr : vendordata.SeconedNameEn,
                                                            mobileNo = vendordata.MobileNo,
                                                            email = vendordata.Email,
                                                            gender = accLanguage == "ar" ? (vendordata.Gender == (int)Gender.Male ? "Male" : vendordata.Gender == (int)Gender.Female ? "Female" : "Prefer not to specify")
                                                            : (vendordata.Gender == (int)Gender.Male ? "ذكر" : vendordata.Gender == (int)Gender.Female ? "انثى" : "تفضل عدم التحديد"),
                                                            personalPhoto = _configuration["VendorImageView"] + vendordata.ProfilePic,
                                                            vendorID = vendordata.VendorsID,
                                                            cityName = accLanguage == "ar" ? vendordata.City.NameAR : vendordata.City.NameEN,
                                                            cityID = vendordata.CityID,
                                                            nationalityName = vendordata.NationalityID != null ? accLanguage == "ar" ? vendordata.Nationality.NameAR : vendordata.Nationality.NameEN : "",
                                                            registerTypeId = vendordata.RegisterType,
                                                            userId = user.Id,
                                                            token = new JwtSecurityTokenHandler().WriteToken(Token),
                                                            isVac = vendordata.IsVac,
                                                            notificationCount = _BlVendor.GetUserNotificationNotSeenCount(vendordata.UserId)
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

        public class CompleteRegisterViewModel
        {
            public IFormFile logo { get; set; }
            public IFormFile banner { get; set; }
            public IFormFile cRPic { get; set; } //صورة السجل التجارى
            [FromJson]
            public CompleteRegisterApi vendorData { get; set; } // <-- JSON will be deserialized to this object
        }
        [CustomAuthorization]
        [HttpPost("CompleteRegister")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult CompleteRegister([FromForm] CompleteRegisterViewModel updateVendorViewModel)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdVendor();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var VendorData = _BlVendor.GetVendorsByUserId(UserId);
                if (VendorData != null)
                {
                    #region Images
                    string ImagePath = _configuration["VendorImageSave"];
                    var logofileName = string.Empty;
                    var bannerfileName = string.Empty;
                    var cRPicfileName = string.Empty;
                    if (updateVendorViewModel.logo != null)
                    {
                        if (updateVendorViewModel.logo.Length > 0)
                        {
                            logofileName = string.Concat(Guid.NewGuid().ToString()/*.Take(8)*/) +
                                    (ContentDispositionHeaderValue.Parse(updateVendorViewModel.logo.ContentDisposition)
                                    .FileName.Trim('"').Substring(
                                        ContentDispositionHeaderValue.Parse(updateVendorViewModel.logo.ContentDisposition)
                                        .FileName.Trim('"').IndexOf(".")));
                            var logofullPath = Path.Combine(ImagePath, logofileName);
                            using (var stream = new FileStream(logofullPath, FileMode.Create))
                            {
                                updateVendorViewModel.logo.CopyTo(stream);
                            }
                        }
                    }
                    if (updateVendorViewModel.banner != null)
                    {
                        if (updateVendorViewModel.banner.Length > 0)
                        {
                            bannerfileName = string.Concat(Guid.NewGuid().ToString()/*.Take(8)*/) +
                                    (ContentDispositionHeaderValue.Parse(updateVendorViewModel.banner.ContentDisposition)
                                    .FileName.Trim('"').Substring(
                                        ContentDispositionHeaderValue.Parse(updateVendorViewModel.banner.ContentDisposition)
                                        .FileName.Trim('"').IndexOf(".")));
                            var bannerfullPath = Path.Combine(ImagePath, bannerfileName);
                            using (var stream = new FileStream(bannerfullPath, FileMode.Create))
                            {
                                updateVendorViewModel.banner.CopyTo(stream);
                            }
                        }
                    }
                    if (updateVendorViewModel.cRPic != null)
                    {
                        if (updateVendorViewModel.cRPic.Length > 0)
                        {
                            cRPicfileName = string.Concat(Guid.NewGuid().ToString()/*.Take(8)*/) +
                                    (ContentDispositionHeaderValue.Parse(updateVendorViewModel.cRPic.ContentDisposition)
                                    .FileName.Trim('"').Substring(
                                        ContentDispositionHeaderValue.Parse(updateVendorViewModel.cRPic.ContentDisposition)
                                        .FileName.Trim('"').IndexOf(".")));
                            var cRPicfullPath = Path.Combine(ImagePath, cRPicfileName);
                            using (var stream = new FileStream(cRPicfullPath, FileMode.Create))
                            {
                                updateVendorViewModel.cRPic.CopyTo(stream);
                            }
                        }
                    }

                    #endregion
                    #region Update Vendor
                    if (string.IsNullOrEmpty(updateVendorViewModel.vendorData.daysWork))
                    {
                        _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Please_Choose_Days_Work : Api.Resources.Response.Please_Choose_Days_Work;
                        _Result.status = false;
                        _Result.isAuthorize = true;
                        return Ok(_Result);
                    }
                    var IsSuccess = _BlVendor.CompleteRegister(updateVendorViewModel.vendorData, logofileName, bannerfileName, cRPicfileName, UserId, VendorData.VendorsID);
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
        #endregion
        #region Login Vendor
        /// <summary>
        /// Login Vendor
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST / LoginVendor
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
        /// "isAuthorize": true,
        /// "data": {
        ///    "firstName": "islam",
        ///   "seconedName": "islam",
        ///   "mobileNo": "567895233",
        ///   "email": "mmmm@m.com",
        ///   "gender": "انثى",
        ///   "personalPhoto": "https://cdn.made-home.com/Shared/Home/Vendor/",
        ///  "vendorsID": 4,
        ///  "cityName": "DMM",
        ///  "nationalityName": "DMM",
        ///  "registerTypeId": 1,
        ///  "userId": 10,
        ///  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiNTY3ODk1MjMzIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvc2lkIjoiMTAiLCJqdGkiOiIzNGI4ODMwMC0yZjNkLTQ0YTUtODU0YS01Yjk4ZWM3ZWI3M2EiLCJleHAiOjE2MzExNDMwNDUsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NDQzOTQiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjQ0Mzk0In0.uRZaBOvPg68AaOdJk0T4YD2TrIJtjVWfjxzxYwY0wfg"
        ///   }
        ///           }
        /// </remarks>
        /// </response>
        /// </returns>
        [AllowAnonymous]
        [Consumes("application/x-www-form-urlencoded")]
        [HttpPost("LoginVendor")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> LoginVendor([FromForm] LoginModel loginModel)
        {
            var accLanguage = GetUserLang();
            try
            {
                var vendordata = _BlVendor.GetVendorsByMobileNo(loginModel.userName, "City,Nationality");
                if (vendordata != null)
                {
                    if (vendordata.IsDeleted && vendordata?.DeleteDate.Value >= DateTime.Now.AddMonths(1))
                    {
                        _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Sorry_Deleted_Account : Api.Resources.Response.Sorry_Deleted_Account;
                        _Result.status = false;
                        _Result.isAuthorize = true;
                        return Ok(_Result);
                    }
                    var user = await _userManager.FindByIdAsync(vendordata.UserId.ToString());
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
                        #region Check Login Vendor 
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
                            if (vendordata.IsEnabled == Convert.ToBoolean(StatusEnable.Enable))
                            {
                                _blTokens.AddTokens(user.Id, loginModel.deviceType, loginModel.token, (int)UserTypeEnum.Vendor);
                                _blTokens.UpdateUserJWTToken(user.Id, new JwtSecurityTokenHandler().WriteToken(Token));
                                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Login_Successfuly : Api.Resources.Response.Login_Successfuly;
                                _Result.status = true;
                                _Result.isAuthorize = true;
                                _Result.data = new
                                {
                                    firstName = accLanguage == "ar" ? vendordata.FirstNameAr : vendordata.FirstNameEn,
                                    seconedName = accLanguage == "ar" ? vendordata.SeconedNameAr : vendordata.SeconedNameEn,
                                    mobileNo = vendordata.MobileNo,
                                    email = vendordata.Email,
                                    gender = accLanguage == "ar" ? (vendordata.Gender == (int)Gender.Male ? "Male" : vendordata.Gender == (int)Gender.Female ? "Female" : "Prefer not to specify")
                                    : (vendordata.Gender == (int)Gender.Male ? "ذكر" : vendordata.Gender == (int)Gender.Female ? "انثى" : "تفضل عدم التحديد"),
                                    personalPhoto = _configuration["VendorImageView"] + vendordata.ProfilePic,
                                    vendorID = vendordata.VendorsID,
                                    cityName = accLanguage == "ar" ? vendordata.City.NameAR : vendordata.City.NameEN,
                                    nationalityName = vendordata.NationalityID != null ? accLanguage == "ar" ? vendordata.Nationality.NameAR : vendordata.Nationality.NameEN : "",
                                    registerTypeId = vendordata.RegisterType,
                                    userId = user.Id,
                                    cityID = vendordata.CityID,
                                    token = new JwtSecurityTokenHandler().WriteToken(Token),
                                    isVac = vendordata.IsVac,
                                    notificationCount = _BlVendor.GetUserNotificationNotSeenCount(user.Id),
                                };
                                return Ok(_Result);
                            }
                            else
                            {
                                if (vendordata.RegisterType == (int)RegisterType.Created)
                                {
                                    _blTokens.AddTokens(user.Id, loginModel.deviceType, loginModel.token, (int)UserTypeEnum.Vendor);
                                    _blTokens.UpdateUserJWTToken(user.Id, new JwtSecurityTokenHandler().WriteToken(Token));
                                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Login_Successfuly : Api.Resources.Response.Login_Successfuly;
                                    _Result.status = true;
                                    _Result.isAuthorize = true;
                                    _Result.data = new
                                    {
                                        firstName = accLanguage == "ar" ? vendordata.FirstNameAr : vendordata.FirstNameEn,
                                        seconedName = accLanguage == "ar" ? vendordata.SeconedNameAr : vendordata.SeconedNameEn,
                                        mobileNo = vendordata.MobileNo,
                                        email = vendordata.Email,
                                        gender = accLanguage == "ar" ? (vendordata.Gender == (int)Gender.Male ? "Male" : vendordata.Gender == (int)Gender.Female ? "Female" : "Prefer not to specify")
                                        : (vendordata.Gender == (int)Gender.Male ? "ذكر" : vendordata.Gender == (int)Gender.Female ? "انثى" : "تفضل عدم التحديد"),
                                        personalPhoto = _configuration["VendorImageView"] + vendordata.ProfilePic,
                                        vendorID = vendordata.VendorsID,
                                        cityName = accLanguage == "ar" ? vendordata.City.NameAR : vendordata.City.NameEN,
                                        nationalityName = vendordata.NationalityID != null ? accLanguage == "ar" ? vendordata.Nationality.NameAR : vendordata.Nationality.NameEN : "",
                                        registerTypeId = vendordata.RegisterType,
                                        userId = user.Id,
                                        token = new JwtSecurityTokenHandler().WriteToken(Token),
                                        isVac = vendordata.IsVac,
                                        notificationCount = _BlVendor.GetUserNotificationNotSeenCount(user.Id),
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
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.This_Account_Not_Vendor : Api.Resources.Response.This_Account_Not_Vendor;
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
        #region Add Product
        /// <summary>
        /// Add New Product
        /// </summary>
        /// {
        /// <remarks>
        /// Sample request:
        ///
        ///     POST / Add Product
        ///  "nameAr": "string",
        ///  "nameEn": "string",
        ///  "vendorsID": 0,
        ///  "departmentsID": 0,
        ///  "descAr": "string",
        ///"descEn": "string",
        ///  "logo": "string",
        ///  "sKU": "string",
        ///  "price": 0,
        ///  "quantity": 0,
        ///  "weight": 0,
        /// "discount": 0,
        ///"startDiscountDate": "2021-09-08T21:44:00.764Z",
        ///"endDiscountDate": "2021-09-08T21:44:00.764Z",
        ///"isFixed": true,
        ///"isHidden": true,
        ///  "isAvailable": true,
        ///  "productImage": [
        ///   "string"
        /// ],
        ///  "productQuestion": [
        /// {
        ///   "questionAr": "string",
        ///  "questionEn": "string",
        ///   "answerAr": "string",
        ///  "answerEn": "string"
        /// }
        ///]
        ///}
        /// </remarks>
        /// <param name="addProductViewModel"></param>
        /// <returns>
        /// <response code="200">
        /// <remarks>
        /// Sample responce:
        /// {         
        ///  {
        /// "message": "تم الحفظ بنجاح",
        /// "status": true,
        /// "isAuthorize": true,
        /// "data": {}
        /// }
        ///           }
        /// </remarks>
        /// </response>
        /// </returns>
        public class MyModelWrapper
        {
            public IFormFile logo { get; set; }
            public IList<IFormFile> productImage { get; set; }
            [FromJson]
            public AddProductViewModel productData { get; set; } // <-- JSON will be deserialized to this object
        }
        [CustomAuthorization]
        [HttpPost("AddProduct")]
        public IActionResult AddProduct([FromForm]  MyModelWrapper addProductViewModel)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdVendor();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var VendorData = _BlVendor.GetVendorsByUserId(UserId);
                if (VendorData != null)
                {
                    #region Images
                    string ImagePath = _configuration["ProductImageSave"];
                    var logofileName = string.Empty;
                    List<string> productImage = new List<string>();
                    if (addProductViewModel.logo != null)
                    {
                        if (addProductViewModel.logo.Length > 0)
                        {
                            logofileName = string.Concat(Guid.NewGuid().ToString()/*.Take(8)*/) +
                                    (ContentDispositionHeaderValue.Parse(addProductViewModel.logo.ContentDisposition)
                                    .FileName.Trim('"').Substring(
                                        ContentDispositionHeaderValue.Parse(addProductViewModel.logo.ContentDisposition)
                                        .FileName.Trim('"').IndexOf(".")));
                            var logofullPath = Path.Combine(ImagePath, logofileName);
                            using (var stream = new FileStream(logofullPath, FileMode.Create))
                            {
                                addProductViewModel.logo.CopyTo(stream);
                            }
                        }
                    }
                    if (addProductViewModel.productImage != null)
                    {
                        if (addProductViewModel.productImage.Count > 0)
                        {
                            foreach (var item in addProductViewModel.productImage)
                            {
                                if (item != null)
                                {
                                    if (item.Length > 0)
                                    {
                                        var fileName = string.Concat(Guid.NewGuid().ToString()/*.Take(8)*/) +
                                                (ContentDispositionHeaderValue.Parse(item.ContentDisposition)
                                                .FileName.Trim('"').Substring(
                                                    ContentDispositionHeaderValue.Parse(item.ContentDisposition)
                                                    .FileName.Trim('"').IndexOf(".")));
                                        var logofullPath = Path.Combine(ImagePath, fileName);
                                        using (var stream = new FileStream(logofullPath, FileMode.Create))
                                        {
                                            item.CopyTo(stream);
                                        }
                                        productImage.Add(fileName);
                                    }
                                }
                            }
                        }

                    }
                    #endregion
                    #region Add product
                    var IsSuccess = _BlProduct.AddProduct(addProductViewModel.productData, logofileName, productImage, UserId, VendorData.VendorsID);
                    if (IsSuccess)
                    {
                        _Result.message = accLanguage == "ar" ? "تم حفظ المنتج بنجاح" : "Save Product Successfully";
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
        [HttpPost("ProductRepeat")]
        public IActionResult ProductRepeat(int productId)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdVendor();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var VendorData = _BlVendor.GetVendorsByUserId(UserId);
                if (VendorData != null)
                {

                    #region Add product

                    var IsSuccess = _BlProduct.ProductRepeat(productId, UserId, VendorData.VendorsID);
                    if (IsSuccess)
                    {
                        _Result.message = accLanguage == "ar" ? "تم حفظ المنتج بنجاح" : "Save Product Successfully";
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
        #endregion
        #region Send Otp Code
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
        ///  [AllowAnonymous]
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
                    if (!_BlVendor.IsExistMobile(mobileNo))
                    {
                        if (!string.IsNullOrWhiteSpace(email))
                        {
                            if (_BlVendor.IsExistEmail(email))
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
                                var IsVisorExists = _BlVendor.GetVendorsByMobileNo(mobileNo, "");
                                if (IsVisorExists == null)
                                {
                                    var KeyCode = _blGeneral.RandomNumber(4);
                                    var SMSStatus = await _OTPService.SendSMSForVerify(mobileNo, KeyCode, (int)UserTypeEnum.Vendor, (int)MessageReasonEnum.Registration);
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
                    if (!_BlVendor.IsExistMobile(mobileNo))
                    {
                        var IsVisorExists = _BlVendor.GetVendorsByMobileNo(mobileNo, "");
                        if (IsVisorExists == null)
                        {
                            var KeyCode = _blGeneral.RandomNumber(4);
                            var SMSStatus = await _OTPService.SendSMSForVerify(mobileNo, KeyCode, (int)UserTypeEnum.Vendor, (int)MessageReasonEnum.Registration);
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
                    var IsVisorExists = _BlVendor.GetVendorsByMobileNo(mobileNo, "");
                    if (IsVisorExists != null)
                    {
                        var KeyCode = _blGeneral.RandomNumber(4);
                        var SMSStatus = await _OTPService.SendSMSForVerify(mobileNo, KeyCode, (int)UserTypeEnum.Vendor, (int)MessageReasonEnum.ForgetPassword);
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

                var vendorData = _BlVendor.GetVendorsByMobileNo(resetPasswordViewModel.mobileNo, "");
                if (vendorData != null)
                {

                    var UserData = await _userManager.FindByIdAsync(vendorData.UserId.ToString());
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
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.This_Account_Not_Vendor : Api.Resources.Response.This_Account_Not_Vendor;
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
        #region UpdatePass
        [HttpPost("UpdatePassword")]
        [Consumes("application/x-www-form-urlencoded")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdatePassword([FromForm] string oldPassword, [FromForm] string newPassword)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdVendor();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var VendorData = _BlVendor.GetVendorsByUserId(UserId);
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
        #region VendorProfile
        [CustomAuthorization]
        [HttpGet("VendorProfile")]
        public IActionResult VendorProfile()
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdVendor();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var VendorData = _BlVendor.GetVendorsByUserId(UserId);
                int VendorID = 0;
                if (VendorData != null)
                {
                    VendorID = VendorData.VendorsID;
                }
                if (VendorID != 0)
                {
                    var data = _BlVendor.GetVendorViewModelApi(VendorID, accLanguage, _configuration["VendorImageView"], UserId);
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
        #region Update Vendor Profile
        [CustomAuthorization]
        [HttpPost("UpdateVendorProfile")]
        public async Task<IActionResult> UpdateVendorProfile([FromForm] VendorUpdateProfile createVendorViewModel)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdVendor();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var VendorData = _BlVendor.GetVendorsByUserId(UserId);
                int VendorID = 0;
                if (VendorData != null)
                {
                    VendorID = VendorData.VendorsID;
                }
                if (VendorID != 0)
                {
                        if (!string.IsNullOrWhiteSpace(createVendorViewModel.vendorData.mobileNo))
                        {
                            if (!string.IsNullOrWhiteSpace(createVendorViewModel.vendorData.firstName))
                            {
                                if (createVendorViewModel.vendorData.nationaltyID != 0)
                                {
                                    int countryId = _blCity.GetById(VendorData.CityID, "Region").Region.CountryID;
                                    createVendorViewModel.vendorData.mobileNo = MobileFormat(createVendorViewModel.vendorData.mobileNo, countryId);
                                    if (!A_Valid_Phone_Number(createVendorViewModel.vendorData.mobileNo, countryId))
                                    {
                                        _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Invalid_Phone_number : Api.Resources.Response.Invalid_Phone_number;
                                        _Result.status = false;
                                        _Result.isAuthorize = true;
                                        return Ok(_Result);
                                    }

                                    if (!_BlVendor.IsExistMobile(createVendorViewModel.vendorData.mobileNo, VendorID))
                                    {
                                        if (!string.IsNullOrWhiteSpace(createVendorViewModel.vendorData.email))
                                        {
                                            if (_BlVendor.IsExistEmail(createVendorViewModel.vendorData.email, VendorID))
                                            {
                                                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Email_Is_Exist : Api.Resources.Response.Email_Is_Exist;
                                                _Result.status = false;
                                                _Result.isAuthorize = true;
                                                return Ok(_Result);
                                            }
                                        }
                                                string ImagePath = _configuration["VendorImageSave"];
                                                var profilePhotofileName = string.Empty;
                                                if (createVendorViewModel.profilePhoto != null)
                                                {
                                                    if (createVendorViewModel.profilePhoto.Length > 0)
                                                    {
                                                        profilePhotofileName = string.Concat(Guid.NewGuid().ToString()/*.Take(8)*/) +
                                                                (ContentDispositionHeaderValue.Parse(createVendorViewModel.profilePhoto.ContentDisposition)
                                                                .FileName.Trim('"').Substring(
                                                                    ContentDispositionHeaderValue.Parse(createVendorViewModel.profilePhoto.ContentDisposition)
                                                                    .FileName.Trim('"').IndexOf(".")));
                                                        var profilePhotofullPath = Path.Combine(ImagePath, profilePhotofileName);
                                                        using (var stream = new FileStream(profilePhotofullPath, FileMode.Create))
                                                        {
                                                            createVendorViewModel.profilePhoto.CopyTo(stream);
                                                        }
                                                    }
                                                }
                                                #region User
                                                //Update User
                                                if (createVendorViewModel.vendorData.mobileNo != VendorData.MobileNo)
                                                {
                                                    if (_BlVendor.IsUserNameExists(createVendorViewModel.vendorData.mobileNo))
                                                    {
                                                        var UserData = await _userManager.FindByIdAsync(VendorData.UserId.ToString());
                                                        UserData.UserName = createVendorViewModel.vendorData.mobileNo;
                                                        var identityResult = await _userManager.UpdateAsync(UserData);
                                                    }
                                                }
                                                #endregion
                                                var IsSuccess = _BlVendor.UpdateVendorApi(createVendorViewModel.vendorData, profilePhotofileName, VendorID, UserId);
                                                if (IsSuccess)
                                                {
                                                    var vendordata = _BlVendor.GetVendorsByMobileNo(createVendorViewModel.vendorData.mobileNo, "City,Nationality");
                                                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Update_Success : Api.Resources.Response.Update_Success;
                                                    _Result.status = true;
                                                    _Result.isAuthorize = true;
                                                    _Result.data = new
                                                    {
                                                        firstName = accLanguage == "ar" ? vendordata.FirstNameAr : vendordata.FirstNameEn,
                                                        seconedName = accLanguage == "ar" ? vendordata.SeconedNameAr : vendordata.SeconedNameEn,
                                                        mobileNo = vendordata.MobileNo,
                                                        email = vendordata.Email,
                                                        gender = accLanguage == "ar" ? (vendordata.Gender == (int)Gender.Male ? "Male" : vendordata.Gender == (int)Gender.Female ? "Female" : "Prefer not to specify") : (vendordata.Gender == (int)Gender.Male ? "ذكر" : vendordata.Gender == (int)Gender.Female ? "انثى" : "تفضل عدم التحديد"),
                                                        personalPhoto = _configuration["VendorImageView"] + vendordata.ProfilePic,
                                                        vendorsID = vendordata.VendorsID,
                                                        cityName = accLanguage == "ar" ? vendordata.City.NameAR : vendordata.City.NameEN,
                                                        nationalityName = accLanguage == "ar" ? vendordata.Nationality.NameAR : vendordata.Nationality.NameEN,
                                                    };
                                                    return Ok(_Result);
                                                }
                                                else
                                                {
                                                    var UserDataError = await _userManager.FindByIdAsync(VendorData.UserId.ToString());
                                                    UserDataError.UserName = VendorData.MobileNo;
                                                    var identityResult = await _userManager.UpdateAsync(UserDataError);

                                                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Cant_Create : Api.Resources.Response.Cant_Create;
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
        #region CustomersVendor
        [CustomAuthorization]
        [HttpGet("CustomersVendor")]
        public IActionResult CustomersVendor(int page,string search)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdVendor();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var VendorData = _BlVendor.GetVendorsByUserId(UserId);
                int VendorID = 0;
                if (VendorData != null)
                {
                    VendorID = VendorData.VendorsID;
                }
                if (VendorID != 0)
                {
                    var data = _BlVendor.GetCustomerVendor(VendorID, accLanguage, _configuration["CustomerImageView"], page, search);
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                    _Result.status = true;
                    _Result.isAuthorize = true;
                    _Result.data = new { customersVendor = data };
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
        #region Monthly Target
        [HttpPost("MonthlyTarget")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> MonthlyTarget(decimal monthlyTarget)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdVendor();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var VendorData = _BlVendor.GetVendorsByUserId(UserId);
                int VendorID = 0;
                if (VendorData != null)
                {
                    VendorID = VendorData.VendorsID;
                }
                if (VendorID != 0)
                {
                    var IsSuccess = _BlVendor.MonthyTargetUpdate(monthlyTarget, VendorID, UserId);
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
                        _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Cant_Create : Api.Resources.Response.Cant_Create;
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
        #region Orders
        [AllowAnonymous]
        [HttpGet("OrderDetails")]
        public IActionResult OrderDetails(int orderId)
        {
            var accLanguage = GetUserLang();
            try
            {
                var NationalityList = _BlOrders.GetOrdersViewModelApi(orderId, accLanguage, _configuration["ProductImageView"], _configuration["VendorImageView"]);
                if (NationalityList != null)
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                    _Result.status = true;
                    _Result.isAuthorize = true;
                    _Result.data = new { orderData = NationalityList };
                    return Ok(_Result);
                }
                else
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Data_Not_Found : Api.Resources.Response.Data_Not_Found;
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
        [HttpGet("VendorOrders")]
        public IActionResult VendorOrders(int page, int type)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdVendor();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var VendorData = _BlVendor.GetVendorByUserId(UserId);
                int vendorId = 0;
                if (VendorData != null)
                {
                    vendorId = VendorData.VendorsID;
                }
                if (vendorId != 0)
                {
                    var NationalityList = _BlVendor.VendorOrders(accLanguage, page, _configuration["CustomerImageView"], vendorId, type);
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                    _Result.status = true;
                    _Result.isAuthorize = true;
                    _Result.data = new { orderList = NationalityList };
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
            catch (Exception e)
            {
                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Error + e.ToString() : Api.Resources.Response.Error + e.ToString();
                _Result.status = false;
                _Result.isAuthorize = true;
                return Ok(_Result);
            }
        }
        [CustomAuthorization]
        [HttpPost("ChangeStatus")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ChangeStatus(int orderId, int orderStatusId)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdVendor();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var VendorData = _BlVendor.GetVendorsByUserId(UserId);
                int VendorID = 0;
                if (VendorData != null)
                {
                    VendorID = VendorData.VendorsID;
                }
                if (VendorID != 0)
                {
                    var IsSuccess = _BlVendor.ChangeStatus(orderId, UserId, accLanguage, orderStatusId, (int)CancelReasonEnum.Return_to_Wallet, _configuration["FireBaseKey"]);
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
        [CustomAuthorization]
        [HttpGet("VendorOrderDetails")]
        public IActionResult VendorOrderDetails(int orderId)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdVendor();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var VendorData = _BlVendor.GetVendorByUserId(UserId);
                int vendorId = 0;
                if (VendorData != null)
                {
                    vendorId = VendorData.VendorsID;
                }
                if (vendorId != 0)
                {
                    var NationalityList = _BlOrders.VendorOrderDetails(orderId, accLanguage, _configuration["ProductImageView"], _configuration["CustomerImageView"]);
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                    _Result.status = true;
                    _Result.isAuthorize = true;
                    _Result.data = new { orderData = NationalityList };
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
            catch (Exception e)
            {
                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Error + e.ToString() : Api.Resources.Response.Error + e.ToString();
                _Result.status = false;
                _Result.isAuthorize = true;
                return Ok(_Result);
            }
        }
        [CustomAuthorization]
        [HttpGet("CancelOrders")]
        public IActionResult CancelOrders(int page)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdVendor();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var VendorData = _BlVendor.GetVendorByUserId(UserId);
                int vendorId = 0;
                if (VendorData != null)
                {
                    vendorId = VendorData.VendorsID;
                }
                if (vendorId != 0)
                {
                    var NationalityList = _BlVendor.VendorOrders(accLanguage, page, _configuration["CustomerImageView"], vendorId, 0);
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                    _Result.status = true;
                    _Result.isAuthorize = true;
                    _Result.data = new { orderList = NationalityList };
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
            catch (Exception e)
            {
                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Error + e.ToString() : Api.Resources.Response.Error + e.ToString();
                _Result.status = false;
                _Result.isAuthorize = true;
                return Ok(_Result);
            }
        }
        #endregion
        #region Home
        [CustomAuthorization]
        [HttpGet("VendorHome")]
        public IActionResult VendorHome()
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdVendor();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var VendorData = _BlVendor.GetVendorByUserId(UserId);
                int vendorId = 0;
                if (VendorData != null)
                {
                    vendorId = VendorData.VendorsID;
                }
                if (vendorId != 0)
                {
                    var NationalityList = _BlVendor.GetVendorHome(accLanguage, vendorId, _configuration["CustomerImageView"]);
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                    _Result.status = true;
                    _Result.isAuthorize = true;
                    _Result.data = new { orderData = NationalityList };
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
        [HttpGet("QuestionList")]
        public IActionResult QuestionList(int page)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdVendor();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var VendorData = _BlVendor.GetVendorByUserId(UserId);
                int vendorId = 0;
                if (VendorData != null)
                {
                    vendorId = VendorData.VendorsID;
                }
                if (vendorId != 0)
                {
                    var NationalityList = _BlVendor.GetQuestionList(accLanguage, vendorId, _configuration["CustomerImageView"], _configuration["ProductImageView"], page);
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                    _Result.status = true;
                    _Result.isAuthorize = true;
                    _Result.data = new { questionsList = NationalityList };
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
        [HttpGet("QuestionDetails")]
        public IActionResult QuestionDetails(int questionId)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdVendor();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var VendorData = _BlVendor.GetVendorByUserId(UserId);
                int vendorId = 0;
                if (VendorData != null)
                {
                    vendorId = VendorData.VendorsID;
                }
                if (vendorId != 0)
                {
                    var NationalityList = _BlVendor.QuestionDetaiils(questionId, accLanguage, _configuration["CustomerImageView"], _configuration["ProductImageView"]);
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                    _Result.status = true;
                    _Result.isAuthorize = true;
                    _Result.data = new { questionsList = NationalityList };
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
        [HttpPost("RepleyQuestion")]
        public IActionResult RepleyQuestion(ReleyViewModelApi model)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdVendor();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var VendorData = _BlVendor.GetVendorByUserId(UserId);
                int vendorId = 0;
                if (VendorData != null)
                {
                    vendorId = VendorData.VendorsID;
                }
                if (vendorId != 0)
                {
                    var NationalityList = _BlVendor.UpdateQuestion(model.Id, model.isPublish, accLanguage, _configuration["CustomerImageView"], _configuration["ProductImageView"], model.comment, UserId, _configuration["FireBaseKey"]);
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                    _Result.status = true;
                    _Result.isAuthorize = true;
                    _Result.data = new { questionsList = NationalityList };
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
        #region Rate
        [CustomAuthorization]
        [HttpGet("RateList")]
        public IActionResult RateList(int page)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdVendor();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var VendorData = _BlVendor.GetVendorByUserId(UserId);
                int vendorId = 0;
                if (VendorData != null)
                {
                    vendorId = VendorData.VendorsID;
                }
                if (vendorId != 0)
                {
                    var NationalityList = _BlVendor.GetRateList(accLanguage, vendorId, _configuration["CustomerImageView"], _configuration["ProductImageView"], page);
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                    _Result.status = true;
                    _Result.isAuthorize = true;
                    _Result.data = new { rateList = NationalityList };
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
        [HttpGet("RateDetails")]
        public IActionResult RateDetails(int rateId)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdVendor();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var VendorData = _BlVendor.GetVendorByUserId(UserId);
                int vendorId = 0;
                if (VendorData != null)
                {
                    vendorId = VendorData.VendorsID;
                }
                if (vendorId != 0)
                {
                    var NationalityList = _BlVendor.RateDetaiils(rateId, accLanguage, _configuration["CustomerImageView"], _configuration["ProductImageView"]);
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                    _Result.status = true;
                    _Result.isAuthorize = true;
                    _Result.data = new { rateList = NationalityList };
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
        public class ReleyViewModelApi
        {
            public int Id { get; set; }
            public string comment { get; set; }
            public bool isPublish { get; set; }
        }
        [CustomAuthorization]
        [HttpPost("RepleyRate")]
        public IActionResult RepleyRate(ReleyViewModelApi model)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdVendor();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var VendorData = _BlVendor.GetVendorByUserId(UserId);
                int vendorId = 0;
                if (VendorData != null)
                {
                    vendorId = VendorData.VendorsID;
                }
                if (vendorId != 0)
                {
                    var NationalityList = _BlVendor.UpdateRate(model.Id, accLanguage, _configuration["CustomerImageView"], _configuration["ProductImageView"], model.comment, UserId, _configuration["FireBaseKey"]);
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                    _Result.status = true;
                    _Result.isAuthorize = true;
                    _Result.data = new { rateList = NationalityList };
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
        #region Setting Mnue
        [CustomAuthorization]
        [HttpPost("CancelRegister")]
        public IActionResult CancelRegister()
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdVendor();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var VendorData = _BlVendor.GetVendorByUserId(UserId);
                int vendorId = 0;
                if (VendorData != null)
                {
                    vendorId = VendorData.VendorsID;
                }
                if (vendorId != 0)
                {
                    var NationalityList = _BlVendor.CancelRegister(vendorId, UserId);
                    _blTokens.UpdateTokens(UserId, 0, string.Empty);
                    _blTokens.UpdateUserJWTToken(UserId, string.Empty);
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
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
        [CustomAuthorization]
        [HttpPost("AddVacation")]
        public IActionResult AddVacation(DateTime? startDate, DateTime? endDate, int type)
        {
            var accLanguage = GetUserLang();
            try
            {
                //type == 1 اضافه اجازة 
                //type == 2 ايقاف اجازة 
                var UserLogin = GetUserIdVendor();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var VendorData = _BlVendor.GetVendorByUserId(UserId);
                int vendorId = 0;
                if (VendorData != null)
                {
                    vendorId = VendorData.VendorsID;
                }
                if (vendorId != 0)
                {
                    var NationalityList = _BlVendor.AddVaction(vendorId, UserId, startDate, endDate, type, accLanguage);
                    if (NationalityList == "true")
                    {
                        _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Save_Sucsess : Api.Resources.Response.Save_Sucsess;
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
        #region Invoices
        [CustomAuthorization]
        [HttpGet("VendorInvoices")]
        public IActionResult VendorInvoices(int page, string search, string status, DateTime? fromdate, DateTime? todate)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdVendor();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var VendorData = _BlVendor.GetVendorByUserId(UserId);
                int vendorId = 0;
                if (VendorData != null)
                {
                    vendorId = VendorData.VendorsID;
                }
                if (vendorId != 0)
                {
                    var NationalityList = _BlVendor.GetInvoiceMastetByVendor(vendorId, accLanguage, page, search, status, fromdate, todate);
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                    _Result.status = true;
                    _Result.isAuthorize = true;
                    _Result.data = new { invoiceData = NationalityList };
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
        [HttpGet("InvoiceDetails")]
        public IActionResult InvoiceDetails(int invoiceId, int page, string search)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdVendor();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var VendorData = _BlVendor.GetVendorByUserId(UserId);
                int vendorId = 0;
                if (VendorData != null)
                {
                    vendorId = VendorData.VendorsID;
                }
                if (vendorId != 0)
                {
                    var NationalityList = _BlVendor.GetInvoiceDetails(invoiceId, accLanguage, page, search);
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                    _Result.status = true;
                    _Result.isAuthorize = true;
                    _Result.data = new { invoiceData = NationalityList };
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
        [HttpGet("VendorOrderInvoices")]
        public IActionResult VendorOrderInvoices(int page, bool type, string search, DateTime? fromdate, DateTime? todate)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdVendor();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var VendorData = _BlVendor.GetVendorByUserId(UserId);
                int vendorId = 0;
                if (VendorData != null)
                {
                    vendorId = VendorData.VendorsID;
                }
                if (vendorId != 0)
                {
                    var NationalityList = _BlVendor.InvoicesVendorOrders(accLanguage, page, _configuration["CustomerImageView"], vendorId, type, fromdate, todate, search);
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                    _Result.status = true;
                    _Result.isAuthorize = true;
                    _Result.data = new { invoiceData = NationalityList };
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
        #region Notification
        [CustomAuthorization]
        [HttpGet("GetNotifications")]
        public IActionResult GetNotifications(int page)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdVendor();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var Driverobj = _BlVendor.GetVendorByUserId(UserId);
                if (Driverobj != null)
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                    _Result.status = true;
                    _Result.isAuthorize = true;
                    _Result.data = _BlVendor.GetNotificationListApiViewModelByDriver(Driverobj.VendorsID, accLanguage, page);
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
                var UserLogin = GetUserIdVendor();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var notification = _BlVendor.GetNotificationById(notificationID);
                if (notification != null)
                {
                    if (notification.IsSeen == false)
                    {
                        var IsSuccess = _BlVendor.UpdateNotificationSeen(notificationID, UserId);
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
                var UserLogin = GetUserIdVendor();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var IsSuccess = _BlVendor.UpdateNotificationSeen(UserId);
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
        #region Help
        [HttpGet("HelpQuestionsList")]
        public IActionResult HelpQuestionsList(bool isOrder)
        {
            var accLanguage = GetUserLang();
            try
            {
                var HelpQuestionsList = _blHelpQuestions.GetAllHelpQuestionsViewModelApiByIsForTrip(isOrder, accLanguage, (int)HelpUserTypeEnum.Vendor);
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
        [HttpPost("VendorHelp")]
        public async Task<IActionResult> VendorHelp(int helpQuestionsID, int? orderId, string descripe, IFormFile image)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdVendor();
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

                    var Driverobj = _BlVendor.GetVendorsByUserId(UserId);
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
                        var IsSave = _BlVendorSupport.InsertVendorsHelp(Driverobj.VendorsID, helpQuestionsID, orderId, UserId, descripe, routeImagefileName);
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
        #endregion
        #region Auto_Job
        [HttpGet("CheckCreated")]
        public IActionResult CheckCreated()
        {
            try
            {
                var NationalityList = _BlVendor.CheckAllCreatedOrders(_configuration["FireBaseKey"]);
                _Result.message = Api.Resources.Response.Return_Data_Successfully;
                _Result.status = true;
                _Result.isAuthorize = true;
                _Result.data = NationalityList ;
                return Ok(_Result);
            }
            catch (Exception e)
            {
                _Result.message = e.InnerException != null ? e.InnerException.Message:e.Message;
                _Result.status = false;
                _Result.isAuthorize = true;
                return Ok(_Result);
            }
        }
        #endregion
        #region Dates_Check
        [HttpPost("DatesCheck")]
        public IActionResult DatesCheck()
        {
            try
            {
                var date1 =_blGeneral.GetDateTimeNow();
                var date2 =_blGeneral.GetDateTimeNow_UTC();
                _Result.message = Api.Resources.Response.Return_Data_Successfully;
                _Result.status = true;
                _Result.isAuthorize = true;
                _Result.data = new { GetDateTimeNow = date1, GetDateTimeNow_UTC=date2 };
                return Ok(_Result);
            }
            catch (Exception e)
            {
                _Result.message = e.InnerException != null ? e.InnerException.Message : e.Message;
                _Result.status = false;
                _Result.isAuthorize = true;
                return Ok(_Result);
            }
        }
        #endregion
        #region Vendor Product Question
        [HttpGet("VendorProductQuestion")]
        public IActionResult VendorProductQuestion(int page, int productId)
        {
            var accLanguage = GetUserLang();
            try
            {
                var NationalityList = _BlProduct.GetAllProductQuestionViewModelApiByProduct(productId, accLanguage, page);
                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                _Result.status = true;
                _Result.isAuthorize = true;
                _Result.data = new { productQuestion = NationalityList };
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
        #region VendorIncreaseOrders
        [CustomAuthorization]
        [HttpGet("VendorIncreaseOrders")]
        public IActionResult VendorIncreaseOrders(int page, int type)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdVendor();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var VendorData = _BlVendor.GetVendorByUserId(UserId);
                int vendorId = 0;
                if (VendorData != null)
                {
                    vendorId = VendorData.VendorsID;
                }
                if (vendorId != 0)
                {
                    var NationalityList = _BlVendor.VendorIncreaseOrders(accLanguage, page, _configuration["VendorImageView"], vendorId, type, _configuration["CustomerImageView"]);
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
        #region VendorIncreaseOrders
        [CustomAuthorization]
        [HttpGet("IncreaseOrderDetails")]
        public IActionResult IncreaseOrderDetails(int orderVendorID)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdVendor();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var VendorData = _BlVendor.GetVendorByUserId(UserId);
                int vendorId = 0;
                if (VendorData != null)
                {
                    vendorId = VendorData.VendorsID;
                }
                if (vendorId != 0)
                {
                    var NationalityList = _BlOrders.GetIncreaseOrdersViewModelApi(orderVendorID, accLanguage, _configuration["ProductImageView"]);
                    if(NationalityList != null)
                    {
                        _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                        _Result.status = true;
                        _Result.isAuthorize = true;
                        _Result.data = new { orderData = NationalityList };
                        return Ok(_Result);
                    }
                    else
                    {
                        _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Data_Not_Found : Api.Resources.Response.Data_Not_Found;
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
        #region UpdateIncreaseOrders
        [CustomAuthorization]
        [HttpPost("UpdateIncreaseOrders")]
        public async Task<IActionResult> UpdateIncreaseOrders(UpdateIncreaseOrderViewModelApi model)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdVendor();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var VendorData = _BlVendor.GetVendorByUserId(UserId);
                int vendorId = 0;
                if (VendorData != null)
                {
                    vendorId = VendorData.VendorsID;
                }
                if (vendorId != 0)
                {
                    if (!model.listItem.Any(x => x.quantity == 0))
                    {
                        var IsSuccess = _BlOrders.UpdateIncreaseOrder(model, UserId);
                        if (IsSuccess)
                        {
                            if (model.type == (int)ApprovalQuantityEnum.Approve)
                            {
                                var ordervendor = _BlOrders.GetOrderVendorByIdAndDeleted(model.orderVendorID, "Orders.Customers");
                                if (ordervendor != null)
                                {
                                    var host = _configuration["SiteLink"];
                                    var message = "هلا " + ordervendor.Orders.Customers.FirstName + Environment.NewLine + " تم الموافقة على طلب اذن زيادة الكمية للطلب رقم " + ordervendor.OrderVendorID;
                                    message += Environment.NewLine + " اللينك: " + host + "/Site/Home/completePendingOrder?id=" + ordervendor.Orders.OrdersGuid;

                                    await _OTPService.SendSMSForOrder(ordervendor.Orders.Customers.MobileNo, message, (int)UserTypeEnum.Customer, (int)MessageReasonEnum.Order);

                                }
                            }
                            else if (model.type == (int)ApprovalQuantityEnum.Reject)
                            {
                                var ordervendor = _BlOrders.GetOrderVendorByIdAndDeleted(model.orderVendorID, "Orders.Customers");
                                if (ordervendor != null)
                                {
                                    var host = _configuration["SiteLink"];
                                    var message = "هلا " + ordervendor.Orders.Customers.FirstName + Environment.NewLine + " تم رفض طلب اذن زيادة الكمية للطلب رقم " + ordervendor.OrderVendorID;
                                    message += Environment.NewLine + " اللينك: " + host + "/Site/Home/completePendingOrder?id=" + ordervendor.Orders.OrdersGuid;

                                    await _OTPService.SendSMSForOrder(ordervendor.Orders.Customers.MobileNo, message, (int)UserTypeEnum.Customer, (int)MessageReasonEnum.Order);
                                }
                            }
                            _Result.message = accLanguage == "ar" ? "تم حفظ البيانات بنجاح" : "Order Update Successfully";
                            _Result.status = true;
                            _Result.isAuthorize = true;
                            _Result.data = new { };
                            return Ok(_Result);
                        }
                        else
                        {
                            _Result.message = accLanguage == "ar" ? "عفو لا يمكنك التعديل على هذا الطلب" : "Sorry You Can't Update This Order";
                            _Result.status = false;
                            _Result.isAuthorize = true;
                            _Result.data = new { };
                            return Ok(_Result);
                        }
                    }
                    else
                    {
                        _Result.message = accLanguage == "ar" ? "لا يمكنك اضافة صنف بصفر" : "You Can't Add Item quantity Zero";
                        _Result.status = false;
                        _Result.isAuthorize = true;
                        _Result.data = new { };
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
        #region Quantity Request
        [CustomAuthorization]
        [HttpGet("ListDepartments")]
        public IActionResult ListDepartments()
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdVendor();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var VendorData = _BlVendor.GetVendorsByUserId(UserId);
                int VendorsID = 0;
                if (VendorData != null)
                {
                    VendorsID = VendorData.VendorsID;
                }
                if (VendorsID != 0)
                {
                    var departmentsList = _blDepartments.GetAllDepartmentsViewModelApi(accLanguage);
                    _Result.message = accLanguage == "ar" ? Resources.Response_ar.Return_Data_Successfully : Resources.Response.Return_Data_Successfully;
                    _Result.status = true;
                    _Result.isAuthorize = true;
                    _Result.data = new { departmentsList = departmentsList };
                    return Ok(_Result);
                }
                else
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.This_Account_Not_Vendor : Api.Resources.Response.This_Account_Not_Vendor;
                    _Result.status = false;
                    _Result.isAuthorize = true;
                    return Ok(_Result);
                }

            }
            catch (Exception e)
            {
                _Result.message = accLanguage == "ar" ? Resources.Response_ar.Error + e.ToString() : Resources.Response.Error + e.ToString();
                _Result.status = false;
                _Result.isAuthorize = true;
                return Ok(_Result);
            }
        }
        [CustomAuthorization]
        [HttpGet("ListProducts")]
        public IActionResult ListProducts(int? departmentsID, int page, string search)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdVendor();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var VendorData = _BlVendor.GetVendorsByUserId(UserId);
                int VendorsID = 0;
                if (VendorData != null)
                {
                    VendorsID = VendorData.VendorsID;
                }
                if (VendorsID != 0)
                {
                    int ProductVendorsID = _configuration["ProductVendorsID"] != null ? Convert.ToInt32(_configuration["ProductVendorsID"]) : 0;
                    var blockList = _BlProduct.GetProductBySubCatIDSerach(_configuration["ProductImageView"], departmentsID, ProductVendorsID, page, search, accLanguage);
                    _Result.message = accLanguage == "ar" ? Resources.Response_ar.Return_Data_Successfully : Resources.Response.Return_Data_Successfully;
                    _Result.status = true;
                    _Result.isAuthorize = true;
                    _Result.data = new { productsList = blockList };
                    return Ok(_Result);
                }
                else
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.This_Account_Not_Vendor : Api.Resources.Response.This_Account_Not_Vendor;
                    _Result.status = false;
                    _Result.isAuthorize = true;
                    return Ok(_Result);
                }

            }
            catch (Exception e)
            {
                _Result.message = accLanguage == "ar" ? Resources.Response_ar.Error + e.ToString() : Resources.Response.Error + e.ToString();
                _Result.status = false;
                _Result.isAuthorize = true;
                return Ok(_Result);
            }
        }
        [CustomAuthorization]
        [HttpPost("AddRequestQuantity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult AddRequestQuantity(OrderRequestViewModel order)
        {
            var accLanguage = GetUserLang();
            try
            {
                var UserLogin = GetUserIdVendor();
                if (UserLogin == "incorrect Authorization Value")
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                int UserId = int.Parse(UserLogin.ToString());
                var VendorData = _BlVendor.GetVendorByUserId(UserId);
                int VendorsID = 0;
                if (VendorData != null)
                {
                    VendorsID = VendorData.VendorsID;
                }
                if (VendorsID != 0)
                {
                    if (order.order.Count() > 0)
                    {
                        var IsSuccess = _BlVendor.InsertRequestQuantity(order.order, VendorsID, UserId);
                        if (IsSuccess)
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
                            _Result.message = "خطأ في حفظ الطلب";
                            _Result.status = false;
                            _Result.isAuthorize = true;
                            return Ok(_Result);
                        }
                    }
                    else
                    {
                        _Result.message = "لا يوجد منتجات تم اختيارها";
                        _Result.status = false;
                        _Result.isAuthorize = true;
                        return Ok(_Result);
                    }
                }
                else
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.This_Account_Not_Vendor : Api.Resources.Response.This_Account_Not_Vendor;
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
