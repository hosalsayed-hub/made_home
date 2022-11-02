using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Homemade.Model.Customer;
using Homemade.Model.Setting;
using Homemade.Model.BankTransaction;

namespace Homemade.Model.Order
{
    [Table("Orders", Schema = "Order")]
    public class Orders : BaseEntity
    {
        public Orders()
        {
            OrdersGuid = Guid.NewGuid();
            OrderPromo = new HashSet<OrderPromo>();
            OrderVendor = new HashSet<OrderVendor>();
            TransactionCard = new HashSet<TransactionCard>();
            TabCharge = new HashSet<TabCharge>();
            TabChargeExLog = new HashSet<TabChargeExLog>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrdersID { get; set; }
        public Guid OrdersGuid { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal Vat { get; set; }
        public decimal DeliveryPrice { get; set; }
        public decimal Total { get; set; }
        public string Notes { get; set; }
        public int PaymentTypeId { get; set; } //PaymentTypeEnum
        public string InvoiceId { get; set; }
        public string ReferenceNo { get; set; }
        #region Relations
        public int OrderStatusID { get; set; }
        public int CustomersID { get; set; }
        public int CustomerLocationID { get; set; }
        public int? PromoCodeID { get; set; }
        public string Desc { get; set; }

        public virtual User User { get; set; }
        public virtual Customers Customers { get; set; }
        public virtual CustomerLocation CustomerLocation { get; set; }
        public virtual OrderStatus OrderStatus { get; set; }
        public virtual PromoCode PromoCode { get; set; }
        public virtual ICollection<OrderPromo> OrderPromo { get; set; }
        public virtual ICollection<OrderVendor> OrderVendor { get; set; }
        public virtual ICollection<TransactionCard> TransactionCard { get; set; }
        public virtual ICollection<TabCharge> TabCharge { get; set; }
        public virtual ICollection<TabChargeExLog> TabChargeExLog { get; set; }
        #endregion
    }
}
