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
    public class BlActivity
    {
        #region Decleration
        private readonly BLGeneral _blGeneral;
        private readonly IUOW Uow;
        public BlActivity(IUOW _uow, BLGeneral blGeneral)
        {
            _blGeneral = blGeneral;
            this.Uow = _uow;
        }
        #endregion
        #region Helpers
        /// Check If Bank Name Is Used Before Or Not اختبار هل اسم النشاطة عربي موجود من قبل ام لا
        public bool IsExist(int activityID, string activityName, Language language) => activityID != 0 ? (language == Language.Arabic ? Uow.Activity.GetAll().Any(p => p.ActivityID != activityID && p.NameAR.Trim() == activityName && !p.IsDeleted) : Uow.Activity.GetAll().Any(p => p.ActivityID != activityID && p.NameEN.Trim() == activityName && !p.IsDeleted)) : (language == Language.Arabic ? Uow.Activity.GetAll().Any(p => p.NameAR.Trim() == activityName && !p.IsDeleted) : Uow.Activity.GetAll().Any(p => p.NameEN.Trim() == activityName && !p.IsDeleted));
        #endregion
        #region Actions
        /// Add New Bank اضافة النشاط
        public bool Insert(ActivityViewModel activityModel, out int activityID)
        {
            Activity activity = new Activity
            {
                NameAR = activityModel.ActivityNameAR,
                NameEN = activityModel.ActivityNameEN,
                CreateDate = _blGeneral.GetDateTimeNow(),
                UserId = activityModel.UserId ,
                IsEnable = true
            };
            activity = Uow.Activity.Insert(activity);
            Uow.Commit();
            activityID = activity.ActivityID;
            return true;
        }
        /// Delete Bank By ID حذف النشاطة
        public bool Delete(int id, int userId)
        {
            var Bank = GetById(id);
            if (Bank != null)
            {
                Bank.IsDeleted = true;
                Bank.UserDelete = userId;
                Bank.DeleteDate = _blGeneral.GetDateTimeNow();
                Uow.Activity.Update(Bank);
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
            var data = Uow.Activity.GetAll(p => p.ActivityID == id).FirstOrDefault();

            if (data != null)
            {
                data.UserEnable = userId;
                data.IsEnable = data.IsEnable == Convert.ToBoolean(StatusEnable.Enable) ? Convert.ToBoolean(StatusEnable.Disable) : Convert.ToBoolean(StatusEnable.Enable);
                data.EnableDate = _blGeneral.GetDateTimeNow();
                data = Uow.Activity.Update(data);
                Uow.Commit();
                return true;
            }
            return false;
        }
        /// Update Bank تعديل النشاطة
        public bool Update(ActivityViewModel model)
        {
            IQueryable<Activity> data = Uow.Activity.GetAll(p => p.ActivityID == model.ActivityID);
            if (data != null)
            {
                Activity activity = data.FirstOrDefault();
                activity.NameAR = model.ActivityNameAR;
                activity.NameEN = model.ActivityNameEN;
                activity.UpdateDate = model.UpdateDate;
                activity.UserUpdate = model.UserUpdate;
                activity = Uow.Activity.Update(activity);
                Uow.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }
        public Activity GetById(int id) => Uow.Activity.GetAll(x => x.ActivityID == id).FirstOrDefault();
        #endregion
        #region GetActivity
        /// Get All Activity جلب النشاط
        public List<ActivityViewModel> GetActivitytaghelper() => Uow.Activity.GetAll(p => !p.IsDeleted).OrderByDescending(p => p.CreateDate).Select(p => new ActivityViewModel()
        { ActivityID = p.ActivityID, ActivityNameAR = p.NameAR, ActivityNameEN = p.NameEN , IsEnable = p.IsEnable}).ToList();
        public List<ActivityViewModelApi> GetActivityApi(string lang) => Uow.Activity.GetAll(p => !p.IsDeleted).OrderByDescending(p => p.CreateDate).Select(p => new ActivityViewModelApi()
        { activityID = p.ActivityID, activityName = lang == "ar" ?  p.NameAR: p.NameEN }).ToList();
        public List<Activity> GetAllActivity()
        {
            var data = Uow.Activity.GetAll(x => !x.IsDeleted).ToList();
            return data;
        }
        #endregion
    }
}
