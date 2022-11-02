using System;
using System.Collections.Generic;
using System.Linq;
using Homemade.BLL.General;
using Homemade.BLL.ViewModel.Order;
using Homemade.DAL.Contract;
using Homemade.Model.Order;

namespace Homemade.BLL.Order
{
    public class BlOrderNotesAdmin
    {
        #region Decleration
        private readonly BLGeneral _blGeneral;
        private readonly IUOW Uow;
        public BlOrderNotesAdmin(IUOW _uow, BLGeneral blGeneral)
        {
            _blGeneral = blGeneral;
            this.Uow = _uow;
        }
        #endregion
        #region Helpers
        public OrderNotesAdmin GetById(int id) => Uow.OrderNotesAdmin.GetAll(x => x.OrderNotesAdminID == id).FirstOrDefault();
        public List<OrderNotesAdminViewModel> GetOrderNotesAdminViewModelByOrderVendor(int OrderVendorID, string lang) => Uow.OrderNotesAdmin.GetAll(p => !p.IsDeleted && p.OrderVendorID == OrderVendorID).OrderByDescending(p => p.CreateDate).Select(p => new OrderNotesAdminViewModel()
        {
            OrderNotesAdminID = p.OrderNotesAdminID,
            Notes = p.Notes,
            IsEnable = p.IsEnable,
            EnableDate = p.EnableDate,
            IsDeleted = p.IsDeleted,
            CreateDate = p.CreateDate,
            DeleteDate = p.DeleteDate,
            UserId = p.UserId,
            UserDelete = p.UserDelete,
            UserEnable = p.UserEnable,
            UpdateDate = p.UpdateDate,
            UserUpdate = p.UserUpdate,
            OrderNotesAdminGuid = p.OrderNotesAdminGuid,
            OrderVendorID = p.OrderVendorID,
            CreateDateString = p.CreateDate.ToString("dd/MM/yyyy hh:mm tt"),
            UserName = lang == "ar" ? ((p.User.UserType == (int)UserTypeEnum.Admin || p.User.UserType == (int)UserTypeEnum.Employee) ? p.User.Employees.Where(s => s.UserId == p.UserId).FirstOrDefault().FirstNameAR
                : p.User.UserType == (int)UserTypeEnum.Customer ? p.User.Customers.Where(s => s.UserId == p.UserId).FirstOrDefault().FirstName
                : p.User.UserType == (int)UserTypeEnum.Vendor ? p.User.Vendors.Where(s => s.UserId == p.UserId).FirstOrDefault().FirstNameAr
                : p.User.UserType == (int)UserTypeEnum.ShipingCompany ? p.OrderVendor.ShippingCompany.NameAr
             : p.User.Drivers.Where(s => s.UserId == p.UserId).FirstOrDefault().NameAr) :
                ((p.User.UserType == (int)UserTypeEnum.Admin || p.User.UserType == (int)UserTypeEnum.Employee) ? p.User.Employees.Where(s => s.UserId == p.UserId).FirstOrDefault().FirstNameEN
                : p.User.UserType == (int)UserTypeEnum.Customer ? p.User.Customers.Where(s => s.UserId == p.UserId).FirstOrDefault().FirstName
                : p.User.UserType == (int)UserTypeEnum.Vendor ? p.User.Vendors.Where(s => s.UserId == p.UserId).FirstOrDefault().FirstNameEn
                : p.User.UserType == (int)UserTypeEnum.ShipingCompany ? p.OrderVendor.ShippingCompany.NameEn
                : p.User.Drivers.Where(s => s.UserId == p.UserId).FirstOrDefault().NameEn)
        }).ToList();
        #endregion
        #region Actions
        public bool Insert(int OrderVendorID, string Notes, int UserId)
        {
            OrderNotesAdmin OrderNotesAdmin = new OrderNotesAdmin
            {
                Notes = Notes,
                OrderVendorID = OrderVendorID,
                CreateDate = _blGeneral.GetDateTimeNow(),
                UserId = UserId,
                IsDeleted = false,
                IsEnable = true,
                OrderNotesAdminGuid = Guid.NewGuid(),

            };

            OrderNotesAdmin = Uow.OrderNotesAdmin.Insert(OrderNotesAdmin);
            Uow.Commit();
            return true;
        }
        public bool Delete(int id, int userId)
        {
            var OrderNotesAdmin = GetById(id);
            if (OrderNotesAdmin != null)
            {
                OrderNotesAdmin.IsDeleted = true;
                OrderNotesAdmin.UserDelete = userId;
                OrderNotesAdmin.DeleteDate = _blGeneral.GetDateTimeNow();
                Uow.OrderNotesAdmin.Update(OrderNotesAdmin);
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
            var data = Uow.OrderNotesAdmin.GetAll(p => p.OrderNotesAdminID == id).FirstOrDefault();

            if (data != null)
            {
                data.UserEnable = userId;
                data.IsEnable = data.IsEnable == Convert.ToBoolean(StatusEnable.Enable) ? Convert.ToBoolean(StatusEnable.Disable) : Convert.ToBoolean(StatusEnable.Enable);
                data.EnableDate = _blGeneral.GetDateTimeNow();
                data = Uow.OrderNotesAdmin.Update(data);
                Uow.Commit();
                return true;
            }
            return false;
        }
        public bool Update(OrderNotesAdminViewModel model)
        {
            IQueryable<OrderNotesAdmin> data = Uow.OrderNotesAdmin.GetAll(p => p.OrderNotesAdminID == model.OrderNotesAdminID);
            if (data != null)
            {
                OrderNotesAdmin OrderNotesAdmin = data.FirstOrDefault();
                OrderNotesAdmin.Notes = model.Notes;
                OrderNotesAdmin.UpdateDate = model.UpdateDate;
                OrderNotesAdmin.UserUpdate = model.UserUpdate;
              
                OrderNotesAdmin = Uow.OrderNotesAdmin.Update(OrderNotesAdmin);
                Uow.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

    }
}
