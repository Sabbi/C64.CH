using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.Email;
using System.Net;

namespace C64.FrontEnd
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var loggerConfig = new ConfigurationBuilder()
                .AddJsonFile("loggersettings.json")
                .Build();

            var emailConf = new EmailConnectionInfo
            {
                FromEmail = loggerConfig.GetValue<string>("Mailer:FromEmail"),
                ToEmail = loggerConfig.GetValue<string>("Mailer:ToEmail"),
                MailServer = loggerConfig.GetValue<string>("Mailer:MailServer"),
                EmailSubject = loggerConfig.GetValue<string>("Mailer:EmailSubject"),
                Port = loggerConfig.GetValue<int>("Mailer:Port"),
                EnableSsl = loggerConfig.GetValue<bool>("Mailer:EnableSsl"),
                NetworkCredentials = new NetworkCredential(loggerConfig.GetValue<string>("Mailer:UserName"), loggerConfig.GetValue<string>("Mailer:Password"))
            };

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(loggerConfig)
                .WriteTo.Email(emailConf, restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error)
                .CreateLogger();

            var builder = CreateHostBuilder(args);
            builder.Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}