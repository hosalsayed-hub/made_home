using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.ViewModel.Customer
{
    public class CustomerViewModelApi
    {
        public int cityID { get;  set; }
        public int gender { get;  set; }
        public string mobileNo { get;  set; }
        public string email { get;  set; }
        public string firstName { get;  set; }
        public string seconedName { get;  set; }
        public int nationalityID { get;  set; }
        public string address { get;  set; }
        public string cityName { get;  set; }
        public string nationalityName { get;  set; }
        public string profilePic { get;  set; }
        public int customerID { get;  set; }
        public bool isLocation { get; set; }
        public int notificationCount { get; set; }
        public decimal walletAmount { get;  set; }
        public string wallet { get; set; }
    }
    public class CustomerVendorApi
    {
        public string mobileNo { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string profilePic { get; set; }
        public string address { get; set; }
        public string cityName { get; set; }
        public string nationalityName { get; set; }
        public int customersID { get; set; }
        public int coutorders { get; set; }
    }
    public class CustomersVendor
    {
        public List<CustomerVendorApi> customerVendor { get; set; }
        public bool isNextPage { get; set; }
    }
}
