using Homemade.DAL.Contract;
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

namespace Homemade.DAL
{
    public class UOW : IUOW
    {
        private readonly HomemadeContext _context;

        #region Permission
        private IGenericRepository<User> _User;
        private IGenericRepository<CustomRole> _CustomRole;
        private IGenericRepository<Permission> _Permission;
        private IGenericRepository<TempPermission> _TempPermission;
        private IGenericRepository<PermissionAction> _PermissionAction;
        private IGenericRepository<PermissionController> _PermissionController;
        private IGenericRepository<RoleConfig> _RoleConfig;
        private IGenericRepository<PermissionControllerAction> _PermissionControllerAction;
        private IGenericRepository<IdentityUserRole<int>> _UserRole;
        #endregion
        #region Setting
        private IGenericRepository<InqueriesReply> _InqueriesReply;
        private IGenericRepository<OrderNotesAdmin> _OrderNotesAdmin;
        private IGenericRepository<Country> _Country;
        private IGenericRepository<Branches> _Branches;
        private IGenericRepository<Region> _Region;
        private IGenericRepository<City> _City;
        private IGenericRepository<Employees> _Employees;
        private IGenericRepository<Jobs> _Jobs;
        private IGenericRepository<Nationality> _Nationality;
        private IGenericRepository<Departments> _Departments;
        private IGenericRepository<MainCategory> _MainCategory;
        private IGenericRepository<Configuration> _Configuration;
        private IGenericRepository<PaymentConfiguration> _PaymentConfiguration;
        private IGenericRepository<Banks> _Banks;
        private IGenericRepository<PaymentWay> _PaymentWay;
        private IGenericRepository<OrderStatus> _OrderStatus;
        private IGenericRepository<Slider> _Slider;
        private IGenericRepository<PaymentStatus> _PaymentStatus;
        private IGenericRepository<Activity> _Activity;
        private IGenericRepository<Package> _Package;
        private IGenericRepository<Customers> _Customers;
        private IGenericRepository<CustomerLocation> _CustomerLocation;
        private IGenericRepository<Block> _Block;
        private IGenericRepository<Vendors> _Vendors;
        private IGenericRepository<Product> _Product;
        private IGenericRepository<ProductImage> _ProductImage;
        private IGenericRepository<ProductQuestion> _ProductQuestion;
        private IGenericRepository<Tokens> _Tokens;
        private IGenericRepository<MainPage> _MainPage;
        private IGenericRepository<MainPageDetails> _MainPageDetails;
        private IGenericRepository<MainPageImages> _MainPageImages;
        private IGenericRepository<Inqueries> _Inqueries;
        private IGenericRepository<ShippingCompany> _ShippingCompany;
        private IGenericRepository<KeyWords> _KeyWords;
        private IGenericRepository<CustomerFavourites> _CustomerFavourites;
        private IGenericRepository<AddressTypes> _AddressTypes;
        private IGenericRepository<ProdQuestion> _ProdQuestion;
        private IGenericRepository<Subscribe> _Subscribe;
        private IGenericRepository<CitiesCovered> _CitiesCovered;
        private IGenericRepository<CaptainZone> _CaptainZone;
        private IGenericRepository<StatusCompany> _StatusCompany;
        private IGenericRepository<ShippingCompanyBlocks> _ShippingCompanyBlocks;
        private IGenericRepository<RegionCity> _RegionCity;
        private IGenericRepository<CompanyWorkingHours> _CompanyWorkingHours;
        #endregion
        #region Driver
        private IGenericRepository<Drivers> _Drivers;
        private IGenericRepository<DriverBlance> _DriverBlance;
        private IGenericRepository<TranLogSTCPay> _TranLogSTCPay;
        private IGenericRepository<TransactionSTCPay> _TransactionSTCPay;
        private IGenericRepository<TransactionType> _TransactionType;
        private IGenericRepository<DeliverySetting> _DeliverySetting;
        private IGenericRepository<DriverSupport> _DriverSupport;
        private IGenericRepository<VendorSupport> _VendorSupport;
        private IGenericRepository<HelpQuestions> _HelpQuestions;
        private IGenericRepository<LogTextMessage> _LogTextMessage;
        private IGenericRepository<VendorBalance> _VendorBalance;
        private IGenericRepository<CustomerBalance> _CustomerBalance;
        private IGenericRepository<InvoiceHistory> _InvoiceHistory;
        private IGenericRepository<ListTransfer> _ListTransfer;
        private IGenericRepository<Discount> _Discount;
        private IGenericRepository<ShipCompanyHistory> _ShipCompanyHistory;
        private IGenericRepository<TabCharge> _TabCharge;
        private IGenericRepository<TabChargeExLog> _TabChargeExLog;
        #endregion
        #region Promo Code
        private IGenericRepository<OrderPromo> _OrderPromo;
        private IGenericRepository<PromoCode> _PromoCode;
        private IGenericRepository<VendorPromo> _VendorPromo;
        #endregion
        #region Order
        private IGenericRepository<Orders> _Orders;
        private IGenericRepository<OrderVendor> _OrderVendor;
        private IGenericRepository<OrderHistory> _OrderHistory;
        private IGenericRepository<OrderItems> _OrderItems;
        private IGenericRepository<OrderRate> _OrderRate;
        private IGenericRepository<TransactionCard> _TransactionCard;
        private IGenericRepository<EnableHistory> _EnableHistory;
        private IGenericRepository<VacHistory> _VacHistory;
        private IGenericRepository<CartDetails> _CartDetails;
        private IGenericRepository<CartMaster> _CartMaster;
        private IGenericRepository<Notification> _Notification;
        private IGenericRepository<InvoiceMaster> _InvoiceMaster;
        private IGenericRepository<InvoiceDetails> _InvoiceDetails;
        private IGenericRepository<TransactionCardLog> _TransactionCardLog;
        private IGenericRepository<QuantitiesRequest> _QuantitiesRequest;
        private IGenericRepository<QuantitiesRequestProduct> _QuantitiesRequestProduct;
        #endregion
        public UOW(HomemadeContext context)
        {
            _context = context;
        }
        #region Permission
        public IGenericRepository<User> User
        {
            get
            {
                return _User ?? (_User = new GenericRepository<User>(_context));
            }
        }
        public IGenericRepository<RoleConfig> RoleConfig
        {
            get
            {
                return _RoleConfig ?? (_RoleConfig = new GenericRepository<RoleConfig>(_context));
            }
        }
        public IGenericRepository<CustomRole> Role
        {
            get
            {
                return _CustomRole ?? (_CustomRole = new GenericRepository<CustomRole>(_context));
            }
        }
        public IGenericRepository<Permission> Permission
        {
            get
            {
                return _Permission ?? (_Permission = new GenericRepository<Permission>(_context));
            }
        }
        public IGenericRepository<TempPermission> TempPermission
        {
            get
            {
                return _TempPermission ?? (_TempPermission = new GenericRepository<TempPermission>(_context));
            }
        }
        public IGenericRepository<PermissionAction> PermissionAction
        {
            get
            {
                return _PermissionAction ?? (_PermissionAction = new GenericRepository<PermissionAction>(_context));
            }
        }
        public IGenericRepository<PermissionController> PermissionController
        {
            get
            {
                return _PermissionController ?? (_PermissionController = new GenericRepository<PermissionController>(_context));
            }
        }
        public IGenericRepository<PermissionControllerAction> PermissionControllerAction
        {
            get
            {
                return _PermissionControllerAction ?? (_PermissionControllerAction = new GenericRepository<PermissionControllerAction>(_context));
            }
        }
        public IGenericRepository<IdentityUserRole<int>> UserRole
        {
            get
            {
                return _UserRole ?? (_UserRole = new GenericRepository<IdentityUserRole<int>>(_context));
            }
        }
        #endregion
        #region Setting
        public IGenericRepository<InqueriesReply> InqueriesReply { get { return _InqueriesReply ?? (_InqueriesReply = new GenericRepository<InqueriesReply>(_context)); } }
        public IGenericRepository<OrderNotesAdmin> OrderNotesAdmin { get { return _OrderNotesAdmin ?? (_OrderNotesAdmin = new GenericRepository<OrderNotesAdmin>(_context)); } }
        public IGenericRepository<Discount> Discount { get { return _Discount ?? (_Discount = new GenericRepository<Discount>(_context)); } }
        public IGenericRepository<ListTransfer> ListTransfer { get { return _ListTransfer ?? (_ListTransfer = new GenericRepository<ListTransfer>(_context)); } }
        public IGenericRepository<InvoiceHistory> InvoiceHistory { get { return _InvoiceHistory ?? (_InvoiceHistory = new GenericRepository<InvoiceHistory>(_context)); } }
        public IGenericRepository<VendorBalance> VendorBalance { get { return _VendorBalance ?? (_VendorBalance = new GenericRepository<VendorBalance>(_context)); } }
        public IGenericRepository<CustomerBalance> CustomerBalance { get { return _CustomerBalance ?? (_CustomerBalance = new GenericRepository<CustomerBalance>(_context)); } }
        public IGenericRepository<LogTextMessage> LogTextMessage { get { return _LogTextMessage ?? (_LogTextMessage = new GenericRepository<LogTextMessage>(_context)); } }
        public IGenericRepository<HelpQuestions> HelpQuestions { get { return _HelpQuestions ?? (_HelpQuestions = new GenericRepository<HelpQuestions>(_context)); } }
        public IGenericRepository<VendorSupport> VendorSupport { get { return _VendorSupport ?? (_VendorSupport = new GenericRepository<VendorSupport>(_context)); } }
        public IGenericRepository<DriverSupport> DriverSupport { get { return _DriverSupport ?? (_DriverSupport = new GenericRepository<DriverSupport>(_context)); } }
        public IGenericRepository<DeliverySetting> DeliverySetting { get { return _DeliverySetting ?? (_DeliverySetting = new GenericRepository<DeliverySetting>(_context)); } }
        public IGenericRepository<InvoiceMaster> InvoiceMaster { get { return _InvoiceMaster ?? (_InvoiceMaster = new GenericRepository<InvoiceMaster>(_context)); } }
        public IGenericRepository<InvoiceDetails> InvoiceDetails { get { return _InvoiceDetails ?? (_InvoiceDetails = new GenericRepository<InvoiceDetails>(_context)); } }
        public IGenericRepository<Notification> Notification { get { return _Notification ?? (_Notification = new GenericRepository<Notification>(_context)); } }
        public IGenericRepository<EnableHistory> EnableHistory { get { return _EnableHistory ?? (_EnableHistory = new GenericRepository<EnableHistory>(_context)); } }
        public IGenericRepository<VacHistory> VacHistory { get { return _VacHistory ?? (_VacHistory = new GenericRepository<VacHistory>(_context)); } }
        public IGenericRepository<OrderRate> OrderRate { get { return _OrderRate ?? (_OrderRate = new GenericRepository<OrderRate>(_context)); } }
        public IGenericRepository<ProdQuestion> ProdQuestion { get { return _ProdQuestion ?? (_ProdQuestion = new GenericRepository<ProdQuestion>(_context)); } }
        public IGenericRepository<VendorPromo> VendorPromo { get { return _VendorPromo ?? (_VendorPromo = new GenericRepository<VendorPromo>(_context)); } }
        public IGenericRepository<Branches> Branches { get { return _Branches ?? (_Branches = new GenericRepository<Branches>(_context)); } }
        public IGenericRepository<PromoCode> PromoCode { get { return _PromoCode ?? (_PromoCode = new GenericRepository<PromoCode>(_context)); } }

        public IGenericRepository<Orders> Orders { get { return _Orders ?? (_Orders = new GenericRepository<Orders>(_context)); } }
        public IGenericRepository<AddressTypes> AddressTypes { get { return _AddressTypes ?? (_AddressTypes = new GenericRepository<AddressTypes>(_context)); } }
        public IGenericRepository<CustomerFavourites> CustomerFavourites { get { return _CustomerFavourites ?? (_CustomerFavourites = new GenericRepository<CustomerFavourites>(_context)); } }
        public IGenericRepository<KeyWords> KeyWords { get { return _KeyWords ?? (_KeyWords = new GenericRepository<KeyWords>(_context)); } }
        public IGenericRepository<MainPage> MainPage { get { return _MainPage ?? (_MainPage = new GenericRepository<MainPage>(_context)); } }
        public IGenericRepository<MainPageDetails> MainPageDetails { get { return _MainPageDetails ?? (_MainPageDetails = new GenericRepository<MainPageDetails>(_context)); } }
        public IGenericRepository<MainPageImages> MainPageImages { get { return _MainPageImages ?? (_MainPageImages = new GenericRepository<MainPageImages>(_context)); } }
        public IGenericRepository<Inqueries> Inqueries { get { return _Inqueries ?? (_Inqueries = new GenericRepository<Inqueries>(_context)); } }
        public IGenericRepository<ShippingCompany> ShippingCompany { get { return _ShippingCompany ?? (_ShippingCompany = new GenericRepository<ShippingCompany>(_context)); } }
        public IGenericRepository<ProductQuestion> ProductQuestion { get { return _ProductQuestion ?? (_ProductQuestion = new GenericRepository<ProductQuestion>(_context)); } }
        public IGenericRepository<ProductImage> ProductImage { get { return _ProductImage ?? (_ProductImage = new GenericRepository<ProductImage>(_context)); } }
        public IGenericRepository<Vendors> Vendors { get { return _Vendors ?? (_Vendors = new GenericRepository<Vendors>(_context)); } }
        public IGenericRepository<Product> Product { get { return _Product ?? (_Product = new GenericRepository<Product>(_context)); } }
        public IGenericRepository<Block> Block { get { return _Block ?? (_Block = new GenericRepository<Block>(_context)); } }
        public IGenericRepository<CustomerLocation> CustomerLocation { get { return _CustomerLocation ?? (_CustomerLocation = new GenericRepository<CustomerLocation>(_context)); } }
        public IGenericRepository<Customers> Customers { get { return _Customers ?? (_Customers = new GenericRepository<Customers>(_context)); } }
        public IGenericRepository<PaymentWay> PaymentWay { get { return _PaymentWay ?? (_PaymentWay = new GenericRepository<PaymentWay>(_context)); } }
        public IGenericRepository<OrderStatus> OrderStatus { get { return _OrderStatus ?? (_OrderStatus = new GenericRepository<OrderStatus>(_context)); } }
        public IGenericRepository<Slider> Slider { get { return _Slider ?? (_Slider = new GenericRepository<Slider>(_context)); } }
        public IGenericRepository<PaymentStatus> PaymentStatus { get { return _PaymentStatus ?? (_PaymentStatus = new GenericRepository<PaymentStatus>(_context)); } }
        public IGenericRepository<Activity> Activity { get { return _Activity ?? (_Activity = new GenericRepository<Activity>(_context)); } }
        public IGenericRepository<Banks> Banks { get { return _Banks ?? (_Banks = new GenericRepository<Banks>(_context)); } }
        public IGenericRepository<Package> Package { get { return _Package ?? (_Package = new GenericRepository<Package>(_context)); } }
        public IGenericRepository<Configuration> Configuration { get { return _Configuration ?? (_Configuration = new GenericRepository<Configuration>(_context)); } }
        public IGenericRepository<PaymentConfiguration> PaymentConfiguration { get { return _PaymentConfiguration ?? (_PaymentConfiguration = new GenericRepository<PaymentConfiguration>(_context)); } }
        public IGenericRepository<MainCategory> MainCategory { get { return _MainCategory ?? (_MainCategory = new GenericRepository<MainCategory>(_context)); } }
        public IGenericRepository<Country> Country { get { return _Country ?? (_Country = new GenericRepository<Country>(_context)); }}
        public IGenericRepository<Region> Region { get { return _Region ?? (_Region = new GenericRepository<Region>(_context)); } }
        public IGenericRepository<City> City { get { return _City ?? (_City = new GenericRepository<City>(_context)); } }
        public IGenericRepository<Employees> Employees { get { return _Employees ?? (_Employees = new GenericRepository<Employees>(_context)); } }
        public IGenericRepository<Jobs> Jobs { get { return _Jobs ?? (_Jobs = new GenericRepository<Jobs>(_context)); } }
        public IGenericRepository<Nationality> Nationality { get { return _Nationality ?? (_Nationality = new GenericRepository<Nationality>(_context)); } }
        public IGenericRepository<Departments> Departments { get { return _Departments ?? (_Departments = new GenericRepository<Departments>(_context)); } }
        public IGenericRepository<Tokens> Tokens { get { return _Tokens ?? (_Tokens = new GenericRepository<Tokens>(_context)); } }
        public IGenericRepository<Subscribe> Subscribe { get { return _Subscribe ?? (_Subscribe = new GenericRepository<Subscribe>(_context)); } }
        public IGenericRepository<CitiesCovered> CitiesCovered { get { return _CitiesCovered ?? (_CitiesCovered = new GenericRepository<CitiesCovered>(_context)); } }
        public IGenericRepository<CaptainZone> CaptainZone { get { return _CaptainZone ?? (_CaptainZone = new GenericRepository<CaptainZone>(_context)); } }
        public IGenericRepository<StatusCompany> StatusCompany { get { return _StatusCompany ?? (_StatusCompany = new GenericRepository<StatusCompany>(_context)); } }
        public IGenericRepository<ShippingCompanyBlocks> ShippingCompanyBlocks { get { return _ShippingCompanyBlocks ?? (_ShippingCompanyBlocks = new GenericRepository<ShippingCompanyBlocks>(_context)); } }
        public IGenericRepository<RegionCity> RegionCity { get { return _RegionCity ?? (_RegionCity = new GenericRepository<RegionCity>(_context)); } }
        public IGenericRepository<CompanyWorkingHours> CompanyWorkingHours { get { return _CompanyWorkingHours ?? (_CompanyWorkingHours = new GenericRepository<CompanyWorkingHours>(_context)); } }

        #endregion
        #region Driver
        public IGenericRepository<Drivers> Drivers { get { return _Drivers ?? (_Drivers = new GenericRepository<Drivers>(_context)); } }
        public IGenericRepository<DriverBlance> DriverBlance { get { return _DriverBlance ?? (_DriverBlance = new GenericRepository<DriverBlance>(_context)); } }
        public IGenericRepository<TranLogSTCPay> TranLogSTCPay { get { return _TranLogSTCPay ?? (_TranLogSTCPay = new GenericRepository<TranLogSTCPay>(_context)); } }
        public IGenericRepository<TransactionSTCPay> TransactionSTCPay { get { return _TransactionSTCPay ?? (_TransactionSTCPay = new GenericRepository<TransactionSTCPay>(_context)); } }
        public IGenericRepository<TransactionType> TransactionType { get { return _TransactionType ?? (_TransactionType = new GenericRepository<TransactionType>(_context)); } }
        #endregion
        #region Card
        public IGenericRepository<TabChargeExLog> TabChargeExLog { get { return _TabChargeExLog ?? (_TabChargeExLog = new GenericRepository<TabChargeExLog>(_context)); } }
        public IGenericRepository<TabCharge> TabCharge { get { return _TabCharge ?? (_TabCharge = new GenericRepository<TabCharge>(_context)); } }
        public IGenericRepository<ShipCompanyHistory> ShipCompanyHistory { get { return _ShipCompanyHistory ?? (_ShipCompanyHistory = new GenericRepository<ShipCompanyHistory>(_context)); } }
        public IGenericRepository<CartDetails> CartDetails { get { return _CartDetails ?? (_CartDetails = new GenericRepository<CartDetails>(_context)); } }
        public IGenericRepository<CartMaster> CartMaster { get { return _CartMaster ?? (_CartMaster = new GenericRepository<CartMaster>(_context)); } }

        public IGenericRepository<TransactionCard> TransactionCard { get { return _TransactionCard ?? (_TransactionCard = new GenericRepository<TransactionCard>(_context)); } }
        public IGenericRepository<TransactionCardLog> TransactionCardLog { get { return _TransactionCardLog ?? (_TransactionCardLog = new GenericRepository<TransactionCardLog>(_context)); } }
        #endregion
        #region Order
        public IGenericRepository<OrderVendor> OrderVendor { get { return _OrderVendor ?? (_OrderVendor = new GenericRepository<OrderVendor>(_context)); } }
        public IGenericRepository<OrderPromo> OrderPromo { get { return _OrderPromo ?? (_OrderPromo = new GenericRepository<OrderPromo>(_context)); } }
        public IGenericRepository<OrderItems> OrderItems { get { return _OrderItems ?? (_OrderItems = new GenericRepository<OrderItems>(_context)); } }
        public IGenericRepository<OrderHistory> OrderHistory { get { return _OrderHistory ?? (_OrderHistory = new GenericRepository<OrderHistory>(_context)); } }
        public IGenericRepository<QuantitiesRequest> QuantitiesRequest { get { return _QuantitiesRequest ?? (_QuantitiesRequest = new GenericRepository<QuantitiesRequest>(_context)); } }
        public IGenericRepository<QuantitiesRequestProduct> QuantitiesRequestProduct { get { return _QuantitiesRequestProduct ?? (_QuantitiesRequestProduct = new GenericRepository<QuantitiesRequestProduct>(_context)); } }
        #endregion

        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}
