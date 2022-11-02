using System;
using System.Collections.Generic;
using System.Text;

namespace Homemade.Api.Model
{
    public class FixedResultMessage
    {
        public string message { get; set; }
        public bool status { get; set; }
        public bool isAuthorize { get; set; }
        public object data { get; set; }
    }
}
