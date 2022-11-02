using System.Collections.Generic;

namespace Homemade.BLL.ViewModel.Vendor
{
    public class ProductQuestionViewModelApi
    {
        public List<ListProductQuestionViewModelApi> questions { get; set; }
        public bool isNextPage { get; set; }
    }
    public class ListProductQuestionViewModelApi
    {
        public int productQuestionID { get; set; }
        public int productID { get; set; }
        public string question { get; set; }
        public string answer { get; set; }
    }
}
