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
    public class BlRegionCity
    {
        #region Decleration
        private readonly BLGeneral _blGeneral;
        private readonly IUOW Uow;
        public BlRegionCity(IUOW _uow, BLGeneral blGeneral)
        {
            _blGeneral = blGeneral;
            this.Uow = _uow;
        }
        #endregion
        #region GetRegionCity
        public List<RegionCityViewModel> GetRegionCity() => Uow.RegionCity.GetAll(p => p.IsEnable && !p.IsDeleted)
            .Select(p => new RegionCityViewModel() { RegionCityID = p.RegionCityID, RegionCityNameAR = p.NameAR, RegionCityNameEN = p.NameEN })
            .ToList();
        public List<RegionCityViewModelApi> GetRegionCityApi(string lang) => Uow.RegionCity.GetAll(p => p.IsEnable && !p.IsDeleted).Select(p => new RegionCityViewModelApi()
        { regionCityID = p.RegionCityID, regionCityName = lang == "ar" ? p.NameAR: p.NameEN }).ToList();
        #endregion
    }
}
