using Homemade.BLL;
using Homemade.BLL.Driver;
using Homemade.BLL.General;
using Homemade.BLL.Order;
using Homemade.BLL.Setting;
using Homemade.BLL.SMS;
using Homemade.BLL.Vendor;
using Homemade.BLL.ViewModel.Order;
using Homemade.UI.Attributes;
using Homemade.UI.InfraStructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homemade.UI.Areas.Order.Controllers
{
    public class OrdersController : Controller
    {
        #region Declarations
        private readonly ResultMessage result;
        private readonly BlOrders _blOrders;
        private readonly IConfiguration _configuration;
        private readonly BLPermission _bLPermission;
        private readonly BLUser bLUser;
        private readonly BlCity _BlCity;
        private readonly BlVendor _blVendor;
        private readonly BlDriver _blDriver;
        private readonly OTPService _OTPService;
        private readonly BlOrderNotesAdmin _blOrderNotesAdmin;
        public OrdersController(BlCity BlCity, BlOrders blOrders, ResultMessage result, IConfiguration configuration, BLPermission bLPermission, BLUser bLUser, BlVendor blVendor, BlDriver blDriver, OTPService oTPService, BlOrderNotesAdmin blOrderNotesAdmin)
        {
            _blOrders = blOrders;
            this.result = result;
            _configuration = configuration;
            _bLPermission = bLPermission;
            this.bLUser = bLUser;
            _blVendor = blVendor;
            _BlCity = BlCity;
            _blDriver = blDriver;
            _OTPService = oTPService;
            _blOrderNotesAdmin = blOrderNotesAdmin;
        }
        #endregion
        #region View
        [CustomeAuthoriz((int)ControllerEnum.OperationOrders, (int)ActionEnum.View)]
        public IActionResult Index()
        {
            if (GetPermissionUser((int)ActionEnum.View))
            {
                return View();
            }
            else
            {
                return Redirect("/AccessDenied/Unauthorized");
            }
        }
        [CustomeAuthoriz((int)ControllerEnum.VendorOrders, (int)ActionEnum.View)]
        public IActionResult IndexVendor()
        {
            if (GetPermissionUser((int)ActionEnum.View))
            {
                return View();
            }
            else
            {
                return Redirect("/AccessDenied/Unauthorized");
            }
        }
        [CustomeAuthoriz((int)ControllerEnum.OperationOrders, (int)ActionEnum.View)]
        public IActionResult Details(Guid? orderVendorGuid)
        {
            if (GetPermissionUser((int)ActionEnum.View))
            {
                if (orderVendorGuid.HasValue)
                {
                    OrdersVendorViewModel OrderVendorViewModel = _blOrders.GetOrderVendorByGuid(orderVendorGuid.Value, InfraStructure.Utility.CurrentLanguageCode, _configuration["ProductImageView"], _configuration["VendorImageView"], _configuration["CustomerImageView"]);
                    if (OrderVendorViewModel != null)
                    {
                        return View(OrderVendorViewModel);
                    }
                }
                return Redirect(Url.Action("Index", "Orders", new { Area = "Order" }));
            }
            else
            {
                return Redirect("/AccessDenied/Unauthorized");
            }
        }
        [CustomeAuthoriz((int)ControllerEnum.VendorOrders, (int)ActionEnum.View)]
        public IActionResult DetailsVendor(Guid? orderVendorGuid)
        {
            if (GetPermissionUser((int)ActionEnum.View))
            {
                if (orderVendorGuid.HasValue)
                {
                    OrdersVendorViewModel OrderVendorViewModel = _blOrders.GetOrderVendorByGuid(orderVendorGuid.Value, InfraStructure.Utility.CurrentLanguageCode, _configuration["ProductImageView"], _configuration["VendorImageView"], _configuration["CustomerImageView"]);
                    if (OrderVendorViewModel != null)
                    {
                        return View(OrderVendorViewModel);
                    }
                }
                return Redirect(Url.Action("IndexVendor", "Orders", new { Area = "Order" }));
            }
            else
            {
                return Redirect("/AccessDenied/Unauthorized");
            }
        }
        [CustomeAuthoriz((int)ControllerEnum.VendorOrders, (int)ActionEnum.View)]
        public IActionResult DetailsPendingOrdersVendor(Guid? orderVendorGuid)
        {
            if (GetPermissionUser((int)ActionEnum.View))
            {
                if (orderVendorGuid.HasValue)
                {
                    OrdersVendorViewModel OrderVendorViewModel = _blOrders.GetOrderVendorByGuidAndPending(orderVendorGuid.Value, InfraStructure.Utility.CurrentLanguageCode, _configuration["ProductImageView"], _configuration["VendorImageView"], _configuration["CustomerImageView"]);
                    if (OrderVendorViewModel != null)
                    {
                        return View(OrderVendorViewModel);
                    }
                }
                return Redirect(Url.Action("PendingOrdersVendor", "Orders", new { Area = "Order" }));
            }
            else
            {
                return Redirect("/AccessDenied/Unauthorized");
            }
        }

        [CustomeAuthoriz((int)ControllerEnum.OperationOrders, (int)ActionEnum.View)]
        public IActionResult AssignOrder()
        {
            return View();
        }

        [CustomeAuthoriz((int)ControllerEnum.OperationOrders, (int)ActionEnum.Insert)]
        public IActionResult TrackingOrder()
        {
            return View();
        }
        [CustomeAuthoriz((int)ControllerEnum.OperationScheduleOrders, (int)ActionEnum.View)]
        public IActionResult ScheduleOrders()
        {
            return View();
        }
        [CustomeAuthoriz((int)ControllerEnum.VendorScheduleOrders, (int)ActionEnum.View)]
        public IActionResult ScheduleOrdersVendor()
        {
            return View();
        }
        [CustomeAuthoriz((int)ControllerEnum.VendorOrders, (int)ActionEnum.View)]
        public IActionResult PendingOrdersVendor()
        {
            if (GetPermissionUser((int)ActionEnum.View))
            {
                return View();
            }
            else
            {
                return Redirect("/AccessDenied/Unauthorized");
            }
        }
        [CustomeAuthoriz((int)ControllerEnum.OperationOrders, (int)ActionEnum.View)]
        public IActionResult OrdersByStatus(int? statusID, int? customersID, int cancelTypeID = 0)
        {
            if (GetPermissionUser((int)ActionEnum.View))
            {
                ViewData["OrdersStatusId"] = statusID;
                ViewData["CancelTypeID"] = cancelTypeID;
                ViewData["CustomersID"] = customersID;
                return View();
            }
            else
            {
                return Redirect("/AccessDenied/Unauthorized");
            }
        }
        #endregion
        #region Helper
        public JsonResult GetOrdersStatusList()
        {
            var listOfCities = _blOrders.GetAllOrdersStatus(Utility.CurrentLanguageCode).Where(s => /*s.OrderStatusID != (int)OrderStatusEnum.Shipping &&*/ s.OrderStatusID != (int)OrderStatusEnum.Cancel && s.OrderStatusID != (int)OrderStatusEnum.Drafts && s.OrderStatusID != (int)OrderStatusEnum.Pending).Select(p => new { p.OrderStatusID, OrderStatusName = Utility.CurrentLanguageCode == "ar" ? p.NameAR : p.NameEN });
            return Json(listOfCities);
        }
        public JsonResult GetAllOrdersStatusList()
        {
            var listOfCities = _blOrders.GetAllOrdersStatus(Utility.CurrentLanguageCode).Select(p => new { p.OrderStatusID, OrderStatusName = Utility.CurrentLanguageCode == "ar" ? p.NameAR : p.NameEN });
            return Json(listOfCities);
        }
        public JsonResult GetOrdersStatusTrackingList()
        {
            var listOfCities = _blOrders.GetAllOrdersStatus(Utility.CurrentLanguageCode).Where(s => s.OrderStatusID == (int)OrderStatusEnum.Assign || s.OrderStatusID == (int)OrderStatusEnum.Being_Delivery || s.OrderStatusID == (int)OrderStatusEnum.OnWay_Store
                ).Select(p => new { p.OrderStatusID, OrderStatusName = Utility.CurrentLanguageCode == "ar" ? p.NameAR : p.NameEN });
            return Json(listOfCities);
        }
        public JsonResult GetOrdersStatusListForVendorDetails(int captainTypeId)
        {
            var listOfCities = _blOrders.GetAllOrdersStatus(Utility.CurrentLanguageCode).ToList();
            if (captainTypeId == (int)CaptainTypeEnum.Store)
            {
                return Json(listOfCities.Where(s => s.OrderStatusID == (int)OrderStatusEnum.Accept || s.OrderStatusID == (int)OrderStatusEnum.Being_Processed || s.OrderStatusID == (int)OrderStatusEnum.Shipping
                || s.OrderStatusID == (int)OrderStatusEnum.Being_Delivery || s.OrderStatusID == (int)OrderStatusEnum.Delivered || s.OrderStatusID == (int)OrderStatusEnum.OnWay_Store)
                    .Select(p => new { p.OrderStatusID, OrderStatusName = Utility.CurrentLanguageCode == "ar" ? p.NameAR : p.NameEN }));
            }
            else
            {
                return Json(listOfCities.Where(s => s.OrderStatusID == (int)OrderStatusEnum.Accept || s.OrderStatusID == (int)OrderStatusEnum.Being_Processed || s.OrderStatusID == (int)OrderStatusEnum.Shipping)
                    .Select(p => new { p.OrderStatusID, OrderStatusName = Utility.CurrentLanguageCode == "ar" ? p.NameAR : p.NameEN }));
            }

        }
        public JsonResult GetDriversList()
        {
            var listOfCities = _blDriver.GetAllDriverActive().Select(p => new { p.DriversID, DriversName = Utility.CurrentLanguageCode == "ar" ? p.NameAr : p.NameEn });
            return Json(listOfCities);
        }
        public JsonResult GetVendorsListForTrackingOrder()
        {
            var listOfCities = _blVendor.GetAllVendorsForTrackingOrder().Select(p => new { p.VendorsID, VendorsName = Utility.CurrentLanguageCode == "ar" ? p.StoreNameAr : p.StoreNameEn });
            return Json(listOfCities);
        }
        [HttpPost]
        public JsonResult LoadTable(string listRegionID, string listCityID,
           string listBlockID, string listVendorID, string customerNameMobile, int? orderTypeId, DateTime? fromDateTime, DateTime? toDateTime, string listOrdersStatusId)
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
            var user = _blVendor.GetVendorByUserId(User.GetUserIdInt());
            if (user != null)
            {
                vendor = new string[1];
                vendor[0] = user.VendorsID.ToString();
            }
            string[] status = null;
            if (!string.IsNullOrEmpty(listOrdersStatusId) && listOrdersStatusId != "null")
            {
                status = listOrdersStatusId.Split(',');
            }
            var data = _blOrders.GetAllOrdersVendorViewModelBySearch(null, region, city, block, vendor, customerNameMobile, orderTypeId, fromDateTime, toDateTime, status, InfraStructure.Utility.CurrentLanguageCode, _configuration["ProductImageView"]);
            if (Request.Form.ContainsKey("sSearch"))
            {
                string search = Request.Form["sSearch"];
                if (!string.IsNullOrEmpty(search))
                {
                    data = data.Where(x =>
                    x.CustomersName.ToLower().Contains(search.ToLower()) || x.CustomersMobileNo.ToLower().Contains(search.ToLower()) || x.VendorsName.ToLower().Contains(search.ToLower()) ||
                    x.Price.ToString().ToLower().Contains(search.ToLower()) || x.Discount.ToString().ToLower().Contains(search.ToLower()) || x.Vat.ToString().ToLower().Contains(search.ToLower())
                    || x.DeliveryPrice.ToString().ToLower().Contains(search.ToLower()));
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
        [HttpPost]
        public JsonResult LoadTableTrackingOrder(string listOrdersStatusId, string listDriversID, string listVendorsID, int? captainTypeId)
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
            string[] status = null;
            if (!string.IsNullOrEmpty(listOrdersStatusId) && listOrdersStatusId != "null")
            {
                status = listOrdersStatusId.Split(',');
            }
            string[] driver = null;
            if (!string.IsNullOrEmpty(listDriversID) && listDriversID != "null")
            {
                driver = listDriversID.Split(',');
            }
            string[] vendor = null;
            if (!string.IsNullOrEmpty(listVendorsID) && listVendorsID != "null")
            {
                vendor = listVendorsID.Split(',');
            }
            var data = _blOrders.GetAllOrdersVendorViewModelForTrackingOrder(status, driver, vendor, captainTypeId, InfraStructure.Utility.CurrentLanguageCode, _configuration["ProductImageView"]);
            if (Request.Form.ContainsKey("sSearch"))
            {
                string search = Request.Form["sSearch"];
                if (!string.IsNullOrEmpty(search))
                {
                    data = data.Where(x =>
                    x.CustomersName.ToLower().Contains(search.ToLower()) || x.CustomersMobileNo.ToLower().Contains(search.ToLower()) || x.VendorsName.ToLower().Contains(search.ToLower()) ||
                    x.Price.ToString().ToLower().Contains(search.ToLower()) || x.Discount.ToString().ToLower().Contains(search.ToLower()) || x.Vat.ToString().ToLower().Contains(search.ToLower())
                    || x.DeliveryPrice.ToString().ToLower().Contains(search.ToLower()));
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
        public JsonResult LoadTableOrderItems(int orderVendorID)
        {
            var data = _blOrders.GetAllOrderItemsViewModelByOrderVendorID(orderVendorID, _configuration["ProductImageView"], InfraStructure.Utility.CurrentLanguageCode);
            if (Request.Form.ContainsKey("sSearch"))
            {
                string search = Request.Form["sSearch"];
                if (!string.IsNullOrEmpty(search))
                {
                    data = data.Where(x =>
                    x.ProductID.ToString().Contains(search) || x.ProdName.Contains(search) || x.Quantity.ToString().Contains(search) || x.Price.ToString().Contains(search) ||
                    x.Discount.ToString().Contains(search));
                }
            }
            var Responce = data.AsQueryable();
            int totalCount = data.Count();
            return Json(new
            {
                iTotalRecords = totalCount,
                iTotalDisplayRecords = totalCount,
                aaData = Responce
            });

        }
        public JsonResult GetAllListOrdersVendor(int ordersID)
        {
            var AllOrdersVendor = _blOrders.GetAllOrdersVendorViewModelByOrderID(ordersID, InfraStructure.Utility.CurrentLanguageCode);
            var list = AllOrdersVendor.Select(c => c.OrderVendorID).ToArray();
            return Json(list);
        }
        public bool GetPermissionUser(int action)
        {
            var user = bLUser.GetUserInfo(User.GetUserIdInt());
            int UserTypeId = user.UserType;
            int controller = (int)ControllerEnum.OperationOrders;
            if (UserTypeId == (int)UserTypeEnum.Vendor)
            {
                controller = (int)ControllerEnum.VendorOrders;
            }
            return _bLPermission.GetPermissionByUserAndController(user.UserName, controller, action);
        }
        [HttpPost]
        public JsonResult LoadTablePendingOrdersVendor(string listRegionID, string listCityID, string listBlockID, string listVendorID, string customerNameMobile)
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
            string[] region = null;
            string[] block = null;
            if (!string.IsNullOrEmpty(listBlockID) && listBlockID != "null" && listBlockID != "undefined")
            {
                block = listBlockID.Split(',');
            }
            if (string.IsNullOrEmpty(customerNameMobile) || customerNameMobile == "null" || customerNameMobile == "undefined")
            {
                customerNameMobile = string.Empty;
            }
            string[] vendor = null;
            if (!string.IsNullOrEmpty(listVendorID) && listVendorID != "null")
            {
                vendor = listVendorID.Split(',');
            }
            var user = _blVendor.GetVendorByUserId(User.GetUserIdInt());
            if (user != null)
            {
                vendor = new string[1];
                vendor[0] = user.VendorsID.ToString();
            }
            var data = _blOrders.GetAllOrdersVendorViewModelByPending(region, city, block, vendor, customerNameMobile, InfraStructure.Utility.CurrentLanguageCode, _configuration["ProductImageView"]);
            if (Request.Form.ContainsKey("sSearch"))
            {
                string search = Request.Form["sSearch"];
                if (!string.IsNullOrEmpty(search))
                {
                    data = data.Where(x =>
                    x.CustomersName.ToLower().Contains(search.ToLower()) || x.CustomersMobileNo.ToLower().Contains(search.ToLower()) || x.VendorsName.ToLower().Contains(search.ToLower()) ||
                    x.Price.ToString().ToLower().Contains(search.ToLower()) || x.Discount.ToString().ToLower().Contains(search.ToLower()) || x.Vat.ToString().ToLower().Contains(search.ToLower())
                    || x.DeliveryPrice.ToString().ToLower().Contains(search.ToLower()));
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
        public IActionResult GetIncreaseOrdersPartail(int OrderVendorID, bool? IsEdit)
        {
            var OrderVendorViewModel = _blOrders.GetOrderVendorByGuidAndPending(OrderVendorID, InfraStructure.Utility.CurrentLanguageCode, _configuration["ProductImageView"], _configuration["VendorImageView"], _configuration["CustomerImageView"]);
            if (IsEdit != null)
            {
                OrderVendorViewModel.IsEdit = (bool)IsEdit;
            }
            return PartialView("_IncreaseOrdersPartail", OrderVendorViewModel);
        }
        [HttpPost]
        public JsonResult LoadTableByStatus(int? ordersStatusId, int customersID, int cancelTypeID)
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
            int VendorsID = 0;
            var user = _blVendor.GetVendorByUserId(User.GetUserIdInt());
            if (user != null)
            {
                VendorsID = user.VendorsID;
            }
            var data = _blOrders.GetAllOrdersVendorViewModelByStatus(ordersStatusId, customersID, cancelTypeID, VendorsID, InfraStructure.Utility.CurrentLanguageCode, _configuration["ProductImageView"]);
            if (Request.Form.ContainsKey("sSearch"))
            {
                string search = Request.Form["sSearch"];
                if (!string.IsNullOrEmpty(search))
                {
                    data = data.Where(x =>
                    x.CustomersName.ToLower().Contains(search.ToLower()) || x.CustomersMobileNo.ToLower().Contains(search.ToLower()) || x.VendorsName.ToLower().Contains(search.ToLower()) ||
                    x.Price.ToString().ToLower().Contains(search.ToLower()) || x.Discount.ToString().ToLower().Contains(search.ToLower()) || x.Vat.ToString().ToLower().Contains(search.ToLower())
                    || x.DeliveryPrice.ToString().ToLower().Contains(search.ToLower()));
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
        [HttpPost]
        public JsonResult LoadTableOrderNotesAdmin(int orderVendorID)
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
            var data = _blOrderNotesAdmin.GetOrderNotesAdminViewModelByOrderVendor(orderVendorID, InfraStructure.Utility.CurrentLanguageCode);
            if (Request.Form.ContainsKey("sSearch"))
            {
                string search = Request.Form["sSearch"];
                if (!string.IsNullOrEmpty(search))
                {
                    data = data.Where(x =>
                    x.Notes.ToLower().Contains(search.ToLower()) || x.CreateDateString.ToLower().Contains(search.ToLower()) || x.UserName.ToLower().Contains(search.ToLower())).ToList();
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
        #endregion
        #region Promo
        [CustomeAuthoriz((int)ControllerEnum.PromoCode, (int)ActionEnum.View)]
        public IActionResult IndexPromo()
        {
            return View();
        }
        [CustomeAuthoriz((int)ControllerEnum.PromoCode, (int)ActionEnum.Insert)]
        public IActionResult CreatePromo()
        {
            return View(new PromoCodeMasterViewModel() { Cities = _blVendor.GetAllVendors() });
        }
        [CustomeAuthoriz((int)ControllerEnum.PromoCode, (int)ActionEnum.View)]
        public IActionResult DetailsPromo(Guid? id)
        {
            if (id.HasValue)
            {
                PromoCodeMasterViewModel promoCodeMasterViewModel = _blOrders.GetPromoCodeMasterViewModelByGuid(id.Value, Request.Cookies.IsArabic());
                if (promoCodeMasterViewModel != null)
                {
                    promoCodeMasterViewModel.Cities = _blVendor.GetAllVendors();
                    return View(promoCodeMasterViewModel);
                }
            }
            return Redirect(Url.Action("Index", "Orders", new { Area = "Order" }));
        }
        [CustomeAuthoriz((int)ControllerEnum.PromoCode, (int)ActionEnum.Update)]
        public IActionResult EditPromo(Guid? id)
        {
            if (id.HasValue)
            {
                PromoCodeMasterViewModel promoCodeMasterViewModel = _blOrders.GetPromoCodeMasterViewModelByGuid(id.Value, Request.Cookies.IsArabic());
                if (promoCodeMasterViewModel != null)
                {
                    promoCodeMasterViewModel.Cities = _blVendor.GetAllVendors();
                    return View(promoCodeMasterViewModel);
                }
            }
            return Redirect(Url.Action("Index", "Orders", new { Area = "Order" }));
        }
        #region Actions
        [CustomeAuthoriz((int)ControllerEnum.PromoCode, (int)ActionEnum.Insert)]
        [HttpPost, AutoValidateAntiforgeryToken]
        public IActionResult CreatePromo(PromoCodeMasterViewModel viewModel)
        {
            ResultMessage _Result = new ResultMessage();
            DateTime Datestart = DateTime.Now;
            DateTime.TryParseExact(viewModel.StartDateString, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out Datestart);
            viewModel.StartDate = Datestart;
            DateTime.TryParseExact(viewModel.ExpiryDateString, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out Datestart);
            viewModel.ExpiryDate = Datestart;
            ModelState.Remove("StartDate");
            ModelState.Remove("ExpiryDate");
            try
            {
                if (ModelState.IsValid)
                {
                    if (!_blOrders.IsPromoExisits(viewModel.PromoCode))
                    {
                        viewModel.UserId = User.GetUserIdInt();
                        var IsSave = _blOrders.Insert(viewModel);
                        if (IsSave == true)
                        {
                            _Result.Status = true;
                            _Result.ResultType = ResultMessageType.success.ToString();
                            _Result.Message = UI.Resources.Homemade.SuccessSaveMessage;
                            return Json(_Result);
                        }
                    }
                    else
                    {
                        _Result.Status = false;
                        _Result.ResultType = ResultMessageType.error.ToString();
                        _Result.Message = "الكود الترويجي موجود بالفعل";
                        return Json(_Result);
                    }
                    _Result.Status = false;
                    _Result.ResultType = ResultMessageType.error.ToString();
                    _Result.Message = Resources.Homemade.FailSaveMessage;
                    return Json(_Result);
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
            catch (System.Exception e)
            {
                return Json(new ResultMessage
                {
                    Status = false,
                    ResultType = ResultMessageType.error.ToString(),
                    Message = e.Message
                });
            }
        }
        [CustomeAuthoriz((int)ControllerEnum.PromoCode, (int)ActionEnum.Update)]
        [HttpPost, AutoValidateAntiforgeryToken]
        public IActionResult EditPromo(PromoCodeMasterViewModel viewModel)
        {
            ResultMessage _Result = new ResultMessage();
            DateTime Datestart = DateTime.Now;
            DateTime.TryParseExact(viewModel.StartDateString, "MM/dd/yyyy", null, System.Globalization.DateTimeStyles.None, out Datestart);
            viewModel.StartDate = Datestart;
            DateTime.TryParseExact(viewModel.ExpiryDateString, "MM/dd/yyyy", null, System.Globalization.DateTimeStyles.None, out Datestart);
            viewModel.ExpiryDate = Datestart;
            ModelState.Remove("StartDate");
            ModelState.Remove("ExpiryDate");
            try
            {
                if (ModelState.IsValid)
                {
                    if (viewModel.LimitCount >= _blOrders.GetPromoCodeDetailsCountByPromoCodeMasterID(viewModel.PromoCodeMasterID))
                    {
                        if (!_blOrders.IsPromoExisits(viewModel.PromoCode, viewModel.PromoCodeMasterID))
                        {
                            viewModel.UserUpdate = viewModel.UserDelete = viewModel.UserId = User.GetUserIdInt();
                            var IsSave = _blOrders.Update(viewModel);
                            if (IsSave == true)
                            {
                                _Result.Status = true;
                                _Result.ResultType = ResultMessageType.success.ToString();
                                _Result.Message = Homemade.UI.Resources.Homemade.SuccessSaveMessage;
                                return Json(_Result);
                            }
                        }
                        else
                        {
                            _Result.Status = false;
                            _Result.ResultType = ResultMessageType.error.ToString();
                            _Result.Message = "الكود الترويجي موجود بالفعل";
                        }
                        _Result.Status = false;
                        _Result.ResultType = ResultMessageType.error.ToString();
                        _Result.Message = Homemade.UI.Resources.Homemade.FailSaveMessage;
                        return Json(_Result);
                    }
                    else
                    {
                        return Json(new ResultMessage
                        {
                            Status = false,
                            ResultType = ResultMessageType.error.ToString(),
                            Message = Homemade.UI.Resources.Homemade.Limit_Count_Not_Valid
                        });
                    }
                }
                else
                {
                    return Json(new ResultMessage
                    {
                        Status = false,
                        ResultType = ResultMessageType.error.ToString(),
                        Message = Homemade.UI.Resources.Homemade.InsertValidDataMessage
                    });
                }
            }
            catch (System.Exception e)
            {
                return Json(new ResultMessage { Status = false, ResultType = ResultMessageType.error.ToString(), Message = e.Message });
            }
        }
        [CustomeAuthoriz((int)ControllerEnum.PromoCode, (int)ActionEnum.Delete)]
        [HttpPost]
        public JsonResult Delete(int id)
        {
            ResultMessage IsDelete = _blOrders.Delete(id, User.GetUserIdInt());
            if (IsDelete.Status == true)
            {
                IsDelete.ResultType = ResultMessageType.success.ToString();
                IsDelete.Message = Resources.Homemade.SuccessDeleteMessage;
            }
            else
            {
                IsDelete.ResultType = ResultMessageType.error.ToString();
                IsDelete.Message = Resources.Homemade.FailDeleteMessage;
            }
            return Json(IsDelete);
        }
        public class CancelReasons
        {
            public int id { get; set; }
            public string name { get; set; }
        }
        public JsonResult GetCancelReasons()
        {
            var list = new List<CancelReasons>();
            list.Add(new CancelReasons() { id = (int)CancelReasonEnum.Return_to_Wallet, name = Utility.CurrentLanguageCode == "ar" ? "إرجاع المبلغ في المحفظة" : "Return the amount in the wallet" });
            list.Add(new CancelReasons() { id = (int)CancelReasonEnum.Without, name = Utility.CurrentLanguageCode == "ar" ? "بدون" : "without" });
            return Json(list);
        }
        [HttpPost]
        public JsonResult ChangeOrderStatus(int orderId, int statusId)
        {
            var _Result = new ResultMessage();
            if (!_blOrders.IsExistOrderHistoryByOrderStatus(orderId, statusId))
            {
                var IsSuccess = _blVendor.ChangeStatus(orderId, User.GetUserIdInt(), InfraStructure.Utility.CurrentLanguageCode, statusId, null, _configuration["FireBaseKey"]);
                if (IsSuccess == "true")
                {
                    _Result.Message = InfraStructure.Utility.CurrentLanguageCode == "ar" ? Resources.Response_ar.Update_Success : Resources.Response.Update_Success;
                    _Result.Status = true;
                }
                else
                {
                    _Result.Message = IsSuccess;
                    _Result.Status = false;
                }
            }
            else
            {
                _Result.Message = Resources.Homemade.Error_In_Status_Order;
                _Result.Status = false;
            }

            return Json(_Result);
        }
        [HttpPost]
        public JsonResult CancelOrder(int OrderVendorID, int CancelReasonID)
        {
            var _Result = new ResultMessage();
            var IsSuccess = _blVendor.ChangeStatus(OrderVendorID, User.GetUserIdInt(), InfraStructure.Utility.CurrentLanguageCode, (int)OrderStatusEnum.Cancel, CancelReasonID, _configuration["FireBaseKey"]);
            if (IsSuccess == "true")
            {
                _Result.Message = InfraStructure.Utility.CurrentLanguageCode == "ar" ? Resources.Response_ar.Update_Success : Resources.Response.Update_Success;
                _Result.Status = true;
            }
            else
            {
                _Result.Message = IsSuccess;
                _Result.Status = false;
            }
            return Json(_Result);
        }
        [HttpPost]
        public JsonResult ShippingOrder(int OrderVendorID)
        {
            var _Result = new ResultMessage();
            var IsSuccess = _blVendor.ChangeStatusShipping(OrderVendorID, User.GetUserIdInt(), InfraStructure.Utility.CurrentLanguageCode, (int)OrderStatusEnum.Shipping, _configuration["FireBaseKey"]);
            if (IsSuccess == "true")
            {
                _Result.Message = InfraStructure.Utility.CurrentLanguageCode == "ar" ? Resources.Response_ar.Update_Success : Resources.Response.Update_Success;
                _Result.Status = true;
            }
            else
            {
                _Result.Message = IsSuccess;
                _Result.Status = false;
            }
            return Json(_Result);
        }
        public JsonResult ReAssginOrder(int OrderVendorID, int DriversID)
        {
            var IsSuccess = _blOrders.ReAssignOrderVendorToHomeMade(OrderVendorID, DriversID, User.GetUserIdInt());
            if (IsSuccess)
            {
                result.Status = true;
                result.ResultType = ResultMessageType.success.ToString();
                result.Message = Resources.Homemade.Orders_have_been_Assign_to_the_captain;
            }
            else
            {
                result.Status = false;
                result.ResultType = ResultMessageType.error.ToString();
                result.Message = Resources.Homemade.FailSaveMessage;
            }
            return Json(result);
        }
        [HttpPost]
        public async Task<JsonResult> UpdateOrderItems(int orderVendorID, int[] listOrderItemsID, string[] listQuantity)
        {
            if (listOrderItemsID.Any())
            {
                var IsSuccess = _blOrders.UpdateOrderItems(orderVendorID, listOrderItemsID, listQuantity, User.GetUserIdInt());
                if (IsSuccess)
                {
                    var ordervendor = _blOrders.GetOrderVendorByIdAndDeleted(orderVendorID, "Orders.Customers");
                    if (ordervendor != null)
                    {
                        var host = this.Request.Scheme + "://" + this.Request.Host;
                        var message = "هلا " + ordervendor.Orders.Customers.FirstName + Environment.NewLine + " تم الموافقة على طلب اذن زيادة الكمية للطلب رقم " + ordervendor.OrderVendorID;
                        message += Environment.NewLine + " اللينك: " + host + "/Site/Home/completePendingOrder?id=" + ordervendor.Orders.OrdersGuid;
                        await _OTPService.SendSMSForOrder(ordervendor.Orders.Customers.MobileNo, message, (int)UserTypeEnum.Customer, (int)MessageReasonEnum.Order);
                    }
                    result.Message = Resources.Homemade.SuccessSaveMessage;
                    result.Status = true;
                    result.ResultType = ResultMessageType.success.ToString();
                }
                else
                {
                    result.Status = false;
                    result.ResultType = ResultMessageType.error.ToString();
                    result.Message = Resources.Homemade.FailSaveMessage;
                }
            }
            else
            {
                result.Status = false;
                result.ResultType = ResultMessageType.error.ToString();
                result.Message = Resources.Homemade.FailSaveMessage;
            }
            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> ApproveOrder(int orderVendorID)
        {
            var IsSuccess = _blOrders.ApproveOrder(orderVendorID, User.GetUserIdInt());
            if (IsSuccess)
            {
                var ordervendor = _blOrders.GetOrderVendorByIdAndDeleted(orderVendorID, "Orders.Customers");
                if (ordervendor != null)
                {
                    var host = this.Request.Scheme + "://" + this.Request.Host;
                    var message = "هلا " + ordervendor.Orders.Customers.FirstName + Environment.NewLine + " تم الموافقة على طلب اذن زيادة الكمية للطلب رقم " + ordervendor.OrderVendorID;
                    message += Environment.NewLine + " اللينك: " + host + "/Site/Home/completePendingOrder?id=" + ordervendor.Orders.OrdersGuid;
                    await _OTPService.SendSMSForOrder(ordervendor.Orders.Customers.MobileNo, message, (int)UserTypeEnum.Customer, (int)MessageReasonEnum.Order);
                }
                result.Message = Resources.Homemade.SuccessSaveMessage;
                result.Status = true;
                result.ResultType = ResultMessageType.success.ToString();
            }
            else
            {
                result.Status = false;
                result.ResultType = ResultMessageType.error.ToString();
                result.Message = Resources.Homemade.FailSaveMessage;
            }
            return Json(result);
        }
        [HttpPost]
        public async Task<JsonResult> RejectOrder(int orderVendorID)
        {
            var IsSuccess = _blOrders.RejectOrder(orderVendorID, User.GetUserIdInt());
            if (IsSuccess)
            {
                var ordervendor = _blOrders.GetOrderVendorByIdAndDeleted(orderVendorID, "Orders.Customers");
                if (ordervendor != null)
                {
                    var host = this.Request.Scheme + "://" + this.Request.Host;
                    var message = "هلا " + ordervendor.Orders.Customers.FirstName + Environment.NewLine + " تم رفض طلب اذن زيادة الكمية للطلب رقم " + ordervendor.OrderVendorID;
                    message += Environment.NewLine + " اللينك: " + host + "/Site/Home/completePendingOrder?id=" + ordervendor.Orders.OrdersGuid;
                    await _OTPService.SendSMSForOrder(ordervendor.Orders.Customers.MobileNo, message, (int)UserTypeEnum.Customer, (int)MessageReasonEnum.Order);
                }
                result.Message = Resources.Homemade.SuccessSaveMessage;
                result.Status = true;
                result.ResultType = ResultMessageType.success.ToString();
            }
            else
            {
                result.Status = false;
                result.ResultType = ResultMessageType.error.ToString();
                result.Message = Resources.Homemade.FailSaveMessage;
            }
            return Json(result);
        }
        #endregion
        #region Json
        [HttpPost]
        public JsonResult AddOrderNotesAdmin(int orderVendorID, string notes)
        {
            var _Result = new ResultMessage();
            if (!string.IsNullOrEmpty(notes))
            {
                var IsSuccess = _blOrderNotesAdmin.Insert(orderVendorID, notes, User.GetUserIdInt());
                if (IsSuccess)
                {
                    _Result.Status = true;
                    _Result.ResultType = ResultMessageType.success.ToString();
                    _Result.Message = UI.Resources.Homemade.SuccessSaveMessage;
                }
                else
                {
                    _Result.Status = false;
                    _Result.ResultType = ResultMessageType.error.ToString();
                    _Result.Message = UI.Resources.Homemade.FailSaveMessage;
                }
            }
            else
            {
                _Result.Status = false;
                _Result.ResultType = ResultMessageType.error.ToString();
                _Result.Message = UI.Resources.Homemade.Please_Insert_Notes;
            }
            return Json(_Result);
        }
        public JsonResult GetVendors()
        {
            var list = _blVendor.GetAllVendors().Select(p => new { ID = p.VendorsID, Name = Request.Cookies.IsArabic() ? p.StoreNameAr : p.StoreNameEn });
            return Json(list);
        }

        [HttpPost]
        public JsonResult LoadTablePromo()
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
            var data = _blOrders.GetPromoCodeMasterViewModels();
            if (Request.Form.ContainsKey("sSearch"))
            {
                string sea = Request.Form["sSearch"];
                if (!string.IsNullOrEmpty(sea))
                {
                    data = data.Where(x => x.PromoCode.Contains(sea));
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
                aaData = DataResponce.ToList().Select(x => new
                {
                    StartDate = x.StartDate.ToString("dd/MM/yyyy"),
                    ExpiryDate = x.ExpiryDate.ToString("dd/MM/yyyy"),
                    x.Content,
                    x.CreateDate,
                    DiscountType = (DiscountTypeEnum)x.DiscountType == DiscountTypeEnum.Amount ? Resources.Homemade.Amount : Resources.Homemade.Percent,
                    x.DiscountValue,
                    x.PromoCodeGuid,
                    x.PromoCodeMasterID,
                    x.Subject,
                    x.PromoCode,
                    x.LimitCount,

                })
            });
        }
        public JsonResult IsExistOrderHistoryByOrderStatus(int orderId, int statusId)
        {
            var obj = _blOrders.IsExistOrderHistoryByOrderStatus(orderId, statusId);
            return Json(obj);
        }
        #endregion
        #endregion
        #region AssignOrder
        [HttpPost]
        public JsonResult LoadTableBlocks()
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
            var data = _blOrders.GetAllBlockAssignOrderViewModel(Utility.CurrentLanguageCode);
            if (Request.Form.ContainsKey("sSearch"))
            {
                string sea = Request.Form["sSearch"];
                if (!string.IsNullOrEmpty(sea))
                {
                    data = data.Where(x => x.BlockID.ToString().ToLower().Contains(sea.ToLower()) || x.BlockName.ToLower().Contains(sea.ToLower())
                    || x.OrdersCount.ToString().ToLower().Contains(sea.ToLower())).ToList();
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
        [HttpPost]
        public JsonResult LoadTableOrderVendor(string listBlockID)
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
            string[] block = null;
            if (!string.IsNullOrEmpty(listBlockID) && listBlockID != "null")
            {
                block = listBlockID.Split(',');
            }
            var data = _blOrders.GetAllAssignOrderVendorViewModel(block, Utility.CurrentLanguageCode);
            if (Request.Form.ContainsKey("sSearch"))
            {
                string sea = Request.Form["sSearch"];
                if (!string.IsNullOrEmpty(sea))
                {
                    data = data.Where(x => x.CustomersName.ToLower().Contains(sea.ToLower()) || x.OrderVendorID.ToString().ToLower().Contains(sea.ToLower())).ToList();
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
        [HttpPost]
        public JsonResult LoadTableCaptain(string listBlockID, int? onlineTypeID)
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
            string[] block = null;
            if (!string.IsNullOrEmpty(listBlockID) && listBlockID != "null")
            {
                block = listBlockID.Split(',');
            }
            var data = _blOrders.GetAllDriverAssignOrderViewModel(block, onlineTypeID, Utility.CurrentLanguageCode);
            if (Request.Form.ContainsKey("sSearch"))
            {
                string sea = Request.Form["sSearch"];
                if (!string.IsNullOrEmpty(sea))
                {
                    data = data.Where(x => x.DriversName.ToLower().Contains(sea.ToLower()) || x.DriversID.ToString().ToLower().Contains(sea.ToLower())).ToList();
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
        [HttpPost]
        public JsonResult AssignOrder(string ListOrderVendorID, int CaptainTypeID, int ShippingCompanyID, int DriversID)
        {
            try
            {

                if (!string.IsNullOrEmpty(ListOrderVendorID))
                {
                    string[] ListOrderVendor = ListOrderVendorID.Trim().Split(new string[] { "," }, StringSplitOptions.None);
                    if (ListOrderVendor.Any())
                    {
                        if (CaptainTypeID == (int)CaptainTypeEnum.Shipping_Company)
                        {
                            var status = _blOrders.AssignOrderVendorToExternalCompany(ListOrderVendor, ShippingCompanyID, CaptainTypeID, User.GetUserIdInt());
                            if (status)
                            {
                                result.Status = true;
                                result.ResultType = ResultMessageType.success.ToString();
                                result.Message = Resources.Homemade.Orders_have_been_Assign_to_the_captain;
                            }
                            else
                            {
                                result.Status = false;
                                result.ResultType = ResultMessageType.error.ToString();
                                result.Message = Resources.Homemade.FailSaveMessage;
                            }
                        }
                        else
                        {
                            var status = _blOrders.AssignOrderVendorToHomeMade(ListOrderVendor, DriversID, User.GetUserIdInt());
                            if (status)
                            {
                                result.Status = true;
                                result.ResultType = ResultMessageType.success.ToString();
                                result.Message = Resources.Homemade.Orders_have_been_Assign_to_the_captain;
                            }
                            else
                            {
                                result.Status = false;
                                result.ResultType = ResultMessageType.error.ToString();
                                result.Message = Resources.Homemade.FailSaveMessage;
                            }
                        }
                    }
                    else
                    {
                        result.Status = false;
                        result.ResultType = ResultMessageType.error.ToString();
                        result.Message = Resources.Homemade.Please_Select_One_Order_At_Least;
                    }

                }
                else
                {
                    result.Status = false;
                    result.ResultType = ResultMessageType.error.ToString();
                    result.Message = Resources.Homemade.Please_Select_One_Order_At_Least;
                }
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.ResultType = ResultMessageType.error.ToString();
                result.Message = Resources.Homemade.FailSaveMessage;
            }
            return Json(result);
        }
        #endregion
    }
}
