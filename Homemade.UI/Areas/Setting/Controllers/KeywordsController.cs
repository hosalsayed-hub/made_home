using Homemade.BLL.Setting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Homemade.BLL;
using Homemade.BLL.General;
using Homemade.BLL.ViewModel.Setting;
using Homemade.UI.Attributes;
using Homemade.UI.InfraStructure;

namespace Homemade.UI.Areas.Setting.Controllers
{
    public class KeyWordsController : Controller
    {
        #region Declarations
        private readonly BlKeyWords blKeyWords;
        private readonly BlDepartments _BlDepartment;
        private readonly ResultMessage result;
        private readonly BLGeneral _bLGeneral;
        public KeyWordsController(BlKeyWords blKeyWords, ResultMessage result, BLGeneral bLGeneral, BlDepartments blDepartment)
        {
            _BlDepartment = blDepartment;
            _bLGeneral = bLGeneral;
            this.blKeyWords = blKeyWords;
            this.result = result;
        }
        #endregion
        #region View
        [CustomeAuthoriz((int)ControllerEnum.KeyWords, (int)ActionEnum.View)]
        public IActionResult Index() => View(new KeyWordsViewModel());
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

            var data = blKeyWords.GetKeyWordstaghelper(Utility.CurrentLanguageCode);
            if (Request.Form.ContainsKey("sSearch"))
            {
                string sea = Request.Form["sSearch"];
                if (!string.IsNullOrEmpty(sea))
                {
                    data = data.Where(x => x.KeyWordsNameAR.ToLower().Contains(sea.ToLower()) || x.KeyWordsNameEN.ToLower().Contains(sea.ToLower())
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
        [CustomeAuthoriz((int)ControllerEnum.KeyWords, (int)ActionEnum.Insert)]
        [HttpPost]
        public JsonResult Index(KeyWordsViewModel model)
        {
            try
            {
                #region Vaildation
                if (string.IsNullOrWhiteSpace(model.KeyWordsNameAR))
                {
                    result.Message = Homemade.BLL.Resources.HomemadeErrorMessages.ArNameRequierd;
                    result.ResultType = ResultMessageType.error.ToString();
                    return Json(result);
                }
                if (string.IsNullOrWhiteSpace(model.KeyWordsNameEN))
                {
                    result.Message = Homemade.BLL.Resources.HomemadeErrorMessages.EnNameRequierd;
                    result.ResultType = ResultMessageType.error.ToString();
                    return Json(result);
                }
                if (blKeyWords.IsExist(model.KeyWordsID, model.KeyWordsNameAR, Language.Arabic))
                {
                    result.ResultType = ResultMessageType.error.ToString();
                    result.Message = Resources.Homemade.NameIsExists;
                    return Json(new { result = result });
                }
                if (model.DepartmentID == 0)
                {
                    result.Message = Homemade.BLL.Resources.HomemadeErrorMessages.DepartmentRequierd;
                    result.ResultType = ResultMessageType.error.ToString();
                    return Json(result);
                }

                if (blKeyWords.IsExist(model.KeyWordsID, model.KeyWordsNameEN, Language.English))
                {
                    result.ResultType = ResultMessageType.error.ToString();
                    result.Message = Resources.Homemade.NameEnIsExists;
                    return Json(new { result = result });

                }
                #endregion
                #region SaveData
                int KeyWordsID = 0;
                string OperationType = "add";

                bool IsSuccess;
                if (model.KeyWordsID == 0)
                {
                    #region AddKeyWords
                    model.CreateDate = _bLGeneral.GetDateTimeNow();
                    model.UserId = User.GetUserIdInt();
                    IsSuccess = blKeyWords.Insert(model, out KeyWordsID);
                    #endregion
                }
                else
                {
                    #region UpdateKeyWords
                    model.UpdateDate = _bLGeneral.GetDateTimeNow();
                    model.UserUpdate = User.GetUserIdInt();
                    IsSuccess = blKeyWords.Update(model);
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
                return Json(new { result, model.KeyWordsNameAR, model.KeyWordsNameEN, KeyWordsID, OperationType });
            }
            catch (System.Exception ex)
            {

                result.ResultType = ResultMessageType.error.ToString();
                result.Message = ex.Message.ToString();
                return Json(result);
            }
        }
        [CustomeAuthoriz((int)ControllerEnum.KeyWords, (int)ActionEnum.Delete)]
        [HttpPost]
        public JsonResult Delete(int id)
        {
            var result = false;
            try
            {
                result = blKeyWords.Delete(id, User.GetUserIdInt());
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
        [CustomeAuthoriz((int)ControllerEnum.KeyWords, (int)ActionEnum.Update)]
        [HttpPost]
        public JsonResult ChangeStatue(int id)
        {
            var result = false;
            try
            {
                result = blKeyWords.ChangeStatus(id, User.GetUserIdInt());
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
        public JsonResult GetAllKeyWords()
        {
            var obj = blKeyWords.GetAllKeyWords().Select(p => new { p.KeyWordsID, KeyWordsName = Homemade.UI.InfraStructure.Utility.CurrentLanguageCode == "ar" ? p.NameAR : p.NameEN }).ToList();
            return Json(obj);
        }

        public JsonResult GetAllDepartments()
        {
            var obj = _BlDepartment.GetAllDepartments().Select(p => new { p.DepartmentsID, DepartmentName = Homemade.UI.InfraStructure.Utility.CurrentLanguageCode == "ar" ? p.NameAR : p.NameEN }).ToList();
            return Json(obj);
        }
        public JsonResult GetKeyWordsByDepartmentList(string[] listDepartmentID)
        {
            var listOfDepartments = blKeyWords.GetAllKeyWordssByListDepartment(listDepartmentID).Select(p => new { p.KeyWordsID, KeyWordsName = Utility.CurrentLanguageCode == "ar" ? p.NameAR : p.NameEN });
            return Json(listOfDepartments);
        }
        #endregion
    }
}
