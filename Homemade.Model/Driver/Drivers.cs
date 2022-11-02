using Homemade.Model.Order;
using Homemade.Model.Setting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.Model.Driver
{
    [Table("Drivers", Schema = "Driver")]
    public class Drivers : BaseEntity
    {
        public Drivers()    
        {
            DriverGuid = Guid.NewGuid();
            OrderVendor = new HashSet<OrderVendor>();
            DriverBlance = new HashSet<DriverBlance>();
            Notification = new HashSet<Notification>();
            TransactionSTCPay = new HashSet<TransactionSTCPay>();
            DriverSupport = new HashSet<DriverSupport>();
            CaptainZone = new HashSet<CaptainZone>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        public DateTime? BirthDate { get; set; }
        public string HijriBirthDate { get; set; }
        public string IDNo { get; set; }
        public DateTime? IDDate { get; set; }
        public string HijriIDDate { get; set; }
        public string PrivateLicenseNumber { get; set; }
        public byte PrivateLicenseTypeEndDate { get; set; }
        public DateTime? PrivateLicenseEndDate { get; set; }
        public string PrivateHijriLicenseEndDate { get; set; }
        public byte PCOEndDateType { get; set; }
        public string PCONumber { get; set; }
        public DateTime? PCOEndDate { get; set; }
        public string PCOHijriEndDate { get; set; }
        public string InsuranceNumber { get; set; }
        public byte InsuranceEndDateType { get; set; }
        public DateTime? InsuranceEndDate { get; set; }
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
        #endregion
        public int? RegionCityID { get; set; }
        public int RequestStatusId { get; set; }
        public string RequestNotes { get; set; }
        public bool IsActive { get; set; }
        public bool IsOnline { get; set; }
        public virtual RegionCity RegionCity { get; set; }
        public virtual User User { get; set; }
        public virtual City City { get; set; }
        public virtual Nationality Nationality { get; set; }
        public virtual ICollection<TransactionSTCPay> TransactionSTCPay { get; set; }
        public virtual ICollection<OrderVendor> OrderVendor { get; set; }
        public virtual ICollection<DriverBlance> DriverBlance { get; set; }
        public virtual ICollection<Notification> Notification { get; set; }
        public virtual ICollection<DriverSupport> DriverSupport { get; set; }
        public virtual ICollection<CaptainZone> CaptainZone { get; set; }

    }
}
