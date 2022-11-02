using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.ViewModel.Driver
{
    public class TotalBalanceViewModel
    {
        public int DriverID { get; set; }
        public string DriverNameEn { get; set; }
        public string DriverNameAr { get; set; }
        public decimal TotalCredit { get; set; }
        public decimal TotalDebit { get; set; }
        public decimal NetBalance { get; set; }
    }
}
