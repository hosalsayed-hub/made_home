using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homemade.BLL.ViewModel.MyFatoora.FatoraPayment
{
    public class DataResponce
    {
        public string InvoiceId { get; set; }
        public string PaymentId { get; set; }
        public bool IsDirectPayment { get; set; }
        public string PaymentURL { get; set; }
        public string CustomerReference { get; set; }
        public string UserDefinedField { get; set; }
    }

    public class ExecutePaymentResponce
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<ValidationError> ValidationErrors { get; set; }
        public DataResponce Data { get; set; }
    }


}
