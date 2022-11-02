using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Homemade.Model;

namespace Homemade.BLL.ViewModel.Setting
{
    public class JobsViewModel : BaseEntity
    {
        public int JobsID { get; set; }
        public Guid JobsGuid { get; set; }

        [Required(ErrorMessageResourceName = "ArNameRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [StringLength(75, ErrorMessageResourceName = "NameMax75", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [RegularExpression(@"^[\u0621-\u064A\u0660-\u0669\0-9]+$", ErrorMessageResourceName = "ArabicOnly", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string JobsNameAR { get; set; }
        [Required(ErrorMessageResourceName = "EnNameRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [StringLength(75, ErrorMessageResourceName = "NameMax75", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [RegularExpression(@"^[a-zA-Z\0-9]+$", ErrorMessageResourceName = "EnglishOnly", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string JobsNameEN { get; set; }
        public DateTime CreationDate { get; set; }
        //[Display(Name = "JobType")]
        //[Required(ErrorMessageResourceName = "JobTypeRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        //[Range(1, int.MaxValue, ErrorMessageResourceName = "JobTypeRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        //public int JobTypeId { get; set; }
        [RegularExpression(@"^[\u0621-\u064A\u0660-\u0669\0-9]+$", ErrorMessageResourceName = "ArabicOnly", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string DescriptionAr { get; set; }
        [RegularExpression(@"^[a-zA-Z\0-9]+$", ErrorMessageResourceName = "EnglishOnly", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]

        public string DescriptionEn { get; set; }
        //[Display(Name = "JobType Name Ar")]
        //public string JobTypeNameAR { get; set; }
        //[Display(Name = "JobType Name Eng")]
        //public string JobTypeNameEN { get; set; }
    }
}
