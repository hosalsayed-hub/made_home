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
    public class PaymentWayController : Controller
    {
        #region Declarations
        private readonly BlPaymentWay blPaymentWay;
        private readonly ResultMessage result;
        private readonly BLGeneral _bLGeneral;
        public PaymentWayController(BlPaymentWay blPaymentWay, ResultMessage result, BLGeneral bLGeneral)
        {
            _bLGeneral = bLGeneral;
            this.blPaymentWay = blPaymentWay;
            this.result = result;
        }
        #endregion
        #region View
        [CustomeAuthoriz((int)ControllerEnum.PaymentWay, (int)ActionEnum.View)]
        public IActionResult Index() => View(new PaymentWayViewModel());
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
            var data = blPaymentWay.GetPaymentWaytaghelper().Select(p => new
            {
                PaymentWayID = p.PaymentWayID,
                PaymentWayNameAR = p.PaymentWayNameAR,
                PaymentWayNameEN = p.PaymentWayNameEN,
                IsEnable = p.IsEnable,
                PaymentWayName = Utility.CurrentLanguageCode == "ar" ? p.PaymentWayNameAR : p.PaymentWayNameEN,
                IsEnableString = Utility.CurrentLanguageCode == "ar" ? (p.IsEnable ? "مفعل" : "غير مفعل") : (p.IsEnable ? "Enabled" : "Disabled"),
            }).ToList();
            if (Request.Form.ContainsKey("sSearch"))
            {
                string sea = Request.Form["sSearch"];
                if (!string.IsNullOrEmpty(sea))
                {
                    data = data.Where(x => x.PaymentWayNameAR.ToLower().Contains(sea.ToLower()) || x.PaymentWayNameEN.ToLower().Contains(sea.ToLower())
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
        // حفظ الطريقة الدفع
        [CustomeAuthoriz((int)ControllerEnum.PaymentWay, (int)ActionEnum.Insert)]
        [HttpPost]
        public JsonResult Index(PaymentWayViewModel model)
        {
            try
            {
                #region Vaildation
                if (string.IsNullOrWhiteSpace(model.PaymentWayNameAR))
                {
                    result.Message = Homemade.BLL.Resources.HomemadeErrorMessages.ArNameRequierd;
                    result.ResultType = ResultMessageType.error.ToString();
                    return Json(result);
                }
                if (string.IsNullOrWhiteSpace(model.PaymentWayNameEN))
                {
                    result.Message = Homemade.BLL.Resources.HomemadeErrorMessages.EnNameRequierd;
                    result.ResultType = ResultMessageType.error.ToString();
                    return Json(result);
                }
                if (blPaymentWay.IsExist(model.PaymentWayID, model.PaymentWayNameAR, Language.Arabic))
                {
                    result.ResultType = ResultMessageType.error.ToString();
                    result.Message = Resources.Homemade.NameIsExists;
                    return Json(new { result = result });


                }
                if (blPaymentWay.IsExist(model.PaymentWayID, model.PaymentWayNameEN, Language.English))
                {
                    result.ResultType = ResultMessageType.error.ToString();
                    result.Message = Resources.Homemade.NameEnIsExists;
                    return Json(new { result = result });

                }
                #endregion
                #region SaveData
                int PaymentWayID = 0;
                string OperationType = "add";

                bool IsSuccess;
                if (model.PaymentWayID == 0)
                {
                    #region AddPaymentWay
                    model.CreationDate = _bLGeneral.GetDateTimeNow();
                    model.UserId = User.GetUserIdInt();
                    IsSuccess = blPaymentWay.Insert(model, out PaymentWayID);
                    #endregion
                }
                else
                {
                    #region UpdatePaymentWay
                    model.UpdateDate = _bLGeneral.GetDateTimeNow();
                    model.UserUpdate = User.GetUserIdInt();
                    IsSuccess = blPaymentWay.Update(model);
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
                return Json(new { result, model.PaymentWayNameAR, model.PaymentWayNameEN, PaymentWayID, OperationType });
            }
            catch (System.Exception ex)
            {
                result.ResultType = ResultMessageType.error.ToString();
                result.Message = ex.Message.ToString();
                return Json(result);
            }
        }
        // حذف الطريقة الدفع
        [CustomeAuthoriz((int)ControllerEnum.PaymentWay, (int)ActionEnum.Delete)]
        [HttpPost]
        public JsonResult Delete(int id)
        {
            var result = false;
            try
            {
                result = blPaymentWay.Delete(id, User.GetUserIdInt());
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
        [CustomeAuthoriz((int)ControllerEnum.PaymentWay, (int)ActionEnum.Update)]
        [HttpPost]
        public JsonResult ChangeStatue(int id)
        {
            var result = false;
            try
            {
                result = blPaymentWay.ChangeStatus(id, User.GetUserIdInt());
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
        public JsonResult GetAllPaymentWay()
        {
            var obj = blPaymentWay.GetAllPaymentWay().Select(x => x.PaymentWayID.ToString()).ToList();
            return Json(obj);
        }

        #endregion
    }
}
