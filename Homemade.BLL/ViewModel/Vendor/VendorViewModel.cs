using Homemade.BLL.Resources;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Homemade.BLL.ViewModel.Vendor
{
    public class VendorViewModel
    {
        public int VendorsID { get; set; }
        public Guid VendorsGuid { get; set; }
        #region Vendor_Data
        [Required(ErrorMessageResourceName = "NameRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [StringLength(75, ErrorMessageResourceName = "NameMax75", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string FirstNameAr { get; set; }
        public string SeconedNameAr { get; set; }
        [Required(ErrorMessageResourceName = "FirstNameENRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [StringLength(75, ErrorMessageResourceName = "NameMax75", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [RegularExpression(@"^[a-zA-Z\0-9]+$", ErrorMessageResourceName = "EnglishOnly", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string FirstNameEn { get; set; }
        public string SeconedNameEn { get; set; }
        [StringLength(20, MinimumLength = 9, ErrorMessageResourceName = nameof(HomemadeErrorMessages.NameMax20), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        [RegularExpression(@"^(5)[0-9]{8}$", ErrorMessageResourceName = nameof(HomemadeErrorMessages.MobileValid), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        public string MobileNo { get; set; }
        public bool IsEnabled { get; set; }
        public string ProfilePic { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessageResourceName = "GenderRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public int Gender { get; set; } //GenderEnum      
        [Required(ErrorMessageResourceName = "CityRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))] 
        public int CityID { get; set; }
        [Required(ErrorMessageResourceName = "NationalityRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))] 
        public int NationalityID { get; set; }
        [Required(ErrorMessageResourceName = "RegionRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public int RegionID { get; set; }
        public string RegType { get; set; }
        public int UserId { get; set; }
        public string CheckedItems { get; set; }
        public string[] Roles { get; set; }
        public string FirstName { get; set; }
        public string SeconedName { get; set; }
        public int? PackageID { get; set; } //fk
        #endregion
        #region Store_Data
        [Required(ErrorMessageResourceName = "StoreNameEn", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string StoreNameEn { get; set; }
        [Required(ErrorMessageResourceName = "StoreNameAr", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string StoreNameAr { get; set; }
        public string AboutStoreEn { get; set; }
        public string AboutStoreAr { get; set; }
        public string Logo { get; set; }
        public string Banner { get; set; }
        public string CRnumber { get; set; } //السجل التجارى
        public string CRPic { get; set; } //صورة السجل التجارى
        #endregion
        #region Account_Data
        public string TaxNumber { get; set; }//الرقم الضريبي
        public string AccountNumber { get; set; }//رقم الحساب البنكي
        public string IBANNumber { get; set; }
        public string SwiftCode { get; set; }
        public int? BanksID { get; set; } //fk
        #endregion
        #region Location
        public string Address { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        [Required(ErrorMessageResourceName = "BlockRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public int? BlockID { get; set; } //fk
        #endregion
        public bool IsShowContact { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsEnable { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public DateTime? EnableDate { get; set; }
        public int? UserUpdate { get; set; }
        public int? UserDelete { get; set; }
        public int? UserEnable { get; set; }
        public string IsEnableString { get; set; }
        public string GenderString { get; set; }
        public string CityName { get; set; }
        public string RegionName { get; set; }
        public string NationalityName { get; set; }
        public string PackageName { get; set; }
        public string BlockName { get; set; }
        public string BanksName { get; set; }
        public string DeliveryPrice { get; set; }
        [Required(ErrorMessageResourceName = "DeliveryTypeRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public int DeliveryType { get; set; }
        public string DeliveryTypeName { set; get; }
        public string IsVacString { set; get; }
        public string StoreName { get; set; }
        public string ActivityName { get;   set; }
        public string WorkingTimes { get; set; }
        public string IsShowContactString { get; set; }
        public string CreateDateString { get; set; }
        public string EntryString { get; set; }
        #region Working_Times
        public string DaysWork { get; set; }
        public string[] ListDaysWork { get; set; }
        public DateTime? WorkFrom { get; set; }
        public DateTime? WorkTo { get; set; }
        public string WorkFromString { get; set; }
        public string WorkToString { get; set; }
        public string DaysVac { get; set; }
        public string[] ListDaysVac { get; set; }
        public DateTime? VacWorkFrom { get; set; }
        public DateTime? VacWorkTo { get; set; }
        public string VacWorkFromString { get; set; }
        public string VacWorkToString { get; set; }
        public bool IsVendorWorking { get; set; }
        public string IsVendorWorkingString { get; set; }
        #endregion
    }
    public class VendorStoreViewModel
    {
        #region Store_Data
        public int VendorsID { get; set; }
        [RegularExpression(@"^[a-zA-Z\0-9]+$", ErrorMessageResourceName = "EnglishOnly", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string StoreNameEn { get; set; }
        public string StoreNameAr { get; set; }
        public string AboutStoreEn { get; set; }
        public string AboutStoreAr { get; set; }
        public IFormFile LogoFile { get; set; }
        public IFormFile BannerFile { get; set; }
        public string CRnumber { get; set; } //السجل التجارى
        public IFormFile CRPicFile { get; set; } //صورة السجل التجارى
        public string DeliveryPrice { get; set; }
        [Required(ErrorMessageResourceName = "DeliveryTypeRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public int DeliveryType { get; set; }
        #endregion
    }
    public class VendorAccountViewModel
    {
        #region Account_Data
        public int VendorsID { get; set; }
        public string TaxNumber { get; set; }//الرقم الضريبي
        public string AccountNumber { get; set; }//رقم الحساب البنكي
        public string IBANNumber { get; set; }
        public string SwiftCode { get; set; }
        public int? BanksID { get; set; } //fk
        #endregion
    }

    public class VendorLocationViewModel
    {
        #region Location
        public int VendorsID { get; set; }
        public string Address { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
        public int? BlockID { get; set; } //fk
        #endregion
    }
    public class ReturnVendorViewModel
    {
        public int VendorsID { get; set; }
        public bool BoolID { get; set; }
    }


    public class VendorViewModellistApi
    {
        
        public List<VendorViewModel> vendors { get; set; }
        public bool isNextPage { get; set; }

    }
    public class VendorWorkingTime
    {
        public string DaysWork { get; set; }
        public string DaysVac { get; set; }
        public string SiteDaysWork { get; set; }
        public string SiteDaysVac { get; set; }
        public string SiteTimeWork { get; set; }
        public string SiteTimeVac { get; set; }
    }
}
