using System.Collections.Generic;

namespace Homemade.BLL.ViewModel.Order
{
    public class IncreaseOrdersViewModelApi
    {

        public List<OrderItemsIncreaseApi> orderItems { get; set; }
      
        public int orderVendorID { get; set; }
        public int approvalQuantity { get; set; }
    }
    public class OrderItemsIncreaseApi
    {
        public string logo { get; set; }
        public string name { get; set; }
        public decimal quantity { get; set; }
        public string price { get; set; }
        public int orderItemId { get; set; }
    }
}
