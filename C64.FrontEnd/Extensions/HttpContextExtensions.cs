using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;

namespace C64.FrontEnd.Extensions
{
    public static class HttpContextExtensions
    {
        public static string GetUserId(this HttpContext httpContext)
        {
            return httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public static string Referer(this HttpContext httpContext)
        {
            return string.IsNullOrEmpty(httpContext.Request.Headers["Referer"].ToString()) ? null : httpContext.Request.Headers["Referer"].ToString();
        }

        public static string RemoteIp(this HttpContext httpContext)
        {
            var context = httpContext.Request.HttpContext;
            string header = (context.Request.Headers["CF-Connecting-IP"].FirstOrDefault() ?? context.Request.Headers["X-Forwarded-For"].FirstOrDefault());
            if (IPAddress.TryParse(header, out IPAddress ip))
                return ip.ToString();

            return httpContext.Request.HttpContext.Connection.RemoteIpAddress.ToString();
        }

        public static string GetUserName(this HttpContext httpContext)
        {
            return httpContext.User.FindFirstValue(ClaimTypes.Name);
        }

        public static bool CanEdit(this HttpContext httpContext)
        {
            return httpContext.User.IsInRole("Moderator") || httpContext.User.IsInRole("Editor");
        }

        public static IEnumerable<string> GetRoles(this HttpContext httpContext)
        {
            return httpContext.User.Claims.Where(p => p.Type == ClaimTypes.Role).Select(p => p.Value);
        }

        public static bool CheckRole(this HttpContext httpContext, string role)
        {
            return httpContext.GetRoles().Any(p => p.Equals(role, StringComparison.OrdinalIgnoreCase));
        }
    }
}