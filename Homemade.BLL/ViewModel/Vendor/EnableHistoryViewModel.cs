using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homemade.Model.Vendor
{
    public class EnableHistoryViewModel : BaseEntity
    {
        public int EnableHistoryID { get; set; }
        public Guid EnableHistoryGuid { get; set; }
        public bool Status { get; set; }
        public int VendorsID { get; set; }
        public virtual User User { get; set; }
        public virtual Vendors Vendors { get; set; }
        public string VendorName { set; get; }
        public string UserName { set; get; }
        public string StatusString { set; get; }
        public string UserTypeString { set; get; }
        public string CreateDateString { get; set; }
    }
}
