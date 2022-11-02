using Homemade.BLL.Resources;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Homemade.BLL.ViewModel.Driver
{
    public class AssignDriverVewModel
    {

        public int DriverID { get; set; }
        public Guid DriverGuid { get; set; }
        public int? RegionCityID { get; set; }
        [Required(ErrorMessageResourceName = nameof(HomemadeErrorMessages.CityRequierd), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        public int CityID { get; set; }

        public string CityName { get; set; }

        [Required(ErrorMessageResourceName = nameof(HomemadeErrorMessages.Required), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        public int CountryID { get; set; }

        public string CountryName { get; set; }
        public string NationalityName { get; set; }

        public bool IsActive { get; set; }

        [Required(ErrorMessageResourceName = nameof(HomemadeErrorMessages.Required), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        [StringLength(20, ErrorMessageResourceName = nameof(HomemadeErrorMessages.NameMax20), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        public string CarNumber { get; set; }
        public string InsuranceNumber { get; set; }
        public byte InsuranceEndDateType { get; set; }

        public string InsuranceEndDate { get; set; }

        [StringLength(10, ErrorMessageResourceName = nameof(HomemadeErrorMessages.NameMax10), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        public string HijiriInsuranceEndDate { get; set; }

        public string CarPictrue { get; set; }


        [Required(ErrorMessageResourceName = nameof(HomemadeErrorMessages.ArNameRequierd), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        [StringLength(75, ErrorMessageResourceName = nameof(HomemadeErrorMessages.NameMax75), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        [RegularExpression(@"^[\u0621-\u064A\u0660-\u0669\0-9]+$", ErrorMessageResourceName = nameof(HomemadeErrorMessages.ArabicOnly),
            ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        public string NameAr { get; set; }

        [Required(ErrorMessageResourceName = nameof(HomemadeErrorMessages.EnNameRequierd) , ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        [StringLength(75, ErrorMessageResourceName = nameof(HomemadeErrorMessages.NameMax75), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        [RegularExpression(@"^[a-zA-Z\0-9]+$", ErrorMessageResourceName = nameof(HomemadeErrorMessages.EnglishOnly), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        public string NameEn { get; set; }

        [Required(ErrorMessageResourceName = nameof(HomemadeErrorMessages.Required), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        [StringLength(10, ErrorMessageResourceName = nameof(HomemadeErrorMessages.NameMax10), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        [RegularExpression(@"^(5)[0-9]{8}$", ErrorMessageResourceName = nameof(HomemadeErrorMessages.InvalidMobile), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        [Display(Name = "Mobil No")]
        public string PhoneNumber { get; set; }

        [StringLength(100, ErrorMessageResourceName = nameof(HomemadeErrorMessages.NameMax100), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        [RegularExpression(@"^[_a-z0-9-]+(.[a-z0-9-]+)@[a-z0-9-]+(.[a-z0-9-]+)*(.[a-z]{2,4})$", ErrorMessageResourceName = nameof(HomemadeErrorMessages.Email), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        public string Email { get; set; }

        [StringLength(100, ErrorMessageResourceName = nameof(HomemadeErrorMessages.NameMax100), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        [Display(Name = "Address")]
        public string Address { get; set; }

        public byte Gender { get; set; }

        public byte BirthDateType { get; set; }

        public string BirthDate { get; set; }

        public int VerifyStc { get; set; }

        public string HijriBirthDate { get; set; }

        [StringLength(10, ErrorMessageResourceName = nameof(HomemadeErrorMessages.NameMax10), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        [Display(Name = "Id No")]
        [Required(ErrorMessageResourceName = nameof(HomemadeErrorMessages.Required), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        public string IDNo { get; set; }

        [StringLength(15, ErrorMessageResourceName = nameof(HomemadeErrorMessages.NameMax15), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        public string PrivateLicenseNumber { get; set; }

        public byte PrivateLicenseTypeEndDate { get; set; }

        public string PrivateLicenseEndDate { get; set; }

        public string PrivateHijriLicenseEndDate { get; set; }

        public byte PCOEndDateType { get; set; }

        public string PCONumber { get; set; }

        public string PCOEndDate { get; set; }

        public string PCOHijriEndDate { get; set; }

        public string DriverInsuranceNumber { get; set; }
        public byte DriverInsuranceEndDateType { get; set; }

        public string DriverInsuranceEndDate { get; set; }

        [StringLength(10, ErrorMessageResourceName = nameof(HomemadeErrorMessages.NameMax10), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        public string DriverHijiriInsuranceEndDate { get; set; }

        public string PersonalPicture { get; set; }

        public string LicensePicture { get; set; }

        public string IDPicture { get; set; }
        public string CarLicensePicture { get; set; }//صورة رخصة القياده 
        [Required(ErrorMessageResourceName = nameof(HomemadeErrorMessages.Required), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        public int NationalityID { get; set; }

        #region NewData
        [Required(ErrorMessageResourceName = nameof(HomemadeErrorMessages.Required), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        [StringLength(20, ErrorMessageResourceName = nameof(HomemadeErrorMessages.NameMax20), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        public string CarSerialNumber { get; set; }
        [Required(ErrorMessageResourceName = nameof(HomemadeErrorMessages.Required), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        [StringLength(10, ErrorMessageResourceName = nameof(HomemadeErrorMessages.NameMax10), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        [RegularExpression(@"^(5)[0-9]{8}$", ErrorMessageResourceName = nameof(HomemadeErrorMessages.InvalidMobile), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        [Display(Name = "Mobil No STC")]
        public string PhoneNumberStc { get; set; }
        public string BankAccountPicture { get; set; }
        [StringLength(100, ErrorMessageResourceName = nameof(HomemadeErrorMessages.NameMax100), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        [Display(Name = "IBAN Number")]
        public string IBANNumber { get; set; }
        [Required(ErrorMessageResourceName = nameof(HomemadeErrorMessages.Required), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        public byte PhoneType { get; set; }
        [StringLength(75, ErrorMessageResourceName = nameof(HomemadeErrorMessages.NameMax75), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        public string NickName { get; set; }
        public string FileNumber { get; set; }
        public string RegionCityName { get; set; }
        #endregion
        public IFormFile fupCarLicensePicture { get; set; }//صورة رخصة القياده 
        public IFormFile fupCarPictrue { get; set; }
        public IFormFile fupPersonalPicture { get; set; }
        public IFormFile fupLicensePicture { get; set; }
        public IFormFile fupIDPicture { get; set; }
        public IFormFile fupBankAccountPicture { get; set; }
    }
}
