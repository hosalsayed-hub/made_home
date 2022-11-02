using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.ViewModel.Order
{
    public class OrdersViewModelApi
    {
         public string vatAddress { get; set; }

        public List<OrderItemsApi> orderItems { get; set; }
        public string deliveryPrice { get;   set; }
        public string discount { get;   set; }
        public int orderStatusID { get;   set; }
        public string price { get;   set; }
        public string total { get;   set; }
        public string vat { get;   set; }
        public string vendorName { get;   set; }
        public string paymentType { get;   set; }
        public string vendorPic { get;   set; }
        public string address { get;   set; }
        public string trackNo { get;   set; }
        public string date { get;   set; }
        public string createDate { get;   set; }
        public string createTime { get;   set; }
        public string processDate { get;   set; }
        public string processTime { get;   set; }
        public string deliveryDate { get;   set; }
        public string deliveryTime { get;   set; }
        public string cancelDate { get;   set; }
        public string cancelTime { get;   set; }
        public string receivedDate { get;   set; }
        public string receivedTime { get;   set; }
        public string paymentStatus { get; set; }
        public bool isRated { get; set; }
        public decimal vatPercent { get; set; }
        public string totalWithDelivery { get;  set; }
        public bool isShowContact { get;   set; }
        public string mobileNo { get;   set; }
        public string deliveryVatPercent { get; set; }
        public string deliveryVatValue { get; set; }
        public string taxNumber { get; set; }
        public string orderType { get; set; }
        public int orderVendorID { get; set; }
        public string companyName { get; set; }
        public string captainName { get; set; }
        public string captainMobile { get; set; }
        public string totalBeforDiscount { get; set; }
        public string invoiceCompanyName { get; set; }
    }
    public class OrderItemsApi
    {
        public string logo { get;   set; }
        public string name { get;   set; }
        public decimal quantity { get;   set; }
        public string price { get;   set; }
    }
   
}
