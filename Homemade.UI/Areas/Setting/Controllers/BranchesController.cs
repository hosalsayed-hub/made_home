using Homemade.BLL;
using Homemade.BLL.General;
using Homemade.BLL.Setting;
using Homemade.BLL.Vendor;
using Homemade.BLL.ViewModel.Setting;
using Homemade.UI.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homemade.UI.Areas.Setting.Controllers
{
    public class BranchesController : Controller
    {

        #region Decleration
        private readonly ResultMessage result;
        private readonly BlBranches _BlBranches;
        private readonly BLGeneral bLGeneral;
        private readonly IConfiguration _configuration;
        public BranchesController(ResultMessage result, BLGeneral bLGeneral, BlBranches BlBranches, IConfiguration configuration)
        {
            this.result = result;
            _BlBranches = BlBranches;
            _configuration = configuration;
            this.bLGeneral = bLGeneral;

        }
        #endregion
        #region Views
        [CustomeAuthoriz((int)ControllerEnum.Vendors, (int)ActionEnum.View)]
        public IActionResult Index()
        {

            return View();
        }
        [CustomeAuthoriz((int)ControllerEnum.Vendors, (int)ActionEnum.View)]
        public IActionResult Create()
        {
            return View();
        }
        [CustomeAuthoriz((int)ControllerEnum.Vendors, (int)ActionEnum.View)]
        public IActionResult Details(Guid? id)
        {

            if (id.HasValue)
            {
                BranchesViewModel BranchesViewModel = _BlBranches.GetBranchesViewModelByGuid(id.Value, InfraStructure.Utility.CurrentLanguageCode, _configuration["FilesImageView"]);
                if (BranchesViewModel != null)
                {
                    return View(BranchesViewModel);
                }
            }
            return Redirect(Url.Action("Index", "Vendors", new { Area = "Vendor" }));

        }
        [CustomeAuthoriz((int)ControllerEnum.Vendors, (int)ActionEnum.Update)]
        public IActionResult Edit(Guid? id)
        {
            if (id.HasValue)
            {
                BranchesViewModel VendorViewModel = _BlBranches.GetBranchesViewModelByGuid(id.Value, InfraStructure.Utility.CurrentLanguageCode, _configuration["VendorImageView"]);
                if (VendorViewModel != null)
                {
                    return View(VendorViewModel);
                }
            }
            return Redirect(Url.Action("Index", "Vendors", new { Area = "Vendor" }));
        }
        #endregion
        #region Actions
        [CustomeAuthoriz((int)ControllerEnum.Vendors, (int)ActionEnum.Insert)]
        [HttpPost, ValidateAntiForgeryToken]
        [Obsolete]
        public async Task<JsonResult> Create(BranchesViewModel viewModel)
        {
            try
            {
                #region Validation

                JsonResult ValidateData = ValidateModel(viewModel.BranchesID, viewModel.CityID, viewModel.MobileNo, viewModel.FirstEamil);

                if (ValidateData != null) return ValidateData;

                #endregion
                if (viewModel.BranchesID == 0)
                {
                    viewModel.CreateDate = bLGeneral.GetDateTimeNow();
                    viewModel.UserId = User.GetUserIdInt();
                    int ID = 0;
                    if (_BlBranches.Insert(viewModel, out ID))
                    {
                        result.Message = Homemade.UI.Resources.Homemade.SuccessSaveMessage;
                        result.ResultType = ResultMessageType.success.ToString();
                        result.Status = true;
                        result.ID = ID;
                        return Json(result);
                    }
                    else
                    {
                        result.Message = Homemade.UI.Resources.Homemade.FailSaveMessage;
                        result.ResultType = ResultMessageType.error.ToString();
                        result.Status = false;
                        return Json(result);
                    }
                }
                else
                {
                    viewModel.CreateDate = bLGeneral.GetDateTimeNow();
                    viewModel.UserId = User.GetUserIdInt();

                    viewModel.UpdateDate = bLGeneral.GetDateTimeNow();
                    viewModel.UserUpdate = User.GetUserIdInt();
                    #region Update
                    int UserId = User.GetUserIdInt();
                    if (_BlBranches.Update(viewModel))
                    {
                        result.Message = Homemade.UI.Resources.Homemade.SuccessSaveMessage;
                        result.ResultType = ResultMessageType.success.ToString();
                        result.Status = true;
                        return Json(result);
                    }
                    else
                    {
                        result.Message = Homemade.UI.Resources.Homemade.FailSaveMessage;
                        result.ResultType = ResultMessageType.error.ToString();
                        result.Status = false;
                        return Json(result);
                    }
                    #endregion
                }
            }
            catch (System.Exception ex)
            {
                result.ResultType = ResultMessageType.error.ToString();
                result.Message = ex.Message.ToString();
                result.Status = false;
                return Json(result);
            }
        }
        [CustomeAuthoriz((int)ControllerEnum.Vendors, (int)ActionEnum.Update)]
        [HttpPost, ValidateAntiForgeryToken]
        [Obsolete]
        public async Task<JsonResult> Edit(BranchesViewModel viewModel)
        {
            try
            {
                #region Validation

                JsonResult ValidateData = ValidateModel(viewModel.BranchesID, viewModel.CityID, viewModel.MobileNo, viewModel.FirstEamil);

                if (ValidateData != null) return ValidateData;

                #endregion
                #region Update
                int UserId = User.GetUserIdInt();
                if (_BlBranches.Update(viewModel))
                {
                    result.Message = Homemade.UI.Resources.Homemade.SuccessSaveMessage;
                    result.ResultType = ResultMessageType.success.ToString();
                    result.Status = true;
                    return Json(result);
                }
                else
                {
                    result.Message = Homemade.UI.Resources.Homemade.FailSaveMessage;
                    result.ResultType = ResultMessageType.error.ToString();
                    result.Status = false;
                    return Json(result);
                }
                #endregion

            }
            catch (System.Exception ex)
            {
                result.ResultType = ResultMessageType.error.ToString();
                result.Message = ex.Message.ToString();
                result.Status = false;
                return Json(result);
            }
        }
        private JsonResult ValidateModel(int BranchesID, int CityID, string Mobile, string Email)
        {

            if (_BlBranches.IsExistMobile(Mobile, BranchesID))
            {
                result.Message = Homemade.UI.Resources.Homemade.MobileIsExists;
                result.ResultType = ResultMessageType.error.ToString();
                return Json(result);
            }
            if (!string.IsNullOrWhiteSpace(Email))
            {
                if (_BlBranches.IsExistEmail(Email, BranchesID))
                {
                    result.Message = Homemade.UI.Resources.Homemade.EmailExists;
                    result.ResultType = ResultMessageType.error.ToString();
                    return Json(result);
                }
            }

            if (CityID == 0)
            {
                result.Message = BLL.Resources.HomemadeErrorMessages.CityRequierd;
                result.ResultType = ResultMessageType.error.ToString();
                return Json(result);
            }
            return null;
        }
        [CustomeAuthoriz((int)ControllerEnum.Vendors, (int)ActionEnum.Delete)]
        [HttpPost]
        public JsonResult Delete(int id)
        {
            var result = false;
            try
            {
                result = _BlBranches.Delete(id, User.GetUserIdInt());
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
        [CustomeAuthoriz((int)ControllerEnum.Vendors, (int)ActionEnum.Update)]
        [HttpPost]
        public JsonResult ChangeStatue(int id)
        {
            var result = false;
            try
            {
                result = _BlBranches.ChangeStatus(id, User.GetUserIdInt());
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
        #region Helpers
        [HttpPost]
        public JsonResult LoadTable(string listCountryID, string listCityID, string email, string mobile, string listBranchesID)
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


            string[] CountryIds = null;
            if (!string.IsNullOrEmpty(listCountryID) && listCountryID != "null")
            {
                CountryIds = listCountryID.Split(',');
            }
            string[] BranchesIds = null;
            if (!string.IsNullOrEmpty(listBranchesID) && listBranchesID != "null")
            {
                BranchesIds = listBranchesID.Split(',');
            }
            #endregion
            string[] city = null;
            if (!string.IsNullOrEmpty(listCityID) && listCityID != "null")
            {
                city = listCityID.Split(',');
            }


            string MainPathView = _configuration["FilesImageView"];
            var data = _BlBranches.GetAllBranchesViewModelBySearch(CountryIds, city, email, mobile, BranchesIds, InfraStructure.Utility.CurrentLanguageCode, MainPathView);
            if (Request.Form.ContainsKey("sSearch"))
            {
                string search = Request.Form["sSearch"];
                if (!string.IsNullOrEmpty(search))
                {
                    data = data.Where(x =>
                    x.BranchesNameAR.ToLower().Contains(search.ToLower()) || x.BranchesNameEN.ToLower().Contains(search.ToLower()) || x.CityNameEn.ToLower().Contains(search.ToLower()) || x.CityNameAr.ToLower().Contains(search.ToLower())
                    || x.MobileNo.ToLower().Contains(search.ToLower())
                    || x.FirstEamil.ToLower().Contains(search.ToLower()));
                }
            }
            var Responce = data.AsQueryable().Select(p => new
            {
                id = p.BranchesID,
                address = p.Address,
                name = Homemade.UI.InfraStructure.Utility.CurrentLanguageCode == "ar" ? p.BranchesNameAR : p.BranchesNameEN,
                phoneNo = p.MobileNo,
                email = p.FirstEamil,
                isEnable = p.IsEnable,
                cityNameEN = Homemade.UI.InfraStructure.Utility.CurrentLanguageCode == "ar" ? p.CityNameAr : p.CityNameEn,
                BranchesGuid = p.BranchesGuid
            });
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
        public JsonResult GetAllShippingCompanies()
        {
            var obj = _BlBranches.GetBranches().Select(p => new { p.BranchesID, BranchesName = Homemade.UI.InfraStructure.Utility.CurrentLanguageCode == "ar" ? p.BranchesNameAR : p.BranchesNameEN }).ToList();
            return Json(obj);
        }
        #endregion
    }
}
