using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homemade.BLL.ViewModel.MyFatoora.FatoraPayment
{
    public class InvoiveDataResponce
    {
        public int InvoiceId { get; set; }
        public bool IsDirectPayment { get; set; }
        public string InvoiceURL { get; set; }
        public string CustomerReference { get; set; }
        public string UserDefinedField { get; set; }
    }

    public class InvoiceResponce
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<ValidationError> ValidationErrors { get; set; }
        public InvoiveDataResponce Data { get; set; }
    }


}
