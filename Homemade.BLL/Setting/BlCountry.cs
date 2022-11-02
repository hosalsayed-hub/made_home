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
    public class BlCountry
    {
        #region Decleration
        private readonly BLGeneral _blGeneral;
        private readonly IUOW Uow;
        public BlCountry(IUOW _uow, BLGeneral blGeneral)
        {
            _blGeneral = blGeneral;
            this.Uow = _uow;
        }
        #endregion
        #region GetCountries
        /// Get All Countries جلب الدول
        public List<CountryViewModelApi> GetCountryList(string lang) => Uow.Country.GetAll(p => !p.IsDeleted).OrderByDescending(p => p.CreateDate).Select(p => new CountryViewModelApi()
        { CountryID = p.CountryID, CountryName = lang == "ar"?p.NameAR:p.NameEN, Flag = p.Flag, Code = p.Extension , length = p.CountryID == 1 ? 9 : 10}).ToList();
        public List<KeyWordViewModelApi> GetKeyWordsList(string lang,int departmentID) => Uow.KeyWords.GetAll(p => !p.IsDeleted && p.DepartmentsID == departmentID).OrderByDescending(p => p.CreateDate).Select(p => new KeyWordViewModelApi()
        { KeyWordsID = p.KeyWordsID, Name = lang == "ar" ? p.NameAR : p.NameEN }).ToList();
        public List<Country> GetAllCountryByOperators()
        {
            var data = Uow.Country.GetAll(x => !x.IsDeleted).ToList();
            return data;
        }
        #endregion
        public List<MainPageViewModelApi> GetmainByType (int Type,string lang)
        {
            return Uow.MainPageDetails.GetAll(s => s.MainPage.MainPageTypeId == Type).Select(s => new MainPageViewModelApi() { 
            title = lang == "ar" ? s.TitleAr : s.TitleEn,
            description = lang == "ar" ? s.DescAr : s.DescEn,
            }).ToList();
        }
        public AboutViewModel GetAbout(string lang,string aboutpath)
        {
            var listabout = new List<ListContactus>();
            var ListBranches = new List<ListBranches>();
            var configration = Uow.Configuration.GetAll().FirstOrDefault();
            var cites = Uow.Orders.GetAll(e => !e.IsDeleted).Select(x => x.Customers.CityID).Distinct().Count();
            var orders = Uow.OrderVendor.GetAll(e => !e.IsDeleted).Count();
            var users = Uow.Customers.GetAll(e => !e.IsDeleted).Count();
            listabout.Add(new ListContactus() { number = configration.MobileNumber ,icon = true});
            listabout.Add(new ListContactus() { number = configration.PhoneNumber, icon = true });
            listabout.Add(new ListContactus() { number = configration.Email, icon = false });
            listabout.Add(new ListContactus() { number = configration.Fax, icon = true });
            ListBranches.Add(new ListBranches() { branch = configration.Address });
            ListBranches.Add(new ListBranches() { branch = configration.StreetNo });
            return Uow.MainPageDetails.GetAll(s => s.MainPage.MainPageTypeId == (int)MainPageTypeEnum.HowWeAre).Select(s => new AboutViewModel()
            {
                title = lang == "ar" ? s.TitleAr : s.TitleEn,
                description = lang == "ar" ? s.DescAr : s.DescEn,
                cities = cites,
                orders = orders,
                users = users,
                image = !string.IsNullOrWhiteSpace(s.Image) ? aboutpath+ s.Image : s.Image,
                branchs = ListBranches,
                numbers = listabout
            }).FirstOrDefault();
        }
    }
}
