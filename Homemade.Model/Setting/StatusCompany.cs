using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace Homemade.Model.Setting
{
    [Table("StatusCompany", Schema = "Setting")]
    public class StatusCompany : BaseEntity
    {
        public StatusCompany()
        {
            StatusCompanyGuid = Guid.NewGuid();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StatusCompanyID { get; set; }
        public Guid StatusCompanyGuid { get; set; }
        public int ShippingCompanyID { get; set; }
        [StringLength(75)]
        public string NameAR { get; set; }
        [StringLength(75)]
        public string NameEN { get; set; }
        public int StatusCoId { get; set; }
        public int StatusHomeMadeId { get; set; }
        public virtual User User { get; set; }
        public virtual ShippingCompany ShippingCompany { get; set; }
    }
}
