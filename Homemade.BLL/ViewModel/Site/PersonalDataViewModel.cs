using Homemade.BLL.Resources;
using System.ComponentModel.DataAnnotations;

namespace Homemade.BLL.ViewModel.Site
{
    public class PersonalDataViewModel
    {
        public int CustomersID { get; set; }
        [Required(ErrorMessageResourceName = "NameRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string Name { get; set; }
        public string LastName { get; set; }
        [Required(ErrorMessageResourceName = "EmailRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [StringLength(75, ErrorMessageResourceName = "NameMax75", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [EmailAddress(ErrorMessageResourceName = "Email", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string Email { get; set; }
        [StringLength(20, MinimumLength = 9, ErrorMessageResourceName = nameof(HomemadeErrorMessages.NameMax20), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        [RegularExpression(@"^(5)[0-9]{8}$", ErrorMessageResourceName = nameof(HomemadeErrorMessages.MobileValid), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        public string MobileNo { get; set; }
        [Required(ErrorMessageResourceName = "GenderRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public int GenderID { get; set; }
        [Required(ErrorMessageResourceName = "CityRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public int CityID { get; set; }
        [Required(ErrorMessageResourceName = "NationalityRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public int NationalityID { get; set; }
    }
    public class PagingViewModel
    {
        public int ItemCount { get; set; }
        public int Pagecount { get; set; }
        public int CurrentPage { get; set; }
        public int NumberOfItems { get; set; }
    }
}
