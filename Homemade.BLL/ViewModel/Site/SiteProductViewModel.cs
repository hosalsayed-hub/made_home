using Homemade.BLL.ViewModel.Order;
using Homemade.BLL.ViewModel.Vendor;
using System;
using System.Collections.Generic;

namespace Homemade.BLL.ViewModel.Site
{
    public class SiteProductViewModel
    {
        public int ProductID { get; set; }
        public Guid ProductGuid { get; set; }
        public int VendorsID { get; set; }
        public string ProductName { get; set; }
        public int DepartmentsID { get; set; }
        public string ProductDesc { get; set; }
        public decimal ProductPrice { get; set; }
        public string DepartmentName { get; set; }
        public string ProductLogo { get; set; }
        public string VendorsLogo { get; set; }
        public string VendorsName { get; set; }
        public string Pieces { get;  set; }
        public Guid VendorsGuid { get;  set; }
        public bool IsFixed { get;  set; }
        public bool IsHidden { get;  set; }
        public bool IsLimited { get; set; }
        public decimal Quantity { get; set; }
        public string VendorsDaysWork { get; set; }
        public DateTime? VendorsWorkFrom { get; set; }
        public DateTime? VendorsWorkTo { get; set; }
        public string VendorsDaysVac { get; set; }
        public DateTime? VendorsVacWorkFrom { get; set; }
        public DateTime? VendorsVacWorkTo { get; set; }
    }
    public class SiteProductDetailsViewModel
    {
        public int ProductID { get; set; }
        public Guid ProductGuid { get; set; }
        public Guid? DepartmentsGuid { get; set; }
        public int VendorsID { get; set; }
        public string ProductName { get; set; }
        public int DepartmentsID { get; set; }
        public string ProductDesc { get; set; }
        public decimal ProductPrice { get; set; }
        public string DepartmentName { get; set; }
        public string ProductLogo { get; set; }
        public string VendorsLogo { get; set; }
        public string VendorsName { get; set; }
        public IEnumerable<ProductImageViewModel> ProductImages { get; set; }
        public string KeyWords { get; set; }
        public List<string> ListKeyWordsString { get; set; }
        public IEnumerable<ProductQuestionViewModel> ProductQuestions { get; set; }
        public IEnumerable<ProdQuestionViewModel> ProdQuestion { get; set; }
        public IEnumerable<SiteProductViewModel> AllOtherProduct { get; set; }
        public Guid VendorsGuid { get; internal set; }
        public bool? IsFavorites { get; set; }
        public string TimeTakenProcess { get; set; }
        public decimal TimeTakenProcessHours { get; set; }
        public decimal Weight { get; set; }
        public string ProductPieces { get; set; }
        public bool IsLimited { get; set; }
        public decimal Quantity { get; set; }
        public string MeasurementName { get; set; }
        public string VendorsDaysWork { get; set; }
        public DateTime? VendorsWorkFrom { get; set; }
        public DateTime? VendorsWorkTo { get; set; }
        public string VendorsDaysVac { get; set; }
        public DateTime? VendorsVacWorkFrom { get; set; }
        public DateTime? VendorsVacWorkTo { get; set; }
    }
    public class SiteProductPriceViewModel
    {
        public decimal MinProductPrice { get; set; }
        public decimal MaxProductPrice { get; set; }
    }
}
