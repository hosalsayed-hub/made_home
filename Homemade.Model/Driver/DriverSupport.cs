using Homemade.Model.Order;
using Homemade.Model.Setting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.Model.Driver
{
    [Table("DriverSupport", Schema = "Driver")]
    public class DriverSupport : BaseEntity
    {
        public DriverSupport()
        {
            DriverSupportGuid = Guid.NewGuid();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DriverSupportID { get; set; }
        public Guid DriverSupportGuid { get; set; }
        public int HelpQuestionsID { get; set; }
        public int DriversID { get; set; }
        public int? OrderVendorID { get; set; }
        public string Descripe { get; set; }
        public string Image { get; set; }

        public virtual User User { get; set; }
        public virtual HelpQuestions HelpQuestions { get; set; }
        public virtual Drivers Drivers { get; set; }
        public virtual OrderVendor OrderVendor { get; set; }
    }
}
