using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Homemade.Model;

namespace Homemade.BLL.ViewModel.Setting
{
    public class KeyWordsViewModel : BaseEntity
    {
        public int KeyWordsID { get; set; }
        public Guid KeyWordsGuid { get; set; }

        [Required(ErrorMessageResourceName = "ArNameRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [StringLength(75, ErrorMessageResourceName = "NameMax75", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [RegularExpression(@"^[\u0621-\u064A\u0660-\u0669\0-9]+$", ErrorMessageResourceName = "ArabicOnly", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string KeyWordsNameAR { get; set; }
        [Required(ErrorMessageResourceName = "EnNameRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [StringLength(75, ErrorMessageResourceName = "NameMax75", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [RegularExpression(@"^[a-zA-Z\0-9]+$", ErrorMessageResourceName = "EnglishOnly", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string KeyWordsNameEN { get; set; }

        [Required(ErrorMessageResourceName = "CodeRequired", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [StringLength(75, ErrorMessageResourceName = "NameMax75", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]

        public string KeyWordsName { get; set; }

        public string IsEnableString { get; set; }

        [Required(ErrorMessageResourceName = "MainCategoryRequired", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public int DepartmentID { get; set; }
        public string DepartmentName { set; get; }
    }
}
