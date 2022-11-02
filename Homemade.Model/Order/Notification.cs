using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Homemade.Model.Customer;
using Homemade.Model.Vendor;
using Homemade.Model.Driver;

namespace Homemade.Model.Order
{
    [Table("Notification", Schema = "Order")]
    public class Notification : BaseEntity
    {
        public Notification()
        {
            NotificationGuid = Guid.NewGuid();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NotificationID { get; set; }
        public Guid NotificationGuid { get; set; }

        public DateTime NotificationDate { get; set; }
        public string TitleAR { get; set; }
        public string TitleEN { get; set; }
        public string DescAR { get; set; }
        public string DescEN { get; set; }
        public bool IsSeen { get; set; }
        public int NotificationTypeID { get; set; }
        #region FK
        public int? VendorsID { get; set; }
        public int? OrderVendorID { get; set; }
        public int? CustomersID { get; set; }
        public int? OrderRateID { get; set; }
        public int? ProdQuestionID { get; set; }
        public int? DriverBlanceID { get; set; }
        public int? DriversID { get; set; }


        public virtual User User { get; set; }
        public virtual Vendors Vendors { get; set; }
        public virtual Customers Customers { get; set; }
        public virtual OrderVendor OrderVendor { get; set; }
        public virtual OrderRate OrderRate { get; set; }
        public virtual ProdQuestion ProdQuestion { get; set; }
        public virtual Drivers Drivers { get; set; }
        public virtual DriverBlance DriverBlance { get; set; }
        #endregion
    }
}
