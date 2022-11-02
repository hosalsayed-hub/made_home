using Homemade.BLL.General;
using Homemade.BLL.ViewModel.Setting;
using Homemade.DAL.Contract;
using Homemade.Model.Setting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.Setting
{
    public class BlConfiguration
    {
        #region Decleration
        private readonly BLGeneral _blGeneral;
        private readonly IUOW Uow;
        public BlConfiguration(IUOW _uow, BLGeneral blGeneral)
        {
            _blGeneral = blGeneral;
            this.Uow = _uow;
        }
        #endregion
        #region Action
        public bool Insert(ConfigurationViewModel configurationViewModel, out int configurationID)
        {
            Configuration Configuration = new Configuration
            {
                CompanNameAr = configurationViewModel.CompanNameAr,
                CompanNameEn = configurationViewModel.CompanNameEn,
                MobileNumber = configurationViewModel.MobileNumber,
                Fax = configurationViewModel.Fax,
                Address = configurationViewModel.Address,
                StreetNo = configurationViewModel.StreetNo,
                UserId = configurationViewModel.UserId,
                Banner = configurationViewModel.Banner,
                CRImage = configurationViewModel.CRImage,
                Logo = configurationViewModel.Logo,
                CRNumber = configurationViewModel.CRNumber,
                PhoneNumber = configurationViewModel.PhoneNumber,
                SeconedEmail = configurationViewModel.SeconedEmail,
                CreateDate = _blGeneral.GetDateTimeNow(),
                IsEnable = true,
                Email = configurationViewModel.Email,
                DeliveryPrice = decimal.Parse(configurationViewModel.DeliveryPrice, CultureInfo.InvariantCulture),
                MinDeliveryPrice = decimal.Parse(configurationViewModel.MinDeliveryPrice, CultureInfo.InvariantCulture),
                MaxDeliveryPrice = decimal.Parse(configurationViewModel.MaxDeliveryPrice, CultureInfo.InvariantCulture),
                DeliveryPriceVatPercent = configurationViewModel.DeliveryPriceVatPercent,
                DeliveryPriceWithoutVat = decimal.Parse(configurationViewModel.DeliveryPriceVatPercentstring),
                MinKM = decimal.Parse(configurationViewModel.MinKM, CultureInfo.InvariantCulture),
                OverKmFare = decimal.Parse(configurationViewModel.OverKmFare, CultureInfo.InvariantCulture),
                WhatsappLink = configurationViewModel.WhatsappLink,
                TwitterLink = configurationViewModel.TwitterLink,
                InstagramLink = configurationViewModel.InstagramLink,
                SnapchatLink = configurationViewModel.SnapchatLink,
            };
            Configuration = Uow.Configuration.Insert(Configuration);
            Uow.Commit();
            configurationID = Configuration.ConfigurationID;
            return true;
        }
        public bool Update(ConfigurationViewModel model)
        {
            var data = Uow.Configuration.GetAll(p => p.ConfigurationID == model.ConfigurationID).FirstOrDefault();
            if (data != null)
            {
                data.CompanNameAr = model.CompanNameAr;
                data.CompanNameEn = model.CompanNameEn;
                data.MobileNumber = model.MobileNumber;
                data.Fax = model.Fax;
                data.Address = model.Address;
                data.SeconedEmail = model.SeconedEmail;
                data.Email = model.Email;
                data.StreetNo = model.StreetNo;
                if (!string.IsNullOrWhiteSpace(model.Logo))
                {
                    data.Logo = model.Logo;
                }
                if (!string.IsNullOrWhiteSpace(model.CRImage))
                {
                    data.CRImage = model.CRImage;
                }
                if (!string.IsNullOrWhiteSpace(model.Banner))
                {
                    data.Banner = model.Banner;
                }
                data.CRNumber = model.CRNumber;
                data.PhoneNumber = model.PhoneNumber;
                data.UserUpdate = model.UserUpdate;
                data.UpdateDate = _blGeneral.GetDateTimeNow();
                data.DeliveryPrice = decimal.Parse(model.DeliveryPrice, CultureInfo.InvariantCulture);
                data.MaxDeliveryPrice = decimal.Parse(model.MaxDeliveryPrice, CultureInfo.InvariantCulture);
                data.MinDeliveryPrice = decimal.Parse(model.MinDeliveryPrice, CultureInfo.InvariantCulture);
                data.DeliveryPriceVatPercent = decimal.Parse(model.DeliveryPriceVatPercent.ToString(), CultureInfo.InvariantCulture);
                data.DeliveryPriceWithoutVat = decimal.Parse(model.DeliveryPriceVatPercentstring, CultureInfo.InvariantCulture);
                data.MinKM = decimal.Parse(model.MinKM, CultureInfo.InvariantCulture);
                data.OverKmFare = decimal.Parse(model.OverKmFare, CultureInfo.InvariantCulture);
                data.WhatsappLink = model.WhatsappLink;
                data.TwitterLink = model.TwitterLink;
                data.InstagramLink = model.InstagramLink;
                data.SnapchatLink = model.SnapchatLink;
                Uow.Configuration.Update(data);
                Uow.Commit();
                return true;
            }
            return false;
        }
        public bool Delete(int id, int userId)
        {
            var data = Uow.Configuration.GetAll(p => p.ConfigurationID == id).FirstOrDefault();
            if (data != null)
            {
                data.IsDeleted = true;
                data.DeleteDate = _blGeneral.GetDateTimeNow();
                data.UserDelete = userId;
                Uow.Configuration.Update(data);
                Uow.Commit();
                return true;
            }
            return false;
        }
        public bool ChangeStatus(int id, int userId)
        {
            var data = Uow.Configuration.GetAll(p => p.ConfigurationID == id).FirstOrDefault();

            if (data != null)
            {
                data.UserEnable = userId;
                data.IsEnable = data.IsEnable == Convert.ToBoolean(StatusEnable.Enable) ? Convert.ToBoolean(StatusEnable.Disable) : Convert.ToBoolean(StatusEnable.Enable);
                data.EnableDate = _blGeneral.GetDateTimeNow();
                data = Uow.Configuration.Update(data);
                Uow.Commit();
                return true;
            }
            return false;
        }
        #endregion
        #region Get
        public SocialInformationViewModelApi GetSocialInformationViewModelApi()
        {
            return Uow.Configuration.GetAll(x => !x.IsDeleted).OrderBy(p => p.ConfigurationID).OrderByDescending(p => p.CreateDate).Select(m => new SocialInformationViewModelApi()
            {
                whatsappLink = !string.IsNullOrWhiteSpace(m.WhatsappLink) ? (m.WhatsappLink) : "",
                twitterLink = !string.IsNullOrWhiteSpace(m.TwitterLink) ? (m.TwitterLink) : "",
                instagramLink = !string.IsNullOrWhiteSpace(m.InstagramLink) ? (m.InstagramLink) : "",
                snapchatLink = !string.IsNullOrWhiteSpace(m.SnapchatLink) ? (m.SnapchatLink) : "",
            }).FirstOrDefault();
        }
        public Configuration GetFirstConfiguration()
        {
            return Uow.Configuration.GetAll(x => !x.IsDeleted).OrderBy(p => p.ConfigurationID).OrderByDescending(p => p.CreateDate).FirstOrDefault();
        }
        public string GetConfigurationMinKM()
        {
            return Uow.Configuration.GetAll(x => !x.IsDeleted).OrderBy(p => p.ConfigurationID).OrderByDescending(p => p.CreateDate).Select(p => p.MinKM.ToString()).FirstOrDefault();
        }
        public decimal GetDeliveryPriceVatPercent()
        {
            return Uow.Configuration.GetAll(x => !x.IsDeleted).OrderBy(p => p.ConfigurationID).OrderByDescending(p => p.CreateDate).Select(p => p.DeliveryPriceVatPercent).FirstOrDefault();
        }
        public bool GetIsCloseCompletePurchase()
        {
            return Uow.Configuration.GetAll(x => !x.IsDeleted).OrderBy(p => p.ConfigurationID).OrderByDescending(p => p.CreateDate).Select(p => p.IsCloseCompletePurchase).FirstOrDefault();
        }
        public VatPriceViewModel GetVatForPrice(decimal Price)
        {
            var vatPercent = GetDeliveryPriceVatPercent();
            var VatValue = Price * vatPercent / 100;
            var PriceWithVat = Price + VatValue;
            VatPriceViewModel vatPriceViewModel = new VatPriceViewModel();
            vatPriceViewModel.VatValue = Math.Round(VatValue, 2);
            vatPriceViewModel.PriceWithVat = Math.Round(PriceWithVat, 2);
            return vatPriceViewModel;
        }
        public VatPriceViewModel GetVatForPrice(decimal vatPercent, decimal Price)
        {
            var VatValue = Price * vatPercent / 100;
            var PriceWithVat = Price + VatValue;
            VatPriceViewModel vatPriceViewModel = new VatPriceViewModel();
            vatPriceViewModel.VatValue = Math.Round(VatValue, 2);
            vatPriceViewModel.PriceWithVat = Math.Round(PriceWithVat, 2);
            return vatPriceViewModel;
        }
        public IQueryable<ConfigurationViewModel> GetConfigurations()
        {
            var xdata = Uow.Configuration.GetAll(x => !x.IsDeleted).OrderBy(p => p.ConfigurationID).OrderByDescending(p => p.CreateDate)
             .Select(p => new ConfigurationViewModel
             {
                 ConfigurationID = p.ConfigurationID,
                 CompanNameAr = p.CompanNameAr,
                 CompanNameEn = p.CompanNameEn,
                 PhoneNumber = p.PhoneNumber,
                 Fax = p.Fax,
                 MobileNumber = p.MobileNumber,
                 CRNumber = p.CRNumber,
                 Address = p.Address,
                 StreetNo = p.StreetNo,
                 CRImage = p.CRImage,
                 Logo = p.Logo,
                 Banner = p.Banner,
                 IsEnable = p.IsEnable,
                 SeconedEmail = p.SeconedEmail,
                 Email = p.Email,
                 DeliveryPrice = p.DeliveryPrice.ToString(),
                 MinDeliveryPrice = p.MinDeliveryPrice.ToString(),
                 MaxDeliveryPrice = p.MaxDeliveryPrice.ToString(),

                 DeliveryPriceVatPercent = p.DeliveryPriceVatPercent,
                 DeliveryPriceWithoutVat = (double)p.DeliveryPriceWithoutVat,
                 MinKM = p.MinKM.ToString(),
                 OverKmFare = p.OverKmFare.ToString(),
                 WhatsappLink = p.WhatsappLink,
                 TwitterLink = p.TwitterLink,
                 InstagramLink = p.InstagramLink,
                 SnapchatLink = p.SnapchatLink,

             });
            return xdata;
        }
        #endregion     
    }
}
