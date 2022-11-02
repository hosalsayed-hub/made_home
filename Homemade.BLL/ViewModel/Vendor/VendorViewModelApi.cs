using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.ViewModel.Vendor
{
    public class VendorViewModelApi
    {
        public int cityID { get; set; }
        public int gender { get; set; }
        public string mobileNo { get; set; }
        public string email { get; set; }
        public string firstName { get; set; }
        public string seconedName { get; set; }
        public int? nationalityID { get; set; }
        public string address { get; set; }
        public string cityName { get; set; }
        public string nationalityName { get; set; }
        public string profilePic { get; set; }
        public int vendorsID { get; set; }
        public bool isVac { get; set; }
        public int notificationsCount { get; set; }
        public bool isLocation { get; set; }
    }
}
