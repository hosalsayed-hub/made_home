using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.ViewModel.Driver.STC
{
    public class STCPayment
    {
        public PaymentOrderRequestMessage PaymentOrderRequestMessage { get; set; }
    }
    public class PaymentOrderRequestMessage
    {
        public string CustomerReference { get; set; }
        public string Description { get; set; }
        public string ValueDate { get; set; }
        public List<Payments> Payments { get; set; }
    }
    public class Payments
    {
        public string MobileNumber { get; set; }
        public string ItemReference { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
    }
    public class InquirySTC
    {
        public PaymentOrderInquiryRequestMessage PaymentOrderInquiryRequestMessage { get; set; }
    }
    public class PaymentOrderInquiryRequestMessage
    {
        public string CustomerReference { get; set; }
        public string PaymentReference { get; set; }
        public string ItemReference { get; set; }
    }
    public class STCResultMessage
    {
        public string Content { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public int PaidStatus { get; set; }
    }
    public class STCRes
    {
        public PaymentOrderResponseMessage PaymentOrderResponseMessage { get; set; }
    }
    public class PaymentOrderResponseMessage
    {
        public string PaymentOrderReference { get; set; }
    }
    public class STCInquiryRes
    {
        public PaymentOrderInquiryResponseMessage PaymentOrderInquiryResponseMessage { get; set; }
        public PaymentOrderCancelResponseMessage PaymentOrderCancelResponseMessage { get; set; }
    }
    public class PaymentOrderInquiryResponseMessage
    {
        public List<Payments> Payments { get; set; }
    }
    public class CancelStc
    {
        public PaymentOrderCancelRequestMessage PaymentOrderCancelRequestMessage { get; set; }
    }
    public class PaymentOrderCancelRequestMessage
    {
        public string PaymentOrderReference { get; set; }
        public string CustomerReference { get; set; }
        public List<Payments> Payments { get; set; }

    }
    public class PaymentOrderCancelResponseMessage
    {
        public List<Payments> Payments { get; set; }
    }
    #region DirectClasses
    public class DirectPayment
    {
        public DirectPaymentAuthorize DirectPaymentAuthorizeV4RequestMessage { get; set; }
    }
    public class DirectPaymentPaid
    {
        public DirectPaymentPay DirectPaymentConfirmV4RequestMessage { get; set; }
    }
    public class DirectPaymentAuthorize
    {
        public string BranchID { get; set; }
        public string TellerID { get; set; }
        public string DeviceID { get; set; }
        public string RefNum { get; set; }
        public string BillNumber { get; set; }
        public string MobileNo { get; set; }
        public string Amount { get; set; }
        public string MerchantNote { get; set; }
    }
    public class DirectPaymentPay
    {
        public string OtpReference { get; set; }
        public string OtpValue { get; set; }
        public string STCPayPmtReference { get; set; }
        public string TokenRefrence { get; set; }
        public bool TokenizeYn { get; set; }
    }
    public class DirectPaymentAuthorizeV4ResponseMessage
    {
        public string OtpReference { get; set; }
        public string STCPayPmtReference { get; set; }
        public int ExpiryDuration { get; set; }
        public DateTime? DateExpiry { get; set; }

    }
    public class DirectPaymentResponse
    {
        public DirectPaymentAuthorizeV4ResponseMessage DirectPaymentAuthorizeV4ResponseMessage { get; set; }
    }
    public class DirectPaymentResponseError
    {
        public int Code { get; set; }
        public string Text { get; set; }
        public int Type { get; set; }
    }
    //public class DirectPaymentResponseError
    //{
    //    public string Code { get; set; }
    //    public string STCPayPmtReference { get; set; }
    //    public int ExpiryDuration { get; set; }
    //}
    public class DirectPaymentPaidRef
    {
        public DirectPaymentConfirmV4ResponseMessage DirectPaymentConfirmV4ResponseMessage { get; set; }
    }

    public class STCError
    {
        public string Code { get; set; }
        public string Text { get; set; }
        public string Type { get; set; }
    }
    public class DirectPaymentConfirmV4ResponseMessage
    {
        public string BranchID { get; set; }
        public string TellerID { get; set; }
        public string DeviceID { get; set; }
        public string RefNum { get; set; }
        public string BillNumber { get; set; }
        public string PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public int PaymentStatus { get; set; }
        public string PaymentStatusDesc { get; set; }
    }
    #endregion
}
