using System.Collections.Generic;

namespace Homemade.BLL.ViewModel.Order
{
    public class UpdateIncreaseOrderViewModelApi
    {
        public int orderVendorID { get; set; }
        public int type { get; set; }
        public List<IncreaseOrderItemViewModelAPI> listItem { get; set; }
    }
    public class IncreaseOrderItemViewModelAPI
    {
        public int orderItemId { get; set; }
        public decimal quantity { get; set; }
    }
}
