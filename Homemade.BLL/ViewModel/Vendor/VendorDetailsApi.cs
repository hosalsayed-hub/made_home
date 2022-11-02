using Homemade.BLL.ViewModel.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.ViewModel.Vendor
{
   public class VendorDetailsApi
    {

        public ProdResonse productList { get; set; }
        public string banner { get;  set; }
        public string profilePic { get;  set; }
        public string name { get;  set; }
        public string storeName { get;  set; }
        public string descripe { get;  set; }
        public double lat { get;  set; }
        public double lng { get;  set; }
        public string address { get;  set; }
        public decimal rate { get; set; }
        public string startWork { get; set; }
        public string vacWork { get; set; }
        public bool isVendorWorking { get; set; }
        public string isVendorWorkingString { get; set; }
    }
}
