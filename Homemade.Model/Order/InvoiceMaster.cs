using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using Homemade.Model.Vendor;

namespace Homemade.Model.Order
{
    [Table("InvoiceMaster", Schema = "Order")]
    public class InvoiceMaster : BaseEntity
    {
        public InvoiceMaster()
        {
            InvoiceMasterGuid = Guid.NewGuid();
            InvoiceDetails = new HashSet<InvoiceDetails>();
            InvoiceHistory = new HashSet<InvoiceHistory>();
            ListTransfer = new HashSet<ListTransfer>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InvoiceMasterID { get; set; }
        public Guid InvoiceMasterGuid { get; set; }
        public int InvoiceStatusId { get; set; }
        public int InvoiceTypeId { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
        public decimal Vat { get; set; }
        public decimal Total { get; set; }
        public string InvoiceNo { get; set; }
        public decimal PerStore { get; set; }
        public decimal PerHomeMade { get; set; }
        public int VendorsID { get; set; }
        public virtual User User { get; set; }
        public virtual Vendors Vendors { get; set; }
        public virtual ICollection<InvoiceDetails> InvoiceDetails { get; set; }
        public virtual ICollection<ListTransfer> ListTransfer { get; set; }
        public virtual ICollection<InvoiceHistory> InvoiceHistory { get; set; }

    }
}
