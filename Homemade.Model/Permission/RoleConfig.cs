using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.Model
{
    [Table("RoleConfig", Schema = "Permission")]
   public partial class RoleConfig
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int RoleConfigID { get; set; }

        public int RoleId { get; set; }

        public int PermissionControllerActionID { get; set; }

        public virtual PermissionControllerAction PermissionControllerActions { get; set; }

        public virtual CustomRole Role { get; set; }
    }
}
