using System.Collections.Generic;

namespace Homemade.BLL.ViewModel.Vendor
{
    public class MasterProductsViewModelApi
    {
        public bool isNextPage { get; set; }
        public List<ProductWithoutApiViewModel> products { get; set; }
    }
    public class ProductWithoutApiViewModel
    {
        public string productName { get; set; }
        public string price { get; set; }
        public decimal weight { get; set; }
        public int productID { get; set; }
        public string image { get; set; }
    }

}
