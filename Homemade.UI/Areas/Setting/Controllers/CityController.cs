using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Homemade.BLL;
using Homemade.BLL.General;
using Homemade.BLL.Setting;
using Homemade.BLL.ViewModel.Setting;
using Homemade.UI.Attributes;
using Homemade.UI.InfraStructure;

namespace Homemade.UI.Areas.Setting.Controllers
{
    public class CityController : Controller
    {
        #region Declarations
        private readonly BlCity blCity;
        private readonly ResultMessage result;
        private readonly BLGeneral bLGeneral;
        private readonly BlCountry _blCountry;

        public CityController(BlCity blCity, ResultMessage result, BLGeneral bLGeneral, BlCountry blCountry)
        {
            _blCountry = blCountry;
            this.bLGeneral = bLGeneral;
            this.blCity = blCity;
            this.result = result;
        }
        #endregion
        #region View
        // صفحة عرض المدن
        [CustomeAuthoriz((int)ControllerEnum.City, (int)ActionEnum.View)]
        public IActionResult Index() => View(new CityViewModel());
        #endregion
        #region Actions

        /// <summary>
        /// عرض المدن
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
            var data = blCity.GetCities().Select(p => new
            {
                id = p.CityID,
                nameAr = p.CityNameAR,
                nameEn = p.CityNameEN,
                countryID = p.CountryID,
                countryName = Utility.CurrentLanguageCode == "ar" ? p.CountryNameAR : p.CountryNameEN,
                regionID = p.RegionID,
                regionName = Utility.CurrentLanguageCode == "ar" ? p.RegionNameAR : p.RegionNameEN,
                p.Lat,
                p.Lng,
                p.IsEnable,
                name = Utility.CurrentLanguageCode == "ar" ? p.CityNameAR : p.CityNameEN,
                IsEnableString = Utility.CurrentLanguageCode == "ar" ? (p.IsEnable ? "مفعل" : "غير مفعل") : (p.IsEnable ? "Enabled" : "Disabled"),

            }).ToList();
            if (Request.Form.ContainsKey("sSearch"))
            {
                string sea = Request.Form["sSearch"];
                if (!string.IsNullOrEmpty(sea))
                {
                    data = data.Where(x => x.regionName.ToLower().Contains(sea.ToLower()) || x.nameAr.ToLower().Contains(sea.ToLower()) || x.nameEn.ToLower().Contains(sea.ToLower()) || x.countryName.ToLower().Contains(sea.ToLower())).ToList();
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

        // حفظ المدينة
        [CustomeAuthoriz((int)ControllerEnum.City, (int)ActionEnum.Insert)]
        [HttpPost]
        public JsonResult Index(CityViewModel model)
        {
            #region Vaildation
            if (string.IsNullOrWhiteSpace(model.CityNameAR))
            {
                result.Message = Homemade.BLL.Resources.HomemadeErrorMessages.ArNameRequierd;
                result.ResultType = ResultMessageType.error.ToString();
                return Json(result);
            }
            if (string.IsNullOrWhiteSpace(model.CityNameEN))
            {
                result.Message = Homemade.BLL.Resources.HomemadeErrorMessages.EnNameRequierd;
                result.ResultType = ResultMessageType.error.ToString();
                return Json(result);
            }
            if (model.RegionID == 0)
            {
                result.Message = Homemade.BLL.Resources.HomemadeErrorMessages.RegionRequierd;
                result.ResultType = ResultMessageType.error.ToString();
                return Json(result);
            }
            if (model.CountryID == 0)
            {
                result.Message = Homemade.BLL.Resources.HomemadeErrorMessages.CountryRequierd;
                result.ResultType = ResultMessageType.error.ToString();
                return Json(result);
            }

            if (blCity.IsExist(model.CityID, model.RegionID, model.CityNameAR, Language.Arabic))
            {
                result.ResultType = ResultMessageType.error.ToString();
                result.Message = Resources.Homemade.NameIsExists;
                return Json(new { result });


            }
            if (blCity.IsExist(model.CityID, model.RegionID, model.CityNameEN, Language.English))
            {
                result.ResultType = ResultMessageType.error.ToString();
                result.Message = Resources.Homemade.NameEnIsExists;
                return Json(new { result });

            }
            #endregion
            #region SaveData
            int CityID = 0;
            string OperationType = "add";

            bool IsSuccess = false;
            if (model.CityID == 0)
            {
                #region AddCity
                model.CreateDate = bLGeneral.GetDateTimeNow();
                model.UserId = User.GetUserIdInt();
                try
                {
                    IsSuccess = blCity.Insert(model, out CityID);
                }
                catch (System.Exception ex)
                {
                    //  _blErrorLog.Insert("UI", "Setting", "City", "Index", ex.Message, ex.InnerException?.Message);

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
                    IsSuccess = blCity.Update(model);
                }
                catch (System.Exception ex)
                {
                    //   _blErrorLog.Insert("UI", "Setting", "City", "Index", ex.Message, ex.InnerException?.Message);

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
            return Json(new { result, model.CityNameAR, model.CityNameEN, CityID, OperationType });
        }
        // حذف المدينة
        [CustomeAuthoriz((int)ControllerEnum.City, (int)ActionEnum.Delete)]
        [HttpPost]
        public JsonResult Delete(int id)
        {
            var result = false;
            try
            {
                result = blCity.Delete(id, User.GetUserIdInt());
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
        [CustomeAuthoriz((int)ControllerEnum.City, (int)ActionEnum.Update)]
        [HttpPost]
        public JsonResult ChangeStatue(int id)
        {
            var result = false;
            try
            {
                result = blCity.ChangeStatus(id, User.GetUserIdInt());
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

        [HttpPost]
        public JsonResult GetAllCities()
        {
            var obj = blCity.GetCities().Select(p => new { p.CityID, CityName = Homemade.UI.InfraStructure.Utility.CurrentLanguageCode == "ar" ? p.CityNameAR : p.CityNameEN }).ToList();
            return Json(obj);
        }
        [HttpPost]
        public JsonResult GetAllCountries()
        {
            var obj = _blCountry.GetAllCountryByOperators().Select(p => new { p.CountryID, CountryName = Homemade.UI.InfraStructure.Utility.CurrentLanguageCode == "ar" ? p.NameAR : p.NameEN }).ToList();
            return Json(obj);
        }
        #endregion
        #region Helper
        public JsonResult GetAllCityByRegion(int regionID)
        {
            var listOfCities = blCity.GetAllCitiesByRegion(regionID).Select(p => new { p.CityID, CityName = Utility.CurrentLanguageCode == "ar" ? p.NameAR : p.NameEN });
            return Json(listOfCities);
        }
        public JsonResult GetCities()
        {
            var listOfCities = blCity.GetAllCities().Select(p => new { p.CityID, CityName = Utility.CurrentLanguageCode == "ar" ? p.NameAR : p.NameEN });
            return Json(listOfCities);
        }
        public JsonResult GetCityLocation(int cityID)
        {
            var CityData = blCity.GetById(cityID);
            return Json(CityData);
        }

        public JsonResult GetCitiesByRegionist(string[] listRegionID)
        {
            var listOfCities = blCity.GetAllCitiesByListRegion(listRegionID).Select(p => new { p.CityID, CityName = Utility.CurrentLanguageCode == "ar" ? p.NameAR : p.NameEN });
            return Json(listOfCities);
        }
        public JsonResult GetCitiesByRegionList(string[] listRegionID)
        {
            var listOfCities = blCity.GetAllCitiesByListRegion(listRegionID).Select(p => new { p.CityID, CityName = Utility.CurrentLanguageCode == "ar" ? p.NameAR : p.NameEN });
            return Json(listOfCities);
        }
        public JsonResult GetCitiesByCountryList(string[] listCountryD)
        {
            var listOfCities = blCity.GetAllCitiesByListCountry(listCountryD).Select(p => new { p.CityID, CityName = Utility.CurrentLanguageCode == "ar" ? p.NameAR : p.NameEN });
            return Json(listOfCities);
        }
        #endregion
    }
}
