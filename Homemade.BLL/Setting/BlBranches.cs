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
   public class BlBranches
    {
        #region Decleration
        private readonly BLGeneral _blGeneral;
        private readonly IUOW Uow;
        public BlBranches(IUOW _uow, BLGeneral blGeneral)
        {
            _blGeneral = blGeneral;
            this.Uow = _uow;
        }
        #endregion
        #region Action
        public bool Insert(BranchesViewModel BranchesViewModel, out int configurationID)
        {
            Branches Branches = new Branches
            {
                NameAr = BranchesViewModel.BranchesNameAR,
                NameEn = BranchesViewModel.BranchesNameEN,
                Mobile = BranchesViewModel.MobileNo,
                Fax = BranchesViewModel.FaxNumber,
                Address = BranchesViewModel.Address,
                Phone = BranchesViewModel.MobileNo,
                CityID = BranchesViewModel.CityID,
                Mail = BranchesViewModel.FirstEamil,
                UserId = BranchesViewModel.UserId,
                CreateDate = _blGeneral.GetDateTimeNow(),
                IsEnable = true
            };
            Branches = Uow.Branches.Insert(Branches);
            Uow.Commit();
            configurationID = Branches.BranchesID;
            return true; 
        }
        public bool Update(BranchesViewModel model)
        {
            var data = Uow.Branches.GetAll(p => p.BranchesID == model.BranchesID).FirstOrDefault();
            if (data != null)
            {
                data.NameAr = model.BranchesNameAR;
                data.NameEn = model.BranchesNameEN;
                data.Mail = model.FirstEamil;
                data.Fax = model.FaxNumber;
                data.Address = model.Address;
                data.Phone = model.MobileNo;
                data.CityID = model.CityID;
                data.UserUpdate = model.UserUpdate;
                data.UpdateDate = _blGeneral.GetDateTimeNow();
                Uow.Branches.Update(data);
                Uow.Commit();
                return true;
            }
            return false;
        }
        public bool Delete(int id, int userId)
        {
            var data = Uow.Branches.GetAll(p => p.BranchesID == id).FirstOrDefault();
            if (data != null)
            {
                data.IsDeleted = true;
                data.DeleteDate = _blGeneral.GetDateTimeNow();
                data.UserDelete = userId;
                Uow.Branches.Update(data);
                Uow.Commit();
                return true;
            }
            return false;
        }
        public bool ChangeStatus(int id, int userId)
        {
            var data = Uow.Branches.GetAll(p => p.BranchesID == id).FirstOrDefault();

            if (data != null)
            {
                data.UserEnable = userId;
                data.IsEnable = data.IsEnable == Convert.ToBoolean(StatusEnable.Enable) ? Convert.ToBoolean(StatusEnable.Disable) : Convert.ToBoolean(StatusEnable.Enable);
                data.EnableDate = _blGeneral.GetDateTimeNow();
                data = Uow.Branches.Update(data);
                Uow.Commit();
                return true;
            }
            return false;
        }
        #endregion
        #region Get
        public IQueryable<BranchesViewModel> GetBranches()
        {
            var xdata = Uow.Branches.GetAll(x => !x.IsDeleted).OrderBy(p => p.BranchesID).OrderByDescending(p => p.CreateDate)
             .Select(p => new BranchesViewModel
             {
                 BranchesID = p.BranchesID,
                 BranchesNameAR = p.NameAr,
                 BranchesNameEN = p.NameEn,
                 FirstEamil = p.Mail,
                 FaxNumber = p.Fax,
                 Address = p.Address,
                 CityID = p.CityID,
                 MobileNo = p.Phone,
                 IsEnable = p.IsEnable,

             });
            return xdata;
        }
        public BranchesViewModel GetBranchesViewModelByGuid(Guid BranchesGuid, string lang, string MainPathView)
        {
            var getData = Uow.Branches.GetAll(x => !x.IsDeleted && x.BranchesGuid == BranchesGuid)
            .Select(p => new BranchesViewModel()
            {
                BranchesID = p.BranchesID,
                CityID = p.CityID,
                IsEnable = p.IsEnable,
                MobileNo = p.Phone,
                FirstEamil = p.Mail,
                Name = lang == "ar" ? p.NameAr : p.NameEn,
                IsDeleted = p.IsDeleted,
                UpdateDate = p.UpdateDate,
                UserUpdate = p.UserUpdate,
                CreateDate = p.CreateDate,
                DeleteDate = p.DeleteDate,
                UserId = p.UserId,
                UserDelete = p.UserDelete,
                Address = p.Address,
                FaxNumber = p.Fax,
                BranchesNameAR = p.NameAr,
                BranchesNameEN = p.NameEn,
                CityNameAr = p.City.NameAR,
                CityNameEn = p.City.NameEN,
                CityName = lang == "ar" ? p.City.NameAR : p.City.NameEN,
            }).FirstOrDefault();
            return getData;

        }
        public bool IsExistMobile(string Mobile, int VendorsID)
        {
            return Uow.Branches.GetAll(s => s.Phone == Mobile && s.BranchesID != VendorsID && !s.IsDeleted).Any();
        }
        public bool IsExistEmail(string Email, int VendorsID)
        {
            return Uow.Branches.GetAll(s => s.Mail == Email && s.BranchesID != VendorsID && !s.IsDeleted).Any();
        }
        public IQueryable<BranchesViewModel> GetAllBranchesViewModelBySearch(string[] listCountryID, string[] ListCityID, string email, string Mobile, string[] listBranchesID, string lang, string MainPathView)
        {
            if (listCountryID != null)
            {
                if (listCountryID.Count(p => !string.IsNullOrEmpty(p)) == 0)
                {
                    listCountryID = null;
                }
            }

            if (ListCityID != null)
            {
                if (ListCityID.Count(p => !string.IsNullOrEmpty(p)) == 0)
                {
                    ListCityID = null;
                }
            }

            if (listBranchesID != null)
            {
                if (listBranchesID.Count(p => !string.IsNullOrEmpty(p)) == 0)
                {
                    listBranchesID = null;
                }
            }

            var getData = Uow.Branches.GetAll(x => !x.IsDeleted
                                                   && (listCountryID != null ? listCountryID.Any(y => x.City.Region.CountryID.ToString() == y) : true)
                                                   && (ListCityID != null ? ListCityID.Any(y => x.CityID.ToString() == y) : true)
                                                   && (listBranchesID != null ? listBranchesID.Any(y => x.BranchesID.ToString() == y) : true)
                                                   && (!string.IsNullOrEmpty(email) ? (x.Mail == email) : true)
                                                   && (!string.IsNullOrEmpty(Mobile) ? x.Phone == Mobile : true)
                , "City").OrderByDescending(p => p.CreateDate)
            .Select(p => new BranchesViewModel()
            {
                CityID = p.CityID,
                IsEnable = p.IsEnable,
                MobileNo = p.Phone,
                FirstEamil = p.Mail,
                BranchesID = p.BranchesID,
                IsDeleted = p.IsDeleted,
                UpdateDate = p.UpdateDate,
                UserUpdate = p.UserUpdate,
                CreateDate = p.CreateDate,
                DeleteDate = p.DeleteDate,
                UserId = p.UserId,
                UserDelete = p.UserDelete,
                Address = p.Address,
                FaxNumber = p.Fax,
                BranchesNameAR = p.NameAr,
                BranchesNameEN = p.NameEn,
                CityNameAr = p.City.NameAR,
                CityNameEn = p.City.NameEN,
                BranchesGuid = p.BranchesGuid,
            });
            return getData;
        }
        #endregion
    }
}
