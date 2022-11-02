using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homemade.BLL.ViewModel.MyFatoora.FatoraPayment
{


    public class DataStr
    {
        public string Status { get; set; }
        public object ErrorMessage { get; set; }
        public string PaymentId { get; set; }
        public string Token { get; set; }
        public string RecurringId { get; set; }
        public string PaymentURL { get; set; }
        public object CardInfo { get; set; }
    }

    public class DirectPaymentResponce
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<ValidationError> ValidationErrors { get; set; }
        public DataStr Data { get; set; }
    }
}
