using Homemade.BLL.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace Homemade.BLL.ViewModel.User
{
    public class LoginModel
    {
        /// <summary>
        /// Is The Phone Number Of The User Should Check In Table Oprtaion
        /// </summary>
        [Required(ErrorMessageResourceName = nameof(HomemadeErrorMessages.Required) , ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        public string Mobile { get; set; }

        [Required(ErrorMessageResourceName = nameof(HomemadeErrorMessages.Required) , ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        public string Password { get; set; }

    }
    public class CompanyLoginModel
    {
        /// <summary>
        /// Is The Phone Number Of The User Should Check In Table Oprtaion
        /// </summary>
        [Required(ErrorMessageResourceName = nameof(HomemadeErrorMessages.Required), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceName = nameof(HomemadeErrorMessages.Required), ErrorMessageResourceType = typeof(HomemadeErrorMessages))]
        public string Password { get; set; }
        public int Type { get; set; }
    }

    public class CompanyAccountViewModel
    {
        public string Code { get; set; }
        public string Email { get; set; }
        public int CompanyID { get; set; }
        public int UserID { get; set; }
        public string Password { get; set; }
    }
    public class AutoVerifyViewModel
    {
       public Guid? Id { get; set; }
        public int? Type { get; set; }
    }
}
