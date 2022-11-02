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
   public class BlShippingCompany
    {
        #region Decleration
        private readonly BLGeneral _blGeneral;
        private readonly IUOW Uow;
        public BlShippingCompany(IUOW _uow, BLGeneral blGeneral)
        {
            _blGeneral = blGeneral;
            this.Uow = _uow;
        }
        #endregion
        #region Actions
        public bool Insert(ShippingCompanyViewModel ShippingCompanyViewModel, out int configurationID)
        {
            ShippingCompany ShippingCompany = new ShippingCompany
            {
                NameAr = ShippingCompanyViewModel.NameAr,
                NameEn = ShippingCompanyViewModel.NameEn,
                Email = ShippingCompanyViewModel.Email,
                Fax = ShippingCompanyViewModel.Fax,
                Address = ShippingCompanyViewModel.Address,
                PhoneNo = ShippingCompanyViewModel.PhoneNo,

                CityID = ShippingCompanyViewModel.CityID,

                Logo = ShippingCompanyViewModel.Logo,


                MobileNo = ShippingCompanyViewModel.MobileNo,
                UserId = ShippingCompanyViewModel.UserId,
                CreateDate = _blGeneral.GetDateTimeNow(),
                IsEnable = true,
                MaxKM = ShippingCompanyViewModel.MaxKM,
                DeliveryPrice = ShippingCompanyViewModel.DeliveryPrice,
            };
            ShippingCompany = Uow.ShippingCompany.Insert(ShippingCompany);
            Uow.Commit();
            configurationID = ShippingCompany.ShippingCompanyID;
            return true; ;
        }
        public bool Update(ShippingCompanyViewModel model)
        {
            var data = Uow.ShippingCompany.GetAll(p => p.ShippingCompanyID == model.ShippingCompanyID).FirstOrDefault();
            if (data != null)
            {
                data.NameAr = model.NameAr;
                data.NameEn = model.NameEn;
                data.Email = model.Email;
                data.Fax = model.Fax;
                data.Address = model.Address;
                data.PhoneNo = model.PhoneNo;

                data.CityID = model.CityID;
                data.MaxKM = model.MaxKM;
                data.DeliveryPrice = model.DeliveryPrice;

                if (!string.IsNullOrWhiteSpace(model.Logo))
                {
                    data.Logo = model.Logo;
                }

                data.MobileNo = model.MobileNo;
                data.UserUpdate = model.UserUpdate;
                data.UpdateDate = _blGeneral.GetDateTimeNow();
                Uow.ShippingCompany.Update(data);
                Uow.Commit();
                return true;
            }
            return false;
        }
        public bool Delete(int id, int userId)
        {
            var data = Uow.ShippingCompany.GetAll(p => p.ShippingCompanyID == id).FirstOrDefault();
            if (data != null)
            {
                data.IsDeleted = true;
                data.DeleteDate = _blGeneral.GetDateTimeNow();
                data.UserDelete = userId;
                Uow.ShippingCompany.Update(data);
                Uow.Commit();
                return true;
            }
            return false;
        }
        public bool ChangeStatus(int id, int userId)
        {
            var data = Uow.ShippingCompany.GetAll(p => p.ShippingCompanyID == id).FirstOrDefault();

            if (data != null)
            {
                data.UserEnable = userId;
                data.IsEnable = data.IsEnable == Convert.ToBoolean(StatusEnable.Enable) ? Convert.ToBoolean(StatusEnable.Disable) : Convert.ToBoolean(StatusEnable.Enable);
                data.EnableDate = _blGeneral.GetDateTimeNow();
                data = Uow.ShippingCompany.Update(data);
                Uow.Commit();
                return true;
            }
            return false;
        }
        #endregion
        #region Get
        public IQueryable<ShippingCompanyViewModel> GetShippingCompanies()
        {
            var xdata = Uow.ShippingCompany.GetAll(x => !x.IsDeleted).OrderBy(p => p.ShippingCompanyID).OrderByDescending(p => p.CreateDate)
             .Select(p => new ShippingCompanyViewModel
             {
                 ShippingCompanyID = p.ShippingCompanyID,
                 NameAr = p.NameAr,
                 NameEn = p.NameEn,
                 Email = p.Email,
                 Fax = p.Fax,
                 Address = p.Address,
                 PhoneNo = p.PhoneNo,

                 CityID = p.CityID,

                 Logo = p.Logo,


                 MobileNo = p.MobileNo,
                 IsEnable = p.IsEnable,
                 DeliveryPrice = p.DeliveryPrice,
                 MaxKM = p.MaxKM,
                 ShippingCompanyGuid = p.ShippingCompanyGuid

             });
            return xdata;
        }
        public ShippingCompanyViewModel GetShippingCompanyViewModelByGuid(Guid ShippingCompanyGuid, string lang, string MainPathView)
        {


            var getData = Uow.ShippingCompany.GetAll(x => !x.IsDeleted && x.ShippingCompanyGuid == ShippingCompanyGuid)
            .Select(p => new ShippingCompanyViewModel()
            {
                ShippingCompanyID = p.ShippingCompanyID,
                CityID = p.CityID,
                IsEnable = p.IsEnable,
                MobileNo = p.MobileNo,
                Email = p.Email,
                Name = lang == "ar" ? p.NameAr : p.NameEn,
                IsDeleted = p.IsDeleted,
                UpdateDate = p.UpdateDate,
                UserUpdate = p.UserUpdate,
                CreateDate = p.CreateDate,
                DeleteDate = p.DeleteDate,
                UserId = p.UserId,
                UserDelete = p.UserDelete,

                Address = p.Address,

                Logo = !string.IsNullOrEmpty(p.Logo) ? (MainPathView + p.Logo) : "/Images/noImage.png",
                Fax = p.Fax,
                NameAr = p.NameAr,
                NameEn = p.NameEn,
                PhoneNo = p.PhoneNo,
                Notes = p.Notes,
                CityNameAR = p.City.NameAR,
                CityNameEN = p.City.NameEN,
                NameCity = lang == "ar" ? p.City.NameAR : p.City.NameEN,
                DeliveryPrice = p.DeliveryPrice,
                MaxKM = p.MaxKM,
                ShippingCompanyGuid = p.ShippingCompanyGuid

            }).FirstOrDefault();
            return getData;

        }
        public bool IsExistMobile(string Mobile, int VendorsID)
        {
            return Uow.ShippingCompany.GetAll(s => s.MobileNo == Mobile && s.ShippingCompanyID != VendorsID && !s.IsDeleted).Any();
        }
        public bool IsExistEmail(string Email, int VendorsID)
        {
            return Uow.ShippingCompany.GetAll(s => s.Email == Email && s.ShippingCompanyID != VendorsID && !s.IsDeleted).Any();
        }
        public decimal GetShippingCompanyMaxKM(int ShippingCompanyID) => Uow.ShippingCompany.GetAll(x => !x.IsDeleted && x.ShippingCompanyID == ShippingCompanyID).Select(p => p.MaxKM).FirstOrDefault();
        public int[] GetListBlocksFromShippingCompanyBlocks(int ShippingCompanyID) => Uow.ShippingCompanyBlocks.GetAll(x => !x.IsDeleted && x.ShippingCompanyID == ShippingCompanyID).Select(p => p.BlockID).ToArray();
        public IQueryable<ShippingCompanyViewModel> GetAllShippingCompanyViewModelBySearch(string[] listCountryID, string[] ListCityID, string email, string Mobile, string[] listShippingCompanyID, string lang, string MainPathView)
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

            if (listShippingCompanyID != null)
            {
                if (listShippingCompanyID.Count(p => !string.IsNullOrEmpty(p)) == 0)
                {
                    listShippingCompanyID = null;
                }
            }

            var getData = Uow.ShippingCompany.GetAll(x => !x.IsDeleted
                                                   && (listCountryID != null ? listCountryID.Any(y => x.City.Region.CountryID.ToString() == y) : true)
                                                   && (ListCityID != null ? ListCityID.Any(y => x.CityID.ToString() == y) : true)
                                                   && (listShippingCompanyID != null ? listShippingCompanyID.Any(y => x.ShippingCompanyID.ToString() == y) : true)
                                                   && (!string.IsNullOrEmpty(email) ? (x.Email == email) : true)
                                                   && (!string.IsNullOrEmpty(Mobile) ? x.MobileNo == Mobile : true)
                , "City").OrderByDescending(p => p.CreateDate)
            .Select(p => new ShippingCompanyViewModel()
            {
                CityID = p.CityID,
                IsEnable = p.IsEnable,
                MobileNo = p.MobileNo,
                Email = p.Email,
                ShippingCompanyID = p.ShippingCompanyID,
                IsDeleted = p.IsDeleted,
                UpdateDate = p.UpdateDate,
                UserUpdate = p.UserUpdate,
                CreateDate = p.CreateDate,
                DeleteDate = p.DeleteDate,
                UserId = p.UserId,
                UserDelete = p.UserDelete,
                Address = p.Address,
                Logo = !string.IsNullOrEmpty(p.Logo) ? (MainPathView + p.Logo) : "/Images/noImage.png",
                Fax = p.Fax,
                NameAr = p.NameAr,
                NameEn = p.NameEn,
                PhoneNo = p.PhoneNo,
                Notes = p.Notes,
                CityNameAR = p.City.NameAR,
                CityNameEN = p.City.NameEN,
                ShippingCompanyGuid = p.ShippingCompanyGuid,
            });
            return getData;
        }
        #endregion
    }
}
