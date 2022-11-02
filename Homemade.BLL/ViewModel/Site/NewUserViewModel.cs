using Homemade.BLL.Resources;
using System.ComponentModel.DataAnnotations;

namespace Homemade.BLL.ViewModel.Site
{
    public class NewUserViewModel
    {
        [Required(ErrorMessageResourceName = "MobileRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [StringLength(20, MinimumLength = 9, ErrorMessageResourceName = nameof(HomemadeErrorMessages.NameMax20), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        [RegularExpression(@"^(5)[0-9]{8}$", ErrorMessageResourceName = nameof(HomemadeErrorMessages.MobileValid), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        public string MobileNo { get; set; }
        [Required(ErrorMessageResourceName = "UserNameRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string UserName { get; set; }
        [StringLength(75, ErrorMessageResourceName = "NameMax75", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [EmailAddress(ErrorMessageResourceName = "Email", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string Email { get; set; }
        [Required(ErrorMessageResourceName = "AddressRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string Address { get; set; }
        [Required(ErrorMessageResourceName = "CityRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public int CityID { get; set; }
        [Required(ErrorMessageResourceName = "NationalityRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public int NationalityID { get; set; }
        [Required(ErrorMessageResourceName = "GenderRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public int Gender { get; set; }
        [Required(ErrorMessageResourceName = "PasswordRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [StringLength(255, MinimumLength = 5, ErrorMessageResourceName = nameof(HomemadeErrorMessages.PasswordValidNumber), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessageResourceName = "PasswordRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [StringLength(255, MinimumLength = 5, ErrorMessageResourceName = nameof(HomemadeErrorMessages.PasswordValidNumber), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessageResourceName = "IsAgreeTermsRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public bool IsAgreeTerms { get; set; }
        public int RegisterTypeId { get; set; }
    }
    public class SiteVerfiedCodeViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Code { get; set; }
    }
}
