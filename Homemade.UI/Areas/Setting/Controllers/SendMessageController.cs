using Homemade.BLL;
using Homemade.BLL.General;
using Homemade.BLL.SMS;
using Homemade.BLL.Vendor;
using Homemade.UI.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homemade.UI.Areas.Setting.Controllers
{
    public class SendMessageController : Controller
    {
        #region Declarations
        private readonly BlVendor _blVendor;
        private readonly IConfiguration _configuration;
        private readonly BLGeneral _blGeneral;
        private readonly OTPService _OTPService;
        public SendMessageController(BlVendor blVendor, IConfiguration configuration, BLGeneral blGeneral, OTPService oTPService)
        {
            _blVendor = blVendor;
            _configuration = configuration;
            _blGeneral = blGeneral;
            _OTPService = oTPService;
        }
        #endregion
        #region View
        [CustomeAuthoriz((int)ControllerEnum.SendMessage, (int)ActionEnum.View)]
        public IActionResult Index()
        {
            return View();
        }
        #endregion
        #region Helpers
        public JsonResult LoadTable()
        {
            var data = _blVendor.GetAllVendorViewModel(InfraStructure.Utility.CurrentLanguageCode, _configuration["VendorImageView"]);
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
            int totalCount = data.Count();
            return Json(new
            {
                iTotalRecords = totalCount,
                iTotalDisplayRecords = totalCount,
                aaData = data
            });

        }
        [HttpPost]
        public async Task<JsonResult> SendMessageToVendor(int sendTypeId, int[] messageTypeId, string checkedItems, string message)
        {
            var result = false;
            try
            {
                if (!string.IsNullOrEmpty(message))
                {

                    if (messageTypeId != null)
                    {
                        if (messageTypeId.Any(x => x == (int)MessageTypeEnum.SMS))
                        {
                            if (sendTypeId == 1)
                            {
                                var vendors = _blVendor.GetAllVendors("User");
                                foreach (var item in vendors)
                                {
                                    if (!string.IsNullOrEmpty(item.Email))
                                    {
                                        string vendorname = InfraStructure.Utility.CurrentLanguageCode == "ar" ? item.FirstNameAr : item.FirstNameEn;
                                        var ms = message.Replace("@Username", item.User.UserName).Replace("@Username", vendorname);
                                    }
                                }
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(checkedItems))
                                {
                                    string[] vendotcheck = checkedItems.Trim().Split(new string[] { "," }, StringSplitOptions.None);
                                    foreach (var item in vendotcheck)
                                    {
                                        if (!string.IsNullOrEmpty(item))
                                        {
                                            var vendor = _blVendor.GetById(Convert.ToInt32(item), "User");
                                            if (vendor != null)
                                            {
                                                string vendorname = InfraStructure.Utility.CurrentLanguageCode == "ar" ? vendor.FirstNameAr : vendor.FirstNameEn;
                                                var ms = message.Replace("@Username", vendor.User.UserName).Replace("@VendorName", vendorname);
                                            }
                                        }
                                    }
                                }
                            }
                            result = true;
                        }
                        if (messageTypeId.Any(x => x == (int)MessageTypeEnum.Email))
                        {
                            if (sendTypeId == 1)
                            {
                                var vendors = _blVendor.GetAllVendors("User");
                                foreach (var item in vendors)
                                {
                                    if (!string.IsNullOrEmpty(item.Email))
                                    {
                                        string vendorname = InfraStructure.Utility.CurrentLanguageCode == "ar" ? item.FirstNameAr : item.FirstNameEn;
                                        var ms = message.Replace("@Username", item.User.UserName).Replace("@Username", vendorname);
                                        _blGeneral.SendEmail(item.Email, "Home Made", ms);
                                    }
                                }
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(checkedItems))
                                {
                                    string[] vendotcheck = checkedItems.Trim().Split(new string[] { "," }, StringSplitOptions.None);
                                    foreach (var item in vendotcheck)
                                    {
                                        if (!string.IsNullOrEmpty(item))
                                        {
                                            var vendor = _blVendor.GetById(Convert.ToInt32(item), "User");
                                            if (vendor != null)
                                            {
                                                string vendorname = InfraStructure.Utility.CurrentLanguageCode == "ar" ? vendor.FirstNameAr : vendor.FirstNameEn;
                                                var ms = message.Replace("@Username", vendor.User.UserName).Replace("@VendorName", vendorname);
                                                _blGeneral.SendEmail(vendor.Email, "Home Made", ms);
                                            }
                                        }
                                    }
                                }
                            }
                            result = true;
                        }
                    }
                }
                else
                {
                    ResultMessage resultMessage = new ResultMessage { Message = Resources.Homemade.InsertValidDataMessage, ResultType = ResultMessageType.error.ToString() };
                    return Json(resultMessage);
                }
            }
            catch (Exception ex)
            {
                result = false;
            }
            ResultMessage rResultMessage = result ?
              new ResultMessage { Message = Resources.Homemade.SuccessSendMessage, ResultType = ResultMessageType.success.ToString() }
              :
              new ResultMessage { Message = Resources.Homemade.FailSendMessage, ResultType = ResultMessageType.error.ToString() };
            return Json(rResultMessage);
        }

        #endregion

    }
}
