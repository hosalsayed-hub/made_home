using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.ViewModel.Customer
{
   public class CustomerOrdersViewModelApi
    {
        public bool isNextPage { get; set; }
        public List<ListProduct> listProduct { get; set; }
    }
    public class ListProduct
    {
        public List<ListItems> listItems { get; set; }
        public string vendorImage { get;  set; }
        public int vendorId { get;  set; }
        public string vendorName { get;  set; }
        public string paymentTypestr { get;  set; }
        public string date { get;  set; }
        public string address { get;  set; }
        public string status { get;  set; }
        public string price { get;  set; }
        public int countItems { get; set; }
        public string colorHex { get; set; }
        public int orderId { get; set; }
        public int orderStatusId { get; set; }
        public bool isCancel { get; set; }
        public string trackNo { get; set; }
        public double customerLat { get; set; }
        public double customerLng { get; set; }
        public double vendorLat { get; set; }
        public double vendorLng { get; set; }
        public string customerLogo { get; set; }
        public string customerName { get; set; }
        public string customerMobile { get;  set; }
        public string customerWhats { get;  set; }
        public string vendorMobile { get;  set; }
        public string vendorWhats { get;  set; }
        public DateTime createDate { get; set; }
        public string blockName { get; set; }
        public string orderType { get; set; }
        public int? approvalQuantity { get; set; }
        public string approvalQuantityString { get; set; }
        public int orderVendorID { get; set; }
    }
    public class ListItems
    {
        public string price { get;  set; }
        public string productName { get;  set; }
        public decimal quantity { get; set; }
        public string productImage { get; set; }
    }
}
