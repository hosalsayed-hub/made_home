using Homemade.Model.Customer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.Model.Setting
{
   [Table("AddressTypes", Schema = "Setting")]
   public class AddressTypes : BaseEntity
    {
        public AddressTypes()
        {
            AddressTypesGuid = Guid.NewGuid();
            CustomerLocation = new HashSet<CustomerLocation>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AddressTypesID { get; set; }
        public Guid AddressTypesGuid { get; set; }
        public string NameAR { get; set; }
        public string NameEN { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<CustomerLocation> CustomerLocation { get; set; }
    }
}
