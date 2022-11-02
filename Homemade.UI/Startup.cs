using Homemade.BLL;
using Homemade.BLL.Customer;
using Homemade.BLL.Driver;
using Homemade.BLL.Emp;
using Homemade.BLL.General;
using Homemade.BLL.Order;
using Homemade.BLL.Purchase;
using Homemade.BLL.Setting;
using Homemade.BLL.Site;
using Homemade.BLL.SMS;
using Homemade.BLL.Vendor;
using Homemade.DAL;
using Homemade.DAL.Contract;
using Homemade.Model;
using Homemade.UI.InfraStructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace Homemade.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();

            services.AddLocalization(options => options.ResourcesPath = "Homemade.UI.Resources");
            services.AddMvc().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);
            services.Configure<IISServerOptions>(options =>
            {
                options.MaxRequestBodySize = int.MaxValue;
            });
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en"),
                    new CultureInfo("ar"),
                    new CultureInfo("de"),
                    new CultureInfo("hi"),
                    new CultureInfo("zh"),
                };
                options.DefaultRequestCulture = new RequestCulture("ar");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });
            services.AddControllersWithViews();
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });
            services.AddDbContext<HomemadeContext>(x => x.UseSqlServer(Configuration.GetConnectionString("HomemadeDBConnection")));
            services.AddIdentity<User, CustomRole>(config =>
            {
                config.User.RequireUniqueEmail = false;
                #region Password
                config.Password.RequiredLength = 5;
                config.Password.RequireDigit = false;
                config.Password.RequireLowercase = false;
                config.Password.RequiredUniqueChars = 1;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
                #endregion
                config.SignIn.RequireConfirmedEmail = false;
                config.SignIn.RequireConfirmedPhoneNumber = false;
            }).AddEntityFrameworkStores<HomemadeContext>().AddDefaultTokenProviders();
            services.AddMemoryCache();
            services.AddHttpContextAccessor();
            services.AddHealthChecks();
            services.AddServerSideBlazor();
            services.Configure<PasswordHasherOptions>(options => options.CompatibilityMode = PasswordHasherCompatibilityMode.IdentityV3);
            services.ConfigureApplicationCookie(options => { options.LoginPath = "/Identity/Account/Login"; });
            #region AddScoped
            services.AddScoped(typeof(IUOW), typeof(UOW));
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromDays(1);
                options.IOTimeout = TimeSpan.FromDays(1);
            });
            #region Email
            services.AddScoped((serviceProvider) =>
            {
                IConfigurationSection emailSection = Configuration.GetSection("Email");
                var smtp = new SmtpClient();
                if (emailSection != null)
                {
                    smtp.Host = emailSection.GetValue<string>(nameof(smtp.Host));
                    smtp.Port = emailSection.GetValue<int>(nameof(smtp.Port));
                    smtp.UseDefaultCredentials = emailSection.GetValue<bool>(nameof(smtp.UseDefaultCredentials));
                    smtp.Credentials = new NetworkCredential(emailSection.GetValue<string>("Username"), emailSection.GetValue<string>("Password"));
                    smtp.EnableSsl = emailSection.GetValue<bool>(nameof(smtp.EnableSsl));
                    //   smtp.DeliveryMethod = emailSection.GetValue<SmtpDeliveryMethod>(nameof(smtp.DeliveryMethod));
                }
                return smtp;
            });
            #endregion
            #region Scoped
            services.AddScoped<BlEnableHistory>();
            services.AddScoped<BlVacHistory>();
            services.AddScoped<BlMainPageDetails>();
            services.AddScoped<BlBranches>();
            services.AddScoped<BlAddressTypes>();
            services.AddScoped<BlKeyWords>();
            services.AddScoped<BlMainPage>();
            services.AddScoped<BlBanks>();
            services.AddScoped<BlCustomer>();
            services.AddScoped<BlPaymentStatus>();
            services.AddScoped<BlPackage>();
            services.AddScoped<BlBlock>();
            services.AddScoped<BlActivity>();
            services.AddScoped<BlPaymentWay>();
            services.AddScoped<BLGeneral>();
            services.AddScoped<BlMainCategory>();
            services.AddScoped<ResultMessage>();
            services.AddScoped<BLPermission>();
            services.AddScoped<BLUser>();
            services.AddScoped<BlCountry>();
            services.AddScoped<BlRegion>();
            services.AddScoped<BlCity>();
            services.AddScoped<BlEmployee>();
            services.AddScoped<BlDepartments>();
            services.AddScoped<BlJobs>();
            services.AddScoped<BlNationality>();
            services.AddScoped<BlConfiguration>();
            services.AddScoped<BlVendor>();
            services.AddScoped<BlSlider>();
            services.AddScoped<BlInqueries>();
            services.AddScoped<BlShippingCompany>();
            services.AddScoped<BlPaymentConfiguration>();
            services.AddScoped<BlProduct>();
            services.AddScoped<BlOrders>();
            services.AddScoped<BlTokens>();
            services.AddScoped<BlCustomerAddress>();
            services.AddScoped<ResultMessage>();
            services.AddScoped<BlSubscribe>();
            services.AddScoped<BLTransaction>();
            services.AddScoped<BLSite>();
            services.AddScoped<BlDriver>();
            services.AddScoped<OTPService>();
            services.AddScoped<BlDriverBalance>();
            services.AddScoped<BlTransactionType>();
            services.AddScoped<BlHelpQuestions>();
            services.AddScoped<BlDriverSupport>();
            services.AddScoped<BlVendorSupport>();
            services.AddScoped<BlLogTextMessage>();
            services.AddScoped<BlCustomerBalance>();
            services.AddScoped<BlVendorBalance>();
            services.AddScoped<BlInvoice>();
            services.AddScoped<BlDiscount>();
            services.AddScoped<BlListTransfer>();
            services.AddScoped<BlCitiesCovered>();
            services.AddScoped<BlCaptainZone>();
            services.AddScoped<BlProdQuestion>();
            services.AddScoped<BlOrderRate>();
            services.AddScoped<BlDeliverySetting>();
            services.AddScoped<BlRegionCity>();
            services.AddScoped<BlCompanyWorkingHours>();
            services.AddScoped<BlOrderNotesAdmin>();

            #endregion
            //services.AddSingleton<IS3Service, S3Service>();
            #endregion
            services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Fastest);
            services.AddResponseCompression(
                options =>
                {
                    options.Providers.Add<BrotliCompressionProvider>();
                    options.EnableForHttps = true;
                    options.MimeTypes = new[]
                    {  "text/plain",
                        "text/css",
                        "application/javascript",
                        "text/html",
                        "application/xml",
                        "text/xml",
                        "application/json",
                        "text/json", };
                    options.Providers.Add<GzipCompressionProvider>();
                });
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<HtmlEncoder>(HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.BasicLatin, UnicodeRanges.Arabic }));
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        [Obsolete]
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Microsoft.AspNetCore.Hosting.IHostingEnvironment env2)
        {

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/home/error");
                app.UseHsts();
            }
            var requestlocalizationOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(requestlocalizationOptions.Value);
            var serverAddressesFeature =
               app.ServerFeatures.Get<IServerAddressesFeature>();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseRouting();
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCertificateForwarding();
            app.UseStatusCodePages(async context =>
            {
                var response = context.HttpContext.Response;
                if (response.StatusCode == (int)HttpStatusCode.Unauthorized ||
                    response.StatusCode == (int)HttpStatusCode.Forbidden)
                    response.Redirect("/AccessDenied/Unauthorized");
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapAreaControllerRoute(
                 "Setting",
                 "Setting",
                 "{area:exists}/{controller}/{action}/{id?}");
                endpoints.MapAreaControllerRoute(
                 "Identity",
                 "Identity",
                 "{area:exists}/{controller}/{action}/{id?}");
                endpoints.MapAreaControllerRoute(
                  "Permission",
                  "Permission",
                  "{area:exists}/{controller}/{action}/{id?}");
                endpoints.MapAreaControllerRoute(
                    "Places",
                    "Places",
                    "{area:exists}/{controller}/{action}/{id?}");
                endpoints.MapAreaControllerRoute(
                    "Patients",
                    "Patients",
                    "{area:exists}/{controller}/{action}/{id?}");
                endpoints.MapAreaControllerRoute(
                    "Employees",
                    "Employees",
                    "{area:exists}/{controller}/{action}/{id?}");
                endpoints.MapAreaControllerRoute(
                   "Appointment",
                   "Appointment",
                   "{area:exists}/{controller}/{action}/{id?}");
                endpoints.MapAreaControllerRoute(
                 "Vendor",
                 "Vendor",
                 "{area:exists}/{controller}/{action}/{id?}");
                endpoints.MapAreaControllerRoute(
                 "Order",
                 "Order",
                 "{area:exists}/{controller}/{action}/{id?}");
                endpoints.MapControllerRoute(
                    "default", "{area=Site}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                 name: "AccessDenied",
                 pattern: "{controller=AccessDenied}/{action=Unauthorized}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                     "Site",
                     "Site",
                     "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                     );
            });
        }
    }
}
