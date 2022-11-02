using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Homemade.Model.Order;

namespace Homemade.Model.Setting
{
    [Table("OrderStatus", Schema = "Setting")]
    public class OrderStatus : BaseEntity
    {
        public OrderStatus()
        {
            OrderStatusGuid = Guid.NewGuid();
            Orders = new HashSet<Orders>();
            OrderVendor = new HashSet<OrderVendor>();
            OrderHistory = new HashSet<OrderHistory>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderStatusID { get; set; }
        public Guid OrderStatusGuid { get; set; }
        [StringLength(75)]
        public string NameAR { get; set; }
        [StringLength(75)]
        public string NameEN { get; set; }
        [StringLength(75)]
        public string DescAr { get; set; }
        [StringLength(75)]
        public string DescEN { get; set; }

        public int? Arrange { get; set; }
        public int? OrderStatusType { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
        public virtual ICollection<OrderVendor> OrderVendor { get; set; }
        public virtual ICollection<OrderHistory> OrderHistory { get; set; }
    }
}
