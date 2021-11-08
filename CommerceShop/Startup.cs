using Commerce.Services;
using Commerce.Services.Caching;
using CommerceShop.Data;
using Commerce.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Localization.Routing;

namespace CommerceShop
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
            // adding localization
            services.AddLocalization(opts => {
                opts.ResourcesPath = "Resources";
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<ApplicationDbContext>();

            // adding identity
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            // adding memory cashing
            services.AddMemoryCache();

            // multi language start
            services.AddControllersWithViews().AddViewLocalization(opts => { opts.ResourcesPath = "Resources"; })
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();
            services.Configure<RequestLocalizationOptions>(opts => {
                var supportedCultures = new List<CultureInfo> {
                    new CultureInfo("en"),  // English
                    new CultureInfo("ar"),  // Arabic
                  };

                opts.DefaultRequestCulture = new RequestCulture("en");
                // Formatting numbers, dates, etc.
                opts.SupportedCultures = supportedCultures;
                // UI strings that we have localized.
                opts.SupportedUICultures = supportedCultures;
            });
            // multi language end

            services.AddRazorPages();

            // adding dependincy
            services.AddScoped<ICachingService, CachingService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped(typeof(IEntityRepo<>), typeof(EntityRepo<>));
            //services.AddScoped(typeof(IEntityCultureRepo<>), typeof(EntityCultureRepo<>));
            //services.AddScoped<IProductCultureService, ProductCultureService>();
            //services.AddScoped<IEntityRepo<BaseEntity>, EntityRepo<BaseEntity>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            // multi language start
            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);
            // multi language end

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            // this to add the culture in the url
            //IList<CultureInfo> supportedCulture = new List<CultureInfo>
            //{
            //    new CultureInfo("en"),
            //    new CultureInfo("ar")
            //};

            //var requestLocalizationOptions = new RequestLocalizationOptions()
            //{
            //    DefaultRequestCulture = new RequestCulture("en"),
            //    SupportedCultures = supportedCulture,
            //    SupportedUICultures = supportedCulture
            //};

            //var requestProvider = new RouteDataRequestCultureProvider();
            //requestLocalizationOptions.RequestCultureProviders.Insert(0, requestProvider);
            //app.UseRequestLocalization(requestLocalizationOptions);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                    //pattern: "{culture=en}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
