using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Homemade.BLL;
using Homemade.BLL.General;
using Homemade.BLL.ViewModel.Vendor;
using Homemade.UI.Attributes;
using Microsoft.Extensions.Configuration;
using Homemade.UI.InfraStructure;
using Homemade.BLL.ViewModel;
using Microsoft.AspNetCore.Identity;
using Homemade.Model;
using Homemade.BLL.Emp;
using Homemade.BLL.Vendor;

namespace Homemade.UI.Areas.Vendor.Controllers
{
    public class VacHistoryController : Controller
    {
        #region Declarations
        private readonly BlVacHistory blVacHistory;
        private readonly BLUser bLUser;
        private readonly BlVendor _blVendor;

        public VacHistoryController(BlVacHistory blVacHistory, BLUser bLUser, BlVendor blVendor)
        {
            this.blVacHistory = blVacHistory;
            this.bLUser = bLUser;
            _blVendor = blVendor;
        }
        #endregion
        #region View
        public IActionResult Index()
        {
            return View();
        }
        [CustomeAuthoriz((int)ControllerEnum.VendorVacHistory, (int)ActionEnum.View)]
        public IActionResult IndexVendor()
        {
            return View();
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

            var data = blVacHistory.GetAllVacHistory(InfraStructure.Utility.CurrentLanguageCode);
            if (Request.Form.ContainsKey("sSearch"))
            {
                string search = Request.Form["sSearch"];
                if (!string.IsNullOrEmpty(search))
                {
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
        public JsonResult LoadTableVendor()
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

            var user = bLUser.GetUserInfo(User.GetUserIdInt());
            int UserTypeId = user.UserType;
            int VendorsID = 0;
            if (UserTypeId == (int)UserTypeEnum.Vendor)
            {
                var vednor = _blVendor.GetVendorByUserId(User.GetUserIdInt());
                if (vednor != null)
                {
                    VendorsID = vednor.VendorsID;
                }
            }
            var data = blVacHistory.GetAllVacHistoryByVendor(VendorsID, InfraStructure.Utility.CurrentLanguageCode);
            if (Request.Form.ContainsKey("sSearch"))
            {
                string search = Request.Form["sSearch"];
                if (!string.IsNullOrEmpty(search))
                {
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
        #endregion
    }
}
