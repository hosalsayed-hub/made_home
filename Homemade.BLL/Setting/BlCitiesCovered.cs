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
    public class BlCitiesCovered
    {
        #region Decleration
        private readonly BLGeneral _blGeneral;
        private readonly IUOW Uow;
        public BlCitiesCovered(IUOW _uow, BLGeneral blGeneral)
        {
            _blGeneral = blGeneral;
            this.Uow = _uow;
        }
        #endregion
        #region Helper
        public CitiesCovered GetById(int CitiesCoveredID)
        {
            return Uow.CitiesCovered.GetAll(x => !x.IsDeleted && x.CitiesCoveredID == CitiesCoveredID).FirstOrDefault();
        }
        public IQueryable<CitiesCovered> GetAllCitiesCovered()
        {
            return Uow.CitiesCovered.GetAll(x => !x.IsDeleted);
        }
        public IQueryable<CitiesCoveredViewModel> GetAllCitiesCoveredViewModel(string lang)
        {
            var xdata = Uow.City.GetAll(x => !x.IsDeleted && x.IsEnable).OrderByDescending(p => p.CreateDate)
             .Select(p => new CitiesCoveredViewModel
             {
                 CityID = p.CityID,
                 CityName = lang == "ar" ? p.NameAR : p.NameEN,
                 IsEnable = p.IsEnable,
                 CreateDate = p.CreateDate,
                 DeleteDate = p.DeleteDate,
                 EnableDate = p.EnableDate,
                 IsDeleted = p.IsDeleted,
                 UpdateDate = p.UpdateDate,
                 UserDelete = p.UserDelete,
                 UserEnable = p.UserEnable,
                 UserId = p.UserId,
                 UserUpdate = p.UserUpdate,
                 IsCovered = p.CitiesCovered.Any(x => !x.IsDeleted),
             });
            return xdata;
        }
        public bool IsExistCitiesCovered(int CityID)
        {
            return Uow.CitiesCovered.GetAll(s => s.CityID == CityID && !s.IsDeleted).Count() > 0;
        }
        public CitiesCovered GetCitiesCoveredDeleted(int CityID)
        {
            return Uow.CitiesCovered.GetAll(s => s.CityID == CityID).FirstOrDefault();
        }
        #endregion
        #region Action 
        public bool SaveCovered(string CheckedItems, int UserId)
        {
            string[] NewCheck = CheckedItems.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var cityID in NewCheck)
            {
                if (!IsExistCitiesCovered(Convert.ToInt32(cityID)))
                {
                    var item = GetCitiesCoveredDeleted(Convert.ToInt32(cityID));
                    if (item != null)
                    {
                        item.UpdateDate = _blGeneral.GetDateTimeNow();
                        item.IsDeleted = false;
                        item.UserUpdate = UserId;
                        Uow.CitiesCovered.Update(item);
                    }
                    else
                    {
                        Uow.CitiesCovered.Insert(new CitiesCovered()
                        {
                            IsDeleted = false,
                            IsEnable = true,
                            CitiesCoveredGuid = Guid.NewGuid(),
                            UserId = UserId,
                            CityID = Convert.ToInt32(cityID),
                            CreateDate = _blGeneral.GetDateTimeNow(),
                        });
                    }
                }
            }
            var existDriverCity = GetAllCitiesCovered();
            foreach (var item in existDriverCity)
            {
                if (!NewCheck.Any(x => x == item.CityID.ToString()))
                {
                    item.DeleteDate = _blGeneral.GetDateTimeNow();
                    item.IsDeleted = true;
                    item.UserDelete = UserId;
                    Uow.CitiesCovered.Update(item);
                }
            }
            Uow.Commit();
            return true;
        }
        #endregion
    }
}
