using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.Model.Order
{
    [Table("PromoCode", Schema = "Order")]
    public class PromoCode : BaseEntity
    {
        public PromoCode()
        {
            PromoCodeGuid = Guid.NewGuid();
            OrderPromo = new HashSet<OrderPromo>();
            Orders = new HashSet<Orders>();
            VendorPromo = new HashSet<VendorPromo>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PromoCodeID { get; set; }
        public Guid PromoCodeGuid { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public double LimitCount { get; set; }
        public string Code { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public double DiscountValue { get; set; }
        public int DiscountType { get; set; }
        public int PromoType { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<OrderPromo> OrderPromo { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
        public virtual ICollection<VendorPromo> VendorPromo { get; set; }
        
    }
}
