using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.ViewModel.Order
{
    public class LavenderCreate
    {
        public string RefOrderNo { get; set; }
        public string StoreName { get; set; }
        public string CustomerName { get; set; }
        public string StorePhone { get; set; }
        public string PickupLat { get; set; }
        public string PickupLong { get; set; }
        public string PickupAddress { get; set; }
        public string DestinationLat { get; set; }
        public string DestinationLong { get; set; }
        public string DestinationAddress { get; set; }
        public string LogoStore { get; set; }
        public string CustomerPhone { get; set; }
    }
    public class DriverDetail
    {
        public string name { get; set; }
        public string phone { get; set; }
        public string image { get; set; }
        public int rating { get; set; }
        public string driverLat { get; set; }
        public string driverLong { get; set; }
    }

    public class LavenderResp0nse
    {
        public string refOrderNo { get; set; }
        public string lavender_order_id { get; set; }
        public int status_code { get; set; }
        public string status { get; set; }
        public DriverDetail driverDetail { get; set; }
    }
}
