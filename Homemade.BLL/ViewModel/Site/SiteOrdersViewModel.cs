using System;

namespace Homemade.BLL.ViewModel.Site
{
    public class SiteOrdersViewModel
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
        public int? Type { get; set; }
        public Guid? VendorsGuid { get; set; }
        public string TrackNo { get; set; }
        public string VendorsContact { get; set; }
        public int OrderTypeId { get; set; }
        public string OrderTypeName { get; set; }
        public string ScheduleDateString { get; set; }
        public string ScheduleTimeString { get; set; }
        public int OrderVendorID { get; set; }
    }
}
