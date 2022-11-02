using Homemade.BLL.General;
using Homemade.BLL.ViewModel.Setting;
using Homemade.DAL.Contract;
using Homemade.Model.Driver;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.Setting
{
    public class BlDeliverySetting
    {
        #region Decleration
        private readonly BLGeneral _blGeneral;
        private readonly IUOW Uow;
        public BlDeliverySetting(IUOW _uow, BLGeneral blGeneral)
        {
            _blGeneral = blGeneral;
            this.Uow = _uow;
        }
        #endregion
        #region Helpers
        public DeliverySettingViewModel GetDeliverySettingViewModel()
        {
            return Uow.DeliverySetting.GetAll(p => !p.IsDeleted).OrderByDescending(p => p.CreateDate)
                .Select(p => new DeliverySettingViewModel
                {
                    DeliverySettingID = p.DeliverySettingID,
                    DriverCommision = p.DriverCommision.ToString(),
                    MinKM = p.MinKM.ToString(),
                    OverKmFare = p.OverKmFare.ToString(),
                }).FirstOrDefault();
        }
        public DeliverySetting GetById(int id) => Uow.DeliverySetting.GetAll(x => x.DeliverySettingID == id).FirstOrDefault();
        #endregion
        #region Actions
        public bool Update(DeliverySettingViewModel model, int UserId)
        {
            var DeliverySetting = GetById(model.DeliverySettingID);
            if (DeliverySetting != null)
            {
                DeliverySetting.MinKM = decimal.Parse(model.MinKM, CultureInfo.InvariantCulture);
                DeliverySetting.OverKmFare = decimal.Parse(model.OverKmFare, CultureInfo.InvariantCulture);
                DeliverySetting.DriverCommision = decimal.Parse(model.DriverCommision, CultureInfo.InvariantCulture);
                DeliverySetting.UpdateDate = _blGeneral.GetDateTimeNow();
                DeliverySetting.UserUpdate = UserId;
                Uow.DeliverySetting.Update(DeliverySetting);
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
