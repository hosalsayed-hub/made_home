using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Homemade.BLL.General;
using Homemade.BLL.ViewModel.Setting;
using Homemade.BLL.ViewModel.Site;
using Homemade.DAL.Contract;
using Homemade.Model.Setting;

namespace Homemade.BLL.Setting
{
    public class BlDepartments
    {
        #region Decleration
        private readonly BLGeneral _blGeneral;
        private readonly IUOW Uow;
        public BlDepartments(IUOW _uow, BLGeneral blGeneral)
        {
            _blGeneral = blGeneral;
            this.Uow = _uow;
        }
        #endregion
        #region Helpers
        /// Check If Departments Name Is Used Before Or Not اختبار هل اسم الدولة عربي موجود من قبل ام لا
        public bool IsExist(int DepartmentsID, string DepartmentsName, Language language) => DepartmentsID != 0 ? (language == Language.Arabic ? Uow.Departments.GetAll().Any(p => p.DepartmentsID != DepartmentsID && p.NameAR.Trim() == DepartmentsName && !p.IsDeleted) : Uow.Departments.GetAll().Any(p => p.DepartmentsID != DepartmentsID && p.NameEN.Trim() == DepartmentsName && !p.IsDeleted)) : (language == Language.Arabic ? Uow.Departments.GetAll().Any(p => p.NameAR.Trim() == DepartmentsName && !p.IsDeleted) : Uow.Departments.GetAll().Any(p => p.NameEN.Trim() == DepartmentsName && !p.IsDeleted));
        public List<DepartmentsViewModel> GetDepartmentstaghelper(string lang , string MainPathView) => Uow.Departments.GetAll(p => !p.IsDeleted).OrderByDescending(p => p.CreateDate).Select(p => new DepartmentsViewModel()
        {
            DepartmentsID = p.DepartmentsID,
            DepartmentsNameAR = p.NameAR,
            DepartmentsNameEN = p.NameEN,
            MainCategoryID = p.MainCategoryID , 
            MainCatgoryName = p.MainCategory!=null ? lang == "ar" ? p.MainCategory.NameAR : p.MainCategory.NameEN : "", 
            IsEnable = p.IsEnable,
            EnableDate = p.EnableDate,
            IsDeleted = p.IsDeleted,
            CreateDate = p.CreateDate,
            DeleteDate = p.DeleteDate,
            UserId = p.UserId,
            UserDelete = p.UserDelete,
            UserEnable = p.UserEnable,
            DescriptionAr = !string.IsNullOrEmpty(p.DescriptionAr) ? p.DescriptionAr : "",
            DescriptionEn = !string.IsNullOrEmpty(p.DescriptionEn) ? p.DescriptionEn : "",
            UpdateDate = p.UpdateDate,
            UserUpdate = p.UserUpdate,
            DepartmentsGuid = p.DepartmentsGuid,
            DepartmentsName = lang == "ar" ? p.NameAR : p.NameEN,
            IsEnableString = lang == "ar" ? (p.IsEnable ? "مفعل"  : "غير مفعل") : (p.IsEnable ? "Enabled" : "Disabled"),
            Image = !string.IsNullOrEmpty(p.Image) ? (MainPathView + p.Image) : "/Images/noImage.png",
            SiteImage = !string.IsNullOrEmpty(p.SiteImage) ? (MainPathView + p.SiteImage) : "/Images/noImage.png",
            Arrange = p.Arrange
        }).ToList();
        #endregion
        #region Actions
        /// Add New Departments اضافة دولة
        public bool Insert(DepartmentsViewModel DepartmentsModel, out int DepartmentsID)
        {
            Departments Departments = new Departments
            {
                NameAR = DepartmentsModel.DepartmentsNameAR,
                NameEN = DepartmentsModel.DepartmentsNameEN,
                CreateDate = _blGeneral.GetDateTimeNow(),
                UserId = DepartmentsModel.UserId,
                IsDeleted = false,
                IsEnable = true,
                DescriptionAr = DepartmentsModel.DescriptionAr,
                DescriptionEn = DepartmentsModel.DescriptionEn,
                MainCategoryID = DepartmentsModel.MainCategoryID , 
                Image = DepartmentsModel.Image , 
                SiteImage = DepartmentsModel.SiteImage , 
                Arrange = DepartmentsModel.Arrange
            };

            Departments = Uow.Departments.Insert(Departments);
            Uow.Commit();
            DepartmentsID = Departments.DepartmentsID;
            return true;
        }
        /// Delete Departments By ID حذف الدولة
        public bool Delete(int id, int userId)
        {
            var Departments = GetById(id);
            if (Departments != null)
            {
                Departments.IsDeleted = true;
                Departments.UserDelete = userId;
                Departments.DeleteDate = _blGeneral.GetDateTimeNow();
                Uow.Departments.Update(Departments);
                Uow.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool ChangeStatus(int id, int userId)
        {
            var data = Uow.Departments.GetAll(p => p.DepartmentsID == id).FirstOrDefault();

            if (data != null)
            {
                data.UserEnable = userId;
                data.IsEnable = data.IsEnable == Convert.ToBoolean(StatusEnable.Enable) ? Convert.ToBoolean(StatusEnable.Disable) : Convert.ToBoolean(StatusEnable.Enable);
                data.EnableDate = _blGeneral.GetDateTimeNow();
                data = Uow.Departments.Update(data);
                Uow.Commit();
                return true;
            }
            return false;
        }
        /// Update Departments تعديل الدولة
        public bool Update(DepartmentsViewModel model)
        {
            IQueryable<Departments> data = Uow.Departments.GetAll(p => p.DepartmentsID == model.DepartmentsID);
            if (data != null)
            {
                Departments Departments = data.FirstOrDefault();
                Departments.NameAR = model.DepartmentsNameAR;
                Departments.NameEN = model.DepartmentsNameEN;
                Departments.UpdateDate = model.UpdateDate;
                Departments.UserUpdate = model.UserUpdate;
                Departments.DescriptionEn = model.DescriptionEn;
                Departments.DescriptionAr = model.DescriptionAr;
                Departments.Arrange = model.Arrange;
                if (!string.IsNullOrWhiteSpace(model.SiteImage))
                {
                    Departments.SiteImage = model.SiteImage;
                }
                if (!string.IsNullOrWhiteSpace(model.Image))
                {
                    Departments.Image = model.Image;
                }
                Departments = Uow.Departments.Update(Departments);
                Uow.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }
        public Departments GetById(int id) => Uow.Departments.GetAll(x => x.DepartmentsID == id).FirstOrDefault();
        #endregion
        #region GetDepartments
        /// Get All Departments جلب الدول
      
        public List<Departments> GetAllDepartments()
        {
            var data = Uow.Departments.GetAll(x => !x.IsDeleted).ToList();
            return data;
        }
        public List<DeptViewModelApi> GetAllDepartmentsApi(string Lang,string DeptPath)
        {
            var data = Uow.Departments.GetAll(x => !x.IsDeleted && x.IsEnable).Select(s=> new DeptViewModelApi() { 
            title = Lang == "ar" ? s.NameAR : s.NameEN,
            image = !string.IsNullOrWhiteSpace(s.Image) ? DeptPath + s.Image : "",
            departmentID = s.DepartmentsID
            }).ToList();
            return data;
        }
        public List<DepartmentsViewModelApi> GetAllDepartmentsViewModelApi(string lang) => Uow.Departments.GetAll(x => !x.IsDeleted && x.IsEnable).Select(s => new DepartmentsViewModelApi()
        { name = lang == "ar" ? s.NameAR : s.NameEN, departmentsID = s.DepartmentsID }).ToList();

        public List<DeptViewModel> GetAllDepartments(string Lang, string DeptPath)
        {
            var data = Uow.Departments.GetAll(x => !x.IsDeleted && x.IsEnable).Select(s => new DeptViewModel()
            {
                title = Lang == "ar" ? s.NameAR : s.NameEN,
                image = !string.IsNullOrWhiteSpace(s.SiteImage) ? DeptPath + s.SiteImage : "",
                departmentID = s.DepartmentsID,
                DepartmentsGuid = s.DepartmentsGuid,
                Arrange = s.Arrange,
                Isunique = s.Isunique,
                
 
            }).ToList();
            return data;
        }

        public SiteDepartmentsViewModel GetSiteDepartmentsViewModelByGuid(Guid DepartmentsGuid, string lang)
        {
            var getData = Uow.Departments.GetAll(x => !x.IsDeleted && x.DepartmentsGuid == DepartmentsGuid)
                .Select(p => new SiteDepartmentsViewModel()
                {
                    DepartmentsGuid = p.DepartmentsGuid,
                    DepartmentsID = p.DepartmentsID,
                    DepartmentsName = lang == "ar" ? p.NameAR : p.NameEN,
                }).FirstOrDefault();
            return getData;
        }
        #endregion
    }
}
