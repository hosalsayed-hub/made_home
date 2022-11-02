using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Homemade.Model.Emp;

namespace Homemade.Model.Setting
{
    [Table("Jobs", Schema = "Setting")]
    public class Jobs : BaseEntity
    {
        public Jobs()
        {
            JobsGuid = Guid.NewGuid();
            Employees = new HashSet<Employees>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int JobsID { get; set; }
        public Guid JobsGuid { get; set; }
        [StringLength(75)]
        public string NameAR { get; set; }
        [StringLength(75)]
        public string NameEN { get; set; }
        public string Image { get; set; }
        public string DescriptionAr { get; set; }
        public string DescriptionEn { get; set; }
        public int JobTypeId { get; set; }  // JobTypeEnum
        public virtual User User { get; set; }
        public virtual ICollection<Employees> Employees { get; set; }
    }
}
