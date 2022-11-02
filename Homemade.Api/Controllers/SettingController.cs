using Homemade.Api.Authentication;
using Homemade.Api.Model;
using Homemade.BLL;
using Homemade.BLL.Order;
using Homemade.BLL.Setting;
using Homemade.BLL.Vendor;
using Homemade.BLL.ViewModel.Setting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Homemade.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingController : ControllerBase
    {
        #region Declarations
        private readonly IConfiguration _configuration;
        private readonly BlBlock _BlBlock;
        private readonly BlBanks _BlBanks;
        private readonly BlAddressTypes _BlAddressTypes;
        private readonly BlCity _blCity;
        private readonly BlRegion _BlRegion;
        private readonly BlNationality _blNationality;
        private readonly BlPackage _BlPackage;
        private readonly BlActivity _BlActivity;
        private readonly BlCountry _BlCountry;
        private readonly BlDepartments _BlDepartments;
        private FixedResultMessage _Result = new FixedResultMessage();
        private readonly BlVendor _BlVendor;
        private readonly BlTokens _blTokens;
        private readonly BlOrders _BlOrders;
        private readonly BlMainPageDetails _blMainPageDetails;
        private readonly BlRegionCity _BlRegionCity;
        private readonly BlProduct _blProduct;
        private readonly BlConfiguration _blConfiguration;

        public SettingController(BlOrders BlOrders, BlVendor BlVendor, BlTokens BlTokens, BlBanks BlBanks, BlPackage BlPackage, BlActivity BlActivity, BlAddressTypes BlAddressTypes, BlCity blCity, BlNationality blNationality, BlRegion blRegion, BlDepartments blDepartments, IConfiguration configuration, BlCountry BlCountry, BlBlock BlBlock, BlMainPageDetails blMainPageDetails, BlRegionCity blRegionCity, BlProduct blProduct, BlConfiguration blConfiguration)
        {
            _blCity = blCity;
            _blNationality = blNationality;
            _configuration = configuration;
            _BlDepartments = blDepartments;
            _BlCountry = BlCountry;
            _BlRegion = blRegion;
            _BlBlock = BlBlock;
            _BlAddressTypes = BlAddressTypes;
            _BlPackage = BlPackage;
            _BlActivity = BlActivity;
            _BlBanks = BlBanks;
            _BlVendor = BlVendor;
            _blTokens = BlTokens;
            _BlOrders = BlOrders;
            _blMainPageDetails = blMainPageDetails;
            _BlRegionCity = blRegionCity;
            _blProduct = blProduct;
            _blConfiguration = blConfiguration;
        }
        #endregion
        private string GetUserIdVendor()
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
        #region Get City
        [AllowAnonymous]
        [HttpGet("GetCities")]
        public IActionResult GetCities(int? countryId)
        {
            var accLanguage = GetUserLang();
            try
            {
                var cityList = _blCity.GetCitiesViewModel(accLanguage, countryId).OrderByDescending(e => e.cityID);
                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                _Result.status = true;
                _Result.isAuthorize = true;
                _Result.data = new { cityList = cityList };
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
        #region Get Nationality
        [AllowAnonymous]
        [HttpGet("NationalityList")]
        public IActionResult NationalityList()
        {
            var accLanguage = GetUserLang();
            try
            {
                var NationalityList = _blNationality.GetNationalities(accLanguage);
                if (NationalityList.Any())
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                    _Result.status = true;
                    _Result.isAuthorize = true;
                    _Result.data = new { nationalityList = NationalityList };
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
        [AllowAnonymous]
        [HttpGet("BankList")]
        public IActionResult BankList()
        {
            var accLanguage = GetUserLang();
            try
            {
                var NationalityList = _BlBanks.BanksViewModelApiList(accLanguage);
                if (NationalityList.Any())
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                    _Result.status = true;
                    _Result.isAuthorize = true;
                    _Result.data = new { bankList = NationalityList };
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
        #endregion
        #region Departments
        [AllowAnonymous]
        [HttpGet("DepartmentList")]
        public IActionResult DepartmentList()
        {
            var accLanguage = GetUserLang();
            try
            {
                var NationalityList = _BlDepartments.GetAllDepartmentsApi(accLanguage, _configuration["DeptImageView"]);
                var minmaxprice = _blProduct.GetProductPriceViewModelApi();
                if (NationalityList.Any())
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                    _Result.status = true;
                    _Result.isAuthorize = true;
                    _Result.data = new { productPrice = minmaxprice,
                        departmentList = NationalityList };
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
        #endregion
        #region Get Country
        [AllowAnonymous]
        [HttpGet("GetCountries")]
        public IActionResult GetCountries()
        {
            var accLanguage = GetUserLang();
            try
            {
                var cityList = _BlCountry.GetCountryList(accLanguage);
                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                _Result.status = true;
                _Result.isAuthorize = true;
                _Result.data = new { countryList = cityList };
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
        #region Get KeyWords
        [AllowAnonymous]
        [HttpGet("GetKeyWords")]
        public IActionResult GetKeyWords(int departmentID)
        {
            var accLanguage = GetUserLang();
            try
            {
                var cityList = _BlCountry.GetKeyWordsList(accLanguage, departmentID);
                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                _Result.status = true;
                _Result.isAuthorize = true;
                _Result.data = new { keyWordsList = cityList };
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
        #region Get Region
        [AllowAnonymous]
        [HttpGet("GetRegion")]
        public IActionResult GetRegion(int countryId)
        {
            var accLanguage = GetUserLang();
            try
            {
                var cityList = _BlRegion.GetRegionsApiByCountryID(accLanguage, countryId);
                 _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                 _Result.status = true;
                 _Result.isAuthorize = true;
                 _Result.data = new { regionList = cityList };
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
        #region Get Cities Region
        [AllowAnonymous]
        [HttpGet("GetCitiesRegion")]
        public IActionResult GetCitiesRegion(int regionId)
        {
            var accLanguage = GetUserLang();
            try
            {
                var cityList = _blCity.GetCitiesViewModelRegion(accLanguage, regionId).OrderByDescending(e=> e.cityID);
                 _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                 _Result.status = true;
                 _Result.isAuthorize = true;
                 _Result.data = new { cityList = cityList };
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
        #region Get Blocks City
        [AllowAnonymous]
        [HttpGet("GetBlocksCity")]
        public IActionResult GetBlocksCity(int cityId)
        {
            var accLanguage = GetUserLang();
            try
            {
                var cityList = _BlBlock.GetAllBlocksByListCityApi(accLanguage, cityId);
                 _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                 _Result.status = true;
                 _Result.isAuthorize = true;
                 _Result.data = new { blockList = cityList };
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
        #region Get Address Type
        [AllowAnonymous]
        [HttpGet("GetAddressTypes")]
        public IActionResult GetAddressTypes()
        {
            var accLanguage = GetUserLang();
            try
            {
                var cityList = _BlAddressTypes.GetAddressTypesApi(accLanguage);
                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                _Result.status = true;
                _Result.isAuthorize = true;
                _Result.data = new { addressTypesList = cityList };
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
        #region mainPages
        [AllowAnonymous]
        [HttpGet("MainPageList")]
        public IActionResult MainPageList(int Type) 
        {
            var accLanguage = GetUserLang();
            try
            {
               var cityList = _BlCountry.GetmainByType(Type,accLanguage);
               _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
               _Result.status = true;
               _Result.isAuthorize = true;
               _Result.data = new { mainPageList = cityList };
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
        #region About
        [AllowAnonymous]
        [HttpGet("GetAbout")]
        public IActionResult GetAbout()
        {
            var accLanguage = GetUserLang();
            try
            {
                var cityList = _BlCountry.GetAbout(accLanguage, _configuration["AboutImageView"]);
                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                _Result.status = true;
                _Result.isAuthorize = true;
                _Result.data = new { mainPageList = cityList };
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
        #region Activites
        [AllowAnonymous]
        [HttpGet("ActivityList")]
        public IActionResult ActivityList()
        {
            var accLanguage = GetUserLang();
            try
            {
                var cityList = _BlActivity.GetActivityApi(accLanguage);
                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                _Result.status = true;
                _Result.isAuthorize = true;
                _Result.data = new { mainPageList = cityList };
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
        #region PackageList
        [AllowAnonymous]
        [HttpGet("PackageList")]
        public IActionResult PackageList()
        {
            var accLanguage = GetUserLang();
            try
            {
                var cityList = _BlPackage.GetPackageApi(accLanguage);
                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                _Result.status = true;
                _Result.isAuthorize = true;
                _Result.data = new { mainPageList = cityList };
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
        #region  Get Order Status
        [CustomAuthorization]
        [HttpGet("GetOrderStatus")]
        public IActionResult GetOrderStatus()
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
                     var cityList = _blCity.GetOrderStatusApi(accLanguage);
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                    _Result.status = true;
                    _Result.isAuthorize = true;
                    _Result.data = new { statusList = cityList };
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
        [HttpGet("GetOrderStatusById")]
        public IActionResult GetOrderStatusById(int orderId)
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
                    var cityList = _blCity.GetOrderStatusApi(accLanguage);
                    var NationalityList = _BlOrders.VendorOrderDetails(orderId);
                    if (NationalityList.CaptainTypeId == (int)CaptainTypeEnum.Home_Made)
                    {
                        cityList = cityList.Where(e => e.OrderStatusType == (int)OrderStatusTypeEnum.HomeMade 
                        && e.Arrange > NationalityList.OrderStatus.Arrange).ToList();
                    }
                    else
                    {
                        cityList = cityList.Where(e => e.OrderStatusType != null
                         && e.Arrange > NationalityList.OrderStatus.Arrange).ToList();
                    }

                    //if (cityList.Count > 0)
                    //{
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                    _Result.status = true;
                    _Result.isAuthorize = true;
                    _Result.data = new { statusList = cityList };
                    return Ok(_Result);
                }
                else
                {
                    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.incorrect_Authorization_Value : Api.Resources.Response.incorrect_Authorization_Value;
                    _Result.status = false;
                    _Result.isAuthorize = false;
                    return Ok(_Result);
                }
                //}
                //else
                //{
                //    _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Data_Not_Found : Api.Resources.Response.Data_Not_Found;
                //    _Result.status = false;
                //    _Result.isAuthorize = true;
                //    return Ok(_Result);
                //}
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
        #region Mobile Version
        [HttpGet("GetVersion")]
        public IActionResult GetVersion(int type, bool mobile)
        {
            var accLanguage = GetUserLang();
            try
            {
                var cityList = _blCity.GetVersionMobile(type, mobile);
                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                _Result.status = true;
                _Result.isAuthorize = true;
                _Result.data = new { statusList = cityList };
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
        public class UpdateVersionModel
        {
            public int type { get; set; }
            public bool mobile { get; set; }
            public string version { get; set; }
        }
        [HttpPost("UpdateVersion")]
        public IActionResult UpdateVersion(UpdateVersionModel model)
        {
            var accLanguage = GetUserLang();
            try
            {
                var cityList = _blCity.updatemobileVersion(model.type, model.mobile, model.version);
                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                _Result.status = true;
                _Result.isAuthorize = true;
                _Result.data = new { statusList = cityList };
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
        #region Product Type
        [HttpGet("ProductTypeList")]
        public IActionResult ProductTypeList()
        {
            var accLanguage = GetUserLang();
            try
            {
                List<ProductTypeViewModelApi> productTypeViewModelApi = new List<ProductTypeViewModelApi>();
                productTypeViewModelApi.Add(new ProductTypeViewModelApi { typeID = (int)ProductTypeEnum.Food, name = accLanguage == "ar" ? "اكل" : "Food" });
                productTypeViewModelApi.Add(new ProductTypeViewModelApi { typeID = (int)ProductTypeEnum.Product_Ready_Shipping, name = accLanguage == "ar" ? "منتج جاهز للشحن" : "Product Ready Shipping" });
                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                _Result.status = true;
                _Result.isAuthorize = true;
                _Result.data = new { productTypeData = productTypeViewModelApi };
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
        #region Check All Schedule
        [AllowAnonymous]
        [HttpGet("CheckSchedule")]
        public IActionResult CheckSchedule()
        {
            var accLanguage = GetUserLang();
            try
            {
                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                _Result.status = true;
                _Result.isAuthorize = true;
                _Result.data = new { Status = _configuration["CheckScheduleStatus"] };
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
        #region Get Terms And Conditions
        [AllowAnonymous]
        [HttpGet("GetTermsAndConditions")]
        public IActionResult GetTermsAndConditions(int? typeId)
        {
            var accLanguage = GetUserLang();
            try
            {
                var List = _blMainPageDetails.GetAllTermsConditionsAPIViewModel(typeId,accLanguage);
                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                _Result.status = true;
                _Result.isAuthorize = true;
                _Result.data = new { list = List };
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
        #region  Get Region City
        [AllowAnonymous]
        [HttpGet("GetRegionCity")]
        public IActionResult GetRegionCity()
        {
            var accLanguage = GetUserLang();
            try
            {
                var cityList = _BlRegionCity.GetRegionCityApi(accLanguage);
                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                _Result.status = true;
                _Result.isAuthorize = true;
                _Result.data = new { regionList = cityList };
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
        #region  Get Social Information
        [AllowAnonymous]
        [HttpGet("GetSocialInformation")]
        public IActionResult GetSocialInformation()
        {
            var accLanguage = GetUserLang();
            try
            {
                var SoicalMedia = _blConfiguration.GetSocialInformationViewModelApi();
                _Result.message = accLanguage == "ar" ? Api.Resources.Response_ar.Return_Data_Successfully : Api.Resources.Response.Return_Data_Successfully;
                _Result.status = true;
                _Result.isAuthorize = true;
                _Result.data = SoicalMedia;
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
    }
}
