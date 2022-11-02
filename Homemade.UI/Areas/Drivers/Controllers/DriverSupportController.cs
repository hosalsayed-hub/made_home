using Homemade.BLL;
using Homemade.BLL.Driver;
using Homemade.UI.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homemade.UI.Areas.Drivers.Controllers
{
    public class DriverSupportController : Controller
    {
        #region Declarations
        private readonly BlDriverSupport _blDriverSupport;
        private readonly IConfiguration _configuration;

        public DriverSupportController(IConfiguration configuration, BlDriverSupport blDriverSupport)
        {
            _configuration = configuration;
            _blDriverSupport = blDriverSupport;
        }
        #endregion
        #region View
        [CustomeAuthoriz((int)ControllerEnum.DriverSupport, (int)ActionEnum.View)]
        public IActionResult Index()
        {
            return View();
        }
        [CustomeAuthoriz((int)ControllerEnum.DriverSupport, (int)ActionEnum.View)]
        public IActionResult Details(Guid? id)
        {
            if (id.HasValue)
            {
                var tbl = _blDriverSupport.GetDriversHelpViewModelByGuid(id.Value, InfraStructure.Utility.CurrentLanguageCode, _configuration["HelpImageView"]);
                return View(tbl);
            }
            return LocalRedirect("/Drivers/DriverSupport/Index");
        }
        #endregion
        #region Helpers
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

            var data = _blDriverSupport.GetAllDriversHelpViewModel(InfraStructure.Utility.CurrentLanguageCode, _configuration["HelpImageView"]);
            if (Request.Form.ContainsKey("sSearch"))
            {
                string search = Request.Form["sSearch"];
                if (!string.IsNullOrEmpty(search))
                {
                    data = data.Where(x =>
                    x.DriverName.ToLower().Contains(search.ToLower()) || x.DriverMobileNo.ToLower().Contains(search.ToLower()) || x.DriverIDNo.ToLower().Contains(search.ToLower()) || x.Descripe.ToLower().Contains(search.ToLower())
                    || x.CreateDate.ToLower().Contains(search.ToLower())
                    || x.HelpQuestionsName.ToLower().Contains(search.ToLower()));
                }
            }
            data = data.Skip(Convert.ToInt32(pageno - 1) * displayLength);
            var takenData = data.Take(((Convert.ToInt32(pageno) * displayLength)) - (Convert.ToInt32(pageno - 1) * displayLength));
            int totalCount = data.Count();
            return Json(new
            {
                iTotalRecords = totalCount,
                iTotalDisplayRecords = totalCount,
                aaData = takenData
            });

        }
        #endregion
    }
}
