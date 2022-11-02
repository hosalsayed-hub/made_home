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
    public class BlPaymentStatus
    {
        #region Decleration
        private readonly BLGeneral _blGeneral;
        private readonly IUOW Uow;
        public BlPaymentStatus(IUOW _uow, BLGeneral blGeneral)
        {
            _blGeneral = blGeneral;
            this.Uow = _uow;
        }
        #endregion
        #region Helpers
        public bool IsExist(int activityID, string activityName, Language language) => activityID != 0 ? (language == Language.Arabic ? Uow.PaymentStatus.GetAll().Any(p => p.PaymentStatusID != activityID && p.NameAR.Trim() == activityName && !p.IsDeleted) : Uow.PaymentStatus.GetAll().Any(p => p.PaymentStatusID != activityID && p.NameEN.Trim() == activityName && !p.IsDeleted)) : (language == Language.Arabic ? Uow.PaymentStatus.GetAll().Any(p => p.NameAR.Trim() == activityName && !p.IsDeleted) : Uow.PaymentStatus.GetAll().Any(p => p.NameEN.Trim() == activityName && !p.IsDeleted));
        #endregion
        #region Actions
        public bool Insert(PaymentStatusViewModel activityModel, out int activityID)
        {
            PaymentStatus activity = new PaymentStatus
            {
                NameAR = activityModel.PaymentStatusNameAR,
                NameEN = activityModel.PaymentStatusNameEN,
                CreateDate = _blGeneral.GetDateTimeNow(),
                UserId = activityModel.UserId ,
                IsEnable = true
            };
            activity = Uow.PaymentStatus.Insert(activity);
            Uow.Commit();
            activityID = activity.PaymentStatusID;
            return true;
        }
        public bool Delete(int id, int userId)
        {
            var Bank = GetById(id);
            if (Bank != null)
            {
                Bank.IsDeleted = true;
                Bank.UserDelete = userId;
                Bank.DeleteDate = _blGeneral.GetDateTimeNow();
                Uow.PaymentStatus.Update(Bank);
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
            var data = Uow.PaymentStatus.GetAll(p => p.PaymentStatusID == id).FirstOrDefault();

            if (data != null)
            {
                data.UserEnable = userId;
                data.IsEnable = data.IsEnable == Convert.ToBoolean(StatusEnable.Enable) ? Convert.ToBoolean(StatusEnable.Disable) : Convert.ToBoolean(StatusEnable.Enable);
                data.EnableDate = _blGeneral.GetDateTimeNow();
                data = Uow.PaymentStatus.Update(data);
                Uow.Commit();
                return true;
            }
            return false;
        }
        public bool Update(PaymentStatusViewModel model)
        {
            IQueryable<PaymentStatus> data = Uow.PaymentStatus.GetAll(p => p.PaymentStatusID == model.PaymentStatusID);
            if (data != null)
            {
                PaymentStatus activity = data.FirstOrDefault();
                activity.NameAR = model.PaymentStatusNameAR;
                activity.NameEN = model.PaymentStatusNameEN;
                activity.UpdateDate = model.UpdateDate;
                activity.UserUpdate = model.UserUpdate;
                activity = Uow.PaymentStatus.Update(activity);
                Uow.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }
        public PaymentStatus GetById(int id) => Uow.PaymentStatus.GetAll(x => x.PaymentStatusID == id).FirstOrDefault();
        #endregion
        #region GetPaymentStatus
        public List<PaymentStatusViewModel> GetPaymentStatustaghelper() => Uow.PaymentStatus.GetAll(p => !p.IsDeleted).OrderByDescending(p => p.CreateDate).Select(p => new PaymentStatusViewModel()
        { PaymentStatusID = p.PaymentStatusID, PaymentStatusNameAR = p.NameAR, PaymentStatusNameEN = p.NameEN , IsEnable = p.IsEnable}).ToList();
        public List<PaymentStatus> GetAllPaymentStatus()
        {
            var data = Uow.PaymentStatus.GetAll(x => !x.IsDeleted).ToList();
            return data;
        }
        #endregion
    }
}
