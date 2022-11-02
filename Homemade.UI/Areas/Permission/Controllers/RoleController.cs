using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Homemade.BLL;
using Homemade.BLL.General;
using Homemade.BLL.ViewModel;
using Homemade.Model;
using Homemade.UI.Attributes;
using Homemade.UI.InfraStructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Homemade.UI.Areas.Permission.Controllers
{
    public class RoleController : Controller
    {
        #region Declarations
        private readonly RoleManager<CustomRole> _roleManager;
        private readonly ResultMessage result;
        private readonly BLGeneral _bLGeneral;
        private readonly BLPermission _bLPermission;
        public RoleController(RoleManager<CustomRole> roleManager, ResultMessage result, BLGeneral bLGeneral, BLPermission bLPermission)
        {
            _bLGeneral = bLGeneral;
            _roleManager = roleManager;
            this.result = result;
            _bLPermission = bLPermission;
        }
        #endregion
        #region View
        [CustomeAuthoriz((int)ControllerEnum.Role, (int)ActionEnum.View)]
        public IActionResult Index()
        {
            return View();
        }
        #endregion
        #region Actions
        [HttpPost]
        public JsonResult LoadTable()
        {
            int pageNumber = int.Parse(Request.Form["iDisplayStart"]);
            int skip = (pageNumber);
            int take = (skip + 1) * AppConst.NumberOfObjectsPerPage;

            var data = _roleManager.Roles.Where(e => e.RoleTypeId == (int)RoleTypeEnum.Master).Select(s => new
            {
                s.Id,
                s.Name
            });

            if (Request.Form.ContainsKey("sSearch"))
            {
                string s = Request.Form["sSearch"];
                if (!string.IsNullOrEmpty(s))
                {
                    data = data.Where(x => x.Name.Contains(s));
                }
            }



            var takenData = data.Take(take).Skip(skip);
            int totalCount = data.Count();
            return Json(new
            {
                iTotalRecords = totalCount,
                iTotalDisplayRecords = totalCount,
                aaData = takenData
            });
        }
        [HttpPost]
        public JsonResult LoadTableRolePermissions()
        {
            List<PermissionControllerActionViewModel> controller = new List<PermissionControllerActionViewModel>();
            controller = _bLPermission.GetAllPermissionControllerActionViewModelByRole(Request.Cookies.IsArabic());
            var data = controller;
            int totalCount = data.Count();
            return Json(new
            {
                iTotalRecords = totalCount,
                iTotalDisplayRecords = totalCount,
                aaData = data
            });
        }
        [CustomeAuthoriz((int)ControllerEnum.Role, (int)ActionEnum.Insert)]
        [HttpPost]
        public async Task<JsonResult> Index(CustomRole model)
        {
            try
            {
                #region Vaildation
                if (string.IsNullOrWhiteSpace(model.Name))
                {
                    result.Message = Homemade.BLL.Resources.HomemadeErrorMessages.ArNameRequierd;
                    result.ResultType = ResultMessageType.error.ToString();
                    return Json(result);
                }
                if (await _roleManager.RoleExistsAsync(model.Name))
                {
                    result.ResultType = ResultMessageType.error.ToString();
                    result.Message = Resources.Homemade.NameIsExists;
                    return Json(new { result = result });
                }
                #endregion
                #region SaveData
                string OperationType = "add";
                if (model.Id == 0)
                {
                    #region AddRole
                    //model.BranchesID = (int)BranchesEnum.Master_Branch;
                    var identityResult = await _roleManager.CreateAsync(model);
                    if (identityResult.Succeeded == false)
                    {
                        result.ResultType = ResultMessageType.error.ToString();
                        result.Message = Resources.Homemade.NameIsExists;
                        return Json(new { result = result });
                    }
                    else
                    {
                        result.ResultType = ResultMessageType.success.ToString();
                        result.Message = Resources.Homemade.SuccessSaveMessage;
                        return Json(new { result, model.Name, model.Id, OperationType });
                    }
                    #endregion
                }
                else
                {
                    #region UpdateRole
                    var d = await _roleManager.FindByIdAsync(model.Id.ToString());
                    d.Name = model.Name;
                    var identityResult = await _roleManager.UpdateAsync(d);
                    if (identityResult.Succeeded == false)
                    {
                        result.ResultType = ResultMessageType.error.ToString();
                        result.Message = Resources.Homemade.NameIsExists;
                        return Json(new { result = result });
                    }
                    else
                    {
                        result.ResultType = ResultMessageType.success.ToString();
                        result.Message = Resources.Homemade.SuccessSaveMessage;
                        OperationType = "update";
                        return Json(new { result, model.Name, model.Id, OperationType });
                    }
                    #endregion
                }
                #endregion
            }
            catch (System.Exception ex)
            {
                result.ResultType = ResultMessageType.error.ToString();
                result.Message = ex.Message.ToString();
                return Json(result);
            }
        }
        // حذف الدور
        [CustomeAuthoriz((int)ControllerEnum.Role, (int)ActionEnum.Delete)]
        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            CustomRole role = await _roleManager.FindByIdAsync(id.ToString());
            ResultMessage rResultMessage = new ResultMessage();
            try
            {
                if (_bLPermission.DeleteAllRolePermissions(role.Id))
                {
                    _bLPermission.DeleteAllRoleTempPermission(role.Id);
                    var identityResult = await _roleManager.DeleteAsync(role);

                    if (identityResult.Succeeded == false)
                    {
                        rResultMessage = new ResultMessage { Message = Resources.Homemade.FailDeleteMessage, ResultType = ResultMessageType.error.ToString() };
                    }
                    else
                    {
                        rResultMessage = new ResultMessage { Message = Resources.Homemade.SuccessDeleteMessage, ResultType = ResultMessageType.success.ToString() };
                    }
                }
                else
                {
                    rResultMessage = new ResultMessage { Message = Resources.Homemade.FailDeleteMessage, ResultType = ResultMessageType.error.ToString() };
                }
            }
            catch (System.Exception ex)
            {
                rResultMessage = new ResultMessage { Message = Resources.Homemade.FailDeleteMessage, ResultType = ResultMessageType.error.ToString() };
            }
            return Json(rResultMessage);
        }
        [HttpPost]
        public ActionResult InsertTempPermissions(int RoleID, string CheckedItems)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (CheckedItems != null)
                    {
                        if (_bLPermission.InsertTempPermissions(RoleID, CheckedItems))
                        {
                            result.ResultType = ResultMessageType.success.ToString();
                            result.Message = Resources.Homemade.SuccessSaveMessage;
                        }
                        else
                        {
                            result.ResultType = ResultMessageType.error.ToString();
                            result.Message = Resources.Homemade.FailSaveMessage;
                        }
                    }
                    else
                    {
                        result.ResultType = ResultMessageType.error.ToString();
                        result.Message = Resources.Homemade.FailSaveMessage;
                    }
                }
                else
                {
                    result.ResultType = ResultMessageType.error.ToString();
                    result.Message = Resources.Homemade.FailSaveMessage;
                }
            }
            catch (System.Exception ex)
            {
                result.ResultType = ResultMessageType.error.ToString();
                result.Message = Resources.Homemade.FailSaveMessage;
            }
            return Json(result);
        }
        #endregion
    }
}
