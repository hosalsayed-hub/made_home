using Homemade.Model.Setting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.Model.Order
{
    [Table("ShipCompanyHistory", Schema = "Order")]
    public class ShipCompanyHistory
    {
        public ShipCompanyHistory()
        {
            ShipCompanyHistoryGuid = Guid.NewGuid();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ShipCompanyHistoryID { get; set; }
        public Guid ShipCompanyHistoryGuid { get; set; }
        public string Response { get; set; }
        public int OrderVendorID { get; set; }
        public int ShippingStatusId { get; set; }
        public string ResponseStatusId { get; set; }
        public int ShippingCompanyID { get; set; }
        public string ResponseStatusName { get; set; }
        public DateTime CreateDate { get; set; }
        public string DriverName { get; set; }
        public string CancelReason { get; set; }
        public string DriverPhone { get; set; }

        public virtual OrderVendor OrderVendor { get; set; }
        public virtual ShippingCompany ShippingCompany { get; set; }
    }
}
