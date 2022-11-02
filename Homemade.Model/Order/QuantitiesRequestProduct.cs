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
    [Table("QuantitiesRequestProduct", Schema = "Order")]
    public class QuantitiesRequestProduct : BaseEntity
    {
        public QuantitiesRequestProduct()
        {
            QuantitiesRequestProductGuid = Guid.NewGuid();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuantitiesRequestProductID { get; set; }
        public Guid QuantitiesRequestProductGuid { get; set; }
        public int QuantitiesRequestID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductImage { get; set; }
        public double Quantity { get; set; }
        public double QuantityAllowed { get; set; }
        public double QuantityInventory { get; set; }
        public virtual QuantitiesRequest QuantitiesRequest { get; set; }
        public virtual User User { get; set; }
        public virtual Product Product { get; set; }
    }
}
