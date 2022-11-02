using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using Homemade.Model.Order;

namespace Homemade.Model.Setting
{
    [Table("ShippingCompany", Schema = "Setting")]
    public class ShippingCompany : BaseEntity
    {
        public ShippingCompany()
        {
            ShippingCompanyGuid = Guid.NewGuid();
            OrderVendor = new HashSet<OrderVendor>();
            StatusCompany = new HashSet<StatusCompany>();
            ShippingCompanyBlocks = new HashSet<ShippingCompanyBlocks>();
            ShipCompanyHistory = new HashSet<ShipCompanyHistory>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ShippingCompanyID { get; set; }
        public Guid ShippingCompanyGuid { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string PhoneNo { get; set; }
        public string Notes { get; set; }
        public string Address { get; set; }
        public string Fax { get; set; }
        public string Logo { get; set; }
        public decimal MaxKM { get; set; }
        public decimal DeliveryPrice { get; set; }
        public int CityID { get; set; }
        public int ShippingEnum { get; set; }
        public bool IsAuto { get; set; }
        public bool IsBlockCovered { get; set; }
        public virtual User User { get; set; }
        public virtual City City { get; set; }
        public virtual ICollection<ShipCompanyHistory> ShipCompanyHistory { get; set; }
        public virtual ICollection<OrderVendor> OrderVendor { get; set; }
        public virtual ICollection<StatusCompany> StatusCompany { get; set; }
        public virtual ICollection<ShippingCompanyBlocks> ShippingCompanyBlocks { get; set; }

    }
}
