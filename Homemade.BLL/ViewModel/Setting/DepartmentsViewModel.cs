using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Homemade.Model;

namespace Homemade.BLL.ViewModel.Setting
{
    public class DepartmentsViewModel : BaseEntity
    {
        public int DepartmentsID { get; set; }
        public Guid DepartmentsGuid { get; set; }

        [Required(ErrorMessageResourceName = "ArNameRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [StringLength(75, ErrorMessageResourceName = "NameMax75", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [RegularExpression(@"^[\u0621-\u064A\u0660-\u0669\0-9]+$", ErrorMessageResourceName = "ArabicOnly", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string DepartmentsNameAR { get; set; }
        [Required(ErrorMessageResourceName = "EnNameRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [StringLength(75, ErrorMessageResourceName = "NameMax75", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [RegularExpression(@"^[a-zA-Z\0-9]+$", ErrorMessageResourceName = "EnglishOnly", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string DepartmentsNameEN { get; set; }
        [RegularExpression(@"^[\u0621-\u064A\u0660-\u0669\0-9]+$", ErrorMessageResourceName = "ArabicOnly", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string DescriptionAr { get; set; }
        [RegularExpression(@"^[a-zA-Z\0-9]+$", ErrorMessageResourceName = "EnglishOnly", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]

        public string DescriptionEn { get; set; }
        public string DepartmentsName { get; set; }
        public string IsEnableString { get; set; }
        public string Image { get; set; }
        public bool Isunique { get; set; }

        [Required(ErrorMessageResourceName = "MainCategoryRequired", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public int MainCategoryID { get; set; }
        public string MainCatgoryName { set; get; }
        public string SiteImage { get; set; }
        public int? Arrange { get; set; }
    }
}
