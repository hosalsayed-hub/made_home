using Homemade.Model.Setting;
using Homemade.Model.Vendor;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.Model.Order
{
    [Table("QuantitiesRequest", Schema = "Order")]
    public class QuantitiesRequest : BaseEntity
    {
        public QuantitiesRequest()
          {
            QuantitiesRequestGuid = Guid.NewGuid();
            QuantitiesRequestProduct = new HashSet<QuantitiesRequestProduct>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuantitiesRequestID { get; set; }
        public Guid QuantitiesRequestGuid { get; set; }
        public int VendorsID { get; set; } // fk
        public virtual User User { get; set; }
        public virtual Vendors Vendors { get; set; }
        public virtual ICollection<QuantitiesRequestProduct> QuantitiesRequestProduct { get; set; }
    }
}
