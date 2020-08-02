using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Net;
using System.Security.Claims;

namespace C64.FrontEnd.Pages
{
    public class BasePageModel : PageModel
    {
        protected string UserId => Request.HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        protected string Referer => string.IsNullOrEmpty(Request.Headers["Referer"].ToString()) ? null : Request.Headers["Referer"].ToString();

        protected string RemoteIp
        {
            get
            {
                var context = Request.HttpContext;
                string header = (context.Request.Headers["CF-Connecting-IP"].FirstOrDefault() ?? context.Request.Headers["X-Forwarded-For"].FirstOrDefault());
                if (IPAddress.TryParse(header, out IPAddress ip))
                    return ip.ToString();

                return Request.HttpContext.Connection.RemoteIpAddress.ToString();
            }
        }
    }
}