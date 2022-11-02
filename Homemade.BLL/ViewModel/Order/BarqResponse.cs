using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.ViewModel.Order
{
  
    public class Merchant
    {
        public int id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public string manager_name { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string server_ip { get; set; }
        public bool is_active { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }

    public class Origin
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    }

    public class Hub
    {
        public int id { get; set; }
        public string code { get; set; }
        public string manager { get; set; }
        public string mobile { get; set; }
        public string phone { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public bool is_active { get; set; }
        public int city_id { get; set; }
        public int merchant_id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime opening_time { get; set; }
        public DateTime closing_time { get; set; }
        public int start_day { get; set; }
        public int end_day { get; set; }
        public object store_id { get; set; }
    }
    public class BarqResponse
    {
        public int id { get; set; }
        public string tracking_no { get; set; }
        public string payment_type { get; set; }
        public string shipment_type { get; set; }
        public string order_status { get; set; }
        public bool is_assigned { get; set; }
        public bool is_active { get; set; }
        public double grand_total { get; set; }
        public double delivery_fee { get; set; }
        public double invoice_total { get; set; }
        public double cod_fee { get; set; }
        public string pickup_otp { get; set; }
        public string delivery_otp { get; set; }
        public string return_otp { get; set; }
        public string merchant_order_id { get; set; }
        public object merchant_tracking_no { get; set; }
        public Merchant merchant { get; set; }
        public Origin origin { get; set; }
        public string address_status { get; set; }
        public Destination destination { get; set; }
        public List<ProductBarq> products { get; set; }
        public Hub hub { get; set; }
        public object courier { get; set; }
        public CustomerDetails customer_details { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime created_at { get; set; }
        public object shipment { get; set; }
    }


}
