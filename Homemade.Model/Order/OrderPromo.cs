using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.Model.Order
{
    [Table("OrderPromo", Schema = "Order")]
    public class OrderPromo :BaseEntity
    {
        public OrderPromo()
        {
            OrderPromoGuid = Guid.NewGuid();
        }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderPromoID { get; set; }
        public Guid OrderPromoGuid { get; set; }
        public int PromoCodeID { get; set; }
        public int OrdersID { get; set; }
        public double DiscountValue { get; set; }
        public int DiscountType { get; set; }
        public virtual User User { get; set; }
        public virtual PromoCode PromoCode { get; set; }
        public virtual Orders Orders { get; set; }
    }
}

