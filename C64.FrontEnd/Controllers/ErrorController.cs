using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace C64.FrontEnd.Controllers
{
    public class ErrorController : BaseController
    {
        private readonly ILogger<ErrorController> logger;
        public string RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        public string ExceptionMessage { get; set; }

        public ErrorController(ILogger<ErrorController> logger)
        {
            this.logger = logger;
        }

        public IActionResult Index()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;

            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            logger.LogError("Exception on first render {@exceptionHandler} ", exceptionHandlerPathFeature);

            var message = exceptionHandlerPathFeature?.Error.GetType().ToString() + "- " + exceptionHandlerPathFeature?.Error?.Message;

            return View("~/Shared/Error.cshtml", new ErrorControllerViewModel
            {
                Path = exceptionHandlerPathFeature?.Path ?? "unknown",
                Text = message ?? "Unknown error",
                Image = "errorOther.png"
            });
        }
    }

    public class ErrorControllerViewModel
    {
        public string Path { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
    }
}