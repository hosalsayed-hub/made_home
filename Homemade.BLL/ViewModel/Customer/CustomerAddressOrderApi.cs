using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.ViewModel.Customer
{
   public class CustomerAddressOrderApi
    {
        public double lat { get; set; }
        public double lng { get; set; }
        public string streetNo { get; set; }
        public string uniqueSign { get; set; }
        public string addressType { get; set; }
        public string address { get; set; }
        public string name { get; set; }
        public string mobileNo { get; set; }
        public string buildingNumber { get; set; }
        public int addressID { get; set; }
    }
}
