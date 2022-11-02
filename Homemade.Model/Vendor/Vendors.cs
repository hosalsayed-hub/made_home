using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;
using Homemade.Model.Setting;
using Homemade.Model.Order;

namespace Homemade.Model.Vendor
{
    [Table("Vendors", Schema = "Vendor")]
    public class Vendors : BaseEntity
    {
        public Vendors()
        {
            VendorsGuid = Guid.NewGuid();
            Product = new HashSet<Product>();
            OrderVendor = new HashSet<OrderVendor>();
            VendorPromo = new HashSet<VendorPromo>();
            EnableHistory = new HashSet<EnableHistory>();
            VacHistory = new HashSet<VacHistory>();
            Notification = new HashSet<Notification>();
            InvoiceMaster = new HashSet<InvoiceMaster>();
            VendorSupport = new HashSet<VendorSupport>();
            VendorBalance = new HashSet<VendorBalance>();
            QuantitiesRequest = new HashSet<QuantitiesRequest>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VendorsID { get; set; }
        public Guid VendorsGuid { get; set; }
        #region Vendor_Data
        public string FirstNameAr { get; set; }
        public string SeconedNameAr { get; set; }
        public string FirstNameEn { get; set; }
        public string SeconedNameEn { get; set; }
        public string MobileNo { get; set; }
        public string ProfilePic { get; set; }
        public string Email { get; set; }
        public int Gender { get; set; } //GenderEnum
        public int RegisterType { get; set; } //RegisterTypeEnum
        public bool IsVac { get; set; }
        public bool IsLocation { get; set; }

        #endregion
        #region Store_Data
        public string StoreNameEn { get; set; }
        public string StoreNameAr { get; set; }
        public string AboutStoreEn { get; set; }
        public string AboutStoreAr { get; set; }
        public string Logo { get; set; }
        public string Banner { get; set; }
        #endregion
        #region Financial_Data
        public string CRnumber { get; set; } //السجل التجارى
        public string CRPic { get; set; } //صورة السجل التجارى
        public string TaxNumber { get; set; }//الرقم الضريبي
        public string AccountNumber { get; set; }//رقم الحساب البنكي
        public string IBANNumber { get; set; }
        public string SwiftCode { get; set; }
        public string MaarofNumber { get; set; }
        public decimal MonthlyTarget { get; set; }
        #endregion
        #region Location
        public string Address { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public decimal? DeliveryPrice { get; set; }
        public int DeliveryType { get; set; }
        #endregion
        public string WorkingTimes { get; set; }
        public bool IsShowContact { get; set; } = true;
        #region Working_Times
        public string DaysWork { get; set; }
        public DateTime? WorkFrom { get; set; }
        public DateTime? WorkTo { get; set; }
        public string DaysVac { get; set; }
        public DateTime? VacWorkFrom { get; set; }
        public DateTime? VacWorkTo { get; set; }
        public bool IsDaysWork { get; set; }
        public bool IsDaysVac { get; set; }
        #endregion
        /// <summary>
        /// رقم البيك اب عند شركه شروق
        /// </summary>
        public string PickupId { get; set; }
        #region FK
        public int CityID { get; set; } //fk
        public int? ActivityID { get; set; } //fk
        public int? BanksID { get; set; } //fk
        public int? PackageID { get; set; } //fk
        public int? BlockID { get; set; } //fk
        public int? NationalityID { get; set; }  //fk
        public int EntryID { get; set; }  // EntryEnum
        public bool IsEnabled { get; set; }
        public virtual Package Package { get; set; }
        public virtual Block Block { get; set; }
        public virtual User User { get; set; }
        public virtual Banks Banks { get; set; }
        public virtual City City { get; set; }
        public virtual Activity Activity { get; set; }
        public virtual Nationality Nationality { get; set; }
        
        public virtual ICollection<VendorBalance> VendorBalance { get; set; }
        public virtual ICollection<VendorSupport> VendorSupport { get; set; }
        public virtual ICollection<InvoiceMaster> InvoiceMaster { get; set; }
        public virtual ICollection<EnableHistory> EnableHistory { get; set; }
        public virtual ICollection<Notification> Notification { get; set; }
        public virtual ICollection<VacHistory> VacHistory { get; set; }
        public virtual ICollection<OrderVendor> OrderVendor { get; set; }
        public virtual ICollection<VendorPromo> VendorPromo { get; set; }
        public virtual ICollection<Product> Product { get; set; }
        public virtual ICollection<QuantitiesRequest> QuantitiesRequest { get; set; }

        #endregion
    }
}
