using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homemade.BLL.ViewModel.MyFatoora.FatoraPayment
{
    public class PaymentStatusInvoiceItem
    {
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public float UnitPrice { get; set; }
    }

    public class InvoiceTransaction
    {
        public DateTime TransactionDate { get; set; }
        public string PaymentGateway { get; set; }
        public string ReferenceId { get; set; }
        public string TrackId { get; set; }
        public string TransactionId { get; set; }
        public string PaymentId { get; set; }
        public string AuthorizationId { get; set; }
        public string TransactionStatus { get; set; }
        public string TransationValue { get; set; }
        public string CustomerServiceCharge { get; set; }
        public string DueValue { get; set; }
        public string PaidCurrency { get; set; }
        public string PaidCurrencyValue { get; set; }
        public object Currency { get; set; }
        public object Error { get; set; }
    }

    public class PaymentStatusData
    {
        public int InvoiceId { get; set; }
        public string InvoiceStatus { get; set; }
        public string InvoiceReference { get; set; }
        public string CustomerReference { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ExpiryDate { get; set; }
        public float InvoiceValue { get; set; }
        public string Comments { get; set; }
        public string CustomerName { get; set; }
        public string CustomerMobile { get; set; }
        public string CustomerEmail { get; set; }
        public string UserDefinedField { get; set; }
        public string InvoiceDisplayValue { get; set; }
        public List<PaymentStatusInvoiceItem> InvoiceItems { get; set; }
        public List<InvoiceTransaction> InvoiceTransactions { get; set; }
    }

    public class PaymentStatus
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<ValidationError> ValidationErrors { get; set; }
        public PaymentStatusData Data { get; set; }
    }

}
