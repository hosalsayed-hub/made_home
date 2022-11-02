using Homemade.Model.Customer;
using Homemade.Model.Driver;
using Homemade.Model.Setting;
using Homemade.Model.Vendor;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homemade.Model.Order
{
    [Table("OrderVendor", Schema = "Order")]
    public class OrderVendor : BaseEntity
    {
        public OrderVendor()
        {
            OrderVendorGuid = Guid.NewGuid();
            OrderItems = new HashSet<OrderItems>();
            OrderHistory = new HashSet<OrderHistory>();
            OrderRate = new HashSet<OrderRate>();
            InvoiceDetails = new HashSet<InvoiceDetails>();
            Notification = new HashSet<Notification>();
            TranLogSTCPay = new HashSet<TranLogSTCPay>();
            DriverBlance = new HashSet<DriverBlance>();
            DriverSupport = new HashSet<DriverSupport>();
            VendorSupport = new HashSet<VendorSupport>();
            CustomerBalance = new HashSet<CustomerBalance>();
            VendorBalance = new HashSet<VendorBalance>();
            ShipCompanyHistory = new HashSet<ShipCompanyHistory>();
            OrderNotesAdmin = new HashSet<OrderNotesAdmin>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderVendorID { get; set; }
        public Guid OrderVendorGuid { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal DeliveryPrice { get; set; }
        public decimal Total { get; set; }
        public string TrackNo { get; set; }
        public string InvoiceImage { get; set; }
        public string Notes { get; set; }
        public int InvoiceTypeId { get; set; }
        public int CaptainTypeId { get; set; }
        public decimal PerHome { get; set; }
        public decimal PerStore { get; set; }
        public decimal PackageAmount { get; set; }
        public decimal PackagePercent { get; set; }
        #region Relations
        
        
        public decimal DeliveryVatValue { get; set; }
        public decimal VatPercent { get; set; }


        public int OrdersID { get; set; }
        public int VendorsID { get; set; }
        public int OrderStatusID { get; set; }
        public int CaptainPaidID { get; set; }
        public decimal DriverCharge { get; set; }
        public int? DriversID { get; set; }
        public int? ShippingCompanyID { get; set; }
        public int? PackageID { get; set; }

        public decimal? DistanceKM { get; set; }
        public decimal ShippingCompanyPrice { get; set; }

        public string AddedFrom { get; set; } // MOB , WEB
        public int OrderTypeId { get; set; } = 0; // OrderTypeEnum
        public DateTime? ScheduleDate { get; set; } // OrderTypeEnum
        public DateTime? ScheduleTime { get; set; } // OrderTypeEnum
        public string CardType { get; set; }
        public int ApprovalQuantity { get; set; }
        public bool IsIncreaseQuantity { get; set; }
        public int CancelTypeID { get; set; }
        public virtual Package Package { get; set; }
        public virtual ShippingCompany ShippingCompany { get; set; }
        public virtual Drivers Drivers { get; set; }
        public virtual OrderStatus OrderStatus { get; set; }
        public virtual Vendors Vendors { get; set; }
        public virtual Orders Orders { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<ShipCompanyHistory> ShipCompanyHistory { get; set; }
        public virtual ICollection<Notification> Notification { get; set; }
        public virtual ICollection<OrderItems> OrderItems { get; set; }
        public virtual ICollection<CustomerBalance> CustomerBalance { get; set; }
        public virtual ICollection<OrderHistory> OrderHistory { get; set; }
        public virtual ICollection<OrderRate> OrderRate { get; set; }
        public virtual ICollection<InvoiceDetails> InvoiceDetails { get; set; }
        public virtual ICollection<TranLogSTCPay> TranLogSTCPay { get; set; }
        public virtual ICollection<DriverBlance> DriverBlance { get; set; }
        public virtual ICollection<DriverSupport> DriverSupport { get; set; }
        public virtual ICollection<VendorSupport> VendorSupport { get; set; }
        public virtual ICollection<VendorBalance> VendorBalance { get; set; }
        public virtual ICollection<OrderNotesAdmin> OrderNotesAdmin { get; set; }

        #endregion
        public string IntegrationOrderId { get; set; }
        public decimal VatProduct { get; set; }
        public decimal VatValue { get; set; }
        public decimal TotalBaseItems { get; set; }
    }
}
