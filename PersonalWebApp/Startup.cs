using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using PersonalWebApp.Interfaces;
using PersonalWebApp.Models;
using PersonalWebApp.Resources;
using PersonalWebApp.Services;

namespace PersonalWebApp
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<AppDbContext>(options =>
            {
                options.UseSqlServer(_config.GetConnectionString("DefaultConnection"));
                options.EnableSensitiveDataLogging();
            });

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 10;
                options.Password.RequiredUniqueChars = 3;
                options.Tokens.EmailConfirmationTokenProvider = "CustomEmailConfirmation";
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
            }).AddEntityFrameworkStores<AppDbContext>();
            services.AddLocalization(o =>
            {
                o.ResourcesPath = "Resources";
            });
            services.AddMvc().AddDataAnnotationsLocalization(o => {
                var type = typeof(SharedResource);
                var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
                var factory = services.BuildServiceProvider().GetService<IStringLocalizerFactory>();
                var localizer = factory.Create("SharedResource", assemblyName.Name);
                o.DataAnnotationLocalizerProvider = (t, f) => localizer;
            });
            services.AddTransient<IGenericRepository<Project>, GenericRepository<Project>>();
            services.AddTransient<IGenericRepository<Category>, GenericRepository<Category>>();
            services.AddTransient<IGenericRepository<About>, GenericRepository<About>>();
            services.AddTransient<IGenericRepository<Education>, GenericRepository<Education>>();
            services.AddTransient<IGenericRepository<AboutSkill>, GenericRepository<AboutSkill>>();
            services.AddTransient<IGenericRepository<Skill>, GenericRepository<Skill>>();
            services.AddTransient<IGenericRepository<Experience>, GenericRepository<Experience>>();
            services.AddTransient<IGenericRepository<Blog>, GenericRepository<Blog>>();
            services.AddTransient<IGenericRepository<ContactRequest>, GenericRepository<ContactRequest>>();
            services.AddTransient<IGenericRepository<General>, GenericRepository<General>>();
            services.AddTransient<IGenericRepository<Page>, GenericRepository<Page>>();
            services.AddSingleton<SharedViewLocalizer>();

            //services.AddScoped<IProjectRepository, SqlProjectRepository>();
            //services.AddScoped<ICategoryRepository, SqlCategoryRepository>();
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/panel";
                options.ReturnUrlParameter = "AccessDenied";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Areas/Panel/wwwroot")),
                RequestPath = new PathString("/panel")
            });
            app.UseAuthentication();
            app.UseBrowserLink();
            var options = app.ApplicationServices
              .GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);
            IList<CultureInfo> supportedCultures = new List<CultureInfo>
{
    new CultureInfo("tr"),
    new CultureInfo("en"),
};
            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            };
            var requestProvider = new RouteDataRequestCultureProvider();
            localizationOptions.RequestCultureProviders.Insert(0, requestProvider);

            app.UseRouter(routes =>
            {
                routes.MapMiddlewareRoute("{culture=en-US}/{*mvcRoute}", subApp =>
                {
                    subApp.UseRequestLocalization(localizationOptions);

                    subApp.UseMvc(mvcRoutes =>
                    {
                        mvcRoutes.MapRoute(
                         name: "areas",
                         template: "{area:exists}/{controller=Panel}/{action=Index}/{id?}");

                        mvcRoutes.MapRoute(
                            name: "default",
                            template: "{culture=en-US}/{controller=Home}/{action=Index}/{id?}");
                    });
                });
            });
            app.UseMvc();
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "areas",
            //        template: "{area:exists}/{controller=Panel}/{action=Index}/{id?}"
            //    );
            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=Home}/{action=Index}/{id?}"
            //    );
            //});
        }
    }
}
