using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.ViewModel.Customer
{
    public class CustomerAddressViewModelApi
    {
        public string address { get;  set; }
        public string name { get;  set; }
        public string mobileNo { get;  set; }
        public int blockID { get;  set; }
        public int cityID { get;  set; }
        public int regionID { get;  set; }
        public double lat { get;  set; }
        public double lng { get;  set; }
        public string streetNo { get;  set; }
        public string uniqueSign { get;  set; }
        public int addressTypesID { get;  set; }
        public string buildingNumber { get;  set; }
    }
    public class UpdateCustomerLocation : CustomerAddressViewModelApi
    {
        public string addressTypesName { get; set; }
        public int addressID { get; set; }
        public string blockName { get; set; }
        public string cityName { get; set; }
        public string regionName { get; set; }
    }
}
