using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Homemade.Model.Emp;
using Homemade.Model.Vendor;

namespace Homemade.Model.Setting
{
    [Table("Departments", Schema = "Setting")]
    public class Departments : BaseEntity
    {
        public Departments()
        {
            DepartmentsGuid = Guid.NewGuid();
            KeyWords = new HashSet<KeyWords>();
            Product = new HashSet<Product>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DepartmentsID { get; set; }
        public Guid DepartmentsGuid { get; set; }
        [StringLength(75)]
        public string NameAR { get; set; }
        [StringLength(75)]
        public string NameEN { get; set; }
        public string DescriptionAr { get; set; }
        public string DescriptionEn { get; set; }
        public string Image { get; set; }
        public string SiteImage { get; set; }
        public bool Isunique { get; set; }
        public int MainCategoryID { get; set; }
        public int? Arrange { get; set; }
        public virtual MainCategory MainCategory { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<KeyWords> KeyWords { get; set; }
        public virtual ICollection<Product> Product { get; set; }
        
    }
}
