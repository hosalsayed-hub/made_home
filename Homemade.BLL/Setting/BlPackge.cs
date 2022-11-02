using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Homemade.BLL.General;
using Homemade.BLL.ViewModel.Setting;
using Homemade.DAL.Contract;
using Homemade.Model.Setting;

namespace Homemade.BLL.Setting
{
    public class BlPackage
    {
        #region Decleration
        private readonly BLGeneral _blGeneral;
        private readonly IUOW Uow;
        public BlPackage(IUOW _uow, BLGeneral blGeneral)
        {
            _blGeneral = blGeneral;
            this.Uow = _uow;
        }
        #endregion
        #region Helpers
        /// Check If Package Name Is Used Before Or Not اختبار هل اسم الباقة عربي موجود من قبل ام لا
        public bool IsExist(int PackageID, string PackageName, Language language) => PackageID != 0 ? (language == Language.Arabic ? Uow.Package.GetAll().Any(p => p.PackageID != PackageID && p.NameAR.Trim() == PackageName && !p.IsDeleted) : Uow.Package.GetAll().Any(p => p.PackageID != PackageID && p.NameEN.Trim() == PackageName && !p.IsDeleted)) : (language == Language.Arabic ? Uow.Package.GetAll().Any(p => p.NameAR.Trim() == PackageName && !p.IsDeleted) : Uow.Package.GetAll().Any(p => p.NameEN.Trim() == PackageName && !p.IsDeleted));
        public List<PackageViewModel> GetPackagetaghelper(string lang) => Uow.Package.GetAll(p => !p.IsDeleted).OrderByDescending(p => p.CreateDate).Select(p => new PackageViewModel()
        {
            PackageID = p.PackageID,
            PackageNameAR = p.NameAR,
            PackageNameEN = p.NameEN,
            IsEnable = p.IsEnable,
            EnableDate = p.EnableDate,
            IsDeleted = p.IsDeleted,
            CreateDate = p.CreateDate,
            DeleteDate = p.DeleteDate,
            UserId = p.UserId,
            UserDelete = p.UserDelete,
            UserEnable = p.UserEnable,
            DescriptionAr = !string.IsNullOrEmpty(p.DescAr) ? p.DescAr : "",
            DescriptionEn = !string.IsNullOrEmpty(p.DescEn) ? p.DescEn : "",
            UpdateDate = p.UpdateDate,
            UserUpdate = p.UserUpdate,
            PackageGuid = p.PackageGuid,
            PackageName = lang == "ar" ? p.NameAR : p.NameEN,
            Percent =p.Percent.ToString() , 
            Price = p.Price.ToString() ,
            PackageType = p.PackageType ,
        }).ToList();
        #endregion
        public List<PackageViewModelApi> GetPackageApi(string lang) => Uow.Package.GetAll(p => !p.IsDeleted).OrderByDescending(p => p.CreateDate).Select(p => new PackageViewModelApi()
        {
            packageID = p.PackageID,
            packageName = lang == "ar" ? p.NameAR : p.NameEN,
        }).ToList();
        #region Actions
        /// Add New Package اضافة أدوية
        public bool Insert(PackageViewModel PackageModel, out int PackageID)
        {
            Package Package = new Package
            {
                NameAR = PackageModel.PackageNameAR,
                NameEN = PackageModel.PackageNameEN,
                CreateDate = _blGeneral.GetDateTimeNow(),
                UserId = PackageModel.UserId,
                IsDeleted = false,
                IsEnable = true,
                DescAr = PackageModel.DescriptionAr,
                DescEn = PackageModel.DescriptionEn,
                Percent = decimal.Parse(PackageModel.Percent , CultureInfo.InvariantCulture) ,
                Price = decimal.Parse(PackageModel.Price, CultureInfo.InvariantCulture),
                PackageType = (int)PackageTypeEnum.Other
            };
            Package = Uow.Package.Insert(Package);
            Uow.Commit();
            PackageID = Package.PackageID;
            return true;
        }
        /// Delete Package By ID حذف الباقة
        public bool Delete(int id, int userId)
        {
            var Package = GetById(id);
            if (Package != null)
            {
                Package.IsDeleted = true;
                Package.UserDelete = userId;
                Package.DeleteDate = _blGeneral.GetDateTimeNow();
                Uow.Package.Update(Package);
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
            var data = Uow.Package.GetAll(p => p.PackageID == id).FirstOrDefault();

            if (data != null)
            {
                data.UserEnable = userId;
                data.IsEnable = data.IsEnable == Convert.ToBoolean(StatusEnable.Enable) ? Convert.ToBoolean(StatusEnable.Disable) : Convert.ToBoolean(StatusEnable.Enable);
                data.EnableDate = _blGeneral.GetDateTimeNow();
                data = Uow.Package.Update(data);
                Uow.Commit();
                return true;
            }
            return false;
        }
        /// Update Package تعديل الباقة
        public bool Update(PackageViewModel model)
        {
            IQueryable<Package> data = Uow.Package.GetAll(p => p.PackageID == model.PackageID);
            if (data != null)
            {
                Package Package = data.FirstOrDefault();
                Package.NameAR = model.PackageNameAR;
                Package.NameEN = model.PackageNameEN;
                Package.UpdateDate = model.UpdateDate;
                Package.UserUpdate = model.UserUpdate;
                Package.DescEn = model.DescriptionEn;
                Package.Price = decimal.Parse(model.Price, CultureInfo.InvariantCulture);
                Package.Percent = decimal.Parse(model.Percent, CultureInfo.InvariantCulture);
                Package.DescAr = model.DescriptionAr;
                Package = Uow.Package.Update(Package);

                Uow.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }
        public Package GetById(int id) => Uow.Package.GetAll(x => x.PackageID == id).FirstOrDefault();
        #endregion
        #region GetPackage
        /// Get All Package جلب الباقات
        public List<Package> GetAllPackages()
        {
            var data = Uow.Package.GetAll(x => !x.IsDeleted).ToList();
            return data;
        }
        #endregion
    }
}
