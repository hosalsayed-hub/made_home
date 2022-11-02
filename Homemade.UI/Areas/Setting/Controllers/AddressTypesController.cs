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
    public class AddressTypesController : Controller
    {
        #region Declarations
        private readonly BlAddressTypes blAddressTypes;
        private readonly ResultMessage result;
        private readonly BLGeneral _bLGeneral;
        public AddressTypesController(BlAddressTypes blAddressTypes, ResultMessage result, BLGeneral bLGeneral)
        {
            _bLGeneral = bLGeneral;
            this.blAddressTypes = blAddressTypes;
            this.result = result;
        }
        #endregion
        #region View
        [CustomeAuthoriz((int)ControllerEnum.AddressTypes, (int)ActionEnum.View)]
        public IActionResult Index() => View(new AddressTypesViewModel());
        #endregion
        #region Actions
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
            var data = blAddressTypes.GetAddressTypestaghelper().Select(p => new
            {    
                AddressTypesID = p.AddressTypesID,
                NameAR = p.NameAR,
                NameEN = p.NameEN,
                IsEnable = p.IsEnable,
                AddressTypesName = Utility.CurrentLanguageCode == "ar" ? p.NameAR : p.NameEN,
                IsEnableString = Utility.CurrentLanguageCode == "ar" ? (p.IsEnable ? "مفعل" : "غير مفعل") : (p.IsEnable ? "Enabled" : "Disabled"),
            }).ToList();
            if (Request.Form.ContainsKey("sSearch"))
            {
                string sea = Request.Form["sSearch"];
                if (!string.IsNullOrEmpty(sea))
                {
                    data = data.Where(x => x.NameAR.ToLower().Contains(sea.ToLower()) || x.NameEN.ToLower().Contains(sea.ToLower())
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
        [CustomeAuthoriz((int)ControllerEnum.AddressTypes, (int)ActionEnum.Insert)]
        [HttpPost]
        public JsonResult Index(AddressTypesViewModel model)
        {
            try
            {
                #region Vaildation
                if (string.IsNullOrWhiteSpace(model.NameAR))
                {
                    result.Message = Homemade.BLL.Resources.HomemadeErrorMessages.ArNameRequierd;
                    result.ResultType = ResultMessageType.error.ToString();
                    return Json(result);
                }
                if (string.IsNullOrWhiteSpace(model.NameEN))
                {
                    result.Message = Homemade.BLL.Resources.HomemadeErrorMessages.EnNameRequierd;
                    result.ResultType = ResultMessageType.error.ToString();
                    return Json(result);
                }
                if (blAddressTypes.IsExist(model.AddressTypesID, model.NameAR, Language.Arabic))
                {
                    result.ResultType = ResultMessageType.error.ToString();
                    result.Message = Resources.Homemade.NameIsExists;
                    return Json(new { result = result });


                }
                if (blAddressTypes.IsExist(model.AddressTypesID , model.NameEN, Language.English))
                {
                    result.ResultType = ResultMessageType.error.ToString();
                    result.Message = Resources.Homemade.NameEnIsExists;
                    return Json(new { result = result });

                }
                #endregion
                #region SaveData
                int AddressTypesID = 0;
                string OperationType = "add";

                bool IsSuccess;
                if (model.AddressTypesID == 0)
                {
                    #region AddAddressTypes
                    model.CreateDate = _bLGeneral.GetDateTimeNow();
                    model.UserId = User.GetUserIdInt();
                    IsSuccess = blAddressTypes.Insert(model, out AddressTypesID);
                    #endregion
                }
                else
                {
                    #region UpdateAddressTypes
                    model.UpdateDate = _bLGeneral.GetDateTimeNow();
                    model.UserUpdate = User.GetUserIdInt();
                    IsSuccess = blAddressTypes.Update(model);
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
                return Json(new { result, model.NameAR, model.NameEN, AddressTypesID, OperationType });
            }
            catch (System.Exception ex)
            {
                result.ResultType = ResultMessageType.error.ToString();
                result.Message = ex.Message.ToString();
                return Json(result);
            }
        }
        [CustomeAuthoriz((int)ControllerEnum.AddressTypes, (int)ActionEnum.Delete)]
        [HttpPost]
        public JsonResult Delete(int id)
        {
            var result = false;
            try
            {
                result = blAddressTypes.Delete(id, User.GetUserIdInt());
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
        [CustomeAuthoriz((int)ControllerEnum.AddressTypes, (int)ActionEnum.Update)]
        [HttpPost]
        public JsonResult ChangeStatue(int id)
        {
            var result = false;
            try
            {
                result = blAddressTypes.ChangeStatus(id, User.GetUserIdInt());
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
        public JsonResult GetAllAddressTypes()
        {
            var obj = blAddressTypes.GetAllAddressTypes().Select(x => x.AddressTypesID.ToString()).ToList();
            return Json(obj);
        }
        public JsonResult GetAddressTypes()
        {
            var obj = blAddressTypes.GetAllAddressTypes().Select(p => new { p.AddressTypesID, AddressTypesName = Utility.CurrentLanguageCode == "ar" ? p.NameAR : p.NameEN });
            return Json(obj);
        }

        #endregion
    }
}
