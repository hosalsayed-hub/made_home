namespace Homemade.Model.Order
{
    public class Root
    {
        public int order_id { get; set; }
        public int customer_id { get; set; }
        public string client_order_id { get; set; }
        public string shop { get; set; }
        public string branch { get; set; }
        public int branch_id { get; set; }
        public string branch_area { get; set; }
        public string dropoff_area { get; set; }
        public string tracking_code { get; set; }
        public string tracking_url { get; set; }
        public string expected_pickup { get; set; }
        public string expected_delivery { get; set; }
        public object at_pickup_at { get; set; }
        public object pickup_at { get; set; }
        public object at_dropoff_at { get; set; }
        public object dropoff_at { get; set; }
        public string fees { get; set; }
        public double distance { get; set; }
        public string status { get; set; }
        public int status_id { get; set; }
        public object value { get; set; }
        public string payment_type { get; set; }
        public string details { get; set; }
        public string currency { get; set; }
        public object driver { get; set; }
        public string created_at { get; set; }
    }
}
