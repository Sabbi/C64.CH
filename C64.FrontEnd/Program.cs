using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.Email;
using System;
using System.Net;

namespace C64.FrontEnd
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var loggerConfig = new ConfigurationBuilder()
                // .AddJsonFile("loggersettings.json")
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
                .Build();

            if (loggerConfig.GetValue<bool>("EmailSettings:EmailEnabled"))
            {
                var emailConf = new EmailConnectionInfo
                {
                    FromEmail = loggerConfig.GetValue<string>("EmailSettings:Logger_SenderEmail"),
                    ToEmail = loggerConfig.GetValue<string>("EmailSettings:Logger_RecipientEmail"),
                    EmailSubject = loggerConfig.GetValue<string>("EmailSettings:Logger_MailSubject"),
                    MailServer = loggerConfig.GetValue<string>("EmailSettings:MailServer"),
                    Port = loggerConfig.GetValue<int>("EmailSettings:MailPort"),
                    EnableSsl = loggerConfig.GetValue<bool>("EmailSettings:EnableSsl"),
                    NetworkCredentials = new NetworkCredential(loggerConfig.GetValue<string>("EmailSettings:SmtpName"), loggerConfig.GetValue<string>("EmailSettings:SmtpPassword"))
                };

                Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(loggerConfig)
                    .WriteTo.Email(emailConf, restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error)
                    .CreateLogger();
            }
            else
            {
                Log.Logger = new LoggerConfiguration()
                  .ReadFrom.Configuration(loggerConfig)
                  .CreateLogger();
            }

            // Enable to see Serilogs' own log in the console (f.ex. to debug emails)
            if (loggerConfig.GetValue<bool>("Serilog:SelfLogging"))
                Serilog.Debugging.SelfLog.Enable(Console.WriteLine);

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