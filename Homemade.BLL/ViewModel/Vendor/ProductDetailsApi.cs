using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.BLL.ViewModel.Vendor
{
    public class ProductDetailsApi
    {
        public List<ProdQuestions> questions { get; set; }
        public List<Banners> banners { get; set; }
        public List<KeyWords> keyWords { get; set; }
        public int productId { get;   set; }
        public string image { get;   set; }
        public string descripe { get;   set; }
        public decimal price { get;   set; }
        public decimal discount { get;   set; }
        public decimal quantity { get;   set; }
        public decimal weight { get;   set; }
        public string productName { get;   set; }
        public int vendorId { get;   set; }
        public string vendorName { get;   set; }
        public string vendorImage { get;   set; }
        public string size { get; set; }
        public string pieces { get; set; }
        public int questionsCount { get; set; }
        public decimal rate { get; set; }
        public string timeTaken { get;  set; }
        public string productTypeName { get;  set; }
        public bool isLimited { get;  set; }
        public bool isVendorWorking { get; set; }
        public string isVendorWorkingString { get; set; }
        public decimal priceBefor { get;  set; }
        public decimal discountAmount { get;  set; }
    }
    public class ProdQuestions
    {
        public string question { get; set; }
        public string answer { get; set; }
    }
    public class Banners
    {
        public string image { get; set; }
    }
    public class KeyWords
    {
        public string word { get; set; }
    }
}
