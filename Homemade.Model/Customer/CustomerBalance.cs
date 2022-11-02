using Homemade.Model.Driver;
using Homemade.Model.Order;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.Model.Customer
{
    [Table("CustomerBalance", Schema = "Customer")]
   public class CustomerBalance : BaseEntity
    {
        public CustomerBalance()
        {
            CustomerBlanceGuid = Guid.NewGuid();
            TabCharge = new HashSet<TabCharge>();

        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerBlanceID { get; set; }
        public Guid CustomerBlanceGuid { get; set; }
        public decimal Amount { get; set; }
        public int TransactionTypeID { get; set; } //fk
        public decimal Before { get; set; }
        public decimal After { get; set; }
        public int CustomersID { get; set; }  //fk
        public int TypeOperationID { get; set; } //TypeOperationEnum
        public int? OrderVendorID { get; set; }
        public string Attachment { get; set; }
        public string Discripe { get; set; }
        public DateTime DateOperation { get; set; }
        public virtual User User { get; set; }
        public virtual OrderVendor OrderVendor { get; set; }
        public virtual Customers Customers { get; set; }
        public virtual TransactionType TransactionType { get; set; }
        public virtual ICollection<TabCharge> TabCharge { get; set; }
    }
}
