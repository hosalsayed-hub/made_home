using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.ViewModel.Vendor
{
    public class VendorSupportViewModel
    {
        public int VendorsID { get;  set; }
        public Guid VendorSupportGuid { get;  set; }
        public int VendorSupportID { get;  set; }
        public string Descripe { get;  set; }
        public int HelpQuestionsID { get;  set; }
        public string Image { get;  set; }
        public int? OrderVendorID { get;  set; }
        public string VendorName { get;  set; }
        public string VendorMobileNo { get;  set; }
        public string CreateDate { get;  set; }
        public string HelpQuestionsName { get;  set; }
    }
}
