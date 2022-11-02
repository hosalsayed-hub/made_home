using Homemade.BLL.ViewModel.MyFatoora.FatoraPayment;
 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homemade.BLL.ViewModel.MyFatoora
{
    public class StatusDesc
    {
        public int Orderid { get; set; }
        public string statusdescription { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
    }
}
