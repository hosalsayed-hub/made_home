using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Homemade.BLL.ViewModel.Vendor
{
    public class OrderRequestViewModel
    {
        public List<RequestQuantityViewModel> order { get; set; }
    }
    public class RequestQuantityViewModel
    {
        public int productID { get; set; }
        public double quantity { get; set; }
    }
    public class OrderViewModelApi
    {
        public string customerName { get; set; }
        public string customerPhone { get; set; }
        public string alternatePhone { get; set; }
        public int? blockID { get; set; }
        public int pickupLocationId { get; set; }
        public DateTime? deliveryDate { get; set; }
        public string address { get; set; }
        public string notes { get; set; }
        public decimal lat { get; set; }
        public decimal lng { get; set; }
        public IFormFileCollection image { get; set; }
        public int? callOrderID { get; set; }
        public int orderTypeId { get; set; }
    }
}
