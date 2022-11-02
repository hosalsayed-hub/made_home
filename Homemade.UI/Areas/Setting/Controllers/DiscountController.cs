using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Homemade.BLL;
using Homemade.BLL.Setting;
using Homemade.BLL.ViewModel.Setting;
using Homemade.UI.Attributes;
using Homemade.UI.InfraStructure;
using Microsoft.AspNetCore.Mvc;

namespace Homemade.UI.Areas.Setting.Controllers
{
    public class DiscountController : Controller
    {
        #region Declarations
        private readonly BlDiscount _blDiscount;
        private readonly ResultMessage _result;
        public DiscountController(BlDiscount blDiscount, ResultMessage result)
        {
            _blDiscount = blDiscount;
            _result = result;
        }
        #endregion
        #region View
        // صفحة عرض الخصم
        [CustomeAuthoriz((int)ControllerEnum.Discount, (int)ActionEnum.View)]
        public IActionResult Index() => View();
        #endregion
        #region Actions

        /// <summary>
        /// عرض الخصم
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
            var data = _blDiscount.GetAllDiscountViewModel();
            if (Request.Form.ContainsKey("sSearch"))
            {
                string sea = Request.Form["sSearch"];
                if (!string.IsNullOrEmpty(sea))
                {
                    data = data.Where(x => x.DiscountValue.ToString().Contains(sea));
                }
            }
            var Responce = data.AsQueryable();
            Responce = Responce.Skip(Convert.ToInt32(pageno - 1) * displayLength);
            var takenData = Responce.Take(((Convert.ToInt32(pageno) * displayLength)) - (Convert.ToInt32(pageno - 1) * displayLength))
                .Select(x => new { x.DiscountID, x.DiscountGuid, x.DiscountValue, x.DiscountTypeID,
                    DiscountTypeName = 
                    (DiscountSettingEnum)x.DiscountTypeID == DiscountSettingEnum.Discount ? 
              
                    Resources.Homemade.Discount : ((DiscountSettingEnum)x.DiscountTypeID == DiscountSettingEnum.Delivery_Vat ? Resources.Homemade.DeliveryVat : Resources.Homemade.Vat)  });
            int totalCount = data.Count();
            return Json(new
            {
                iTotalRecords = totalCount,
                iTotalDisplayRecords = totalCount,
                aaData = takenData
            });
        }
        // حفظ اللون
        [CustomeAuthoriz((int)ControllerEnum.Discount, (int)ActionEnum.Insert)]
        [HttpPost]
        public JsonResult Index(DiscountViewModel model)
        {
            try
            {
                #region SaveData
                if (_blDiscount.IsExist(model.DiscountTypeID, model.DiscountID))
                {
                    _result.ResultType = ResultMessageType.error.ToString();
                    _result.Message = Resources.Homemade.DiscountTypeIsExists;
                    return Json(new { result = _result });
                }

                int DiscountID = 0;
                string OperationType = "add";

                bool IsSuccess;
                if (model.DiscountID == 0)
                {
                    #region AddDiscount
                    IsSuccess = _blDiscount.Insert(model, out DiscountID, User.GetUserIdInt());
                    #endregion
                }
                else
                {
                    #region UpdateDiscount
                    IsSuccess = _blDiscount.Update(model, User.GetUserIdInt());
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

                return Json(new { result = _result, model.DiscountTypeID, model.DiscountValue, DiscountID, OperationType });
            }
            catch (System.Exception ex)
            {
                _result.ResultType = ResultMessageType.error.ToString();
                _result.Message = ex.Message.ToString();
                return Json(_result);
            }
        }
        [CustomeAuthoriz((int)ControllerEnum.Discount, (int)ActionEnum.Delete)]
        // حذف اللون
        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                if (_blDiscount.Delete(id, User.GetUserIdInt()))
                {
                    return Json(new ResultMessage { Message = Resources.Homemade.SuccessDeleteMessage, ResultType = ResultMessageType.success.ToString() });
                }

                return Json(new ResultMessage { Message = Resources.Homemade.FailDeleteMessage, ResultType = ResultMessageType.error.ToString() });
            }
            catch (Exception ex)
            {
                return Json(new ResultMessage { Message = Resources.Homemade.FailDeleteMessage, ResultType = ResultMessageType.error.ToString() });

            }
        }
        #endregion
    }
}
