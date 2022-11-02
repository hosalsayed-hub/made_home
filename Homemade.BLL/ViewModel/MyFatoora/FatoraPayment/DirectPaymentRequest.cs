using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homemade.BLL.ViewModel.MyFatoora.FatoraPayment
{
     

    public class Card
    {
        public string Number { get; set; }
        public string expiryMonth { get; set; }
        public string expiryYear { get; set; }
        public string securityCode { get; set; }
        public string PaymentMethodId { get; set; }
         
    }
    public class DirectPaymentRequest
    {
        public string paymentType { get; set; }
        public Card card { get; set; }
        public bool saveToken { get; set; }
        public bool bypass3ds { get; set; }
    }
    public class InvoiceTransactionT
    {
        public string authorizationId { get; set; }
        public string currency { get; set; }
        public string customerServiceCharge { get; set; }
        public string dueValue { get; set; }
        public string paidCurrency { get; set; }
        public string paidCurrencyValue { get; set; }
        public string paymentGateway { get; set; }
        public string paymentId { get; set; }
        public string referenceId { get; set; }
        public string trackId { get; set; }
        public DateTime transactionDate { get; set; }
        public string transactionId { get; set; }
        public string transactionStatus { get; set; }
        public string transationValue { get; set; }
    }

    public class Data
    {
        public DateTime createdDate { get; set; }
        public string customerMobile { get; set; }
        public string customerName { get; set; }
        public string customerReference { get; set; }
        public string expiryDate { get; set; }
        public string invoiceDisplayValue { get; set; }
        public int invoiceId { get; set; }
        public List<object> invoiceItems { get; set; }
        public string invoiceReference { get; set; }
        public string invoiceStatus { get; set; }
        public List<InvoiceTransactionT> invoiceTransactions { get; set; }
        public double invoiceValue { get; set; }
        public List<object> suppliers { get; set; }
    }

    public class Root
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public Data Data { get; set; }
    }
    public class RooterrorMobile
    {
        public string statusCode { get; set; }
        public string message { get; set; }
        public string invoiceId { get; set; }
        public string customerReference { get; set; }
    }
    public class RooterrorMobileWallet
    {
        public string statusCode { get; set; }
        public string message { get; set; }
        public string invoiceId { get; set; }
        public string customerReference { get; set; }
        public string amount { get; set; }
    }

    /// error Class
    public class DataError
    {
        public string customerReference { get; set; }
        public int invoiceId { get; set; }
    }

    public class RootError
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public DataError Data { get; set; }
    }

}
