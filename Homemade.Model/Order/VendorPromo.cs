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
    [Table("VendorPromo", Schema = "Order")]
    public class VendorPromo :BaseEntity
    {
        public VendorPromo()
        {
            VendorPromoGuid = Guid.NewGuid();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VendorPromoID { get; set; }
        public Guid VendorPromoGuid { get; set; }
        public int VendorsID { get; set; }
        public int PromoCodeID { get; set; }
        public virtual User User { get; set; }
        public virtual PromoCode PromoCode { get; set; }
        public virtual Vendors Vendors { get; set; }

    }
}
