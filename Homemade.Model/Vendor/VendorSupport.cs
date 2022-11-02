using Homemade.Model.Order;
using Homemade.Model.Setting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.Model.Vendor
{
    [Table("VendorSupport", Schema = "Vendor")]
    public class VendorSupport : BaseEntity
    {
        public VendorSupport()
        {
            VendorSupportGuid = Guid.NewGuid();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VendorSupportID { get; set; }
        public Guid VendorSupportGuid { get; set; }
        public int HelpQuestionsID { get; set; }
        public int VendorsID { get; set; }
        public int? OrderVendorID { get; set; }
        public string Descripe { get; set; }
        public string Image { get; set; }

        public virtual User User { get; set; }
        public virtual HelpQuestions HelpQuestions { get; set; }
        public virtual Vendors Vendors { get; set; }
        public virtual OrderVendor OrderVendor { get; set; }
    }
}
