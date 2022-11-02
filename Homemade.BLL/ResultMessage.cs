using System;
using System.Linq;

namespace Homemade.BLL
{
    public class ResultMessage
    {
        /// <summary>
        /// This Value Must Be On Of This Values (success-error-info-warning) To pass this value to toastr as Toastr Type
        /// </summary>
        public string ResultType { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; }
        public string ObjResult { get;  set; }
        public bool? IsSendEmail { get; set; }
        public int? ID { get; set; }
    }
}
