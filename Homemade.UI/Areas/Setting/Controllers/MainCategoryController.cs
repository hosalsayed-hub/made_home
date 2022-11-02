using Homemade.BLL;
using Homemade.BLL.General;
using Homemade.BLL.Setting;
using Homemade.BLL.ViewModel.Setting;
using Homemade.UI.Attributes;
using Homemade.UI.InfraStructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homemade.UI.Areas.Setting.Controllers
{
    public class MainCategoryController : Controller
    {

        #region Decleration
        private readonly BlMainCategory blMainCategory;
        private readonly ResultMessage result;
        private readonly BLGeneral bLGeneral;

        public MainCategoryController(BlMainCategory blMainCategory, ResultMessage result, BLGeneral bLGeneral)
        {
            this.bLGeneral = bLGeneral;
            this.blMainCategory = blMainCategory;
            this.result = result;
        }
        #endregion
        #region View
        [CustomeAuthoriz((int)ControllerEnum.MainCategory, (int)ActionEnum.View)]
        public IActionResult Index() => View(new MainCategoryViewModel());
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
            var data = blMainCategory.GetAllMainCategories().Select(p => new
            {
                id = p.MainCategoryID,
                nameAr = p.NameAR,
                nameEn = p.NameEN,
                p.IsEnable,
                name = Utility.CurrentLanguageCode == "ar" ? p.NameAR : p.NameEN,
                IsEnableString = Utility.CurrentLanguageCode == "ar" ? (p.IsEnable ? "مفعل" : "غير مفعل") : (p.IsEnable ? "Enabled" : "Disabled"),

            }).ToList();
            if (Request.Form.ContainsKey("sSearch"))
            {
                string sea = Request.Form["sSearch"];
                if (!string.IsNullOrEmpty(sea))
                {
                    data = data.Where(x => x.nameAr.ToLower().Contains(sea.ToLower()) || x.nameEn.ToLower().Contains(sea.ToLower())).ToList();
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

        [CustomeAuthoriz((int)ControllerEnum.MainCategory, (int)ActionEnum.Insert)]
        [HttpPost]
        public JsonResult Index(MainCategoryViewModel model)
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


            if (blMainCategory.IsExist(model.MainCategoryID, model.NameAR, Language.Arabic))
            {
                result.ResultType = ResultMessageType.error.ToString();
                result.Message = Resources.Homemade.NameIsExists;
                return Json(new { result });


            }
            if (blMainCategory.IsExist(model.MainCategoryID, model.NameEN, Language.English))

            {
                result.ResultType = ResultMessageType.error.ToString();
                result.Message = Resources.Homemade.NameEnIsExists;
                return Json(new { result });

            }
            #endregion
            #region SaveData
            int MainCategoryID = 0;
            string OperationType = "add";

            bool IsSuccess = false;
            if (model.MainCategoryID == 0)
            {
                #region AddCity
                model.CreateDate = bLGeneral.GetDateTimeNow();
                model.UserId = User.GetUserIdInt();
                try
                {
                    IsSuccess = blMainCategory.Insert(model, out MainCategoryID);
                }
                catch (System.Exception ex)
                {
                    result.ResultType = ResultMessageType.error.ToString();
                    result.Message = ex.Message.ToString();
                }
                #endregion
            }
            else
            {
                #region UpdateCity
                model.UpdateDate = bLGeneral.GetDateTimeNow();
                model.UserUpdate = User.GetUserIdInt();
                try
                {
                    IsSuccess = blMainCategory.Update(model);
                }
                catch (System.Exception ex)
                {
                    result.ResultType = ResultMessageType.error.ToString();
                    result.Message = ex.Message.ToString();
                }
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
            return Json(new { result, model.NameAR, model.NameEN, MainCategoryID, OperationType });
        }
        [CustomeAuthoriz((int)ControllerEnum.MainCategory, (int)ActionEnum.Delete)]
        [HttpPost]
        public JsonResult Delete(int id)
        {
            var result = false;
            try
            {
                result = blMainCategory.Delete(id, User.GetUserIdInt());
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
        [CustomeAuthoriz((int)ControllerEnum.MainCategory, (int)ActionEnum.Update)]
        [HttpPost]
        public JsonResult ChangeStatue(int id)
        {
            var result = false;
            try
            {
                result = blMainCategory.ChangeStatus(id, User.GetUserIdInt());
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
    }
}
