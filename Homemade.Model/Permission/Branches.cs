using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.Model
{
    //[Table("Branches", Schema = "Setting")]

    //public partial class Branches : BaseEntity
    //{
    //    public Branches()
    //    {
    //        BranchesGuid = Guid.NewGuid();
    //        CustomRole = new HashSet<CustomRole>();
            
    //    }
    //    [Key]
    //    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //    public int BranchesID { get; set; }
        
    //    public int ClientID { get; set; } // PK
    //    public Guid BranchesGuid { get; set; }
    //    [Required(ErrorMessage = "Required")]
    //    [StringLength(75, ErrorMessage = "The maximum allowed is 75 characters ")]
    //    [Display(Name = "Name Ar")]
    //    public string NameAR { get; set; }
    //    [Required(ErrorMessage = "Required")]
    //    [StringLength(75, ErrorMessage = "The maximum allowed is 75 characters ")]
    //    [Display(Name = " Name En")]
    //    public string NameEn { get; set; }
    //    public string Address { get; set; }
    //    #region Map
    //    [DefaultValue(0)]
    //    public double Lat { get; set; }
    //    [DefaultValue(0)]

    //    public double Lng { get; set; }
    //    [DefaultValue(0)]

    //    public double Zoom { get; set; }
    //    #endregion
    //    public bool IsMain { get; set; }
    //    public string Fax { get; set; }
    //    public string MobileNo { get; set; }
    //    public string Email { get; set; }
    //    public string WebSite { get; set; }
    //    public string BranchesManger { get; set; }
    //    public virtual User User { get; set; }
    //    public virtual ICollection<CustomRole> CustomRole { get; set; }
        
    //}
}
