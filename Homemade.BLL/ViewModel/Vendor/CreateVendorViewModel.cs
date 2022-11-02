using Microsoft.AspNetCore.Http;

namespace Homemade.BLL.ViewModel.Vendor
{
    public class CreateVendorViewModel
    {
        public string mobileNo { get; set; }
        public string firstName { get; set; }
        public string seconedName { get; set; }
        public string email { get; set; }
        public int cityID { get; set; }
        public string password { get; set; }
        public string address { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
        public int blockID { get; set; }
    }
    public class ProfileVendorViewModel
    {
        public string mobileNo { get; set; }
        public string firstName { get; set; }
        public string seconedName { get; set; }
        public string email { get; set; }
        public int nationaltyID { get; set; }
        public int gender { get; set; }
    }
}
