using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.ViewModel.Customer
{
    public class CustomerFavourritesViewModel
    {
        public int CustomerFavouritesID { get; set; }
        public Guid CustomerFavouritesGuid { get; set; }
        public int CustomersID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { set; get; }
        public string Logo { set; get; }
    }
}
