using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using Homemade.Model.Setting;
using Homemade.Model.Customer;
using Homemade.Model.Vendor;
using Homemade.Model.Order;
using Homemade.Model.Emp;
using Homemade.Model.Driver;

namespace Homemade.Model
{
    public class User : IdentityUser<int>
    {
        public User()
        {
            Country = new HashSet<Country>();
            Region = new HashSet<Region>();
            City = new HashSet<City>();
            Jobs = new HashSet<Jobs>();
            Departments = new HashSet<Departments>();
            Nationality = new HashSet<Nationality>();
            MainCategory = new HashSet<MainCategory>();
            Banks = new HashSet<Banks>();
            Configuration = new HashSet<Configuration>();
            PaymentConfiguration = new HashSet<PaymentConfiguration>();
            Configuration = new HashSet<Configuration>();
            PaymentStatus = new HashSet<PaymentStatus>();
            Package = new HashSet<Package>();
            Slider = new HashSet<Slider>();
            OrderStatus = new HashSet<OrderStatus>();
            PaymentWay = new HashSet<PaymentWay>();
            Block = new HashSet<Block>();
            Customers = new HashSet<Customers>();
            CustomerLocation = new HashSet<CustomerLocation>();
            Vendors = new HashSet<Vendors>();
            Product = new HashSet<Product>();
            ProductQuestion = new HashSet<ProductQuestion>();
            ProductImage = new HashSet<ProductImage>();
            ShippingCompany = new HashSet<ShippingCompany>();
            MainPage = new HashSet<MainPage>();
            MainPageDetails = new HashSet<MainPageDetails>();
            MainPageImages = new HashSet<MainPageImages>();
            KeyWords = new HashSet<KeyWords>();
            CustomerFavourites = new HashSet<CustomerFavourites>();
            AddressTypes = new HashSet<AddressTypes>();
            PromoCode = new HashSet<PromoCode>();
            OrderPromo = new HashSet<OrderPromo>();
            Orders = new HashSet<Orders>();
            OrderItems = new HashSet<OrderItems>();
            OrderVendor = new HashSet<OrderVendor>();
            Branches = new HashSet<Branches>();
            OrderHistory = new HashSet<OrderHistory>();
            VendorPromo = new HashSet<VendorPromo>();
            ProdQuestion = new HashSet<ProdQuestion>();
            OrderRate = new HashSet<OrderRate>();
            EnableHistory = new HashSet<EnableHistory>();
            VacHistory = new HashSet<VacHistory>();
            Employees = new HashSet<Employees>();
            Notification = new HashSet<Notification>();
            InvoiceDetails = new HashSet<InvoiceDetails>();
            InvoiceMaster = new HashSet<InvoiceMaster>();
            Drivers = new HashSet<Drivers>();
            TranLogSTCPay = new HashSet<TranLogSTCPay>();
            TransactionSTCPay = new HashSet<TransactionSTCPay>();
            TransactionType = new HashSet<TransactionType>();
            DriverBlance = new HashSet<DriverBlance>();
            DeliverySetting = new HashSet<DeliverySetting>();
            VendorSupport = new HashSet<VendorSupport>();
            DriverSupport = new HashSet<DriverSupport>();
            HelpQuestions = new HashSet<HelpQuestions>();
            CustomerBalance = new HashSet<CustomerBalance>();
            InvoiceHistory = new HashSet<InvoiceHistory>();
            VendorBalance = new HashSet<VendorBalance>();
            ListTransfer = new HashSet<ListTransfer>();
            Discount = new HashSet<Discount>();
            CitiesCovered = new HashSet<CitiesCovered>();
            CaptainZone = new HashSet<CaptainZone>();
            StatusCompany = new HashSet<StatusCompany>();
            ShippingCompanyBlocks = new HashSet<ShippingCompanyBlocks>();
            RegionCity = new HashSet<RegionCity>();
            CompanyWorkingHours = new HashSet<CompanyWorkingHours>();
            QuantitiesRequest = new HashSet<QuantitiesRequest>();
            QuantitiesRequestProduct = new HashSet<QuantitiesRequestProduct>();
            InqueriesReply = new HashSet<InqueriesReply>();
            OrderNotesAdmin = new HashSet<OrderNotesAdmin>();
        }
        public int UserType { get; set; }
        public string UserJWTToken { get; set; }
        
        public virtual ICollection<Discount> Discount { get; set; }
        public virtual ICollection<ListTransfer> ListTransfer { get; set; }
        public virtual ICollection<InvoiceHistory> InvoiceHistory { get; set; }
        public virtual ICollection<CustomerBalance> CustomerBalance { get; set; }
        public virtual ICollection<VendorBalance> VendorBalance { get; set; }
        public virtual ICollection<DriverSupport> DriverSupport { get; set; }
        public virtual ICollection<HelpQuestions> HelpQuestions { get; set; }
        public virtual ICollection<VendorSupport> VendorSupport { get; set; }
        public virtual ICollection<DeliverySetting> DeliverySetting { get; set; }
        public virtual ICollection<DriverBlance> DriverBlance { get; set; }
        public virtual ICollection<TransactionType> TransactionType { get; set; }
        public virtual ICollection<TransactionSTCPay> TransactionSTCPay { get; set; }
        public virtual ICollection<TranLogSTCPay> TranLogSTCPay { get; set; }
        public virtual ICollection<Drivers> Drivers { get; set; }
        public virtual ICollection<Notification> Notification { get; set; }
        public virtual ICollection<InvoiceDetails> InvoiceDetails { get; set; }
        public virtual ICollection<InvoiceMaster> InvoiceMaster { get; set; }
        public virtual ICollection<Employees> Employees { get; set; }
        public virtual ICollection<OrderRate> OrderRate { get; set; }
        public virtual ICollection<EnableHistory> EnableHistory { get; set; }
        public virtual ICollection<VacHistory> VacHistory { get; set; }
        public virtual ICollection<ProdQuestion> ProdQuestion { get; set; }
        public virtual ICollection<VendorPromo> VendorPromo { get; set; }
        public virtual ICollection<OrderHistory> OrderHistory { get; set; }
        public virtual ICollection<OrderVendor> OrderVendor { get; set; }
        public virtual ICollection<OrderPromo> OrderPromo { get; set; }
        public virtual ICollection<OrderItems> OrderItems { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
        public virtual ICollection<Branches> Branches { get; set; }
        public virtual ICollection<PromoCode> PromoCode { get; set; }
        public virtual ICollection<AddressTypes> AddressTypes { get; set; }
        public virtual ICollection<KeyWords> KeyWords { get; set; }
        public virtual ICollection<CustomerFavourites> CustomerFavourites { get; set; }
        public virtual ICollection<MainPageDetails> MainPageDetails { get; set; }
        public virtual ICollection<MainPageImages> MainPageImages { get; set; }
        public virtual ICollection<MainPage> MainPage { get; set; }
        public virtual ICollection<ShippingCompany> ShippingCompany { get; set; }
        public virtual ICollection<Product> Product { get; set; }
        public virtual ICollection<Vendors> Vendors { get; set; }
        public virtual ICollection<CustomerLocation> CustomerLocation { get; set; }
        public virtual ICollection<Customers> Customers { get; set; }
        public virtual ICollection<Block> Block { get; set; }
        public virtual ICollection<Country> Country { get; set; }
        public virtual ICollection<City> City { get; set; }
        public virtual ICollection<Region> Region { get; set; }
        public virtual ICollection<Jobs> Jobs { get; set; }
        public virtual ICollection<Departments> Departments { get; set; }
        public virtual ICollection<Nationality> Nationality { get; set; }
        public virtual ICollection<MainCategory> MainCategory { get; set; }
        public virtual ICollection<Banks> Banks { get; set; }
        public virtual ICollection<Configuration> Configuration { get; set; }
        public virtual ICollection<PaymentConfiguration> PaymentConfiguration { get; set; }
        public virtual ICollection<PaymentStatus> PaymentStatus { get; set; }
        public virtual ICollection<PaymentWay> PaymentWay { get; set; }
        public virtual ICollection<OrderStatus> OrderStatus { get; set; }
        public virtual ICollection<Activity> Activity { get; set; }
        public virtual ICollection<Package> Package { get; set; }
        public virtual ICollection<Slider> Slider { get; set; }
        public virtual ICollection<ProductImage> ProductImage { get; set; }
        public virtual ICollection<ProductQuestion> ProductQuestion { get; set; }
        public virtual ICollection<CitiesCovered> CitiesCovered { get; set; }
        public virtual ICollection<CaptainZone> CaptainZone { get; set; }
        public virtual ICollection<StatusCompany> StatusCompany { get; set; }
        public virtual ICollection<ShippingCompanyBlocks> ShippingCompanyBlocks { get; set; }
        public virtual ICollection<RegionCity> RegionCity { get; set; }
        public virtual ICollection<CompanyWorkingHours> CompanyWorkingHours { get; set; }
        public virtual ICollection<QuantitiesRequest> QuantitiesRequest { get; set; }
        public virtual ICollection<QuantitiesRequestProduct> QuantitiesRequestProduct { get; set; }
        public virtual ICollection<InqueriesReply> InqueriesReply { get; set; }
        public virtual ICollection<OrderNotesAdmin> OrderNotesAdmin { get; set; }

    }
    [Table("Role")]
    public class CustomRole : IdentityRole<int>
    {
        public CustomRole()
        {
            Permissions = new HashSet<Permission>();
            RoleConfig = new HashSet<RoleConfig>();
            TempPermission = new HashSet<TempPermission>();
        }
        public int RoleTypeId { get; set; } //RoleTypeEnum
        //public int? EntityID { get; set; }
        //public virtual Entities Entities { get; set; }
        public virtual ICollection<Permission> Permissions { get; set; }
        public virtual ICollection<TempPermission> TempPermission { get; set; }
        public virtual ICollection<RoleConfig> RoleConfig { get; set; }

    }

    public class CustomUserRole : IdentityUserRole<int> { }
    public class CustomUserClaim : IdentityUserClaim<int> { }
    public class CustomUserLogin : IdentityUserLogin<int> { }
}