using Homemade.BLL;
using Homemade.BLL.General;
using Homemade.BLL.Setting;
using Homemade.UI.Attributes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homemade.UI.Areas.Setting.Controllers
{
    public class CitiesCoveredController : Controller
    {
        #region Declarations
        private readonly BlCitiesCovered blCitiesCovered;
        private readonly ResultMessage result;

        public CitiesCoveredController(BlCitiesCovered blCitiesCovered, ResultMessage result)
        {
            this.blCitiesCovered = blCitiesCovered;
            this.result = result;
        }
        #endregion
        #region View
        [CustomeAuthoriz((int)ControllerEnum.CitiesCovered, (int)ActionEnum.View)]
        public IActionResult Index() => View();
        #endregion
        #region Actions
        [HttpPost]
        public JsonResult LoadTable()
        {
            var data = blCitiesCovered.GetAllCitiesCoveredViewModel(Homemade.UI.InfraStructure.Utility.CurrentLanguageCode);
            if (Request.Form.ContainsKey("sSearch"))
            {
                string sea = Request.Form["sSearch"];
                if (!string.IsNullOrEmpty(sea))
                {
                    data = data.Where(x => x.CityName.ToLower().Contains(sea.ToLower()) || x.CityID.ToString().ToLower().Contains(sea.ToLower()));
                }
            }
            int totalCount = data.Count();
            return Json(new
            {
                iTotalRecords = totalCount,
                iTotalDisplayRecords = totalCount,
                aaData = data
            });
        }
        [HttpPost]
        public JsonResult SaveCovered(string CheckedItems, string Items, int[] t)
        {
            var result = false;
            try
            {
                result = blCitiesCovered.SaveCovered(CheckedItems, User.GetUserIdInt());
            }
            catch (Exception ex)
            {
                result = false;
            }
            ResultMessage rResultMessage = result ?
                new ResultMessage { Message = Resources.Homemade.SuccessChangeMessage, ResultType = ResultMessageType.success.ToString() }
                :
                new ResultMessage { Message = Resources.Homemade.FailSaveMessage, ResultType = ResultMessageType.error.ToString() };
            return Json(rResultMessage);

        }
        
        #endregion
    }
}
