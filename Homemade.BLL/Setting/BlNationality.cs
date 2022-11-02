using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Homemade.BLL.General;
using Homemade.BLL.ViewModel.Setting;
using Homemade.DAL.Contract;
using Homemade.Model.Setting;

namespace Homemade.BLL.Setting
{
    public class BlNationality
    {
        #region Decleration
        private readonly BLGeneral _blGeneral;
        private readonly IUOW Uow;
        public BlNationality(IUOW _uow, BLGeneral blGeneral)
        {
            _blGeneral = blGeneral;
            this.Uow = _uow;
        }
        #endregion
        #region Helpers
        /// Check If Nationality Name Is Used Before Or Not اختبار هل اسم الجنسية عربي موجود من قبل ام لا
        public bool IsExist(int NationalityID, string NationalityName, Language language) => NationalityID != 0 ? (language == Language.Arabic ? Uow.Nationality.GetAll().Any(p => p.NationalityID != NationalityID && p.NameAR.Trim() == NationalityName && !p.IsDeleted) : Uow.Nationality.GetAll().Any(p => p.NationalityID != NationalityID && p.NameEN.Trim() == NationalityName && !p.IsDeleted)) : (language == Language.Arabic ? Uow.Nationality.GetAll().Any(p => p.NameAR.Trim() == NationalityName && !p.IsDeleted) : Uow.Nationality.GetAll().Any(p => p.NameEN.Trim() == NationalityName && !p.IsDeleted));
        public List<NationalityViewModel> GetNationalitytaghelper(string lang) => Uow.Nationality.GetAll(p => !p.IsDeleted).OrderBy(p => p.NameAR).Select(p => new NationalityViewModel()
        {
            NationalityID = p.NationalityID,
            NationalityNameAR = p.NameAR,
            NationalityNameEN = p.NameEN,
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
            NationalityGuid = p.NationalityGuid,
            NationalityName = lang == "ar" ? p.NameAR : p.NameEN,
            IsEnableString = lang == "ar" ? (p.IsEnable ? "مفعل" : "غير مفعل") : (p.IsEnable ? "Enabled" : "Disabled"),
        }).OrderByDescending(x => x.CreateDate).ToList();
        public List<NationalityViewModelApi> GetNationalities(string accLanguage) => Uow.Nationality.GetAll(p => !p.IsDeleted).OrderBy(p => p.NameAR).Select(p => new NationalityViewModelApi()
        {
            nationalityID = p.NationalityID,
            name = accLanguage == "ar" ? p.NameAR : p.NameEN
        }).ToList();
        #endregion
        #region Actions
        /// Add New Nationality اضافة جنسية
        public bool Insert(NationalityViewModel NationalityModel, out int NationalityID)
        {
            Nationality Nationality = new Nationality
            {
                NameAR = NationalityModel.NationalityNameAR,
                NameEN = NationalityModel.NationalityNameEN,
                CreateDate = _blGeneral.GetDateTimeNow(),
                UserId = NationalityModel.UserId,
                IsDeleted = false,
                IsEnable = true,
                DescriptionAr = NationalityModel.DescriptionAr,
                DescriptionEn = NationalityModel.DescriptionEn,
            };
            Nationality = Uow.Nationality.Insert(Nationality);
            Uow.Commit();
            NationalityID = Nationality.NationalityID;
            return true;
        }
        /// Delete Nationality By ID حذف الجنسية
        public bool Delete(int id, int userId)
        {
            var Nationality = GetById(id);
            if (Nationality != null)
            {
                Nationality.IsDeleted = true;
                Nationality.UserDelete = userId;
                Nationality.DeleteDate = _blGeneral.GetDateTimeNow();
                Uow.Nationality.Update(Nationality);
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
            var data = Uow.Nationality.GetAll(p => p.NationalityID == id).FirstOrDefault();

            if (data != null)
            {
                data.UserEnable = userId;
                data.IsEnable = data.IsEnable == Convert.ToBoolean(StatusEnable.Enable) ? Convert.ToBoolean(StatusEnable.Disable) : Convert.ToBoolean(StatusEnable.Enable);
                data.EnableDate = _blGeneral.GetDateTimeNow();
                data = Uow.Nationality.Update(data);
                Uow.Commit();
                return true;
            }
            return false;
        }
        /// Update Nationality تعديل الجنسية
        public bool Update(NationalityViewModel model)
        {
            IQueryable<Nationality> data = Uow.Nationality.GetAll(p => p.NationalityID == model.NationalityID);
            if (data != null)
            {
                Nationality Nationality = data.FirstOrDefault();
                Nationality.NameAR = model.NationalityNameAR;
                Nationality.NameEN = model.NationalityNameEN;
                Nationality.UpdateDate = model.UpdateDate;
                Nationality.UserUpdate = model.UserUpdate;
                Nationality.DescriptionEn = model.DescriptionEn;
                Nationality.DescriptionAr = model.DescriptionAr;
                Nationality = Uow.Nationality.Update(Nationality);
                Uow.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }
        public Nationality GetById(int id) => Uow.Nationality.GetAll(x => x.NationalityID == id).FirstOrDefault();
        #endregion
        #region GetNationality
        /// Get All Nationality جلب الجنسيات
        public List<Nationality> GetAllNationality()
        {
            var data = Uow.Nationality.GetAll(x => !x.IsDeleted).OrderBy(x => x.NameAR).ToList();
            return data;
        }
        /// <summary>
        /// Get First Delivery Type
        /// </summary>
        /// <returns></returns>
        public Nationality GetFirstNationality() => Uow.Nationality.GetAll(x => !x.IsDeleted).OrderBy(x => x.NameAR).FirstOrDefault();
        #endregion
    }
}
