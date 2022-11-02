using System;

namespace Homemade.BLL.ViewModel.Site
{
    public class SiteVendorsViewModel
    {
        public int VendorsID { get; set; }
        public string VendorsLogo { get; set; }
        public string VendorsName { get; set; }
        public Guid VendorsGuid { get; set; }
        public string WorkingTimes { get; set; }
        public string VendorsDaysWork { get; set; }
        public DateTime? VendorsWorkFrom { get; set; }
        public DateTime? VendorsWorkTo { get; set; }
        public string VendorsDaysVac { get; set; }
        public DateTime? VendorsVacWorkFrom { get; set; }
        public DateTime? VendorsVacWorkTo { get; set; }
    }
}
