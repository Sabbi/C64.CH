//using Serilog;
//using Serilog.Configuration;
//using Serilog.Events;
//using Serilog.Sinks.Email;
//using System;
//using System.Net;

//namespace C64.FrontEnd
//{
//    public static class SerilogEmailExtensions
//    {
//        public static LoggerConfiguration EmailCustom(this LoggerSinkConfiguration sinkConfiguration,
//               string fromEmail,
//               string toEmail,
//               string enableSsl,
//               string mailSubject,
//               string isBodyHtml,
//               string mailServer,
//               string networkCredentialuserName,
//               string networkCredentialpassword,
//               string smtpPort,
//               string outputTemplate,
//               string batchPostingLimit,
//               string periodMinutes,
//               string restrictedToMinimumLevel)
//        {
//            return sinkConfiguration.Email(new EmailConnectionInfo
//            {
//                FromEmail = fromEmail,
//                ToEmail = toEmail,
//                EnableSsl = GetBoolean(enableSsl),
//                EmailSubject = mailSubject,
//                IsBodyHtml = GetBoolean(isBodyHtml),
//                MailServer = mailServer,
//                NetworkCredentials = new NetworkCredential(networkCredentialuserName, networkCredentialpassword),
//                Port = GetInt(smtpPort)
//            }, outputTemplate, GetLevel(restrictedToMinimumLevel),
//                GetInt(batchPostingLimit), TimeSpan.FromMinutes(GetInt(periodMinutes))
//            );
//        }

//        //The system hated converting the string inputs inline so I added the conversion methods:

//        private static int GetInt(string instring)
//        {
//            return int.TryParse(instring, out var result) ? result : 0;
//        }

//        private static bool GetBoolean(string instring)
//        {
//            return bool.TryParse(instring, out var result) && result;
//        }

//        private static LogEventLevel GetLevel(string restrictedtominimumlevel)
//        {
//            return Enum.TryParse(restrictedtominimumlevel, true,
//                out LogEventLevel level) ? level : LogEventLevel.Warning;
//        }
//    }
//}