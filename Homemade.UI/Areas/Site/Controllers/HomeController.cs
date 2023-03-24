using Homemade.BLL;
using Homemade.BLL.Customer;
using Homemade.BLL.Order;
using Homemade.BLL.Setting;
using Homemade.BLL.SMS;
using Homemade.BLL.Vendor;
using Homemade.BLL.ViewModel.Customer;
using Homemade.BLL.ViewModel.Site;
using Homemade.BLL.ViewModel.User;
using Homemade.BLL.ViewModel.Vendor;
using Homemade.Model;
using Homemade.UI.InfraStructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Homemade.BLL.General;
using System.Net.Http;
using Homemade.BLL.Site;
using Homemade.Model.Site;
using Homemade.BLL.Purchase;
using Newtonsoft.Json;
using Homemade.BLL.ViewModel.MyFatoora;
using RestSharp;
using Homemade.BLL.ViewModel.MyFatoora.FatoraPayment;
using Homemade.Model.BankTransaction;
using Microsoft.AspNetCore.SignalR;
using Homemade.BLL.ViewModel.Order;
using Homemade.BLL.ViewModel.Identity;
using System.Globalization;
using System.Threading;
using Homemade.Model.Order;
using QRCoder;
using System.Drawing;
using System.Text;
using Homemade.BLL.ViewModel.Tab;
using EnumsNET;

namespace Homemade.UI.Areas.Site.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        #region Declarations
        private readonly BlDepartments _blDepartments;
        private readonly BlProduct _blProduct;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly BlCustomer _blCustomer;
        private readonly BlCustomerBalance _blCustomerBalance;
        private readonly BlCity _blCity;
        private readonly BlTokens _blTokens;
        private readonly BlCustomerAddress _BlCustomerAddress;
        private readonly ResultMessage _result;
        private readonly BlBlock _BlBlock;
        private readonly BlActivity _BlActivity;
        private readonly BlBanks _BlBanks;
        private readonly BlPackage _BlPackage;
        private readonly BlVendor _BlVendor;
        private readonly BLGeneral _BLGeneral;
        private readonly BlInqueries _blInqueries;
        private readonly BlOrders _blOrders;
        private readonly BlNationality _blNationality;
        private readonly BlSubscribe _blSubscribe;
        private readonly BLSite _BLSite;
        private readonly BlMainPageDetails _blMainPageDetails;
        private readonly BLTransaction _BLTransaction;
        private readonly OTPService _OTPService;
        private readonly BlConfiguration _blConfiguration;
        private readonly BlCompanyWorkingHours _blCompanyWorkingHours;
        public HomeController(BlCustomerBalance BlCustomerBalance, BlDepartments blDepartments, BlProduct blProduct, IConfiguration configuration, SignInManager<User> signInManager, UserManager<User> userManager,BlCustomer blCustomer, BlCity blCity, BlTokens blTokens, BlCustomerAddress blCustomerAddress, ResultMessage resultMessage,
          BlBlock blBlock,BlActivity blActivity, BlBanks blBanks, BlPackage blPackage, BlVendor blVendor, BLGeneral bLGeneral, BlInqueries blInqueries, BlOrders blOrders, BlNationality blNationality, BlSubscribe blSubscribe, BLSite bLSite, BlMainPageDetails blMainPageDetails,BLTransaction bLTransaction, OTPService oTPService, BlConfiguration blConfiguration, BlCompanyWorkingHours blCompanyWorkingHours)
        {
            _blDepartments = blDepartments;
            _blProduct = blProduct;
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
            _blCustomer = blCustomer;
            _blCustomerBalance = BlCustomerBalance;
            _blCity = blCity;
            _blTokens = blTokens;
            _BlCustomerAddress = blCustomerAddress;
            _result = resultMessage;
            _BlBlock = blBlock;
            _BlActivity = blActivity;
            _BlBanks = blBanks;
            _BlPackage = blPackage;
            _BlVendor = blVendor;
            _BLGeneral = bLGeneral;
            _blInqueries = blInqueries;
            _blOrders = blOrders;
            _blNationality = blNationality;
            _blSubscribe = blSubscribe;
            _BLSite = bLSite;
            _blMainPageDetails = blMainPageDetails;
            _BLTransaction = bLTransaction;
            this._OTPService = oTPService;
            _blConfiguration = blConfiguration;
            _blCompanyWorkingHours = blCompanyWorkingHours;
        }
        #endregion
        #region View
        public IActionResult Maintenance()
        {
            return View();
        }
        public IActionResult GetMaintenancePartial()
        {
            return PartialView("_MaintanancePartail");
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult categories()
        {
            return View();
        }
        public IActionResult category_items(Guid? id)
        {
            if (id.HasValue)
            {
                var model = _blDepartments.GetSiteDepartmentsViewModelByGuid(id.Value, InfraStructure.Utility.CurrentLanguageCode);
                if (model != null)
                {
                    return View(model);
                }
            }
            return Redirect(Url.Action("Index", "Home", new { Area = "Site" }));
        }
        public IActionResult produc_default(Guid? id)
        {
            if (id.HasValue)
            {
                var customer = _blCustomer.GetCustomerByUser(User.GetUserIdInt());
                int CustomerID = customer != null ? customer.CustomersID : 0;
                var model = _blProduct.GetSiteProductDetailsViewModelByGuid(id.Value, CustomerID, InfraStructure.Utility.CurrentLanguageCode, _configuration["VendorImageView"], _configuration["ProductImageView"]);
                if (model != null)
                {
                    return View(model);
                }
            }
            return Redirect(Url.Action("Index", "Home", new { Area = "Site" }));
        }
        public IActionResult vendor(Guid id)
        {
            ViewBag.VendorsGuid = id;
            return View();
        }

        public IActionResult Wizzard()
        {
            return View();
        }
        public IActionResult Terms()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Help()
        {
            return View();
        }
        public IActionResult CommonQuestions()
        {
            return View();
        }
        public IActionResult AboutUs()
        {
            var model = _blMainPageDetails.GetMainPageDetailsViewModelForAbout(Utility.CurrentLanguageCode, 11, _configuration["AboutImageView"]);
            return View(model);
        }
        [Authorize]
        public IActionResult PersonalData()
        {
            var model = _blCustomer.GetPersonalDataViewModelByUser(User.GetUserIdInt());
            if (model != null)
            {
                return View(model);
            }
            return Redirect(Url.Action("Index", "Home", new { Area = "Site" }));
        }
        public IActionResult ChangePassword()
        {
            var model = _blCustomer.GetCustomerByUser(User.GetUserIdInt());
            if (model != null)
            {
                return View();
            }
            return Redirect(Url.Action("Index", "Home", new { Area = "Site" }));
        }
        [Authorize]
        [HttpGet]
        public IActionResult DeliveryAddresses()
        {
            return View();
        }
        [Authorize]
        public IActionResult OrderList()
        {
            return View();
        }
        [Authorize]
        public IActionResult FavoriteProducts()
        {
            var model = _blProduct.GetAllCustomerFavouritesByUserCustomer(User.GetUserIdInt(), InfraStructure.Utility.CurrentLanguageCode, _configuration["VendorImageView"], _configuration["ProductImageView"]);
            if (model != null)
            {
                return View(model);
            }
            return Redirect(Url.Action("Index", "Home", new { Area = "Site" }));
        }
        [Authorize]
        public IActionResult Reviews()
        {
            var model = _blCustomer.GetSiteReviewQeustionsViewModelByUserCustomer(User.GetUserIdInt(), InfraStructure.Utility.CurrentLanguageCode, _configuration["VendorImageView"]);
            if (model != null)
            {
                return View(model);
            }
            return Redirect(Url.Action("Index", "Home", new { Area = "Site" }));
        }
        [Authorize]
        public IActionResult Wallet()
        {
            var model = _blCustomer.GetPersonalDataViewModelByUser(User.GetUserIdInt());
            if (model != null)
            {
                return View();
            }
            return Redirect(Url.Action("Index", "Home", new { Area = "Site" }));
        }
        [Authorize]
        public IActionResult Products()
        {
            if (HttpContext.Session.Keys.Contains("SessionSearchProductsViewModel"))
            {
                SearchProductsViewModel model = HttpContext.Session.GetObject<SearchProductsViewModel>("SessionSearchProductsViewModel");
                ViewData["SearchDepartmentID"] = model.SearchDepartmentID;
                ViewData["SearchProducts"] = model.SearchProducts;
            }
            HttpContext.Session.Remove("SessionSearchProductsViewModel");
            return View();
        }
        public IActionResult AllVendor()
        {
            return View();
        }
        [Authorize]
        public IActionResult PendingOrderList()
        {
            return View();
        }
        [Authorize]
        public IActionResult CustomerVendors()
        {
            return View();
        }
        #endregion
        #region Action
        [HttpPost, ValidateAntiForgeryToken]
        public JsonResult Help(SiteInqueriesViewModel viewModel)
        {
            ResultMessage _Result = new ResultMessage();
            try
            {
                if (ModelState.IsValid)
                {
                    bool IsSave = _blInqueries.Insert(viewModel);
                    if (IsSave)
                    {
                        _Result.Status = true;
                        _Result.ResultType = ResultMessageType.success.ToString();
                        _Result.Message = Resources.Homemade.The_inquiry_has_been_sent_successfully;
                        return Json(_Result);
                    }
                    else
                    {
                        _Result.Status = false;
                        _Result.ResultType = ResultMessageType.error.ToString();
                        _Result.Message = Resources.Homemade.FailSendMessage;
                        return Json(_Result);
                    }
                }
                else
                {
                    return Json(new ResultMessage
                    {
                        Status = false,
                        ResultType = ResultMessageType.error.ToString(),
                        Message = Resources.Homemade.InsertValidDataMessage
                    });
                }
            }
            catch (Exception e)
            {
                return Json(new ResultMessage
                {
                    Status = false,
                    ResultType = ResultMessageType.error.ToString(),
                    Message = e.Message
                });
            }
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PersonalData(PersonalDataViewModel model)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(model.MobileNo))
                {
                    if (!string.IsNullOrWhiteSpace(model.Name))
                    {
                        if (model.NationalityID != 0)
                        {
                            int countryId = _blCity.GetById(model.CityID, "Region").Region.CountryID;
                            model.MobileNo = MobileFormat(model.MobileNo, countryId);
                            if (!A_Valid_Phone_Number(model.MobileNo, countryId))
                            {
                                _result.Message = Resources.Homemade.Invalid_Phone_number;
                                _result.Status = false;
                                _result.ResultType = ResultMessageType.error.ToString();

                                return Json(_result);
                            }

                            if (!_blCustomer.IsExistMobile(model.MobileNo, model.CustomersID))
                            {
                                if (!string.IsNullOrWhiteSpace(model.Email))
                                {
                                    if (_blCustomer.IsExistEmail(model.Email, model.CustomersID))
                                    {
                                        _result.Message = Resources.Homemade.EmailExists;
                                        _result.Status = false;
                                        _result.ResultType = ResultMessageType.error.ToString();

                                        return Json(_result);
                                    }
                                }

                                try
                                {

                                    var IsSuccess = _blCustomer.UpdatePersonalData(model, User.GetUserIdInt());
                                    if (IsSuccess)
                                    {
                                        _result.Message = Resources.Homemade.SuccessSaveMessage;
                                        _result.Status = true;
                                        _result.ResultType = ResultMessageType.success.ToString();

                                        return Json(_result);
                                    }

                                    else
                                    {
                                        _result.Message = Resources.Homemade.FailSaveMessage;
                                        _result.Status = false;
                                        _result.ResultType = ResultMessageType.error.ToString();
                                        return Json(_result);

                                    }
                                }
                                catch (Exception ex)
                                {
                                    _result.Message = Resources.Homemade.FailSaveMessage + ex.Message.ToString();
                                    _result.Status = false;
                                    _result.ResultType = ResultMessageType.error.ToString();
                                    return Json(_result);
                                }


                            }
                            else
                            {
                                _result.Message = Resources.Homemade.MobileNumberExists;
                                _result.Status = false;
                                _result.ResultType = ResultMessageType.error.ToString();
                                return Json(_result);
                            }

                        }
                        else
                        {
                            _result.Message = Resources.Homemade.Choose_Nationality;
                            _result.Status = false;
                            _result.ResultType = ResultMessageType.error.ToString();
                            return Json(_result);
                        }

                    }
                    else
                    {
                        _result.Message = Resources.Homemade.Name_Not_Valid;
                        _result.Status = false;
                        _result.ResultType = ResultMessageType.error.ToString();
                        return Json(_result);
                    }

                }
                else
                {
                    _result.Message = Resources.Homemade.Mobile_Not_Valid;
                    _result.Status = false;
                    _result.ResultType = ResultMessageType.error.ToString();
                    return Json(_result);
                }

            }
            catch (Exception ex)
            {
                _result.Message = Resources.Homemade.Error + Environment.NewLine + ex.Message.ToString();
                _result.Status = false;
                _result.ResultType = ResultMessageType.error.ToString();
                return Json(_result);
            }
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(model.OldPassword) && !string.IsNullOrWhiteSpace(model.NewPassword) && !string.IsNullOrWhiteSpace(model.ConfirmNewPassword))
                {
                    if (model.NewPassword.Length > 5)
                    {
                        var user = await _userManager.GetUserAsync(User);
                        if (user != null)
                        {
                            var checkPassword = await _userManager.CheckPasswordAsync(user, model.OldPassword);
                            if (checkPassword)
                            {
                                #region User
                                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.NewPassword);
                                var identityResult = await _userManager.UpdateAsync(user);
                                #endregion
                                if (identityResult.Succeeded)
                                {
                                    _result.Message = Resources.Homemade.Success_Update_Password;
                                    _result.Status = true;
                                    _result.ResultType = ResultMessageType.success.ToString();
                                    return Json(_result);
                                }
                                else
                                {
                                    _result.Message = Resources.Homemade.FaildSaveUser;
                                    _result.Status = false;
                                    _result.ResultType = ResultMessageType.error.ToString();
                                    return Json(_result);
                                }
                            }
                            else
                            {
                                _result.Message = Resources.Homemade.InvalidPassword;
                                _result.Status = false;
                                _result.ResultType = ResultMessageType.error.ToString();
                                return Json(_result);
                            }
                        }
                        else
                        {
                            _result.Message = Resources.Homemade.Error;
                            _result.Status = false;
                            _result.ResultType = ResultMessageType.error.ToString();
                            return Json(_result);
                        }
                    }
                    else
                    {
                        _result.Message = Resources.Homemade.Please_Insert_New_Password_Valid;
                        _result.Status = false;
                        _result.ResultType = ResultMessageType.error.ToString();
                        return Json(_result);
                    }
                }
                else
                {
                    _result.Message = Resources.Homemade.Enter_Password;
                    _result.Status = false;
                    _result.ResultType = ResultMessageType.error.ToString();
                    return Json(_result);
                }
            }
            catch (Exception ex)
            {
                _result.Message = Resources.Homemade.Error + Environment.NewLine + ex.Message.ToString();
                _result.Status = false;
                _result.ResultType = ResultMessageType.error.ToString();
                return Json(_result);
            }
        }
        [HttpPost]
        public JsonResult DeliveryAddresses(SiteCustomerLocationViewModel model)
        {
            try
            {
                #region SaveData
                string OperationType = "add";

                model.Lat = double.Parse(model.LatStr, CultureInfo.InvariantCulture);
                model.Lng = double.Parse(model.LngStr, CultureInfo.InvariantCulture);

                bool IsSuccess;
                int UserId = User.GetUserIdInt();
                var customer = _blCustomer.GetCustomerByUser(UserId);
                if (customer != null)
                {
                    model.CustomersID = customer.CustomersID;
                }
                if (model.RegionID == 0 || model.CityID == 0 || model.BlockID == 0)
                {
                    _result.ResultType = ResultMessageType.error.ToString();
                    _result.Message = "فضلا قم باختيار موقع صالح من الخريطة ";
                    return Json(new { _result, OperationType });

                }
                if (model.CustomerLocationID == 0)
                {
                    #region Add


                    IsSuccess = _blCustomer.InsertCustomerLocation(model, UserId);
                    #endregion
                }
                else
                {
                    #region Update

                    IsSuccess = _blCustomer.UpdateCustomerLocation(model, UserId);
                    OperationType = "update";
                    #endregion
                }
                if (IsSuccess)
                {
                    _result.ResultType = ResultMessageType.success.ToString();
                    _result.Message = Resources.Homemade.SuccessSaveMessage;
                }
                else
                {
                    _result.ResultType = ResultMessageType.error.ToString();
                    _result.Message = Resources.Homemade.FailSaveMessage;
                }
                #endregion
                return Json(new { _result, OperationType });
            }
            catch (System.Exception ex)
            {
                _result.ResultType = ResultMessageType.error.ToString();
                _result.Message = ex.Message.ToString();
                return Json(new { _result });
            }
        }
        [HttpPost]
        public JsonResult DeleteLocation(int id)
        {
            var result = false;
            try
            {
                result = _blCustomer.DeleteCustomerLocation(id, User.GetUserIdInt());
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

        #endregion
        #region Helpers
        public IActionResult GetProductItemsPartial(string searchProducts, string departmentsCheck, string productTypeCheck, int? minPrice, int? maxPrice, double? rating, int page = 1)
        {
            var model = _blProduct.GetAllSiteProductViewModelByDepartments(searchProducts, departmentsCheck, productTypeCheck, minPrice, maxPrice, rating, InfraStructure.Utility.CurrentLanguageCode, _configuration["VendorImageView"], _configuration["ProductImageView"])
                 .Skip(Convert.ToInt32(page - 1) * 21).Take(((Convert.ToInt32(page) * 21)) - (Convert.ToInt32(page - 1) * 21));
            return PartialView("_ProductItemsPartial", model);
        }
        public IActionResult GetProductItemsCategoryPartial(int departmentsId, string searchProducts, int page = 1)
        {
            var model = _blProduct.GetAllSiteProductViewModelByDepartments(departmentsId, searchProducts, InfraStructure.Utility.CurrentLanguageCode, _configuration["VendorImageView"], _configuration["ProductImageView"])
                                 .Skip(Convert.ToInt32(page - 1) * 28).Take(((Convert.ToInt32(page) * 28)) - (Convert.ToInt32(page - 1) * 28));
            return PartialView("_ProductItemsCategoryPartial", model);
        }
        public IActionResult GetVendorProductsPartial(int vendorsID, int page = 1)
        {
            var model = _blProduct.GetAllSiteProductViewModelByVendorsID(vendorsID, InfraStructure.Utility.CurrentLanguageCode, _configuration["VendorImageView"], _configuration["ProductImageView"])
                              .Where(e => !e.IsHidden).OrderByDescending(e => e.IsFixed)
                              .Skip(Convert.ToInt32(page - 1) * 28).Take(((Convert.ToInt32(page) * 28)) - (Convert.ToInt32(page - 1) * 28));
            return PartialView("_VendorProductsPartial", model);
        }
        public IActionResult GetSiteVendorsViewModel(string searchVendors)
        {
            var model = _BlVendor.GetAllSiteVendorsViewModel(searchVendors, InfraStructure.Utility.CurrentLanguageCode, _configuration["VendorImageView"], _configuration["ProductImageView"]);
            return PartialView("_VendorsPartial", model);
        }
        public IActionResult GetSiteOrdersPartial(int type, bool allOrder)
        {
            var model = _blOrders.GetAllSiteOrdersViewModelByUserCustomer(User.GetUserIdInt(), InfraStructure.Utility.CurrentLanguageCode, type, _configuration["CustomerImageView"], _configuration["VendorImageView"]);
            if (!allOrder)
            {
                var DataTake = model.Take(10);
                return PartialView("_SiteOrdersPartial", DataTake);
            }
            return PartialView("_SiteOrdersPartial", model);
        }
        public IActionResult GetSiteOrdersPendingPartial(bool allOrder)
        {
            var model = _blOrders.GetAllSiteOrdersViewModelByUserCustomerAndPending(User.GetUserIdInt(), InfraStructure.Utility.CurrentLanguageCode, _configuration["CustomerImageView"], _configuration["VendorImageView"]);
            if (!allOrder)
            {
                var DataTake = model.Take(10);
                return PartialView("_SiteOrdersPendingPartial", DataTake);
            }
            return PartialView("_SiteOrdersPendingPartial", model);
        }
        public JsonResult GetCustomerLocation(int customerLocationID)
        {
            var obj = _blCustomer.GetSiteCustomerLocationViewModelByCustomerLocation(customerLocationID, InfraStructure.Utility.CurrentLanguageCode);
            return Json(obj);
        }
        public IActionResult GetOrderDetailsPartial(int ordersID)
        {
            var model = _blOrders.GetSiteOrdersDetailsViewModelByUserOrderID(ordersID, InfraStructure.Utility.CurrentLanguageCode, _configuration["CustomerImageView"], _configuration["VendorImageView"], _configuration["ProductImageView"]);
            return PartialView("_ModalOrderDetails", model);
        }
        public IActionResult GetOrderVatPartial(int ordersID)
        {
            var model = _blOrders.GetSiteOrdersVatViewModelByOrderID(ordersID, InfraStructure.Utility.CurrentLanguageCode);
            var barcodeString = "التاجر: " + model.VendorsName + "\n";
            barcodeString += "رقم تسجيل الضريبة: " + model.TaxIdentificationNumber + "\n";
            barcodeString += "تاريخ الفاتورة: " + model.OrderDate + "\n";
            barcodeString += "اجمالي الفاتورة مع الضريبة: " + Math.Round(model.Total, 2) + Homemade.UI.Resources.Homemade.SR + "\n";
            barcodeString += "اجمالي ضريبة القيمة المضافة: " + Math.Round(model.VatValue, 2).ToString() + Homemade.UI.Resources.Homemade.SR;

            model.BarCode = GenerateBarCode(barcodeString);
            return PartialView("_ModalOrderVatNew", model);
        }
        public IActionResult GetSearchProducts(string searchProducts, int? searchDepartmentID)
        {
            var model = new SearchProductsViewModel();
            model.SearchDepartmentID = searchDepartmentID;
            model.SearchProducts = searchProducts;
            HttpContext.Session.SetObject("SessionSearchProductsViewModel", model);
            return RedirectToAction("Products");
        }
        public IActionResult GetDeliveryAddressesPartial()
        {
            return PartialView("_DeliveryAddressesPartial");
        }
        public IActionResult GetCompleteOrderAddressPartial()
        {
            return PartialView("_completeOrderAddressPartial");
        }
        public IActionResult GetProductQuestionsPartial(int productID)
        {
            var customer = _blCustomer.GetCustomerByUser(User.GetUserIdInt());
            int CustomerID = customer != null ? customer.CustomersID : 0;
            var model = _blProduct.GetSiteProductDetailsViewModelByID(productID, CustomerID, InfraStructure.Utility.CurrentLanguageCode, _configuration["VendorImageView"], _configuration["ProductImageView"]);
            return PartialView("_ProductQuestionsPartial", model);
        }
        public IActionResult GetProductItemsPagingPartial(string searchProducts, string departmentsCheck, string productTypeCheck, int? minPrice, int? maxPrice, double? rating, int page = 1)
        {
            var itemCount = _blProduct.GetAllSiteProductViewModelByDepartments(searchProducts, departmentsCheck, productTypeCheck, minPrice, maxPrice, rating, InfraStructure.Utility.CurrentLanguageCode, _configuration["VendorImageView"], _configuration["ProductImageView"]).Count();
            var model = new PagingViewModel();
            model.ItemCount = itemCount;
            int NumberOfItems = 21;
            model.NumberOfItems = NumberOfItems;
            model.Pagecount = (itemCount / NumberOfItems) == 0 ? 1 : (itemCount % NumberOfItems == 0 ? (itemCount / NumberOfItems) : (itemCount / NumberOfItems) + 1);
            model.CurrentPage = page;
            return PartialView("_PagingPartial", model);
        }
        public IActionResult GetProductItemsCategoryPagingPartial(int departmentsId, string searchProducts, int page = 1)
        {
            var itemCount = _blProduct.GetAllSiteProductViewModelByDepartments(departmentsId, searchProducts, InfraStructure.Utility.CurrentLanguageCode, _configuration["VendorImageView"], _configuration["ProductImageView"]).Count();
            var model = new PagingViewModel();
            model.ItemCount = itemCount;
            int NumberOfItems = 28;
            model.NumberOfItems = NumberOfItems;
            model.Pagecount = (itemCount / NumberOfItems) == 0 ? 1 : (itemCount % NumberOfItems == 0 ? (itemCount / NumberOfItems) : (itemCount / NumberOfItems) + 1);
            model.CurrentPage = page;
            return PartialView("_PagingPartial", model);
        }
        public IActionResult GetVendorProductsPagingPartial(int vendorsID, int page = 1)
        {
            var itemCount = _blProduct.GetAllSiteProductViewModelByVendorsID(vendorsID, InfraStructure.Utility.CurrentLanguageCode, _configuration["VendorImageView"], _configuration["ProductImageView"]).Where(e => !e.IsHidden).OrderByDescending(e => e.IsFixed).Count();
            var model = new PagingViewModel();
            model.ItemCount = itemCount;
            int NumberOfItems = 28;
            model.NumberOfItems = NumberOfItems;
            model.Pagecount = (itemCount / NumberOfItems) == 0 ? 1 : (itemCount % NumberOfItems == 0 ? (itemCount / NumberOfItems) : (itemCount / NumberOfItems) + 1);
            model.CurrentPage = page;
            return PartialView("_PagingPartial", model);
        }

        public JsonResult LoadCities()
        {
            var listOfCities = _blCity.GetCities()
                .Select(p => new { p.CityID, CityName = Utility.CurrentLanguageCode == "ar" ? p.CityNameAR : p.CityNameEN }).OrderByDescending(e => e.CityID);
            return Json(listOfCities);
        }

        public JsonResult LoadBlocks(int cityID)
        {
            var listOfCities = _BlBlock.GetAllBlocksByListCity(cityID).Select(p => new { p.BlockID, blockName = Utility.CurrentLanguageCode == "ar" ? p.NameAR : p.NameEN });
            return Json(listOfCities);
        }

        public JsonResult LoadActivities()
        {
            var listOfCities = _BlActivity.GetActivitytaghelper().Select(p => new { p.ActivityID, activityName = Utility.CurrentLanguageCode == "ar" ? p.ActivityNameAR : p.ActivityNameEN });
            return Json(listOfCities);
        }

        public JsonResult LoadBanks()
        {
            var listOfCities = _BlBanks.GetBankstaghelper().Select(p => new { p.BanksID, banksName = Utility.CurrentLanguageCode == "ar" ? p.BanksNameAR : p.BanksNameEN });
            return Json(listOfCities);
        }

        public JsonResult LoadPackages()
        {
            var listOfCities = _BlPackage.GetAllPackages().Select(p => new { p.PackageID, packageName = Utility.CurrentLanguageCode == "ar" ? p.NameAR : p.NameEN });
            return Json(listOfCities);
        }
        public JsonResult LoadNationality()
        {
            var listOfCities = _blNationality.GetAllNationality().Select(p => new { p.NationalityID, NationalityName = Utility.CurrentLanguageCode == "ar" ? p.NameAR : p.NameEN });
            return Json(listOfCities);
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
        #region authentication
        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> LogIn(BLL.ViewModel.User.LoginModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    #region Check User Data
                    model.Mobile = MobileFormat(model.Mobile, (int)CountryEnum.SA);
                    var customerdata = _blCustomer.GetCustomersByMobileNo(model.Mobile, "City,Nationality");
                    if (customerdata != null)
                    {
                        var Userdata = await _userManager.FindByIdAsync(customerdata.UserId.ToString());
                        var result = await _userManager.CheckPasswordAsync(Userdata, model.Password);
                        if (result)
                        {
                            var KeyCode = _BLGeneral.RandomNumber(4);
                            HttpContext.Session.SetObject("UserSiteVerfiedCodeViewModel", new SiteVerfiedCodeViewModel() { Code = KeyCode, Password = model.Password, UserName = model.Mobile });
                            var SmsUserName = _configuration["SmsUserName"];
                            var SmsSender = _configuration["SmsSender"];
                            var SmsPassword = _configuration["SmsPassword"];
                            var SMSStatus = await _OTPService.SendSMSForVerify(model.Mobile
                                , KeyCode, (int)UserTypeEnum.Customer, (int)MessageReasonEnum.Activation);

                            _result.Message = Resources.Homemade.SuccessSendCode;
                            _result.Status = true;
                            _result.ResultType = ResultMessageType.success.ToString();
                            return Json(_result);
                        }
                        else
                        {
                            _result.Message = Resources.Homemade.Invalid_login_attempt;
                            _result.Status = false;
                            _result.ResultType = ResultMessageType.error.ToString();
                            return Json(_result);
                        }

                        #endregion
                    }
                    else
                    {
                        _result.Message = Resources.Homemade.User_Not_Found;
                        _result.Status = false;
                        _result.ResultType = ResultMessageType.error.ToString();
                        return Json(_result);
                    }
                }
                return View(model);

            }
            catch (Exception e)
            {
                return View(model);
            }
        }
        /// <summary>
        /// تسجيل الخروج
        /// </summary>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return Redirect(Url.Action("index", "home", new { Area = "site" }));
        }
        #endregion
        #region Customer

        #region  Register Customer
        [HttpPost("RegisterCustomer")]
        public async Task<IActionResult> RegisterCustomer([FromForm] NewUserViewModel model)
        {
            try
            {
                if (model.IsAgreeTerms)
                {
                    model.CityID = _blCity.GetFirstCity().CityID;
                    model.Gender = (int)Gender.Male;
                    model.NationalityID = _blNationality.GetFirstNationality().NationalityID;
                    model.Address = string.Empty;
                    if (model.CityID != 0)
                    {
                        if (!string.IsNullOrWhiteSpace(model.MobileNo))
                        {
                            if (!string.IsNullOrWhiteSpace(model.UserName))
                            {
                                if (model.NationalityID != 0)
                                {

                                    int countryId = _blCity.GetById(model.CityID, "Region").Region.CountryID;
                                    model.MobileNo = MobileFormat(model.MobileNo, countryId);
                                    if (!A_Valid_Phone_Number(model.MobileNo, countryId))
                                    {
                                        _result.Message = Resources.Homemade.Invalid_Phone_number;
                                        _result.Status = false;
                                        _result.ResultType = ResultMessageType.error.ToString();

                                        return Json(_result);
                                    }

                                    if (!_blCustomer.IsExistMobile(model.MobileNo))
                                    {
                                        if (!string.IsNullOrWhiteSpace(model.Email))
                                        {
                                            if (_blCustomer.IsExistEmail(model.Email))
                                            {
                                                _result.Message = Resources.Homemade.EmailExists;
                                                _result.Status = false;
                                                _result.ResultType = ResultMessageType.error.ToString();

                                                return Json(_result);
                                            }
                                        }
                                        if (!string.IsNullOrWhiteSpace(model.Password))
                                        {
                                            if (model.Password.Length > 5)
                                            {
                                                if (model.RegisterTypeId == 1)
                                                {
                                                    var KeyCode = _BLGeneral.RandomNumber(4);
                                                    HttpContext.Session.SetObject("UserSiteVerfiedCodeViewModel", new SiteVerfiedCodeViewModel() { Code = KeyCode, Password = model.Password, UserName = model.MobileNo });
                                                    var SmsUserName = _configuration["SmsUserName"];
                                                    var SmsSender = _configuration["SmsSender"];
                                                    var SmsPassword = _configuration["SmsPassword"];
                                                    var SMSStatus = await _OTPService.SendSMSForVerify(model.MobileNo
                                                        , KeyCode, (int)UserTypeEnum.Customer, (int)MessageReasonEnum.Activation);

                                                    _result.Message = Resources.Homemade.Successfully_Registered;
                                                    _result.Status = true;
                                                    _result.ResultType = ResultMessageType.success.ToString();
                                                    _result.ObjResult = "verfied";
                                                    return Ok(_result);
                                                }
                                                else
                                                {
                                                    #region add Customer
                                                    #region User
                                                    //Insert User
                                                    var user = new User
                                                    {
                                                        UserName = _BLGeneral.GenerateToken(200),
                                                        Email = _BLGeneral.RandomString(10) + "@made-home.com",
                                                        PhoneNumber = _BLGeneral.RandomNumber(10),
                                                        UserType = (int)UserTypeEnum.Customer
                                                    };
                                                    var identityResult = await _userManager.CreateAsync(user, model.Password);
                                                    #endregion
                                                    if (identityResult.Succeeded)
                                                    {
                                                        try
                                                        {
                                                            var IsSuccess = _blCustomer.AddCustomer(model, user.Id);
                                                            if (IsSuccess)
                                                            {
                                                                #region Identity

                                                                #region Token
                                                                #region Create JWT Token
                                                                var token = string.Empty;
                                                                _blTokens.AddTokens(user.Id, 1, token, (int)UserTypeEnum.Customer);
                                                                #endregion


                                                                #endregion
                                                                #endregion
                                                                Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user, model.Password, true, lockoutOnFailure: false);
                                                                if (result.Succeeded)
                                                                {
                                                                    _result.Message = Resources.Homemade.Success_Verfied;
                                                                    _result.Status = true;
                                                                    _result.ResultType = ResultMessageType.success.ToString();
                                                                    return Json(_result);
                                                                }
                                                                else
                                                                {
                                                                    _result.Message = Resources.Homemade.Error;
                                                                    _result.Status = false;
                                                                    _result.ResultType = ResultMessageType.error.ToString();
                                                                    return Json(_result);
                                                                }
                                                            }

                                                            else
                                                            {
                                                                await _userManager.DeleteAsync(user);
                                                                _result.Message = Resources.Homemade.FailSaveMessage;
                                                                _result.Status = false;
                                                                _result.ResultType = ResultMessageType.error.ToString();
                                                                return Json(_result);

                                                            }
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            await _userManager.DeleteAsync(user);
                                                            _result.Message = Resources.Homemade.FailSaveMessage + ex.Message.ToString();
                                                            _result.Status = false;
                                                            _result.ResultType = ResultMessageType.error.ToString();
                                                            return Json(_result);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        _result.Message = Resources.Homemade.FaildSaveUser;
                                                        _result.Status = false;
                                                        _result.ResultType = ResultMessageType.error.ToString();
                                                        return Json(_result);
                                                    }
                                                    #endregion
                                                }
                                            }
                                            else
                                            {
                                                _result.Message = Resources.Homemade.InvalidPassword;
                                                _result.Status = false;
                                                _result.ResultType = ResultMessageType.error.ToString();
                                                return Json(_result);
                                            }
                                        }
                                        else
                                        {
                                            _result.Message = Resources.Homemade.Enter_Password;
                                            _result.Status = false;
                                            _result.ResultType = ResultMessageType.error.ToString();
                                            return Json(_result);
                                        }
                                    }
                                    else
                                    {
                                        _result.Message = Resources.Homemade.MobileNumberExists;
                                        _result.Status = false;
                                        _result.ResultType = ResultMessageType.error.ToString();
                                        return Json(_result);
                                    }

                                }
                                else
                                {
                                    _result.Message = Resources.Homemade.Choose_Nationality;
                                    _result.Status = false;
                                    _result.ResultType = ResultMessageType.error.ToString();
                                    return Json(_result);
                                }

                            }
                            else
                            {
                                _result.Message = Resources.Homemade.Name_Not_Valid;
                                _result.Status = false;
                                _result.ResultType = ResultMessageType.error.ToString();
                                return Json(_result);
                            }

                        }
                        else
                        {
                            _result.Message = Resources.Homemade.Mobile_Not_Valid;
                            _result.Status = false;
                            _result.ResultType = ResultMessageType.error.ToString();
                            return Json(_result);
                        }

                    }
                    else
                    {
                        _result.Message = Resources.Homemade.Choose_City;
                        _result.Status = false;
                        _result.ResultType = ResultMessageType.error.ToString();
                        return Json(_result);

                    }
                }
                else
                {
                    _result.Message = Resources.Homemade.IsAgreeTermsRequierd;
                    _result.Status = false;
                    _result.ResultType = ResultMessageType.error.ToString();
                    return Json(_result);
                }
            }
            catch (Exception ex)
            {
                _result.Message = Resources.Homemade.Error + Environment.NewLine + ex.Message.ToString();
                _result.Status = false;
                _result.ResultType = ResultMessageType.error.ToString();
                return Json(_result);
            }
        }
        #endregion
        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> CheckVerfiedCode(string code)
        {
            if (!HttpContext.Session.Keys.Contains("UserSiteVerfiedCodeViewModel"))
            {
                _result.Message = Resources.Homemade.Error;
                _result.Status = false;
                _result.ResultType = ResultMessageType.error.ToString();
                return Json(_result);
            }
            SiteVerfiedCodeViewModel model = HttpContext.Session.GetObject<SiteVerfiedCodeViewModel>("UserSiteVerfiedCodeViewModel");
            if (model.Code == code)
            {
                _result.Message = Resources.Homemade.Success_Verfied;
                _result.Status = true;
                _result.ResultType = ResultMessageType.success.ToString();
                return Json(_result);
            }
            else
            {
                _result.Message = Resources.Homemade.WrongCode;
                _result.Status = false;
                _result.ResultType = ResultMessageType.error.ToString();
                return Json(_result);
            }



        }
        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> CheckVerfiedCodeLogin(string code)
        {
            if (!HttpContext.Session.Keys.Contains("UserSiteVerfiedCodeViewModel"))
            {
                _result.Message = Resources.Homemade.Error;
                _result.Status = false;
                _result.ResultType = ResultMessageType.error.ToString();
                return Json(_result);
            }
            SiteVerfiedCodeViewModel model = HttpContext.Session.GetObject<SiteVerfiedCodeViewModel>("UserSiteVerfiedCodeViewModel");

            if (model.Code == code)
            {
                var customerdata = _blCustomer.GetCustomersByMobileNo(model.UserName, "City,Nationality");
                if (customerdata != null)
                {
                    var Userdata = await _userManager.FindByIdAsync(customerdata.UserId.ToString());
                    Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(Userdata, model.Password, true, lockoutOnFailure: false);

                    if (result.Succeeded)
                    {
                        _result.Message = Resources.Homemade.Login_Success;
                        _result.Status = true;
                        _result.ResultType = ResultMessageType.success.ToString();
                        return Json(_result);
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
                        _result.Message = Resources.Homemade.Invalid_login_attempt;
                        _result.Status = false;
                        _result.ResultType = ResultMessageType.error.ToString();
                        return Json(_result);
                    }
                }
                else
                {
                    _result.Message = Resources.Homemade.Invalid_login_attempt;
                    _result.Status = false;
                    _result.ResultType = ResultMessageType.error.ToString();
                    return Json(_result);
                }
            }
            else
            {
                _result.Message = Resources.Homemade.WrongCode;
                _result.Status = false;
                _result.ResultType = ResultMessageType.error.ToString();
                return Json(_result);
            }

        }
        public async Task<IActionResult> SendOtpCode(string mobileNo)
        {
            try
            {
                mobileNo = MobileFormat(mobileNo, (int)CountryEnum.SA);
                if (!A_Valid_Phone_Number(mobileNo, (int)CountryEnum.SA))
                {
                    _result.Message = Resources.Homemade.Mobile_Not_Valid;
                    _result.Status = false;
                    _result.ResultType = ResultMessageType.error.ToString();
                    return Json(_result);
                }
                else
                {
                    var IsCustomerExists = _blCustomer.GetCustomersByMobileNo(mobileNo, "");
                    if (IsCustomerExists != null)
                    {
                        #region Send Otp code
                        var KeyCode = _BLGeneral.RandomNumber(4);

                        var SMSStatus = await _OTPService.SendSMSForVerify(mobileNo, KeyCode, (int)UserTypeEnum.Customer, (int)MessageReasonEnum.ForgetPassword);

                        HttpContext.Session.SetObject("UserForgetVerfiedCodeViewModel", new SiteVerfiedCodeViewModel() { Code = KeyCode, UserName = IsCustomerExists.UserId.ToString() });

                        _result.Message = Resources.Homemade.SuccessSendCode;
                        _result.Status = true;
                        _result.ResultType = ResultMessageType.success.ToString();
                        return Json(_result);
                        #endregion
                    }
                    else
                    {
                        _result.Message = Resources.Homemade.User_Not_Found;
                        _result.Status = false;
                        _result.ResultType = ResultMessageType.error.ToString();
                        return Json(_result);
                    }
                }
            }
            catch (Exception ex)
            {
                _result.Message = Resources.Homemade.Error + Environment.NewLine + ex.Message.ToString();
                _result.Status = false;
                _result.ResultType = ResultMessageType.error.ToString();
                return Json(_result);
            }
        }
        public IActionResult CheckOtpCode(string code)
        {
            if (!HttpContext.Session.Keys.Contains("UserForgetVerfiedCodeViewModel"))
            {
                _result.Message = Resources.Homemade.Error;
                _result.Status = false;
                _result.ResultType = ResultMessageType.error.ToString();
                return Json(_result);
            }
            SiteVerfiedCodeViewModel model = HttpContext.Session.GetObject<SiteVerfiedCodeViewModel>("UserForgetVerfiedCodeViewModel");
            if (model.Code == code)
            {
                _result.Message = Resources.Homemade.Success_Verfied;
                _result.Status = true;
                _result.ResultType = ResultMessageType.success.ToString();
                return Json(_result);
            }
            else
            {
                _result.Message = Resources.Homemade.WrongCode;
                _result.Status = false;
                _result.ResultType = ResultMessageType.error.ToString();
                return Json(_result);
            }
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> ResetForgetPassword(string newPassword, string confirmNewPassword)
        {
            try
            {

                if (!HttpContext.Session.Keys.Contains("UserForgetVerfiedCodeViewModel"))
                {
                    _result.Message = Resources.Homemade.Error;
                    _result.Status = false;
                    _result.ResultType = ResultMessageType.error.ToString();
                    return Json(_result);
                }
                SiteVerfiedCodeViewModel model = HttpContext.Session.GetObject<SiteVerfiedCodeViewModel>("UserForgetVerfiedCodeViewModel");
                if (newPassword.Length > 5)
                {
                    if (newPassword == confirmNewPassword)
                    {
                        var user = await _userManager.FindByIdAsync(model.UserName);
                        user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, newPassword);
                        var identityResult = await _userManager.UpdateAsync(user);
                        if (!identityResult.Succeeded)
                        {
                            _result.Message = Resources.Homemade.FaildSaveUser;
                            _result.Status = false;
                            _result.ResultType = ResultMessageType.error.ToString();
                            return Json(_result);
                        }

                        Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user, newPassword, true, lockoutOnFailure: false);

                        if (result.Succeeded)
                        {
                            _result.Message = Resources.Homemade.Success_Update_Password;
                            _result.Status = true;
                            _result.ResultType = ResultMessageType.success.ToString();
                            return Json(_result);
                        }
                        else
                        {
                            _result.Message = Resources.Homemade.Error;
                            _result.Status = false;
                            _result.ResultType = ResultMessageType.error.ToString();
                            return Json(_result);
                        }
                    }
                    else
                    {
                        _result.Message = Resources.Homemade.PasswordNotMatched;
                        _result.Status = false;
                        _result.ResultType = ResultMessageType.error.ToString();
                        return Json(_result);
                    }
                }
                else
                {
                    _result.Message = Resources.Homemade.InvalidPassword;
                    _result.Status = false;
                    _result.ResultType = ResultMessageType.error.ToString();
                    return Json(_result);
                }
            }
            catch (Exception ex)
            {
                _result.Message = Resources.Homemade.Error + Environment.NewLine + ex.Message.ToString();
                _result.Status = false;
                _result.ResultType = ResultMessageType.error.ToString();
                return Json(_result);
            }

        }
        #endregion
        #region Vendor

        [HttpPost]
        public async Task<JsonResult> CheckVendorRegisterAndSendCode(VendorSiteRequestVM viewModel)
        {
            var accLanguage = Utility.CurrentLanguageCode;
            try
            {
                if (viewModel.cities != 0)
                {
                    if (!string.IsNullOrWhiteSpace(viewModel.Txt_Mobile))
                    {
                        if (!string.IsNullOrWhiteSpace(viewModel.Txt_FnameAr))
                        {
                            int countryId = _blCity.GetById(viewModel.cities, "Region").Region.CountryID;
                            viewModel.Txt_Mobile = MobileFormat(viewModel.Txt_Mobile, countryId);
                            if (!A_Valid_Phone_Number(viewModel.Txt_Mobile, countryId))
                            {
                                _result.Message = accLanguage == "ar" ? Resources.Response_ar.Invalid_Phone_number : Resources.Response.Invalid_Phone_number;
                                _result.Status = false;
                                _result.ResultType = ResultMessageType.error.ToString();
                                return Json(_result);
                            }

                            if (!_BlVendor.IsExistMobile(viewModel.Txt_Mobile))
                            {
                                if (!string.IsNullOrWhiteSpace(viewModel.Txt_Email))
                                {
                                    if (_BlVendor.IsExistEmail(viewModel.Txt_Email))
                                    {
                                        _result.Message = accLanguage == "ar" ? Resources.Response_ar.Email_Is_Exist : Resources.Response.Email_Is_Exist;
                                        _result.Status = false;
                                        _result.ResultType = ResultMessageType.error.ToString();
                                        return Json(_result);
                                    }
                                }
                                if (!string.IsNullOrWhiteSpace(viewModel.Txt_password))
                                {
                                    if (viewModel.Txt_password.Length < 5)
                                    {
                                        _result.Message = accLanguage == "ar" ? Resources.Response_ar.Password_Not_Valid : Resources.Response.Password_Not_Valid;
                                        _result.Status = false;
                                        _result.ResultType = ResultMessageType.error.ToString();
                                        return Json(_result);
                                    }
                                    else
                                    {

                                        var KeyCode = _BLGeneral.RandomNumber(4);
                                        HttpContext.Session.SetObject("UserForgetVerfiedCodeViewModel", new SiteVerfiedCodeViewModel() { Code = KeyCode, UserName = viewModel.Txt_Mobile });
                                        var SMSStatus = await _OTPService.SendSMSForVerify(viewModel.Txt_Mobile, KeyCode, (int)UserTypeEnum.Vendor, (int)MessageReasonEnum.Activation);


                                        _result.Message = Resources.Homemade.SuccessSendCode;
                                        _result.Status = true;
                                        _result.ResultType = ResultMessageType.success.ToString();
                                        return Json(_result);
                                    }
                                }
                                else
                                {
                                    _result.Message = accLanguage == "ar" ? Resources.Response_ar.Please_Insert_Password : Resources.Response.Please_Insert_Password;
                                    _result.Status = false;
                                    _result.ResultType = ResultMessageType.error.ToString();
                                    return Json(_result);
                                }
                            }
                            else
                            {
                                _result.Message = accLanguage == "ar" ? Resources.Response_ar.Mobile_No_Is_Exists : Resources.Response.Mobile_No_Is_Exists;
                                _result.Status = false;
                                _result.ResultType = ResultMessageType.error.ToString();
                                return Json(_result);
                            }
                        }
                        else
                        {
                            _result.Message = accLanguage == "ar" ? Resources.Response_ar.Name_not_Found : Resources.Response.Name_not_Found;
                            _result.Status = false;
                            _result.ResultType = ResultMessageType.error.ToString();
                            return Json(_result);
                        }

                    }
                    else
                    {
                        _result.Message = accLanguage == "ar" ? Resources.Response_ar.Mobile_No_not_Found : Resources.Response.Mobile_No_not_Found;
                        _result.Status = false;
                        _result.ResultType = ResultMessageType.error.ToString();
                        return Json(_result);
                    }

                }
                else
                {
                    _result.Message = accLanguage == "ar" ? Resources.Response_ar.city_ID_not_Found : Resources.Response.city_ID_not_Found;
                    _result.Status = false;
                    _result.ResultType = ResultMessageType.error.ToString();
                    return Json(_result);
                }
                return Json(_result);
            }
            catch (Exception ex)
            {
                _result.Message = accLanguage == "ar" ? Resources.Response_ar.Error + Environment.NewLine + ex.Message.ToString() : Resources.Response.Error + Environment.NewLine + ex.Message.ToString();
                _result.Status = false;
                _result.ResultType = ResultMessageType.error.ToString();
                return Json(_result);
            }
        }
        public async Task<JsonResult> VendorRegister(VendorSiteRequestVM viewModel)
        {
            var accLanguage = Utility.CurrentLanguageCode;
            try
            {
                if (viewModel.cities != 0)
                {
                    if (!string.IsNullOrWhiteSpace(viewModel.Txt_Mobile))
                    {
                        if (!string.IsNullOrWhiteSpace(viewModel.Txt_FnameAr))
                        {
                            int countryId = _blCity.GetById(viewModel.cities, "Region").Region.CountryID;
                            viewModel.Txt_Mobile = MobileFormat(viewModel.Txt_Mobile, countryId);
                            if (!A_Valid_Phone_Number(viewModel.Txt_Mobile, countryId))
                            {
                                _result.Message = accLanguage == "ar" ? Resources.Response_ar.Invalid_Phone_number : Resources.Response.Invalid_Phone_number;
                                _result.Status = false;
                                _result.ResultType = ResultMessageType.error.ToString();
                                return Json(_result);
                            }

                            if (!_BlVendor.IsExistMobile(viewModel.Txt_Mobile))
                            {
                                if (!string.IsNullOrWhiteSpace(viewModel.Txt_Email))
                                {
                                    if (_BlVendor.IsExistEmail(viewModel.Txt_Email))
                                    {
                                        _result.Message = accLanguage == "ar" ? Resources.Response_ar.Email_Is_Exist : Resources.Response.Email_Is_Exist;
                                        _result.Status = false;
                                        _result.ResultType = ResultMessageType.error.ToString();
                                        return Json(_result);
                                    }
                                }
                                if (!string.IsNullOrWhiteSpace(viewModel.Txt_password))
                                {
                                    if (viewModel.Txt_password.Length > 5)
                                    {
                                        string ImagePath = _configuration["VendorImageSave"];
                                        var profilePhotofileName = string.Empty;

                                        #region Upload Image
                                        if (viewModel.fileowner != null)
                                        {
                                            string FileName = Guid.NewGuid() + ".png";

                                            _BLGeneral.SaveImageIFromFile(new BLGeneral.SaveImageFormFileModel
                                            {
                                                file = viewModel.fileowner,
                                                filename = FileName,
                                                key = "",
                                                mainPath = ImagePath
                                            });
                                            viewModel.imageownerlogo = FileName;
                                        }

                                        if (viewModel.filestore != null)
                                        {
                                            string FileName = Guid.NewGuid() + ".png";

                                            _BLGeneral.SaveImageIFromFile(new BLGeneral.SaveImageFormFileModel
                                            {
                                                file = viewModel.filestore,
                                                filename = FileName,
                                                key = "",
                                                mainPath = ImagePath
                                            });
                                            viewModel.imagestorelogo = FileName;
                                        }


                                        if (viewModel.filecommerc != null)
                                        {
                                            string FileName = Guid.NewGuid() + ".png";

                                            _BLGeneral.SaveImageIFromFile(new BLGeneral.SaveImageFormFileModel
                                            {
                                                file = viewModel.filecommerc,
                                                filename = FileName,
                                                key = "",
                                                mainPath = ImagePath
                                            });
                                            viewModel.imagecommercialcumber = FileName;
                                        }

                                        #endregion
                                        #region add Vendor
                                        #region User
                                        //Insert User
                                        var user = new User
                                        {
                                            UserName = _BLGeneral.GenerateToken(200),
                                            Email = _BLGeneral.RandomString(10) + "@made-home.com",
                                            PhoneNumber = _BLGeneral.RandomNumber(10),

                                            UserType = (int)UserTypeEnum.Vendor
                                        };
                                        var identityResult = await _userManager.CreateAsync(user, viewModel.Txt_password);
                                        #endregion
                                        if (identityResult.Succeeded)
                                        {
                                            try
                                            {
                                                var IsSuccess = _BlVendor.CreateSiteVendor(viewModel, user.Id);
                                                if (IsSuccess)
                                                {

                                                    HttpContext.Session.SetObject("RegisterVendorEmail", viewModel.Txt_Email);

                                                    var vendordata = _BlVendor.GetVendorsByMobileNo(viewModel.Txt_Mobile, "City,Nationality");
                                                    _result.Message = accLanguage == "ar" ? Resources.Response_ar.Thanks_for_Registration_Your_request_has_been_send_and_we_will_contact_with_you_soon : Resources.Response.Thanks_for_Registration_Your_request_has_been_send_and_we_will_contact_with_you_soon;
                                                    _result.Status = true;
                                                    _result.ResultType = ResultMessageType.error.ToString();

                                                    return Json(_result);
                                                }

                                                else
                                                {
                                                    await _userManager.DeleteAsync(user);
                                                    _result.Message = accLanguage == "ar" ? Resources.Response_ar.Cant_Create : Resources.Response.Cant_Create;
                                                    _result.Status = false;
                                                    _result.ResultType = ResultMessageType.error.ToString();
                                                    return Json(_result);

                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                await _userManager.DeleteAsync(user);
                                                _result.Message = accLanguage == "ar" ? Resources.Response_ar.Cant_Create + ex.Message.ToString() : Resources.Response.Cant_Create + ex.Message.ToString();
                                                _result.Status = false;
                                                _result.ResultType = ResultMessageType.error.ToString();
                                                return Json(_result);
                                            }
                                        }
                                        else
                                        {
                                            _result.Message = accLanguage == "ar" ? Resources.Response_ar.Error_when_Create_User_Login_Data_Check_the_Email_And_Mobile_And_Try_Again : Resources.Response.Error_when_Create_User_Login_Data_Check_the_Email_And_Mobile_And_Try_Again;
                                            _result.Status = false;
                                            _result.ResultType = ResultMessageType.error.ToString();
                                            return Json(_result);
                                        }
                                        #endregion
                                    }
                                    else
                                    {
                                        _result.Message = accLanguage == "ar" ? Resources.Response_ar.Password_Not_Valid : Resources.Response.Password_Not_Valid;
                                        _result.Status = false;
                                        _result.ResultType = ResultMessageType.error.ToString();
                                        return Json(_result);
                                    }
                                }
                                else
                                {
                                    _result.Message = accLanguage == "ar" ? Resources.Response_ar.Please_Insert_Password : Resources.Response.Please_Insert_Password;
                                    _result.Status = false;
                                    _result.ResultType = ResultMessageType.error.ToString();
                                    return Json(_result);
                                }
                            }
                            else
                            {
                                _result.Message = accLanguage == "ar" ? Resources.Response_ar.Mobile_No_Is_Exists : Resources.Response.Mobile_No_Is_Exists;
                                _result.Status = false;
                                _result.ResultType = ResultMessageType.error.ToString();
                                return Json(_result);
                            }
                        }
                        else
                        {
                            _result.Message = accLanguage == "ar" ? Resources.Response_ar.Name_not_Found : Resources.Response.Name_not_Found;
                            _result.Status = false;
                            _result.ResultType = ResultMessageType.error.ToString();
                            return Json(_result);
                        }

                    }
                    else
                    {
                        _result.Message = accLanguage == "ar" ? Resources.Response_ar.Mobile_No_not_Found : Resources.Response.Mobile_No_not_Found;
                        _result.Status = false;
                        _result.ResultType = ResultMessageType.error.ToString();
                        return Json(_result);
                    }

                }
                else
                {
                    _result.Message = accLanguage == "ar" ? Resources.Response_ar.city_ID_not_Found : Resources.Response.city_ID_not_Found;
                    _result.Status = false;
                    _result.ResultType = ResultMessageType.error.ToString();
                    return Json(_result);

                }
            }
            catch (Exception ex)
            {
                _result.Message = accLanguage == "ar" ? Resources.Response_ar.Error + Environment.NewLine + ex.Message.ToString() : Resources.Response.Error + Environment.NewLine + ex.Message.ToString();
                _result.Status = false;
                _result.ResultType = ResultMessageType.error.ToString();
                return Json(_result);
            }

        }
        public JsonResult checkExistVendorData(string txt_Email, string txt_Mobile)
        {
            var existEmail = !string.IsNullOrEmpty(txt_Email) ? _BlVendor.IsExistEmail(txt_Email) : false;
            var existMobile = _BlVendor.IsExistMobile(txt_Mobile);
            return Json(new { existEmail = existEmail, existMobile = existMobile });
        }

        public ActionResult SendDone()
        {
            if (HttpContext.Session.Keys.Contains("RegisterVendorEmail"))
            {
                ViewBag.RegisterVendorEmail = HttpContext.Session.GetObject<string>("RegisterVendorEmail");
            }
            return View();
        }
        #endregion
        #region Site
        [HttpPost, AllowAnonymous]
        public IActionResult Subscribe(string SubscribeName, string SubscribeEmail)
        {
            try
            {
                if (!string.IsNullOrEmpty(SubscribeName) && !string.IsNullOrEmpty(SubscribeEmail))
                {
                    var IsExistSubscribe = _blSubscribe.IsExistEmail(SubscribeEmail);
                    if (!IsExistSubscribe)
                    {
                        var IsSucccess = _blSubscribe.Insert(SubscribeName, SubscribeEmail);
                        if (IsSucccess)
                        {
                            _result.Message = Resources.Homemade.Success_SubScribe;
                            _result.Status = true;
                            _result.ResultType = ResultMessageType.success.ToString();
                            return Json(_result);
                        }
                        else
                        {
                            _result.Message = Resources.Homemade.Error;
                            _result.Status = false;
                            _result.ResultType = ResultMessageType.error.ToString();
                            return Json(_result);
                        }
                    }
                    else
                    {
                        _result.Message = Resources.Homemade.Already_Subscribed;
                        _result.Status = false;
                        _result.ResultType = ResultMessageType.error.ToString();
                        return Json(_result);
                    }
                }
                else
                {
                    _result.Message = Resources.Homemade.InsertValidDataMessage;
                    _result.Status = false;
                    _result.ResultType = ResultMessageType.error.ToString();
                    return Json(_result);
                }
            }
            catch (Exception ex)
            {
                _result.Message = Resources.Homemade.Error + Environment.NewLine + ex.Message.ToString();
                _result.Status = false;
                _result.ResultType = ResultMessageType.error.ToString();
                return Json(_result);
            }

        }
        [HttpPost]
        public IActionResult AddQuestionProduct(int ProductID, string ProductQuesion)
        {
            try
            {
                var customer = _blCustomer.GetCustomerByUser(User.GetUserIdInt());
                if (customer != null)
                {
                    if (!string.IsNullOrEmpty(ProductQuesion))
                    {

                        var IsSucccess = _blProduct.AddQuestionProd(ProductQuesion, ProductID, customer.CustomersID, User.GetUserIdInt(), _configuration["FireBaseKey"]);
                        if (IsSucccess)
                        {
                            _result.Message = Resources.Homemade.Success_SubScribe;
                            _result.Status = true;
                            _result.ResultType = ResultMessageType.success.ToString();
                            return Json(_result);
                        }
                        else
                        {
                            _result.Message = Resources.Homemade.Error;
                            _result.Status = false;
                            _result.ResultType = ResultMessageType.error.ToString();
                            return Json(_result);
                        }
                    }
                    else
                    {
                        _result.Message = Resources.Homemade.InsertValidDataMessage;
                        _result.Status = false;
                        _result.ResultType = ResultMessageType.error.ToString();
                        return Json(_result);
                    }
                }
                else
                {
                    _result.Message = Resources.Homemade.Error;
                    _result.Status = false;
                    _result.ResultType = ResultMessageType.error.ToString();
                    return Json(_result);
                }
            }
            catch (Exception ex)
            {
                _result.Message = Resources.Homemade.Error + Environment.NewLine + ex.Message.ToString();
                _result.Status = false;
                _result.ResultType = ResultMessageType.error.ToString();
                return Json(_result);
            }

        }
        [HttpPost]
        public IActionResult AddToFavorites(int ProductID)
        {
            try
            {
                var customer = _blCustomer.GetCustomerByUser(User.GetUserIdInt());
                if (customer != null)
                {
                    var IsSucccess = _blProduct.UpdateFavorites(customer.CustomersID, ProductID, User.GetUserIdInt());
                    if (IsSucccess)
                    {
                        _result.Message = Resources.Homemade.Success_SubScribe;
                        _result.Status = true;
                        _result.ResultType = ResultMessageType.success.ToString();
                        return Json(_result);
                    }
                    else
                    {
                        _result.Message = Resources.Homemade.Error;
                        _result.Status = false;
                        _result.ResultType = ResultMessageType.error.ToString();
                        return Json(_result);
                    }
                }

                else
                {
                    _result.Message = Resources.Homemade.Error;
                    _result.Status = false;
                    _result.ResultType = ResultMessageType.error.ToString();
                    return Json(_result);
                }
            }
            catch (Exception ex)
            {
                _result.Message = Resources.Homemade.Error + Environment.NewLine + ex.Message.ToString();
                _result.Status = false;
                _result.ResultType = ResultMessageType.error.ToString();
                return Json(_result);
            }

        }
        [HttpPost]
        public JsonResult SeenNotifictions(int notId)
        {
            int UserId = User.GetUserIdInt();
            var notification = _blCustomer.GetNotificationById(notId);
            if (notification != null)
            {
                if (notification.IsSeen == false)
                {
                    var IsSuccess = _blCustomer.UpdateNotificationSeen(notId, UserId);
                    return Json(new ResultMessage { Message = Resources.Homemade.SuccessSaveMessage, ResultType = ResultMessageType.success.ToString() });
                }
                else
                {
                    return Json(new ResultMessage { Message = Resources.Homemade.SuccessSaveMessage, ResultType = ResultMessageType.success.ToString() });
                }
            }
            else
            {
                return Json(new ResultMessage { Message = Resources.Homemade.FailSaveMessage, ResultType = ResultMessageType.error.ToString() });
            }
        }
        [HttpPost]
        public JsonResult SeenAllNotifictions()
        {
            int UserId = User.GetUserIdInt();
            var IsSuccess = _blCustomer.UpdateAllNotificationSeen(UserId);
            if (IsSuccess)
            {
                return Json(new ResultMessage { Message = Resources.Homemade.SuccessSaveMessage, ResultType = ResultMessageType.success.ToString() });
            }
            else
            {
                return Json(new ResultMessage { Message = Resources.Homemade.FailSaveMessage, ResultType = ResultMessageType.error.ToString() });
            }
        }
        #endregion
        #region execute Order
        /// <summary>
        /// paying order With Online Transfer
        /// </summary>
        /// <param name="_paymentId"></param>
        /// <param name="_UserName"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult PayingReditectStatus(string _paymentId, string _UserName)
        {
            string status = "";
            var json = JsonConvert.SerializeObject(CheckIsPaid_invoice(_paymentId).Value);
            StatusDesc dataID = JsonConvert.DeserializeObject<StatusDesc>(json);
            if (dataID != null)
            {
                TransactionCard tripChargeLog = _BLTransaction.GetTransactionCard(_paymentId, dataID.PaymentStatus.Data.InvoiceId.ToString(), dataID.PaymentStatus.Data.CustomerReference.ToString());
                if (tripChargeLog != null)
                {
                    if (!tripChargeLog.IsRedirect)
                    {
                        #region Confirm pay
                        if (dataID.PaymentStatus.Data.InvoiceStatus.ToLower() == "paid" && tripChargeLog.Status.ToLower() != dataID.PaymentStatus.Data.InvoiceStatus.ToLower())
                        {
                            var OrderId = dataID.Orderid;
                            string SecertToken = _configuration["SecertToken"];
                            string NodeLink = _configuration["NodeLink"];
                            #region Log
                            tripChargeLog.PaymentID = _paymentId;
                            tripChargeLog.InvoiceId = dataID.PaymentStatus.Data.InvoiceId.ToString();
                            tripChargeLog.IsRedirect = true;
                            tripChargeLog.UpdateDate = _BLGeneral.GetDateTimeNow();
                            tripChargeLog.Status = dataID.PaymentStatus.Data.InvoiceStatus;
                            tripChargeLog.TransactionStatus = (int)TransactionEnum.BeginRedirect;
                            tripChargeLog.TransactionCardLog.Add(new TransactionCardLog()
                            {
                                OrdersID = dataID.Orderid,
                                DateAdded = _BLGeneral.GetDateTimeNow(),
                                LastStatusUpdateFrom = (int)LastStatusUpdateFromEnum.Web,
                                Message = "ConfirmPaymyfatoorah Success And Begin To redirect .. ",
                                PaymentID = _paymentId,
                                InvoiceId = dataID.PaymentStatus.Data.InvoiceId.ToString(),
                                Status = dataID.PaymentStatus.Data.InvoiceStatus.ToString(),
                                TransactionStatus = (int)TransactionEnum.BeginRedirect,
                            });
                            tripChargeLog = _BLTransaction.UpdateTransactionCard(tripChargeLog);

                            #endregion

                            #region add Order 
                            var masterdata = _BLSite.GeCartMasterByID(int.Parse(dataID.PaymentStatus.Data.UserDefinedField));
                            var promoCodeID = _blCustomer.GetPromoCodeId(masterdata.Promocode);
                            List<VendorOrderApiNew> listofitem = new List<VendorOrderApiNew>();
                            var results = _BLSite.GeCartDetailsVM(int.Parse(dataID.PaymentStatus.Data.UserDefinedField), Utility.CurrentLanguageCode).ToList();
                            var listofdata = results.GroupBy(
                            p => p.VendorsID,
                            (key, g) => new { VendorsID = key, listitems = g.ToList() });
                            var orderobject = new AddOrderViewModelApiNew();
                            try
                            {
                                foreach (var itemlistofdata in listofdata)
                                {
                                    VendorOrderApiNew vendoritem = new VendorOrderApiNew();
                                    vendoritem.vendorId = itemlistofdata.VendorsID;
                                    vendoritem.notes = itemlistofdata.listitems.FirstOrDefault().Note;
                                    vendoritem.deliveryprice = (decimal)itemlistofdata.listitems.FirstOrDefault().DeliveryPrice;
                                    vendoritem.distanceKM = itemlistofdata.listitems.FirstOrDefault().distanceKM;
                                    List<ItemVendorApi> itemcart = new List<ItemVendorApi>();
                                    foreach (var item in itemlistofdata.listitems)
                                    {
                                        var ItemVendorApi = new ItemVendorApi { productId = item.ProductID, quantity = item.ProductQuantity };
                                        itemcart.Add(ItemVendorApi);
                                    }
                                    vendoritem.products = itemcart;
                                    listofitem.Add(vendoritem);
                                }
                                orderobject = new AddOrderViewModelApiNew
                                {
                                    addressID = masterdata.AddressID,
                                    customerReference = masterdata.SessionID ?? string.Concat(Guid.NewGuid().ToString(), Guid.NewGuid().ToString()),
                                    invoiceId = dataID.PaymentStatus.Data.InvoiceId.ToString(),
                                    paymentTypeId = (int)PaymentTypeEnum.Payment_Card,
                                    promoCodeID = promoCodeID,
                                    vendorOrder = listofitem
                                };
                                var orderstatus = AddOrder(orderobject, masterdata.Customers.UserId, masterdata.ScheduleDate, masterdata.ScheduleTime, masterdata.OrderTypeId);
                            }
                            catch (Exception ex)
                            {

                            }
                            _BLSite.RemoveCart(masterdata.CartMasterID);
                            status = "OK";

                            #endregion
                        }
                        else
                        {
                            status += dataID.PaymentStatus.Data.InvoiceTransactions?.FirstOrDefault()?.Error?.ToString();
                            status += dataID.PaymentStatus.Data.InvoiceTransactions?.LastOrDefault()?.Error?.ToString();


                            var masterdata = _BLSite.GeCartMasterByID(int.Parse(dataID.PaymentStatus.Data.UserDefinedField));
                            var promoCodeID = _blCustomer.GetPromoCodeId(masterdata.Promocode);

                            List<VendorOrderApi> listofitem = new List<VendorOrderApi>();
                            var results = _BLSite.GeCartDetailsVM(int.Parse(dataID.PaymentStatus.Data.UserDefinedField), Utility.CurrentLanguageCode).ToList();
                            var listofdata = results.GroupBy(
                            p => p.VendorsID,
                            (key, g) => new { VendorsID = key, listitems = g.ToList() });
                            var orderobject = new AddOrderViewModelApi();
                            try
                            {
                                foreach (var itemlistofdata in listofdata)
                                {
                                    VendorOrderApi vendoritem = new VendorOrderApi();
                                    vendoritem.vendorId = itemlistofdata.VendorsID;
                                    vendoritem.notes = itemlistofdata.listitems.FirstOrDefault().Note;
                                    List<ItemVendorApi> itemcart = new List<ItemVendorApi>();
                                    foreach (var item in itemlistofdata.listitems)
                                    {
                                        var ItemVendorApi = new ItemVendorApi { productId = item.ProductID, quantity = item.ProductQuantity };
                                        itemcart.Add(ItemVendorApi);
                                    }
                                    vendoritem.products = itemcart;
                                    listofitem.Add(vendoritem);
                                }
                                orderobject = new AddOrderViewModelApi
                                {
                                    addressID = masterdata.AddressID,
                                    customerReference = masterdata.SessionID ?? string.Concat(Guid.NewGuid().ToString(), Guid.NewGuid().ToString()),
                                    invoiceId = dataID.PaymentStatus.Data.InvoiceId.ToString(),
                                    paymentTypeId = (int)PaymentTypeEnum.Payment_Card,
                                    promoCodeID = promoCodeID,
                                    vendorOrder = listofitem
                                };

                            }
                            catch (Exception ex)
                            {
                                status += "<br />" + "Exception Message : " + ex.Message + "<br > Exception InnerException Message :" + ex.InnerException?.Message + "<br > Exception InnerException InnerException Message :" + ex.InnerException?.InnerException?.Message;
                            }
                        }

                        #endregion
                    }
                    else
                    {
                        var jsons = JsonConvert.SerializeObject(CheckIsPaid_invoice(_paymentId).Value);
                        StatusDesc dataIDs = JsonConvert.DeserializeObject<StatusDesc>(jsons);
                        if (dataIDs.PaymentStatus.Data.InvoiceStatus.ToLower() == "paid" && tripChargeLog.Status.ToLower() != dataID.PaymentStatus.Data.InvoiceStatus.ToLower())
                        {
                            // is Order Not Added Before
                            if (!_blCustomer.IsOrderExistByInvoiceId(dataID.PaymentStatus.Data.InvoiceId.ToString()))
                            {
                                #region add Order 


                                var masterdata = _BLSite.GeCartMasterByID(int.Parse(dataID.PaymentStatus.Data.UserDefinedField));
                                var promoCodeID = _blCustomer.GetPromoCodeId(masterdata.Promocode);

                                List<VendorOrderApiNew> listofitem = new List<VendorOrderApiNew>();
                                var results = _BLSite.GeCartDetailsVM(int.Parse(dataID.PaymentStatus.Data.UserDefinedField), Utility.CurrentLanguageCode).ToList();
                                var listofdata = results.GroupBy(
                                p => p.VendorsID,
                                (key, g) => new { VendorsID = key, listitems = g.ToList() });
                                var orderobject = new AddOrderViewModelApiNew();
                                try
                                {
                                    foreach (var itemlistofdata in listofdata)
                                    {
                                        VendorOrderApiNew vendoritem = new VendorOrderApiNew();
                                        vendoritem.vendorId = itemlistofdata.VendorsID;
                                        vendoritem.notes = itemlistofdata.listitems.FirstOrDefault().Note;
                                        vendoritem.deliveryprice = (decimal)itemlistofdata.listitems.FirstOrDefault().DeliveryPrice;
                                        vendoritem.distanceKM = itemlistofdata.listitems.FirstOrDefault().distanceKM;

                                        List<ItemVendorApi> itemcart = new List<ItemVendorApi>();
                                        foreach (var item in itemlistofdata.listitems)
                                        {
                                            var ItemVendorApi = new ItemVendorApi { productId = item.ProductID, quantity = item.ProductQuantity };
                                            itemcart.Add(ItemVendorApi);
                                        }
                                        vendoritem.products = itemcart;
                                        listofitem.Add(vendoritem);
                                    }
                                    orderobject = new AddOrderViewModelApiNew
                                    {
                                        addressID = masterdata.AddressID,
                                        customerReference = masterdata.SessionID ?? string.Concat(Guid.NewGuid().ToString(), Guid.NewGuid().ToString()),
                                        invoiceId = dataID.PaymentStatus.Data.InvoiceId.ToString(),
                                        paymentTypeId = (int)PaymentTypeEnum.Payment_Card,
                                        promoCodeID = promoCodeID,
                                        vendorOrder = listofitem
                                    };
                                    var orderstatus = AddOrder(orderobject, masterdata.Customers.UserId, masterdata.ScheduleDate, masterdata.ScheduleTime, masterdata.OrderTypeId);
                                }
                                catch (Exception ex)
                                {
                                 
                                }
                                _BLSite.RemoveCart(masterdata.CartMasterID);
                                status = "OK";

                                #endregion
                            }
                            status = "OK";
                        }
                    }
                }
                else
                {
                    status = "ERROR : Transaction Not Found Our System";
                }
            }
            else
            {
                status = "ERROR : Transaction Not Found in Bank Provider";
            }
            return Json(status);
        }
        /// <summary>
        /// paying order With Wallet
        /// </summary>
        /// <param name="_sessionID"></param>
        /// <returns></returns>
        [HttpGet]
        public ResultMessage executePaymentWallet(string _sessionID)
        {
            var total = GetOrderTotals(int.Parse(_sessionID));
            if (total > 0)
            {
                #region add Order 

                var masterdata = _BLSite.GeCartMasterByID(int.Parse(_sessionID));
                var promoCodeID = _blCustomer.GetPromoCodeId(masterdata.Promocode);
                List<VendorOrderApiNew> listofitem = new List<VendorOrderApiNew>();
                var results = _BLSite.GeCartDetailsVM(int.Parse(_sessionID), Utility.CurrentLanguageCode).ToList();

                var listofdata = results.GroupBy(
                p => p.VendorsID,
                (key, g) => new { VendorsID = key, listitems = g.ToList() });

                var orderobject = new AddOrderViewModelApiNew();
                try
                {
                    foreach (var itemlistofdata in listofdata)
                    {
                        VendorOrderApiNew vendoritem = new VendorOrderApiNew();
                        vendoritem.vendorId = itemlistofdata.VendorsID;
                        vendoritem.notes = itemlistofdata.listitems.FirstOrDefault().Note;
                        vendoritem.deliveryprice = (decimal)itemlistofdata.listitems.FirstOrDefault().DeliveryPrice;
                        vendoritem.distanceKM = itemlistofdata.listitems.FirstOrDefault().distanceKM;

                        List<ItemVendorApi> itemcart = new List<ItemVendorApi>();
                        foreach (var item in itemlistofdata.listitems)
                        {
                            var ItemVendorApi = new ItemVendorApi { productId = item.ProductID, quantity = item.ProductQuantity };
                            itemcart.Add(ItemVendorApi);
                        }
                        vendoritem.products = itemcart;
                        listofitem.Add(vendoritem);
                    }
                    orderobject = new AddOrderViewModelApiNew
                    {
                        addressID = masterdata.AddressID,
                        customerReference = masterdata.SessionID ?? string.Concat(Guid.NewGuid().ToString(), Guid.NewGuid().ToString()),
                        invoiceId = _sessionID,
                        paymentTypeId = (int)PaymentTypeEnum.Wallet,
                        promoCodeID = promoCodeID,
                        vendorOrder = listofitem
                    };
                    var orderstatus = AddOrder(orderobject, masterdata.Customers.UserId, masterdata.ScheduleDate, masterdata.ScheduleTime, masterdata.OrderTypeId);
                    if (orderstatus.Status)
                    {
                        _BLSite.RemoveCart(masterdata.CartMasterID);
                        _result.Message = "OK";
                        _result.Status = true;
                        _result.ID = orderstatus.ID;
                    }
                    else
                    {
                        _result.Message = orderstatus.Message;
                        _result.Status = false;
                        _result.ID = 0;
                    }
                }
                catch (Exception ex)
                {
                }


                #endregion
            }
            else
            {
                _result.Message = "totalequalzero";
                _result.Status = false;
            }
            return (_result);
        }
        #endregion
        #region My Fatorrah actions
        public JsonResult CheckIsPaid_invoice(string paymentId)
        {
            string SecertToken = _configuration["PaymyFatoraSecertToken"];
            string baseURL = _configuration["PaymyFatoraUrl"];
            var url = baseURL + "/v2/GetPaymentStatus";


            string json = "{\"Key\":\"" + paymentId + "\",\"KeyType\": \"paymentId\"}";

            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Bearer " + SecertToken + "");
            request.AddParameter("application/json", json, ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            var StatusCode = response.StatusCode;
            PaymentStatus deserializedProduct = JsonConvert.DeserializeObject<PaymentStatus>(response.Content);
            if (deserializedProduct.Data.InvoiceStatus != "Paid")
            {
                return Json(new { Orderid = int.Parse("0"), statusdescription = deserializedProduct.Data.InvoiceStatus, PaymentStatus = deserializedProduct });
            }
            return Json(new { Orderid = int.Parse(deserializedProduct.Data.UserDefinedField.ToString()), statusdescription = "", PaymentStatus = deserializedProduct });
        }
        public JsonResult CheckIsPaid_invoice_Draft(string paymentId)
        {
            string SecertToken = _configuration["PaymyFatoraSecertToken"];
            string baseURL = _configuration["PaymyFatoraUrl"];
            var url = baseURL + "/v2/GetPaymentStatus";


            string json = "{\"Key\":\"" + paymentId + "\",\"KeyType\": \"paymentId\"}";

            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Bearer " + SecertToken + "");
            request.AddParameter("application/json", json, ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            var StatusCode = response.StatusCode;
            PaymentStatus deserializedProduct = JsonConvert.DeserializeObject<PaymentStatus>(response.Content);
            if (deserializedProduct.Data.InvoiceStatus != "Paid")
            {
                return Json(new { Orderid = int.Parse("0"), statusdescription = deserializedProduct.Data.InvoiceStatus, PaymentStatus = deserializedProduct });
            }
            return Json(new { Orderid = int.Parse(deserializedProduct.Data.CustomerReference.ToString()), statusdescription = "", PaymentStatus = deserializedProduct });
        }
        public IActionResult PayingReditect(string paymentId, string Id)
        {
            var json = JsonConvert.SerializeObject(CheckIsPaid_invoice(paymentId).Value);
            StatusDesc dataID = JsonConvert.DeserializeObject<StatusDesc>(json);
            TransactionCard tripChargeLog = _BLTransaction.GetTransactionCard(paymentId, dataID.PaymentStatus.Data.InvoiceId.ToString(), dataID.PaymentStatus.Data.CustomerReference.ToString());
            var CustomerById = _blCustomer.GetCustomerById(tripChargeLog.CustomersID);
            ViewBag.UserName = CustomerById.UserId;
            ViewBag.paymentId = paymentId;
            return View();
        }
        public JsonResult PayingReditectPaidStatus(string _paymentId)
        {
            string status = "";
            var json = JsonConvert.SerializeObject(CheckIsPaid_invoice(_paymentId).Value);
            StatusDesc dataID = JsonConvert.DeserializeObject<StatusDesc>(json);
            if (dataID != null)
            {
                if (dataID.PaymentStatus.Data.InvoiceStatus.ToLower() == "paid")
                {
                    #region  Order paid
                    status = "OK";
                    #endregion
                }
                else
                {
                    status += dataID.PaymentStatus.Data.InvoiceTransactions?.FirstOrDefault()?.Error?.ToString();
                    status += "<br />";
                    status += dataID.PaymentStatus.Data.InvoiceTransactions?.FirstOrDefault()?.Error?.ToString() != dataID.PaymentStatus.Data.InvoiceTransactions?.LastOrDefault()?.Error?.ToString() ? dataID.PaymentStatus.Data.InvoiceTransactions?.LastOrDefault()?.Error?.ToString() : "";
                }
            }
            else
            {
                status = "ERROR : Transaction Not Found in Bank Provider";
            }
            return Json(status);
        }
        #endregion
        #region Cart
        [HttpGet]
        public JsonResult RemoveItemFromCart(int _sessionID, int _itemid)
        {
            var cartDetails = _BLSite.GeCartDetails(_sessionID, _itemid);
            var masterdata = _BLSite.RemoveItemFromCart(cartDetails.CartDetailsID);
            return Json(cartDetails.CartDetailsID);
        }
        [HttpGet]
        public JsonResult RemoveAllItemCart(int _sessionID)
        {
            var masterdata = _BLSite.RemoveCart(_sessionID);
            return Json("OK");
        }
        public JsonResult CreateSession(string _sessionID)
        {

            if (_sessionID != null && _BLSite.IsExistSession(_sessionID, false))
            {
                var sessiondata = _BLSite.GeCartMaster(_sessionID, false);
                return Json(new { sessionID = sessiondata.CartMasterID, sessionToken = sessiondata.SessionID });

            }
            else
            {
                string sessionToken = string.Concat(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
                var CustomerData = _blCustomer.GetCustomerByUser(User.GetUserIdInt());
                int? CustomerId = null;
                if (CustomerData != null)
                {
                    CustomerId = CustomerData.CustomersID;
                }
                var insertSession = _BLSite.InsertSession(new CartMaster()
                {
                    CartMasterStatus = CartMasterStatusEnum.Cart_New.ToString(),
                    CustomersID = CustomerId,
                    ExpiryDate = DateTime.Now.AddHours(3).AddDays(7),
                    IsAnOrder = false,
                    SessionID = sessionToken

                });

                return Json(new { sessionID = insertSession.CartMasterID, sessionToken = insertSession.SessionID });

            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_itemid"></param>
        /// <param name="_quantity"></param>
        /// <param name="_sessionID"></param>
        /// <param name="_type"> p : mean Plus
        /// m : mean minus
        /// c : mean : change </param>
        /// <returns></returns>
        public JsonResult addToCart(string _itemid, string _quantity, string _sessionID, string _type)
        {
            int cartMasterID = int.Parse(_sessionID);
            string sessionToken = "";
            bool _newsession = false;
            CartDetails cartDetails = new CartDetails();
            var DeliveryPriceVatPercent = _blConfiguration.GetDeliveryPriceVatPercent();

            if (!_BLSite.IsExistMaster(int.Parse(_sessionID), false))
            {
                var CustomerData = _blCustomer.GetCustomerByUser(User.GetUserIdInt());
                int? CustomerId = null;
                if (CustomerData != null)
                {
                    CustomerId = CustomerData.CustomersID;
                }
                sessionToken = string.Concat(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
                var insertSession = _BLSite.InsertSession(new CartMaster()
                {
                    CartMasterStatus = CartMasterStatusEnum.Cart_New.ToString(),
                    CustomersID = CustomerId,
                    ExpiryDate = DateTime.Now.AddHours(3).AddDays(7),
                    IsAnOrder = false,
                    SessionID = sessionToken,
                });
                cartMasterID = insertSession.CartMasterID;
                _newsession = true;
            }
            else
            {
                sessionToken = _BLSite.GeCartMasterByID(int.Parse(_sessionID)).SessionID;
            }

            var productData = _blProduct.GetProductById(int.Parse(_itemid));

            if (_BlVendor.IsVendorWorking(productData.VendorsID))
            {
                cartDetails = _BLSite.GeCartDetails(cartMasterID, int.Parse(_itemid));
                if (cartDetails == null)
                {

                    // add item
                    cartDetails = new CartDetails()
                    {
                        CartMasterID = cartMasterID,
                        ProductDiscount = _BLGeneral.CalcProductNet(productData.Discount, productData.StartDiscountDate, productData.EndDiscountDate, productData.Price),
                        ProductID = productData.ProductID,
                        ProductImage = productData.Logo,
                        ProductNameAr = productData.NameAr,
                        ProductNameEn = productData.NameEn,
                        ProductPrice = _blConfiguration.GetVatForPrice(DeliveryPriceVatPercent, productData.Price - _BLGeneral.CalcProductNet(productData.Discount, productData.StartDiscountDate, productData.EndDiscountDate, productData.Price)).PriceWithVat,
                        ProductQuantity = (_type == "p" || _type == "m" ? 1 : int.Parse(_quantity)),


                    };
                    _BLSite.AddItemToCart(cartDetails);
                }
                else
                {
                    // update item
                    if (_type == "p")
                    {
                        cartDetails.ProductQuantity += 1;

                    }
                    else if (_type == "m")
                    {
                        cartDetails.ProductQuantity = cartDetails.ProductQuantity >= 1 ? cartDetails.ProductQuantity - 1 : cartDetails.ProductQuantity;

                    }
                    else
                    {
                        cartDetails.ProductQuantity = int.Parse(_quantity);

                    }
                    _BLSite.updateCartDetails(cartDetails);

                }

                return Json(new
                {
                    status = true,
                    newsession = _newsession,
                    sessionToken = sessionToken,
                    itemDetailsid = cartDetails.CartDetailsID,
                    itemDetailsguid = productData.ProductGuid,
                    itemname = UI.InfraStructure.Utility.CurrentLanguageCode == "en" ? productData.NameEn : productData.NameAr,
                    itemqu = cartDetails.ProductQuantity,
                    itemimg = productData.Logo,
                    itemprice = _blConfiguration.GetVatForPrice(DeliveryPriceVatPercent, productData.Price - _BLGeneral.CalcProductNet(productData.Discount, productData.StartDiscountDate, productData.EndDiscountDate, productData.Price)).PriceWithVat,
                    itemTotal = cartDetails.ProductQuantity * (_blConfiguration.GetVatForPrice(DeliveryPriceVatPercent, productData.Price - _BLGeneral.CalcProductNet(productData.Discount, productData.StartDiscountDate, productData.EndDiscountDate, productData.Price)).PriceWithVat),
                    sessionID = cartMasterID.ToString(),
                    itemID = productData.ProductID,

                });
            }
            else
            {
                return Json(new
                {
                    status = false,
                    newsession = _newsession,
                    sessionToken = sessionToken,
                    itemDetailsguid = productData.ProductGuid,
                    itemname = UI.InfraStructure.Utility.CurrentLanguageCode == "en" ? productData.NameEn : productData.NameAr,
                    itemimg = productData.Logo,
                    itemprice = _blConfiguration.GetVatForPrice(DeliveryPriceVatPercent, productData.Price - _BLGeneral.CalcProductNet(productData.Discount, productData.StartDiscountDate, productData.EndDiscountDate, productData.Price)).PriceWithVat,
                    sessionID = cartMasterID.ToString(),
                    itemID = productData.ProductID,
                    message = Resources.Homemade.Vendor_Closed,
                });
            }

        }
        public JsonResult CartData(string _sessionID)
        {
            try
            {
                int cartMasterID = int.Parse(_sessionID);
                List<CartDetails> cartDetails = new List<CartDetails>();
                var CustomerData = _blCustomer.GetCustomerByUser(User.GetUserIdInt());
                int CustomerId = 0;
                if (CustomerData != null)
                {
                    CustomerId = CustomerData.CustomersID;
                    if (!_BLSite.IsExistMaster(int.Parse(_sessionID), CustomerId, false))
                    {
                        var cartmaster = _BLSite.GetCartMaster(CustomerId, false);
                        if (cartmaster != null)
                        {
                            _BLSite.updateCartMasterWithCustomer(cartmaster.CartMasterID, cartMasterID, CustomerId);
                        }
                    }

                }
                if (_BLSite.IsExistMaster(int.Parse(_sessionID), false))
                {

                    cartDetails = _BLSite.GeCartDetails(cartMasterID).ToList();
                    return Json(new
                    {
                        status = true,
                        recordCount = cartDetails.Count(),
                        listofdata = cartDetails.Select(e => new
                        {
                            itemDetailsid = e.CartDetailsID,
                            itemDetailsguid = e.Product.ProductGuid,
                            itemname = UI.InfraStructure.Utility.CurrentLanguageCode == "en" ? (e.ProductNameEn.Length > 15 ? e.ProductNameEn.Substring(0, 15) : e.ProductNameEn) : (e.ProductNameAr.Length > 15 ? e.ProductNameAr.Substring(0, 15) : e.ProductNameAr),
                            itemqu = e.ProductQuantity,
                            itemimg = e.ProductImage,
                            itemprice = e.ProductPrice,
                            itemTotal = e.ProductQuantity * e.ProductPrice,
                            itemID = e.ProductID,

                        })
                    });
                }
                else
                {
                    return Json(new
                    {
                        status = true,
                        recordCount = 0,
                        listofdata = new List<CartDetails>()
                    });
                }
            }
            catch (Exception ex)
            {

                return Json(new
                {
                    status = true,
                    recordCount = 0,
                    listofdata = new List<CartDetails>()
                });
            }

        }
        public JsonResult GetCartTotal(string _sessionID)
        {
            try
            {
                int cartMasterID = int.Parse(_sessionID);
                if (_BLSite.IsExistMaster(int.Parse(_sessionID), false))
                {

                    decimal total = _BLSite.GeCartDetails(cartMasterID).Sum(e => e.ProductQuantity * e.ProductPrice);
                    return Json(new
                    {
                        status = true,
                        total = Math.Round(total, 2)
                    });
                }
                else
                {
                    return Json(new
                    {
                        status = true,
                        total = 0,
                    });
                }
            }
            catch (Exception ex)
            {

                return Json(new
                {
                    status = true,
                    total = 0,
                });
            }

        }
        public IActionResult ViewCart()
        {
            return View();
        }
        public JsonResult checkPromocode(int _sessionID, string _code)
        {
            var userdata = _blCustomer.GetCustomerByUser(User.GetUserIdInt());
            var masterdata = _BLSite.GeCartMasterByID(_sessionID);


            var total = GetOrderTotals(_sessionID);


            var results = _BLSite.GeCartDetailsVM(_sessionID, Utility.CurrentLanguageCode).ToList();
            var listofdata = results.GroupBy(
            p => p.VendorsID,
            (key, g) => new { VendorsID = key, listitems = g.ToList() });

            var NationalityList = _blCustomer.CheckPromoCodeValid_Web(_code, (int)masterdata.CustomersID, total, Utility.CurrentLanguageCode, listofdata.Select(e => e.VendorsID).ToList(), masterdata.AddressID, _sessionID);


            if (NationalityList.status)
            {
                masterdata.Promocode = _code;
                masterdata.PromocodeDiscount = NationalityList.discount;
                NationalityList.vatTotal = NationalityList.vatTotal;
            }
            else
            {
                masterdata.Promocode = "";
                masterdata.PromocodeDiscount = 0;

            }
            _BLSite.updateCartMaster(masterdata);
            return Json(NationalityList);
        }
        public JsonResult checkPromocodeByOrder(int _sessionID, string _code)
        {
            var userdata = _blCustomer.GetCustomerByUser(User.GetUserIdInt());
            var masterdata = _blOrders.getOrderData(_sessionID);


            var total = GetOrderTotalsByOrder(_sessionID);


            var results = _BLSite.GeCartDetailsVMByOrder(_sessionID, Utility.CurrentLanguageCode).ToList();
            var listofdata = results.GroupBy(
            p => p.VendorsID,
            (key, g) => new { VendorsID = key, listitems = g.ToList() });

            var NationalityList = _blCustomer.CheckPromoCodeValid_WebByOrder(_code, (int)masterdata.CustomersID, total, Utility.CurrentLanguageCode, listofdata.Select(e => e.VendorsID).ToList(), masterdata.CustomerLocationID, _sessionID);


            if (NationalityList.status)
            {
                masterdata.PromoCodeID = NationalityList.promoCodeID != 0 ? NationalityList.promoCodeID : null;
                masterdata.Discount = NationalityList.discount;
                _blOrders.updateOrders(masterdata);
                NationalityList.vatTotal = NationalityList.vatTotal;
            }
            return Json(NationalityList);
        }
        public JsonResult changeAddress(int _sessionID, int _addressID)
        {
            var masterdata = _BLSite.GeCartMasterByID(_sessionID);
            masterdata.AddressID = _addressID;
            _BLSite.updateCartMaster(masterdata);
            return Json(true);
        }
        public JsonResult changeAddressByOrder(int _sessionID, int _addressID)
        {
            var masterdata = _blOrders.getOrderData(_sessionID);
            masterdata.CustomerLocationID = _addressID;
            _blOrders.updateOrders(masterdata);
            return Json(true);
        }
        public decimal GetOrderTotals(int _sessionID)
        {
            var masterdata = _BLSite.GeCartMasterByID(_sessionID);
            var sss = masterdata.CartDetails.Where(e => !e.IsDeleted).Sum(e => (e.ProductPrice) * e.ProductQuantity);
            return sss;
        }
        public decimal GetOrderTotalsByOrder(int _sessionID)
        {
            var masterdata = _blOrders.getOrderData(_sessionID, "OrderVendor.OrderItems");
            var sss = masterdata.OrderVendor.Sum(x => x.OrderItems.Where(x => !x.IsDeleted).Sum(e => (e.Price - e.Discount) * e.Quantity));
            return sss;
        }
        public bool addNote(int _sessionID, int _vendorid, string _note)
        {
            List<CartDetails> cartDetails = new List<CartDetails>();
            var results = _BLSite.GeCartDetailsVM(_sessionID, Utility.CurrentLanguageCode).ToList();

            foreach (var itemlistofdata in results.Where(e => e.VendorsID == _vendorid))
            {
                CartDetails cart = _BLSite.GeCartDetailsByID(itemlistofdata.CartDetailsID);
                cart.Note = _note;
                cartDetails.Add(cart);
            }
            _BLSite.updateRangeCartDetails(cartDetails);
            return true;
        }
        public bool addNoteOrder(int _sessionID, int _vendorid, string _note)
        {
            List<Orders> orders = new List<Orders>();
            var results = _BLSite.GeCartDetailsVMByOrder(_sessionID, Utility.CurrentLanguageCode).ToList();

            foreach (var itemlistofdata in results.Where(e => e.VendorsID == _vendorid))
            {
                Orders order = _blOrders.getOrderData(itemlistofdata.CartDetailsID);
                order.Notes = _note;
                orders.Add(order);
            }
            _BLSite.updateRangeOrders(orders);
            return true;
        }
        [HttpPost]
        public JsonResult DeleteOrderVendor(int vendorId, int ordersID)
        {
            var result = false;
            try
            {
                result = _blOrders.DeleteOrderVendor(vendorId, ordersID);
            }
            catch (Exception ex)
            {
            }
            ResultMessage rResultMessage = result ?
                new ResultMessage { Status = result, Message = Resources.Homemade.SuccessDeleteMessage, ResultType = ResultMessageType.success.ToString() }
                :
                new ResultMessage { Status = result, Message = Resources.Homemade.FailDeleteMessage, ResultType = ResultMessageType.error.ToString() };
            return Json(rResultMessage);
        }
        [HttpPost]
        public JsonResult DeleteItemFromPending(int productID, int ordersID)
        {
            var result = false;
            int VendorsID = 0;
            bool IsDeletedVendor = false;
            decimal price = 0;
            try
            {
                var orderItems = _blOrders.GetOrderItemsByProductAndOrderAndPending(productID, ordersID, "OrderVendor");
                if (orderItems != null)
                {
                    VendorsID = orderItems.OrderVendor.VendorsID;
                    price = orderItems.Price * orderItems.Quantity;
                    result = _blOrders.DeleteItemFromPending(productID, ordersID, out IsDeletedVendor);
                }
            }
            catch (Exception ex)
            {
            }
            ResultMessage rResultMessage = result ?
                new ResultMessage { Status = result, ID = VendorsID, ObjResult = price.ToString("#.00"), IsSendEmail = IsDeletedVendor, Message = Resources.Homemade.SuccessDeleteMessage, ResultType = ResultMessageType.success.ToString() }
                :
                new ResultMessage { Status = result, ID = VendorsID, ObjResult = price.ToString("#.00"), IsSendEmail = IsDeletedVendor, Message = Resources.Homemade.FailDeleteMessage, ResultType = ResultMessageType.error.ToString() };
            return Json(rResultMessage);
        }
        public JsonResult IsQuantityAvilable(int masterid)
        {
            var obj = _BLSite.GeCartDetails(masterid).Any(x => x.Product.IsLimited && x.Product.DailyQuantity < x.ProductQuantity);
            return Json(obj);
        }
        #endregion
        #region Order
        public ResultMessage AddOrder(AddOrderViewModelApiNew model, int UserId, DateTime? ScheduleDate, DateTime? ScheduleTime, int OrderTypeId)
        {
            try
            {
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
                                    model.scheduleDate = ScheduleDate.ToString();
                                    model.scheduleTime = ScheduleTime.ToString();
                                    model.orderTypeId = OrderTypeId;

                                    var NationalityList = _blCustomer.AddOrder_New(model, UserId, CustomerId, _configuration["FireBaseKey"], "WEB", "", ScheduleDate, ScheduleTime);
                                    if (NationalityList.message == "true")
                                    {
                                        _result.Message = Utility.CurrentLanguageCode == "ar" ? "تم ارسال الطلب بنجاح" : "Order Send Successfully";
                                        _result.Status = true;
                                        _result.ID = NationalityList.orderId;
                                        return (_result);
                                    }
                                    else
                                    {
                                        _result.Message = NationalityList.message;
                                        _result.Status = false;
                                        return (_result);
                                    }
                                }
                                else
                                {
                                    _result.Message = Utility.CurrentLanguageCode == "ar" ? "فضلا قم بتحديد العنوان" : "Please choose address";
                                    _result.Status = false;
                                    return (_result);
                                }
                            }
                            else
                            {
                                _result.Message = Utility.CurrentLanguageCode == "ar" ? "فضلا قم بتحديد المنتجات" : "Please choose products";
                                _result.Status = false;
                                return (_result);
                            }
                        }
                        else
                        {
                            _result.Message = Utility.CurrentLanguageCode == "ar" ? "فضلا قم بتحديد المنتجات" : "Please choose products";
                            _result.Status = false;
                            return (_result);
                        }
                    }
                    else
                    {
                        _result.Message = Utility.CurrentLanguageCode == "ar" ? "فضلا قم بتحديد طريقة الدفع" : "Payment Type not found";
                        _result.Status = false;
                        return (_result);
                    }
                }
                else
                {
                    _result.Message = Utility.CurrentLanguageCode == "ar" ? Resources.Response_ar.Error_in_the_drivers_username : Resources.Response.Error_in_the_drivers_username;
                    _result.Status = false;
                    return (_result);
                }
            }
            catch (Exception e)
            {
                _result.Message = Utility.CurrentLanguageCode == "ar" ? Resources.Response_ar.Error + e.ToString() : Resources.Response.Error + e.ToString();
                _result.Status = false;
                return (_result);
            }
        }
        [HttpPost]
        public JsonResult AddScheduled(string _sessionID, string ScheduledDate, string ScheduledTime)
        {
            #region add Order 
            string[] timeformat = { "hh:mm tt", "h:mm tt" };
            DateTime? Time = !string.IsNullOrEmpty(ScheduledTime) ? DateTime.ParseExact(ScheduledTime, timeformat, System.Globalization.CultureInfo.InvariantCulture) : null;
            DateTime? Date = !string.IsNullOrEmpty(ScheduledDate) ? DateTime.ParseExact(ScheduledDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture) : null;

            if (Date != null && Time != null)
            {
                if ((Date.Value.Date == _BLGeneral.GetDateTimeNow().AddHours(2).Date && Time.Value > _BLGeneral.GetDateTimeNow().AddHours(2)) || Date > _BLGeneral.GetDateTimeNow().AddHours(2))
                {
                    var masterdata = _BLSite.GeCartMasterByID(int.Parse(_sessionID));
                    if (masterdata != null)
                    {
                        var IsSuccess = _BLSite.updateCartMasterSchedule(masterdata, Date, Time);
                        if (IsSuccess)
                        {
                            _result.Message = Resources.Homemade.AddScheduledSuccessfly;
                            _result.Status = true;
                            _result.ResultType = ResultMessageType.success.ToString();
                            return Json(_result);
                        }
                        else
                        {
                            _result.Message = Resources.Homemade.Error;
                            _result.Status = false;
                            _result.ResultType = ResultMessageType.error.ToString();
                            return Json(_result);
                        }
                    }
                    else
                    {
                        _result.Message = Resources.Homemade.Error;
                        _result.Status = false;
                        _result.ResultType = ResultMessageType.error.ToString();
                        return Json(_result);
                    }
                }
                else
                {
                    _result.Message = Resources.Homemade.You_cannot_add_an_appointment_to_scheduling_less_than_today;
                    _result.Status = false;
                    _result.ResultType = ResultMessageType.error.ToString();
                    return Json(_result);
                }
            }
            else
            {
                _result.Message = Resources.Homemade.Please_Insert_Data;
                _result.Status = false;
                _result.ObjResult = (Date == null ? "Problem in date" + ScheduledDate : "") + (Time == null ? "Problem in time" + ScheduledTime : "");
                _result.ResultType = ResultMessageType.error.ToString();
                return Json(_result);
            }

            #endregion
        }
        [HttpPost]
        public JsonResult CancelSchedule(string _sessionID)
        {
            #region add Order 
            var masterdata = _BLSite.GeCartMasterByID(int.Parse(_sessionID));
            if (masterdata != null)
            {
                var IsSuccess = _BLSite.CancelCartMasterSchedule(masterdata);
                if (IsSuccess)
                {
                    _result.Message = Resources.Homemade.CancelScheduledSuccessfly;
                    _result.Status = true;
                    _result.ResultType = ResultMessageType.success.ToString();
                    return Json(_result);
                }
                else
                {
                    _result.Message = Resources.Homemade.Error;
                    _result.Status = false;
                    _result.ResultType = ResultMessageType.error.ToString();
                    return Json(_result);
                }
            }
            else
            {
                _result.Message = Resources.Homemade.Error;
                _result.Status = false;
                _result.ResultType = ResultMessageType.error.ToString();
                return Json(_result);
            }
            #endregion
        }
        public IActionResult orderSuccess()
        {
            return View();
        }
        public IActionResult orderSuccesswallet()
        {
            return View();
        }
        public IActionResult orderfail()
        {
            return View();
        }
        public IActionResult completeOrder()
        {
            try
            {
                if (_blCompanyWorkingHours.IsCompanyWorking())
                {
                    if (Request.Cookies["sessionID"] != null)
                    {
                        var _sessionID = Request.Cookies["sessionID"].ToString();
                        var userdata = _blCustomer.GetCustomerByUser(User.GetUserIdInt());
                        var masterdata = _BLSite.GeCartMasterByID(int.Parse(_sessionID));
                        if (masterdata != null)
                        {
                            masterdata.CustomersID = userdata.CustomersID;
                            if (masterdata.AddressID == 0)
                            {
                                var address = _blCustomer.GetCustomerLocationByCustomersID(userdata.CustomersID);
                                if (address != null)
                                {
                                    masterdata.AddressID = address.CustomerLocationID;
                                }
                            }
                            _BLSite.updateCartMasterWithNewCustomer(masterdata);
                            var total = GetOrderTotals(int.Parse(_sessionID));
                            if (total > 0)
                            {
                                var vatdata = _blCustomer.GetVatValue_Web(total);
                                ViewBag.vat = vatdata.vat;
                                ViewBag.vatValue = vatdata.vatValue;
                                return View();
                            }
                            else
                            {
                                return RedirectToAction("ViewCart");
                            }
                        }
                        else
                        {
                            return RedirectToAction("ViewCart");
                        }

                    }
                    else
                    {
                        return RedirectToAction("ViewCart");
                    }
                }
                else
                {
                    return RedirectToAction("ViewCart");
                }
            }
            catch (Exception)
            {

                return RedirectToAction("Index");

            }
        }
        public IActionResult completePendingOrder(Guid? id)
        {
            try
            {
                if (id.HasValue)
                {
                    var userdata = _blCustomer.GetCustomerByUser(User.GetUserIdInt());
                    var order = _blOrders.getOrderData(id.Value, "");
                    if (order != null)
                    {
                        ViewBag.OrdersID = order.OrdersID;
                        var vatdata = _blCustomer.GetVatValue_Web(GetOrderTotalsByOrder(order.OrdersID));
                        if (_BLSite.GeCartDetailsVMByOrder(order.OrdersID, Utility.CurrentLanguageCode, User.GetUserIdInt()).Count(x => x.VendorsID != 0) > 0)
                        {
                            ViewBag.vat = vatdata.vat;
                            ViewBag.vatValue = vatdata.vatValue;
                            return View();
                        }
                        else
                        {
                            return RedirectToAction("Index");
                        }
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                return RedirectToAction("Index");

            }
            catch (Exception)
            {

                return RedirectToAction("Index");

            }
        }
        #endregion
        #region New Paid Order
        /// <summary>
        /// عند طلب تنفيذ الاوردر مع دفع اونلاين
        /// بتنزل الاوردر بحالته درافت 
        /// بعد كده بيروح يدفع ومعاه رقم الطلب
        /// لو الدفع نجح فا بنحول حالة الطلب الى تم الانشاء
        /// لو فشل فا بنخلية درافت زي مهو
        /// </summary>
        /// <param name="_sessionID"></param>
        /// <returns></returns>
        public async Task<JsonResult> executeOrderDraft(int _sessionID)
        {
            string message = "";
            bool status = false;
            var total = GetOrderTotals(_sessionID);
            if (total > 0)
            {
                var masterdata = _BLSite.GeCartMasterByID(_sessionID);
                var promoCodeID = _blCustomer.GetPromoCodeId(masterdata.Promocode);
                List<VendorOrderApiNew> listofitem = new List<VendorOrderApiNew>();
                var results = _BLSite.GeCartDetailsVM(_sessionID, Utility.CurrentLanguageCode).ToList();
                var listofdata = results.GroupBy(
                p => p.VendorsID,
                (key, g) => new { VendorsID = key, listitems = g.ToList() });
                var orderobject = new AddOrderViewModelApiNew();
                try
                {
                    foreach (var itemlistofdata in listofdata)
                    {
                        VendorOrderApiNew vendoritem = new VendorOrderApiNew();
                        vendoritem.vendorId = itemlistofdata.VendorsID;
                        vendoritem.notes = itemlistofdata.listitems.FirstOrDefault().Note;
                        vendoritem.deliveryprice = (decimal)itemlistofdata.listitems.FirstOrDefault().DeliveryPrice;
                        vendoritem.distanceKM = itemlistofdata.listitems.FirstOrDefault().distanceKM;
                        List<ItemVendorApi> itemcart = new List<ItemVendorApi>();
                        foreach (var item in itemlistofdata.listitems)
                        {
                            var ItemVendorApi = new ItemVendorApi { productId = item.ProductID, quantity = item.ProductQuantity };
                            itemcart.Add(ItemVendorApi);
                        }
                        vendoritem.products = itemcart;
                        listofitem.Add(vendoritem);
                    }
                    orderobject = new AddOrderViewModelApiNew
                    {
                        addressID = masterdata.AddressID,
                        customerReference = masterdata.SessionID ?? _BLGeneral.GenerateToken(100),
                        invoiceId = "Pending",
                        paymentTypeId = (int)PaymentTypeEnum.Payment_Card,
                        promoCodeID = promoCodeID,
                        vendorOrder = listofitem
                    };
                    var orderstatus = AddOrderDraft(orderobject, masterdata.Customers.UserId, masterdata.ScheduleDate, masterdata.ScheduleTime, masterdata.OrderTypeId);

                    if (orderstatus.Status)
                    {

                        message = PayNow((int)orderstatus.ID);
                        if (!string.IsNullOrWhiteSpace(message))
                        {
                            status = true;
                        }
                        else
                        {
                            status = false;
                        }
                    }
                    else
                    {
                        message = orderstatus.Message;
                        status = false;
                    }
                }
                catch (Exception ex)
                {
                   
                }
            }
            else
            {
                message = "totalequalzero";
            }
            return Json(new { status, message });

        }
        public async Task<JsonResult> executeOrderDraftFromPending(int _sessionID)
        {
            string message = "";
            bool status = false;

            #region add Order 
            var masterdata = _blOrders.getOrderData(_sessionID);
            var promoCodeID = masterdata.PromoCodeID;
            List<VendorOrderApiNew> listofitem = new List<VendorOrderApiNew>();
            var results = _BLSite.GeCartDetailsVMByOrder(_sessionID, Utility.CurrentLanguageCode).ToList();
            var listofdata = results.GroupBy(
            p => p.VendorsID,
            (key, g) => new { VendorsID = key, listitems = g.ToList() });
            var orderobject = new AddOrderViewModelApiNew();
            try
            {
                var total = GetOrderTotalsByOrder(_sessionID);

                var NationalityList = _blCustomer.CheckPromoCodeValid_WebByOrder((promoCodeID != null ? masterdata.PromoCode.Code : ""), (int)masterdata.CustomersID, total, Utility.CurrentLanguageCode, listofdata.Select(e => e.VendorsID).ToList(), masterdata.CustomerLocationID, _sessionID);

                if (NationalityList.status)
                {
                    masterdata.PromoCodeID = NationalityList.promoCodeID != 0 ? NationalityList.promoCodeID : null;
                    NationalityList.vatTotal = NationalityList.vatTotal;
                }

                foreach (var itemlistofdata in listofdata)
                {
                    VendorOrderApiNew vendoritem = new VendorOrderApiNew();
                    vendoritem.vendorId = itemlistofdata.VendorsID;
                    vendoritem.notes = itemlistofdata.listitems.FirstOrDefault().Note;
                    vendoritem.deliveryprice = NationalityList.listvendor.FirstOrDefault(x => x.vendorId == itemlistofdata.VendorsID).deliveryPrice;
                    vendoritem.distanceKM = NationalityList.listvendor.FirstOrDefault(x => x.vendorId == itemlistofdata.VendorsID).distanceKM;

                    List<ItemVendorApi> itemcart = new List<ItemVendorApi>();
                    foreach (var item in itemlistofdata.listitems)
                    {
                        var ItemVendorApi = new ItemVendorApi { productId = item.ProductID, quantity = item.ProductQuantity };
                        itemcart.Add(ItemVendorApi);
                    }
                    vendoritem.products = itemcart;
                    listofitem.Add(vendoritem);
                }
                orderobject = new AddOrderViewModelApiNew
                {
                    addressID = masterdata.CustomerLocationID,
                    customerReference = masterdata.ReferenceNo ?? _BLGeneral.GenerateToken(100),
                    invoiceId = _sessionID.ToString(),
                    paymentTypeId = (int)PaymentTypeEnum.Payment_Card,
                    promoCodeID = promoCodeID,
                    vendorOrder = listofitem
                };
                var orderstatus = ChangeOrderPendingToDraft(orderobject, _sessionID, masterdata.Customers.UserId);

                if (orderstatus.Status)
                {
                    message = PayNow((int)orderstatus.ID);
                    if (!string.IsNullOrWhiteSpace(message))
                    {
                        status = true;
                    }
                    else
                    {
                        status = false;
                       
                    }
                }
                else
                {
                    message = orderstatus.Message;
                    status = false;
                }
            }
            catch (Exception ex)
            {
            }

            #endregion

            return Json(new { status, message });

        }
        [HttpGet]
        public ResultMessage executePaymentWalletFromPending(int _sessionID)
        {
            #region add Order 
            var masterdata = _blOrders.getOrderData(_sessionID);
            var promoCodeID = masterdata.PromoCodeID;
            List<VendorOrderApiNew> listofitem = new List<VendorOrderApiNew>();
            var results = _BLSite.GeCartDetailsVMByOrder(_sessionID, Utility.CurrentLanguageCode).ToList();
            var listofdata = results.GroupBy(
            p => p.VendorsID,
            (key, g) => new { VendorsID = key, listitems = g.ToList() });
            var orderobject = new AddOrderViewModelApiNew();
            try
            {
                var total = GetOrderTotalsByOrder(_sessionID);

                var NationalityList = _blCustomer.CheckPromoCodeValid_WebByOrder((promoCodeID != null ? masterdata.PromoCode.Code : ""), (int)masterdata.CustomersID, total, Utility.CurrentLanguageCode, listofdata.Select(e => e.VendorsID).ToList(), masterdata.CustomerLocationID, _sessionID);

                if (NationalityList.status)
                {
                    masterdata.PromoCodeID = NationalityList.promoCodeID != 0 ? NationalityList.promoCodeID : null;
                    NationalityList.vatTotal = NationalityList.vatTotal;
                }

                foreach (var itemlistofdata in listofdata)
                {
                    VendorOrderApiNew vendoritem = new VendorOrderApiNew();
                    vendoritem.vendorId = itemlistofdata.VendorsID;
                    vendoritem.notes = itemlistofdata.listitems.FirstOrDefault().Note;
                    vendoritem.deliveryprice = NationalityList.listvendor.FirstOrDefault(x => x.vendorId == itemlistofdata.VendorsID).deliveryPrice;
                    vendoritem.distanceKM = NationalityList.listvendor.FirstOrDefault(x => x.vendorId == itemlistofdata.VendorsID).distanceKM;

                    List<ItemVendorApi> itemcart = new List<ItemVendorApi>();
                    foreach (var item in itemlistofdata.listitems)
                    {
                        var ItemVendorApi = new ItemVendorApi { productId = item.ProductID, quantity = item.ProductQuantity };
                        itemcart.Add(ItemVendorApi);
                    }
                    vendoritem.products = itemcart;
                    listofitem.Add(vendoritem);
                }
                orderobject = new AddOrderViewModelApiNew
                {
                    addressID = masterdata.CustomerLocationID,
                    customerReference = masterdata.ReferenceNo ?? _BLGeneral.GenerateToken(100),
                    invoiceId = _sessionID.ToString(),
                    paymentTypeId = (int)PaymentTypeEnum.Wallet,
                    promoCodeID = promoCodeID,
                    vendorOrder = listofitem
                };
                var orderstatus = ChangeOrderPendingToCreate(orderobject, _sessionID, masterdata.Customers.UserId);

                if (orderstatus.Status)
                {
                    _result.Message = "OK";
                    _result.Status = true;
                    _result.ID = orderstatus.ID;
                }
                else
                {
                    _result.Message = orderstatus.Message;
                    _result.Status = false;
                    _result.ID = 0;
                }
            }
            catch (Exception ex)
            {
               
            }


            #endregion

            return (_result);
        }
        public async Task<JsonResult> executeOrderPending(int _sessionID)
        {
            string message = "";
            string ordersGuid = "";
            bool status = false;
            string objResult = "";
            #region add Order 
            var masterdata = _BLSite.GeCartMasterByID(_sessionID);
            var promoCodeID = _blCustomer.GetPromoCodeId(masterdata.Promocode);
            List<VendorOrderApiNew> listofitem = new List<VendorOrderApiNew>();
            var results = _BLSite.GeCartDetailsVM(_sessionID, Utility.CurrentLanguageCode).ToList();
            var listofdata = results.GroupBy(
            p => p.VendorsID,
            (key, g) => new { VendorsID = key, listitems = g.ToList() });
            var orderobject = new AddOrderViewModelApiNew();
            try
            {
                foreach (var itemlistofdata in listofdata)
                {
                    VendorOrderApiNew vendoritem = new VendorOrderApiNew();
                    vendoritem.vendorId = itemlistofdata.VendorsID;
                    vendoritem.notes = itemlistofdata.listitems.FirstOrDefault().Note;
                    vendoritem.deliveryprice = (decimal)itemlistofdata.listitems.FirstOrDefault().DeliveryPrice;
                    vendoritem.distanceKM = itemlistofdata.listitems.FirstOrDefault().distanceKM;
                    List<ItemVendorApi> itemcart = new List<ItemVendorApi>();
                    foreach (var item in itemlistofdata.listitems)
                    {
                        var ItemVendorApi = new ItemVendorApi { productId = item.ProductID, quantity = item.ProductQuantity };
                        itemcart.Add(ItemVendorApi);
                    }
                    vendoritem.products = itemcart;
                    listofitem.Add(vendoritem);
                }
                orderobject = new AddOrderViewModelApiNew
                {
                    addressID = masterdata.AddressID,
                    customerReference = masterdata.SessionID ?? _BLGeneral.GenerateToken(100),
                    invoiceId = "Pending",
                    paymentTypeId = (int)PaymentTypeEnum.Payment_Card,
                    promoCodeID = promoCodeID,
                    vendorOrder = listofitem
                };
                var orderstatus = await AddOrderPending(orderobject, masterdata.Customers.UserId, masterdata.ScheduleDate, masterdata.ScheduleTime, masterdata.OrderTypeId);

                if (orderstatus.Status)
                {
                    message = orderstatus.Message;
                    status = true;

                    if (orderstatus.ID != 0 && orderstatus.ID != null)
                    {
                        var orders = _blOrders.GetOrdersById((int)orderstatus.ID);
                        if (orders != null)
                        {
                            ordersGuid = orders.OrdersGuid.ToString();
                        }
                    }
                }
                else
                {
                    message = orderstatus.Message;
                    status = false;
                    objResult = orderstatus.ObjResult;
                }
            }
            catch (Exception ex)
            {
               
            }

            #endregion
            return Json(new { status, message, ordersGuid, objResult });

        }
        public ResultMessage AddOrderDraft(AddOrderViewModelApiNew model, int UserId, DateTime? ScheduleDate, DateTime? ScheduleTime, int OrderTypeId)
        {
            try
            {
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
                                    model.scheduleDate = ScheduleDate.ToString();
                                    model.scheduleTime = ScheduleTime.ToString();
                                    model.orderTypeId = OrderTypeId;

                                    var NationalityList = _blCustomer.AddOrder_New_Drafts(model, UserId, CustomerId, _configuration["FireBaseKey"], "WEB", "", ScheduleDate, ScheduleTime);
                                    if (NationalityList.message == "true")
                                    {
                                        _result.Message = Utility.CurrentLanguageCode == "ar" ? "تم ارسال الطلب بنجاح" : "Order Send Successfully";
                                        _result.Status = true;
                                        _result.ID = NationalityList.orderId;

                                        return (_result);
                                    }
                                    else
                                    {
                                        _result.Message = NationalityList.message;
                                        _result.Status = false;
                                        return (_result);
                                    }
                                }
                                else
                                {
                                    _result.Message = Utility.CurrentLanguageCode == "ar" ? "فضلا قم بتحديد العنوان" : "Please choose address";
                                    _result.Status = false;
                                    return (_result);
                                }
                            }
                            else
                            {
                                _result.Message = Utility.CurrentLanguageCode == "ar" ? "فضلا قم بتحديد المنتجات" : "Please choose products";
                                _result.Status = false;
                                return (_result);
                            }
                        }
                        else
                        {
                            _result.Message = Utility.CurrentLanguageCode == "ar" ? "فضلا قم بتحديد المنتجات" : "Please choose products";
                            _result.Status = false;
                            return (_result);
                        }
                    }
                    else
                    {
                        _result.Message = Utility.CurrentLanguageCode == "ar" ? "فضلا قم بتحديد طريقة الدفع" : "Payment Type not found";
                        _result.Status = false;
                        return (_result);
                    }
                }
                else
                {
                    _result.Message = Utility.CurrentLanguageCode == "ar" ? Resources.Response_ar.Error_in_the_drivers_username : Resources.Response.Error_in_the_drivers_username;
                    _result.Status = false;
                    return (_result);
                }
            }
            catch (Exception e)
            {
                _result.Message = Utility.CurrentLanguageCode == "ar" ? Resources.Response_ar.Error + e.ToString() : Resources.Response.Error + e.ToString();
                _result.Status = false;
                return (_result);
            }
        }
        public async Task<ResultMessage> AddOrderPending(AddOrderViewModelApiNew model, int UserId, DateTime? ScheduleDate, DateTime? ScheduleTime, int OrderTypeId)
        {
            try
            {
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
                                model.addressID = model.addressID == 0 ? (_blCustomer.GetCustomerLocationByCustomersID(CustomerId) != null ? _blCustomer.GetCustomerLocationByCustomersID(CustomerId).CustomerLocationID : model.addressID) : model.addressID;
                                if (model.addressID != 0)
                                {
                                    model.scheduleDate = ScheduleDate.ToString();
                                    model.scheduleTime = ScheduleTime.ToString();
                                    model.orderTypeId = OrderTypeId;

                                    var NationalityList = _blCustomer.AddOrder_New_Pending(model, UserId, CustomerId, _configuration["FireBaseKey"], "WEB", "", ScheduleDate, ScheduleTime);
                                    if (NationalityList.message == "true")
                                    {
                                        if (NationalityList.listVendorsID != null)
                                        {
                                            if (NationalityList.listVendorsID.Any())
                                            {
                                                foreach (var vendorsID in NationalityList.listVendorsID)
                                                {
                                                    var vendor = _BlVendor.GetById(vendorsID);
                                                    if (vendor != null)
                                                    {
                                                        var orderVendor = _blOrders.GetOrderVendorByOrderAndVendorAndDelete(NationalityList.orderId, vendorsID);
                                                        var OrderVendorID = orderVendor != null ? orderVendor.OrderVendorID : 0;
                                                        var message = "هلا متجر " + vendor.StoreNameAr + Environment.NewLine + " يوجد لديك طلب اذن بزيادة الكمية على طلب رقم " + OrderVendorID;
                                                        await _OTPService.SendSMSForOrder(vendor.MobileNo, message, (int)UserTypeEnum.Vendor, (int)MessageReasonEnum.Order);
                                                    }
                                                }
                                            }
                                        }

                                        _result.Message = Utility.CurrentLanguageCode == "ar" ? "تم ارسال الطلب بنجاح" : "Order Send Successfully";
                                        _result.Status = true;
                                        _result.ID = NationalityList.orderId;

                                        return (_result);
                                    }
                                    else
                                    {
                                        _result.Message = NationalityList.message;
                                        _result.Status = false;
                                        return (_result);
                                    }
                                }
                                else
                                {
                                    _result.Message = Utility.CurrentLanguageCode == "ar" ? "لابد من اضافه عنوان لكي تستكمل الطلب " : "You must add an address to complete the Request";
                                    _result.Status = false;
                                    _result.ObjResult = "Address";
                                    return (_result);
                                }
                            }
                            else
                            {
                                _result.Message = Utility.CurrentLanguageCode == "ar" ? "فضلا قم بتحديد المنتجات" : "Please choose products";
                                _result.Status = false;
                                return (_result);
                            }
                        }
                        else
                        {
                            _result.Message = Utility.CurrentLanguageCode == "ar" ? "فضلا قم بتحديد المنتجات" : "Please choose products";
                            _result.Status = false;
                            return (_result);
                        }
                    }
                    else
                    {
                        _result.Message = Utility.CurrentLanguageCode == "ar" ? "فضلا قم بتحديد طريقة الدفع" : "Payment Type not found";
                        _result.Status = false;
                        return (_result);
                    }
                }
                else
                {
                    _result.Message = Utility.CurrentLanguageCode == "ar" ? Resources.Response_ar.Error_in_the_drivers_username : Resources.Response.Error_in_the_drivers_username;
                    _result.Status = false;
                    return (_result);
                }
            }
            catch (Exception e)
            {
                _result.Message = Utility.CurrentLanguageCode == "ar" ? Resources.Response_ar.Error + e.ToString() : Resources.Response.Error + e.ToString();
                _result.Status = false;
                return (_result);
            }
        }
        public ResultMessage ChangeOrderPendingToDraft(AddOrderViewModelApiNew model, int OrdersID, int UserId)
        {
            try
            {
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

                                    var NationalityList = _blCustomer.ChangeOrder_New_Pending_To_Create(model, OrdersID, UserId, CustomerData.CustomersID, (int)OrderStatusEnum.Drafts);
                                    if (NationalityList.message == "true")
                                    {
                                        _result.Message = Utility.CurrentLanguageCode == "ar" ? "تم ارسال الطلب بنجاح" : "Order Send Successfully";
                                        _result.Status = true;
                                        _result.ID = NationalityList.orderId;

                                        return (_result);
                                    }
                                    else
                                    {
                                        _result.Message = NationalityList.message;
                                        _result.Status = false;
                                        return (_result);
                                    }
                                }
                                else
                                {
                                    _result.Message = Utility.CurrentLanguageCode == "ar" ? "فضلا قم بتحديد العنوان" : "Please choose address";
                                    _result.Status = false;
                                    return (_result);
                                }
                            }
                            else
                            {
                                _result.Message = Utility.CurrentLanguageCode == "ar" ? "فضلا قم بتحديد المنتجات" : "Please choose products";
                                _result.Status = false;
                                return (_result);
                            }
                        }
                        else
                        {
                            _result.Message = Utility.CurrentLanguageCode == "ar" ? "فضلا قم بتحديد المنتجات" : "Please choose products";
                            _result.Status = false;
                            return (_result);
                        }
                    }
                    else
                    {
                        _result.Message = Utility.CurrentLanguageCode == "ar" ? "فضلا قم بتحديد طريقة الدفع" : "Payment Type not found";
                        _result.Status = false;
                        return (_result);
                    }
                }
                else
                {
                    _result.Message = Utility.CurrentLanguageCode == "ar" ? Resources.Response_ar.Error_in_the_drivers_username : Resources.Response.Error_in_the_drivers_username;
                    _result.Status = false;
                    return (_result);
                }
            }
            catch (Exception e)
            {
                _result.Message = Utility.CurrentLanguageCode == "ar" ? Resources.Response_ar.Error + e.ToString() : Resources.Response.Error + e.ToString();
                _result.Status = false;
                return (_result);
            }
        }
        public ResultMessage ChangeOrderPendingToCreate(AddOrderViewModelApiNew model, int OrderID, int UserId)
        {
            try
            {
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
                                    var NationalityList = _blCustomer.ChangeOrder_New_Pending_To_Create(model, OrderID, UserId, CustomerId, (int)OrderStatusEnum.Create);
                                    if (NationalityList.message == "true")
                                    {
                                        _result.Message = Utility.CurrentLanguageCode == "ar" ? "تم ارسال الطلب بنجاح" : "Order Send Successfully";
                                        _result.Status = true;
                                        _result.ID = NationalityList.orderId;
                                        return (_result);
                                    }
                                    else
                                    {
                                        _result.Message = NationalityList.message;
                                        _result.Status = false;
                                        return (_result);
                                    }
                                }
                                else
                                {
                                    _result.Message = Utility.CurrentLanguageCode == "ar" ? "فضلا قم بتحديد العنوان" : "Please choose address";
                                    _result.Status = false;
                                    return (_result);
                                }
                            }
                            else
                            {
                                _result.Message = Utility.CurrentLanguageCode == "ar" ? "فضلا قم بتحديد المنتجات" : "Please choose products";
                                _result.Status = false;
                                return (_result);
                            }
                        }
                        else
                        {
                            _result.Message = Utility.CurrentLanguageCode == "ar" ? "فضلا قم بتحديد المنتجات" : "Please choose products";
                            _result.Status = false;
                            return (_result);
                        }
                    }
                    else
                    {
                        _result.Message = Utility.CurrentLanguageCode == "ar" ? "فضلا قم بتحديد طريقة الدفع" : "Payment Type not found";
                        _result.Status = false;
                        return (_result);
                    }
                }
                else
                {
                    _result.Message = Utility.CurrentLanguageCode == "ar" ? Resources.Response_ar.Error_in_the_drivers_username : Resources.Response.Error_in_the_drivers_username;
                    _result.Status = false;
                    return (_result);
                }
            }
            catch (Exception e)
            {
                _result.Message = Utility.CurrentLanguageCode == "ar" ? Resources.Response_ar.Error + e.ToString() : Resources.Response.Error + e.ToString();
                _result.Status = false;
                return (_result);
            }
        }
        public async Task<string> GeneratePaymentInvoice_Draft(int orderid, string _sessionID)
        {
            var userdata = _blCustomer.GetCustomerByUser(User.GetUserIdInt());

            var total = GetOrderTotals_Drafts(orderid);

            if (total > 0)
            {
                var paymentUrl = await _BLTransaction.SendPayMyfatoraLink_Drafts(orderid, _sessionID, "", Utility.CurrentLanguageCode, _configuration["PaymyFatoraSecertToken"], _configuration["PaymyFatoraUrl"], userdata.CustomersID, _configuration["PaymyFatoraRedirectUrl"], total);


                return paymentUrl;
            }
            else
            {
                ResultMessage data = executePaymentWallet(_sessionID.ToString());
                return (_configuration["TabRedirect"]);
            }
        }
        public async Task<string> GeneratePaymentInvoice_DraftFromPending(int orderid, string _sessionID)
        {
            var userdata = _blCustomer.GetCustomerByUser(User.GetUserIdInt());

            var total = GetOrderTotals_Drafts(orderid);

            if (total > 0)
            {
                var paymentUrl = await _BLTransaction.SendPayMyfatoraLink_Drafts(orderid, _sessionID, "", Utility.CurrentLanguageCode, _configuration["PaymyFatoraSecertToken"], _configuration["PaymyFatoraUrl"], userdata.CustomersID, _configuration["PaymyFatoraRedirectUrl"], total);


                return paymentUrl;
            }
            else
            {
                ResultMessage data = executePaymentWalletFromPending(orderid);
                return (_configuration["TabRedirect"]);
            }
        }
        public decimal GetOrderTotals_Drafts(int orderId)
        {
            var masterdata = _blOrders.getOrderData(orderId);
            return masterdata.Total;
        }
        public IActionResult Checking_PayingReditect(string paymentId, string Id)
        {
            var json = JsonConvert.SerializeObject(CheckIsPaid_invoice_Draft(paymentId).Value);
            StatusDesc dataID = JsonConvert.DeserializeObject<StatusDesc>(json);
            TransactionCard tripChargeLog = _BLTransaction.GetTransactionCard(paymentId, dataID.PaymentStatus.Data.InvoiceId.ToString(), dataID.PaymentStatus.Data.CustomerReference.ToString());
            var CustomerById = _blCustomer.GetCustomerById(tripChargeLog.CustomersID);
            // "Pending" Failed
            ViewBag.UserName = CustomerById.UserId;
            ViewBag.paymentId = paymentId;
            return View();
        }
        public JsonResult Checking_PayingReditectPaidStatus(string _paymentId)
        {
            string status = "";
            var json = JsonConvert.SerializeObject(CheckIsPaid_invoice_Draft(_paymentId).Value);
            StatusDesc dataID = JsonConvert.DeserializeObject<StatusDesc>(json);
            if (dataID != null)
            {
                if (dataID.PaymentStatus.Data.InvoiceStatus.ToLower() == "paid")
                {
                    #region  Order paid
                    _BLSite.RemoveCart(_BLSite.GeCartMasterBySessionID(dataID.PaymentStatus.Data.UserDefinedField).CartMasterID);
                    status = "OK";
                    #endregion
                }
                else
                {
                    status += dataID.PaymentStatus.Data.InvoiceTransactions?.FirstOrDefault()?.Error?.ToString();
                    status += "<br />";
                    status += dataID.PaymentStatus.Data.InvoiceTransactions?.FirstOrDefault()?.Error?.ToString() != dataID.PaymentStatus.Data.InvoiceTransactions?.LastOrDefault()?.Error?.ToString() ? dataID.PaymentStatus.Data.InvoiceTransactions?.LastOrDefault()?.Error?.ToString() : "";
                }
            }
            else
            {
                status = "ERROR : Transaction Not Found in Bank Provider";
            }
            return Json(status);
        }
        /// <summary>
        /// paying order With Online Transfer
        /// </summary>
        /// <param name="_paymentId"></param>
        /// <param name="_UserName"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Checking_PayingReditectStatus(string _paymentId)
        {
            string status = "";
            var json = JsonConvert.SerializeObject(CheckIsPaid_invoice_Draft(_paymentId).Value);
            StatusDesc dataID = JsonConvert.DeserializeObject<StatusDesc>(json);
            if (dataID != null)
            {
                TransactionCard tripChargeLog = _BLTransaction.GetTransactionCard(_paymentId, dataID.PaymentStatus.Data.InvoiceId.ToString(), dataID.PaymentStatus.Data.CustomerReference.ToString());
                if (tripChargeLog != null)
                {
                    var OrderId = dataID.Orderid;
                    if (_blOrders.getOrderData(OrderId).OrderStatusID != (int)OrderStatusEnum.Drafts)
                    {
                        status = "OK";
                    }
                    else
                    {
                        #region Confirm pay
                        if (dataID.PaymentStatus.Data.InvoiceStatus.ToLower() == "paid" && tripChargeLog.Status.ToLower() != dataID.PaymentStatus.Data.InvoiceStatus.ToLower())
                        {
                            #region Log
                            tripChargeLog.PaymentID = _paymentId;
                            tripChargeLog.InvoiceId = dataID.PaymentStatus.Data.InvoiceId.ToString();
                            tripChargeLog.IsRedirect = true;
                            tripChargeLog.UpdateDate = _BLGeneral.GetDateTimeNow();
                            tripChargeLog.Status = dataID.PaymentStatus.Data.InvoiceStatus;
                            tripChargeLog.TransactionStatus = (int)TransactionEnum.BeginRedirect;
                            tripChargeLog.TransactionCardLog.Add(new TransactionCardLog()
                            {
                                OrdersID = dataID.Orderid,
                                DateAdded = _BLGeneral.GetDateTimeNow(),
                                LastStatusUpdateFrom = (int)LastStatusUpdateFromEnum.Web,
                                Message = "ConfirmPaymyfatoorah Success And Begin To redirect .. ",
                                PaymentID = _paymentId,
                                InvoiceId = dataID.PaymentStatus.Data.InvoiceId.ToString(),
                                Status = dataID.PaymentStatus.Data.InvoiceStatus.ToString(),
                                TransactionStatus = (int)TransactionEnum.BeginRedirect,
                            });
                            tripChargeLog = _BLTransaction.UpdateTransactionCard(tripChargeLog);

                            #endregion
                            #region add Order 
                            // Confirm Order
                            _blOrders.UpdateOrderVendorById(OrderId, _configuration["FireBaseKey"], "");

                            status = "OK";

                            #endregion
                        }
                        else
                        {
                            if (dataID.PaymentStatus.Data.InvoiceStatus.ToLower() == "paid")
                            {
                                status = "OK";
                            }
                            else
                            {
                                status += dataID.PaymentStatus.Data.InvoiceTransactions?.FirstOrDefault()?.Error?.ToString();
                                status += dataID.PaymentStatus.Data.InvoiceTransactions?.LastOrDefault()?.Error?.ToString();
                            }
                        }
                        #endregion
                    }
                }
                else
                {
                    status = "ERROR : Transaction Not Found Our System";
                }
            }
            else
            {
                status = "ERROR : Transaction Not Found in Bank Provider";
            }
            return Json(status);
        }
        #endregion
        #region BarCode
        [HttpPost]
        public Byte[] GenerateBarCode(string qrText)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrText, QRCodeGenerator.ECCLevel.Q);

            ////QRCode qrCode = new QRCode(qrCodeData);
            ////Bitmap qrCodeImage = qrCode.GetGraphic(20);
            Byte[] byteData = null; //BitmapToBytes(qrCodeImage);
            return byteData;
        }
        private static Byte[] BitmapToBytes(Bitmap img)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }
        #endregion
        #region New_Map
        [HttpPost]
        public JsonResult GetLocationData(string lat, string lng)
        {
            var obj = new GoogleGeoCodeResp().GetReciverData(lat, lng, _configuration["GoogleApiKey"]);
            if (obj != null)
            {
                if (!string.IsNullOrWhiteSpace(obj.countryName))
                {
                    var Result = _blCustomer.GetLocationbyMap(obj.CityName, obj.areaName, obj.Blok_place_id, obj.Blok_LocationType, Utility.CurrentLanguageCode, User.GetUserIdInt());
                    if (!string.IsNullOrWhiteSpace(obj.areaName))
                    {
                        if (Result != null)
                        {
                            Result.Address = obj.Blok_place_id;
                            return Json(new
                            {
                                Status = true,
                                Message = "",
                                Data = Result
                            });
                        }
                        else
                        {
                            return Json(new
                            {
                                Status = false,
                                Message = Utility.CurrentLanguageCode == "ar" ? "عفوا , المنطقة المحددة خارج تغطية التوصيل" :
                                "Sorry, this location out of our range"
                            });
                        }
                    }
                    else
                    {
                        return Json(new
                        {
                            Status = false,
                            Message = Utility.CurrentLanguageCode == "ar" ? "عفوا , المنطقة المحددة خارج تغطية التوصيل" :
                             "Sorry, this location out of our range"
                        });
                    }
                }
                else
                {
                    return Json(new
                    {
                        Status = false,
                        Message = Utility.CurrentLanguageCode == "ar" ? "عفوا , لايمكن مشاركة هذا الموقع لانه خارج المملكة العربية السعودية" :
                        "Sorry, this site cannot be shared because it is outside the Kingdom of Saudi Arabia"
                    });

                }
            }
            else
            {
                return Json(new
                {
                    Status = false,
                    Message = Utility.CurrentLanguageCode == "ar" ? "عفوا , لايمكن مشاركة هذا الموقع لانه خارج المملكة العربية السعودية" :
                    "Sorry, this site cannot be shared because it is outside the Kingdom of Saudi Arabia"
                });

            }
        }
        #endregion
        #region Pay_Tab
        /// step 3
        public string PayNow(int id)
        {
            if (id != 0)
            {
                // call paytap To Get Pay Link
                var urlToPaid = GetPayTapLink(id);
                Uri uriResult;
                bool result = Uri.TryCreate(urlToPaid, UriKind.Absolute, out uriResult)
                    && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
                return urlToPaid;
            }
            else
            {
                return "";
            }
        }
        /// step 4
        public async Task<object> ConfirmPayTap(string tap_id)
        {
            var tripChargeLog = _blOrders.GetTripChargeLog(tap_id);
            if (tripChargeLog != null)
            {
                if (!tripChargeLog.IsRedirect)
                {
                    #region Confirm pay

                    var json = JsonConvert.SerializeObject(CheckIsPaid(tap_id).Value);
                    var dataID = JsonConvert.DeserializeObject<StatusDescription>(json);

                    if (string.IsNullOrWhiteSpace(dataID.statusdescription) || dataID.statusdescription.ToLower() == "captured")
                    {
                        var OrderId = dataID.Orderid;
                        tripChargeLog.IsRedirect = true;
                        tripChargeLog.RedirectDate = _BLGeneral.GetDateTimeNow();
                        tripChargeLog.Status = dataID.ResponsPayTap.status;

                        _blOrders.UpdateTripChargeLogWithoutCommit(tripChargeLog);
                        var IsSave = _blOrders.UpdateOrderVendorById(OrderId, _configuration["FireBaseKey"], dataID.CardType);
                        if (IsSave)
                        {
                            var cookieLingua = "ar";
                            var message = "";

                            if (HttpContext.Request.Cookies["Language"] != null) { cookieLingua = HttpContext.Request.Cookies["Language"]; }

                            if (cookieLingua == "ar")
                            {
                                message += "*تم دفع الطلب بنجاح* ";
                                message += Environment.NewLine;
                                message += Environment.NewLine;
                                message += "تمت عمليه الدفع على طلبكم رقم (" + tripChargeLog.OrdersID + ")";
                                message += Environment.NewLine;
                                message += "بوابة الدفع : *تحويل بنكي*";
                                message += Environment.NewLine;
                                message += Environment.NewLine;
                                message += "هوم ميد";
                            }
                            else
                            {
                                message += "*Order has been successfully paid* ";
                                message += Environment.NewLine;
                                message += Environment.NewLine;
                                message += "Payment has been processed on your order number (" + tripChargeLog.OrdersID + ")";
                                message += Environment.NewLine;
                                message += "Payment gateway: *Bank transfer*";
                                message += Environment.NewLine;
                                message += Environment.NewLine;
                                message += "Home Made";
                            }
                            if (Request.Cookies["sessionToken"] != null)
                            {
                                Response.Cookies.Delete("sessionToken");
                            }
                            if (Request.Cookies["sessionID"] != null)
                            {
                                Response.Cookies.Delete("sessionID");
                            }
                            // Send To User
                            //_BLGeneral.SendEmail("loaymamdouh56@hotmail.com", "Paid Success", message);
                            return Redirect(_configuration["TabRedirect"]);
                        }
                        else
                        {
                            var message = "*Urgent Issues*";
                            message += Environment.NewLine;
                            message += Environment.NewLine;
                            message += Environment.NewLine;
                            message += "Paying Urgent Error";
                            message += Environment.NewLine;
                            message += "Order No " + dataID.Orderid + " Is " + dataID.statusdescription + " From Tap Payment But not updated in system";
                            message += Environment.NewLine;
                            message += IsSave;
                            // send urgent Mail To Admin 
                            //_BLGeneral.SendEmail("a.elaf@hotmail.com", "Urgent Paying Issues", message);
                            //_BLGeneral.SendEmail("loaymamdouh56@hotmail.com", "Urgent Paying Issues", message);
                            // insert into Ex
                            _blOrders.InsertTripChargeExLog(new TabChargeExLog
                            {
                                RequestDate = tripChargeLog.RequestDate,
                                ResponseRedirect = json,
                                Status = dataID.ResponsPayTap.status,
                                OrdersID = dataID.Orderid,
                                ErrorFrom = (int)ErrorFromTypeEnum.OurSystem,
                                TabChargeEnum = (int)TabChargeEnum.Order
                            });

                            if (dataID.statusdescription.ToLower() == "captured")
                            {
                                var cookieLingua = "ar";
                                var messageRes = "";

                                if (HttpContext.Request.Cookies["Language"] != null) { cookieLingua = HttpContext.Request.Cookies["Language"]; }

                                if (cookieLingua == "ar")
                                {
                                    messageRes += "*تم دفع الطلب بنجاح* ";
                                    messageRes += Environment.NewLine;
                                    messageRes += Environment.NewLine;
                                    messageRes += "تمت عمليه الدفع على طلبكم رقم (" + tripChargeLog.OrdersID + ")";
                                    messageRes += Environment.NewLine;
                                    messageRes += "بوابة الدفع : *تحويل بنكي*";
                                    messageRes += Environment.NewLine;
                                    messageRes += Environment.NewLine;
                                    messageRes += "هوم ميد";
                                }
                                else
                                {
                                    messageRes += "*Order has been successfully paid* ";
                                    messageRes += Environment.NewLine;
                                    messageRes += Environment.NewLine;
                                    messageRes += "Payment has been processed on your order number (" + tripChargeLog.OrdersID + ")";
                                    messageRes += Environment.NewLine;
                                    messageRes += "Payment gateway: *Bank transfer*";
                                    messageRes += Environment.NewLine;
                                    messageRes += Environment.NewLine;
                                    messageRes += "Home Made";
                                }

                                // Send To User
                                //_BLGeneral.SendEmail("loaymamdouh56@hotmail.com", "Urgent Paying Issues", messageRes);
                                return Redirect(_configuration["TabRedirect"]);
                            }
                            else
                            {
                                return Redirect("/Site/Home/PaymentError?http_code=404&code=404&description=" + dataID.statusdescription + "");
                            }
                        }
                    }
                    else
                    {
                        return Redirect("/Site/Home/PaymentError?http_code=404&code=404&description=" + dataID.statusdescription + "");
                    }
                    #endregion
                }
                else
                {
                    if (Request.Cookies["sessionToken"] != null)
                    {
                        Response.Cookies.Delete("sessionToken");
                    }
                    if (Request.Cookies["sessionID"] != null)
                    {
                        Response.Cookies.Delete("sessionID");
                    }
                    return Redirect(_configuration["TabRedirect"]);
                }
            }
            else
            {
                return Redirect("/Site/home/PaymentError?http_code=404&code=404&description=لا توجد بيانات عن عمليه الدفع المحدده");
            }
        }
        /// step 5
        #region Other Action View
        public IActionResult PaymentError(string http_code, string code, string description)
        {
            ViewBag.http_code = http_code;
            ViewBag.code = code;
            ViewBag.description = description;
            return View();
        }
        #endregion
        #region TapPayment Actions
        public string GetPayTapLink(int id)
        {
            try
            {
                var tripChargeLog = new TabCharge();
                var cookieLingua = "ar";
                var RequestDesc = "";
                var statement_desc = "";
                if (HttpContext.Request.Cookies["Language"] != null) { cookieLingua = HttpContext.Request.Cookies["Language"]; }
                var Data = _blOrders.OrderDetails(id);
                var needToPay = GetOrderTotals_Drafts(id);
                string ProjectUrl = _configuration["ProjectUrl"];
                var RequestPayTapData = new RequestPayTap()
                {
                    amount = (double)needToPay,
                    currency = "sar",
                    threeDSecure = true,
                    save_card = false,
                    description = RequestDesc,
                    statement_descriptor = statement_desc,
                    metadata = new metadataClass { udf1 = "Order Id : " + Data.OrdersID + "", udf2 = "" + Data.OrdersID + "" },
                    reference = new referenceClass { order = string.Concat("txn_", Data.OrdersID), transaction = string.Concat("ord_", Data.OrdersID) },
                    receipt = new receiptClass { email = false, sms = true },
                    customer = new customerClass { email = "", first_name = Data.Customers.FirstName, middle_name = " ", last_name = Data.Customers.SeconedName, phone = new phoneClass { country_code = "966", number = Data.Customers.MobileNo } },
                    merchant = new merchantClass { id = "" },
                    source = new sourceClass { id = "src_all" },
                    post = new postClass { url = string.Concat(ProjectUrl, "/Site/Home/ConfirmPayTapPost") },
                    redirect = new redirectClass { url = string.Concat(ProjectUrl, "/Site/Home/ConfirmPayTap") },
                    language = "ar"

                };
                var jsonRequestPayTapData = System.Text.Json.JsonSerializer.Serialize(RequestPayTapData);

                //insert request
                tripChargeLog.RequestCreate = jsonRequestPayTapData;
                tripChargeLog.RequestDate = _BLGeneral.GetDateTimeNow();
                tripChargeLog.OrdersID = Data.OrdersID;
                tripChargeLog.TabChargeEnum = (int)TabChargeEnum.Order;
                string SecertToken = _configuration["PayTapSecertToken"];
                var client = new RestClient("https://api.tap.company/v2/charges");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Authorization", "Bearer " + SecertToken);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", jsonRequestPayTapData, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                //Console.WriteLine(response.Content);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    ResponcePayTap deserializedProduct = JsonConvert.DeserializeObject<ResponcePayTap>(response.Content);
                    //insert responce
                    tripChargeLog.ResponseCreate = response.Content;
                    tripChargeLog.ResponceDate = _BLGeneral.GetDateTimeNow();
                    tripChargeLog.TapChargeID = deserializedProduct.id;
                    tripChargeLog.Status = deserializedProduct.status;
                    tripChargeLog.StatusId = (int)InsertIntoLogTypeEnum.request;
                    _blOrders.InsertTripCharge(tripChargeLog/*, (int)InsertIntoLogTypeEnum.request*/);
                    return deserializedProduct.transaction.url;
                }
                else
                {
                    ResponceErrorPayTap deserializedProduct = JsonConvert.DeserializeObject<ResponceErrorPayTap>(response.Content);
                    deserializedProduct.tripid = Data.OrdersID.ToString();
                    tripChargeLog.ResponseCreate = response.Content;
                    tripChargeLog.ResponceDate = _BLGeneral.GetDateTimeNow();
                    tripChargeLog.Status = deserializedProduct.http_code;
                    tripChargeLog.StatusId = (int)InsertIntoLogTypeEnum.Errorresponce;
                    _blOrders.InsertTripCharge(tripChargeLog);
                    ViewBag.http_code = deserializedProduct.http_code;
                    ViewBag.code = deserializedProduct.errors[0].code;
                    ViewBag.description = deserializedProduct.errors[0].description;
                    return "/Site/home/PaymentError?http_code=" + deserializedProduct.http_code + "&code=" + deserializedProduct.errors[0].code + "&description=" + deserializedProduct.errors[0].description + "";
                }
            }
            catch (Exception ex)
            {
                return ex.Message + (ex.InnerException != null ? ex.InnerException.Message : "");
            }
        }
        public JsonResult CheckIsPaid(string tap_id)
        {
            string SecertToken = _configuration["PayTapSecertToken"];
            var client = new RestClient("https://api.tap.company/v2/charges/" + tap_id + "");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer " + SecertToken + "");
            IRestResponse response = client.Execute(request);
            var StatusCode = response.StatusCode;
            ResponcePayTap deserializedProduct = JsonConvert.DeserializeObject<ResponcePayTap>(response.Content);
            var responseCode = deserializedProduct.response.message;
            var responseCodeInt = deserializedProduct.response.code;

            if (responseCode != (string)PaymentResponseCodesEnum.Captured.ToString())
            {
                string description = ((PaymentResponseCodesEnum)int.Parse(responseCodeInt)).AsString(EnumFormat.Description);
                return Json(new { Orderid = int.Parse(deserializedProduct.metadata.udf2), statusdescription = description, ResponsPayTap = deserializedProduct, CardType = deserializedProduct.card.brand });
            }
            return Json(new { Orderid = int.Parse(deserializedProduct.metadata.udf2), statusdescription = responseCode, ResponsPayTap = deserializedProduct, Card = deserializedProduct.card });
        }
        public async Task<JsonResult> ConfirmPayTapPost(string tap_id)
        {
            var tripChargeLog = _blOrders.GetTripChargeLog(tap_id);
            if (tripChargeLog.Status.ToLower() != "captured")
            {
                if (!tripChargeLog.IsRedirect)
                {
                    var json = JsonConvert.SerializeObject(CheckIsPaid(tap_id).Value);
                    StatusDescription dataID = JsonConvert.DeserializeObject<StatusDescription>(json);
                    if (string.IsNullOrWhiteSpace(dataID.statusdescription) || dataID.statusdescription.ToLower() == "captured")
                    {
                        var OrderId = dataID.Orderid;
                        tripChargeLog.IsRedirect = true;
                        tripChargeLog.RedirectDate = _BLGeneral.GetDateTimeNow();
                        tripChargeLog.Status = dataID.ResponsPayTap.status;
                        _blOrders.UpdateTripChargeLogWithoutCommit(tripChargeLog);
                        var IsSave = _blOrders.UpdateOrderVendorById(OrderId, _configuration["FireBaseKey"], dataID.CardType);
                        if (IsSave)
                        {
                            var cookieLingua = "ar";
                            var message = "";
                            return Json("Ok");
                        }
                        else
                        {
                            return Json("Ok");
                        }
                    }
                    else
                    {
                        return Json("Ok");
                    }
                }
            }
            return Json("OK");
        }
        #endregion
        #endregion
        #region Charge_Wallet
        public string GetPayTapLink_Wallet(decimal Amount)
        {
            try
            {
                var tripChargeLog = new TabCharge();
                var cookieLingua = "ar";
                var RequestDesc = "";
                var statement_desc = "";

                if (HttpContext.Request.Cookies["Language"] != null) { cookieLingua = HttpContext.Request.Cookies["Language"]; }

                var Data = _blCustomer.GetPersonalDataViewModelByUser(User.GetUserIdInt());
                var needToPay = Amount; //For_Prod
                string ProjectUrl = _configuration["ProjectUrl"];
                var RequestPayTapData = new RequestPayTap()
                {
                    amount = (double)needToPay,
                    currency = "sar",
                    threeDSecure = true,
                    save_card = false,
                    description = RequestDesc,
                    statement_descriptor = statement_desc,
                    metadata = new metadataClass { udf1 = "Order Id : " + Data.CustomersID + "", udf2 = "" + Data.CustomersID + "" },
                    reference = new referenceClass { order = string.Concat("txn_", Data.CustomersID), transaction = string.Concat("ord_", Data.CustomersID) },
                    receipt = new receiptClass { email = false, sms = true },
                    customer = new customerClass { email = "", first_name = Data.Name, middle_name = " ", last_name = Data.LastName, phone = new phoneClass { country_code = "966", number = Data.MobileNo } },
                    merchant = new merchantClass { id = "" },
                    source = new sourceClass { id = "src_all" },
                    post = new postClass { url = string.Concat(ProjectUrl, "/Site/Home/ConfirmBalanceTapPost") },
                    redirect = new redirectClass { url = string.Concat(ProjectUrl, "/Site/Home/ConfirmBalanceTap") },
                    language = "ar"

                };
                var jsonRequestPayTapData = System.Text.Json.JsonSerializer.Serialize(RequestPayTapData);

                //insert request
                tripChargeLog.RequestCreate = jsonRequestPayTapData;
                tripChargeLog.RequestDate = _BLGeneral.GetDateTimeNow();
                tripChargeLog.CustomersID = Data.CustomersID;
                tripChargeLog.TabChargeEnum = (int)TabChargeEnum.Customer_Balance;

                string SecertToken = _configuration["PayTapSecertToken"];
                var client = new RestClient("https://api.tap.company/v2/charges");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Authorization", "Bearer " + SecertToken);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", jsonRequestPayTapData, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    ResponcePayTap deserializedProduct = JsonConvert.DeserializeObject<ResponcePayTap>(response.Content);
                    //insert responce
                    tripChargeLog.ResponseCreate = response.Content;
                    tripChargeLog.ResponceDate = _BLGeneral.GetDateTimeNow();
                    tripChargeLog.TapChargeID = deserializedProduct.id;
                    tripChargeLog.Status = deserializedProduct.status;
                    tripChargeLog.StatusId = (int)InsertIntoLogTypeEnum.request;
                    _blOrders.InsertTripCharge(tripChargeLog);
                    return deserializedProduct.transaction.url;
                }
                else
                {
                    ResponceErrorPayTap deserializedProduct = JsonConvert.DeserializeObject<ResponceErrorPayTap>(response.Content);
                    deserializedProduct.tripid = Data.CustomersID.ToString();
                    tripChargeLog.ResponseCreate = response.Content;
                    tripChargeLog.ResponceDate = _BLGeneral.GetDateTimeNow();
                    tripChargeLog.Status = deserializedProduct.http_code;
                    tripChargeLog.StatusId = (int)InsertIntoLogTypeEnum.Errorresponce;
                    //insert responce
                    _blOrders.InsertTripCharge(tripChargeLog);
                    ViewBag.http_code = deserializedProduct.http_code;
                    ViewBag.code = deserializedProduct.errors[0].code;
                    ViewBag.description = deserializedProduct.errors[0].description;
                    return "/Site/home/PaymentError?http_code=" + deserializedProduct.http_code + "&code=" + deserializedProduct.errors[0].code + "&description=" + deserializedProduct.errors[0].description + "";
                }
            }
            catch (Exception ex)
            {
                return ex.Message + (ex.InnerException != null ? ex.InnerException.Message : "");
            }
        }
        [HttpPost]
        public JsonResult ConfirmCharge(decimal amount)
        {
            var model = _blCustomer.GetPersonalDataViewModelByUser(User.GetUserIdInt());
            if (model != null)
            {
                if (amount > 0)
                {
                    var urlToPaid = GetPayTapLink_Wallet(amount);
                    Uri uriResult;
                    bool result = Uri.TryCreate(urlToPaid, UriKind.Absolute, out uriResult)
                        && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
                    return Json(new { Status = true, Message = urlToPaid });
                }
                else
                {
                    return Json(new { Status = false, Message = "يجب داخال المبلغ المطلوب شحنه" });
                }
            }
            else
            {
                return Json(new { Status = false, Message = "فضلا قم بتسجيل الدخول مره اخري" });
            }
        }
        public async Task<JsonResult> ConfirmBalanceTapPost(string tap_id)
        {
            var tripChargeLog = _blOrders.GetTripChargeLogCustomer(tap_id);
            if (tripChargeLog.Status.ToLower() != "captured")
            {
                if (!tripChargeLog.IsRedirect)
                {
                    var json = JsonConvert.SerializeObject(CheckIsPaid(tap_id).Value);
                    StatusDescription dataID = JsonConvert.DeserializeObject<StatusDescription>(json);
                    if (string.IsNullOrWhiteSpace(dataID.statusdescription) || dataID.statusdescription.ToLower() == "captured")
                    {
                        var OrderId = dataID.Orderid;
                        var IsSave = _blCustomerBalance.InsertBalance((int)TypeOperationEnum.CR, (decimal)dataID.ResponsPayTap.amount, (int)TransactionTypeEnum.Charge_Wallet, OrderId, tripChargeLog.Customers.UserId, null, _BLGeneral.GetDateTimeNow(), "", "");
                        tripChargeLog.IsRedirect = true;
                        tripChargeLog.RedirectDate = _BLGeneral.GetDateTimeNow();
                        tripChargeLog.Status = dataID.ResponsPayTap.status;
                        tripChargeLog.CustomerBalanceID = IsSave.CustomerBlanceID;
                        var IsS = _blOrders.UpdateTripChargeLog(tripChargeLog);
                        if (IsS)
                        {
                            return Json("Ok");
                        }
                        else
                        {
                            return Json("Ok");
                        }
                    }
                    else
                    {
                        return Json("Ok");
                    }
                }
            }
            return Json("OK");
        }
        public async Task<object> ConfirmBalanceTap(string tap_id)
        {
            var tripChargeLog = _blOrders.GetTripChargeLogCustomer(tap_id);
            if (tripChargeLog != null)
            {
                if (!tripChargeLog.IsRedirect)
                {
                    #region Confirm pay

                    var json = JsonConvert.SerializeObject(CheckIsPaid(tap_id).Value);
                    var dataID = JsonConvert.DeserializeObject<StatusDescription>(json);

                    if (string.IsNullOrWhiteSpace(dataID.statusdescription) || dataID.statusdescription.ToLower() == "captured")
                    {
                        var OrderId = dataID.Orderid;
                        var IsSave = _blCustomerBalance.InsertBalance((int)TypeOperationEnum.CR, (decimal)dataID.ResponsPayTap.amount, (int)TransactionTypeEnum.Charge_Wallet, OrderId, tripChargeLog.Customers.UserId, null, _BLGeneral.GetDateTimeNow(), "", "");
                        tripChargeLog.IsRedirect = true;
                        tripChargeLog.RedirectDate = _BLGeneral.GetDateTimeNow();
                        tripChargeLog.Status = dataID.ResponsPayTap.status;
                        tripChargeLog.CustomerBalanceID = IsSave.CustomerBlanceID;
                        var IsS = _blOrders.UpdateTripChargeLog(tripChargeLog);
                        if (IsS)
                        {
                            return Redirect("/Site/Home/OrderSuccessWallet");
                        }
                        else
                        {
                            _blOrders.InsertTripChargeExLog(new TabChargeExLog
                            {
                                RequestDate = tripChargeLog.RequestDate,
                                ResponseRedirect = json,
                                Status = dataID.ResponsPayTap.status,
                                CustomersID = dataID.Orderid,
                                ErrorFrom = (int)ErrorFromTypeEnum.OurSystem,
                                TabChargeEnum = (int)TabChargeEnum.Customer_Balance
                            });
                            if (dataID.statusdescription.ToLower() == "captured")
                            {
                                return Redirect("/Site/Home/OrderSuccessWallet");
                            }
                            else
                            {
                                return Redirect("/Site/Home/PaymentError?http_code=404&code=404&description=" + dataID.statusdescription + "");
                            }
                        }
                    }
                    else
                    {
                        return Redirect("/Site/Home/PaymentError?http_code=404&code=404&description=" + dataID.statusdescription + "");
                    }
                    #endregion
                }
                else
                {
                    return Redirect("/Site/Home/OrderSuccessWallet");
                }
            }
            else
            {
                return Redirect("/Site/home/PaymentError?http_code=404&code=404&description=لا توجد بيانات عن عمليه الدفع المحدده");
            }
        }
        #endregion
    }
}
