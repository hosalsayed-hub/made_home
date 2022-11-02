using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homemade.Model.Driver
{
    [Table("DeliverySetting", Schema = "Driver")]
    public class DeliverySetting : BaseEntity
    {
        public DeliverySetting()
        {
            DeliverySettingGuid = Guid.NewGuid();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DeliverySettingID { get; set; }
        public Guid DeliverySettingGuid { get; set; }
        [Display(Name = "Tax")]
        [Required(ErrorMessage = "Tax Is Required")]
        public decimal Tax { get; set; }
        [Required(ErrorMessage = "Driver Commision Is Required")]
        [Display(Name = "Driver Commision")]
        public decimal DriverCommision { get; set; } // percent
        public decimal BaseFare { get; set; }
        public decimal MinKM { get; set; }
        public decimal OverKmFare { get; set; }
        public virtual User User { get; set; }
    }
}
