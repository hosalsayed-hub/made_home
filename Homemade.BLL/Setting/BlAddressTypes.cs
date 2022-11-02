using Homemade.BLL.General;
using Homemade.BLL.ViewModel.Setting;
using Homemade.DAL.Contract;
using Homemade.Model.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.Setting
{
    public class BlAddressTypes
    {
        #region Decleration
        private readonly BLGeneral _blGeneral;
        private readonly IUOW Uow;
        public BlAddressTypes(IUOW _uow, BLGeneral blGeneral)
        {
            _blGeneral = blGeneral;
            this.Uow = _uow;
        }
        #endregion
        #region Helpers
        /// Check If Bank Name Is Used Before Or Not اختبار هل اسم نوع العنوانة عربي موجود من قبل ام لا
        public bool IsExist(int activityID, string activityName, Language language) => activityID != 0 ? (language == Language.Arabic ? Uow.AddressTypes.GetAll().Any(p => p.AddressTypesID != activityID && p.NameAR.Trim() == activityName && !p.IsDeleted) : Uow.AddressTypes.GetAll().Any(p => p.AddressTypesID != activityID && p.NameEN.Trim() == activityName && !p.IsDeleted)) : (language == Language.Arabic ? Uow.AddressTypes.GetAll().Any(p => p.NameAR.Trim() == activityName && !p.IsDeleted) : Uow.AddressTypes.GetAll().Any(p => p.NameEN.Trim() == activityName && !p.IsDeleted));
        #endregion
        #region Actions
        /// Add New Bank اضافة نوع العنوان
        public bool Insert(AddressTypesViewModel activityModel, out int activityID)
        {
            AddressTypes activity = new AddressTypes
            {
                NameAR = activityModel.NameAR,
                NameEN = activityModel.NameEN,
                CreateDate = _blGeneral.GetDateTimeNow(),
                UserId = activityModel.UserId,
                IsEnable = true
            };
            activity = Uow.AddressTypes.Insert(activity);
            Uow.Commit();
            activityID = activity.AddressTypesID;
            return true;
        }
        /// Delete Bank By ID حذف نوع العنوانة
        public bool Delete(int id, int userId)
        {
            var Bank = GetById(id);
            if (Bank != null)
            {
                Bank.IsDeleted = true;
                Bank.UserDelete = userId;
                Bank.DeleteDate = _blGeneral.GetDateTimeNow();
                Uow.AddressTypes.Update(Bank);
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
            var data = Uow.AddressTypes.GetAll(p => p.AddressTypesID == id).FirstOrDefault();

            if (data != null)
            {
                data.UserEnable = userId;
                data.IsEnable = data.IsEnable == Convert.ToBoolean(StatusEnable.Enable) ? Convert.ToBoolean(StatusEnable.Disable) : Convert.ToBoolean(StatusEnable.Enable);
                data.EnableDate = _blGeneral.GetDateTimeNow();
                data = Uow.AddressTypes.Update(data);
                Uow.Commit();
                return true;
            }
            return false;
        }
        /// Update Bank تعديل نوع العنوانة
        public bool Update(AddressTypesViewModel model)
        {
            IQueryable<AddressTypes> data = Uow.AddressTypes.GetAll(p => p.AddressTypesID == model.AddressTypesID);
            if (data != null)
            {
                AddressTypes activity = data.FirstOrDefault();
                activity.NameAR = model.NameAR;
                activity.NameEN = model.NameEN;
                activity.UpdateDate = model.UpdateDate;
                activity.UserUpdate = model.UserUpdate;
                activity = Uow.AddressTypes.Update(activity);
                Uow.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }
        public AddressTypes GetById(int id) => Uow.AddressTypes.GetAll(x => x.AddressTypesID == id).FirstOrDefault();
        #endregion
        #region GetAddressTypes
        /// Get All AddressTypes جلب نوع العنوان
        public List<AddressTypesViewModel> GetAddressTypestaghelper() => Uow.AddressTypes.GetAll(p => !p.IsDeleted).OrderByDescending(p => p.CreateDate).Select(p => new AddressTypesViewModel()
        {  AddressTypesID = p.AddressTypesID, NameAR = p.NameAR, NameEN = p.NameEN, IsEnable = p.IsEnable }).ToList();
        public List<AddressTypesViewModel> GetAddressTypesApi(string lang) => Uow.AddressTypes.GetAll(p => !p.IsDeleted).OrderByDescending(p => p.CreateDate).Select(p => new AddressTypesViewModel()
        { AddressTypesID = p.AddressTypesID, name = lang == "ar" ? p.NameAR : p.NameEN }).ToList();
        public List<AddressTypes> GetAllAddressTypes()
        {
            var data = Uow.AddressTypes.GetAll(x => !x.IsDeleted).ToList();
            return data;
        }
        #endregion
    }
}
