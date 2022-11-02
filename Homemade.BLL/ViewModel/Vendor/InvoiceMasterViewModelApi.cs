using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.ViewModel.Vendor
{
   public class InvoiceMasterViewModelApi
    {
        public decimal perStore { get; set; }
        public decimal perHomeMade { get; set; }
        public bool isNextPage { get; set; }
        public string lastInvoiceDate { get; set; }
        public List<InvoiceVendorList> invoices { get; set; }
    }
    public class InvoiceVendorList
    {
        public int invoiceId { get; set; }
        public string colorHex { get; set; }
        public string colorText { get; set; }
        public string status { get; set; }
        public string invoiceNo { get; set; }
        public string date { get; set; }
        public string total { get; set; }
        public string perOrderStore { get; set; }
        public string perOrderHomeMade { get; set; }
        public int statusId { get; set; }
        public DateTime createDate { get; set; }
        public decimal perHomeDe { get;  set; }
        public decimal perStoreDe { get;  set; }
    }
    public class InvoiceDetailsViewModelApi
    {
        public string invoiceNo { get; set; }
        public string date { get; set; }
        public bool isNextPage { get; set; }
        public List<InvoiceOrderList> invoices { get; set; }
    }
    public class InvoiceOrderList
    {
        public string trackNo { get; set; }
        public string date { get; set; }
        public string price { get; set; }
        public string vat { get; set; }
        public string deliverycharge { get; set; }
        public string promocode { get; set; }
        public string discount { get; set; }
        public string total { get; set; }
        public int orderId { get; set; }
    }
}
