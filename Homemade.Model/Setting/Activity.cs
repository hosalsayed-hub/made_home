using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Homemade.Model.Vendor;

namespace Homemade.Model.Setting
{
    [Table("Activity", Schema = "Setting")]
    public class Activity : BaseEntity
    {
        public Activity()
        {
            ActivityGuid = Guid.NewGuid();
            Vendors = new HashSet<Vendors>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ActivityID { get; set; }
        public Guid ActivityGuid { get; set; }
        [StringLength(75)]
        public string NameAR { get; set; }
        [StringLength(75)]
        public string NameEN { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Vendors> Vendors { get; set; }
    }
}
