using Homemade.BLL.Resources;
using Homemade.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.ViewModel.Customer
{
    public class CustomerViewModel : BaseEntity
    {
        public int CustomersID { get; set; }
        public Guid CustomersGuid { get; set; }
        [Required(ErrorMessageResourceName = "FirstNameRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [StringLength(75, ErrorMessageResourceName = "NameMax75", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string FirstName { get; set;}
        [Required(ErrorMessageResourceName = "SecondNameRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [StringLength(75, ErrorMessageResourceName = "NameMax75", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string SeconedName { get; set; }
        [StringLength(20, MinimumLength = 9, ErrorMessageResourceName = nameof(HomemadeErrorMessages.NameMax20), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        [RegularExpression(@"^(5)[0-9]{8}$", ErrorMessageResourceName = nameof(HomemadeErrorMessages.MobileValid), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        public string MobileNo { get; set; }
        [Required(ErrorMessageResourceName = "EmailRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [StringLength(75, ErrorMessageResourceName = "NameMax75", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [EmailAddress(ErrorMessageResourceName = "Email", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string Email { get; set; }
        public string Address { get; set; }
        public string ProfilePic { get; set; }
        [Required(ErrorMessageResourceName = "GenderRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public int Gender { get; set; } //GenderEnum
        [Required(ErrorMessageResourceName = "CityRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public int CityID { get; set; }
        [Required(ErrorMessageResourceName = "NationalityRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public int NationalityID { get; set; }
        public string CityName { get; set; }
        public string NationalityName { get; set; }
        public string IsEnableString { get; set; }
        public string CreateDateString { get; set; }
        public int DeliveredCount { get; set; }
        public decimal Balance { get; set; }
        public List<UpdateCustomerLocation> CustomerLocations { set; get; }
        public List<CustomerFavourritesViewModel> CustomerFavourites { set; get; }
    }
}
