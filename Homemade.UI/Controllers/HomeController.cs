using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using Microsoft.Extensions.Configuration;
using Homemade.Model;
using Homemade.UI.InfraStructure;
using Microsoft.AspNetCore.Authorization;
using Homemade.BLL;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Homemade.UI.Attributes;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Net.Http.Headers;
using Homemade.BLL.General;
using Homemade.BLL.Vendor;
using Homemade.BLL.Order;
using Homemade.BLL.Emp;
using Homemade.BLL.Customer;

namespace Homemade.UI.Controllers
{
    public class HomeController : Controller
    {

        #region Declarations

        private readonly IConfiguration _configuration;
        private readonly BLGeneral _bLGeneral;
        private readonly BLUser bLUser;
        private readonly BlVendor _blVendor;
        private readonly BlOrders _blOrders;
        private readonly BlEmployee _blEmployee;
        private readonly BlCustomer _blCustomer;
        public HomeController(BLGeneral BLGeneral, BLUser bLUser, BlVendor blVendor, IConfiguration configuration, BlOrders blOrders, BlEmployee blEmployee, BlCustomer blCustomer)
        {
            _bLGeneral = BLGeneral;
            this.bLUser = bLUser;
            _blVendor = blVendor;
            _configuration = configuration;
            _blOrders = blOrders;
            _blEmployee = blEmployee;
            _blCustomer = blCustomer;
        }

        #endregion
        #region Views
        [CustomeAuthoriz((int)ControllerEnum.OperationDashboard, (int)ActionEnum.View)]
        [HttpGet]
        public ActionResult Index()
        {
            var userdata = bLUser.GetUserInfo(User.GetUserIdInt());
            if (userdata != null)
            {
                if (userdata.UserType == (int)UserTypeEnum.Vendor)
                {
                    return LocalRedirect("/Order/Orders/Index");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return LocalRedirect("/Identity/Account/Login");
            }
        }
        public ActionResult IndexVendor()
        {
            var userdata = bLUser.GetUserInfo(User.GetUserIdInt());
            if (userdata != null)
            {
                return View();
            }
            else
            {
                return LocalRedirect("/Identity/Account/LoginVendor");
            }
        }
        [Authorize]
        [HttpGet]
        public ActionResult C_Dashboard()
        {
            return View();
        }
        public ActionResult error()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
        public ActionResult ChangeLang(string LangId)
        {

            if (LangId != null)
            {
                Response.Cookies.Append(
               CookieRequestCultureProvider.DefaultCookieName,
               CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(LangId)),
               new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
               );

            }
            HttpContext.Response.Cookies.Append("Language", LangId);
            string referer = Request.Headers["Referer"].ToString();
            return Redirect(referer);
        }
        #endregion
        #region Helpers
        public class DashSummaryChart
        {

            public string[] labelsName = { };
            public int?[] Trips = new int?[7];
            public int?[] Delivered = new int?[7];
            public int?[] Canceled = new int?[7];

            public int?[] labelsNamefinances = new int?[7];
            public double?[] BaseCost = new double?[7];
            public double?[] Collected = new double?[7];
            public double?[] PaidToClient = new double?[7];
            public double?[] ReceiverCollected = new double?[7];
            public double?[] DebtClient = new double?[7];
            public double?[] CompanyCharge = new double?[7];
            public double?[] CreditDriver = new double?[7];
            public double?[] CreditClient = new double?[7];
            public double?[] DriverCharge = new double?[7];


        }
        [HttpGet]
        public JsonResult GetDashSummaryChart()
        {

            #region Trip Chart
            int[] ListOrdersCount;
            int[] ListCaptainsCount;
            double[] OrdersCount;
            double[] VendorsCount;

            #endregion
            _blEmployee.GetAllSummaryChart(InfraStructure.Utility.CurrentLanguageCode, out ListOrdersCount, out ListCaptainsCount, out OrdersCount, out VendorsCount);
            return Json(new
            {
                listOrdersCount = ListOrdersCount,
                listCaptainsCount = ListCaptainsCount,
                ordersCount = OrdersCount,
                vendorsCount = VendorsCount,
            });
        }
        [HttpGet]
        public JsonResult GetDashSummaryChartVendor()
        {

            #region Trip Chart
            int[] ListOrdersCount;
            double[] OrdersCount;

            #endregion
            int VendorsID = 0;
            var user = _blVendor.GetVendorByUserId(User.GetUserIdInt());
            if (user != null)
            {
                VendorsID = user.VendorsID;
            }
            _blVendor.GetAllSummaryChartVendor(VendorsID, InfraStructure.Utility.CurrentLanguageCode, out ListOrdersCount
                , out OrdersCount);

            return Json(new
            {
                listOrdersCount = ListOrdersCount,
                ordersCount = OrdersCount,
            });
        }
        [HttpPost]
        public JsonResult LoadTableVendors()
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
            var data = _blVendor.GetAllVendorViewModelByNotEnable(InfraStructure.Utility.CurrentLanguageCode, _configuration["VendorImageView"]);
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
        [HttpPost]
        public JsonResult LoadTableOrders(string listOrdersStatusId)
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
            var data = _blOrders.GetAllOrdersVendorViewModelForDashBoard(status, InfraStructure.Utility.CurrentLanguageCode, _configuration["ProductImageView"]);
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
        public JsonResult LoadTableOrdersVendor(string listOrdersStatusId)
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

            int VendorsID = 0;
            var user = _blVendor.GetVendorByUserId(User.GetUserIdInt());
            if (user != null)
            {
                VendorsID = user.VendorsID;
            }
            var data = _blOrders.GetAllOrdersVendorViewModelForDashBoardVendor(VendorsID, status, InfraStructure.Utility.CurrentLanguageCode, _configuration["ProductImageView"]);
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
        #endregion
        #region Notifictions
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
        public JsonResult SeenAllNotifictionsVendors()
        {
            int UserId = User.GetUserIdInt();
            var IsSuccess = _blCustomer.UpdateAllNotificationSeenByVendors(UserId);
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
    }
}