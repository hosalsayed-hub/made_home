using Homemade.BLL.Driver;
using Homemade.BLL.General;
using Homemade.BLL.ViewModel.Vendor;
using Homemade.DAL.Contract;
using Homemade.Model.Order;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.Vendor
{
   public class BlInvoice
    {
        #region Decleration
        private readonly IUOW Uow;
        private readonly BLGeneral _bLGeneral;
        private readonly BlTransactionType _blTransactionType;
        private readonly BlVendorBalance _BlVendorBalance;
        public BlInvoice(IUOW _uow, BLGeneral bLGeneral, BlTransactionType blTransactionType, BlVendorBalance BlVendorBalance)
        {
            _bLGeneral = bLGeneral;
            this.Uow = _uow;
            _blTransactionType = blTransactionType;
            _BlVendorBalance = BlVendorBalance;
        }
        #endregion
        #region Helper
        public InvoiceMaster GetInvoiceMasterByGuid(Guid InvoiceMasterGuid)
        {
            var data = Uow.InvoiceMaster.GetAll(x => !x.IsDeleted && x.InvoiceMasterGuid == InvoiceMasterGuid).FirstOrDefault();
            return data;
        }
        public InvoiceMaster GetInvoiceMasterById(int InvoiceMasterID)
        {
            var data = Uow.InvoiceMaster.GetAll(x => !x.IsDeleted && x.InvoiceMasterID == InvoiceMasterID).FirstOrDefault();
            return data;
        }
        public InvoiceMasterViewModel GetInvoiceMasterViewModelById(int InvoiceMasterID, string accLanguage)
        {
            var data = Uow.InvoiceMaster.GetAll(x => !x.IsDeleted && x.InvoiceMasterID == InvoiceMasterID).OrderByDescending(a => a.CreateDate).Select(m => new InvoiceMasterViewModel()
            {

                CreateDate = m.CreateDate.ToString("dd/MM/yyyy hh:mm tt"),
                Discount = m.Discount,
                VendorsID = m.VendorsID,
                Vat = m.Vat,
                InvoiceMasterGuid = m.InvoiceMasterGuid,
                InvoiceMasterID = m.InvoiceMasterID,
                InvoiceNo = m.InvoiceNo,
                InvoiceTypeId = m.InvoiceTypeId,
                VendorName = accLanguage == "ar" ? m.Vendors.StoreNameAr : m.Vendors.StoreNameEn,
                DebitAmount = m.InvoiceDetails.Any(x => !x.IsDeleted) ? m.InvoiceDetails.Sum(x => x.Total) : 0,
                Total = ((m.InvoiceDetails.Any(x => !x.IsDeleted) ? m.InvoiceDetails.Sum(x => x.Total) : 0) - m.Discount)
                        + (((m.InvoiceDetails.Any(x => !x.IsDeleted) ? m.InvoiceDetails.Sum(x => x.Total) : 0)) * (m.Vat / 100)),
                InvoiceTypeName = accLanguage == "ar" ? (m.InvoiceTypeId == (int)InvoiceTypeEnum.Pending_Operation ? "انتظار المراجعة" : m.InvoiceTypeId == (int)InvoiceTypeEnum.Pending_Admin ? "انتظار اعتماد التحويل" : m.InvoiceTypeId == (int)InvoiceTypeEnum.Pending_Paid ? "انتظار تأكيد التحويل" : m.InvoiceTypeId == (int)InvoiceTypeEnum.Paid ? "تم التحويل" : "")
                        : (m.InvoiceTypeId == (int)InvoiceTypeEnum.Pending_Operation ? "Waiting review" : m.InvoiceTypeId == (int)InvoiceTypeEnum.Pending_Admin ? "Waiting transfer approval" : m.InvoiceTypeId == (int)InvoiceTypeEnum.Pending_Paid ? "Waiting transfer confirmation" : m.InvoiceTypeId == (int)InvoiceTypeEnum.Paid ? "Transferred" : ""),

            }).FirstOrDefault();
            return data;
        }
        public IQueryable<InvoiceMasterViewModel> GetAllInvoiceMasterViewModelForInvoiceReport(string[] ListClientID, DateTime? FromDate, DateTime? ToDate, string accLanguage, int? statustype)
        {
            var data = Uow.InvoiceMaster.GetAll(x => !x.IsDeleted
                                                     && (statustype != null ? x.InvoiceTypeId == statustype : true)
                                                     && (ListClientID != null ? ListClientID.Any(y => x.VendorsID.ToString() == y) : true)
                                                     && (FromDate != null ? x.CreateDate.Date >= FromDate.Value.Date : true)
                                                     && (ToDate != null ? x.CreateDate.Date <= ToDate.Value.Date : true)
                , "").OrderByDescending(a => a.CreateDate).Select(m => new InvoiceMasterViewModel()
                {

                    CreateDate = m.CreateDate.ToString("dd/MM/yyyy hh:mm tt"),
                    Discount = m.Discount,
                    VendorsID = m.VendorsID,
                    Vat = m.Vat,
                    InvoiceMasterGuid = m.InvoiceMasterGuid,
                    InvoiceMasterID = m.InvoiceMasterID,
                    InvoiceNo = m.InvoiceNo,
                    InvoiceTypeId = m.InvoiceTypeId,
                    VendorName = accLanguage == "ar" ? m.Vendors.StoreNameAr : m.Vendors.StoreNameEn,
                    DebitAmount = m.InvoiceDetails.Any(x => !x.IsDeleted) ? m.InvoiceDetails.Sum(x => x.Total) : 0,
                    Total = ((m.InvoiceDetails.Any(x => !x.IsDeleted) ? m.InvoiceDetails.Sum(x => x.Total) : 0) - m.Discount)
                    + (((m.InvoiceDetails.Any(x => !x.IsDeleted) ? m.InvoiceDetails.Sum(x => x.Total) : 0)) * (m.Vat / 100)),
                    InvoiceTypeName = accLanguage == "ar" ? (m.InvoiceTypeId == (int)InvoiceTypeEnum.Pending_Operation ? "انتظار المراجعة" : m.InvoiceTypeId == (int)InvoiceTypeEnum.Pending_Admin ? "انتظار اعتماد التحويل" : m.InvoiceTypeId == (int)InvoiceTypeEnum.Pending_Paid ? "انتظار تأكيد التحويل" : m.InvoiceTypeId == (int)InvoiceTypeEnum.Paid ? "تم التحويل" : "")
                        : (m.InvoiceTypeId == (int)InvoiceTypeEnum.Pending_Operation ? "Waiting review" : m.InvoiceTypeId == (int)InvoiceTypeEnum.Pending_Admin ? "Waiting transfer approval" : m.InvoiceTypeId == (int)InvoiceTypeEnum.Pending_Paid ? "Waiting transfer confirmation" : m.InvoiceTypeId == (int)InvoiceTypeEnum.Paid ? "Transferred" : ""),

                });
            return data;
        }
        public IQueryable<InvoiceMasterViewModel> GetAllInvoiceMasterViewModelForPendingAction(string[] ListClientID, DateTime? FromDate, DateTime? ToDate, string accLanguage)
        {
            var data = Uow.InvoiceMaster.GetAll(x => !x.IsDeleted 
 && (x.InvoiceTypeId == (int)InvoiceTypeEnum.Pending_Operation || x.InvoiceTypeId == (int)InvoiceTypeEnum.Pending_Admin)
                                                     && (ListClientID != null ? ListClientID.Any(y => x.VendorsID.ToString() == y) : true)
                                                     && (FromDate != null ? x.CreateDate.Date >= FromDate.Value.Date : true)
                                                     && (ToDate != null ? x.CreateDate.Date <= ToDate.Value.Date : true)
                , "").OrderByDescending(a => a.CreateDate).Select(m => new InvoiceMasterViewModel()
                {

                    CreateDate = m.CreateDate.ToString("dd/MM/yyyy hh:mm tt"),
                    Discount = m.Discount,
                    VendorsID = m.VendorsID,
                    Vat = m.Vat,
                    InvoiceMasterGuid = m.InvoiceMasterGuid,
                    InvoiceMasterID = m.InvoiceMasterID,
                    InvoiceNo = m.InvoiceNo,
                    InvoiceTypeId = m.InvoiceTypeId,
                    VendorName = accLanguage == "ar" ? m.Vendors.StoreNameAr : m.Vendors.StoreNameEn,
                    DebitAmount = m.InvoiceDetails.Any(x => !x.IsDeleted) ? m.InvoiceDetails.Sum(x => x.Total) : 0,
                    Total = ((m.InvoiceDetails.Any(x => !x.IsDeleted) ? m.InvoiceDetails.Sum(x => x.Total) : 0) - m.Discount)
                        + (((m.InvoiceDetails.Any(x => !x.IsDeleted) ? m.InvoiceDetails.Sum(x => x.Total) : 0)) * (m.Vat / 100)),
                    InvoiceTypeName = accLanguage == "ar" ? (m.InvoiceTypeId == (int)InvoiceTypeEnum.Pending_Operation ? "انتظار المراجعة" : m.InvoiceTypeId == (int)InvoiceTypeEnum.Pending_Admin ? "انتظار اعتماد التحويل" : m.InvoiceTypeId == (int)InvoiceTypeEnum.Pending_Paid ? "انتظار تأكيد التحويل" : m.InvoiceTypeId == (int)InvoiceTypeEnum.Paid ? "تم التحويل" : "")
                        : (m.InvoiceTypeId == (int)InvoiceTypeEnum.Pending_Operation ? "Waiting review" : m.InvoiceTypeId == (int)InvoiceTypeEnum.Pending_Admin ? "Waiting transfer approval" : m.InvoiceTypeId == (int)InvoiceTypeEnum.Pending_Paid ? "Waiting transfer confirmation" : m.InvoiceTypeId == (int)InvoiceTypeEnum.Paid ? "Transferred" : ""),

                });
            return data;
        }
        public IQueryable<InvoiceMasterViewModel> GetAllInvoiceMasterViewModelForPendingAction(int ListClientID, DateTime? FromDate, DateTime? ToDate, string accLanguage)
        {
            var data = Uow.InvoiceMaster.GetAll(x => !x.IsDeleted && ListClientID == x.VendorsID
 && (x.InvoiceTypeId == (int)InvoiceTypeEnum.Pending_Operation || x.InvoiceTypeId == (int)InvoiceTypeEnum.Pending_Admin)
                                                     && (FromDate != null ? x.CreateDate.Date >= FromDate.Value.Date : true)
                                                     && (ToDate != null ? x.CreateDate.Date <= ToDate.Value.Date : true)
                , "").OrderByDescending(a => a.CreateDate).Select(m => new InvoiceMasterViewModel()
                {

                    CreateDate = m.CreateDate.ToString("dd/MM/yyyy hh:mm tt"),
                    Discount = m.Discount,
                    VendorsID = m.VendorsID,
                    Vat = m.Vat,
                    InvoiceMasterGuid = m.InvoiceMasterGuid,
                    InvoiceMasterID = m.InvoiceMasterID,
                    InvoiceNo = m.InvoiceNo,
                    InvoiceTypeId = m.InvoiceTypeId,
                    VendorName = accLanguage == "ar" ? m.Vendors.FirstNameAr : m.Vendors.FirstNameEn,
                    DebitAmount = m.InvoiceDetails.Any(x => !x.IsDeleted) ? m.InvoiceDetails.Sum(x => x.Total) : 0,
                    Total = ((m.InvoiceDetails.Any(x => !x.IsDeleted) ? m.InvoiceDetails.Sum(x => x.Total) : 0) - m.Discount)
                        + (((m.InvoiceDetails.Any(x => !x.IsDeleted) ? m.InvoiceDetails.Sum(x => x.Total) : 0)) * (m.Vat / 100)),
                    InvoiceTypeName = accLanguage == "ar" ? (m.InvoiceTypeId == (int)InvoiceTypeEnum.Pending_Operation ? "انتظار المراجعة" : m.InvoiceTypeId == (int)InvoiceTypeEnum.Pending_Admin ? "انتظار اعتماد التحويل" : m.InvoiceTypeId == (int)InvoiceTypeEnum.Pending_Paid ? "انتظار تأكيد التحويل" : m.InvoiceTypeId == (int)InvoiceTypeEnum.Paid ? "تم التحويل" : "")
                        : (m.InvoiceTypeId == (int)InvoiceTypeEnum.Pending_Operation ? "Waiting review" : m.InvoiceTypeId == (int)InvoiceTypeEnum.Pending_Admin ? "Waiting transfer approval" : m.InvoiceTypeId == (int)InvoiceTypeEnum.Pending_Paid ? "Waiting transfer confirmation" : m.InvoiceTypeId == (int)InvoiceTypeEnum.Paid ? "Transferred" : ""),
                });
            return data;
        }
        public IQueryable<InvoiceMasterViewModel> GetAllInvoiceMasterViewModelForCollectInvoice(string[] ListClientID, DateTime? FromDate, DateTime? ToDate, string accLanguage)
        {
            var data = Uow.InvoiceMaster.GetAll(x => !x.IsDeleted
 && x.InvoiceTypeId == (int)InvoiceTypeEnum.Pending_Paid
                                                     && (ListClientID != null ? ListClientID.Any(y => x.VendorsID.ToString() == y) : true)
                                                     && (FromDate != null ? x.CreateDate.Date >= FromDate.Value.Date : true)
                                                     && (ToDate != null ? x.CreateDate.Date <= ToDate.Value.Date : true)
                , "").OrderByDescending(a => a.CreateDate).Select(m => new InvoiceMasterViewModel()
                {

                    CreateDate = m.CreateDate.ToString("dd/MM/yyyy hh:mm tt"),
                    Discount = m.Discount,
                    VendorsID = m.VendorsID,
                    Vat = m.Vat,
                    InvoiceMasterGuid = m.InvoiceMasterGuid,
                    InvoiceMasterID = m.InvoiceMasterID,
                    InvoiceNo = m.InvoiceNo,
                    InvoiceTypeId = m.InvoiceTypeId,
                    VendorName = accLanguage == "ar" ? m.Vendors.StoreNameAr : m.Vendors.StoreNameEn,
                    DebitAmount = m.InvoiceDetails.Any(x => !x.IsDeleted) ? m.InvoiceDetails.Sum(x => x.Total) : 0,
                    Total = ((m.InvoiceDetails.Any(x => !x.IsDeleted) ? m.InvoiceDetails.Sum(x => x.Total) : 0) - m.Discount)
                        + (((m.InvoiceDetails.Any(x => !x.IsDeleted) ? m.InvoiceDetails.Sum(x => x.Total) : 0)) * (m.Vat / 100)),
                    InvoiceTypeName = accLanguage == "ar" ? (m.InvoiceTypeId == (int)InvoiceTypeEnum.Pending_Operation ? "انتظار المراجعة" : m.InvoiceTypeId == (int)InvoiceTypeEnum.Pending_Admin ? "انتظار اعتماد التحويل" : m.InvoiceTypeId == (int)InvoiceTypeEnum.Pending_Paid ? "انتظار تأكيد التحويل" : m.InvoiceTypeId == (int)InvoiceTypeEnum.Paid ? "تم التحويل" : "")
                        : (m.InvoiceTypeId == (int)InvoiceTypeEnum.Pending_Operation ? "Waiting review" : m.InvoiceTypeId == (int)InvoiceTypeEnum.Pending_Admin ? "Waiting transfer approval" : m.InvoiceTypeId == (int)InvoiceTypeEnum.Pending_Paid ? "Waiting transfer confirmation" : m.InvoiceTypeId == (int)InvoiceTypeEnum.Paid ? "Transferred" : ""),

                });
            return data;
        }
        public decimal GetInvoiceTotal(int InvoiceMasterID)
        {
            var total = Uow.InvoiceMaster.GetAll(e => e.InvoiceMasterID == InvoiceMasterID).Select(m =>
                ((m.InvoiceDetails.Any(x => !x.IsDeleted) ? m.InvoiceDetails.Sum(x => x.Total) : 0) - m.Discount)
                + (((m.InvoiceDetails.Any(x => !x.IsDeleted) ? m.InvoiceDetails.Sum(x => x.Total) : 0)) *
                   (m.Vat / 100))).FirstOrDefault();
            return Math.Round(total, 2);
        }
        public IQueryable<InvoiceDetailsViewModel> GetAllInvoiceDeatilsViewModelByInvoiceMaster(int? InvoiceMasterID, string accLanguage)
        {
            var data = Uow.InvoiceDetails.GetAll(x => !x.IsDeleted && x.InvoiceMasterID == InvoiceMasterID).OrderByDescending(a => a.CreateDate).Select(m => new InvoiceDetailsViewModel()
            {

                CreateDate = m.CreateDate.ToString("dd/MM/yyyy hh:mm tt"),
                FinishDate = m.OrderVendor.OrderHistory.FirstOrDefault(s=>s.OrderStatusID == (int)OrderStatusEnum.Delivered).CreateDate.ToString("dd/MM/yyyy hh:mm tt"),
                VendorsID = m.InvoiceMaster.VendorsID,
                InvoiceMasterID = m.InvoiceMasterID,
                InvoiceDetailsID = m.InvoiceDetailsID,
                InvoiceNo = m.InvoiceMaster.InvoiceNo,
                InvoiceTypeId = m.InvoiceMaster.InvoiceTypeId,
                OrderVendorID = m.OrderVendorID,
                CustomerName = m.OrderVendor.Orders.Customers.FirstName,
                CustomerMobile = m.OrderVendor.Orders.Customers.MobileNo,
                InvoiceDetailsGuid = m.InvoiceDetailsGuid,
                VendorName = accLanguage == "ar" ? m.InvoiceMaster.Vendors.FirstNameAr : m.InvoiceMaster.Vendors.FirstNameEn,
                DebitAmount = m.Total,
            });
            return data;
        }
        #endregion
        #region Actions
        public StoredResultViewModel GenerateInvoice(TripItemsProc items, int UserId, int VendorID, decimal discount, decimal vat, string storedName, string connectionString,decimal total,decimal Amount)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(storedName, sqlConnection);
            cmd.Parameters.Add("@Items", SqlDbType.Structured).Value = items.GetItemsAsDataTable();
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@UserId", Value = UserId });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@VendorID", Value = VendorID });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Discount", Value = discount });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Vat", Value = vat });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@OrderInvoiceTypeId", Value = (int)OrderInvoiceTypeEnum.Invoiced });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@InvoiceTypeId", Value = (int)InvoiceTypeEnum.Pending_Operation });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@DateNow", Value = _bLGeneral.GetDateTimeNow() });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@AmountIN", Value = Amount });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Total", Value = total });
            sqlConnection.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader ResultsMeeting = cmd.ExecuteReader();
            DataTable dtMeeting = new DataTable();
            dtMeeting.Load(ResultsMeeting);
            StoredResultViewModel _res = new StoredResultViewModel();
            if (dtMeeting.Rows.Count > 0)
            {
                _res.RefID = Convert.ToInt32(dtMeeting.Rows[0]["RefID"]);
                _res.flag = Convert.ToString(dtMeeting.Rows[0]["flag"]);
            }
            if (_res.flag == "102")
            {
                var rr = Convert.ToString(dtMeeting.Rows[0]["ErrorNumber"]);
                var rrr = Convert.ToString(dtMeeting.Rows[0]["ErrorSeverity"]);
                var rrrs = Convert.ToString(dtMeeting.Rows[0]["ErrorLine"]);
                var rrrrr = Convert.ToString(dtMeeting.Rows[0]["ErrorMessage"]);

            }
            sqlConnection.Close();
            return _res;
        }
        public StoredResultViewModel GenerateInvoiceBulk(TripItemsProc items, VendorItemsProc Vendoritems, int UserId, decimal discount, decimal vat, string storedName, string connectionString)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(storedName, sqlConnection);
            cmd.Parameters.Add("@Items", SqlDbType.Structured).Value = items.GetItemsAsDataTable();
            cmd.Parameters.Add("@VendorItems", SqlDbType.Structured).Value = Vendoritems.GetItemsAsDataTable();
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@UserId", Value = UserId });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Discount", Value = discount });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@Vat", Value = vat });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@OrderInvoiceTypeId", Value = (int)OrderInvoiceTypeEnum.Invoiced });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@InvoiceTypeId", Value = (int)InvoiceTypeEnum.Pending_Operation });
            cmd.Parameters.Add(new SqlParameter() { ParameterName = "@DateNow", Value = _bLGeneral.GetDateTimeNow() });
            sqlConnection.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader ResultsMeeting = cmd.ExecuteReader();
            DataTable dtMeeting = new DataTable();
            dtMeeting.Load(ResultsMeeting);
            StoredResultViewModel _res = new StoredResultViewModel();
            if (dtMeeting.Rows.Count > 0)
            {
                _res.RefID = Convert.ToInt32(dtMeeting.Rows[0]["RefID"]);
                _res.flag = Convert.ToString(dtMeeting.Rows[0]["flag"]);
            }
            if (_res.flag == "102")
            {
                var rr = Convert.ToString(dtMeeting.Rows[0]["ErrorNumber"]);
                var rrr = Convert.ToString(dtMeeting.Rows[0]["ErrorSeverity"]);
                var rrrs = Convert.ToString(dtMeeting.Rows[0]["ErrorLine"]);
                var rrrrr = Convert.ToString(dtMeeting.Rows[0]["ErrorMessage"]);

            }
            sqlConnection.Close();
            return _res;
        }
        public bool ChangeInvoiceType(int InvoiceMasterID, int userId, int InvoiceTypeId)
        {
            var model = GetInvoiceMasterById(InvoiceMasterID);
            if (model != null)
            {
                model.UserUpdate = userId;
                model.InvoiceTypeId = InvoiceTypeId;
                model.UpdateDate = _bLGeneral.GetDateTimeNow();
                model = Uow.InvoiceMaster.Update(model);
                InvoiceHistory history = new InvoiceHistory()
                {
                    InvoiceMasterID = InvoiceMasterID,
                    ActionType = (int)ActionEnum.Update,
                    CreateDate = _bLGeneral.GetDateTimeNow(),
                    InvoiceTypeId = InvoiceTypeId,
                    IsDeleted = false,
                    InvoiceHistoryGuid = Guid.NewGuid(),
                    UserId = userId,
                };
                Uow.InvoiceHistory.Insert(history);
                Uow.Commit();
                return true;
            }
            return false;
        }
        [Obsolete]
        public bool CollectInvoice(int InvoiceMasterID, int userId, int InvoiceTypeId, int? BankID, string Attachment, string ReferenceNo, DateTime TransferDate)
        {
            var model = GetInvoiceMasterById(InvoiceMasterID);
            if (model != null)
            {
                var Total = GetInvoiceTotal(InvoiceMasterID);
                model.UserUpdate = userId;
                model.InvoiceTypeId = InvoiceTypeId;
                model.InvoiceStatusId = (int)InvoiceStatusEnum.Paid;
                model.UpdateDate = _bLGeneral.GetDateTimeNow();
                model = Uow.InvoiceMaster.Update(model);
                InvoiceHistory history = new InvoiceHistory()
                {
                    InvoiceMasterID = InvoiceMasterID,
                    ActionType = (int)ActionEnum.Update,
                    CreateDate = _bLGeneral.GetDateTimeNow(),
                    InvoiceTypeId = InvoiceTypeId,
                    IsDeleted = false,
                    InvoiceHistoryGuid = Guid.NewGuid(),
                    UserId = userId,
                };
                ListTransfer transfer = new ListTransfer()
                {
                    InvoiceMasterID = InvoiceMasterID,
                    CreateDate = _bLGeneral.GetDateTimeNow(),
                    IsDeleted = false,
                    ListTransferGuid = Guid.NewGuid(),
                    UserId = userId,
                    Attachment = Attachment,
                    BanksID = BankID,
                    ReferenceNo = ReferenceNo,
                    Total = Total,
                    TransferDate = TransferDate,
                };
                Uow.InvoiceHistory.Insert(history);
                Uow.ListTransfer.Insert(transfer);
                foreach (var item in model.InvoiceDetails)
                {
                    var tripdata = Uow.OrderVendor.GetAll(e => e.OrderVendorID == item.OrderVendorID).FirstOrDefault();
                    if (tripdata != null)
                    {
                        tripdata.InvoiceTypeId = (int)OrderInvoiceTypeEnum.Cash_Transfered;
                        Uow.OrderVendor.Update(tripdata);
                    }
                }
                _BlVendorBalance.InsertBalance((int)TypeOperationEnum.DR, Total, (int)TransactionTypeEnum.Pay_Invoice, model.VendorsID, userId, null, _bLGeneral.GetDateTimeNow(), "دفع فاتورة رقم " + model.InvoiceNo, "");
                return true;
            }
            return false;
        }
        #endregion
    }
}
