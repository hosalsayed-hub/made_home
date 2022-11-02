using Homemade.Model.Customer;
using Homemade.Model.Vendor;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homemade.Model.Driver
{
    [Table("TransactionType", Schema = "Driver")]

    public class TransactionType : BaseEntity
    {
        public TransactionType()
        {
            TransactionTypeGuid = Guid.NewGuid();
            DriverBlance = new HashSet<DriverBlance>();
            CustomerBalance = new HashSet<CustomerBalance>();
            VendorBalance = new HashSet<VendorBalance>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionTypeID { get; set; }
        public Guid TransactionTypeGuid { get; set; }

        [StringLength(75)]
        public string NameAR { get; set; }

        [StringLength(75)]
        public string NameEN { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<CustomerBalance> CustomerBalance { get; set; }
        public virtual ICollection<DriverBlance> DriverBlance { get; set; }
        public virtual ICollection<VendorBalance> VendorBalance { get; set; }
    }
}
