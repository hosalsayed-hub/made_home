using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.Model.Setting
{
    [Table("Country", Schema = "Setting")]

    public class Country : BaseEntity
    {
        public Country()
        {
            CountryGuid = Guid.NewGuid();
            Region = new HashSet<Region>();

        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CountryID { get; set; }
        public Guid CountryGuid { get; set; }

        [StringLength(75)]
        public string NameAR { get; set; }

        [StringLength(75)]
        public string NameEN { get; set; }
        public string Flag { get; set; }
        public string Extension { get; set; }
        #region Map Data
        public string Place { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
        public string Zoom { get; set; }
        #endregion
        public virtual ICollection<Region> Region { get; set; }
        public virtual User User { get; set; }

    }
}
