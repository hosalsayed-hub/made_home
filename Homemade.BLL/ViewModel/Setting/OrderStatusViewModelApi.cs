using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.ViewModel.Setting
{
    public class OrderStatusViewModelApi
    {
        public int orderStatusID { get;  set; }
        public string name { get;  set; }
        public int? Arrange { get;   set; }
        public int? OrderStatusType { get;   set; }
    }
}
