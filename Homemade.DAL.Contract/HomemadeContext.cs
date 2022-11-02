using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Homemade.DAL
{
    public class HomemadeContext : IdentityDbContext<User , CustomRole , int>
    {
        public HomemadeContext(DbContextOptions<HomemadeContext> option) :
          base(option)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            #region Identity


            modelBuilder.Entity<User>().ToTable("User").Property(p => p.Id).HasColumnName("UserId");
            modelBuilder.Entity<CustomRole>().ToTable("Role").Property(p => p.Id).HasColumnName("RoleId");
            #endregion
            #region Premessions
            modelBuilder.Entity<CustomRole>().HasMany(e => e.TempPermission).WithOne(x => x.Role).IsRequired(true).HasForeignKey(x => x.RoleId);
            modelBuilder.Entity<CustomRole>().HasMany(e => e.RoleConfig).WithOne(x => x.Role).IsRequired(true).HasForeignKey(x => x.RoleId);
          //  modelBuilder.Entity<User>().HasMany(e => e.CustomRole).WithOne(x => x.User).IsRequired(true).HasForeignKey(x => x.UserId);
            modelBuilder.Entity<PermissionControllerAction>().HasMany(e => e.TempPermission).WithOne(x => x.PermissionControllerActions).IsRequired(true).HasForeignKey(x => x.PermissionControllerActionID);
            modelBuilder.Entity<PermissionControllerAction>().HasMany(e => e.RoleConfig).WithOne(x => x.PermissionControllerActions).IsRequired(true).HasForeignKey(x => x.PermissionControllerActionID);
            //modelBuilder.Entity<Entities>().HasMany(e => e.CustomRole).WithOne(e => e.Entities).HasForeignKey(m => m.EntityID).IsRequired(false);
            #endregion
            #region User
            modelBuilder.Entity<User>().HasMany(e => e.MainCategory).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            modelBuilder.Entity<User>().HasMany(e => e.Country).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            modelBuilder.Entity<User>().HasMany(e => e.Region).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            modelBuilder.Entity<User>().HasMany(e => e.City).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            modelBuilder.Entity<User>().HasMany(e => e.Nationality).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            modelBuilder.Entity<User>().HasMany(e => e.PaymentConfiguration).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            modelBuilder.Entity<User>().HasMany(e => e.Banks).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            modelBuilder.Entity<User>().HasMany(e => e.Configuration).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            modelBuilder.Entity<User>().HasMany(e => e.Jobs).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            modelBuilder.Entity<User>().HasMany(e => e.Departments).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            modelBuilder.Entity<User>().HasMany(e => e.Package).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            modelBuilder.Entity<User>().HasMany(e => e.PaymentWay).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            modelBuilder.Entity<User>().HasMany(e => e.PaymentStatus).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            modelBuilder.Entity<User>().HasMany(e => e.OrderStatus).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            modelBuilder.Entity<User>().HasMany(e => e.Slider).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            modelBuilder.Entity<User>().HasMany(e => e.Activity).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            modelBuilder.Entity<User>().HasMany(e => e.Customers).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            modelBuilder.Entity<User>().HasMany(e => e.Block).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            modelBuilder.Entity<User>().HasMany(e => e.CustomerLocation).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            modelBuilder.Entity<User>().HasMany(e => e.Vendors).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            modelBuilder.Entity<User>().HasMany(e => e.Product).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            modelBuilder.Entity<User>().HasMany(e => e.ProductImage).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            modelBuilder.Entity<User>().HasMany(e => e.ProductQuestion).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            modelBuilder.Entity<User>().HasMany(e => e.ShippingCompany).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            modelBuilder.Entity<User>().HasMany(e => e.MainPage).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            modelBuilder.Entity<User>().HasMany(e => e.MainPageDetails).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            modelBuilder.Entity<User>().HasMany(e => e.MainPageImages).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            modelBuilder.Entity<User>().HasMany(e => e.KeyWords).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            modelBuilder.Entity<User>().HasMany(e => e.CustomerFavourites).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            modelBuilder.Entity<User>().HasMany(e => e.AddressTypes).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            modelBuilder.Entity<User>().HasMany(e => e.Orders).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            modelBuilder.Entity<User>().HasMany(e => e.OrderPromo).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            modelBuilder.Entity<User>().HasMany(e => e.OrderItems).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            modelBuilder.Entity<User>().HasMany(e => e.OrderHistory).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            modelBuilder.Entity<User>().HasMany(e => e.OrderVendor).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            modelBuilder.Entity<User>().HasMany(e => e.PromoCode).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            modelBuilder.Entity<User>().HasMany(e => e.Branches).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            modelBuilder.Entity<User>().HasMany(e => e.VendorPromo).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            modelBuilder.Entity<User>().HasMany(e => e.OrderRate).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            modelBuilder.Entity<User>().HasMany(e => e.ProdQuestion).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            modelBuilder.Entity<User>().HasMany(e => e.VacHistory).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            modelBuilder.Entity<User>().HasMany(e => e.EnableHistory).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            modelBuilder.Entity<User>().HasMany(e => e.Employees).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            modelBuilder.Entity<User>().HasMany(e => e.Notification).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            modelBuilder.Entity<User>().HasMany(e => e.InvoiceMaster).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            modelBuilder.Entity<User>().HasMany(e => e.InvoiceDetails).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            modelBuilder.Entity<User>().HasMany(e => e.DriverBlance).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            modelBuilder.Entity<User>().HasMany(e => e.Drivers).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            modelBuilder.Entity<User>().HasMany(e => e.TransactionType).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            modelBuilder.Entity<User>().HasMany(e => e.TranLogSTCPay).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            modelBuilder.Entity<User>().HasMany(e => e.TransactionSTCPay).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            modelBuilder.Entity<User>().HasMany(e => e.DeliverySetting).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            modelBuilder.Entity<User>().HasMany(e => e.DriverSupport).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            modelBuilder.Entity<User>().HasMany(e => e.VendorSupport).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            modelBuilder.Entity<User>().HasMany(e => e.HelpQuestions).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            modelBuilder.Entity<User>().HasMany(e => e.CustomerBalance).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            modelBuilder.Entity<User>().HasMany(e => e.VendorBalance).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            modelBuilder.Entity<User>().HasMany(e => e.InvoiceHistory).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            modelBuilder.Entity<User>().HasMany(e => e.ListTransfer).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            modelBuilder.Entity<User>().HasMany(e => e.Discount).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            modelBuilder.Entity<User>().HasMany(e => e.CitiesCovered).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            modelBuilder.Entity<User>().HasMany(e => e.CaptainZone).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            modelBuilder.Entity<User>().HasMany(e => e.StatusCompany).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            modelBuilder.Entity<User>().HasMany(e => e.ShippingCompanyBlocks).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            modelBuilder.Entity<User>().HasMany(e => e.RegionCity).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            modelBuilder.Entity<User>().HasMany(e => e.CompanyWorkingHours).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            modelBuilder.Entity<User>().HasMany(e => e.QuantitiesRequest).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            modelBuilder.Entity<User>().HasMany(e => e.QuantitiesRequestProduct).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            #endregion
            #region Driver
            modelBuilder.Entity<HelpQuestions>().HasMany(e => e.VendorSupport).WithOne(e => e.HelpQuestions).IsRequired(true);
            modelBuilder.Entity<HelpQuestions>().HasMany(e => e.DriverSupport).WithOne(e => e.HelpQuestions).IsRequired(true);
            modelBuilder.Entity<Drivers>().HasMany(e => e.DriverSupport).WithOne(e => e.Drivers).IsRequired(true);
            modelBuilder.Entity<Vendors>().HasMany(e => e.VendorSupport).WithOne(e => e.Vendors).IsRequired(true);
            modelBuilder.Entity<OrderVendor>().HasMany(e => e.DriverSupport).WithOne(e => e.OrderVendor).IsRequired(false);
            modelBuilder.Entity<OrderVendor>().HasMany(e => e.VendorSupport).WithOne(e => e.OrderVendor).IsRequired(false);
            modelBuilder.Entity<Drivers>().HasMany(e => e.Notification).WithOne(e => e.Drivers).IsRequired(false);
            modelBuilder.Entity<DriverBlance>().HasMany(e => e.Notification).WithOne(e => e.DriverBlance).IsRequired(false);
            modelBuilder.Entity<Drivers>().HasMany(e => e.DriverBlance).WithOne(e => e.Drivers).IsRequired(true);
            modelBuilder.Entity<TransactionType>().HasMany(e => e.DriverBlance).WithOne(e => e.TransactionType).IsRequired(true);
            modelBuilder.Entity<Drivers>().HasMany(e => e.OrderVendor).WithOne(e => e.Drivers).IsRequired(false);
            modelBuilder.Entity<Drivers>().HasMany(e => e.TransactionSTCPay).WithOne(e => e.Drivers).IsRequired(true);
            modelBuilder.Entity<DriverBlance>().HasMany(e => e.TransactionSTCPay).WithOne(e => e.DriverBlance).IsRequired(false);
            modelBuilder.Entity<TransactionSTCPay>().HasMany(e => e.TranLogSTCPay).WithOne(e => e.TransactionSTCPay).IsRequired(true);
            modelBuilder.Entity<RegionCity>().HasMany(e => e.Drivers).WithOne(e => e.RegionCity).IsRequired(false);
            #endregion
            #region Orders
            modelBuilder.Entity<Orders>().HasMany(e => e.TransactionCard).WithOne(x => x.Orders).IsRequired(false);
            modelBuilder.Entity<OrderRate>().HasMany(e => e.Notification).WithOne(e => e.OrderRate).IsRequired(false);
            modelBuilder.Entity<ProdQuestion>().HasMany(e => e.Notification).WithOne(e => e.ProdQuestion).IsRequired(false);
            modelBuilder.Entity<Orders>().HasMany(e => e.OrderPromo).WithOne(e => e.Orders).IsRequired(true);
            modelBuilder.Entity<OrderVendor>().HasMany(e => e.Notification).WithOne(e => e.OrderVendor).IsRequired(false);
            modelBuilder.Entity<Orders>().HasMany(e => e.TabChargeExLog).WithOne(e => e.Orders).IsRequired(false);
            modelBuilder.Entity<Orders>().HasMany(e => e.TabCharge).WithOne(e => e.Orders).IsRequired(false);
            modelBuilder.Entity<Customers>().HasMany(e => e.TabChargeExLog).WithOne(e => e.Customers).IsRequired(false);
            modelBuilder.Entity<Customers>().HasMany(e => e.TabCharge).WithOne(e => e.Customers).IsRequired(false);
            modelBuilder.Entity<CustomerBalance>().HasMany(e => e.TabCharge).WithOne(e => e.CustomerBalance).IsRequired(false);
            modelBuilder.Entity<Customers>().HasMany(e => e.Notification).WithOne(e => e.Customers).IsRequired(false);
            modelBuilder.Entity<Vendors>().HasMany(e => e.Notification).WithOne(e => e.Vendors).IsRequired(false);
            modelBuilder.Entity<Orders>().HasMany(e => e.OrderVendor).WithOne(e => e.Orders).IsRequired(true);
            modelBuilder.Entity<PromoCode>().HasMany(e => e.OrderPromo).WithOne(e => e.PromoCode).IsRequired(true);
            modelBuilder.Entity<PromoCode>().HasMany(e => e.Orders).WithOne(e => e.PromoCode).IsRequired(false);
            modelBuilder.Entity<Customers>().HasMany(e => e.Orders).WithOne(e => e.Customers).IsRequired(true);
            modelBuilder.Entity<CustomerLocation>().HasMany(e => e.Orders).WithOne(e => e.CustomerLocation).IsRequired(true);
            modelBuilder.Entity<OrderVendor>().HasMany(e => e.ShipCompanyHistory).WithOne(e => e.OrderVendor).IsRequired(true);
            modelBuilder.Entity<OrderVendor>().HasMany(e => e.OrderItems).WithOne(e => e.OrderVendor).IsRequired(true);
            modelBuilder.Entity<OrderVendor>().HasMany(e => e.InvoiceDetails).WithOne(e => e.OrderVendor).IsRequired(true);
            modelBuilder.Entity<InvoiceMaster>().HasMany(e => e.InvoiceDetails).WithOne(e => e.InvoiceMaster).IsRequired(true);
            modelBuilder.Entity<InvoiceMaster>().HasMany(e => e.ListTransfer).WithOne(e => e.InvoiceMaster).IsRequired(true);
            modelBuilder.Entity<Banks>().HasMany(e => e.ListTransfer).WithOne(e => e.Banks).IsRequired(true);
            modelBuilder.Entity<InvoiceMaster>().HasMany(e => e.InvoiceHistory).WithOne(e => e.InvoiceMaster).IsRequired(true);
            modelBuilder.Entity<OrderVendor>().HasMany(e => e.OrderHistory).WithOne(e => e.OrderVendor).IsRequired(true);
            modelBuilder.Entity<OrderVendor>().HasMany(e => e.DriverBlance).WithOne(e => e.OrderVendor).IsRequired(false);
            modelBuilder.Entity<OrderStatus>().HasMany(e => e.OrderVendor).WithOne(e => e.OrderStatus).IsRequired(true);
            modelBuilder.Entity<OrderStatus>().HasMany(e => e.OrderHistory).WithOne(e => e.OrderStatus).IsRequired(true);
            modelBuilder.Entity<OrderStatus>().HasMany(e => e.Orders).WithOne(e => e.OrderStatus).IsRequired(true);
            modelBuilder.Entity<Vendors>().HasMany(e => e.OrderVendor).WithOne(e => e.Vendors).IsRequired(true);
            modelBuilder.Entity<Vendors>().HasMany(e => e.VendorPromo).WithOne(e => e.Vendors).IsRequired(true);
            modelBuilder.Entity<Vendors>().HasMany(e => e.InvoiceMaster).WithOne(e => e.Vendors).IsRequired(true);
            modelBuilder.Entity<Vendors>().HasMany(e => e.VacHistory).WithOne(e => e.Vendors).IsRequired(true);
            modelBuilder.Entity<Vendors>().HasMany(e => e.EnableHistory).WithOne(e => e.Vendors).IsRequired(true);
            modelBuilder.Entity<PromoCode>().HasMany(e => e.VendorPromo).WithOne(e => e.PromoCode).IsRequired(true);
            modelBuilder.Entity<Vendors>().HasMany(e => e.QuantitiesRequest).WithOne(e => e.Vendors).IsRequired(true);
            modelBuilder.Entity<QuantitiesRequest>().HasMany(e => e.QuantitiesRequestProduct).WithOne(e => e.QuantitiesRequest).IsRequired(true);
            modelBuilder.Entity<Product>().HasMany(e => e.QuantitiesRequestProduct).WithOne(e => e.Product).IsRequired(true);

            #endregion
            #region Employees
            modelBuilder.Entity<Nationality>().HasMany(e => e.Employees).WithOne(e => e.Nationality).IsRequired(true);
            modelBuilder.Entity<Nationality>().HasMany(e => e.Drivers).WithOne(e => e.Nationality).IsRequired(true);
            modelBuilder.Entity<City>().HasMany(e => e.Employees).WithOne(e => e.City).IsRequired(true);
            modelBuilder.Entity<City>().HasMany(e => e.Drivers).WithOne(e => e.City).IsRequired(true);
            modelBuilder.Entity<Jobs>().HasMany(e => e.Employees).WithOne(e => e.Jobs).IsRequired(true);
            #endregion
            #region Setting
            modelBuilder.Entity<Country>().HasMany(e => e.Region).WithOne(e => e.Country).IsRequired(true);
            modelBuilder.Entity<Region>().HasMany(e => e.City).WithOne(e => e.Region).IsRequired(true);
            modelBuilder.Entity<MainCategory>().HasMany(e => e.Departments).WithOne(e => e.MainCategory).IsRequired(true);
            modelBuilder.Entity<Banks>().HasMany(e => e.PaymentConfiguration).WithOne(e => e.Banks).IsRequired(true);
            modelBuilder.Entity<MainPage>().HasMany(e => e.MainPageDetails).WithOne(e => e.MainPage).IsRequired(true);
            modelBuilder.Entity<MainPageDetails>().HasMany(e => e.MainPageImages).WithOne(e => e.MainPageDetails).IsRequired(true);
            modelBuilder.Entity<City>().HasMany(e => e.ShippingCompany).WithOne(e => e.City).IsRequired(true);
            modelBuilder.Entity<City>().HasMany(e => e.Branches).WithOne(e => e.City).IsRequired(true);
            modelBuilder.Entity<Departments>().HasMany(e => e.KeyWords).WithOne(e => e.Departments).IsRequired(false);
            modelBuilder.Entity<City>().HasMany(e => e.CitiesCovered).WithOne(e => e.City).IsRequired(true);
            modelBuilder.Entity<Block>().HasMany(e => e.CaptainZone).WithOne(e => e.Block).IsRequired(true);
            modelBuilder.Entity<Drivers>().HasMany(e => e.CaptainZone).WithOne(e => e.Drivers).IsRequired(true);
            modelBuilder.Entity<ShippingCompany>().HasMany(e => e.StatusCompany).WithOne(e => e.ShippingCompany).IsRequired(true);
            modelBuilder.Entity<ShippingCompany>().HasMany(e => e.ShipCompanyHistory).WithOne(e => e.ShippingCompany).IsRequired(true);
            modelBuilder.Entity<Inqueries>().HasMany(e => e.InqueriesReply).WithOne(e => e.Inqueries).IsRequired(true);
            modelBuilder.Entity<Block>().HasMany(e => e.ShippingCompanyBlocks).WithOne(e => e.Block).IsRequired(true);
            modelBuilder.Entity<ShippingCompany>().HasMany(e => e.ShippingCompanyBlocks).WithOne(e => e.ShippingCompany).IsRequired(true);
            modelBuilder.Entity<OrderVendor>().HasMany(e => e.OrderNotesAdmin).WithOne(e => e.OrderVendor).IsRequired(true);
            modelBuilder.Entity<User>().HasMany(e => e.InqueriesReply).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            modelBuilder.Entity<User>().HasMany(e => e.OrderNotesAdmin).WithOne(e => e.User).IsRequired(true).HasPrincipalKey(p => p.Id).IsRequired(false);
            #endregion
            #region Vendor
            modelBuilder.Entity<Activity>().HasMany(e => e.Vendors).WithOne(e => e.Activity).IsRequired(false);
            modelBuilder.Entity<Banks>().HasMany(e => e.Vendors).WithOne(e => e.Banks).IsRequired(false);
            modelBuilder.Entity<City>().HasMany(e => e.Vendors).WithOne(e => e.City).IsRequired(true);
            modelBuilder.Entity<Vendors>().HasMany(e => e.Product).WithOne(e => e.Vendors).IsRequired(true);
            modelBuilder.Entity<Departments>().HasMany(e => e.Product).WithOne(e => e.Departments).IsRequired(true);
            modelBuilder.Entity<Package>().HasMany(e => e.Vendors).WithOne(e => e.Package).IsRequired(false);
            modelBuilder.Entity<Block>().HasMany(e => e.Vendors).WithOne(e => e.Block).IsRequired(false);
            modelBuilder.Entity<Product>().HasMany(e => e.ProductQuestion).WithOne(e => e.Product).IsRequired(false);
            modelBuilder.Entity<Product>().HasMany(e => e.ProdQuestion).WithOne(e => e.Product).IsRequired(false);
            modelBuilder.Entity<Product>().HasMany(e => e.ProductImage).WithOne(e => e.Product).IsRequired(false);
            modelBuilder.Entity<Product>().HasMany(e => e.CustomerFavourites).WithOne(e => e.Product).IsRequired(false);
            modelBuilder.Entity<Nationality>().HasMany(e => e.Vendors).WithOne(e => e.Nationality).IsRequired(false);
            #endregion
            #region Customer
            modelBuilder.Entity<Customers>().HasMany(e => e.CartMaster).WithOne(e => e.Customers).IsRequired(false);
            modelBuilder.Entity<Block>().HasMany(e => e.CustomerLocation).WithOne(e => e.Block).IsRequired(true);
            modelBuilder.Entity<Customers>().HasMany(e => e.CustomerLocation).WithOne(e => e.Customers).IsRequired(true);
            modelBuilder.Entity<Customers>().HasMany(e => e.ProdQuestion).WithOne(e => e.Customers).IsRequired(true);
            modelBuilder.Entity<OrderVendor>().HasMany(e => e.OrderRate).WithOne(e => e.OrderVendor).IsRequired(true);
            modelBuilder.Entity<Customers>().HasMany(e => e.CustomerFavourites).WithOne(e => e.Customers).IsRequired(true);
            modelBuilder.Entity<City>().HasMany(e => e.Customers).WithOne(e => e.City).IsRequired(true);
            modelBuilder.Entity<ShippingCompany>().HasMany(e => e.OrderVendor).WithOne(e => e.ShippingCompany).IsRequired(false);
            modelBuilder.Entity<Package>().HasMany(e => e.OrderVendor).WithOne(e => e.Package).IsRequired(false);
            modelBuilder.Entity<Nationality>().HasMany(e => e.Customers).WithOne(e => e.Nationality).IsRequired(true);
            modelBuilder.Entity<Customers>().HasMany(e => e.CustomerBalance).WithOne(e => e.Customers).IsRequired(true);
            modelBuilder.Entity<OrderVendor>().HasMany(e => e.CustomerBalance).WithOne(e => e.OrderVendor).IsRequired(false);
            modelBuilder.Entity<TransactionType>().HasMany(e => e.CustomerBalance).WithOne(e => e.TransactionType).IsRequired(true);
            modelBuilder.Entity<Vendors>().HasMany(e => e.VendorBalance).WithOne(e => e.Vendors).IsRequired(true);
            modelBuilder.Entity<OrderVendor>().HasMany(e => e.VendorBalance).WithOne(e => e.OrderVendor).IsRequired(false);
            modelBuilder.Entity<TransactionType>().HasMany(e => e.VendorBalance).WithOne(e => e.TransactionType).IsRequired(true);
            #endregion
            HomemadeDbDefaultData.Seed(modelBuilder);
            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
            foreach (var property in modelBuilder.Model.GetEntityTypes()
    .SelectMany(t => t.GetProperties())
    .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                // EF Core 5
                property.SetPrecision(18);
                property.SetScale(2);
            }
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
        #region Order
        public virtual DbSet<TabCharge> TabCharge { get; set; }
        public virtual DbSet<TabChargeExLog> TabChargeExLog { get; set; }
        public virtual DbSet<Discount> Discount { get; set; }
        public virtual DbSet<ListTransfer> ListTransfer { get; set; }
        public virtual DbSet<InvoiceHistory> InvoiceHistory { get; set; }
        public virtual DbSet<VendorBalance> VendorBalance { get; set; }
        public virtual DbSet<CustomerBalance> CustomerBalance { get; set; }
        public virtual DbSet<LogTextMessage> LogTextMessage { get; set; }
        public virtual DbSet<HelpQuestions> HelpQuestions { get; set; }
        public virtual DbSet<DriverSupport> DriverSupport { get; set; }
        public virtual DbSet<VendorSupport> VendorSupport { get; set; }
        public virtual DbSet<DeliverySetting> DeliverySetting { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<EnableHistory> EnableHistory { get; set; }
        public virtual DbSet<VacHistory> VacHistory { get; set; }
        public virtual DbSet<OrderRate> OrderRate { get; set; }
        public virtual DbSet<ProdQuestion> ProdQuestion { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<OrderHistory> OrderHistory { get; set; }
        public virtual DbSet<OrderItems> OrderItems { get; set; }
        public virtual DbSet<OrderPromo> OrderPromo { get; set; }
        public virtual DbSet<OrderVendor> OrderVendor { get; set; }
        public virtual DbSet<PromoCode> PromoCode { get; set; }
        public virtual DbSet<QuantitiesRequest> QuantitiesRequest { get; set; }
        public virtual DbSet<QuantitiesRequestProduct> QuantitiesRequestProduct { get; set; }

        #endregion
        #region Customer
        public virtual DbSet<CustomerFavourites> CustomerFavourites { get; set; }
        public virtual DbSet<KeyWords> KeyWords { get; set; }
        public virtual DbSet<AddressTypes> AddressTypes { get; set; }
        #endregion
        #region Premessions
        public virtual DbSet<Permission> Permission { get; set; }
        public virtual DbSet<PermissionControllerAction> PermissionControllerAction { get; set; }
        public virtual DbSet<PermissionAction> PermissionAction { get; set; }
        public virtual DbSet<PermissionController> PermissionController { get; set; }
        public virtual DbSet<TempPermission> TempPermission { get; set; }
        public virtual DbSet<RoleConfig> RoleConfig { get; set; }
        #endregion
        #region Setting
        public virtual DbSet<MainPageImages> MainPageImages { get; set; }
        public virtual DbSet<MainPageDetails> MainPageDetails { get; set; }
        public virtual DbSet<MainPage> MainPage { get; set; }
        public virtual DbSet<OrderNotesAdmin> OrderNotesAdmin { get; set; }
        public virtual DbSet<InqueriesReply> InqueriesReply { get; set; }


        public virtual DbSet<Inqueries> Inqueries { get; set; }
        public virtual DbSet<ShippingCompany> ShippingCompany { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Region> Region { get; set; }
        public virtual DbSet<City> City { get; set; }

        public virtual DbSet<Jobs> Jobs { get; set; }
        public virtual DbSet<Departments> Departments { get; set; }
        public virtual DbSet<Nationality> Nationality { get; set; }
        public virtual DbSet<MainCategory> MainCategory { get; set; }
        public virtual DbSet<Banks> Banks { get; set; }
        public virtual DbSet<PaymentWay> PaymentWay { get; set; }
        public virtual DbSet<PaymentStatus> PaymentStatus { get; set; }
        public virtual DbSet<PaymentConfiguration> PaymentConfiguration { get; set; }
        public virtual DbSet<Activity> Activity { get; set; }
        public virtual DbSet<Slider> Slider { get; set; }
        public virtual DbSet<OrderStatus> OrderStatus { get; set; }
        public virtual DbSet<Package> Package { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<CustomerLocation> CustomerLocation { get; set; }
        public virtual DbSet<Block> Block { get; set; }
        public virtual DbSet<Vendors> Vendors { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductImage> ProductImage { get; set; }
        public virtual DbSet<ProductQuestion> ProductQuestion { get; set; }
        public virtual DbSet<Tokens> Tokens { get; set; }
        public virtual DbSet<Subscribe> Subscribe { get; set; }
        public virtual DbSet<CitiesCovered> CitiesCovered { get; set; }
        public virtual DbSet<CaptainZone> CaptainZone { get; set; }
        public virtual DbSet<RegionCity> RegionCity { get; set; }
        public virtual DbSet<CompanyWorkingHours> CompanyWorkingHours { get; set; }
        #endregion
        #region Card
        public virtual DbSet<CartDetails> CartDetails { get; set; }
        public virtual DbSet<CartMaster> CartMaster { get; set; }
        public virtual DbSet<TransactionCard> TransactionCard { get; set; }
        public virtual DbSet<TransactionCardLog> TransactionCardLog { get; set; }
        #endregion
    }

}