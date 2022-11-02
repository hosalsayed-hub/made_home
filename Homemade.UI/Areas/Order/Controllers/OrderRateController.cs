using Homemade.BLL;
using Homemade.BLL.General;
using Homemade.BLL.Order;
using Homemade.BLL.Vendor;
using Homemade.UI.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homemade.UI.Areas.Order.Controllers
{
    public class OrderRateController : Controller
    {
        #region Declarations
        private readonly BLUser _bLUser;
        private readonly BlVendor _blVendor;
        private readonly BlOrderRate _blOrderRate;
        private readonly IConfiguration _configuration;
        private readonly ResultMessage result;
        public OrderRateController(BLUser bLUser, BlVendor blVendor, BlOrderRate blOrderRate, IConfiguration configuration, ResultMessage result)
        {
            _bLUser = bLUser;
            _blVendor = blVendor;
            _blOrderRate = blOrderRate;
            _configuration = configuration;
            this.result = result;
        }
        #endregion
        #region View
        [CustomeAuthoriz((int)ControllerEnum.OrderRate, (int)ActionEnum.View)]
        public IActionResult Index()
        {
            return View();
        }
        [CustomeAuthoriz((int)ControllerEnum.VendorOrderRate, (int)ActionEnum.View)]
        public IActionResult IndexVendor()
        {
            return View();
        }
        #endregion
        #region Helpers
        public IActionResult GetRateReplyPartial(string listVendorID)
        {
            string[] vendor = null;
            if (!string.IsNullOrEmpty(listVendorID) && listVendorID != "null")
            {
                vendor = listVendorID.Split(',');
            }
            var model = _blOrderRate.GetAllOrderRateViewModelRepley(vendor, InfraStructure.Utility.CurrentLanguageCode, _configuration["CustomerImageView"], _configuration["VendorImageView"]);
            return PartialView("_RateReplyPartial", model);
        }
        public IActionResult GetRateReplyPartialVendor()
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

            var model = _blOrderRate.GetAllOrderRateViewModelRepley(VendorsID, _configuration["CustomerImageView"], _configuration["VendorImageView"], InfraStructure.Utility.CurrentLanguageCode);
            return PartialView("_RateReplyPartialVendor", model);
        }
        public IActionResult GetRateNotReplyPartial(string listVendorID)
        {
            string[] vendor = null;
            if (!string.IsNullOrEmpty(listVendorID) && listVendorID != "null")
            {
                vendor = listVendorID.Split(',');
            }
            var model = _blOrderRate.GetAllOrderRateViewModelNotRepley(vendor, InfraStructure.Utility.CurrentLanguageCode, _configuration["CustomerImageView"], _configuration["VendorImageView"]);
            return PartialView("_RateNotReplyPartial", model);
        }
        public IActionResult GetRateNotReplyPartialVendor()
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

            var model = _blOrderRate.GetAllOrderRateViewModelNotRepley(VendorsID, _configuration["CustomerImageView"], _configuration["VendorImageView"], InfraStructure.Utility.CurrentLanguageCode);
            return PartialView("_RateNotReplyPartialWithAdd", model);
        }
        public JsonResult GetRateCount(bool IsVendor, bool IsReply, string listVendorID)
        {
            string[] vendor = null;
            if (!string.IsNullOrEmpty(listVendorID) && listVendorID != "null")
            {
                vendor = listVendorID.Split(',');
            }
            if (IsVendor)
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
                        vendor = new List<string> { VendorsID.ToString() }.ToArray();
                    }
                }
            }

            var obj = _blOrderRate.GetOrderRateCount(IsReply, vendor);
            return Json(obj);
        }
        #endregion
        #region Actions
        [CustomeAuthoriz((int)ControllerEnum.VendorOrderRate, (int)ActionEnum.Update)]
        [HttpPost]
        public JsonResult UpdateAnswer(int? OrderRateID, string Answer)
        {
            try
            {
                if (OrderRateID != null)
                {
                    if (!string.IsNullOrEmpty(Answer))
                    {
                        var status = _blOrderRate.UpdateRate((int)OrderRateID, InfraStructure.Utility.CurrentLanguageCode, Answer, User.GetUserIdInt(), _configuration["FireBaseKey"]);
                        if (status)
                        {
                            result.Status = true;
                            result.ResultType = ResultMessageType.success.ToString();
                            result.Message = Resources.Homemade.SuccessSaveMessage;
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
                        result.Message = Resources.Homemade.Please_Insert_Answer;
                    }
                }
                else
                {
                    result.Status = false;
                    result.ResultType = ResultMessageType.error.ToString();
                    result.Message = Resources.Homemade.InsertValidDataMessage;
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
        [CustomeAuthoriz((int)ControllerEnum.OrderRate, (int)ActionEnum.Delete)]
        [HttpPost]
        public JsonResult DeleteOrderRate(int id)
        {
            var result = false;
            try
            {
                result = _blOrderRate.Delete(id, User.GetUserIdInt());
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
    }
}
