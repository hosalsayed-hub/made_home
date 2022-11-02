using System;
using Homemade.Model.Emp;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.CompilerServices;
using Homemade.Model.Vendor;
using Homemade.Model.Customer;
using Homemade.Model.Driver;

namespace Homemade.Model.Setting
{
    [Table("CitiesCovered", Schema = "Setting")]

    public class CitiesCovered : BaseEntity
    {
        public CitiesCovered()
        {
            CitiesCoveredGuid = Guid.NewGuid();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int CitiesCoveredID { get; set; }
        public Guid CitiesCoveredGuid { get; set; }

        [ForeignKey(nameof(City))]
        public int CityID { get; set; }

        public virtual City City { get; set; }
        public virtual User User { get; set; }
        
    }
    
}
