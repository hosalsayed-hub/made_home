using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.ViewModel.Order
{
    public class CancelOrderViewModelApi
    {
        public int orderId { get; set; }
        public string reason { get; set; }
    }
   public class AddOrderViewModelApi
    {
        public int? promoCodeID { get; set; }
        public int addressID { get; set; }
        public int paymentTypeId { get; set; }
        public string invoiceId { get; set; }
        public string customerReference { get; set; }
        public List<VendorOrderApi> vendorOrder { get; set; }
    }
    public class VendorOrderApi
    {
        public string notes { get; set; }
        public int vendorId { get; set; }
        public List<ItemVendorApi> products { get; set; }
    }
    public class ItemVendorApi
    {
        public int productId { get; set; }
        public decimal quantity { get; set; }      
    }
    public class AddOrderViewModelApiNew
    {
        public int? promoCodeID { get; set; }
        public int addressID { get; set; }
        public int paymentTypeId { get; set; }
        public string invoiceId { get; set; }
        public string customerReference { get; set; }
        public int orderTypeId { get; set; } = 0; // OrderTypeEnum
        public string scheduleDate { get; set; } 
        public string scheduleTime { get; set; } 
        public List<VendorOrderApiNew> vendorOrder { get; set; }
    }
    public class AddOrderViewModelApiNewPending
    {
        public int? promoCodeID { get; set; }
        public int addressID { get; set; }
        public int paymentTypeId { get; set; }
        public string invoiceId { get; set; }
        public string customerReference { get; set; }
        public int orderTypeId { get; set; } = 0; // OrderTypeEnum
        public string scheduleDate { get; set; }
        public string scheduleTime { get; set; }
        public int orderId { get; set; }
        public List<VendorOrderApiNew> vendorOrder { get; set; }
    }
    public class VendorOrderApiNew
    {
        public string notes { get; set; }
        public int vendorId { get; set; }
        public decimal deliveryprice { get; set; }
        public decimal distanceKM { get; set; }
        
        public List<ItemVendorApi> products { get; set; }
    }
    
}
