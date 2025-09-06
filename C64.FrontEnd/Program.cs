using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Formatting.Display;
using Serilog.Sinks.Email;
using System;
using System.Collections.Generic;
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
                var emailConf = new EmailSinkOptions
                {
                    From = loggerConfig.GetValue<string>("EmailSettings:Logger_SenderEmail"),
                    To = new List<string>() { loggerConfig.GetValue<string>("EmailSettings:Logger_RecipientEmail") },
                    Subject = new MessageTemplateTextFormatter(loggerConfig.GetValue<string>("EmailSettings:Logger_MailSubject"), null),
                    Host = loggerConfig.GetValue<string>("EmailSettings:MailServer"),
                    Port = loggerConfig.GetValue<int>("EmailSettings:MailPort"),
                };

                if (!string.IsNullOrEmpty(loggerConfig.GetValue<string>("EmailSettings:SmtpName")))
                    emailConf.Credentials = new NetworkCredential(loggerConfig.GetValue<string>("EmailSettings:SmtpName"), loggerConfig.GetValue<string>("EmailSettings:SmtpPassword"));

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