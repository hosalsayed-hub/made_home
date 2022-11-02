using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.ViewModel.Order
{
    public class Coordinates
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    }

    public class BarqCancelResponse
    {
        public int code { get; set; }
        public string response_id { get; set; }
        public string courier { get; set; }
        public string mobile { get; set; }
        public string tracking_no { get; set; }
        public Coordinates coordinates { get; set; }
    }
}
