using Blazored.Modal;
using Blazored.Toast;
using C64.Data;
using C64.Data.Entities;
using C64.FrontEnd.Helpers;
using C64.Services;
using C64.Services.Archive;
using C64.Services.Storage;
using C64.Services.Tweeter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MySqlConnector.Logging;
using Serilog;
using System;
using System.Globalization;

namespace C64.FrontEnd
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();

            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), p => p.MigrationsAssembly("C64.Data")), contextLifetime: ServiceLifetime.Transient);

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-.,_@+ /\\[](){}#*!=$Ј|адьцйифоквбуфтс^'Лез&:;КиноМанСанклартаКГригорийИЙ";
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 3;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
              .AddEntityFrameworkStores<ApplicationDbContext>()
              .AddDefaultTokenProviders();

            services.AddBlazoredModal();
            services.AddBlazoredToast();

            services.AddMvc(config =>
            {
                // https://github.com/aspnet/AspNetCore/issues/5055
                config.EnableEndpointRouting = false;
            });

            services.AddScoped<HttpContextAccessor>();
            services.AddOptions();

            // Inject our services.
            // -------------------------------------------------------------------------------------------------------------
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));

            var enableEmail = Configuration.GetValue<bool>("EmailSettings:EmailEnabled");

            if (enableEmail)
                services.AddTransient<IEmailSender, MailKitSender>();
            else
                services.AddTransient<IEmailSender, ToLogEmailSender>();

            services.AddTransient<IFileStorageService, DbFileStorageService>();
            services.AddTransient<IArchiveService, SharpZipArchiveService>();
            services.AddTransient<IFallbackArchiveService, FallbackArchiveService>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddSingleton<IPasswordHasher, Sha256PasswordHasher>();
            services.AddSingleton<NotifierService>();

            services.AddHostedService<StatsUpdater>();

            services.AddSignalR(hubOptions =>
            {
                hubOptions.MaximumReceiveMessageSize = 1024 * 1024;
            });

            if (!string.IsNullOrEmpty(Configuration.GetValue<string>("Twitter:ConsumerKey")))
            {
                var consumerKey = Configuration.GetValue<string>("Twitter:ConsumerKey");
                var consumerSecret = Configuration.GetValue<string>("Twitter:ConsumerSecret");
                var accessToken = Configuration.GetValue<string>("Twitter:AccessToken");
                var accessTokenSecret = Configuration.GetValue<string>("Twitter:AccessTokenSecret");
                var twitterLogo = Configuration.GetValue<string>("Twitter:Logo");

                services.AddTransient<ITweeter>(x => new DefaultTweeter(consumerKey, consumerSecret, accessToken, accessTokenSecret, twitterLogo, x.GetRequiredService<ILogger<DefaultTweeter>>()));
                services.AddTransient<IPictureTweeter>(x => new DefaultTweeter(consumerKey, consumerSecret, accessToken, accessTokenSecret, twitterLogo, x.GetRequiredService<ILogger<DefaultTweeter>>()));
            }
            else
            {
                services.AddTransient<ITweeter, NullTweeter>();
                services.AddTransient<IPictureTweeter, NullTweeter>();
            }

            // Use en-US as culture, but tweak some to a more readable format.
            // -------------------------------------------------------------------------------------------------------------
            var cultureInfo = new CultureInfo("en-US");

            cultureInfo.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
            cultureInfo.DateTimeFormat.LongTimePattern = "HH:mm:ss";
            cultureInfo.NumberFormat.NumberGroupSeparator = "'";
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseRouting();
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                 name: "default",
                 template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });

            var loggerFactory = app.ApplicationServices.GetService<ILoggerFactory>();
            MySqlConnectorLogManager.Provider = new MicrosoftExtensionsLoggingLoggerProvider(loggerFactory);

            app.UseSerilogRequestLogging();
        }
    }
}