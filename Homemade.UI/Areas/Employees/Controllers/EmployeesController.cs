using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Homemade.BLL;
 using Homemade.BLL.General;
using Homemade.BLL.ViewModel.Employees;
using Homemade.UI.Attributes;
using Microsoft.Extensions.Configuration;
using Homemade.UI.InfraStructure;
using Homemade.BLL.ViewModel;
using Microsoft.AspNetCore.Identity;
using Homemade.Model;
using Homemade.BLL.Emp;
using Homemade.BLL.SMS;

namespace Homemade.UI.Areas.Employees.Controllers
{
    public class EmployeesController : Controller
    {
        #region Declarations
        private readonly BlEmployee blEmployees;
        private readonly ResultMessage result;
        private readonly BLPermission _bLPermission;
        private readonly UserManager<User> _userManager;

        private readonly BLGeneral bLGeneral;
        private readonly IConfiguration _configuration;
        private readonly BLUser bLUser;
        private readonly OTPService _oTPService;

        public EmployeesController(OTPService oTPService, BLPermission BLPermission, UserManager<User> userManager, BlEmployee blEmployees, ResultMessage result, BLGeneral bLGeneral, IConfiguration configuration, BLUser bLUser)
        {
            this.bLGeneral = bLGeneral;
            _configuration = configuration;
            _userManager = userManager;
            this.blEmployees = blEmployees;
            this.result = result;
            this._bLPermission = BLPermission;
            this.bLUser = bLUser;
            this._oTPService = oTPService;
        }
        #endregion
        #region View
        [CustomeAuthoriz((int)ControllerEnum.Employees, (int)ActionEnum.View)]
        public IActionResult Index(int? jobtype)
        {
            if (GetPermissionUser((int)ActionEnum.View))
            {
                var data = new EmployeesViewModel();
                data.JobTypeId = jobtype != null ? (int)jobtype : (int)JobTypeEnum.Admin;
                return View(data);
            }
            else
            {
                return Redirect("/AccessDenied/Unauthorized");
            }
        }

        [CustomeAuthoriz((int) ControllerEnum.Employees, (int) ActionEnum.Insert)]
        public IActionResult Create(int? jobtype)
        {
            if (GetPermissionUser((int)ActionEnum.Insert))
            {
                var data = new EmployeesViewModel();
            data.JobTypeId = jobtype != null ? (int)jobtype : (int)JobTypeEnum.Admin;
             
            return View(data);
            }
            else
            {
                return Redirect("/AccessDenied/Unauthorized");
            }
        }

        [CustomeAuthoriz((int)ControllerEnum.Employees, (int)ActionEnum.View)]
        public IActionResult Details(Guid? id)
        {
            if (GetPermissionUser((int)ActionEnum.View))
            {
                if (id.HasValue)
                {
                    EmployeesViewModel EmployeeViewModel = blEmployees.GetEmployeesViewModelByGuid(id.Value, InfraStructure.Utility.CurrentLanguageCode, _configuration["EmployeeImageView"]);
                    if (EmployeeViewModel != null)
                    {
                        return View(EmployeeViewModel);
                    }
                }
                return Redirect(Url.Action("Index", "Employee", new { Area = "Employees" }));
            }
            else
            {
                return Redirect("/AccessDenied/Unauthorized");
            }
        }
        [CustomeAuthoriz((int)ControllerEnum.Employees, (int)ActionEnum.Update)]
        public IActionResult Edit(Guid? id)
        {
            if (GetPermissionUser((int)ActionEnum.View))
            {
                if (id.HasValue)
            {
                EmployeesViewModel EmployeeViewModel = blEmployees.GetEmployeesViewModelByGuid(id.Value, InfraStructure.Utility.CurrentLanguageCode, _configuration["EmployeeImageView"]);
                if (EmployeeViewModel != null)
                {
                    return View(EmployeeViewModel);
                }
            }
            return Redirect(Url.Action("Index", "Employee", new { Area = "Employees" }));
            }
            else
            {
                return Redirect("/AccessDenied/Unauthorized");
            }
        }
        #endregion
        #region Permession
        public JsonResult LoadTableRolePermissions(string Roles, int? userId)
        {
            List<PermissionControllerActionViewModel> controller = new List<PermissionControllerActionViewModel>();
            if (!string.IsNullOrWhiteSpace(Roles))
            {
                controller = _bLPermission.GetAllPermissionForComapny(Roles, userId, Request.Cookies.IsArabic());
            }
            var data = controller;
            int totalCount = data.Count();

            if (Request.Form.ContainsKey("sSearch"))
            {
                string s = Request.Form["sSearch"];
                if (!string.IsNullOrEmpty(s))
                {
                    data = data.Where(x => x.ControllerName.ToLower().Contains(s.ToLower())).ToList();
                }
            }

            return Json(new
            {
                iTotalRecords = totalCount,
                iTotalDisplayRecords = totalCount,
                aaData = data
            });
        }

        #endregion
        #region Action
        [CustomeAuthoriz((int)ControllerEnum.Employees, (int)ActionEnum.Insert)]
        [HttpPost, ValidateAntiForgeryToken]
        [Obsolete]
        public async Task<JsonResult> Create(EmployeesViewModel viewModel)
        {
            try
            {
                if (!string.IsNullOrEmpty(viewModel.BirthDateMiladyString))
                {
                    viewModel.BirthDateMilady = DateTime.ParseExact(viewModel.BirthDateMiladyString, "yyyy-mm-dd", System.Globalization.CultureInfo.InvariantCulture);
                    ModelState.Remove("BirthDateMilady");
                }
                #region Validation

                JsonResult ValidateData = ValidateModel(0, viewModel.FirstNameAR, viewModel.FirstNameEN, viewModel.CityID, viewModel.MobileNo, viewModel.PhoneNumber, viewModel.Email, viewModel.JobTypeId, viewModel.IDNo, viewModel.FileNo);

                if (ValidateData != null) return ValidateData;
                if(string.IsNullOrEmpty(viewModel.CheckedItems))
                {
                    result.Message = BLL.Resources.HomemadeErrorMessages.Please_Check_Any_Permission_First;
                    result.ResultType = ResultMessageType.error.ToString();
                    return Json(result);
                }
                #endregion
                var Photo = "";
                if (!string.IsNullOrEmpty(viewModel.Photo))
                {
                    string FileName = Guid.NewGuid() + ".png";
                    Photo = FileName;
                    string MainPath = _configuration["EmployeeImageSave"];
                    bLGeneral.SaveImage(new BLGeneral.SaveImageModel
                    {
                        base64 = viewModel.Photo,
                        key = "",
                        fileName = FileName,
                        mainPath = MainPath
                    });
                    viewModel.Photo = Photo;
                }
                #region Adding
                if(bLUser.IsExistIDNoInUser(viewModel.IDNo))
                    {
                    result.Message = BLL.Resources.HomemadeErrorMessages.User_Is_Exist;
                    result.ResultType = ResultMessageType.error.ToString();
                    return Json(result);
                }
                string Password = bLGeneral.GeneratePassword(8);
                User user = new User
                {
                    UserName = bLGeneral.GenerateToken(200),
                    Email = bLGeneral.RandomString(10) + "@made-home.com",
                    PhoneNumber = bLGeneral.RandomNumber(10),
                    UserType = viewModel.JobTypeId == (int)JobTypeEnum.Admin ? (int)UserTypeEnum.Admin : (int)UserTypeEnum.Employee,
                };
                var resultsss = await _userManager.CreateAsync(user, Password);
                if (user.Id != 0)
                {
                    viewModel.UserId = user.Id;
                    if (await blEmployees.Insert(viewModel))
                    {

                        await _oTPService.MessageAfterRegister_Employee(viewModel.MobileNo, (int)UserTypeEnum.Employee, viewModel.MobileNo, Password);
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
                }
                else
                {
                    await _userManager.DeleteAsync(user);
                    result.Message = Resources.Homemade.FailSaveMessage;
                    result.ResultType = ResultMessageType.error.ToString();
                }
                return Json(result);
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
        [CustomeAuthoriz((int)ControllerEnum.Employees, (int)ActionEnum.Update)]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<JsonResult> Edit(EmployeesViewModel viewModel)
        {
            try
            {
                if (!string.IsNullOrEmpty(viewModel.BirthDateMiladyString))
                {
                    viewModel.BirthDateMilady = DateTime.ParseExact(viewModel.BirthDateMiladyString, "yyyy-mm-dd", System.Globalization.CultureInfo.InvariantCulture);
                    ModelState.Remove("BirthDateMilady");
                }
                #region Validation

                JsonResult ValidateData = ValidateModel(viewModel.EntityEmpID, viewModel.FirstNameAR, viewModel.FirstNameEN, viewModel.CityID, viewModel.MobileNo, viewModel.PhoneNumber, viewModel.Email, viewModel.JobTypeId, viewModel.IDNo, viewModel.FileNo);

                if (ValidateData != null) return ValidateData;
                if (string.IsNullOrEmpty(viewModel.CheckedItems))
                {
                    result.Message = BLL.Resources.HomemadeErrorMessages.Please_Check_Any_Permission_First;
                    result.ResultType = ResultMessageType.error.ToString();
                    return Json(result);
                }
                #endregion

                #region update
                var obj = blEmployees.GetById(viewModel.EntityEmpID);

                bool IsIdNoChanged = false;
                if(obj.IDNo != viewModel.IDNo)
                {
                    if (bLUser.IsExistIDNoInUser(viewModel.IDNo))
                    {
                        result.Message = BLL.Resources.HomemadeErrorMessages.User_Is_Exist;
                        result.ResultType = ResultMessageType.error.ToString();
                        return Json(result);
                    }
                    else
                    {
                        IsIdNoChanged = true;
                    }
                }
                viewModel.UserUpdate = User.GetUserIdInt();
                if (blEmployees.Update(viewModel, _configuration["EmployeeImageSave"], _configuration["EmployeeImageView"]))
                {
                    if (IsIdNoChanged)
                    {
                        User user = bLUser.GetUserInfo(obj.UserId);
                        user.UserName = viewModel.IDNo;
                        await _userManager.UpdateAsync(user);
                    }
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
        [CustomeAuthoriz((int)ControllerEnum.City, (int)ActionEnum.Delete)]
        [HttpPost]
        public JsonResult Delete(int id)
        {
            var result = false;
            try
            {
                result = blEmployees.Delete(id, User.GetUserIdInt());
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
                result = blEmployees.ChangeStatus(id, User.GetUserIdInt());
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
        public JsonResult LoadTable(int? jobTypeId ,string listRegionID, string listCityID,
            string listEntityID, string name, string fileNo, string mobile, string idNo, string listStatusID, string listDepartmentsID,
            string listJobsID)
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
            string[] region = null;
            if (!string.IsNullOrEmpty(listRegionID) && listRegionID != "null")
            {
                region = listRegionID.Split(',');
            }
            string[] entity = null;
            if (!string.IsNullOrEmpty(listEntityID) && listEntityID != "null" && listEntityID != "undefined")
            {
                entity = listEntityID.Split(',');
            }
            string[] status = null;
            if (!string.IsNullOrEmpty(listStatusID) && listStatusID != "null")
            {
                status = listStatusID.Split(',');
            }
            string[] department = null;
            if (!string.IsNullOrEmpty(listDepartmentsID) && listDepartmentsID != "null" && listDepartmentsID != "undefined")
            {
                department = listDepartmentsID.Split(',');
            }
            string[] job = null;
            if (!string.IsNullOrEmpty(listJobsID) && listJobsID != "null" && listJobsID != "undefined")
            {
                job = listJobsID.Split(',');
            }
            int EntityID = 0;
            var user = bLUser.GetUserInfo(User.GetUserIdInt());
            int UserTypeId = user.UserType;
             

            var data = blEmployees.GetAllEmployeesViewModelBySearch(UserTypeId, EntityID, jobTypeId, region, city, entity, name, fileNo, mobile, idNo,status,department,job, InfraStructure.Utility.CurrentLanguageCode);
            if (Request.Form.ContainsKey("sSearch"))
            {
                string search = Request.Form["sSearch"];
                if (!string.IsNullOrEmpty(search))
                {
                    data = data.Where(x =>
                    x.FirstNameAR.ToLower().Contains(search.ToLower()) || x.FirstNameEN.ToLower().Contains(search.ToLower()) || x.CityName.ToLower().Contains(search.ToLower()) 
                    ||x.RegionName.ToLower().Contains(search.ToLower()) || (x.MobileNo != null ? x.MobileNo.ToLower().Contains(search.ToLower()) : true) || (x.PhoneNumber!=null ? x.PhoneNumber.ToLower().Contains(search.ToLower()) : true)
                    || x.Email.ToLower().Contains(search.ToLower()) 
                    || (x.IDNo != null  ? x.IDNo.ToLower().Contains(search.ToLower()) : true)
                    || x.IsEnableString.ToLower().Contains(search.ToLower())
                    );
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
        private JsonResult ValidateModel(int EntityEmpID, string NameAr, string NameEn, int CityID, string Mobile, string PhoneNumber, string Email,
            int? JobTypeId, string IdNO, string FileNo)
        {
            if (string.IsNullOrWhiteSpace(NameAr))
            {
                result.Message = BLL.Resources.HomemadeErrorMessages.ArNameRequierd;
                result.ResultType = ResultMessageType.error.ToString();
                return Json(result);
            }
            if (string.IsNullOrWhiteSpace(NameEn))
            {
                result.Message = BLL.Resources.HomemadeErrorMessages.EnNameRequierd;
                result.ResultType = ResultMessageType.error.ToString();
                return Json(result);
            }
            if (!string.IsNullOrWhiteSpace(Mobile))
            {
                if (blEmployees.IsExistMobile(Mobile, EntityEmpID))
                {
                    result.Message = Homemade.UI.Resources.Homemade.MobileIsExists;
                    result.ResultType = ResultMessageType.error.ToString();
                    return Json(result);
                }
            }
            if (string.IsNullOrWhiteSpace(IdNO))
            {
                result.Message = Homemade.UI.Resources.Homemade.IdNoRequired;
                    result.ResultType = ResultMessageType.error.ToString();
                  return Json(result);
            }
            if (blEmployees.IsExistIdNo(IdNO, EntityEmpID))
            {
                result.Message = Homemade.UI.Resources.Homemade.IDNoIsExists;
                result.ResultType = ResultMessageType.error.ToString();
                return Json(result);
            }
            if (!string.IsNullOrWhiteSpace(PhoneNumber))
            {
                if (blEmployees.IsExistPhone(PhoneNumber, EntityEmpID))
                {
                    result.Message = Homemade.UI.Resources.Homemade.PhoneIsExists;
                    result.ResultType = ResultMessageType.error.ToString();
                    return Json(result);
                }
            }

            if (string.IsNullOrWhiteSpace(Email))
            {
                result.Message = Homemade.UI.Resources.Homemade.EmailRequired;
                result.ResultType = ResultMessageType.error.ToString();
                return Json(result);
            }
            if (blEmployees.IsExistEmail(Email, EntityEmpID))
            {
                result.Message = Homemade.UI.Resources.Homemade.EmailExists;
                result.ResultType = ResultMessageType.error.ToString();
                return Json(result);
            }

            if (CityID == 0)
            {
                result.Message = BLL.Resources.HomemadeErrorMessages.CityRequierd;
                result.ResultType = ResultMessageType.error.ToString();
                return Json(result);
            }
            if (!string.IsNullOrWhiteSpace(FileNo))
            {
                if (blEmployees.IsExistFileNo(FileNo, EntityEmpID))
                {
                    result.Message = Homemade.UI.Resources.Homemade.FileNoIsExists;
                    result.ResultType = ResultMessageType.error.ToString();
                    return Json(result);
                }
            }
            return null;
        }
        public bool GetPermissionUser(int action)
        {
            var user = bLUser.GetUserInfo(User.GetUserIdInt());
            int UserTypeId = user.UserType;
            int controller = (int)ControllerEnum.Employees;
             
             
            return _bLPermission.GetPermissionByUserAndController(user.UserName, controller, action);
        }
        #endregion
    }
}
