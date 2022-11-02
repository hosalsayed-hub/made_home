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
    public class BlRegion
    {
        #region Decleration
        private readonly BLGeneral _blGeneral;
        private readonly IUOW Uow;
        public BlRegion(IUOW _uow, BLGeneral blGeneral)
        {
            _blGeneral = blGeneral;
            this.Uow = _uow;
        }
        #endregion
        #region Helpers
        /// Check If Region Name Is Used Before Or Not اختبار هل اسم المنطقة عربي موجود من قبل ام لا
        public bool IsExist(int RegionID,int CountryID, string RegionName, Language language) => RegionID != 0 ? 
            (language == Language.Arabic ? Uow.Region.GetAll().Any(p => p.RegionID != RegionID && p.CountryID == CountryID && p.NameAR.Trim() == RegionName && !p.IsDeleted) : Uow.Region.GetAll().Any(p => p.RegionID != RegionID && p.CountryID == CountryID && p.NameEN.Trim() == RegionName && !p.IsDeleted)) : (language == Language.Arabic ? Uow.Region.GetAll().Any(p => p.NameAR.Trim() == RegionName && p.CountryID == CountryID && !p.IsDeleted) : Uow.Region.GetAll().Any(p => p.CountryID == CountryID && p.NameEN.Trim() == RegionName && !p.IsDeleted));
        public bool IsExistCity(int RegionID)
        {
            return Uow.City.GetAll(x => x.RegionID == RegionID && !x.IsDeleted && x.Region.IsEnable).Any();
        }
        public List<RegionViewModel> GetRegiontaghelper() => Uow.Region.GetAll(p => !p.IsDeleted).OrderByDescending(p => p.CreateDate).Select(p => new RegionViewModel()
        {
            RegionID = p.RegionID, RegionNameAR = p.NameAR, RegionNameEN = p.NameEN,
            CountryID = p.CountryID,
            CountryNameAR = p.Country.NameAR,
            CountryNameEN = p.Country.NameEN,
            IsEnable = p.IsEnable,
        }).ToList();
        #endregion
        #region Actions
        /// Add New Region اضافة المنطقة
        public bool Insert(RegionViewModel RegionModel, out int RegionID)
        {
            Region Region = new Region
            {
                NameAR = RegionModel.RegionNameAR,
                NameEN = RegionModel.RegionNameEN,
                CreateDate = _blGeneral.GetDateTimeNow(),
                UserId = RegionModel.UserId,
                CountryID = RegionModel.CountryID,
                IsDeleted = false,
                IsEnable = true
            };
            Region = Uow.Region.Insert(Region);
            Uow.Commit();
            RegionID = Region.RegionID;
            return true;
        }
        /// Delete Region By ID حذف المنطقة
        public bool Delete(int id, int userId)
        {
            var Region = GetById(id);
            if (Region != null)
            {
                Region.IsDeleted = true;
                Region.UserDelete = userId;
                Region.DeleteDate = _blGeneral.GetDateTimeNow();
                Uow.Region.Update(Region);
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
            var data = Uow.Region.GetAll(p => p.RegionID == id).FirstOrDefault();

            if (data != null)
            {
                data.UserEnable = userId;
                data.IsEnable = data.IsEnable == Convert.ToBoolean(StatusEnable.Enable) ? Convert.ToBoolean(StatusEnable.Disable) : Convert.ToBoolean(StatusEnable.Enable);
                data.EnableDate = _blGeneral.GetDateTimeNow();
                data = Uow.Region.Update(data);
                Uow.Commit();
                return true;
            }
            return false;
        }
        /// Update Region تعديل المنطقة
        public bool Update(RegionViewModel model)
        {
            IQueryable<Region> data = Uow.Region.GetAll(p => p.RegionID == model.RegionID);
            if (data != null)
            {
                Region Region = data.FirstOrDefault();
                Region.NameAR = model.RegionNameAR;
                Region.NameEN = model.RegionNameEN;
                Region.UpdateDate = model.UpdateDate;
                Region.UserUpdate = model.UserUpdate;
                Region.CountryID = model.CountryID;
                Region = Uow.Region.Update(Region);
                Uow.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }
        public Region GetById(int id) => Uow.Region.GetAll(x => x.RegionID == id).FirstOrDefault();
        #endregion
        #region GetRegion
        /// Get All Region جلب الدول
        public List<RegionViewModel> GetRegionsByCountryID(int countryID) => Uow.Region.GetAll(p => p.CountryID == countryID && p.IsEnable && !p.IsDeleted).Select(p => new RegionViewModel() { RegionID = p.RegionID, RegionNameAR = p.NameAR, RegionNameEN = p.NameEN }).ToList();
        public List<RegionViewModelApi> GetRegionsApiByCountryID(string lang, int countryID) => Uow.Region.GetAll(p => p.CountryID == countryID && p.IsEnable && !p.IsDeleted).Select(p => new RegionViewModelApi()
        { regionID = p.RegionID, regionName = lang == "ar" ? p.NameAR: p.NameEN }).ToList();
        #endregion
    }
}
