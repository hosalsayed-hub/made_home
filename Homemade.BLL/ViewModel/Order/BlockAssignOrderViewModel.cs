namespace Homemade.BLL.ViewModel.Order
{
    public class BlockAssignOrderViewModel
    {
        public int BlockID { get; set; }
        public int OrdersCount { get; set; }
        public string BlockName { get; set; }
    }
    public class AssignOrderVendorViewModel
    {
        public int OrderVendorID { get; set; }
        public string CustomersName { get; set; }
        public string BlockName { get; set; }
        public decimal? DistanceKM { get; set; }
        public int? BlockID { get; set; }
        public string OrderStatusName { get; set; }
    }
    public class DriverAssignOrderViewModel
    {
        public int DriversID { get; set; }
        public string DriversName { get; set; }
        public string BlockName { get; set; }
    }
}
