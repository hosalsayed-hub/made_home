using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.ViewModel.Driver
{
    public class DriverSTCPay
    {
        public List<DriverWithTrips> drivers { get; set; }
        public int userId { get; set; }
    }
    public class DriverWithTrips
    {
        public int driverId { get; set; }
        public List<int> trips { get; set; }
    }
    public class TranactionList
    {
        public int userId { get; set; }
        public List<int> tranactions { get; set; }
    }
}
