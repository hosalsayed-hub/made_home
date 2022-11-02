using System;

namespace Homemade.BLL.ViewModel.Order
{
    public class OrderRateViewModel
    {
        public int OrderRateID { get; set; }
        public Guid OrderRateGuid { get; set; }
        public string CommentDelivery { get; set; }
        public string CommentOrder { get; set; }
        public string AnswerRate { get; set; }
        public decimal RateOrder { get; set; }
        public decimal RateDelivery { get; set; }
        public bool IsRepley { get; set; }
        public int OrderVendorID { get; set; }
        public string CustomerName { get; set; }
        public string RateDate { get; set; }
        public string TrackNo { get; set; }
        public string RateTitle { get; set; }
        public string VendorName { get; set; }
        public string CustomerImage { get; set; }
        public string VendorImage { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid CustomersGuid { get; set; }
        public Guid VendorsGuid { get; set; }
    }
}
