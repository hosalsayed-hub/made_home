using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.ViewModel.Order
{
    public class OrdersVendorViewModelApi
    {
        public string trackNo { get; set; }
        public string price { get; set; }
        public string paymentType { get; set; }
        public string paymentStatus { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public int orderId { get; set; }
        public List<OrderItemsApi> orderItems { get; set; }
        public string image { get;   set; }
        public string name { get;   set; }
        public string city { get;   set; }
        public string mobile { get;   set; }
        public bool isCompany { get;   set; }
        public string comapnyname { get;   set; }
        public string statusName { get; set; }
        public int orderStatusID { get; set; }
        public string address { get;   set; }
        public string uniqueSign { get;   set; }
        public string notes { get;   set; }
        public string invoiceNo { get;   set; }
        public string invoicePrice { get;   set; }
        public string invoiceVat { get;   set; }
        public string invoiceDeliveryPrice { get;   set; }
        public string invoiceDiscount { get;   set; }
        public string invoicePromo { get;   set; }
        public string invoiceTotal { get;   set; }
        public decimal invoiceVatPercent { get;  set; }
        public string invoiceTotalDelivery { get;  set; }
        public string orderType { get; set; }
        public string captainName { get; set; }
        public string captainMobile { get; set; }

        public decimal invoiceTotalWithDelivery { get; set; }

    }
}
