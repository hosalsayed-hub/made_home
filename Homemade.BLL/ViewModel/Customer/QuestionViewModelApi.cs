using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.ViewModel.Customer
{
   public class QuestionViewModelApi
    {
        public int productId { get; set; }
        public string question { get; set; }
    }
    public class RateViewModelApi
    {
        public int orderId { get; set; }
        public string commentDelivery { get; set; }
        public string commentOrder { get; set; }
        public decimal rateDelivery { get; set; }
        public decimal rateOrder { get; set; }
    }
}
