using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.ViewModel.Setting
{
    public class CityViewModel
    {
        public int CityID { get; set; }
        [Required(ErrorMessageResourceName = "ArNameRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [StringLength(75, ErrorMessageResourceName = "NameMax75", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [RegularExpression(@"^[\u0621-\u064A\u0660-\u0669\0-9]+$", ErrorMessageResourceName = "ArabicOnly", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string CityNameAR { get; set; }
        [Required(ErrorMessageResourceName = "EnNameRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [StringLength(75, ErrorMessageResourceName = "NameMax75", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [RegularExpression(@"^[a-zA-Z\0-9]+$", ErrorMessageResourceName = "EnglishOnly", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string CityNameEN { get; set; }
        [Display(Name = "Country")]
        [Required(ErrorMessageResourceName = "CountryRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [Range(1, int.MaxValue, ErrorMessageResourceName = "CountryRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public int CountryID { get; set; }
        [Display(Name = "Region")]
        [Required(ErrorMessageResourceName = "RegionRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [Range(1, int.MaxValue, ErrorMessageResourceName = "RegionRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public int RegionID { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public int UserId { get; set; }
        public int? UserUpdate { get; set; }
        public int? UserDelete { get; set; }
        [Display(Name = "Country Name Ar")]
        public string CountryNameAR { get; set; }
        [Display(Name = "Country Name Eng")]
        public string CountryNameEN { get; set; }
        [Display(Name = "Region Name Ar")]
        public string RegionNameAR { get; set; }
        [Display(Name = "Region Name Eng")]
        public string RegionNameEN { get; set; }

        public string Lat { get; set; }

        public string Lng { get; set; }
        public bool IsEnable { get; set; }
    }
}
