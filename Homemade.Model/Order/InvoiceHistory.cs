using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.Model.Order
{
    [Table("InvoiceHistory", Schema = "Order")]
    public class InvoiceHistory : BaseEntity
    {
        public InvoiceHistory()
        {
            InvoiceHistoryGuid = Guid.NewGuid();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InvoiceHistoryID { get; set; }
        public Guid InvoiceHistoryGuid { get; set; }
        public int InvoiceMasterID { get; set; }
        public int InvoiceTypeId { get; set; }  // InvoiceTypeEnum
        public int ActionType { get; set; } //ActionEnum
        public virtual User User { get; set; }
        public virtual InvoiceMaster InvoiceMaster { get; set; }
    }
}
