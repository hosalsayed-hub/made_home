using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.Model.Setting
{
    [Table("PaymentWay", Schema = "Setting")]
    public class PaymentWay : BaseEntity
    {
        public PaymentWay()
        {
            PaymentWayGuid = Guid.NewGuid();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PaymentWayID { get; set; }
        public Guid PaymentWayGuid { get; set; }
        [StringLength(75)]
        public string NameAR { get; set; }
        [StringLength(75)]
        public string NameEN { get; set; }
        public virtual User User { get; set; }
    }
}
