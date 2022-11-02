using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homemade.Model.Vendor
{
    [Table("VacHistory", Schema = "Vendor")]
    public class VacHistory : BaseEntity
    {
        public VacHistory()
        {
            VacHistoryGuid = Guid.NewGuid();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VacHistoryID { get; set; }
        public Guid VacHistoryGuid { get; set; }
        public DateTime VacFrom { get; set; }
        public DateTime VacTo { get; set; }
        public bool IsReturn { get; set; }
        public int VendorsID { get; set; }
        public virtual User User { get; set; }
        public virtual Vendors Vendors { get; set; }
    }
}
