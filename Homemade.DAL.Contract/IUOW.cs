using Homemade.Model;
using Homemade.Model.BankTransaction;
using Homemade.Model.Customer;
using Homemade.Model.Driver;
using Homemade.Model.Emp;
using Homemade.Model.Order;
using Homemade.Model.Setting;
using Homemade.Model.Site;
using Homemade.Model.Vendor;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Homemade.DAL.Contract
{
    public interface IUOW
    {
        #region Permission
        IGenericRepository<User> User { get; }
        IGenericRepository<CustomRole> Role { get; }
        IGenericRepository<Permission> Permission { get; }
        IGenericRepository<TempPermission> TempPermission { get; }
        IGenericRepository<PermissionAction> PermissionAction { get; }
        IGenericRepository<RoleConfig> RoleConfig { get; }
        IGenericRepository<PermissionController> PermissionController { get; }
        IGenericRepository<PermissionControllerAction> PermissionControllerAction { get; }
        IGenericRepository<IdentityUserRole<int>> UserRole { get; }

        //IGenericRepository<Branches> Branches { get; }

        #endregion
        #region  Setting
        IGenericRepository<InqueriesReply> InqueriesReply { get; }
        IGenericRepository<OrderNotesAdmin> OrderNotesAdmin { get; }
        IGenericRepository<CustomerFavourites> CustomerFavourites { get; }
        IGenericRepository<KeyWords> KeyWords { get; }
        IGenericRepository<ShippingCompany> ShippingCompany { get; }
        IGenericRepository<Inqueries> Inqueries { get; }
        IGenericRepository<MainPageImages> MainPageImages { get; }
        IGenericRepository<MainPage> MainPage { get; }
        IGenericRepository<MainPageDetails> MainPageDetails { get; }
        IGenericRepository<Country> Country { get; }
        IGenericRepository<Region> Region { get; }
        IGenericRepository<City> City { get; }
        IGenericRepository<Nationality> Nationality { get; }
        IGenericRepository<Departments> Departments { get; }
        IGenericRepository<Jobs> Jobs { get; }
        IGenericRepository<Employees> Employees { get; }
        IGenericRepository<MainCategory> MainCategory { get; }
        IGenericRepository<Banks> Banks { get; }
        IGenericRepository<PaymentConfiguration> PaymentConfiguration { get; }
        IGenericRepository<Configuration> Configuration { get; }
        IGenericRepository<Package> Package { get; }
        IGenericRepository<PaymentStatus> PaymentStatus { get; }
        IGenericRepository<PaymentWay> PaymentWay { get; }
        IGenericRepository<Activity> Activity { get; }
        IGenericRepository<OrderStatus> OrderStatus { get; }
        IGenericRepository<Slider> Slider { get; }
        IGenericRepository<Customers> Customers { get; }
        IGenericRepository<CustomerLocation> CustomerLocation { get; }
        IGenericRepository<Block> Block { get; }
        IGenericRepository<Vendors> Vendors { get; }
        IGenericRepository<Product> Product { get; }
        IGenericRepository<ProductImage> ProductImage { get; }
        IGenericRepository<ProductQuestion> ProductQuestion { get; }
        IGenericRepository<Tokens> Tokens { get; }
        IGenericRepository<AddressTypes> AddressTypes { get; }

        IGenericRepository<Orders> Orders { get; }
        IGenericRepository<OrderHistory> OrderHistory { get; }
        IGenericRepository<OrderItems> OrderItems { get; }
        IGenericRepository<OrderPromo> OrderPromo { get; }
        IGenericRepository<OrderVendor> OrderVendor { get; }
        IGenericRepository<PromoCode> PromoCode { get; }
        IGenericRepository<Branches> Branches { get; }
        IGenericRepository<VendorPromo> VendorPromo { get; }
        IGenericRepository<ProdQuestion> ProdQuestion { get; }
        IGenericRepository<OrderRate> OrderRate { get; }
        IGenericRepository<EnableHistory> EnableHistory { get; }
        IGenericRepository<VacHistory> VacHistory { get; }
        IGenericRepository<Notification> Notification { get; }
        IGenericRepository<InvoiceDetails> InvoiceDetails { get; }
        IGenericRepository<InvoiceMaster> InvoiceMaster { get; }
        IGenericRepository<Subscribe> Subscribe { get; }
        IGenericRepository<CitiesCovered> CitiesCovered { get; }
        IGenericRepository<CaptainZone> CaptainZone { get; }
        IGenericRepository<StatusCompany> StatusCompany { get; }
        IGenericRepository<ShippingCompanyBlocks> ShippingCompanyBlocks { get; }
        IGenericRepository<RegionCity> RegionCity { get; }
        IGenericRepository<CompanyWorkingHours> CompanyWorkingHours { get; }
        #endregion
        #region Card
        IGenericRepository<CartDetails> CartDetails { get; }
        IGenericRepository<CartMaster> CartMaster { get; }
        IGenericRepository<TransactionCard> TransactionCard { get; }
        IGenericRepository<ShipCompanyHistory> ShipCompanyHistory { get; }
        IGenericRepository<TransactionCardLog> TransactionCardLog { get; }
        IGenericRepository<TabCharge> TabCharge { get; }
        IGenericRepository<TabChargeExLog> TabChargeExLog { get; }
        IGenericRepository<QuantitiesRequest> QuantitiesRequest { get; }
        IGenericRepository<QuantitiesRequestProduct> QuantitiesRequestProduct { get; }
        #endregion
        #region Driver
        IGenericRepository<Drivers> Drivers { get; }
        IGenericRepository<TranLogSTCPay> TranLogSTCPay { get; }
        IGenericRepository<TransactionSTCPay> TransactionSTCPay { get; }
        IGenericRepository<TransactionType> TransactionType { get; }
        IGenericRepository<DriverBlance> DriverBlance { get; }
        IGenericRepository<DeliverySetting> DeliverySetting { get; }
        IGenericRepository<DriverSupport> DriverSupport { get; }
        IGenericRepository<VendorSupport> VendorSupport { get; }
        IGenericRepository<HelpQuestions> HelpQuestions { get; }
        IGenericRepository<LogTextMessage> LogTextMessage { get; }
        IGenericRepository<CustomerBalance> CustomerBalance { get; }
        IGenericRepository<VendorBalance> VendorBalance { get; }
        IGenericRepository<InvoiceHistory> InvoiceHistory { get; }
        IGenericRepository<ListTransfer> ListTransfer { get; }
        IGenericRepository<Discount> Discount { get; }
        
        #endregion
        void Commit();
    }
}

