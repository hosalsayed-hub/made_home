using System.Collections.Generic;

namespace Homemade.BLL.ViewModel.Vendor
{
    public class CartDetailsViewModelApi
    {
        public List<CartVendorViewModel> listVendors { get; set; }
    }
    public class CartVendorViewModel
    {
        public int vendorId { get; set; }
        public string vendorName { get; set; }
        public string vendorImage { get; set; }
        public int approvalQuantity { get; set; }
        public List<CartItemsViewModel> products { get; set; }
    }
    public class CartItemsViewModel
    {
        public int productId { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public decimal count { get; set; }
        public decimal availableQuantity { get; set; }
        public bool isIncreaseItem { get; set; }
        public string productImage { get; set; }
    }
}
