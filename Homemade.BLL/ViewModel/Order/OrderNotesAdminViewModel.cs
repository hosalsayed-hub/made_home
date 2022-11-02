using Homemade.Model;
using System;

namespace Homemade.BLL.ViewModel.Order
{
    public class OrderNotesAdminViewModel : BaseEntity
    {
        public int OrderNotesAdminID { get; set; }
        public Guid OrderNotesAdminGuid { get; set; }
        public string Notes { get; set; }
        public int OrderVendorID { get; set; }
        public string CreateDateString { get; set; }
        public string UserName { get; set; }
    }


}
