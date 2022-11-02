using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homemade.BLL.ViewModel.MyFatoora
{
    public class ResponceErrorPayTap
    {
        public List<errors> errors { get; set; }
        public string http_code { get; set; }
        public string tripid { get; set; }

    }
    public class errors
    {
        public string code { get; set; }
        public string description { get; set; }

    }
}
