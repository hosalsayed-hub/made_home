using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.ViewModel.Setting
{
     public   class SliderViewModel
    {
        public int SliderID { get; set; }
        //[Required(ErrorMessageResourceName = "ArNameRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        //[StringLength(75, ErrorMessageResourceName = "NameMax75", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [RegularExpression(@"^[\u0621-\u064A\u0660-\u0669\0-9]+$", ErrorMessageResourceName = "ArabicOnly", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string SliderNameAR { get; set; }
        //[Required(ErrorMessageResourceName = "EnNameRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        //[StringLength(75, ErrorMessageResourceName = "NameMax75", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [RegularExpression(@"^[a-zA-Z\0-9]+$", ErrorMessageResourceName = "EnglishOnly", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string SliderNameEN { get; set; }

        //[StringLength(75, ErrorMessageResourceName = "NameMax75", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [RegularExpression(@"^[\u0621-\u064A\u0660-\u0669\0-9]+$", ErrorMessageResourceName = "ArabicOnly", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string DescAr { get; set; }
       
        //[StringLength(75, ErrorMessageResourceName = "NameMax75", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [RegularExpression(@"^[a-zA-Z\0-9]+$", ErrorMessageResourceName = "EnglishOnly", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string DescEN { get; set; }
        public string Image { get; set; }
        [Display(Name = "SliderType")]
        [Required(ErrorMessageResourceName = "SliderTypeRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
       
        public int SliderTypeId { get; set; } //SliderTypeEnum
        public bool IsDeleted { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public int UserId { get; set; }
        public int? UserUpdate { get; set; }
        public bool IsEnable { get; set; }
        public int DisplayIn { set; get; }
        public string SliderName { get;  set; }
        public string Desc { get;  set; }
    }
}
