using Homemade.BLL;
using Homemade.BLL.General;
using Homemade.BLL.Order;
using Homemade.BLL.Setting;
using Homemade.BLL.Vendor;
using Homemade.BLL.ViewModel.Order;
using Homemade.UI.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homemade.UI.Areas.Order.Controllers
{
    public class OrdersStatusController : Controller
    {
        #region Declarations
        private readonly ResultMessage result;
        private readonly BlOrders _blOrders;
        private readonly IConfiguration _configuration;
        private readonly BLPermission _bLPermission;
        private readonly BLUser bLUser;
        private readonly BlCity _BlCity;
        private readonly BlVendor _blVendor;
        public OrdersStatusController(BlCity BlCity, BlOrders blOrders, ResultMessage result, IConfiguration configuration, BLPermission bLPermission, BLUser bLUser, BlVendor blVendor)
        {
            _blOrders = blOrders;
            this.result = result;
            _configuration = configuration;
            _bLPermission = bLPermission;
            this.bLUser = bLUser;
            _blVendor = blVendor;
            _BlCity = BlCity;
        }
        #endregion
        #region View
        public IActionResult Index(Guid orderStatus)
        {
            if (GetPermissionUser((int)ActionEnum.View))
            {
                var OrderStatus = _blOrders.OrderStatusByGuid(orderStatus);
                ViewBag.OrderStatusId = OrderStatus.OrderStatusID;
                return View();
            }
            else
            {
                return Redirect("/AccessDenied/Unauthorized");
            }
        }
        public IActionResult Details(Guid? id)
        {
            if (GetPermissionUser((int)ActionEnum.View))
            {
                if (id.HasValue)
                {
                    OrdersViewModel ordersViewModel = _blOrders.GetOrdersViewModelByGuid(id.Value, InfraStructure.Utility.CurrentLanguageCode, _configuration["CustomerImageView"]);
                    if (ordersViewModel != null)
                    {
                        return View(ordersViewModel);
                    }
                }
                return Redirect(Url.Action("Index", "Orders", new { Area = "Order" }));
            }
            else
            {
                return Redirect("/AccessDenied/Unauthorized");
            }
        }
        #endregion
        #region Helper
        [HttpPost]
        public JsonResult LoadTable(int ordersStatus, string listRegionID, string listCityID,
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
            string[] status = null;
            if (!string.IsNullOrEmpty(listOrdersStatusId) && listOrdersStatusId != "null")
            {
                status = listOrdersStatusId.Split(',');
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
            var data = _blOrders.GetAllOrdersVendorViewModelBySearch(ordersStatus, region, city, block, vendor, customerNameMobile, orderTypeId, fromDateTime, toDateTime, status, InfraStructure.Utility.CurrentLanguageCode, _configuration["ProductImageView"]);
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
                    viewModel.UserId = User.GetUserIdInt();
                    var IsSave = _blOrders.Insert(viewModel);
                    if (IsSave == true)
                    {
                        _Result.Status = true;
                        _Result.ResultType = ResultMessageType.success.ToString();
                        _Result.Message = UI.Resources.Homemade.SuccessSaveMessage;
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
                    if (viewModel.LimitCount >= _blOrders.GetPromoCodeDetailsCountByPromoCodeMasterID(viewModel.PromoCodeMasterID))
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
        #endregion
        #region Json
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
        #endregion
        #endregion
    }
}
