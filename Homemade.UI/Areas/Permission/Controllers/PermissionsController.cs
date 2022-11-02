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
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Homemade.UI.Areas.Permission.Controllers
{
    public class PermissionsController : Controller
    {
        #region Decleration
        readonly BLPermission _bLPermission;
        private readonly UserManager<User> _userManager;
        private readonly ResultMessage result;
        public PermissionsController(BLPermission bLPermission, UserManager<User> userManager, ResultMessage result)
        {
            _bLPermission = bLPermission;
            _userManager = userManager;
            this.result = result;
        }
        // GET: Permission
        #endregion
        #region MVC Views
        [CustomeAuthoriz((int)ControllerEnum.Permission, (int)ActionEnum.Insert)]
        public ActionResult UsersInRole()
        {
            return View();
        }
        [CustomeAuthoriz((int)ControllerEnum.Permission, (int)ActionEnum.Insert)]
        public ActionResult RolePermissions()
        {
            return View(new PermissionViewModel());
        }
        #endregion
        #region Actions

        /// <summary>
        /// عرض 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> LoadTable(int? UserId)
        {
            int pageNumber = int.Parse(Request.Form["iDisplayStart"]);
            int skip = (pageNumber);
            int take = (skip + 1) * AppConst.NumberOfObjectsPerPage;

            List<UserRoleViewModel> userRoles = new List<UserRoleViewModel>();

            if (UserId.HasValue)
            {
                var user = await _userManager.FindByIdAsync(UserId.ToString());
                var userRolesName = _userManager.GetRolesAsync(user).Result;
                foreach (var urn in userRolesName)
                {
                    userRoles.Add(new UserRoleViewModel()
                    {
                        RoleName = urn,
                        UserID = (int)UserId,
                    });
                }
            }
            var data = userRoles;
            var takenData = data.Take(take).Skip(skip);
            int totalCount = data.Count();
            return Json(new
            {
                iTotalRecords = totalCount,
                iTotalDisplayRecords = totalCount,
                aaData = takenData
            });
        }
        [CustomeAuthoriz((int)ControllerEnum.Permission, (int)ActionEnum.Insert)]
        [HttpPost]
        public async Task<JsonResult> UsersInRole(int RoleID, int UserID)
        {
            bool IsSuccess;
            string OperationType = "add";
            try
            {
                if (UserID != 0 && RoleID != 0)
                {
                    var user = await _userManager.FindByIdAsync(UserID.ToString());
                    if (!_bLPermission.GetIsUserInRole(RoleID, UserID))
                    {
                        var identityResult = _bLPermission.CreateUserRole(RoleID, UserID);
                        if (!identityResult)
                        {
                            IsSuccess = false;
                        }
                        else
                        {
                            IsSuccess = true;
                        }
                    }
                    else
                    {
                        IsSuccess = false;
                    }
                }
                else
                {
                    IsSuccess = false;
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
            }
            catch (Exception ex)
            {
                result.ResultType = ResultMessageType.error.ToString();
                result.Message = Resources.Homemade.FailSaveMessage;
            }
            return Json(new { result, UserID, OperationType });
        }
        [CustomeAuthoriz((int)ControllerEnum.Permission, (int)ActionEnum.Delete)]
        [HttpPost]
        public async Task<JsonResult> Delete(int id, string name)
        {

            bool Result = false;
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.FindByIdAsync(id.ToString());
                    var identityResult = _bLPermission.DeleteUserRole(name, id);
                    if (identityResult)
                    {
                        Result = true;
                    }
                    else
                    {
                        Result = false;
                    }
                }
                else
                {
                    Result = false;
                }
            }
            catch (Exception ex)
            {
            }
            ResultMessage rResultMessage = Result ?
                new ResultMessage { Message = Resources.Homemade.SuccessDeleteMessage, ResultType = ResultMessageType.success.ToString() }
                :
                new ResultMessage { Message = Resources.Homemade.FailDeleteMessage, ResultType = ResultMessageType.error.ToString() };
            return Json(rResultMessage);
        }
        public async Task<bool> JustUsersInRole(int RoleID, int UserID)
        {
            bool IsSuccess = false;
            try
            {
                if (UserID != 0 && RoleID != 0)
                {
                    var user = await _userManager.FindByIdAsync(UserID.ToString());
                    if (!_bLPermission.GetIsUserInRole(RoleID, UserID))
                    {
                        var identityResult = _bLPermission.CreateUserRole(RoleID, UserID);
                    }
                }
            }
            catch (Exception Ex)
            {
            }
            return IsSuccess;
        }
        [CustomeAuthoriz((int)ControllerEnum.Permission, (int)ActionEnum.Update)]
        [HttpPost]
        public async Task<ActionResult> RolePermissions(PermissionViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.CheckedItems != null)
                    {
                        var IsSave = await JustUsersInRole(model.RoleID, model.UserId);
                        if (_bLPermission.EditRolePermissions(model))
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
            catch (Exception ex)
            {
            }
            return Json(result);
        }
        #endregion
        #region Set Role Permissions
        [HttpPost]
        public JsonResult LoadTableRolePermissions(int? roleId, int? userId)
        {
            List<PermissionControllerActionViewModel> controller = new List<PermissionControllerActionViewModel>();

            if (userId.HasValue && roleId.HasValue)
            {
                controller = _bLPermission.GetAllPermissionControllerActionViewModelByRole((int)roleId, (int)userId, Request.Cookies.IsArabic());
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
        public JsonResult GetTreeNode(int? id, int? roleId, int? UserId)
        {
            if (roleId.HasValue)
            {
                var pvm = _bLPermission.GetAllControllersByRole(roleId ?? 0, UserId ?? 0);

                if (!id.HasValue)
                {
                    var node = (from e in pvm.Controllers
                                select new
                                {
                                    id = e.ID,
                                    Name = e.Name,
                                    hasChildren = true,
                                    @checked = e.Actions.Where(s => s.Allow).Count() == e.Actions.Count
                                }).ToList();
                    return Json(node);
                }
                else
                {
                    var node = (from e in pvm.Controllers.Where(s => s.ID == id.Value.ToString()).Select(d => d.Actions).FirstOrDefault()
                                select new
                                {
                                    id = e.ID,
                                    Name = e.Name,
                                    hasChildren = false,
                                    @checked = e.Allow
                                }).OrderBy(c => c.Name).ToList();
                    return Json(node);
                }
            }
            else
            {
                return Json(string.Empty);
            }
        }
        #endregion
        #region Role Configration
        public ActionResult RoleConfigration()
        {
            return View(new PermissionViewModel());
        }
        [HttpPost]
        public JsonResult LoadTableRoleConfigration(int? roleId)
        {
            List<PermissionControllerActionViewModel> controller = new List<PermissionControllerActionViewModel>();

            if (roleId.HasValue)
            {
                controller = _bLPermission.GetAllPermissionControllerActionViewModelByRoleForRoleConfigration((int)roleId, Request.Cookies.IsArabic());
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
        [HttpPost]
        public async Task<ActionResult> UpdateRoleConfigration(RoleConfigrationViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.CheckedItems != null)
                    {
                        if (_bLPermission.EditRoleConfigration(model))
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
            catch (Exception ex)
            {
            }
            return Json(result);
        }
        [HttpPost]
        public ActionResult UpdateRolePermissionsForAllUsers(RoleConfigrationViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.CheckedItems != null)
                    {
                        if (_bLPermission.UpdateRolePermissionsForAllUsers(model))
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
            catch (Exception ex)
            {
            }
            return Json(result);
        }
        #endregion
        #region Helper
        [HttpPost]
        public JsonResult GetRoles(int? type)
        {
            if (type.HasValue)
            {
                var RoleType = type == (int)JobTypeEnum.Admin ? (int)RoleTypeEnum.Master : (int)RoleTypeEnum.Entites;
                var role = _bLPermission.GetRoles(RoleType);
                return Json(role);
            }
            return Json("");
        }
        #endregion
    }
}
