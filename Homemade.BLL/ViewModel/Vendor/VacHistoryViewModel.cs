using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Homemade.Model;

namespace Homemade.BLL.ViewModel.Vendor
{
    public class VacHistoryViewModel : BaseEntity
    {
        public int VacHistoryID { get; set; }
        public Guid VacHistoryGuid { get; set; }
        public DateTime VacFrom { get; set; }
        public DateTime VacTo { get; set; }
        public bool IsReturn { get; set; }
        public int VendorsID { get; set; }
        public string VendorName { set; get; }
        public string IsReturnString { set; get; }
    }
}
