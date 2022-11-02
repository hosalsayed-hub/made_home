using Homemade.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.ViewModel.Setting
{
    public class PackageViewModel : BaseEntity
    {
        public int PackageID { get; set; }
        public Guid PackageGuid { get; set; }

        [Required(ErrorMessageResourceName = "ArNameRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [StringLength(75, ErrorMessageResourceName = "NameMax75", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [RegularExpression(@"^[\u0621-\u064A\u0660-\u0669\0-9]+$", ErrorMessageResourceName = "ArabicOnly", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string PackageNameAR { get; set; }
        [Required(ErrorMessageResourceName = "EnNameRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [StringLength(75, ErrorMessageResourceName = "NameMax75", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [RegularExpression(@"^[a-zA-Z\0-9]+$", ErrorMessageResourceName = "EnglishOnly", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string PackageNameEN { get; set; }
        [RegularExpression(@"^[\u0621-\u064A\u0660-\u0669\0-9]+$", ErrorMessageResourceName = "ArabicOnly", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string DescriptionAr { get; set; }
        [RegularExpression(@"^[a-zA-Z\0-9]+$", ErrorMessageResourceName = "EnglishOnly", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]

        public string DescriptionEn { get; set; }
        public string PackageName { get; set; }
        [Required(ErrorMessageResourceName = "PercentRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string Percent { get; set; }
        [Required(ErrorMessageResourceName = "PriceRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string Price { get; set; }
        public int? PackageType { get;  set; }
    }
}
