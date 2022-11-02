using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Homemade.BLL;
using Homemade.BLL.General;
using Homemade.BLL.Setting;
using Homemade.BLL.ViewModel.Setting;
using Homemade.UI.Attributes;
using Homemade.UI.InfraStructure;

namespace Homemade.UI.Areas.Setting.Controllers
{
    public class PaymentStatusController : Controller
    {
        #region Declarations
        private readonly BlPaymentStatus blPaymentStatus;
        private readonly ResultMessage result;
        private readonly BLGeneral _bLGeneral;
        public PaymentStatusController(BlPaymentStatus blPaymentStatus, ResultMessage result, BLGeneral bLGeneral)
        {
            _bLGeneral = bLGeneral;
            this.blPaymentStatus = blPaymentStatus;
            this.result = result;
        }
        #endregion
        #region View
        [CustomeAuthoriz((int)ControllerEnum.PaymentStatus, (int)ActionEnum.View)]
        public IActionResult Index() => View(new PaymentStatusViewModel());
        #endregion
        #region Actions

        /// <summary>
        /// عرض التيبل
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult LoadTable()
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
            var data = blPaymentStatus.GetPaymentStatustaghelper().Select(p => new
            {
                PaymentStatusID = p.PaymentStatusID,
                PaymentStatusNameAR = p.PaymentStatusNameAR,
                PaymentStatusNameEN = p.PaymentStatusNameEN,
                IsEnable = p.IsEnable,
                PaymentStatusName = Utility.CurrentLanguageCode == "ar" ? p.PaymentStatusNameAR : p.PaymentStatusNameEN,
                IsEnableString = Utility.CurrentLanguageCode == "ar" ? (p.IsEnable ? "مفعل" : "غير مفعل") : (p.IsEnable ? "Enabled" : "Disabled"),
            }).ToList();
            if (Request.Form.ContainsKey("sSearch"))
            {
                string sea = Request.Form["sSearch"];
                if (!string.IsNullOrEmpty(sea))
                {
                    data = data.Where(x => x.PaymentStatusNameAR.ToLower().Contains(sea.ToLower()) || x.PaymentStatusNameEN.ToLower().Contains(sea.ToLower())
                    || x.IsEnableString.ToLower().Contains(sea.ToLower())).ToList();
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
        [CustomeAuthoriz((int)ControllerEnum.PaymentStatus, (int)ActionEnum.Insert)]
        [HttpPost]
        public JsonResult Index(PaymentStatusViewModel model)
        {
            try
            {
                #region Vaildation
                if (string.IsNullOrWhiteSpace(model.PaymentStatusNameAR))
                {
                    result.Message = Homemade.BLL.Resources.HomemadeErrorMessages.ArNameRequierd;
                    result.ResultType = ResultMessageType.error.ToString();
                    return Json(result);
                }
                if (string.IsNullOrWhiteSpace(model.PaymentStatusNameEN))
                {
                    result.Message = Homemade.BLL.Resources.HomemadeErrorMessages.EnNameRequierd;
                    result.ResultType = ResultMessageType.error.ToString();
                    return Json(result);
                }
                if (blPaymentStatus.IsExist(model.PaymentStatusID, model.PaymentStatusNameAR, Language.Arabic))
                {
                    result.ResultType = ResultMessageType.error.ToString();
                    result.Message = Resources.Homemade.NameIsExists;
                    return Json(new { result = result });


                }
                if (blPaymentStatus.IsExist(model.PaymentStatusID, model.PaymentStatusNameEN, Language.English))
                {
                    result.ResultType = ResultMessageType.error.ToString();
                    result.Message = Resources.Homemade.NameEnIsExists;
                    return Json(new { result = result });

                }
                #endregion
                #region SaveData
                int PaymentStatusID = 0;
                string OperationType = "add";

                bool IsSuccess;
                if (model.PaymentStatusID == 0)
                {
                    #region AddPaymentStatus
                    model.UserId = User.GetUserIdInt();
                    IsSuccess = blPaymentStatus.Insert(model, out PaymentStatusID);
                    #endregion
                }
                else
                {
                    #region UpdatePaymentStatus
                    model.UpdateDate = _bLGeneral.GetDateTimeNow();
                    model.UserUpdate = User.GetUserIdInt();
                    IsSuccess = blPaymentStatus.Update(model);
                    OperationType = "update";
                    #endregion
                }
                if (IsSuccess)
                {
                    result.ResultType = ResultMessageType.success.ToString();
                    result.Message = Resources.Homemade.SuccessSaveMessage;
                }
                else
                {
                    result.ResultType = ResultMessageType.error.ToString();
                    result.Message = Resources.Homemade.FailSaveMessage;
                }
                #endregion
                return Json(new { result, model.PaymentStatusNameAR, model.PaymentStatusNameEN, PaymentStatusID, OperationType });
            }
            catch (System.Exception ex)
            {
                result.ResultType = ResultMessageType.error.ToString();
                result.Message = ex.Message.ToString();
                return Json(result);
            }
        }
        [CustomeAuthoriz((int)ControllerEnum.PaymentStatus, (int)ActionEnum.Delete)]
        [HttpPost]
        public JsonResult Delete(int id)
        {
            var result = false;
            try
            {
                result = blPaymentStatus.Delete(id, User.GetUserIdInt());
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
        [CustomeAuthoriz((int)ControllerEnum.PaymentStatus, (int)ActionEnum.Update)]
        [HttpPost]
        public JsonResult ChangeStatue(int id)
        {
            var result = false;
            try
            {
                result = blPaymentStatus.ChangeStatus(id, User.GetUserIdInt());
            }
            catch (Exception ex)
            {
            }
            ResultMessage rResultMessage = result ?
                new ResultMessage { Message = Resources.Homemade.SuccessChangeMessage, ResultType = ResultMessageType.success.ToString() }
                :
                new ResultMessage { Message = Resources.Homemade.FailChangeStatueMessage, ResultType = ResultMessageType.error.ToString() };
            return Json(rResultMessage);

        }
        #endregion
        #region Helper
        public JsonResult GetAllPaymentStatus()
        {
            var obj = blPaymentStatus.GetAllPaymentStatus().Select(x => x.PaymentStatusID.ToString()).ToList();
            return Json(obj);
        }

        #endregion
    }
}
