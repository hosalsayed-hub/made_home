using Homemade.Model.Driver;
using Homemade.Model.Vendor;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homemade.Model.Setting
{
    [Table("HelpQuestions", Schema = "Setting")]
    public class HelpQuestions : BaseEntity
    {
        public HelpQuestions()
        {
            HelpQuestionsGuid = Guid.NewGuid();
            DriverSupport = new HashSet<DriverSupport>();
            VendorSupport = new HashSet<VendorSupport>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HelpQuestionsID { get; set; }
        public Guid HelpQuestionsGuid { get; set; }
        public string NameAR { get; set; }
        public string NameEN { get; set; }
        public bool IsForOrder { get; set; }
        public int HelpUserType { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<DriverSupport> DriverSupport { get; set; }
        public virtual ICollection<VendorSupport> VendorSupport { get; set; }
    }
}
