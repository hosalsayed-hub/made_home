using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Homemade.BLL.General;
using Homemade.BLL.ViewModel;
using Homemade.BLL.ViewModel.Customer;
using Homemade.BLL.ViewModel.Employees;
using Homemade.BLL.ViewModel.Setting;
using Homemade.BLL.ViewModel.Vendor;
using Homemade.DAL.Contract;
using Homemade.Model;
using Homemade.Model.Order;
using Homemade.Model.Setting;
using Homemade.Model.Vendor;
using Microsoft.AspNetCore.Identity;

namespace Homemade.BLL.Vendor
{
   public class BlEnableHistory
    {
        #region Declartion
        private readonly IUOW Uow;
        private readonly BLGeneral _bLGeneral;
        public BlEnableHistory(IUOW _uow, BLGeneral bLGeneral)
        {
            this.Uow = _uow;
            _bLGeneral = bLGeneral;
        }
        #endregion
        #region Helper
        public EnableHistory GetEnableHistoryById(int EnableHistoryID)
        {
            return Uow.EnableHistory.GetAll(x => x.EnableHistoryID == EnableHistoryID && !x.IsDeleted).FirstOrDefault();
        }
        public List<EnableHistoryViewModel> GetAllEnableHistory(string lang)
        {
            var data = Uow.EnableHistory.GetAll(x => !x.IsDeleted).OrderByDescending(o => o.CreateDate).Select(s => new EnableHistoryViewModel()
            {
                EnableHistoryID = s.EnableHistoryID,
                VendorName = lang == "ar" ? s.Vendors.FirstNameAr + " " + s.Vendors.SeconedNameAr : s.Vendors.FirstNameEn + " " + s.Vendors.SeconedNameEn,

                UserTypeString = lang == "ar" ? (s.User.UserType == (int)UserTypeEnum.Admin ? "مسئول نظام" : s.User.UserType == (int)UserTypeEnum.Customer ? "عميل" : s.User.UserType == (int)UserTypeEnum.Employee ? "موظف نظام" : "مزود خدمة") :
               (s.User.UserType == (int)UserTypeEnum.Admin ? "Admin" : s.User.UserType == (int)UserTypeEnum.Customer ? "Customer" : s.User.UserType == (int)UserTypeEnum.Employee ? "Employee" : "Vendor"),

                UserName = lang == "ar" ? ((s.User.UserType == (int)UserTypeEnum.Admin || s.User.UserType == (int)UserTypeEnum.Employee) ? s.User.Employees.Where(s => s.UserId == s.UserId).FirstOrDefault().FirstNameAR : s.User.UserType == (int)UserTypeEnum.Customer ? s.User.Customers.Where(s => s.UserId == s.UserId).FirstOrDefault().FirstName : s.User.Vendors.Where(s => s.UserId == s.UserId).FirstOrDefault().FirstNameAr) :
                ((s.User.UserType == (int)UserTypeEnum.Admin || s.User.UserType == (int)UserTypeEnum.Employee) ? s.User.Employees.Where(s => s.UserId == s.UserId).FirstOrDefault().FirstNameEN : s.User.UserType == (int)UserTypeEnum.Customer ? s.User.Customers.Where(s => s.UserId == s.UserId).FirstOrDefault().FirstName : s.User.Vendors.Where(s => s.UserId == s.UserId).FirstOrDefault().FirstNameEn),

                StatusString = lang == "ar" ? (s.Status ? "مفعل" : "غير مفعل") : (s.Status ? "Enabled" : "Disabled"),
                CreateDate = s.CreateDate,
                CreateDateString = s.CreateDate.ToString(" hh:mm tt"),
            }).ToList();
            return data;
        }
        public List<EnableHistoryViewModel> GetAllEnableHistoryByVendors(int VendorsID, string lang)
        {
            var data = Uow.EnableHistory.GetAll(x => !x.IsDeleted
            && x.VendorsID == VendorsID
            ).OrderByDescending(o => o.CreateDate).Select(s => new EnableHistoryViewModel()
            {
                EnableHistoryID = s.EnableHistoryID,
                VendorName = lang == "ar" ? s.Vendors.FirstNameAr + " " + s.Vendors.SeconedNameAr : s.Vendors.FirstNameEn + " " + s.Vendors.SeconedNameEn,

                UserTypeString = lang == "ar" ? (s.User.UserType == (int)UserTypeEnum.Admin ? "مسئول نظام" : s.User.UserType == (int)UserTypeEnum.Customer ? "عميل" : s.User.UserType == (int)UserTypeEnum.Employee ? "موظف نظام" : "مزود خدمة") :
               (s.User.UserType == (int)UserTypeEnum.Admin ? "Admin" : s.User.UserType == (int)UserTypeEnum.Customer ? "Customer" : s.User.UserType == (int)UserTypeEnum.Employee ? "Employee" : "Vendor"),

                UserName = lang == "ar" ? ((s.User.UserType == (int)UserTypeEnum.Admin || s.User.UserType == (int)UserTypeEnum.Employee) ? s.User.Employees.Where(s => s.UserId == s.UserId).FirstOrDefault().FirstNameAR : s.User.UserType == (int)UserTypeEnum.Customer ? s.User.Customers.Where(s => s.UserId == s.UserId).FirstOrDefault().FirstName : s.User.Vendors.Where(s => s.UserId == s.UserId).FirstOrDefault().FirstNameAr) :
                ((s.User.UserType == (int)UserTypeEnum.Admin || s.User.UserType == (int)UserTypeEnum.Employee) ? s.User.Employees.Where(s => s.UserId == s.UserId).FirstOrDefault().FirstNameEN : s.User.UserType == (int)UserTypeEnum.Customer ? s.User.Customers.Where(s => s.UserId == s.UserId).FirstOrDefault().FirstName : s.User.Vendors.Where(s => s.UserId == s.UserId).FirstOrDefault().FirstNameEn),

                StatusString = lang == "ar" ? (s.Status ? "مفعل" : "غير مفعل") : (s.Status ? "Enabled" : "Disabled"),
                CreateDate = s.CreateDate,
                CreateDateString = s.CreateDate.ToString(" hh:mm tt"),
            }).ToList();
            return data;
        }
        #endregion
    }
}
