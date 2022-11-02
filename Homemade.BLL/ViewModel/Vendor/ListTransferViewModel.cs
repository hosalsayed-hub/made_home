using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.ViewModel.Vendor
{
    public class ListTransferViewModel
    {
        public int ListTransferID { get; set; }
        public Guid ListTransferGuid { get; set; }
        public Guid InvoiceMasterGuid { get; set; }
        public int VendorsID { get; set; }
        public int InvoiceMasterID { get; set; }
        public string InvoiceNo { get; set; }
        public int? BankID { get; set; }
        public decimal Total { get; set; }
        public string ReferenceNo { get; set; }
        public string Attachment { get; set; }
        public string TransferDate { get; set; }
        public string CreateDate { get; set; }
        public string VendorName { get; set; }
        public string BankName { get; set; }
        public decimal Vat { get; set; }
        public decimal Discount { get; set; }
        public decimal DebitAmount { get; set; }
    }
}
