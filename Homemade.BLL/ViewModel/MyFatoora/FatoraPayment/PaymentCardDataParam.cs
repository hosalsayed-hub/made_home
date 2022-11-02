using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homemade.BLL.ViewModel.MyFatoora.FatoraPayment
{
    public class PaymentCardDataParam
    {
        public carddata card { get; set; }
    }
    public class carddata
    {
        public string id { get; set; }
        public string Number { get; set; }
        public string expiryMonth { get; set; }
        public string expiryYear { get; set; }
        public string securityCode { get; set; }
        public string paymentMethod { get; set; }
        public string isdirect { get; set; }
    }
}
