using System.ComponentModel.DataAnnotations;

namespace Homemade.BLL.ViewModel.Site
{
    public class SiteCustomerLocationViewModel
    {
        public int CustomerLocationID { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string Address { get; set; }
        public string BuildingNumber { get; set; }
        public string StreetNo { get; set; }
        public string UniqueSign { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public bool IsVerfiy { get; set; }
        public int CustomersID { get; set; }
        [Required(ErrorMessageResourceName = "BlockRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public int BlockID { get; set; }
        [Required(ErrorMessageResourceName = "AddressTypesRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public int AddressTypesID { get; set; }
        public string CityName { get; set; }
        public string BlockName { get; set; }
        public string AddressTypesName { get; set; }
        [Required(ErrorMessageResourceName = "RegionRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public int RegionID { get; set; }
        [Required(ErrorMessageResourceName = "CityRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public int CityID { get; set; }

        public string LatStr { get; set; }
        public string LngStr { get; set; }
        public string RegionName { get; set; }
    }
    public class SearchProductsViewModel
    {
        public int? SearchDepartmentID { get; set; }
        public string SearchProducts { get; set; }
    }

}
