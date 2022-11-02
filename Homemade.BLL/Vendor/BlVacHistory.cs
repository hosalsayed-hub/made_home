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
   public class BlVacHistory
    {
        #region Declartion
        private readonly IUOW Uow;
        private readonly BLGeneral _bLGeneral;
        public BlVacHistory(IUOW _uow, BLGeneral bLGeneral)
        {
            this.Uow = _uow;
            _bLGeneral = bLGeneral;
        }
        #endregion
        #region Helper
        public VacHistory GetVacHistoryById(int VacHistoryID)
        {
            return Uow.VacHistory.GetAll(x => x.VacHistoryID == VacHistoryID && !x.IsDeleted).FirstOrDefault();
        }
        public List<VacHistoryViewModel> GetAllVacHistory(string lang)
        {
            var data = Uow.VacHistory.GetAll(x => !x.IsDeleted).Select(s=>new VacHistoryViewModel() { 
               VacHistoryID = s.VacHistoryID , 
               VendorName =lang == "ar"? s.Vendors.FirstNameAr +" "+ s.Vendors.SeconedNameAr : s.Vendors.FirstNameEn + " " + s.Vendors.SeconedNameEn ,
               IsReturnString = lang == "ar" ? (!s.IsReturn ? "اجازة" : "قيد العمل") : (!s.IsReturn ? "Vacation" : "On Work"),
               VacFrom = s.VacFrom , 
               VacTo = s.VacTo , 
            }).ToList();
            return data;
        }
        public List<VacHistoryViewModel> GetAllVacHistoryByVendor(int VendorsID, string lang)
        {
            var data = Uow.VacHistory.GetAll(x => !x.IsDeleted
            && x.VendorsID == VendorsID).Select(s => new VacHistoryViewModel()
            {
                VacHistoryID = s.VacHistoryID,
                VendorName = lang == "ar" ? s.Vendors.FirstNameAr + " " + s.Vendors.SeconedNameAr : s.Vendors.FirstNameEn + " " + s.Vendors.SeconedNameEn,
                IsReturnString = lang == "ar" ? (!s.IsReturn ? "اجازة" : "قيد العمل") : (!s.IsReturn ? "Vacation" : "On Work"),
                VacFrom = s.VacFrom,
                VacTo = s.VacTo,
            }).ToList();
            return data;
        }
        #endregion
    }
}
