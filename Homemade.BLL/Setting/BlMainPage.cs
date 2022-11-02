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
    public class BlMainPage
    {
        #region Decleration
        private readonly BLGeneral _blGeneral;
        private readonly IUOW Uow;
        public BlMainPage(IUOW _uow, BLGeneral blGeneral)
        {
            _blGeneral = blGeneral;
            this.Uow = _uow;
        }
        #endregion
        #region Actions
        public MainPage GetByGuidId(Guid guid) => Uow.MainPage.GetAll(x => x.MainPageGuid == guid).FirstOrDefault();
        #endregion
        #region GetMainPage
        /// Get All MainPage جلب الصفحة
        public List<MainPage> GetAllMainPage()
        {
            var data = Uow.MainPage.GetAll(x => !x.IsDeleted).ToList();
            return data;
        }
        #endregion
    }
}
