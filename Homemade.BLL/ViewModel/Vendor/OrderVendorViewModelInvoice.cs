using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.ViewModel.Vendor
{
    public class OrderVendorViewModelInvoice
    {
        public decimal Total { get;  set; }
        public string CreateDate { get;  set; }
        public DateTime CreationDate { get;  set; }
        public decimal Discount { get;  set; }
        public string DriverName { get;  set; }
        public string DriverPhoneNumber { get;  set; }
        public string CustomerName { get;  set; }
        public string CustomerMobileNo { get;  set; }
        public Guid OrderVendorGuid { get;  set; }
        public int OrderVendorID { get;  set; }
        public string OrderStatusName { get;  set; }
        public int? DriversID { get;  set; }
        public int OrderStatusID { get;  set; }
        public string VendorName { get;  set; }
        public string CatainType { get; set; }
        public decimal PerHome { get; set; }
        public decimal DeliveryPrice { get; set; }
        public int VendorsID { get; set; }
        public decimal TotalBaseItem { get; set; }
        public decimal DeliveryPriceVatValue { get; set; }
        public decimal VatProduct { get; set; }
        public decimal VatValue { get; set; }
        public decimal PerStore { get; set; }
        public string PaymentType { get;  set; }
        public decimal PackageAmount { get;  set; }
        public decimal PackagePercent { get;  set; }
        public string PerCompanyPercent { get;  set; }
        public string perStorePercent { get;  set; }
    }
}
