using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homemade.BLL.ViewModel.MyFatoora.FatoraPayment
{
    public class SendPaymentRequest
    {
        public string orderId { get; set; }
        public string paymentMethod { get; set; }

    }
}
