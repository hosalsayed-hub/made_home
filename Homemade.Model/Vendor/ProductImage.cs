using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homemade.Model.Vendor
{
    [Table("ProductImage", Schema = "Vendor")]
    public class ProductImage : BaseEntity
    {
        public ProductImage()
        {
            ProductImageGuid = Guid.NewGuid();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductImageID { get; set; }
        public Guid ProductImageGuid { get; set; }
        public string Image { get; set; }
        public string DescAr { get; set; }
        public string DescEn { get; set; }
        #region FK
        public int ProductID { get; set; }
        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
        #endregion
    }
}
