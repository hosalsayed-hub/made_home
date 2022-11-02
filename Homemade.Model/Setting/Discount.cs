using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.Model.Setting
{
    [Table("Discount", Schema = "Setting")]
    public class Discount : BaseEntity
    {
        public Discount()
        {
            DiscountGuid = Guid.NewGuid();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DiscountID { get; set; }
        public Guid DiscountGuid { get; set; }
        public int DiscountTypeID { get; set; }  // DiscountSettingEnum
        public decimal DiscountValue { get; set; }
        public virtual User User { get; set; }
    }
}
