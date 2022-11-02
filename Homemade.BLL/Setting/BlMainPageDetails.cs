using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Homemade.BLL.General;
using Homemade.BLL.ViewModel.Setting;
using Homemade.DAL.Contract;
using Homemade.Model.Setting;

namespace Homemade.BLL.Setting
{
    public class BlMainPageDetails
    {
        #region Decleration
        private readonly BLGeneral _blGeneral;
        private readonly IUOW Uow;
        public BlMainPageDetails(IUOW _uow, BLGeneral blGeneral)
        {
            _blGeneral = blGeneral;
            this.Uow = _uow;
        }
        #endregion
        #region Helpers
        /// Check If MainPageDetails Name Is Used Before Or Not اختبار هل اسم الصفحة عربي موجود من قبل ام لا
        public bool IsExist(int MainPageDetailsID, string mainPageTitle , Language language) => MainPageDetailsID != 0 ? (language == Language.Arabic ? Uow.MainPageDetails.GetAll().Any(p => p.MainPageDetailsID != MainPageDetailsID && p.TitleAr.Trim() == mainPageTitle && !p.IsDeleted) : Uow.MainPageDetails.GetAll().Any(p => p.MainPageDetailsID != MainPageDetailsID && p.TitleEn.Trim() == mainPageTitle && !p.IsDeleted)) : (language == Language.Arabic ? Uow.MainPageDetails.GetAll().Any(p => p.TitleAr.Trim() == mainPageTitle && !p.IsDeleted) : Uow.MainPageDetails.GetAll().Any(p => p.TitleEn.Trim() == mainPageTitle && !p.IsDeleted));
        public List<MainPageDetailsViewModel> GetMainPageDetailstaghelper(string lang , int mainPageId) => Uow.MainPageDetails.GetAll(p => !p.IsDeleted && p.MainPageID == mainPageId).OrderByDescending(p => p.CreateDate).Select(p => new MainPageDetailsViewModel()
        {
            MainPageDetailsID = p.MainPageDetailsID,
            TitleAr = p.TitleAr,
            TitleEn = p.TitleEn,
            MainPageID = p.MainPageID,
            MainPageName = p.MainPage!=null ? lang == "ar" ? p.MainPage.NameAr : p.MainPage.NameEn : "", 
            IsEnable = p.IsEnable,
            EnableDate = p.EnableDate,
            IsDeleted = p.IsDeleted,
            CreateDate = p.CreateDate,
            DeleteDate = p.DeleteDate,
            UserId = p.UserId,
            UserDelete = p.UserDelete,
            UserEnable = p.UserEnable,
            UpdateDate = p.UpdateDate,
            UserUpdate = p.UserUpdate,
            MainPageDetailsGuid = p.MainPageDetailsGuid, 
            DescAr = p.DescAr ,
            DescEn = p.DescEn , 
            Image =p.Image ,
            VedioLink = p.VedioLink ,
            MainPageDetailsTitle = lang == "ar" ? p.TitleAr : p.TitleEn,
            Desc = lang == "ar" ? p.DescAr : p.DescEn,
        }).ToList();
        public MainPageDetailsViewModel GetMainPageDetailsViewModelForTermsVendor(string lang) => Uow.MainPageDetails.GetAll(p => !p.IsDeleted && p.MainPage.MainPageTypeId == (int)MainPageTypeEnum.Terms_Conditions_Vendor).OrderByDescending(p => p.CreateDate).Select(p => new MainPageDetailsViewModel()
        {
            MainPageDetailsID = p.MainPageDetailsID,
            TitleAr = p.TitleAr,
            TitleEn = p.TitleEn,
            MainPageID = p.MainPageID,
            MainPageName = p.MainPage != null ? lang == "ar" ? p.MainPage.NameAr : p.MainPage.NameEn : "",
            IsEnable = p.IsEnable,
            EnableDate = p.EnableDate,
            IsDeleted = p.IsDeleted,
            CreateDate = p.CreateDate,
            DeleteDate = p.DeleteDate,
            UserId = p.UserId,
            UserDelete = p.UserDelete,
            UserEnable = p.UserEnable,
            UpdateDate = p.UpdateDate,
            UserUpdate = p.UserUpdate,
            MainPageDetailsGuid = p.MainPageDetailsGuid,
            DescAr = p.DescAr,
            DescEn = p.DescEn,
            Image = p.Image,
            VedioLink = p.VedioLink,
            MainPageDetailsTitle = lang == "ar" ? p.TitleAr : p.TitleEn,
            Desc = lang == "ar" ? p.DescAr : p.DescEn,
        }).FirstOrDefault();
        public List<TermsConditionsAPIViewModel> GetAllTermsConditionsAPIViewModel(int? typeId, string lang)
        {
            int MainPageTypeId = (int)MainPageTypeEnum.Terms_Conditions_Customer;
            if(typeId == (int)TermsConditionsAPIEnum.Vendor)
            {
                MainPageTypeId = (int)MainPageTypeEnum.Terms_Conditions_Vendor;
            }
            if (typeId == (int)TermsConditionsAPIEnum.Captain)
            {
                MainPageTypeId = (int)MainPageTypeEnum.Terms_Conditions_Captain;
            }
            return Uow.MainPageDetails.GetAll(p => !p.IsDeleted 
            && p.MainPage.MainPageTypeId == MainPageTypeId
            ).OrderByDescending(p => p.CreateDate).Select(p => new TermsConditionsAPIViewModel()
            {
                title = lang == "ar" ? p.TitleAr : p.TitleEn,
                desc = lang == "ar" ? p.DescAr : p.DescEn,
            }).ToList();
        }
        #endregion
        #region Actions
        /// Add New MainPageDetails اضافة دولة
        public bool Insert(MainPageDetailsViewModel MainPageDetailsModel, out int MainPageDetailsID)
        {
            MainPageDetails MainPageDetails = new MainPageDetails
            {
                TitleAr = MainPageDetailsModel.TitleAr,
                TitleEn = MainPageDetailsModel.TitleEn,
                CreateDate = _blGeneral.GetDateTimeNow(),
                UserId = MainPageDetailsModel.UserId,
                IsDeleted = false,
                IsEnable = true,
                DescEn = MainPageDetailsModel.DescEn, 
                DescAr = MainPageDetailsModel.DescAr ,
                MainPageID = MainPageDetailsModel.MainPageID ,
                VedioLink = MainPageDetailsModel.VedioLink ,
                Image = MainPageDetailsModel.Image , 
                IdeaTitleAr = MainPageDetailsModel.IdeaTitleAr ,
                IdeaTitleEn = MainPageDetailsModel.IdeaTitleEn,
                IdeaDescAr = MainPageDetailsModel.IdeaDescAr,
                IdeaDescEn = MainPageDetailsModel.IdeaDescEn,
                HomeTitleAr = MainPageDetailsModel.HomeTitleAr,
                HomeTitleEn = MainPageDetailsModel.HomeTitleEn,
                HomeDescAr = MainPageDetailsModel.HomeDescAr,
                HomeDescEn = MainPageDetailsModel.HomeDescEn,
            };
            MainPageDetails = Uow.MainPageDetails.Insert(MainPageDetails);
            Uow.Commit();
            MainPageDetailsID = MainPageDetails.MainPageDetailsID;
            return true;
        }
        /// Delete MainPageDetails By ID حذف الصفحة
        public bool Delete(int id, int userId)
        {
            var MainPageDetails = GetById(id);
            if (MainPageDetails != null)
            {
                MainPageDetails.IsDeleted = true;
                MainPageDetails.UserDelete = userId;
                MainPageDetails.DeleteDate = _blGeneral.GetDateTimeNow();
                Uow.MainPageDetails.Update(MainPageDetails);
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
            var data = Uow.MainPageDetails.GetAll(p => p.MainPageDetailsID == id).FirstOrDefault();

            if (data != null)
            {
                data.UserEnable = userId;
                data.IsEnable = data.IsEnable == Convert.ToBoolean(StatusEnable.Enable) ? Convert.ToBoolean(StatusEnable.Disable) : Convert.ToBoolean(StatusEnable.Enable);
                data.EnableDate = _blGeneral.GetDateTimeNow();
                data = Uow.MainPageDetails.Update(data);
                Uow.Commit();
                return true;
            }
            return false;
        }
        /// Update MainPageDetails تعديل الصفحة
        public bool Update(MainPageDetailsViewModel model)
        {
            IQueryable<MainPageDetails> data = Uow.MainPageDetails.GetAll(p => p.MainPageDetailsID == model.MainPageDetailsID);
            if (data != null)
            {
                MainPageDetails MainPageDetails = data.FirstOrDefault();
                MainPageDetails.TitleAr = model.TitleAr;
                MainPageDetails.TitleEn = model.TitleEn;
                MainPageDetails.UpdateDate = model.UpdateDate;
                MainPageDetails.UserUpdate = model.UserUpdate;
                MainPageDetails.DescAr = model.DescAr;
                MainPageDetails.DescEn = model.DescEn;
                MainPageDetails.Image = model.Image;
                MainPageDetails.VedioLink = model.VedioLink;

                MainPageDetails.IdeaTitleAr = model.IdeaTitleAr;
                MainPageDetails.IdeaTitleEn = model.IdeaTitleEn;
                MainPageDetails.IdeaDescAr = model.IdeaDescAr;
                MainPageDetails.IdeaDescEn = model.IdeaDescEn;
                MainPageDetails.HomeTitleAr = model.HomeTitleAr;
                MainPageDetails.HomeTitleEn = model.HomeTitleEn;
                MainPageDetails.HomeDescAr = model.HomeDescAr;
                MainPageDetails.HomeDescEn = model.HomeDescEn;
                MainPageDetails = Uow.MainPageDetails.Update(MainPageDetails);
                Uow.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }
        public MainPageDetailsViewModel GetMainPageDetailsViewModelForAbout(string lang, int mainPageId, string aboutImage) => Uow.MainPageDetails.GetAll(p => !p.IsDeleted && p.MainPageID == mainPageId).OrderByDescending(p => p.CreateDate).Select(p => new MainPageDetailsViewModel()
        {
            MainPageDetailsID = p.MainPageDetailsID,
            TitleAr = p.TitleAr,
            TitleEn = p.TitleEn,
            MainPageID = p.MainPageID,
            MainPageName = p.MainPage != null ? lang == "ar" ? p.MainPage.NameAr : p.MainPage.NameEn : "",
            IsEnable = p.IsEnable,
            EnableDate = p.EnableDate,
            IsDeleted = p.IsDeleted,
            CreateDate = p.CreateDate,
            DeleteDate = p.DeleteDate,
            UserId = p.UserId,
            UserDelete = p.UserDelete,
            UserEnable = p.UserEnable,
            UpdateDate = p.UpdateDate,
            UserUpdate = p.UserUpdate,
            MainPageDetailsGuid = p.MainPageDetailsGuid,
            DescAr = p.DescAr,
            DescEn = p.DescEn,
            Image = !string.IsNullOrEmpty(p.Image)? (aboutImage+ p.Image): "/assets/homeMadeSite/img/group-diverse-people-having-business-meeting.png",
            VedioLink = p.VedioLink,
            MainPageDetailsTitle = lang == "ar" ? p.TitleAr : p.TitleEn,
            Desc = lang == "ar" ? p.DescAr : p.DescEn,
            IdeaTitle = lang == "ar" ? p.IdeaTitleAr : p.IdeaTitleEn,
            IdeaDesc = lang == "ar" ? p.IdeaDescAr : p.IdeaDescEn,
            HomeTitle = lang == "ar" ? p.HomeTitleAr : p.HomeTitleEn,
            HomeDesc = lang == "ar" ? p.HomeDescAr : p.HomeDescEn,
        }).FirstOrDefault();
        public MainPageDetails GetById(int id) => Uow.MainPageDetails.GetAll(x => x.MainPageDetailsID == id).FirstOrDefault();
        #endregion
        #region GetMainPageDetails
        /// Get All MainPageDetails جلب الصفحة
        public List<MainPageDetails> GetAllMainPageDetails()
        {
            var data = Uow.MainPageDetails.GetAll(x => !x.IsDeleted).ToList();
            return data;
        }
        public List<MainPageDetails> GetAllMainPageDetailssByListMainPage(string[] ListMainPageID)
        {
            if (ListMainPageID != null)
            {
                if (ListMainPageID.Count(p => !string.IsNullOrEmpty(p)) == 0)
                {
                    ListMainPageID = null;
                }
            }
            return Uow.MainPageDetails.GetAll(x => !x.IsDeleted && (ListMainPageID != null ? ListMainPageID.Any(y => y == x.MainPageID.ToString()) : false)).ToList();
        }
        #endregion
    }
}
