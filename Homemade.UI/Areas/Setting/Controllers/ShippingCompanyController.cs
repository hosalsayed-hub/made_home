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
    public class ShippingCompanyController : Controller
    {

        private readonly ResultMessage result;
        private readonly BlShippingCompany _BlShippingCompany;
        private readonly BLGeneral bLGeneral;
        private readonly IConfiguration _configuration;
        public ShippingCompanyController(ResultMessage result, BLGeneral bLGeneral, BlShippingCompany BlShippingCompany, IConfiguration configuration)
        {
            this.result = result;
            _BlShippingCompany = BlShippingCompany;
            _configuration = configuration;
            this.bLGeneral = bLGeneral;

        }

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
                ShippingCompanyViewModel ShippingCompanyViewModel = _BlShippingCompany.GetShippingCompanyViewModelByGuid(id.Value, InfraStructure.Utility.CurrentLanguageCode, _configuration["ConfigImageView"]);
                if (ShippingCompanyViewModel != null)
                {
                    return View(ShippingCompanyViewModel);
                }
            }
            return Redirect(Url.Action("Index", "Vendors", new { Area = "Vendor" }));

        }
        [CustomeAuthoriz((int)ControllerEnum.Vendors, (int)ActionEnum.Update)]
        public IActionResult Edit(Guid? id)
        {
            if (id.HasValue)
            {
                ShippingCompanyViewModel VendorViewModel = _BlShippingCompany.GetShippingCompanyViewModelByGuid(id.Value, InfraStructure.Utility.CurrentLanguageCode, _configuration["ConfigImageView"]);
                if (VendorViewModel != null)
                {
                    return View(VendorViewModel);
                }
            }
            return Redirect(Url.Action("Index", "Vendors", new { Area = "Vendor" }));
        }
        [CustomeAuthoriz((int)ControllerEnum.Vendors, (int)ActionEnum.Insert)]
        [HttpPost, ValidateAntiForgeryToken]
        [Obsolete]
        public async Task<JsonResult> Create(ShippingCompanyViewModel viewModel)
        {
            try
            {
                #region Validation

                JsonResult ValidateData = ValidateModel(viewModel.ShippingCompanyID, viewModel.CityID, viewModel.MobileNo, viewModel.Email);

                if (ValidateData != null) return ValidateData;

                #endregion
                if (viewModel.ShippingCompanyID == 0)
                {
                    viewModel.CreateDate = bLGeneral.GetDateTimeNow();
                    viewModel.UserId = User.GetUserIdInt();
                    var Logo = "";
                    if (!string.IsNullOrEmpty(viewModel.Logo))
                    {
                        string FileName = Guid.NewGuid() + ".png";
                        Logo = FileName;
                        string MainPath = _configuration["ConfigImageSave"];
                        bLGeneral.SaveImage(new BLGeneral.SaveImageModel
                        {
                            base64 = viewModel.Logo,
                            key = "",
                            fileName = FileName,
                            mainPath = MainPath
                        });
                        viewModel.Logo = Logo;
                    }



                    int ID = 0;
                    if (_BlShippingCompany.Insert(viewModel, out ID))
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
                    var Logo = "";
                    if (!string.IsNullOrEmpty(viewModel.Logo))
                    {
                        string FileName = Guid.NewGuid() + ".png";
                        Logo = FileName;
                        string MainPath = _configuration["ConfigImageSave"];
                        bLGeneral.SaveImage(new BLGeneral.SaveImageModel
                        {
                            base64 = viewModel.Logo,
                            key = "",
                            fileName = FileName,
                            mainPath = MainPath
                        });
                        viewModel.Logo = Logo;
                    }



                    viewModel.UpdateDate = bLGeneral.GetDateTimeNow();
                    viewModel.UserUpdate = User.GetUserIdInt();
                    #region Update
                    int UserId = User.GetUserIdInt();
                    if (_BlShippingCompany.Update(viewModel))
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
        public async Task<JsonResult> Edit(ShippingCompanyViewModel viewModel)
        {
            try
            {
                #region Validation

                JsonResult ValidateData = ValidateModel(viewModel.ShippingCompanyID, viewModel.CityID, viewModel.MobileNo, viewModel.Email);

                if (ValidateData != null) return ValidateData;

                #endregion

                var Logo = "";
                if (!string.IsNullOrEmpty(viewModel.Logo))
                {
                    string FileName = Guid.NewGuid() + ".png";
                    Logo = FileName;
                    string MainPath = _configuration["ConfigImageSave"];
                    bLGeneral.SaveImage(new BLGeneral.SaveImageModel
                    {
                        base64 = viewModel.Logo,
                        key = "",
                        fileName = FileName,
                        mainPath = MainPath
                    });
                    viewModel.Logo = Logo;
                }
                #region Update
                int UserId = User.GetUserIdInt();
                if (_BlShippingCompany.Update(viewModel))
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
        private JsonResult ValidateModel(int ShippingCompanyID, int CityID, string Mobile, string Email)
        {

            if (_BlShippingCompany.IsExistMobile(Mobile, ShippingCompanyID))
            {
                result.Message = Homemade.UI.Resources.Homemade.MobileIsExists;
                result.ResultType = ResultMessageType.error.ToString();
                return Json(result);
            }
            if (!string.IsNullOrWhiteSpace(Email))
            {
                if (_BlShippingCompany.IsExistEmail(Email, ShippingCompanyID))
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
                result = _BlShippingCompany.Delete(id, User.GetUserIdInt());
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
                result = _BlShippingCompany.ChangeStatus(id, User.GetUserIdInt());
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
        public JsonResult LoadTable(string listCountryID, string listCityID, string email, string mobile, string listShippingCompanyID)
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
            string[] ShippingCompanyIds = null;
            if (!string.IsNullOrEmpty(listShippingCompanyID) && listShippingCompanyID != "null")
            {
                ShippingCompanyIds = listShippingCompanyID.Split(',');
            }
            #endregion
            string[] city = null;
            if (!string.IsNullOrEmpty(listCityID) && listCityID != "null")
            {
                city = listCityID.Split(',');
            }


            string MainPathView = _configuration["ConfigImageView"];
            var data = _BlShippingCompany.GetAllShippingCompanyViewModelBySearch(CountryIds, city, email, mobile, ShippingCompanyIds, InfraStructure.Utility.CurrentLanguageCode, MainPathView);
            if (Request.Form.ContainsKey("sSearch"))
            {
                string search = Request.Form["sSearch"];
                if (!string.IsNullOrEmpty(search))
                {
                    data = data.Where(x =>
                    x.NameAr.ToLower().Contains(search.ToLower()) || x.NameEn.ToLower().Contains(search.ToLower()) || x.CityNameEN.ToLower().Contains(search.ToLower()) || x.CityNameAR.ToLower().Contains(search.ToLower())
                    || x.MobileNo.ToLower().Contains(search.ToLower())
                    || x.Email.ToLower().Contains(search.ToLower()));
                }
            }
            var Responce = data.AsQueryable().Select(p => new
            {
                id = p.ShippingCompanyID,
                address = p.Address,
                name = Homemade.UI.InfraStructure.Utility.CurrentLanguageCode == "ar" ? p.NameAr : p.NameEn,
                phoneNo = p.PhoneNo,
                mobileNo = p.MobileNo,
                logo = p.Logo,
                email = p.Email,
                isEnable = p.IsEnable,
                cityNameEN = Homemade.UI.InfraStructure.Utility.CurrentLanguageCode == "ar" ? p.CityNameAR : p.CityNameEN,
                shippingCompanyGuid = p.ShippingCompanyGuid

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
            var obj = _BlShippingCompany.GetShippingCompanies().Select(p => new { p.ShippingCompanyID, ShippingCompanyName = Homemade.UI.InfraStructure.Utility.CurrentLanguageCode == "ar" ? p.NameAr : p.NameAr }).ToList();
            return Json(obj);
        }
        public JsonResult GetShippingCompanyMaxKM(int ShippingCompanyID)
        {
            var obj = _BlShippingCompany.GetShippingCompanyMaxKM(ShippingCompanyID);
            var listblocks = _BlShippingCompany.GetListBlocksFromShippingCompanyBlocks(ShippingCompanyID);

            return Json(new { ShippingCompanyMaxKM = obj, ListBlocks = listblocks });
        }
    }
}
