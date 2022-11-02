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
    public class RegionController : Controller
    {
        #region Declarations
        private readonly BlRegion blRegion;
        private readonly ResultMessage result;
        private readonly BLGeneral _bLGeneral;
        private readonly BlRegionCity _blRegionCity;
        public RegionController(BlRegion blRegion, ResultMessage result, BLGeneral bLGeneral, BlRegionCity blRegionCity)
        {
            _bLGeneral = bLGeneral;
            this.blRegion = blRegion;
            this.result = result;
            _blRegionCity = blRegionCity;
        }
        #endregion
        #region View
        [CustomeAuthoriz((int)ControllerEnum.Region, (int)ActionEnum.View)]
        public IActionResult Index() => View(new RegionViewModel());
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
            var data = blRegion.GetRegiontaghelper().Select(p => new
            {

                CountryName = Utility.CurrentLanguageCode == "ar" ? p.CountryNameAR : p.CountryNameEN,
                RegionID = p.RegionID,
                RegionNameAR = p.RegionNameAR,
                RegionNameEN = p.RegionNameEN,
                CountryID = p.CountryID,
                IsEnable = p.IsEnable,
                RegionName = Utility.CurrentLanguageCode == "ar" ? p.RegionNameAR : p.RegionNameEN,
                IsEnableString = Utility.CurrentLanguageCode == "ar" ? (p.IsEnable ? "مفعل" : "غير مفعل") : (p.IsEnable ? "Enabled" : "Disabled"),
            }).ToList();
            if (Request.Form.ContainsKey("sSearch"))
            {
                string sea = Request.Form["sSearch"];
                if (!string.IsNullOrEmpty(sea))
                {
                    data = data.Where(x => x.RegionNameAR.ToLower().Contains(sea.ToLower()) || x.RegionNameEN.ToLower().Contains(sea.ToLower())
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
        [CustomeAuthoriz((int)ControllerEnum.Region, (int)ActionEnum.Insert)]
        [HttpPost]
        public JsonResult Index(RegionViewModel model)
        {
            try
            {
                #region Vaildation
                if (string.IsNullOrWhiteSpace(model.RegionNameAR))
                {
                    result.Message = Homemade.BLL.Resources.HomemadeErrorMessages.ArNameRequierd;
                    result.ResultType = ResultMessageType.error.ToString();
                    return Json(result);
                }
                if (string.IsNullOrWhiteSpace(model.RegionNameEN))
                {
                    result.Message = Homemade.BLL.Resources.HomemadeErrorMessages.EnNameRequierd;
                    result.ResultType = ResultMessageType.error.ToString();
                    return Json(result);
                }
                if (blRegion.IsExist(model.RegionID, model.CountryID, model.RegionNameAR, Language.Arabic))
                {
                    result.ResultType = ResultMessageType.error.ToString();
                    result.Message = Resources.Homemade.NameIsExists;
                    return Json(new { result = result });


                }
                if (blRegion.IsExist(model.RegionID, model.CountryID, model.RegionNameEN, Language.English))
                {
                    result.ResultType = ResultMessageType.error.ToString();
                    result.Message = Resources.Homemade.NameEnIsExists;
                    return Json(new { result = result });

                }
                #endregion
                #region SaveData
                int RegionID = 0;
                string OperationType = "add";

                bool IsSuccess;
                if (model.RegionID == 0)
                {
                    #region AddRegion
                    model.CreationDate = _bLGeneral.GetDateTimeNow();
                    model.UserId = User.GetUserIdInt();
                    IsSuccess = blRegion.Insert(model, out RegionID);
                    #endregion
                }
                else
                {
                    #region UpdateRegion
                    model.UpdateDate = _bLGeneral.GetDateTimeNow();
                    model.UserUpdate = User.GetUserIdInt();
                    IsSuccess = blRegion.Update(model);
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
                return Json(new { result, model.RegionNameAR, model.RegionNameEN, RegionID, OperationType });
            }
            catch (System.Exception ex)
            {
                result.ResultType = ResultMessageType.error.ToString();
                result.Message = ex.Message.ToString();
                return Json(result);
            }
        }
        [CustomeAuthoriz((int)ControllerEnum.Region, (int)ActionEnum.Delete)]
        [HttpPost]
        public JsonResult Delete(int id)
        {
            var result = false;
            try
            {
                if (!blRegion.IsExistCity(id))
                {
                    result = blRegion.Delete(id, User.GetUserIdInt());
                }
                else
                {
                    ResultMessage resultMessage = new ResultMessage { Message = Resources.Homemade.Sorry_you_can_Not_because_there, ResultType = ResultMessageType.error.ToString() };
                    return Json(resultMessage);
                }

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
        [CustomeAuthoriz((int)ControllerEnum.Region, (int)ActionEnum.Update)]
        [HttpPost]
        public JsonResult ChangeStatue(int id)
        {
            var result = false;
            try
            {
                if (!blRegion.IsExistCity(id))
                {
                    result = blRegion.ChangeStatus(id, User.GetUserIdInt());
                }
                else
                {
                    ResultMessage resultMessage = new ResultMessage { Message = Resources.Homemade.Sorry_you_can_Not_because_there, ResultType = ResultMessageType.error.ToString() };
                    return Json(resultMessage);
                }
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
        public JsonResult GetAllListRegionIDBySACountry()
        {
            var obj = blRegion.GetRegionsByCountryID((int)CountryEnum.SA).Select(x => x.RegionID.ToString()).ToList();
            return Json(obj);
        }
        public JsonResult GetAllRegionByCountry(int countryID)
        {
            var obj = blRegion.GetRegionsByCountryID(countryID).Select(p => new { p.RegionID, RegionName = Homemade.UI.InfraStructure.Utility.CurrentLanguageCode == "ar" ? p.RegionNameAR : p.RegionNameEN });
            return Json(obj);
        }
        public JsonResult GetAllRegionBySACountry()
        {
            var obj = blRegion.GetRegionsByCountryID((int)CountryEnum.SA).Select(p => new { p.RegionID, RegionName = Homemade.UI.InfraStructure.Utility.CurrentLanguageCode == "ar" ? p.RegionNameAR : p.RegionNameEN });
            return Json(obj);
        }
        public JsonResult GetAllRegionCity()
        {
            var obj = _blRegionCity.GetRegionCity().Select(p => new { p.RegionCityID, RegionCityName = Homemade.UI.InfraStructure.Utility.CurrentLanguageCode == "ar" ? p.RegionCityNameAR : p.RegionCityNameEN });
            return Json(obj);
        }

        #endregion
    }
}
