using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.ViewModel.Driver
{
   public class DriverSupportViewModel
    {
        public int DriverSupportID { get; set; }
        public Guid DriverSupportGuid { get; set; }
        public int HelpQuestionsID { get; set; }
        public int DriversID { get; set; }
        public int? OrderVendorID { get; set; }
        public string Descripe { get; set; }
        public string Image { get; set; }
        public string DriverName { get; set; }
        public string DriverIDNo { get; set; }
        public string DriverMobileNo { get; set; }
        public string HelpQuestionsName { get; set; }
        public string CreateDate { get; set; }
    }
}
