using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Homemade.BLL.ViewModel.Vendor
{
    public class ProductViewModel
    {
        public int ProductID { get; set; }
        public Guid ProductGuid { get; set; }
        [Required(ErrorMessageResourceName = "VendorRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public int VendorsID { get; set; }
        [Required(ErrorMessageResourceName = "ArNameRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [StringLength(50, ErrorMessageResourceName = "NameMax50", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [RegularExpression(@"^[\u0621-\u064A\u0660-\u0669\0-9]+$", ErrorMessageResourceName = "ArabicOnly", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string NameAr { get; set; }
        [StringLength(50, ErrorMessageResourceName = "NameMax50", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string NameEn { get; set; }
        [Required(ErrorMessageResourceName = "DepartmentRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public int DepartmentsID { get; set; }
        [StringLength(300, ErrorMessageResourceName = "NameMax300", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string DescAr { get; set; }
        [StringLength(300, ErrorMessageResourceName = "NameMax300", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [RegularExpression(@"^[a-zA-Z\0-9]+$", ErrorMessageResourceName = "EnglishOnly", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string DescEn { get; set; }
        public string SKU { get; set; } //رقم التخزين
        [Required(ErrorMessageResourceName = "PriceRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string Price { get; set; }
        public string GrossPrice { get; set; }
        public string Quantity { get; set; }
        public string Weight { get; set; }
        public string Discount { get; set; } //Percent
        public string StartDiscountDate { get; set; }
        public string EndDiscountDate { get; set; }
        public bool IsFixed { get; set; } //هل ثابت
        public bool IsHidden { get; set; }
        public bool IsAvailable { get; set; }
        public string Size { get; set; }
        [StringLength(75, ErrorMessageResourceName = "NameMax75", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [RegularExpression(@"^[\u0621-\u064A\u0660-\u0669\0-9]+$", ErrorMessageResourceName = "ArabicOnly", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string PiecesAr { get; set; }
        [StringLength(75, ErrorMessageResourceName = "NameMax75", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        [RegularExpression(@"^[a-zA-Z\0-9]+$", ErrorMessageResourceName = "EnglishOnly", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string PiecesEn { get; set; }
        public List<int> KeyWords { get; set; }
        public string DepartmentName { get; set; }
        public string Name { get; set; }
        public string KeyWordsListString { get; set; }
        public string IsAvailableString { get; set; }
        public string IsHiddenString { get; set; }
        public string IsFixedString { get; set; }
        public string KeyWordsString { get; set; }
        public string Logo { get; set; }
        public string VendorsName { get; set; }
        public string Pieces { get; set; }
        [Required(ErrorMessageResourceName = "ProductTypeRequierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public int ProductTypeId { get; set; }
        public int? ProductOrder { get; set; }
        public int ProductQuantity { get; set; }
        public int ProductQuantityType { get; set; }
        public string ProductQuantityTypeName { get; set; }
        public string ProductTypeName { get; set; }
        public string TimeTakenProcess { get; set; }
        public string TimeTakenProcessHours { get; set; }
        public bool IsLimited { get; set; } = true;
        public int MeasurementId { get; set; }   // MeasurementEnum
    }
    public class ProductQuestionViewModel
    {
        public int ProductQuestionID { get; set; }
        public Guid ProductQuestionGuid { get; set; }
        [Required(ErrorMessageResourceName = "Requierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string QuestionAr { get; set; }
        [Required(ErrorMessageResourceName = "Requierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string QuestionEn { get; set; }
        [Required(ErrorMessageResourceName = "Requierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string AnswerAr { get; set; }
        [Required(ErrorMessageResourceName = "Requierd", ErrorMessageResourceType = typeof(Resources.HomemadeErrorMessages))]
        public string AnswerEn { get; set; }
        public int ProductID { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsEnable { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public DateTime? EnableDate { get; set; }
        public int UserId { get; set; }
        public int? UserUpdate { get; set; }
        public int? UserDelete { get; set; }
        public int? UserEnable { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }
    public class ProductImageViewModel
    {
        public int ProductImageID { get; set; }
        public Guid ProductImageGuid { get; set; }
        public string Image { get; set; }
        public string DescAr { get; set; }
        public string DescEn { get; set; }
    }
}
