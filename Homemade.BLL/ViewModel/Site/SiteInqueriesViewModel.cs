using Homemade.BLL.Resources;
using System.ComponentModel.DataAnnotations;

namespace Homemade.BLL.ViewModel.Site
{
    public class SiteInqueriesViewModel
    {
        [Required(ErrorMessageResourceName = "NameRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string Name { get; set; }
        [StringLength(75, ErrorMessageResourceName = "NameMax75", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [EmailAddress(ErrorMessageResourceName = "Email", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string Email { get; set; }
        [Required(ErrorMessageResourceName = "MessageRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string Message { get; set; }
        [StringLength(20, MinimumLength = 9, ErrorMessageResourceName = nameof(HomemadeErrorMessages.NameMax20), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        [RegularExpression(@"^(5)[0-9]{8}$", ErrorMessageResourceName = nameof(HomemadeErrorMessages.MobileValid), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        public string MobileNo { get; set; }
    }
}
