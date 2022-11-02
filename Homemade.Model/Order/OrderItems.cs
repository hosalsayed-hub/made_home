using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Homemade.Model.Vendor;

namespace Homemade.Model.Order
{
    [Table("OrderItems", Schema = "Order")]
    public class OrderItems : BaseEntity
    {
        public OrderItems()
        {
            OrderItemsGuid = Guid.NewGuid();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderItemsID { get; set; }
        public Guid OrderItemsGuid { get; set; }
        public string ProdNameAr { get; set; }
        public string ProdNameEn { get; set; }
        public decimal VatValue { get; set; }
        public decimal PriceBeforeVat { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal Quantity { get; set; }
        public string ProdImage { get; set; }
        public bool IsIncreaseItem { get; set; }

        #region Relations
        public int OrderVendorID { get; set; }
        public int ProductID { get; set; }
        public virtual OrderVendor OrderVendor { get; set; }
        public virtual User User { get; set; }
        public virtual Product Product { get; set; }
        #endregion
    }
}
