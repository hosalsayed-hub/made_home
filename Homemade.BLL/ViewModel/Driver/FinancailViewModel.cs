using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.ViewModel.Driver
{
   public class FinancailViewModel
    {
        public bool isNextPage { get; set; }
        public int countPaid { get; set; }
        public int countUnPaid { get; set; }
        public decimal amountPaid { get; set; }
        public decimal amountUnPaid { get; set; }
        public List<ListProductWallet> listOrders { get; set; }
    }
    public class ListProductWallet
    {
        public string vendorImage { get; set; }
        public int vendorId { get; set; }
        public string vendorName { get; set; }
        public string date { get; set; }
        public string address { get; set; }
        public string status { get; set; }
        public int orderId { get; set; }
        public int orderStatusId { get; set; }
        public string trackNo { get; set; }
        public decimal deliveryPrice { get; set; }
        public string customerLogo { get; set; }
        public string customerName { get; set; }
        public string colorHex { get;  set; }
        public string colorText { get;  set; }
    }
}
