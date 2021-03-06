using System.Text;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyTestedAspNetCoreApp.Models;
using MyTestedAspNetCoreApp.Services;
using MyTestedAspNetCoreApp.Services.CustomMiddleware;
using MyTestedAspNetCoreApp.Services.Filters;
using MyTestedAspNetCoreApp.Settings;

namespace MyTestedAspNetCoreApp
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.HttpsPolicy;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    using MyTestedAspNetCoreApp.Data;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class Startup
    {
        private readonly IWebHostEnvironment _env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _env = env;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var jwtSettingsSection = Configuration.GetSection("JwtSettings");
            services.Configure<JwtSettings>(jwtSettingsSection);

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            var jwtSettings = jwtSettingsSection.Get<JwtSettings>();
            var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);

            //login s Facebook
            services.AddAuthentication().AddFacebook(options =>
            {
                options.AppId = "kopira se ot Facebook app for developers https://developers.facebook.com/docs/development/create-an-app";
                options.AppSecret = "kopira se ot Facebook app for developers";
            });

            services.AddAuthentication(option =>
                {
                    //option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    //option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(option =>
                {
                    option.RequireHttpsMetadata = false;
                    option.SaveToken = true;
                    option.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                    };

                });

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<ApplicationUser>(options =>
                {
                    // с цел дебъгване опростявам паролата и потвърждаването по имейл на акаунта.
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequiredLength = 6;
                    if (_env.IsProduction())
                    {
                        options.Password.RequiredLength = 10;
                    }
                    options.Password.RequiredUniqueChars = 0;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    // акаунта се заключва след 5 грешни опита за вхид.
                    //options.Lockout.MaxFailedAccessAttempts = 5;
                    // акаунта се заключва за 1 мин.при грешни опити за вхид.
                    //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddControllersWithViews(configure =>
            {
                configure.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                //global registration :
                configure.Filters.Add(new MyAuthFilter());
                configure.Filters.Add(new MyResourceFilter());
                configure.Filters.Add(new MyResourceFilter());
                configure.Filters.Add(new MyAddHeaderActionFilterAttribute());
                configure.Filters.Add(new MyExceptionFilter());
                configure.Filters.Add(new MyResultFilterAttribute());

                // or local registration with attribute in the concrete controller or only in concrete action. In this case in InfoController.
            });
            services.AddRazorPages();

            services.AddTransient<IShortStringService, ShortStringService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // example Map() middleware
            app.Map("/test-map-middleware", app =>
            {
                app.UseWelcomePage();
            });

            app.UseMyMiddleware();

            if (env.IsDevelopment())
            {
                app.UseStatusCodePagesWithRedirects("/Home/StatusCodeError?errorCode={0}");
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllerRoute(
                //name: "areas",
                //pattern:"{area=exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
