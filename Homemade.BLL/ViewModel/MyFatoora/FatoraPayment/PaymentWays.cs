using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homemade.BLL.ViewModel.MyFatoora.FatoraPayment
{
    public class PaymentMethod
    {
        public int PaymentMethodId { get; set; }
        public string PaymentMethodAr { get; set; }
        public string PaymentMethodEn { get; set; }
        public string PaymentMethodCode { get; set; }
        public bool IsDirectPayment { get; set; }
        public double ServiceCharge { get; set; }
        public double TotalAmount { get; set; }
        public string CurrencyIso { get; set; }
        public string ImageUrl { get; set; }
    }

    public class DataPaymentWays
    {
        public List<PaymentMethod> PaymentMethods { get; set; }
    }

    public class PaymentWays
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<ValidationError> ValidationErrors { get; set; }
        public DataPaymentWays Data { get; set; }
    }

}
