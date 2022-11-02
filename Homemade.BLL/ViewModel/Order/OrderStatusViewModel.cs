using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Homemade.Model.Order;

namespace Homemade.Model.Order
{
    public class OrderStatusViewModel : BaseEntity
    {
        public int OrderStatusID { get; set; }
        public Guid OrderStatusGuid { get; set; }
        public string NameAR { get; set; }
        public string NameEN { get; set; }
        public string DescAr { get; set; }
        public string DescEN { get; set; }
        public string OrderStatusName { get;   set; }
    }
}
