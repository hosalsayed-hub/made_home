using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.ViewModel.Order
{
    public class CustomerDetails
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public string mobile { get; set; }
        public string address { get; set; }
    }

    public class ProductBarq
    {
        public string sku { get; set; }
        public string serial_no { get; set; }
        public string name { get; set; }
        public string color { get; set; }
        public string brand { get; set; }
        public double price { get; set; }
        public double weight_kg { get; set; }
        public int qty { get; set; }
    }

    public class Destination
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    }

    public class BarqCreate
    {
        public int payment_type { get; set; }
        public int shipment_type { get; set; }
        public int hub_id { get; set; }
        public string hub_code { get; set; }
        public string merchant_order_id { get; set; }
        public string invoice_total { get; set; }
        public CustomerDetails customer_details { get; set; }
        public List<ProductBarq> products { get; set; }
        public Destination destination { get; set; }
    }


}
