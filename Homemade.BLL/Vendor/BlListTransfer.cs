using Homemade.BLL.ViewModel.Vendor;
using Homemade.DAL.Contract;
using Homemade.Model.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.Vendor
{
    public class BlListTransfer
    {
        #region Decleration
        private readonly IUOW Uow;
        public BlListTransfer(IUOW _uow)
        {
            this.Uow = _uow;
        }
        #endregion
        #region Helper
        public ListTransfer GetListTransferByGuid(Guid ListTransferGuid)
        {
            var data = Uow.ListTransfer.GetAll(x => !x.IsDeleted && x.ListTransferGuid == ListTransferGuid).FirstOrDefault();
            return data;
        }
        public ListTransferViewModel GetListTransferViewModelById(int ListTransferID, string accLanguage, string invoiceView)
        {
            var data = Uow.ListTransfer.GetAll(x => !x.IsDeleted && x.ListTransferID == ListTransferID).OrderByDescending(a => a.CreateDate).Select(m => new ListTransferViewModel()
            {
                CreateDate = m.CreateDate.ToString("dd/MM/yyyy hh:mm tt"),
                VendorsID = m.InvoiceMaster.VendorsID,
                ListTransferGuid = m.ListTransferGuid,
                ListTransferID = m.ListTransferID,
                InvoiceNo = m.InvoiceMaster.InvoiceNo,
                VendorName = accLanguage == "ar" ? m.InvoiceMaster.Vendors.StoreNameAr : m.InvoiceMaster.Vendors.StoreNameEn,
                Vat = m.InvoiceMaster.Vat,
                Discount = m.InvoiceMaster.Discount,
                DebitAmount = m.InvoiceMaster.InvoiceDetails.Any(x => !x.IsDeleted) ? m.InvoiceMaster.InvoiceDetails.Sum(x => x.Total) : 0,
                Total = m.Total,
                TransferDate = m.TransferDate.ToString("dd/MM/yyyy hh:mm tt"),
                Attachment = !String.IsNullOrEmpty(m.Attachment) ? invoiceView + m.Attachment : string.Empty,
                BankID = m.BanksID,
                BankName = m.BanksID != null ? (accLanguage == "ar" ? m.Banks.NameAR : m.Banks.NameEN) : String.Empty,
                ReferenceNo = m.ReferenceNo,
                InvoiceMasterID = m.InvoiceMasterID,
            }).FirstOrDefault();
            return data;
        }
        public IQueryable<ListTransferViewModel> GetAllListTransferViewModelForListCollected(string[] ListClientID, DateTime? FromDate, DateTime? ToDate, string accLanguage, string invoiceView)
        {
            var data = Uow.ListTransfer.GetAll(x => !x.IsDeleted
                                                     && (ListClientID != null ? ListClientID.Any(y => x.InvoiceMaster.VendorsID.ToString() == y) : true)
                                                     && (FromDate != null ? x.TransferDate.Date >= FromDate.Value.Date : true)
                                                     && (ToDate != null ? x.TransferDate.Date <= ToDate.Value.Date : true)
                , "").OrderByDescending(a => a.CreateDate).Select(m => new ListTransferViewModel()
                {
                    CreateDate = m.CreateDate.ToString("dd/MM/yyyy hh:mm tt"),
                    VendorsID = m.InvoiceMaster.VendorsID,
                    ListTransferGuid = m.ListTransferGuid,
                    ListTransferID = m.ListTransferID,
                    InvoiceNo = m.InvoiceMaster.InvoiceNo,
                    VendorName = accLanguage == "ar" ? m.InvoiceMaster.Vendors.StoreNameAr : m.InvoiceMaster.Vendors.StoreNameEn,
                    Vat = m.InvoiceMaster.Vat,
                    Discount = m.InvoiceMaster.Discount,
                    DebitAmount = m.InvoiceMaster.InvoiceDetails.Any(x => !x.IsDeleted) ? m.InvoiceMaster.InvoiceDetails.Sum(x => x.Total) : 0,
                    Total = m.Total,
                    TransferDate = m.TransferDate.ToString("dd/MM/yyyy hh:mm tt"),
                    Attachment = !String.IsNullOrEmpty(m.Attachment) ? invoiceView + m.Attachment : string.Empty,
                    BankID = m.BanksID,
                    BankName = m.BanksID != null ? (accLanguage == "ar" ? m.Banks.NameAR : m.Banks.NameEN) : String.Empty,
                    ReferenceNo = m.ReferenceNo,
                    InvoiceMasterID = m.InvoiceMasterID,
                });
            return data;
        }
        #endregion
    }
}
