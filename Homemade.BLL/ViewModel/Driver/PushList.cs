using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.ViewModel.Driver
{
    public class PushList
    {
        public int orderId { get; set; }
        public int estimation { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
        public string profit { get; set; }
        public string vendorName { get; set; }
        public string vendorLogo { get; set; }
        public string vendorAddress { get; set; }
        public string cusotmerAddress { get;  set; }
        public string trackNo { get; set; }
        public string title { get;  set; }
        public string body { get;  set; }
        public string sound { get;  set; }
        public string priority { get;  set; }
        public string content_available { get;  set; }
        public string serverKey { get; set; }
        public int pushType { get; set; }
    }
}
