using Homemade.Model.Customer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.Model.Site
{
    [Table("CartMaster", Schema = "Site")]
    public class CartMaster 
    {
        public CartMaster()
        {
            CartMasterGUID = Guid.NewGuid();
            CreateDate = DateTime.UtcNow.AddHours(3);
            ExpiryDate = DateTime.UtcNow.AddHours(3).AddDays(7);
            CartDetails = new HashSet<CartDetails>();

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartMasterID { get; set; }
        public Guid CartMasterGUID { get; set; }

        public string SessionID { get; set; }
        public string CartMasterStatus { get; set; } // CartMasterStatusEnum

        public bool IsDeleted { get; set; }
        public bool IsAnOrder { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        public int AddressID { get; set; } 
        public string Promocode { get; set; } 
        public decimal? PromocodeDiscount { get; set; } 
        public string tax { get; set; } 
        public string vat { get; set; } 

        [ForeignKey("Customers")]
        public int? CustomersID { get; set; }


        public int OrderTypeId { get; set; } = 0; // OrderTypeEnum
        public DateTime? ScheduleDate { get; set; }  
        public DateTime? ScheduleTime { get; set; }  


        public virtual Customers Customers { get; set; }
        public virtual ICollection<CartDetails> CartDetails { get; set; }
    }

}
