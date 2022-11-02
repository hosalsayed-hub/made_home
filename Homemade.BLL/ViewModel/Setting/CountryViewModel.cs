using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.ViewModel.Setting
{
    public class CountryViewModel
    {
        public int CountryID { get; set; }
        public Guid CountryGuid { get; set; }

        [Required(ErrorMessageResourceName = "ArNameRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [StringLength(75, ErrorMessageResourceName = "NameMax75", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [RegularExpression(@"^[\u0621-\u064A\u0660-\u0669\0-9]+$", ErrorMessageResourceName = "ArabicOnly", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string CountryNameAR { get; set; }
        [Required(ErrorMessageResourceName = "EnNameRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [StringLength(75, ErrorMessageResourceName = "NameMax75", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [RegularExpression(@"^[a-zA-Z\0-9]+$", ErrorMessageResourceName = "EnglishOnly", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string CountryNameEN { get; set; }
        public string Code { get; set; }
        public DateTime CreationDate { get; set; }
        public int UserId { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UserUpdate { get; set; }
        public string Flag { get; set; }
    }
    public class CountryViewModelApi
    {
        public string CountryName { get; set; }
        public int CountryID { get; set; }
        public string Flag { get; set; }
        public string Code { get; set; }
        public int length { get; set; }
    }
    public class KeyWordViewModelApi
    {
        public int KeyWordsID { get;  set; }
        public string Name { get; set; }
    }
}
