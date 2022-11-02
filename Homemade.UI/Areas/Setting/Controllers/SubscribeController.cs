using Homemade.BLL;
using Homemade.BLL.General;
using Homemade.BLL.Setting;
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
    public class SubscribeController : Controller
    {
        #region Declarations
        private readonly BlVendor _blVendor;
        private readonly BlSubscribe _blSubscribe;
        private readonly IConfiguration _configuration;
        private readonly BLGeneral _blGeneral;
        private readonly OTPService _OTPService;
        public SubscribeController(BlVendor blVendor, IConfiguration configuration, BLGeneral blGeneral, OTPService oTPService, BlSubscribe blSubscribe)
        {
            _blVendor = blVendor;
            _configuration = configuration;
            _blGeneral = blGeneral;
            _OTPService = oTPService;
            _blSubscribe = blSubscribe;
        }
        #endregion
        #region View
        [CustomeAuthoriz((int)ControllerEnum.Subscribe, (int)ActionEnum.View)]
        public IActionResult Index()
        {
            return View();
        }
        #endregion
        #region Helpers
        public JsonResult LoadTable()
        {
            var data = _blSubscribe.GetAllSubscribeViewModel();
            if (Request.Form.ContainsKey("sSearch"))
            {
                string search = Request.Form["sSearch"];
                if (!string.IsNullOrEmpty(search))
                {
                    data = data.Where(x =>
                    x.Name.ToLower().Contains(search.ToLower()) || x.Email.ToLower().Contains(search.ToLower()));
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
        public async Task<JsonResult> SendSubscribe(int sendTypeId, string checkedItems, string message)
        {
            var result = false;
            try
            {
                if (!string.IsNullOrEmpty(message))
                {

                    if (sendTypeId == 1)
                    {
                        var subscribes = _blSubscribe.GetAllSubscribe();
                        foreach (var item in subscribes)
                        {
                            if (!string.IsNullOrEmpty(item.Email))
                            {
                                _blGeneral.SendEmail(item.Email, "Home Made", message);
                            }
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(checkedItems))
                        {
                            string[] subscribecheck = checkedItems.Trim().Split(new string[] { "," }, StringSplitOptions.None);
                            foreach (var item in subscribecheck)
                            {
                                if (!string.IsNullOrEmpty(item))
                                {
                                    var subscribe = _blSubscribe.GetById(Convert.ToInt32(item));
                                    if (subscribe != null)
                                    {
                                        _blGeneral.SendEmail(subscribe.Email, "Home Made", message);
                                    }
                                }
                            }
                        }
                    }
                    result = true;
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
