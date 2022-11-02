using Homemade.Model.Customer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.Model.Order
{
    [Table("TabCharge", Schema = "Order")]
    public class TabCharge
    {
        public TabCharge()
        {
            TabChargeGuid = Guid.NewGuid();
        }
        [Key]
        public int TabChargeID { get; set; }
        public Guid TabChargeGuid { get; set; }
        public int? OrdersID { get; set; }
        public string TapChargeID { get; set; }
        public double Amount { get; set; }
        public string Status { get; set; }
        public int StatusId { get; set; }
        public string RequestCreate { get; set; }
        public string ResponseCreate { get; set; }
        public string ResponseRedirect { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime? RedirectDate { get; set; }
        public DateTime? ResponceDate { get; set; }
        public bool IsRedirect { get; set; }
        public string Message { get; set; }
        public int? CustomersID { get; set; }
        public int? CustomerBalanceID { get; set; }
        public int TabChargeEnum { get; set; }
        public virtual Orders Orders { get; set; }
        public virtual Customers Customers { get; set; }
        public virtual CustomerBalance CustomerBalance { get; set; }
    }
}
