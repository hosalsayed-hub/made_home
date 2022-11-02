using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Homemade.Model.Customer;
using Homemade.Model.Driver;
using Homemade.Model.Emp;
using Homemade.Model.Vendor;

namespace Homemade.Model.Setting
{
    [Table("Nationality", Schema = "Setting")]
    public class Nationality : BaseEntity
    {
        public Nationality()
        {
            NationalityGuid = Guid.NewGuid();
            Employees = new HashSet<Employees>();
            Customers = new HashSet<Customers>();
            Vendors = new HashSet<Vendors>();
            Drivers = new HashSet<Drivers>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int NationalityID { get; set; }
        public Guid NationalityGuid { get; set; }
        [StringLength(75)]
        public string NameAR { get; set; }
        [StringLength(75)]
        public string NameEN { get; set; }
        public string DescriptionAr { get; set; }
        public string DescriptionEn { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<Drivers> Drivers { get; set; }
        public virtual ICollection<Employees> Employees { get; set; }
        public virtual ICollection<Customers> Customers { get; set; }
        public virtual ICollection<Vendors> Vendors { get; set; }
    }
}
