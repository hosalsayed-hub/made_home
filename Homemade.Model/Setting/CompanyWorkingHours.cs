using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;
using Homemade.Model.Customer;
using Homemade.Model.Vendor;
using Homemade.Model.Driver;

namespace Homemade.Model.Setting
{
    [Table("CompanyWorkingHours", Schema = "Setting")]
    public class CompanyWorkingHours : BaseEntity
    {
        public CompanyWorkingHours()
        {
            CompanyWorkingHoursGuid = Guid.NewGuid();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CompanyWorkingHoursID { get; set; }
        public Guid CompanyWorkingHoursGuid { get; set; }
        public string DaysWork { get; set; }
        public DateTime FirstShiftWorkFrom { get; set; }
        public DateTime FirstShiftWorkTo { get; set; }
        public DateTime? SecondShiftWorkFrom { get; set; }
        public DateTime? SecondShiftWorkTo { get; set; }
        public string DaysVac { get; set; }
        public DateTime? FirstShiftVacWorkFrom { get; set; }
        public DateTime? FirstShiftVacWorkTo { get; set; }
        public DateTime? SecondShiftVacWorkFrom { get; set; }
        public DateTime? SecondShiftVacWorkTo { get; set; }
        public virtual User User { get; set; }

    }
}
