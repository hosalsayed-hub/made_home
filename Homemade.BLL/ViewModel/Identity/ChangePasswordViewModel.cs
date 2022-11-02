using Homemade.BLL.Resources;
using System.ComponentModel.DataAnnotations;

namespace Homemade.BLL.ViewModel.Identity
{
    public class ChangePasswordViewModel
    {

        [Required(ErrorMessageResourceType = typeof(HomemadeErrorMessages) , ErrorMessageResourceName = nameof(HomemadeErrorMessages.Required))]
        public string OldPassword { get; set; }

        [Required(ErrorMessageResourceType = typeof(HomemadeErrorMessages) , ErrorMessageResourceName = nameof(HomemadeErrorMessages.Required))]
        public string NewPassword { get; set; }

        [Required(ErrorMessageResourceType = typeof(HomemadeErrorMessages) , ErrorMessageResourceName = nameof(HomemadeErrorMessages.Required))]
        [Compare(nameof(NewPassword) , ErrorMessageResourceType = typeof(HomemadeErrorMessages) , ErrorMessageResourceName = nameof(HomemadeErrorMessages.NotIdentical))]
        public string ConfirmNewPassword { get; set; }

    }
}
