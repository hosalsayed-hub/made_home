using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homemade.Api.Models
{
    public class LoginModel
    {
        public string userName { get; set; }
        public string password { get; set; }
        public string token { get; set; }
        public int deviceType { get; set; }
    }
    public class ResetPasswordViewModel
    {
        public string mobileNo { get; set; }
        public string password { get; set; }
    }
}
