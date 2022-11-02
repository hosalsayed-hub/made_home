using Homemade.Api.Model;
using Homemade.Api.Models;
using Homemade.BLL;
using Homemade.BLL.Customer;
using Homemade.BLL.Driver;
using Homemade.BLL.Emp;
using Homemade.BLL.General;
using Homemade.BLL.Order;
using Homemade.BLL.Purchase;
using Homemade.BLL.Setting;
using Homemade.BLL.SMS;
using Homemade.BLL.Vendor;
using Homemade.DAL.Contract;
using Homemade.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Homemade.Api
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

            services.AddDbContext<DAL.HomemadeContext>(options => options
           .UseSqlServer(Configuration.GetConnectionString("HomemadeDBConnection")
                
           ));

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
            })
    .AddEntityFrameworkStores<DAL.HomemadeContext>()
    .AddDefaultTokenProviders();

            services.AddControllers();

            services.AddMvc(properties =>
            {
                properties.ModelBinderProviders.Insert(0, new JsonModelBinderProvider());
            });
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(typeof(IUOW), typeof(DAL.UOW));
            services.AddScoped(typeof(IGenericRepository<>), typeof(DAL.GenericRepository<>));
            services.AddScoped<BLGeneral>();
            services.AddScoped<FixedResultMessage>();
            services.AddScoped<BlRegion>();
            services.AddScoped<BlBlock>();
            services.AddScoped<BlAddressTypes>();
            services.AddScoped<BlCity>();
            services.AddScoped<BlCountry>();
            services.AddScoped<BlDepartments>();
            services.AddScoped<BlCustomerAddress>();
            services.AddScoped<BlVendor>();
            services.AddScoped<BlProduct>();
            services.AddScoped<BlNationality>();
            services.AddScoped<BlCustomerBalance>();
            services.AddScoped<BlVendorBalance>();
            services.AddScoped<BlCustomer>();
            services.AddScoped<BlTokens>();
            services.AddScoped<OTPService>();
            services.AddScoped<BlActivity>();
            services.AddScoped<BlPackage>();
            services.AddScoped<BLTransaction>();
            services.AddScoped<BlBanks>();
            services.AddScoped<BLPermission>();
            services.AddScoped<BlOrders>();
            services.AddScoped<BlDriver>();
            services.AddScoped<BlDriverBalance>();
            services.AddScoped<BlTransactionType>();
            services.AddScoped<BlHelpQuestions>();
            services.AddScoped<BlDriverSupport>();
            services.AddScoped<BlVendorSupport>();
            services.AddScoped<BlEmployee>();
            services.AddScoped<BlLogTextMessage>();
            services.AddScoped<BlMainPageDetails>();
            services.AddScoped<BlRegionCity>();
            services.AddScoped<BlConfiguration>();
            services.AddScoped<BLUser>();
            services.AddScoped<BlCompanyWorkingHours>();
            // Adds a default in-memory implementation of IDistributedCache.
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromSeconds(10);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
