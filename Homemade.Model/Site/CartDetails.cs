using Homemade.Model.Vendor;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.Model.Site
{
    public class CartDetails
    {
        public CartDetails()
        {
            CartDetailsGUID = Guid.NewGuid();
            CreateDate = DateTime.UtcNow.AddHours(3);

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartDetailsID { get; set; }
        public Guid CartDetailsGUID { get; set; }


        #region Product
        [ForeignKey("Product")]
        public int ProductID { get; set; }
        public string ProductNameAr { get; set; }
        public string ProductNameEn { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal ProductDiscount { get; set; }
        public decimal ProductQuantity { get; set; }
        public string ProductImage { get; set; }
        #endregion


        public string Note { get; set; }


        public bool IsDeleted { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        [ForeignKey("CartMaster")]
        public int CartMasterID { get; set; }

        public decimal deliveryprice { get; set; }
        public decimal distanceKM { get; set; }

         

        public virtual CartMaster CartMaster { get; set; }
        public virtual Product Product { get; set; }
        
    }
}
