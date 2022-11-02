using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.ViewModel.Vendor
{
   public class InvoiceMasterViewModel
    {
        public int InvoiceMasterID { get; set; }
        public Guid InvoiceMasterGuid { get; set; }
        public int VendorsID { get; set; }
        public int InvoiceTypeId { get; set; }  // InvoiceTypeEnum
        public string InvoiceNo { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal Discount { get; set; }
        public decimal Vat { get; set; }
        public string CreateDate { get; set; }
        public string VendorName { get; set; }
        public decimal DebitAmount { get; set; }
        public decimal Total { get; set; }
        public string InvoiceTypeName { get; set; }
    }
}
