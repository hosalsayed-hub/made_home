using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.ViewModel.Driver
{
   public class DriverViewModel
    {
        public int DriversID { get; set; }
        public int CityID { get; set; }
        public int? NationalityID { get; set; }
        public Guid DriverGuid { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public byte Gender { get; set; }
        public byte BirthDateType { get; set; }
        public string BirthDate { get; set; }
        public string HijriBirthDate { get; set; }
        public string IDNo { get; set; }
        public string IDDate { get; set; }
        public string HijriIDDate { get; set; }
        public string PrivateLicenseNumber { get; set; }
        public byte PrivateLicenseTypeEndDate { get; set; }
        public string PrivateLicenseEndDate { get; set; }
        public string PrivateHijriLicenseEndDate { get; set; }
        public byte PCOEndDateType { get; set; }
        public string PCONumber { get; set; }
        public string PCOEndDate { get; set; }
        public string PCOHijriEndDate { get; set; }
        public string InsuranceNumber { get; set; }
        public byte InsuranceEndDateType { get; set; }
        public string InsuranceEndDate { get; set; }
        public string HijiriInsuranceEndDate { get; set; }
        #region Pictures
        public string PersonalPicture { get; set; } //صوره شخصية
        public string LicensePicture { get; set; } //رخصة القياده
        public string IDPicture { get; set; } //صورة الهويه
        public string CarPictrue { get; set; } //صوره المركية
        #endregion
        #region NewData
        public string PhoneNumberStc { get; set; }
        public byte PhoneType { get; set; }
        public string IBANNumber { get; set; }
        public string BankAccountPicture { get; set; } //صوره الحساب البنكى
        public string NickName { get; set; }
        public string FileNumber { get; set; }
        public int VerifyStc { get; set; }
        public DateTime? VerifyStcDate { get; set; }
        public bool OpenTransaction { get; set; }
        #endregion
        #region Car
        public string CarNumber { get; set; }
        public string CarSerialNumber { get; set; }
        public string CarLicensePicture { get; set; }//صورة رخصة المركبة 
        public string DriverName { get; internal set; }
        public string CityName { get; internal set; }
        public string CountryName { get; internal set; }
        public string GenderName { get; internal set; }
        public decimal Balance { get; internal set; }
        public string CreateDateString { get;  set; }
        public decimal Rate { get;  set; }
        #endregion
        public string RequestStatusName { get; set; }
        public string RegionName { get;  set; }
        public string NationalityName { get;  set; }
        public int RequestStatusId { get;  set; }
        public bool IsEnable { get; set; }
    }
}
