using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homemade.Model.Order
{
    [Table("OrderRate", Schema = "Order")]
    public class OrderRate :BaseEntity
    {
        public OrderRate()
        {
            OrderRateGuid = Guid.NewGuid();
            Notification = new HashSet<Notification>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderRateID { get; set; }
        public Guid OrderRateGuid { get; set; }
        public string CommentDelivery { get; set; }
        public string CommentOrder { get; set; }
        public string AnswerRate { get; set; }
        public decimal RateOrder { get; set; }
        public decimal RateDelivery { get; set; }
        public bool IsRepley { get; set; }
        public int OrderVendorID { get; set; }
        public virtual User User { get; set; }
        public virtual OrderVendor OrderVendor { get; set; }
        public virtual ICollection<Notification> Notification { get; set; }
    }
}
