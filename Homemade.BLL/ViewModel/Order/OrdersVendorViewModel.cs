using Homemade.BLL.ViewModel.Vendor;
using System;
using System.Collections.Generic;

namespace Homemade.BLL.ViewModel.Order
{
    public class OrdersVendorViewModel
    {
        public int OrderVendorID { get; set; }
        public Guid OrderVendorGuid { get; set; }
        public Guid OrdersGuid { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal Vat { get; set; }
        public decimal DeliveryPrice { get; set; }
        public decimal Total { get; set; }
        public string TrackNo { get; set; }
        public string InvoiceImage { get; set; }
        public string Notes { get; set; }
        public int OrdersID { get; set; }
        public int VendorsID { get; set; }
        public int OrderStatusID { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsEnable { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public DateTime? EnableDate { get; set; }
        public int UserId { get; set; }
        public int? UserUpdate { get; set; }
        public int? UserDelete { get; set; }
        public int? UserEnable { get; set; }
        public int CustomersID { get; set; }
        public string CustomersName { get; set; }
        public string CustomersMobileNo { get; set; }
        public string VendorsName { get; set; }
        public int ProductsCount { get; set; }
        public string OrderStatusName { get; set; }
        public string VendorMobilNo { set; get; }
        public string VendorEmail { set; get; }
        public string CustomersEmail { set; get; }
        public List<ProductViewModel> Products { set; get; }
        public string CustomerProgilePic { set; get; }
        public string VendorLogo { set; get; }
        public string CaptainName { get; set; }
        public string Place { get; set; }
        public string UpdateDateString { get; set; }
        public int CaptainTypeId { get; set; }
        public string CustomerBlockName { get; set; }
        public string VendorBlockName { get; set; }
        public string CaptainTypeName { get; set; }
        public string CaptainPhone { get; set; }
        public string IsOnlineString { get; set; }
        public string CreateDateString { get; set; }
        public int? DriversID { get; set; }
        public string OrderTypeName { get; set; }
        public string ScheduleDateString { get; set; }
        public string ScheduleTimeString { get; set; }
        public int OrderTypeId { get; set; }
        public DateTime? ScheduleDate { get; internal set; }
        public string IntegrationOrderId { get; set; }
        public decimal VatProduct { get; set; }
        public decimal DeliveryVatValue { get; set; }
        public decimal VatValue { get; set; }
        public bool IsEdit { get; set; } = false;
    }
}
