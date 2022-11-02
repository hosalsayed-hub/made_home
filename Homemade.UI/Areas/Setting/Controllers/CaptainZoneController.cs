using Homemade.BLL;
using Homemade.BLL.Driver;
using Homemade.BLL.Setting;
using Homemade.UI.Attributes;
using Homemade.UI.InfraStructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homemade.UI.Areas.Setting.Controllers
{
    public class CaptainZoneController : Controller
    {
        #region Declarations
        private readonly BlBlock _blBlock;
        private readonly BlDriver _blDriver;
        private readonly ResultMessage result;
        private readonly BlCaptainZone _blCaptainZone;
        public CaptainZoneController(BlBlock blBlock, BlDriver blDriver, ResultMessage result, BlCaptainZone blCaptainZone)
        {
            _blBlock = blBlock;
            _blDriver = blDriver;
            this.result = result;
            _blCaptainZone = blCaptainZone;
        }
        #endregion
        #region Views
        [CustomeAuthoriz((int)ControllerEnum.CaptainZone, (int)ActionEnum.Insert)]
        public IActionResult Index()
        {
            return View();
        }
        [CustomeAuthoriz((int)ControllerEnum.CaptainZone, (int)ActionEnum.Update)]
        public IActionResult Update()
        {
            return View();
        }
        #endregion
        #region Helpers

        [HttpPost]
        public JsonResult LoadTableBlocks(string listCityID)
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
            string[] city = null;
            if (!string.IsNullOrEmpty(listCityID) && listCityID != "null")
            {
                city = listCityID.Split(',');
            }
            var data = _blBlock.GetAllBlockViewModelByCity(city, Utility.CurrentLanguageCode);
            if (Request.Form.ContainsKey("sSearch"))
            {
                string sea = Request.Form["sSearch"];
                if (!string.IsNullOrEmpty(sea))
                {
                    data = data.Where(x => x.BlockNameAR.ToLower().Contains(sea.ToLower()) || x.BlockNameEN.ToLower().Contains(sea.ToLower())
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
        [HttpPost]
        public JsonResult LoadTableCaptain(string listCityID, string listRegionCityID)
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
            string[] city = null;
            if (!string.IsNullOrEmpty(listCityID) && listCityID != "null")
            {
                city = listCityID.Split(',');
            }
            string[] regionCity = null;
            if (!string.IsNullOrEmpty(listRegionCityID) && listRegionCityID != "null")
            {
                regionCity = listRegionCityID.Split(',');
            }
            var data = _blDriver.GetAllCaptainViewModelByCity(city, regionCity, Utility.CurrentLanguageCode);
            if (Request.Form.ContainsKey("sSearch"))
            {
                string sea = Request.Form["sSearch"];
                if (!string.IsNullOrEmpty(sea))
                {
                    data = data.Where(x => x.DriverName.ToLower().Contains(sea.ToLower()) || x.CityName.ToLower().Contains(sea.ToLower())
                    || x.PhoneNumber.ToLower().Contains(sea.ToLower()));
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
        [HttpPost]
        public JsonResult LoadTableCheckBlocksByCaptain(int? DriversID)
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
            var data = _blCaptainZone.GetAllCaptainZoneViewModelByCaptainZoneChecked(DriversID, Utility.CurrentLanguageCode);
            if (Request.Form.ContainsKey("sSearch"))
            {
                string sea = Request.Form["sSearch"];
                if (!string.IsNullOrEmpty(sea))
                {
                    data = data.Where(x => x.BlockName.ToLower().Contains(sea.ToLower())).ToList();
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
        [HttpPost]
        public JsonResult LoadTableNotCheckBlocksByCaptain(int? DriversID)
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
            var data = _blCaptainZone.GetAllBlockViewModelByCaptainZoneNotChecked(DriversID, Utility.CurrentLanguageCode);
            if (Request.Form.ContainsKey("sSearch"))
            {
                string sea = Request.Form["sSearch"];
                if (!string.IsNullOrEmpty(sea))
                {
                    data = data.Where(x => x.BlockNameAR.ToLower().Contains(sea.ToLower()) || x.BlockNameEN.ToLower().Contains(sea.ToLower())).ToList();
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
        public JsonResult GetDrivers()
        {
            var listOfCities = _blDriver.GetAllDriverActive().Select(p => new { p.DriversID, DriversName = Utility.CurrentLanguageCode == "ar" ? p.NameAr : p.NameEn });
            return Json(listOfCities);
        }
        #endregion
        #region Action
        [HttpPost]
        public JsonResult AddCaptainZone(List<int> ListCaptainID, List<int> ListBlockID)
        {
            try
            {
                if (ListCaptainID != null && ListBlockID != null)
                {
                    if (ListCaptainID.Any() && ListBlockID.Any())
                    {
                        var status = _blCaptainZone.Insert(ListCaptainID, ListBlockID, User.GetUserIdInt());
                        if (status)
                        {
                            result.Status = true;
                            result.ResultType = ResultMessageType.success.ToString();
                            result.Message = Resources.Homemade.SuccessSaveMessage;
                        }
                        else
                        {
                            result.Status = false;
                            result.ResultType = ResultMessageType.error.ToString();
                            result.Message = Resources.Homemade.FailSaveMessage;
                        }
                    }
                    else
                    {
                        result.Status = false;
                        result.ResultType = ResultMessageType.error.ToString();
                        result.Message = Resources.Homemade.InsertValidDataMessage;
                    }
                }
                else
                {
                    result.Status = false;
                    result.ResultType = ResultMessageType.error.ToString();
                    result.Message = Resources.Homemade.InsertValidDataMessage;
                }
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.ResultType = ResultMessageType.error.ToString();
                result.Message = Resources.Homemade.FailSaveMessage;
            }
            return Json(result);
        }
        [CustomeAuthoriz((int)ControllerEnum.CaptainZone, (int)ActionEnum.Delete)]
        [HttpPost]
        public JsonResult Delete(int id)
        {
            var result = false;
            try
            {
                result = _blCaptainZone.Delete(id, User.GetUserIdInt());
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
        [CustomeAuthoriz((int)ControllerEnum.CaptainZone, (int)ActionEnum.Delete)]
        [HttpPost]
        public JsonResult AddBlock(int blockID, int driversID)
        {
            var result = false;
            try
            {
                result = _blCaptainZone.AddBlock(blockID, driversID, User.GetUserIdInt());
            }
            catch (Exception ex)
            {
            }
            ResultMessage rResultMessage = result ?
              new ResultMessage { Message = Resources.Homemade.SuccessSaveMessage, ResultType = ResultMessageType.success.ToString() }
              :
              new ResultMessage { Message = Resources.Homemade.FailSaveMessage, ResultType = ResultMessageType.error.ToString() };
            return Json(rResultMessage);
        }
        #endregion
    }
}
