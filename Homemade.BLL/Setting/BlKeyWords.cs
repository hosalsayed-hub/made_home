using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Homemade.BLL.General;
using Homemade.BLL.ViewModel.Setting;
using Homemade.DAL.Contract;
using Homemade.Model.Setting;

namespace Homemade.BLL.Setting
{
    public class BlKeyWords
    {
        #region Decleration
        private readonly BLGeneral _blGeneral;
        private readonly IUOW Uow;
        public BlKeyWords(IUOW _uow, BLGeneral blGeneral)
        {
            _blGeneral = blGeneral;
            this.Uow = _uow;
        }
        #endregion
        #region Helpers
        /// Check If KeyWords Name Is Used Before Or Not اختبار هل اسم كلمات مفتاحية عربي موجود من قبل ام لا
        public bool IsExist(int KeyWordsID, string KeyWordsName, Language language) => KeyWordsID != 0 ? (language == Language.Arabic ? Uow.KeyWords.GetAll().Any(p => p.KeyWordsID != KeyWordsID && p.NameAR.Trim() == KeyWordsName && !p.IsDeleted) : Uow.KeyWords.GetAll().Any(p => p.KeyWordsID != KeyWordsID && p.NameEN.Trim() == KeyWordsName && !p.IsDeleted)) : (language == Language.Arabic ? Uow.KeyWords.GetAll().Any(p => p.NameAR.Trim() == KeyWordsName && !p.IsDeleted) : Uow.KeyWords.GetAll().Any(p => p.NameEN.Trim() == KeyWordsName && !p.IsDeleted));
        public List<KeyWordsViewModel> GetKeyWordstaghelper(string lang) => Uow.KeyWords.GetAll(p => !p.IsDeleted).OrderByDescending(p => p.CreateDate).Select(p => new KeyWordsViewModel()
        {
            KeyWordsID = p.KeyWordsID,
            KeyWordsNameAR = p.NameAR,
            KeyWordsNameEN = p.NameEN,
            DepartmentID = p.DepartmentsID.HasValue? p.DepartmentsID.Value : 0, 
            DepartmentName = p.Departments!=null ? lang == "ar" ? p.Departments.NameAR : p.Departments.NameEN : "", 
            IsEnable = p.IsEnable,
            EnableDate = p.EnableDate,
            IsDeleted = p.IsDeleted,
            CreateDate = p.CreateDate,
            DeleteDate = p.DeleteDate,
            UserId = p.UserId,
            UserDelete = p.UserDelete,
            UpdateDate = p.UpdateDate,
            UserUpdate = p.UserUpdate,
            KeyWordsGuid = p.KeyWordsGuid,
            KeyWordsName = lang == "ar" ? p.NameAR : p.NameEN,
            IsEnableString = lang == "ar" ? (p.IsEnable ? "مفعل"  : "غير مفعل") : (p.IsEnable ? "Enabled" : "Disabled"),
        }).ToList();
        #endregion
        #region Actions
        /// Add New KeyWords اضافة كلمات مفتاحية
        public bool Insert(KeyWordsViewModel KeyWordsModel, out int KeyWordsID)
        {
            KeyWords KeyWords = new KeyWords
            {
                NameAR = KeyWordsModel.KeyWordsNameAR,
                NameEN = KeyWordsModel.KeyWordsNameEN,
                CreateDate = _blGeneral.GetDateTimeNow(),
                UserId = KeyWordsModel.UserId,
                IsDeleted = false,
                IsEnable = true,
                DepartmentsID = KeyWordsModel.DepartmentID
            };
            KeyWords = Uow.KeyWords.Insert(KeyWords);
            Uow.Commit();
            KeyWordsID = KeyWords.KeyWordsID;
            return true;
        }
        /// Delete KeyWords By ID حذف كلمات مفتاحيةة
        public bool Delete(int id, int userId)
        {
            var KeyWords = GetById(id);
            if (KeyWords != null)
            {
                KeyWords.IsDeleted = true;
                KeyWords.UserDelete = userId;
                KeyWords.DeleteDate = _blGeneral.GetDateTimeNow();
                Uow.KeyWords.Update(KeyWords);
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
            var data = Uow.KeyWords.GetAll(p => p.KeyWordsID == id).FirstOrDefault();

            if (data != null)
            {
                data.UserEnable = userId;
                data.IsEnable = data.IsEnable == Convert.ToBoolean(StatusEnable.Enable) ? Convert.ToBoolean(StatusEnable.Disable) : Convert.ToBoolean(StatusEnable.Enable);
                data.EnableDate = _blGeneral.GetDateTimeNow();
                data = Uow.KeyWords.Update(data);
                Uow.Commit();
                return true;
            }
            return false;
        }
        /// Update KeyWords تعديل كلمات مفتاحيةة
        public bool Update(KeyWordsViewModel model)
        {
            IQueryable<KeyWords> data = Uow.KeyWords.GetAll(p => p.KeyWordsID == model.KeyWordsID);
            if (data != null)
            {
                KeyWords KeyWords = data.FirstOrDefault();
                KeyWords.NameAR = model.KeyWordsNameAR;
                KeyWords.NameEN = model.KeyWordsNameEN;
                KeyWords.UpdateDate = model.UpdateDate;
                KeyWords.UserUpdate = model.UserUpdate;
                KeyWords.DepartmentsID = model.DepartmentID;
                KeyWords = Uow.KeyWords.Update(KeyWords);
                Uow.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }
        public KeyWords GetById(int id) => Uow.KeyWords.GetAll(x => x.KeyWordsID == id).FirstOrDefault();
        #endregion
        #region GetKeyWords
        /// Get All KeyWords جلب كلمات مفتاحية
        public List<KeyWords> GetAllKeyWords()
        {
            var data = Uow.KeyWords.GetAll(x => !x.IsDeleted).ToList();
            return data;
        }
        public List<KeyWords> GetAllKeyWordssByListDepartment(string[] ListDepartmentID)
        {
            if (ListDepartmentID != null)
            {
                if (ListDepartmentID.Count(p => !string.IsNullOrEmpty(p)) == 0)
                {
                    ListDepartmentID = null;
                }
            }
            return Uow.KeyWords.GetAll(x => !x.IsDeleted && (ListDepartmentID != null ? ListDepartmentID.Any(y => y == x.DepartmentsID.ToString()) : false)).ToList();
        }
        #endregion
    }
}
