using Homemade.Model.Driver;
using Homemade.Model.Order;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.Model.Vendor
{
    [Table("VendorBalance", Schema = "Vendor")]
    public class VendorBalance : BaseEntity
    {
        public VendorBalance()
        {
            VendorBlanceGuid = Guid.NewGuid();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VendorBlanceID { get; set; }
        public Guid VendorBlanceGuid { get; set; }
        public decimal Amount { get; set; }
        public int TransactionTypeID { get; set; } //fk
        public decimal Before { get; set; }
        public decimal After { get; set; }
        public int VendorsID { get; set; }  //fk
        public int TypeOperationID { get; set; } //TypeOperationEnum
        public int? OrderVendorID { get; set; }
        public string Attachment { get; set; }
        public string Discripe { get; set; }
        public DateTime DateOperation { get; set; }
        public virtual User User { get; set; }
        public virtual OrderVendor OrderVendor { get; set; }
        public virtual Vendors Vendors { get; set; }
        public virtual TransactionType TransactionType { get; set; }
    }
}
