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
using Microsoft.Extensions.Configuration;

namespace Homemade.UI.Areas.Setting.Controllers
{
    public class DepartmentsController : Controller
    {
        #region Declarations
        private readonly BlDepartments blDepartments;
        private readonly BlMainCategory _BlMainCategory;
        private readonly ResultMessage result;
        private readonly BLGeneral _bLGeneral;
        private readonly IConfiguration _configuration;
        public DepartmentsController(BlDepartments blDepartments, ResultMessage result, BLGeneral bLGeneral, BlMainCategory blMainCategory, IConfiguration configuration)
        {
            _configuration = configuration;
            _BlMainCategory = blMainCategory;
            _BlMainCategory = blMainCategory;
            _bLGeneral = bLGeneral;
            this.blDepartments = blDepartments;
            this.result = result;
        }
        #endregion
        #region View
        [CustomeAuthoriz((int)ControllerEnum.Departments, (int)ActionEnum.View)]
        public IActionResult Index() => View(new DepartmentsViewModel());
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

            string MainPathView = _configuration["DeptImageView"];
            var data = blDepartments.GetDepartmentstaghelper(Utility.CurrentLanguageCode, MainPathView);
            if (Request.Form.ContainsKey("sSearch"))
            {
                string sea = Request.Form["sSearch"];
                if (!string.IsNullOrEmpty(sea))
                {
                    data = data.Where(x => x.DepartmentsNameAR.ToLower().Contains(sea.ToLower()) || x.DepartmentsNameEN.ToLower().Contains(sea.ToLower())
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
        [CustomeAuthoriz((int)ControllerEnum.Departments, (int)ActionEnum.Insert)]
        [HttpPost]
        public JsonResult Index(DepartmentsViewModel model)
        {
            try
            {
                #region Vaildation
                if (string.IsNullOrWhiteSpace(model.DepartmentsNameAR))
                {
                    result.Message = Homemade.BLL.Resources.HomemadeErrorMessages.ArNameRequierd;
                    result.ResultType = ResultMessageType.error.ToString();
                    return Json(result);
                }
                if (string.IsNullOrWhiteSpace(model.DepartmentsNameEN))
                {
                    result.Message = Homemade.BLL.Resources.HomemadeErrorMessages.EnNameRequierd;
                    result.ResultType = ResultMessageType.error.ToString();
                    return Json(result);
                }
                if (blDepartments.IsExist(model.DepartmentsID, model.DepartmentsNameAR, Language.Arabic))
                {
                    result.ResultType = ResultMessageType.error.ToString();
                    result.Message = Resources.Homemade.NameIsExists;
                    return Json(new { result = result });
                }
                if (model.MainCategoryID == 0)
                {
                    result.Message = Homemade.BLL.Resources.HomemadeErrorMessages.MainCategoryRequired;
                    result.ResultType = ResultMessageType.error.ToString();
                    return Json(result);
                }

                if (blDepartments.IsExist(model.DepartmentsID, model.DepartmentsNameEN, Language.English))
                {
                    result.ResultType = ResultMessageType.error.ToString();
                    result.Message = Resources.Homemade.NameEnIsExists;
                    return Json(new { result = result });

                }
                #endregion
                #region SaveData
                int DepartmentsID = 0;
                string OperationType = "add";
                string MainPath = _configuration["DeptImageSave"];
                bool IsSuccess;
                if (!string.IsNullOrEmpty(model.SiteImage))
                {
                    string FileName = Guid.NewGuid() + ".png";
                    _bLGeneral.SaveImage(new BLGeneral.SaveImageModel
                    {
                        base64 = model.SiteImage,
                        key = "",
                        fileName = FileName,
                        mainPath = MainPath
                    });
                    model.SiteImage = FileName;
                }
                if (!string.IsNullOrEmpty(model.Image))
                {
                    string FileName = Guid.NewGuid() + ".png";
                    _bLGeneral.SaveImage(new BLGeneral.SaveImageModel
                    {
                        base64 = model.Image,
                        key = "",
                        fileName = FileName,
                        mainPath = MainPath
                    });
                    model.Image = FileName;
                }

                if (model.DepartmentsID == 0)
                {
                    #region AddDepartments
                    model.CreateDate = _bLGeneral.GetDateTimeNow();
                    model.UserId = User.GetUserIdInt();
                    IsSuccess = blDepartments.Insert(model, out DepartmentsID);
                    #endregion
                }
                else
                {
                    #region UpdateDepartments
                    model.UpdateDate = _bLGeneral.GetDateTimeNow();
                    model.UserUpdate = User.GetUserIdInt();
                    IsSuccess = blDepartments.Update(model);
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
                return Json(new { result, model.DepartmentsNameAR, model.DepartmentsNameEN, DepartmentsID, OperationType });
            }
            catch (System.Exception ex)
            {
                result.ResultType = ResultMessageType.error.ToString();
                result.Message = ex.Message.ToString();
                return Json(result);
            }
        }
        // حذف الدولة
        [CustomeAuthoriz((int)ControllerEnum.Departments, (int)ActionEnum.Delete)]
        [HttpPost]
        public JsonResult Delete(int id)
        {
            var result = false;
            try
            {
                result = blDepartments.Delete(id, User.GetUserIdInt());
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
        [CustomeAuthoriz((int)ControllerEnum.Departments, (int)ActionEnum.Update)]
        [HttpPost]
        public JsonResult ChangeStatue(int id)
        {
            var result = false;
            try
            {
                result = blDepartments.ChangeStatus(id, User.GetUserIdInt());
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
        public JsonResult GetAllDepartments()
        {
            var obj = blDepartments.GetAllDepartments().Select(p => new { p.DepartmentsID, DepartmentsName = Homemade.UI.InfraStructure.Utility.CurrentLanguageCode == "ar" ? p.NameAR : p.NameEN }).ToList();
            return Json(obj);
        }

        public JsonResult GetAllMainCategories()
        {
            var obj = _BlMainCategory.GetAllMainCategories().Select(p => new { p.MainCategoryID, MainCategoryName = Homemade.UI.InfraStructure.Utility.CurrentLanguageCode == "ar" ? p.NameAR : p.NameEN }).ToList();
            return Json(obj);
        }
        #endregion
    }
}
