using Homemade.BLL.ViewModel;
using Homemade.BLL.ViewModel.Permission;
using Homemade.BLL.ViewModel.Setting;
using Homemade.DAL;
using Homemade.DAL.Contract;
using Homemade.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Homemade.BLL
{
    public class BLPermission : BaseEntity, IDisposable
    {
        #region Decleration
        private readonly IUOW Uow;
        public BLPermission(IUOW _uow)
        {
            this.Uow = _uow;
        }
        bool disposed = false;

        readonly SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
            }

            disposed = true;
        }
        ~BLPermission()
        {
            Dispose(false);
        }
        #endregion
        #region Get
        public string GetAllPermissionControllerActionByRoleFromRoleConfig(int RoleID)
        {
            var roleConfig = Uow.RoleConfig.GetAll(p => p.RoleId == RoleID).Select(m => m.PermissionControllerActionID).ToList();
            return string.Join(",", roleConfig.ToArray());
        }
        public bool GetIsUserInRole(int RoleID, int userid)
        {
            return Uow.UserRole.GetAll(x => x.UserId == userid && x.RoleId == RoleID).Any();
        }
        public List<UserRoleViewModel> GetAllUserOperatorAndClient()
        {
            var List = new List<UserRoleViewModel>();
            var data = Uow.User.GetAll().Select(m => new UserRoleViewModel()
            {
                UserID = m.Id,
                RoleName = "",
                UserNameAR = m.UserName,
                UserNameEN = m.UserName,
            });
            foreach (var item in data)
            {
                item.UserNameAR = item.UserID + " - " + item.UserNameEN;
                item.UserNameEN = item.UserID + " - " + item.UserNameEN;
                List.Add(item);
            }
            return List;
        }
        public IQueryable<RoleViewModel> GetRoles(int RoleType)
        {
            var role = Uow.Role.GetAll(e => e.RoleTypeId == RoleType).Select(select => new RoleViewModel()
            {
                RoleId = select.Id,
                RoleName = select.Name,
            }).OrderBy(s => s.RoleName);
            return role;
        }
        public IQueryable<RoleViewModel> GetRoles(string lang)
        {
            var role = Uow.Role.GetAll().Select(select => new RoleViewModel()
            {
                RoleId = select.Id,
                RoleName = select.Name,
            });
            if (lang == "ar")
            {
                role = role.Select(m => new RoleViewModel()
                {
                    RoleId = m.RoleId,
                    RoleName = m.RoleId == (int)Rolenum.Admin ? "ادمن" : m.RoleId == (int)Rolenum.Employee ? "موظف" : m.RoleId == (int)Rolenum.Vendor ? "متجر" : m.RoleId == (int)Rolenum.Customer ? "عميل" : "",
                });
            }
            role.OrderBy(s => s.RoleName);
            return role;
        }
        public bool GetPermissionForAction(int controllerId, int actionId, string userId, IConfiguration Configuration)
        {
            var optionsBuilder = new DbContextOptionsBuilder<HomemadeContext>();
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("HomemadeDBConnection"));
            using (var context = new HomemadeContext(optionsBuilder.Options))
            {
                List<int> allowedRolesID = (from p in context.Permission
                                            join r in context.Roles on p.RoleId equals r.Id
                                            where p.PermissionControllerActions.PermissionActionID == actionId &&
                                                                                p.PermissionControllerActions.PermissionControllerID == controllerId
                                            select new
                                            {
                                                r.Id
                                            }).Select(x => x.Id).ToList();


                List<int> userRolesID = (from r in context.UserRoles
                                         where r.UserId == Convert.ToInt32(userId)
                                         select new
                                         {
                                             r.RoleId
                                         }).Select(x => x.RoleId).ToList();



                foreach (int ur in userRolesID)
                {
                    if (allowedRolesID.Contains(ur))
                    {
                        return true;
                    }
                }

                return false;
            }
        }
        public PermissionViewModel GetAllControllersByRole(int roleId, int userId)
        {
            PermissionViewModel result = new PermissionViewModel();
            result.Controllers = new List<PermissionControllerViewModel>();
            List<Permission> permissions = Uow.Permission.GetAll(p => p.RoleId == roleId && p.UserId == userId, "PermissionControllerActions").ToList();
            List<PermissionController> allControllers = Uow.PermissionController.GetAll(null
                , "PermissionControllerActions,PermissionControllerActions.PermissionActions").OrderByDescending(x => x.PermissionControllerID).ToList();
            PermissionControllerViewModel pcvm;
            PermissionActionViewModel pavm;
            foreach (var c in allControllers)
            {
                pcvm = new PermissionControllerViewModel();
                pcvm.ID = c.PermissionControllerID.ToString();
                pcvm.Name = /*c.PermissionControllerNameAr + " | " +*/ c.PermissionControllerNameEn;
                pcvm.Actions = new List<PermissionActionViewModel>();

                foreach (var ca in c.PermissionControllerActions)
                {
                    pavm = new PermissionActionViewModel();
                    pavm.ID = c.PermissionControllerID.ToString() + "-" + ca.PermissionControllerActionID.ToString();
                    pavm.Name = ca.PermissionActions.PermissionActionNameEn;
                    pavm.Allow = permissions.Any(x => x.PermissionControllerActionID == ca.PermissionControllerActionID);
                    pcvm.Actions.Add(pavm);
                }

                result.Controllers.Add(pcvm);
            }

            return result;
        }
        public List<PermissionControllerActionViewModel> GetAllPermissionControllerActionViewModelByRole(int roleId, int userId, bool IsAr)
        {
            var Controllers = new List<PermissionControllerActionViewModel>();
            if (Uow.Permission.GetAll(p => p.RoleId == roleId && p.UserId == userId /*&& p.Role.RoleTypeId == (int)RoleTypeEnum.Master*/).Any())
            {
                List<Permission> permissions = Uow.Permission.GetAll(p => p.RoleId == roleId && p.UserId == userId /*&& p.Role.RoleTypeId == (int)RoleTypeEnum.Master*/, "PermissionControllerActions").ToList();
                List<PermissionControllerAction> allPermissionControllerAction = Uow.PermissionControllerAction.GetAll().ToList();
                List<PermissionController> allControllers = Uow.PermissionController.GetAll(null
                    , "PermissionControllerActions,PermissionControllerActions.PermissionActions").OrderByDescending(x => x.PermissionControllerID).ToList();
                PermissionControllerActionViewModel pcvm;
                foreach (var c in allControllers)
                {
                    pcvm = new PermissionControllerActionViewModel();
                    pcvm.ControllerID = c.PermissionControllerID.ToString();
                    pcvm.ControllerName = IsAr ? c.PermissionControllerNameAr : c.PermissionControllerNameEn;
                    pcvm.PermissionControllerActionViewID = allPermissionControllerAction.Any(x => x.PermissionControllerID == c.PermissionControllerID && x.PermissionActionID == (int)ActionEnum.View) ? allPermissionControllerAction.Where(x => x.PermissionControllerID == c.PermissionControllerID && x.PermissionActionID == (int)ActionEnum.View).FirstOrDefault().PermissionControllerActionID : 0;
                    pcvm.PermissionControllerActionInsertID = allPermissionControllerAction.Any(x => x.PermissionControllerID == c.PermissionControllerID && x.PermissionActionID == (int)ActionEnum.Insert) ? allPermissionControllerAction.Where(x => x.PermissionControllerID == c.PermissionControllerID && x.PermissionActionID == (int)ActionEnum.Insert).FirstOrDefault().PermissionControllerActionID : 0;
                    pcvm.PermissionControllerActionUpdateID = allPermissionControllerAction.Any(x => x.PermissionControllerID == c.PermissionControllerID && x.PermissionActionID == (int)ActionEnum.Update) ? allPermissionControllerAction.Where(x => x.PermissionControllerID == c.PermissionControllerID && x.PermissionActionID == (int)ActionEnum.Update).FirstOrDefault().PermissionControllerActionID : 0;
                    pcvm.PermissionControllerActionDeleteID = allPermissionControllerAction.Any(x => x.PermissionControllerID == c.PermissionControllerID && x.PermissionActionID == (int)ActionEnum.Delete) ? allPermissionControllerAction.Where(x => x.PermissionControllerID == c.PermissionControllerID && x.PermissionActionID == (int)ActionEnum.Delete).FirstOrDefault().PermissionControllerActionID : 0;
                    pcvm.IsView = permissions.Any(x => x.PermissionControllerActions.PermissionControllerID == c.PermissionControllerID && x.PermissionControllerActions.PermissionActionID == (int)ActionEnum.View);
                    pcvm.IsInsert = permissions.Any(x => x.PermissionControllerActions.PermissionControllerID == c.PermissionControllerID && x.PermissionControllerActions.PermissionActionID == (int)ActionEnum.Insert);
                    pcvm.IsUpdate = permissions.Any(x => x.PermissionControllerActions.PermissionControllerID == c.PermissionControllerID && x.PermissionControllerActions.PermissionActionID == (int)ActionEnum.Update);
                    pcvm.IsDelete = permissions.Any(x => x.PermissionControllerActions.PermissionControllerID == c.PermissionControllerID && x.PermissionControllerActions.PermissionActionID == (int)ActionEnum.Delete);
                    Controllers.Add(pcvm);
                }
            }
            else
            {
                List<TempPermission> temppermissions = Uow.TempPermission.GetAll(p => p.RoleId == roleId && p.Role.RoleTypeId == (int)RoleTypeEnum.Master, "PermissionControllerActions").ToList();
                List<PermissionControllerAction> allPermissionControllerAction = Uow.PermissionControllerAction.GetAll().ToList();
                List<PermissionController> allControllers = Uow.PermissionController.GetAll(null
                    , "PermissionControllerActions,PermissionControllerActions.PermissionActions").OrderByDescending(x => x.PermissionControllerID).ToList();
                PermissionControllerActionViewModel pcvm;
                foreach (var c in allControllers)
                {
                    pcvm = new PermissionControllerActionViewModel();
                    pcvm.ControllerID = c.PermissionControllerID.ToString();
                    pcvm.ControllerName = IsAr ? c.PermissionControllerNameAr : c.PermissionControllerNameEn;
                    pcvm.PermissionControllerActionViewID = allPermissionControllerAction.Any(x => x.PermissionControllerID == c.PermissionControllerID && x.PermissionActionID == (int)ActionEnum.View) ? allPermissionControllerAction.Where(x => x.PermissionControllerID == c.PermissionControllerID && x.PermissionActionID == (int)ActionEnum.View).FirstOrDefault().PermissionControllerActionID : 0;
                    pcvm.PermissionControllerActionInsertID = allPermissionControllerAction.Any(x => x.PermissionControllerID == c.PermissionControllerID && x.PermissionActionID == (int)ActionEnum.Insert) ? allPermissionControllerAction.Where(x => x.PermissionControllerID == c.PermissionControllerID && x.PermissionActionID == (int)ActionEnum.Insert).FirstOrDefault().PermissionControllerActionID : 0;
                    pcvm.PermissionControllerActionUpdateID = allPermissionControllerAction.Any(x => x.PermissionControllerID == c.PermissionControllerID && x.PermissionActionID == (int)ActionEnum.Update) ? allPermissionControllerAction.Where(x => x.PermissionControllerID == c.PermissionControllerID && x.PermissionActionID == (int)ActionEnum.Update).FirstOrDefault().PermissionControllerActionID : 0;
                    pcvm.PermissionControllerActionDeleteID = allPermissionControllerAction.Any(x => x.PermissionControllerID == c.PermissionControllerID && x.PermissionActionID == (int)ActionEnum.Delete) ? allPermissionControllerAction.Where(x => x.PermissionControllerID == c.PermissionControllerID && x.PermissionActionID == (int)ActionEnum.Delete).FirstOrDefault().PermissionControllerActionID : 0;
                    pcvm.IsView = temppermissions.Any(x => x.PermissionControllerActions.PermissionControllerID == c.PermissionControllerID && x.PermissionControllerActions.PermissionActionID == (int)ActionEnum.View);
                    pcvm.IsInsert = temppermissions.Any(x => x.PermissionControllerActions.PermissionControllerID == c.PermissionControllerID && x.PermissionControllerActions.PermissionActionID == (int)ActionEnum.Insert);
                    pcvm.IsUpdate = temppermissions.Any(x => x.PermissionControllerActions.PermissionControllerID == c.PermissionControllerID && x.PermissionControllerActions.PermissionActionID == (int)ActionEnum.Update);
                    pcvm.IsDelete = temppermissions.Any(x => x.PermissionControllerActions.PermissionControllerID == c.PermissionControllerID && x.PermissionControllerActions.PermissionActionID == (int)ActionEnum.Delete);
                    Controllers.Add(pcvm);
                }
            }

            return Controllers;
        }
        public List<PermissionControllerActionViewModel> GetAllPermissionForComapny(string roles, int? userId, bool IsAr)
        {
            string[] rolesstr = roles.Trim().Split(new string[] { "," }, StringSplitOptions.None);
            var Controllers = new List<PermissionControllerActionViewModel>();
            List<Permission> permissions = Uow.Permission.GetAll(p => rolesstr.Any(s => s == p.RoleId.ToString()) && p.UserId == userId, "PermissionControllerActions").ToList();
            List<PermissionControllerAction> allPermissionControllerAction = Uow.PermissionControllerAction.GetAll(s => s.RoleConfig.Any(x => rolesstr.Any(s => s == x.RoleId.ToString()))).ToList();
            var allPermissionControllerActionint = Uow.PermissionControllerAction.GetAll(s => s.RoleConfig.Any(x => rolesstr.Any(s => s == x.RoleId.ToString()))).Select(s => s.PermissionControllerID).ToList();
            List<PermissionController> allControllers = Uow.PermissionController.GetAll(s => allPermissionControllerActionint.Any(x => x == s.PermissionControllerID)
                , "PermissionControllerActions,PermissionControllerActions.PermissionActions").OrderByDescending(x => x.PermissionControllerID).ToList();
            PermissionControllerActionViewModel pcvm;
            foreach (var c in allControllers)
            {
                pcvm = new PermissionControllerActionViewModel();
                pcvm.ControllerID = c.PermissionControllerID.ToString();
                pcvm.ControllerName = IsAr ? c.PermissionControllerNameAr : c.PermissionControllerNameEn;
                pcvm.PermissionControllerActionViewID = allPermissionControllerAction.Any(x => x.PermissionControllerID == c.PermissionControllerID && x.PermissionActionID == (int)ActionEnum.View) ? allPermissionControllerAction.Where(x => x.PermissionControllerID == c.PermissionControllerID && x.PermissionActionID == (int)ActionEnum.View).FirstOrDefault().PermissionControllerActionID : 0;
                pcvm.PermissionControllerActionInsertID = allPermissionControllerAction.Any(x => x.PermissionControllerID == c.PermissionControllerID && x.PermissionActionID == (int)ActionEnum.Insert) ? allPermissionControllerAction.Where(x => x.PermissionControllerID == c.PermissionControllerID && x.PermissionActionID == (int)ActionEnum.Insert).FirstOrDefault().PermissionControllerActionID : 0;
                pcvm.PermissionControllerActionUpdateID = allPermissionControllerAction.Any(x => x.PermissionControllerID == c.PermissionControllerID && x.PermissionActionID == (int)ActionEnum.Update) ? allPermissionControllerAction.Where(x => x.PermissionControllerID == c.PermissionControllerID && x.PermissionActionID == (int)ActionEnum.Update).FirstOrDefault().PermissionControllerActionID : 0;
                pcvm.PermissionControllerActionDeleteID = allPermissionControllerAction.Any(x => x.PermissionControllerID == c.PermissionControllerID && x.PermissionActionID == (int)ActionEnum.Delete) ? allPermissionControllerAction.Where(x => x.PermissionControllerID == c.PermissionControllerID && x.PermissionActionID == (int)ActionEnum.Delete).FirstOrDefault().PermissionControllerActionID : 0;
                pcvm.IsView = permissions.Any(x => x.PermissionControllerActions.PermissionControllerID == c.PermissionControllerID && x.PermissionControllerActions.PermissionActionID == (int)ActionEnum.View);
                pcvm.IsInsert = permissions.Any(x => x.PermissionControllerActions.PermissionControllerID == c.PermissionControllerID && x.PermissionControllerActions.PermissionActionID == (int)ActionEnum.Insert);
                pcvm.IsUpdate = permissions.Any(x => x.PermissionControllerActions.PermissionControllerID == c.PermissionControllerID && x.PermissionControllerActions.PermissionActionID == (int)ActionEnum.Update);
                pcvm.IsDelete = permissions.Any(x => x.PermissionControllerActions.PermissionControllerID == c.PermissionControllerID && x.PermissionControllerActions.PermissionActionID == (int)ActionEnum.Delete);

                if (pcvm.PermissionControllerActionViewID != 0)
                {
                    Controllers.Add(pcvm);
                }
            }

            return Controllers;
        }
        public List<PermissionControllerActionViewModel> GetAllPermissionControllerActionViewModelByRole(bool IsAr)
        {
            var Controllers = new List<PermissionControllerActionViewModel>();
            List<PermissionControllerAction> allPermissionControllerAction = Uow.PermissionControllerAction.GetAll().ToList();
            List<PermissionController> allControllers = Uow.PermissionController.GetAll(null
                , "PermissionControllerActions,PermissionControllerActions.PermissionActions").OrderByDescending(x => x.PermissionControllerID).ToList();
            PermissionControllerActionViewModel pcvm;
            foreach (var c in allControllers)
            {
                pcvm = new PermissionControllerActionViewModel();
                pcvm.ControllerID = c.PermissionControllerID.ToString();
                pcvm.ControllerName = IsAr ? c.PermissionControllerNameAr : c.PermissionControllerNameEn;
                pcvm.PermissionControllerActionViewID = allPermissionControllerAction.Any(x => x.PermissionControllerID == c.PermissionControllerID && x.PermissionActionID == (int)ActionEnum.View) ? allPermissionControllerAction.Where(x => x.PermissionControllerID == c.PermissionControllerID && x.PermissionActionID == (int)ActionEnum.View).FirstOrDefault().PermissionControllerActionID : 0;
                pcvm.PermissionControllerActionInsertID = allPermissionControllerAction.Any(x => x.PermissionControllerID == c.PermissionControllerID && x.PermissionActionID == (int)ActionEnum.Insert) ? allPermissionControllerAction.Where(x => x.PermissionControllerID == c.PermissionControllerID && x.PermissionActionID == (int)ActionEnum.Insert).FirstOrDefault().PermissionControllerActionID : 0;
                pcvm.PermissionControllerActionUpdateID = allPermissionControllerAction.Any(x => x.PermissionControllerID == c.PermissionControllerID && x.PermissionActionID == (int)ActionEnum.Update) ? allPermissionControllerAction.Where(x => x.PermissionControllerID == c.PermissionControllerID && x.PermissionActionID == (int)ActionEnum.Update).FirstOrDefault().PermissionControllerActionID : 0;
                pcvm.PermissionControllerActionDeleteID = allPermissionControllerAction.Any(x => x.PermissionControllerID == c.PermissionControllerID && x.PermissionActionID == (int)ActionEnum.Delete) ? allPermissionControllerAction.Where(x => x.PermissionControllerID == c.PermissionControllerID && x.PermissionActionID == (int)ActionEnum.Delete).FirstOrDefault().PermissionControllerActionID : 0;
                pcvm.IsView = false;
                pcvm.IsInsert = false;
                pcvm.IsUpdate = false;
                pcvm.IsDelete = false;
                Controllers.Add(pcvm);
            }

            return Controllers;
        }
        public bool GetPermissionByUserAndController(string userName, int PermissionControllerID, int PermissionActionID)
        {
            try
            {
                int userId = Uow.User.GetAll(e => e.UserName == userName).FirstOrDefault().Id;
                List<int> userRolesID = Uow.UserRole.GetAll(r => r.UserId == userId).Select(e => e.RoleId).ToList();
                return Uow.Permission.GetAll(p => p.PermissionControllerActions.PermissionActionID == PermissionActionID &&
                             p.PermissionControllerActions.PermissionControllerID == PermissionControllerID && userRolesID.Contains(p.RoleId) && p.UserId == userId).FirstOrDefault() != null;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public List<PermissionControllerActionViewModel> GetAllPermissionControllerActionViewModelByRoleForRoleConfigration(int roleId, bool IsAr)
        {
            var Controllers = new List<PermissionControllerActionViewModel>();
            List<RoleConfig> roleConfig = Uow.RoleConfig.GetAll(p => p.RoleId == roleId, "PermissionControllerActions").ToList();
            List<PermissionControllerAction> allPermissionControllerAction = Uow.PermissionControllerAction.GetAll().ToList();
            List<PermissionController> allControllers = Uow.PermissionController.GetAll(null
                , "PermissionControllerActions,PermissionControllerActions.PermissionActions").OrderByDescending(x => x.PermissionControllerID).ToList();
            PermissionControllerActionViewModel pcvm;
            foreach (var c in allControllers)
            {
                pcvm = new PermissionControllerActionViewModel();
                pcvm.ControllerID = c.PermissionControllerID.ToString();
                pcvm.ControllerName = IsAr ? c.PermissionControllerNameAr : c.PermissionControllerNameEn;
                pcvm.PermissionControllerActionViewID = allPermissionControllerAction.Any(x => x.PermissionControllerID == c.PermissionControllerID && x.PermissionActionID == (int)ActionEnum.View) ? allPermissionControllerAction.Where(x => x.PermissionControllerID == c.PermissionControllerID && x.PermissionActionID == (int)ActionEnum.View).FirstOrDefault().PermissionControllerActionID : 0;
                pcvm.PermissionControllerActionInsertID = allPermissionControllerAction.Any(x => x.PermissionControllerID == c.PermissionControllerID && x.PermissionActionID == (int)ActionEnum.Insert) ? allPermissionControllerAction.Where(x => x.PermissionControllerID == c.PermissionControllerID && x.PermissionActionID == (int)ActionEnum.Insert).FirstOrDefault().PermissionControllerActionID : 0;
                pcvm.PermissionControllerActionUpdateID = allPermissionControllerAction.Any(x => x.PermissionControllerID == c.PermissionControllerID && x.PermissionActionID == (int)ActionEnum.Update) ? allPermissionControllerAction.Where(x => x.PermissionControllerID == c.PermissionControllerID && x.PermissionActionID == (int)ActionEnum.Update).FirstOrDefault().PermissionControllerActionID : 0;
                pcvm.PermissionControllerActionDeleteID = allPermissionControllerAction.Any(x => x.PermissionControllerID == c.PermissionControllerID && x.PermissionActionID == (int)ActionEnum.Delete) ? allPermissionControllerAction.Where(x => x.PermissionControllerID == c.PermissionControllerID && x.PermissionActionID == (int)ActionEnum.Delete).FirstOrDefault().PermissionControllerActionID : 0;
                pcvm.IsView = roleConfig.Any(x => x.PermissionControllerActions.PermissionControllerID == c.PermissionControllerID && x.PermissionControllerActions.PermissionActionID == (int)ActionEnum.View);
                pcvm.IsInsert = roleConfig.Any(x => x.PermissionControllerActions.PermissionControllerID == c.PermissionControllerID && x.PermissionControllerActions.PermissionActionID == (int)ActionEnum.Insert);
                pcvm.IsUpdate = roleConfig.Any(x => x.PermissionControllerActions.PermissionControllerID == c.PermissionControllerID && x.PermissionControllerActions.PermissionActionID == (int)ActionEnum.Update);
                pcvm.IsDelete = roleConfig.Any(x => x.PermissionControllerActions.PermissionControllerID == c.PermissionControllerID && x.PermissionControllerActions.PermissionActionID == (int)ActionEnum.Delete);
                Controllers.Add(pcvm);
            }


            return Controllers;
        }
        #endregion
        #region Actions
        public bool CreateUserRole(int RoleID, int userid)
        {
            var CustomUserRole = new IdentityUserRole<int> { RoleId = RoleID, UserId = userid };
            Uow.UserRole.Insert(CustomUserRole);
            Uow.Commit();
            return true;
        }
        public bool DeleteUserRole(string Rolename, int userid)
        {
            var dataRoles = Uow.UserRole.GetAll(x => x.UserId == userid).Select(x => x.RoleId).ToList();
            var Returndata = Uow.Role.GetAll(s => dataRoles.Any(r => r == s.Id)).Where(e => e.Name == Rolename).FirstOrDefault();
            var CustomUserRole = Uow.UserRole.GetAll(x => x.UserId == userid && x.RoleId == Returndata.Id).FirstOrDefault();
            Uow.UserRole.Delete(CustomUserRole);
            Uow.Commit();
            return true;
        }
        public bool DeleteAllRolePermissions(int roleId)
        {
            Uow.Permission.DeleteRang(Uow.Permission.GetAll(s => s.RoleId == roleId));
            Uow.Commit();
            return true;
        }
        public bool DeleteAllRoleTempPermission(int roleId)
        {
            Uow.TempPermission.DeleteRang(Uow.TempPermission.GetAll(s => s.RoleId == roleId));
            Uow.Commit();
            return true;
        }
        public bool EditRolePermissions(PermissionViewModel model)
        {
            try
            {
                List<Permission> oldPermissions = Uow.Permission.GetAll(s => s.RoleId == model.RoleID && s.UserId == model.UserId).ToList();
                string[] permissions = model.CheckedItems.Trim().Split(new string[] { "," }, StringSplitOptions.None);
                int permissionControllerActionID;
                foreach (string p in permissions)
                {
                    if (!string.IsNullOrEmpty(p))
                    {
                        permissionControllerActionID = Convert.ToInt32(p); // permissionControllerActionID
                        if (oldPermissions.FirstOrDefault(d => d.PermissionControllerActionID == permissionControllerActionID) == null)
                        {
                            var d = Uow.Permission.Insert(new Permission()
                            {
                                UserId = model.UserId,
                                PermissionControllerActionID = Convert.ToInt32(p),
                                RoleId = model.RoleID,
                                PermissionGuid = Guid.NewGuid(),
                            });
                        }
                        else
                        {
                            oldPermissions.Remove(oldPermissions.FirstOrDefault(d => d.PermissionControllerActionID == permissionControllerActionID));
                        }
                    }
                }
                if (oldPermissions.Count > 0)
                {
                    foreach (var oldP in oldPermissions)
                    {
                        Uow.Permission.Delete(oldP);

                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            Uow.Commit();
            return true;
        }
        public bool EditRolePermissionsNoCommit(PermissionViewModel model)
        {
            try
            {
                List<Permission> oldPermissions = Uow.Permission.GetAll(s => s.RoleId == model.RoleID && s.UserId == model.UserId).ToList();
                string[] permissions = model.CheckedItems.Trim().Split(new string[] { "," }, StringSplitOptions.None);
                int permissionControllerActionID;
                foreach (string p in permissions)
                {
                    if (!string.IsNullOrEmpty(p))
                    {
                        permissionControllerActionID = Convert.ToInt32(p); // permissionControllerActionID
                        if (oldPermissions.FirstOrDefault(d => d.PermissionControllerActionID == permissionControllerActionID) == null)
                        {
                            var d = Uow.Permission.Insert(new Permission()
                            {
                                UserId = model.UserId,
                                PermissionControllerActionID = Convert.ToInt32(p),
                                RoleId = model.RoleID,
                                PermissionGuid = Guid.NewGuid(),
                            });
                        }
                        else
                        {
                            oldPermissions.Remove(oldPermissions.FirstOrDefault(d => d.PermissionControllerActionID == permissionControllerActionID));
                        }
                    }
                }
                if (oldPermissions.Count > 0)
                {
                    foreach (var oldP in oldPermissions)
                    {
                        Uow.Permission.Delete(oldP);

                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            Uow.Commit();
            return true;
        }
        public bool UpdateRolePermissionsForAllUsers(RoleConfigrationViewModel model)
        {
            try
            {
                var ListUserId = Uow.UserRole.GetAll(x => x.RoleId == model.RoleID).Select(z => z.UserId).ToList();
                foreach (var UserId in ListUserId)
                {
                    List<Permission> oldPermissions = Uow.Permission.GetAll(s => s.RoleId == model.RoleID && s.UserId == UserId).ToList();
                    string[] permissions = model.CheckedItems.Trim().Split(new string[] { "," }, StringSplitOptions.None);
                    int permissionControllerActionID;
                    foreach (string p in permissions)
                    {
                        if (!string.IsNullOrEmpty(p))
                        {
                            permissionControllerActionID = Convert.ToInt32(p); // permissionControllerActionID
                            if (oldPermissions.FirstOrDefault(d => d.PermissionControllerActionID == permissionControllerActionID) == null)
                            {
                                var d = Uow.Permission.Insert(new Permission()
                                {
                                    UserId = UserId,
                                    PermissionControllerActionID = Convert.ToInt32(p),
                                    RoleId = model.RoleID,
                                    PermissionGuid = Guid.NewGuid(),
                                });
                            }
                            else
                            {
                                oldPermissions.Remove(oldPermissions.FirstOrDefault(d => d.PermissionControllerActionID == permissionControllerActionID));
                            }
                        }
                    }
                    if (oldPermissions.Count > 0)
                    {
                        foreach (var oldP in oldPermissions)
                        {
                            Uow.Permission.Delete(oldP);

                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            Uow.Commit();
            return true;
        }
        public bool InsertTempPermissions(int RoleID, string CheckedItems)
        {
            string[] permissions = CheckedItems.Trim().Split(new string[] { "," }, StringSplitOptions.None);
            int permissionControllerActionID;
            foreach (string p in permissions)
            {
                if (!string.IsNullOrEmpty(p))
                {
                    permissionControllerActionID = Convert.ToInt32(p); // permissionControllerActionID
                    var d = Uow.TempPermission.Insert(new TempPermission()
                    {
                        PermissionControllerActionID = Convert.ToInt32(p),
                        RoleId = RoleID,
                        TempPermissionGuid = Guid.NewGuid(),
                    });
                }
            }
            Uow.Commit();
            return true;
        }
        public bool EditRoleConfigration(RoleConfigrationViewModel model)
        {
            try
            {
                List<RoleConfig> oldRoleConfig = Uow.RoleConfig.GetAll(s => s.RoleId == model.RoleID).ToList();
                string[] roleConfig = model.CheckedItems.Trim().Split(new string[] { "," }, StringSplitOptions.None);
                int permissionControllerActionID;
                foreach (string p in roleConfig)
                {
                    if (!string.IsNullOrEmpty(p))
                    {
                        permissionControllerActionID = Convert.ToInt32(p); // permissionControllerActionID
                        if (oldRoleConfig.FirstOrDefault(d => d.PermissionControllerActionID == permissionControllerActionID) == null)
                        {
                            var d = Uow.RoleConfig.Insert(new RoleConfig()
                            {
                                PermissionControllerActionID = Convert.ToInt32(p),
                                RoleId = model.RoleID
                            });
                        }
                        else
                        {
                            oldRoleConfig.Remove(oldRoleConfig.FirstOrDefault(d => d.PermissionControllerActionID == permissionControllerActionID));
                        }
                    }
                }
                if (oldRoleConfig.Count > 0)
                {
                    foreach (var oldP in oldRoleConfig)
                    {
                        Uow.RoleConfig.Delete(oldP);

                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            Uow.Commit();
            return true;
        }

        #endregion
        
    }
}
