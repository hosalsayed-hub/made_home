using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homemade.Model.Setting
{
    [Table("Branches", Schema = "Setting")]
    public class Branches : BaseEntity
    {
        public Branches()
        {
            BranchesGuid = Guid.NewGuid();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BranchesID { get; set; }
        public Guid BranchesGuid { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Mail { get; set; }
        public string Mobile { get; set; }
        public double? Lat { get; set; }
        public double? Lng { get; set; }
        public int CityID { get; set; }
        public virtual User User { get; set; }
        public virtual City City { get; set; }
    }
}
