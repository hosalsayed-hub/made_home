using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.ViewModel.Vendor
{
   public class VendorHomeViewModelApi
    {
        public List<ListProductVendor> orderList { get; set; }
        public decimal overTarget { get;  set; }
        public decimal monthlyTarget { get;  set; }
        public int cutomers { get;  set; }
        public int sales { get;  set; }
        public int orders { get;  set; }
        public decimal paymentCards { get;  set; }
        public decimal bankTransfer { get;  set; }
        public decimal cash { get;  set; }
        public decimal monthlyTargetPercent { get; set; }
        public bool monthlyTargetSuccess { get; set; }
        public decimal enterMonthlyTarget { get; set; }
    }
}
