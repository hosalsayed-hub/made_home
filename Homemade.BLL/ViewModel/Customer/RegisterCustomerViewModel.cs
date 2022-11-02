using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Homemade.BLL.Resources;
using Homemade.Model;

namespace Homemade.BLL.ViewModel.Customer
{
    public class RegisterCustomerViewModel
    {
        public string mobileNo { get; set; }
        public string firstName { get; set; }
        public string seconedName { get; set; }
        public string email { get; set; }
        public int cityID { get; set; }
        public int nationaltyID { get; set; }
        public int gender { get; set; }
        public string password { get; set; }
    }
    public class UpdateCustomerViewModel
    {
        public string mobileNo { get; set; }
        public string firstName { get; set; }
        public string seconedName { get; set; }
        public string email { get; set; }
        public int cityID { get; set; }
        public int nationaltyID { get; set; }
        public int gender { get; set; }
    }
}
