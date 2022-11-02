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
    public class BlPaymentConfiguration
    {
        #region Decleration
        private readonly BLGeneral _blGeneral;
        private readonly IUOW Uow;
        public BlPaymentConfiguration(IUOW _uow, BLGeneral blGeneral)
        {
            _blGeneral = blGeneral;
            this.Uow = _uow;
        }
        #endregion
        #region Action
        public bool Insert(PaymentConfigurationViewModel PaymentConfigurationViewModel, out int PaymentConfigurationID)
        {
            PaymentConfiguration PaymentConfiguration = new PaymentConfiguration
            {
                IBANnumber = PaymentConfigurationViewModel.IBANnumber,
                AccountNumber = PaymentConfigurationViewModel.AccountNumber,
                BanksID = PaymentConfigurationViewModel.BanksID,
                AccountImage = PaymentConfigurationViewModel.AccountImage,
                Attachment = PaymentConfigurationViewModel.Attachment,
                UserId = PaymentConfigurationViewModel.UserId,
                CreateDate = _blGeneral.GetDateTimeNow(),
                IsEnable = true
            };
            PaymentConfiguration = Uow.PaymentConfiguration.Insert(PaymentConfiguration);
            Uow.Commit();
            PaymentConfigurationID = PaymentConfiguration.PaymentConfigurationID;
            return true; ;
        }
        public bool Update(PaymentConfigurationViewModel model)
        {
            var data = Uow.PaymentConfiguration.GetAll(p => p.PaymentConfigurationID == model.PaymentConfigurationID).FirstOrDefault();
            if (data != null)
            {
                data.IBANnumber = model.IBANnumber;
                data.AccountNumber = model.AccountNumber;
                data.BanksID = model.BanksID;
                data.UserUpdate = model.UserUpdate;
                data.UpdateDate = _blGeneral.GetDateTimeNow();
                if (!string.IsNullOrWhiteSpace(model.Attachment))
                {
                    data.Attachment = model.Attachment;
                }
                if (!string.IsNullOrWhiteSpace(model.AccountImage))
                {
                    data.AccountImage = model.AccountImage;
                }
                Uow.PaymentConfiguration.Update(data);
                Uow.Commit();
                return true;
            }
            return false;
        }
        #endregion
        #region Get
        public IQueryable<PaymentConfigurationViewModel> GetPaymentConfigurations()
        {
            var xdata = Uow.PaymentConfiguration.GetAll(x => !x.IsDeleted).OrderBy(p => p.PaymentConfigurationID).OrderByDescending(p => p.CreateDate)
             .Select(p => new PaymentConfigurationViewModel
             {
                 PaymentConfigurationID = p.PaymentConfigurationID,
                 IBANnumber = p.IBANnumber,
                 AccountNumber = p.AccountNumber,
                 BanksID = p.BanksID,
                 AccountImage = p.AccountImage,
                 Attachment = p.Attachment,
                 IsEnable = p.IsEnable,

             });
            return xdata;
        }
        #endregion
    }
}
