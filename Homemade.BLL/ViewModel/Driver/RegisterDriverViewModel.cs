using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.ViewModel.Driver
{
    public class RegisterDriverViewModel
    {
        public string iDNo { get; set; }
        public string mobileNo { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public int cityID { get; set; }
        public int nationaltyID { get; set; }
        public int gender { get; set; }
        public string password { get; set; }
        public int? regionCityID { get; set; }
    }
    public class UpdateDriverViewModel
    {
        public string iDNo { get; set; }
        public string mobileNo { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public int cityID { get; set; }
        public int nationaltyID { get; set; }
        public int gender { get; set; }
    }
}
