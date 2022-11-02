using System;
using System.Collections.Generic;

namespace Homemade.BLL.ViewModel.Site
{
    public class SiteOrdersDetailsViewModel
    {
        public int OrdersID { get; set; }
        public Guid OrdersGuid { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal Vat { get; set; }
        public decimal DeliveryPrice { get; set; }
        public decimal Total { get; set; }
        public string Notes { get; set; }
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
        public string CustomersProfilePic { get; set; }
        public string CustomersEmail { get; set; }
        public string CustomerLocationAddress { get; set; }
        public double CustomerLocationLat { get; set; }
        public double CustomerLocationLng { get; set; }
        public string CustomerLocationBlockName { get; set; }
        public string CustomerLocationCityName { get; set; }
        public string OrderStatusName { get; set; }
        public string VendorsLogo { get; set; }
        public string VendorsName { get; set; }
        public string PaymentTypeName { get; set; }
        public string OrderAccept { get; set; }
        public string OrderBeingProcessed { get; set; }
        public string OrderBeingDelivery { get; set; }
        public string OrderDelivered { get; set; }
        public string OrderCreate { get; set; }
        public string OrderCancel { get; set; }
        public string OrderShipping { get; set; }
        public IEnumerable<SiteOrderItems> SiteOrderItems { get; set; }
        public string TrackNo { get; set; }
        public Guid? VendorsGuid { get; set; }
        public int CaptainTypeId { get; set; }
        public string CaptainName { get; set; }
        public string CaptainPhone { get; set; }
        public decimal DeliveryVatValue { get; set; }
        public decimal VatProduct { get; set; }
        public decimal VatValue { get; set; }
        public decimal VatPercent { get; set; }
        public int OrderVendorID { get; set; }
    }
    public class SiteOrderItems
    {
        public string ProdName { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public string ProdImage { get; set; }

    }
    public class SiteOrdersVatViewModel
    {
        public int OrdersID { get; set; }
        public Guid OrdersGuid { get; set; }
        public string TaxIdentificationNumber { get; set; }
        public string TrackNo { get; set; }
        public string OrderDate { get; set; }
        public decimal DeliveryPrice { get; set; }
        public decimal DeliveryVatPercent { get; set; }
        public decimal DeliveryVatValue { get; set; }
        public string Address { get; internal set; }
        public string VendorsName { get; set; }
        public IEnumerable<SiteOrderItems> SiteOrderItems { get; set; }
        public decimal Discount { get; set; }
        public decimal Price { get; set; }
        public decimal VatProduct { get; set; }
        public decimal Total { get; set; }
        public decimal VatValue { get; set; }
        public Byte[] BarCode { get; set; }
        public int OrderVendorID { get; set; }
    }

}
