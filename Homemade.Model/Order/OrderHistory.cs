using Homemade.Model.Setting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.Model.Order
{
    [Table("OrderHistory", Schema = "Order")]
    public class OrderHistory : BaseEntity
    {
        public OrderHistory()
        {
            OrderHistoryGuid = Guid.NewGuid();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderHistoryID { get; set; }
        public Guid OrderHistoryGuid { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public string CancelReason { get; set; }
        #region Relations
        public int OrderVendorID { get; set; }
        public int OrderStatusID { get; set; }
        public virtual OrderVendor OrderVendor { get; set; }
        public virtual User User { get; set; }
        public virtual OrderStatus OrderStatus { get; set; }
        #endregion
    }
}
