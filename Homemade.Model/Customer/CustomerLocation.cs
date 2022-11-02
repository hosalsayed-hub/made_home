using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Homemade.Model.Setting;
using Homemade.Model.Order;

namespace Homemade.Model.Customer
{
    [Table("CustomerLocation", Schema = "Customer")]
    public class CustomerLocation : BaseEntity
    {
        public CustomerLocation()
        {
            CustomerLocationGuid = Guid.NewGuid();
            Orders = new HashSet<Orders>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int CustomerLocationID { get; set; }
        public Guid CustomerLocationGuid { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string Address { get; set; }
        public string BuildingNumber { get; set; }
        public string StreetNo { get; set; }
        public string UniqueSign { get; set; } 
        public double Lat { get; set; }
        public double Lng { get; set; }
        public bool IsVerfiy { get; set; }
        public bool IsLocation { get; set; }

        #region Relations
        public int CustomersID { get; set; } //fk
        public int BlockID { get; set; } //fk
        public int AddressTypesID { get; set; }


        public virtual Block Block { get; set; }
        public virtual AddressTypes AddressTypes { get; set; }
        public virtual Customers Customers { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
        #endregion
    }
}
