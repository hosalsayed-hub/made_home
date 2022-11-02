using Homemade.BLL.General;
using Homemade.BLL.ViewModel.Setting;
using Homemade.DAL.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.Setting
{
    public class BlDiscount
    {
        #region Decleration
        private readonly BLGeneral _blGeneral;
        private readonly IUOW Uow;
        public BlDiscount(IUOW _uow, BLGeneral blGeneral)
        {
            _blGeneral = blGeneral;
            this.Uow = _uow;
        }
        #endregion
        #region Helpers
        /// Check If Index Is Used Before Or Not
        public bool IsExist(int DiscountTypeID, int DiscountID) =>
            Uow.Discount.GetAll().Any(query => query.DiscountTypeID == DiscountTypeID && !query.IsDeleted && (DiscountID == 0 || query.DiscountID != DiscountID));
        #endregion
        #region Actions
        /// Add New Discount اضافة في جدول الخصم
        public bool Insert(DiscountViewModel DiscountViewModel, out int DiscountID, int UserId)
        {
            var Discount = new Model.Setting.Discount()
            {
                CreateDate = _blGeneral.GetDateTimeNow(),
                UserId = UserId,
                DiscountGuid = Guid.NewGuid(),
                IsDeleted = false,
                DiscountTypeID = DiscountViewModel.DiscountTypeID,
                DiscountValue = DiscountViewModel.DiscountValue,
            };
            Discount = Uow.Discount.Insert(Discount);
            Uow.Commit();
            DiscountID = Discount.DiscountID;
            return true;
        }
        /// Delete Discount By ID حذف من جدول ألفاصل الزمنى
        public bool Delete(int DiscountId, int UserId)
        {
            var Discount = GetById(DiscountId);
            if (Discount != null)
            {
                Discount.IsDeleted = true;
                Discount.UserDelete = UserId;
                Discount.DeleteDate = _blGeneral.GetDateTimeNow();
                Uow.Discount.Update(Discount);
                Uow.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }
        /// Update Discount التعديل في جدول الخصم
        public bool Update(DiscountViewModel model, int UserId)
        {
            var Discount = GetById(model.DiscountID);
            if (Discount != null)
            {
                Discount.DiscountTypeID = model.DiscountTypeID;
                Discount.DiscountValue = model.DiscountValue;
                Discount.UpdateDate = _blGeneral.GetDateTimeNow();
                Discount.UserUpdate = UserId;
                Uow.Discount.Update(Discount);
                Uow.Commit();
                return true;
            }
            else
            {
                return false;
            }

        }
        #endregion
        #region GetDiscount
        /// Get All Discount جلب كل الخصم
        public IQueryable<DiscountViewModel> GetAllDiscountViewModel()
        {
            return Uow.Discount.GetAll(p => !p.IsDeleted).OrderByDescending(p => p.CreateDate)
                .Select(p => new DiscountViewModel
                {
                    DiscountID = p.DiscountID,
                    DiscountTypeID = p.DiscountTypeID,
                    DiscountValue = p.DiscountValue,
                    CreateDate = p.CreateDate,
                    DeleteDate = p.DeleteDate,
                    DiscountGuid = p.DiscountGuid,
                    IsDeleted = p.IsDeleted,
                    UpdateDate = p.UpdateDate,
                    UserDelete = p.UserDelete,
                    UserId = p.UserId,
                    UserUpdate = p.UserUpdate,
                });
        }
        // جلب الخصم
        public Model.Setting.Discount GetById(int id) => Uow.Discount.GetAll(x => x.DiscountID == id).FirstOrDefault();
        public Model.Setting.Discount GetFirstDiscount() => Uow.Discount.GetAll(x => x.DiscountTypeID == (int)DiscountSettingEnum.Discount).FirstOrDefault();
        #endregion
    }
}
