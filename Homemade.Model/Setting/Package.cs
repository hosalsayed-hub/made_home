using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using Homemade.Model.Vendor;
using Homemade.Model.Order;

namespace Homemade.Model.Setting
{
    [Table("Package", Schema = "Setting")]
    public class Package : BaseEntity
    {
        public Package()
        {
            PackageGuid = Guid.NewGuid();
            Vendors = new HashSet<Vendors>();
            OrderVendor = new HashSet<OrderVendor>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PackageID { get; set; }
        public Guid PackageGuid { get; set; }
        [StringLength(75)]
        public string NameAR { get; set; }
        [StringLength(75)]
        public string NameEN { get; set; }
        public string DescEn { get; set; }
        public string DescAr { get; set; }
        public decimal Percent { get; set; }
        public decimal Price { get; set; }
        public int? PackageType { get; set; } // PackageTypeEnum

        public virtual User User { get; set; }
        public virtual ICollection<Vendors> Vendors { get; set; }
        public virtual ICollection<OrderVendor> OrderVendor { get; set; }
    }
}
