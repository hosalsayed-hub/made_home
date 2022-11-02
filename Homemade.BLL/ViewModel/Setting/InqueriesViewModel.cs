using Homemade.BLL.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.ViewModel.Setting
{
   public  class InqueriesViewModel
    {
        public int InqueriesID { get; set; }
        [Required(ErrorMessageResourceName = "SliderTypeRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string Name { get; set; }
        [Required(ErrorMessageResourceName = "EmailRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [StringLength(75, ErrorMessageResourceName = "NameMax75", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [EmailAddress(ErrorMessageResourceName = "Email", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string Email { get; set; }
        [Required(ErrorMessageResourceName = "SliderTypeRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string Message { get; set; }
        [StringLength(20, MinimumLength = 9, ErrorMessageResourceName = nameof(HomemadeErrorMessages.NameMax20), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        [RegularExpression(@"^(5)[0-9]{8}$", ErrorMessageResourceName = nameof(HomemadeErrorMessages.MobileValid), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        public string MobileNo { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
