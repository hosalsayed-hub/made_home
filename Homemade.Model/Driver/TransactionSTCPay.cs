using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Homemade.Model.Driver
{
    [Table("TransactionSTCPay", Schema = "Driver")]
    public class TransactionSTCPay : BaseEntity
    {
        public TransactionSTCPay()
        {
            TransactionSTCPayGUID = Guid.NewGuid();
            TranLogSTCPay = new HashSet<TranLogSTCPay>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionSTCPayID { get; set; }
        public Guid TransactionSTCPayGUID { get; set; }
        public int TransactionStatusId { get; set; } //TransactionStatusEnum
        public int STCStatusId { get; set; } //PaidStatusEnum
        public decimal Amount { get; set; }
        public string ResponseMessage { get; set; }
        public string InquriyContent { get; set; }
        public string ItemRefrence { get; set; }
        public string CustomerRefrence { get; set; }
        public string PaymentOrderReference { get; set; }
        public string MobileNo { get; set; }
        public int? DriverBlanceID { get; set; }
        public int DriversID { get; set; }
        
        public virtual Drivers Drivers { get; set; }
        public virtual DriverBlance DriverBlance { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<TranLogSTCPay> TranLogSTCPay { get; set; }

    }
}
