using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Homemade.BLL.ViewModel.Vendor
{
  
    public class AddProductViewModel
    {
        public string nameAr { get; set; }
        public string nameEn { get; set; }
        public int departmentsID { get; set; } //fk
        public string descAr { get; set; }
        public string descEn { get; set; }
        public string sKU { get; set; } //رقم التخزين
        public decimal price { get; set; }
        public decimal quantity { get; set; }
        public decimal weight { get; set; }
        public decimal? discount { get; set; } //Percent
        public DateTime? startDiscountDate { get; set; }
        public DateTime? endDiscountDate { get; set; }
        public bool isFixed { get; set; } //هل ثابت
        public bool isHidden { get; set; }
        public bool isAvailable { get; set; }
        public bool? islimited { get; set; }
        public List<ProductQuestionList> productQuestion { get; set; }
        public string size { get; set; }
        public string piecesar { get; set; }
        public string piecesen { get; set; }
        public List<int> keyWords { get; set; }
        public string timeTaken { get; set; }
        public int productType { get; set; }
        public int? measurementId { get; set; }
    }
    public class ProductQuestionList
    {
        public string questionAr { get; set; }
        public string questionEn { get; set; }
        public string answerAr { get; set; }
        public string answerEn { get; set; }
    }
    public class UpdateProductViewModel : AddProductViewModel
    {
        public int productId { get; set; }
        public List<ProductImages> newProductImages { get; set; }
        public string logo { get;  set; }
        public string productTypeName { get; set; }
        public string measurementName { get;  set; }
        public string pieces { get; set; }
        public decimal priceBefor { get; set; }
        public decimal discountAmount { get; set; }
    }
    public class ProductImages
    {
        public int imageId { get; set; }
        public string imagelink { get; set; }
    }
}
