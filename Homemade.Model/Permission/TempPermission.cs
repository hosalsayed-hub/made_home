 using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks.Sources;

namespace Homemade.Model
{
    [Table("TempPermission", Schema = "Permission")]
    public partial class TempPermission
    {

        public TempPermission()
        {
            TempPermissionGuid = Guid.NewGuid();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int TempPermissionID { get; set; }

        public Guid TempPermissionGuid { get; set; }

        public int RoleId { get; set; }

        public int PermissionControllerActionID { get; set; }

        public virtual PermissionControllerAction PermissionControllerActions { get; set; }

        public virtual CustomRole Role { get; set; }
    }
}
