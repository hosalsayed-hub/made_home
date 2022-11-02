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
    public class BlCity
    {
        #region Decleration
        private readonly BLGeneral _blGeneral;
        private readonly IUOW Uow;
        public BlCity(IUOW _uow, BLGeneral blGeneral)
        {
            _blGeneral = blGeneral;
            this.Uow = _uow;
        }
        #endregion
        #region Helpers
        /// Check If City Name Is Used Before Or Not  اختبار اسم المدينة هل هو موجود ام لا
        public bool IsExist(int cityID, int RegionID, string cityName, Language language) => cityID != 0 ? (language == Language.Arabic ? Uow.City.GetAll(p => p.CityID != cityID && p.RegionID == RegionID && p.NameAR.Trim() == cityName && !p.IsDeleted).Any() : Uow.City.GetAll(p => p.CityID != cityID && p.RegionID == RegionID && p.NameEN.Trim() == cityName && !p.IsDeleted).Any()) : (language == Language.Arabic ? Uow.City.GetAll(p => p.NameAR.Trim() == cityName && p.RegionID == RegionID && !p.IsDeleted).Any() : Uow.City.GetAll(p => p.NameEN.Trim() == cityName && p.RegionID == RegionID && !p.IsDeleted).Any());
        public List<CityViewModelApi> GetCitiesViewModel(string accLanguage, int? countryId) => Uow.City.GetAll(p => !p.IsDeleted && p.Region.CountryID == countryId).Select(p => new CityViewModelApi()
        {
            cityID = p.CityID,
            cityName = accLanguage == "ar" ? p.NameAR : p.NameEN,
            lat = p.Lat,
            lng = p.Long,
        }).ToList();
        public string GetVersionMobile(int type,bool mobile)
        {
            var Data = Uow.PaymentConfiguration.GetAll(s => !s.IsDeleted).FirstOrDefault();
            if (type == 1) //user
            {
                if (mobile) //ios
                {
                    return Data.UserIOS;
                }
                else //andorid
                {
                    return Data.UserAnrdoid;
                }
            }
            else if (type == 2) //vedor
            {
                if (mobile)
                {
                    return Data.VendorIOS;
                }
                else
                {
                    return Data.VendorAndorid;
                }
            }
            else // driver
            {
                if (mobile)
                {
                    return Data.DriverIOS;
                }
                else
                {
                    return Data.DriverAndorid;
                }
            }
        }
        public bool updatemobileVersion(int type, bool mobile,string version)
        {
            var Data = Uow.PaymentConfiguration.GetAll(s => !s.IsDeleted).FirstOrDefault();
            if (type == 1) //user
            {
                if (mobile) //ios
                {
                   Data.UserIOS = version;
                }
                else //andorid
                {
                    Data.UserAnrdoid = version;
                }
            }
            else if (type == 2) //vedor
            {
                if (mobile)
                {
                    Data.VendorIOS = version;
                }
                else
                {
                    Data.VendorAndorid = version;
                }
            }
            else //driver
            {
                if (mobile)
                {
                    Data.DriverIOS = version;
                }
                else
                {
                    Data.DriverAndorid = version;
                }
            }
            Uow.PaymentConfiguration.Update(Data);
            Uow.Commit();
            return true;
        }
        public List<OrderStatusViewModelApi> GetOrderStatusApi(string accLanguage) => Uow.OrderStatus.GetAll(p => !p.IsDeleted && p.OrderStatusID != (int)OrderStatusEnum.Create).Select(p => new OrderStatusViewModelApi()
        {
            orderStatusID = p.OrderStatusID,
            name = accLanguage == "ar" ? p.NameAR : p.NameEN,
            Arrange = p.Arrange,
            OrderStatusType = p.OrderStatusType,
        }).ToList();
        public List<CityViewModelApi> GetCitiesViewModelRegion(string accLanguage, int? regionid) => Uow.City.GetAll(p => !p.IsDeleted && p.RegionID == regionid).Select(p => new CityViewModelApi()
        {
            cityID = p.CityID,
            cityName = accLanguage == "ar" ? p.NameAR : p.NameEN,
            lat = p.Lat,
            lng = p.Long,
        }).ToList();
        #endregion
        #region Actions
        /// Add New City اضافه مدينه جديده
        public bool Insert(CityViewModel cityViewModel, out int cityID)
        {
            City city = new City
            {
                NameAR = cityViewModel.CityNameAR,
                NameEN = cityViewModel.CityNameEN,
                RegionID = cityViewModel.RegionID,
                CreateDate = _blGeneral.GetDateTimeNow(),
                UserId = cityViewModel.UserId,
                Lat = cityViewModel.Lat,
                Long = cityViewModel.Lng,
                IsEnable = true
            };
            city = Uow.City.Insert(city);
            Uow.Commit();
            cityID = city.CityID;
            return true; ;
        }
        /// Delete City By ID  حذف مدينه
        public bool Delete(int id, int userId)
        {
            var data = Uow.City.GetAll(p => p.CityID == id).FirstOrDefault();
            if (data != null)
            {
                data.IsDeleted = true;
                data.DeleteDate = _blGeneral.GetDateTimeNow();
                data.UserDelete = userId;
                Uow.City.Update(data);
                Uow.Commit();
                return true;
            }
            return false;
        }
        public bool ChangeStatus(int id, int userId)
        {
            var data = Uow.City.GetAll(p => p.CityID == id).FirstOrDefault();

            if (data != null)
            {
                data.UserEnable = userId;
                data.IsEnable = data.IsEnable == Convert.ToBoolean(StatusEnable.Enable) ? Convert.ToBoolean(StatusEnable.Disable) : Convert.ToBoolean(StatusEnable.Enable);
                data.EnableDate = _blGeneral.GetDateTimeNow();
                data = Uow.City.Update(data);
                Uow.Commit();
                return true;
            }
            return false;
        }
        /// Update City تعديل مدينه
        public bool Update(CityViewModel model)
        {
            var data = Uow.City.GetAll(p => p.CityID == model.CityID).FirstOrDefault();
            if (data != null)
            {
                data.NameAR = model.CityNameAR;
                data.NameEN = model.CityNameEN;
                data.RegionID = model.RegionID;
                data.UpdateDate = _blGeneral.GetDateTimeNow();
                data.UserUpdate = model.UserUpdate;
                data.Lat = model.Lat;
                data.Long = model.Lng;
                Uow.City.Update(data);
                Uow.Commit();
                return true;
            }
            return false;
        }
        /// <summary>
        /// Get City By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public City GetById(int id) => Uow.City.GetAll(x => x.CityID == id).FirstOrDefault();
        public City GetById(int id,string include) => Uow.City.GetAll(x => x.CityID == id, include).FirstOrDefault();
        #endregion
        #region GetCities
        /// Get All Cities جلب كل المدن
        public IQueryable<CityViewModel> GetCities()
        {
            var xdata = Uow.City.GetAll(x => !x.IsDeleted, "Region").OrderBy(p => p.RegionID).OrderByDescending(p => p.CreateDate)
             .Select(p => new CityViewModel
             {
                 CityID = p.CityID,
                 CityNameAR = p.NameAR,
                 CityNameEN = p.NameEN,
                 RegionID = p.RegionID,
                 RegionNameAR = p.Region.NameAR,
                 RegionNameEN = p.Region.NameEN,
                 CountryID = p.Region.CountryID,
                 CountryNameAR = p.Region.Country.NameAR,
                 CountryNameEN = p.Region.Country.NameEN,
                 Lat = p.Lat,
                 Lng = p.Long,
                 IsEnable = p.IsEnable,
             });
            return xdata;
        }
        /// Get First City جلب اول مدينة
        public City GetFirstCity() => Uow.City.GetAll(x => !x.IsDeleted).FirstOrDefault();
        /// Get All Cities جلب كل المدن
        public List<City> GetAllCitiesByRegion(int RegionID)
        {
            return Uow.City.GetAll(x => !x.IsDeleted && x.RegionID == RegionID && x.IsEnable).ToList();
        }
        public List<City> GetAllCitiesByListRegion(string[] ListRegionID)
        {
            if (ListRegionID != null)
            {
                if (ListRegionID.Count(p => !string.IsNullOrEmpty(p)) == 0)
                {
                    ListRegionID = null;
                }
            }
            return Uow.City.GetAll(x => !x.IsDeleted && (ListRegionID != null ? ListRegionID.Any(y => y == x.RegionID.ToString()) : false)).ToList();
        }
        public List<City> GetAllCitiesByListCountry(string[] ListCountryID)
        {
            if (ListCountryID != null)
            {
                if (ListCountryID.Count(p => !string.IsNullOrEmpty(p)) == 0)
                {
                    ListCountryID = null;
                }
            }
            return Uow.City.GetAll(x => !x.IsDeleted && (ListCountryID != null ? ListCountryID.Any(y => y == x.Region.CountryID.ToString()) : false)).ToList();
        }
        public List<City> GetAllCities()
        {
            var data = Uow.City.GetAll(x => !x.IsDeleted).ToList();
            return data;
        }
        #endregion
    }
}
