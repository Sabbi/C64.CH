using C64.Data;
using C64.FrontEnd.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace C64.FrontEnd.Controllers
{
    public class RedirectController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IHttpContextAccessor httpContextAccessor;

        private bool permanentRedirect { get; set; }

        public RedirectController(IConfiguration config, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            permanentRedirect = config.GetValue<bool>("DoPermanentRedirect");
            this.unitOfWork = unitOfWork;
            this.httpContextAccessor = httpContextAccessor;
        }

        // /index.php -> /
        [Route("/index.php")]
        public IActionResult ToIndex()
        {
            return DoRedirect("/");
        }

        [Route("/demos")]
        public IActionResult Demos()
        {
            return DoRedirect("/productions/demos");
        }

        // /about -> /help/about
        [Route("/about")]
        public IActionResult About()
        {
            return DoRedirect("/help/about");
        }

        // /programming/index.php	/programming
        [Route("/programming/index.php")]
        public IActionResult Programming()
        {
            return DoRedirect("/programming");
        }

        // /sources
        [Route("/sources")]
        public IActionResult Sources()
        {
            return DoRedirect("/programming/sourcecodes");
        }

        // /contact -> /help/contact
        [Route("/contact")]
        public IActionResult Contact()
        {
            return DoRedirect("/help/contact");
        }

        // /submit -> /help/contribute
        [Route("/submit")]
        public IActionResult Contribute()
        {
            return DoRedirect("/help/contribute");
        }

        // /demos/parties.php -> /parties
        [Route("/demos/parties.php")]
        public IActionResult Parties()
        {
            return DoRedirect("/parties");
        }

        // /demos/realdetail.php?id=x -> /productions/{id}/{name}
        [Route("/demos/realdetail.php")]
        public async Task<IActionResult> DemoDetail(int id)
        {
            var demo = await unitOfWork.Productions.Get(id);
            if (demo != null)
                return DoRedirect($"/productions/{id}/{demo.Name.UrlEncode()}");

            return DoRedirect($"/productions/{id}");
        }

        // /demos/feedback.php?id=x -> /productions/{id}/{name}
        [Route("/demos/feedback.php")]
        public async Task<IActionResult> Feedback(int id)
        {
            return await DemoDetail(id);
        }

        // /demos/showzip.php?id=x -> /productions/{id}/{name}
        [Route("/demos/showzip.php")]
        public async Task<IActionResult> ShowZip(int id)
        {
            return await DemoDetail(id);
        }

        // /user/personal.php	/favorites/{userId}
        [Route("/user/personal.php")]
        public IActionResult Personal()
        {
            var userId = httpContextAccessor.HttpContext.GetUserId();
            if (!string.IsNullOrEmpty(userId))
                return DoRedirect($"/favorites/{userId}");
            else
                return DoRedirect($"/account");
        }

        // /demos/list.php?group=x&source=group -> /groups/{id}/{name}
        // /demos/list.php?source=toprated -> /productions/demos/toprated
        // /demos/list.php?source=topdownloaded -> /productions/demos/topdownloaded
        // /demos/list.php?source=latest -> /productions/demos/latestadded
        // /demos/list.php?year=x&source=year -> /productions/demos/year/{year}
        // /demos/list.php?groupname=x&source=groupname -> /productions/demos/groupstart/{letter}
        // /demos/list.php?demoname=x&source=demoname -> /productions/demos/namestart/{letter}
        // /demos/list.php?search=search&source=search -> /search/?search={search}
        [Route("/demos/list.php")]
        public async Task<IActionResult> List(string source, int group = 0, string demoname = null, string groupname = null, int year = 0, int partyid = 0, string search = null)
        {
            source = source?.ToLower();

            switch (source)
            {
                case "group":
                    var groupData = await unitOfWork.Groups.Get(group);
                    if (groupData != null)
                        return DoRedirect($"/groups/{group}/{groupData.Name}");
                    else
                        return DoRedirect($"/groups/{group}");

                case "toprated":
                    return DoRedirect("/productions/demos/toprated");

                case "topdownloaded":
                    return DoRedirect("/productions/demos/topdownloaded");

                case "latest":
                    return DoRedirect("/productions/demos/latestadded");

                case "year":
                    return DoRedirect($"/productions/demos/year/{year}");

                case "groupname":
                    return DoRedirect($"/productions/demos/groupname/{groupname}");

                case "demoname":
                    return DoRedirect($"/productions/demos/demoname/{demoname}");

                case "search":
                    return DoRedirect($"/productions/demos/search/?search={search}");

                case "party":
                    return DoRedirect($"/parties/{partyid}");

                default:
                    throw new ArgumentException($"Source {source} invalid");
            }
        }

        // /user/profile.php -> /account/publicprofile
        [Route("/user/profile.php")]
        public IActionResult AccountPublicProfile()
        {
            return DoRedirect($"/account/publicprofile");
        }

        // /register/lostpw.php -> /account/forgotpassword
        [Route("/register/lostpw.php")]
        public IActionResult AccountForgotPassword()
        {
            return DoRedirect($"/account/forgotpassword");
        }

        // /user/changepw.php -> /account/password
        [Route("/user/changepw.php")]
        public IActionResult AccountChangePw()
        {
            return DoRedirect($"/account/password");
        }

        // /register -> /account/register
        [Route("/register")]
        public IActionResult AccountRegister()
        {
            return DoRedirect($"/account/register");
        }

        // /user/changeemail.php -> /account
        // /user/default.php -> /account
        [Route("/user/changeemail.php")]
        [Route("/user/default.php")]
        public IActionResult Account()
        {
            return DoRedirect($"/account");
        }

        private IActionResult DoRedirect(string url)
        {
            return permanentRedirect ? RedirectPermanent(url) : Redirect(url);
        }
    }
}