using System;
using System.Collections.Generic;
using System.Text;

namespace Homemade.BLL.ViewModel.MyFatoora.FatoraPayment
{
    public class PayMyfatora
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public DataResponce Data { get; set; }
        public List<ValidationError> ValidationErrors { get; set; }
    }
}
