using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.ViewModel.Vendor
{
    public class InvoiceDetailsViewModel
    {
        public int InvoiceMasterID { get; set; }
        public int InvoiceDetailsID { get; set; }
        public Guid InvoiceDetailsGuid { get; set; }
        public int VendorsID { get; set; }
        public int InvoiceTypeId { get; set; }  // InvoiceTypeEnum
        public string InvoiceNo { get; set; }
        public int OrderVendorID { get; set; }
        public string CustomerMobile { get; set; }
        public string CustomerName { get; set; }
        public string CreateDate { get; set; }
        public string VendorName { get; set; }
        public decimal DebitAmount { get; set; }
        public string FinishDate { get;  set; }
    }
}
