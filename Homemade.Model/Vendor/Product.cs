using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;
using Homemade.Model.Setting;
using Homemade.Model.Customer;
using Homemade.Model.Order;
using Homemade.Model.Site;

namespace Homemade.Model.Vendor
{
    [Table("Product", Schema = "Vendor")]
    public class Product : BaseEntity
    {
        public Product()
        {
            ProductGuid = Guid.NewGuid();
            ProductQuestion = new HashSet<ProductQuestion>();
            ProductImage = new HashSet<ProductImage>();
            CustomerFavourites = new HashSet<CustomerFavourites>();
            ProdQuestion = new HashSet<ProdQuestion>();
            CartDetails = new HashSet<CartDetails>();
            OrderItems = new HashSet<OrderItems>();
            QuantitiesRequestProduct = new HashSet<QuantitiesRequestProduct>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductID { get; set; }
        public Guid ProductGuid { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string DescAr { get; set; }
        public string DescEn { get; set; }
        public string Logo { get; set; }
        public string SKU { get; set; } //رقم التخزين
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public bool IsLimited { get; set; }
        public decimal DailyQuantity { get; set; }
        public decimal Weight { get; set; }
        public string Size { get; set; }
        public string PiecesAr { get; set; }
        public string PiecesEn { get; set; }
        public decimal Discount { get; set; } //Percent
        public DateTime? StartDiscountDate { get; set; }
        public DateTime? EndDiscountDate { get; set; }
        public bool IsFixed { get; set; } //هل ثابت
        public bool IsHidden { get; set; }
        public string KeyWords { get; set; }
        public int ProductTypeId { get; set; }
        public int ProductOrder { get; set; } = 100;
        public int ProductQuantity { get; set; }
        public int ProductQuantityType { get; set; }
        public decimal TimeTakenProcess { get; set; }
        public int MeasurementId { get; set; }   // MeasurementEnum
        #region FK
        public int VendorsID { get; set; } //fk
        public int DepartmentsID { get; set; } //fk
        public bool IsFavourite { get; set; }
        public virtual User User { get; set; }
        public virtual Departments Departments { get; set; }
        public virtual Vendors Vendors { get; set; }
        public virtual ICollection<CustomerFavourites> CustomerFavourites { get; set; }
        public virtual ICollection<ProductImage> ProductImage { get; set; }
        public virtual ICollection<ProductQuestion> ProductQuestion { get; set; }
        public virtual ICollection<ProdQuestion> ProdQuestion { get; set; }
        public virtual ICollection<CartDetails> CartDetails { get; set; }
        public virtual ICollection<OrderItems> OrderItems { get; set; }
        public virtual ICollection<QuantitiesRequestProduct> QuantitiesRequestProduct { get; set; }


        #endregion
    }
}
