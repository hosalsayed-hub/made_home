using Homemade.Model.Order;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Transactions;

namespace Homemade.Model.Driver
{
    [Table("DriverBlance",Schema = "Driver")]
    public class DriverBlance : BaseEntity
    {
        public DriverBlance()
        {
            DriverBlanceGuid = Guid.NewGuid();
            Notification = new HashSet<Notification>();
            TransactionSTCPay = new HashSet<TransactionSTCPay>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DriverBlanceID { get; set; }
        public Guid DriverBlanceGuid { get; set; }
        public decimal Amount { get; set; }
        public int TransactionTypeID { get; set; } //fk
        public decimal Before { get; set; }
        public decimal After { get; set; }
        public int DriversID { get; set; }  //fk
        public int TypeOperationID { get; set; } //TypeOperationEnum
        public int? OrderVendorID { get; set; }
        public string Attachment { get; set; }
        public string Discripe { get; set; }
        public DateTime DateOperation { get; set; }
        public virtual User User { get; set; }
        public virtual OrderVendor OrderVendor { get; set; }
        public virtual Drivers Drivers { get; set; }
        public virtual TransactionType TransactionType { get; set; }
        public virtual ICollection<Notification> Notification { get; set; }
        public virtual ICollection<TransactionSTCPay> TransactionSTCPay { get; set; }

    }
}
