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
    [Table("Region", Schema = "Setting")]
    public class Region : BaseEntity
    {
        public Region()
        {
            RegionGuid = Guid.NewGuid();
            City = new HashSet<City>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RegionID { get; set; }
        public Guid RegionGuid { get; set; }
        [ForeignKey(nameof(Country))]
        public int CountryID { get; set; }

        [StringLength(75)]
        public string NameAR { get; set; }

        [StringLength(75)]
        public string NameEN { get; set; }
        #region Map Data
        public string Place { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
        public string Zoom { get; set; }
        #endregion
        public virtual ICollection<City> City { get; set; }
        public virtual User User { get; set; }
        public virtual Country Country { get; set; }

    }
}
