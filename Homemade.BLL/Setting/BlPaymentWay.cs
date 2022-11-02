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
    public class BlPaymentWay
    {
        #region Decleration
        private readonly BLGeneral _blGeneral;
        private readonly IUOW Uow;
        public BlPaymentWay(IUOW _uow, BLGeneral blGeneral)
        {
            _blGeneral = blGeneral;
            this.Uow = _uow;
        }
        #endregion
        #region Helpers
        /// Check If Payment_Way Name Is Used Before Or Not اختبار هل اسم طريقة الدفع  عربي موجود من قبل ام لا
        public bool IsExist(int paymentWayID, string paymentWayName, Language language) => paymentWayID != 0 ? (language == Language.Arabic ? Uow.PaymentWay.GetAll().Any(p => p.PaymentWayID != paymentWayID && p.NameAR.Trim() == paymentWayName && !p.IsDeleted) : Uow.PaymentWay.GetAll().Any(p => p.PaymentWayID != paymentWayID && p.NameEN.Trim() == paymentWayName && !p.IsDeleted)) : (language == Language.Arabic ? Uow.PaymentWay.GetAll().Any(p => p.NameAR.Trim() == paymentWayName && !p.IsDeleted) : Uow.PaymentWay.GetAll().Any(p => p.NameEN.Trim() == paymentWayName && !p.IsDeleted));
        #endregion
        #region Actions
        /// Add New Payment_Way اضافة دولة
        public bool Insert(PaymentWayViewModel paymentWayModel, out int paymentWayID)
        {
            PaymentWay paymentWay = new PaymentWay
            {
                NameAR = paymentWayModel.PaymentWayNameAR,
                NameEN = paymentWayModel.PaymentWayNameEN,
                CreateDate = _blGeneral.GetDateTimeNow(),
                UserId = paymentWayModel.UserId ,
                IsEnable = true
            };
            paymentWay = Uow.PaymentWay.Insert(paymentWay);
            Uow.Commit();
            paymentWayID = paymentWay.PaymentWayID;
            return true;
        }
        /// Delete Payment_Way By ID حذف طريقة الدفع 
        public bool Delete(int id, int userId)
        {
            var Payment_Way = GetById(id);
            if (Payment_Way != null)
            {
                Payment_Way.IsDeleted = true;
                Payment_Way.UserDelete = userId;
                Payment_Way.DeleteDate = _blGeneral.GetDateTimeNow();
                Uow.PaymentWay.Update(Payment_Way);
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
            var data = Uow.PaymentWay.GetAll(p => p.PaymentWayID == id).FirstOrDefault();

            if (data != null)
            {
                data.UserEnable = userId;
                data.IsEnable = data.IsEnable == Convert.ToBoolean(StatusEnable.Enable) ? Convert.ToBoolean(StatusEnable.Disable) : Convert.ToBoolean(StatusEnable.Enable);
                data.EnableDate = _blGeneral.GetDateTimeNow();
                data = Uow.PaymentWay.Update(data);
                Uow.Commit();
                return true;
            }
            return false;
        }
        /// Update Payment_Way تعديل طريقة الدفع 
        public bool Update(PaymentWayViewModel model)
        {
            IQueryable<PaymentWay> data = Uow.PaymentWay.GetAll(p => p.PaymentWayID == model.PaymentWayID);
            if (data != null)
            {
                PaymentWay paymentWay = data.FirstOrDefault();
                paymentWay.NameAR = model.PaymentWayNameAR;
                paymentWay.NameEN = model.PaymentWayNameEN;
                paymentWay.UpdateDate = model.UpdateDate;
                paymentWay.UserUpdate = model.UserUpdate;
                paymentWay = Uow.PaymentWay.Update(paymentWay);
                Uow.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }
        public PaymentWay GetById(int id) => Uow.PaymentWay.GetAll(x => x.PaymentWayID == id).FirstOrDefault();
        #endregion
        #region GetPaymentWay
        /// Get All PaymentWay جلب الدول
        public List<PaymentWayViewModel> GetPaymentWaytaghelper() => Uow.PaymentWay.GetAll(p => !p.IsDeleted).OrderByDescending(p => p.CreateDate).Select(p => new PaymentWayViewModel()
        { PaymentWayID = p.PaymentWayID, PaymentWayNameAR = p.NameAR, PaymentWayNameEN = p.NameEN , IsEnable = p.IsEnable}).ToList();
        public List<PaymentWay> GetAllPaymentWay()
        {
            var data = Uow.PaymentWay.GetAll(x => !x.IsDeleted).ToList();
            return data;
        }
        #endregion
    }
}
