using Homemade.Model.Order;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Homemade.Model.BankTransaction
{
    [Table("TransactionCard", Schema = "Transaction")]
    public class TransactionCard
    {
        public TransactionCard()
        {
            TransactionGUID = Guid.NewGuid();
            TransactionCardLog = new HashSet<TransactionCardLog>();
        }

        [Key]
        public int TransactionID { get; set; }
        public Guid TransactionGUID { get; set; }
        public int? OrdersID { get; set; }
        public int CustomersID { get; set; }



        public string PaymentID { get; set; }
        public string InvoiceId { get; set; }
        public string CustomerReference { get; set; }
        public string PaymentMethodId { get; set; }
        public string PaymentUrl { get; set; }

        
        public double Amount { get; set; }
        public string Status { get; set; }
        public int TransactionStatus { get; set; } // Enum TransactionEnum
        public int LastStatusUpdateFrom { get; set; } // Enum LastStatusUpdateFrom


        public DateTime RequestDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsRedirect { get; set; }
        public bool IsSMSSentToUser { get; set; }

        public string Message { get; set; }

        public virtual Orders Orders { get; set; }


        public virtual ICollection<TransactionCardLog> TransactionCardLog { get; set; }
    }
}
