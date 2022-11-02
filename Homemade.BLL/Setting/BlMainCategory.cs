using Homemade.BLL.General;
using Homemade.BLL.ViewModel.Setting;
using Homemade.DAL.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Homemade.Model.Setting;

namespace Homemade.BLL.Setting
{
    public class BlMainCategory
    {
        #region Decleration
        private readonly BLGeneral _blGeneral;
        private readonly IUOW Uow;
        public BlMainCategory(IUOW _uow, BLGeneral blGeneral)
        {
            _blGeneral = blGeneral;
            this.Uow = _uow;
        }
        #endregion
        #region Helpers
        public bool IsExist(int mainCategoryId, string mainCategoryName, Language language) => mainCategoryId != 0 ? (language == Language.Arabic ? Uow.MainCategory.GetAll().Any(p => p.MainCategoryID != mainCategoryId && p.NameAR.Trim() == mainCategoryName && !p.IsDeleted) : Uow.MainCategory.GetAll().Any(p => p.MainCategoryID != mainCategoryId && p.NameEN.Trim() == mainCategoryName && !p.IsDeleted)) : (language == Language.Arabic ? Uow.Country.GetAll().Any(p => p.NameAR.Trim() == mainCategoryName && !p.IsDeleted) : Uow.MainCategory.GetAll().Any(p => p.NameEN.Trim() == mainCategoryName && !p.IsDeleted));
        #endregion
        #region Actions
        public bool Insert(MainCategoryViewModel Model, out int MainCategoryID)
        {
            MainCategory MainCategory = new MainCategory
            {
                NameAR = Model.NameAR,
                NameEN = Model.NameAR,
                CreateDate = _blGeneral.GetDateTimeNow(),
                IsEnable = true,
                UserId = Model.UserId,
             };
            MainCategory = Uow.MainCategory.Insert(MainCategory);
            Uow.Commit();
            MainCategoryID = MainCategory.MainCategoryID;
            return true; ;
        }
        #region Get
        public bool Update(MainCategoryViewModel model)
        {
            var data = Uow.MainCategory.GetAll(p => p.MainCategoryID == model.MainCategoryID).FirstOrDefault();
            if (data != null)
            {
                data.NameAR = model.NameAR;

                data.NameEN = model.NameEN;   
                data.UserUpdate = model.UserUpdate;
                data.UpdateDate = _blGeneral.GetDateTimeNow();
                Uow.MainCategory.Update(data);
                Uow.Commit();
                return true;
            }
            return false;
        }
        #endregion
        public bool Delete(int id, int userId)
        {
            var data = Uow.MainCategory.GetAll(p => p.MainCategoryID == id).FirstOrDefault();
            if (data != null)
            {
                data.IsDeleted = true;
                data.DeleteDate = _blGeneral.GetDateTimeNow();
                data.UserDelete = userId;
                Uow.MainCategory.Update(data);
                Uow.Commit();
                return true;
            }
            return false;
        }
        public bool ChangeStatus(int id, int userId)
        {
            var data = Uow.MainCategory.GetAll(p => p.MainCategoryID == id).FirstOrDefault();

            if (data != null)
            {
                data.UserEnable = userId;
                data.IsEnable = data.IsEnable == Convert.ToBoolean(StatusEnable.Enable) ? Convert.ToBoolean(StatusEnable.Disable) : Convert.ToBoolean(StatusEnable.Enable);
                data.EnableDate = _blGeneral.GetDateTimeNow();
                data = Uow.MainCategory.Update(data);
                Uow.Commit();
                return true;
            }
            return false;
        }
        #endregion
        #region GetMainCategory
        /// Get All MainCategory جلب التصنيفات
        public List<MainCategory> GetAllMainCategories()
        {
            var data = Uow.MainCategory.GetAll(x => !x.IsDeleted).ToList();
            return data;
        }
        #endregion
    }
}
