using Homemade.Model.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.Model.Setting
{
    [Table("RegionCity", Schema = "Setting")]
    public class RegionCity : BaseEntity
    {
        public RegionCity()
        {
            RegionCityGuid = Guid.NewGuid();
            Drivers = new HashSet<Drivers>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RegionCityID { get; set; }
        public Guid RegionCityGuid { get; set; }

        [StringLength(75)]
        public string NameAR { get; set; }

        [StringLength(75)]
        public string NameEN { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Drivers> Drivers { get; set; }

    }
}
