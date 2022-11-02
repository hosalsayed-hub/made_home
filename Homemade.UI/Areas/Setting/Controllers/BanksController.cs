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
    public class BanksController : Controller
    {
        #region Declarations
        private readonly BlBanks blBanks;
        private readonly ResultMessage result;
        private readonly BLGeneral _bLGeneral;
        public BanksController(BlBanks blBanks, ResultMessage result, BLGeneral bLGeneral)
        {
            _bLGeneral = bLGeneral;
            this.blBanks = blBanks;
            this.result = result;
        }
        #endregion
        #region View
        [CustomeAuthoriz((int)ControllerEnum.Banks, (int)ActionEnum.View)]
        public IActionResult Index() => View(new BanksViewModel());
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
            var data = blBanks.GetBankstaghelper().Select(p => new
            {
                BanksID = p.BanksID,
                BanksNameAR = p.BanksNameAR,
                BanksNameEN = p.BanksNameEN,
                IsEnable = p.IsEnable,
                BanksName = Utility.CurrentLanguageCode == "ar" ? p.BanksNameAR : p.BanksNameEN,
                IsEnableString = Utility.CurrentLanguageCode == "ar" ? (p.IsEnable ? "مفعل" : "غير مفعل") : (p.IsEnable ? "Enabled" : "Disabled"),
            }).ToList();
            if (Request.Form.ContainsKey("sSearch"))
            {
                string sea = Request.Form["sSearch"];
                if (!string.IsNullOrEmpty(sea))
                {
                    data = data.Where(x => x.BanksNameAR.ToLower().Contains(sea.ToLower()) || x.BanksNameEN.ToLower().Contains(sea.ToLower())
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
        [CustomeAuthoriz((int)ControllerEnum.Banks, (int)ActionEnum.Insert)]
        [HttpPost]
        public JsonResult Index(BanksViewModel model)
        {
            try
            {
                #region Vaildation
                if (string.IsNullOrWhiteSpace(model.BanksNameAR))
                {
                    result.Message = Homemade.BLL.Resources.HomemadeErrorMessages.ArNameRequierd;
                    result.ResultType = ResultMessageType.error.ToString();
                    return Json(result);
                }
                if (string.IsNullOrWhiteSpace(model.BanksNameEN))
                {
                    result.Message = Homemade.BLL.Resources.HomemadeErrorMessages.EnNameRequierd;
                    result.ResultType = ResultMessageType.error.ToString();
                    return Json(result);
                }
                if (blBanks.IsExist(model.BanksID, model.BanksNameAR, Language.Arabic))
                {
                    result.ResultType = ResultMessageType.error.ToString();
                    result.Message = Resources.Homemade.NameIsExists;
                    return Json(new { result = result });


                }
                if (blBanks.IsExist(model.BanksID, model.BanksNameEN, Language.English))
                {
                    result.ResultType = ResultMessageType.error.ToString();
                    result.Message = Resources.Homemade.NameEnIsExists;
                    return Json(new { result = result });

                }
                #endregion
                #region SaveData
                int BanksID = 0;
                string OperationType = "add";

                bool IsSuccess;
                if (model.BanksID == 0)
                {
                    #region AddBanks
                    model.CreationDate = _bLGeneral.GetDateTimeNow();
                    model.UserId = User.GetUserIdInt();
                    IsSuccess = blBanks.Insert(model, out BanksID);
                    #endregion
                }
                else
                {
                    #region UpdateBanks
                    model.UpdateDate = _bLGeneral.GetDateTimeNow();
                    model.UserUpdate = User.GetUserIdInt();
                    IsSuccess = blBanks.Update(model);
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
                return Json(new { result, model.BanksNameAR, model.BanksNameEN, BanksID, OperationType });
            }
            catch (System.Exception ex)
            {
                result.ResultType = ResultMessageType.error.ToString();
                result.Message = ex.Message.ToString();
                return Json(result);
            }
        }
        [CustomeAuthoriz((int)ControllerEnum.Banks, (int)ActionEnum.Delete)]
        [HttpPost]
        public JsonResult Delete(int id)
        {
            var result = false;
            try
            {
                result = blBanks.Delete(id, User.GetUserIdInt());
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
        [CustomeAuthoriz((int)ControllerEnum.Banks, (int)ActionEnum.Update)]
        [HttpPost]
        public JsonResult ChangeStatue(int id)
        {
            var result = false;
            try
            {
                result = blBanks.ChangeStatus(id, User.GetUserIdInt());
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
        public JsonResult GetAllBanks()
        {
            var obj = blBanks.GetAllBanks().Select(x => x.BanksID.ToString()).ToList();
            return Json(obj);
        }
        [HttpPost]
        public JsonResult GetAllBanksForPayment()
        {
            var obj = blBanks.GetAllBanks().Select(p => new { p.BanksID, BankName = Homemade.UI.InfraStructure.Utility.CurrentLanguageCode == "ar" ? p.NameAR : p.NameEN }).ToList();
            return Json(obj);
        }
        #endregion
    }
}
