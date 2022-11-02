using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.ViewModel.Vendor
{
    public class VendorOrdersViewModelApi
    {
        public bool isNextPage { get; set; }
        public bool isAllow { get; set; }
        public List<ListProductVendor> listOrders { get; set; }
    }
    public class ListProductVendor
    {
        public string customerImage { get; set; }
        public string customerName { get; set; }
        public string trackNo { get; set; }
        public string price { get; set; }
        public string paymentType { get; set; }
        public string paymentStatus { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public int orderId { get; set; }
        public string colorHex { get; set; }
        public string colorText { get; set; }
        public string canceledBy { get; set; }
        public string customerAddress { get; set; }
        public string blockName { get; set; }
        public DateTime createDate { get; set; }
        public bool? isallawed { get; set; }
        public string orderType { get; set; }
        public int orderVendorID { get; set; }
        public int approvalQuantity { get; set; }
        public string approvalQuantityString { get; set; }

    }
}
