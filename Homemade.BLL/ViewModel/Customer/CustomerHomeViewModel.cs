using Homemade.BLL.ViewModel.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.ViewModel.Customer
{
    public class CustomerHomeViewModelSite
    {
        public List<DeptViewModelApi> departments { get; set; }
        public List<BannerList> banners { get; set; }
        public List<AdvertisingList> advertisings { get; set; }
        public ProdResonseSite productList { get; set; }
    }
    public class CustomerHomeViewModel
    {
        public List<DeptViewModelApi> departments { get; set; }
        public List<BannerList> banners { get; set; }
        public List<AdvertisingList> advertisings { get; set; }
        public ProdResonse productList { get; set; }
    }
    public class ProdResonseSite
    {
        public bool isNextPage { get; set; }
        public List<ProductListSite> products { get; set; }
    }
    public class ProductListSite
    {
        public string image { get; set; }
        public string productName { get; set; }
        public string vendorName { get; set; }
        public int vendorId { get; set; }
        public int productId { get; set; }
        public decimal price { get; set; }
        public string descripe { get; set; }
        public bool isHidden { get; set; }
        public bool isFixed { get; set; }
        public string vendorLogo { get; set; }
        public decimal weight { get; set; }
        public Guid VendorsGuid { get;  set; }
        public Guid ProductGuid { get;  set; }
        public string Pieces { get;  set; }
        public decimal Discount { get;  set; }
        public bool IsLimited { get; set; }
        public decimal Quantity { get; set; }
        public string VendorsDaysWork { get; set; }
        public DateTime? VendorsWorkFrom { get; set; }
        public DateTime? VendorsWorkTo { get; set; }
        public string VendorsDaysVac { get; set; }
        public DateTime? VendorsVacWorkFrom { get; set; }
        public DateTime? VendorsVacWorkTo { get; set; }
    }
    public class BannerList
    {
        public string image { get; set; }
        public string title { get; set; }
        public int sliderId{ get; set; }
    }
    public class AdvertisingList
    {
        public string image { get; set; }
    }
    public class ProductList
    {
        public string image { get; set; }
        public string productName { get; set; }
        public string vendorName { get; set; }
        public int vendorId { get; set; }
        public int productId { get; set; }
        public decimal price { get; set; }
        public string descripe { get; set; }
        public bool isHidden { get; set; }
        public bool isFixed { get; set; }
        public string vendorLogo { get; set; }
        public decimal weight { get; set; }
        public decimal quantity { get; set; }
        public string deptName { get; set; }
        public bool isLimited { get; internal set; }
        public bool isVendorWorking { get; set; }
        public string isVendorWorkingString { get; set; }
        public decimal discount { get; internal set; }
        public decimal priceBefor { get; internal set; }
        public decimal discountAmount { get; internal set; }
    }
    public class ProdResonse
    {
        public bool isNextPage { get; set; }
        public List<ProductList> products { get; set; }
    }
    public class AllProductViewModel
    {
        public List<ProductList> listPopular { get; set; }
        public List<ProductList> listProducts { get; set; }
        public bool isNextlistPopular { get; set; }
        public bool isNextlistProducts { get; set; }
    }
}
