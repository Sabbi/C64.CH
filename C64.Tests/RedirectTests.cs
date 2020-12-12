using C64.Data;
using C64.Data.Entities;
using C64.FrontEnd.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using System.IO;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace C64.Tests
{
    public class RedirectTests
    {
        private Mock<IUnitOfWork> unitOfWorkMock;

        public RedirectTests()
        {
            unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(p => p.Productions.Get(It.IsAny<int>())).ReturnsAsync(new Production { Id = 1, Name = "TestProduction" });
            unitOfWorkMock.Setup(p => p.Groups.Get(It.IsAny<int>())).ReturnsAsync(new Group { Id = 1, Name = "TestGroup" });
        }

        private RedirectController CreateController(bool loggedIn = true)
        {
            var appSettings = "{\"DoPermanentRedirect\": false}";
            var builder = new ConfigurationBuilder();
            builder.AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(appSettings)));
            var config = builder.Build();

            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();

            mockHttpContextAccessor.Object.HttpContext = new DefaultHttpContext();

            var claim = new Mock<Claim>(ClaimTypes.NameIdentifier, "11111111-1111-1111-111111111111");

            var claimPrinzipal = new Mock<ClaimsPrincipal>();

            if (loggedIn)
                claimPrinzipal.Setup(p => p.FindFirst(ClaimTypes.NameIdentifier)).Returns(claim.Object);

            mockHttpContextAccessor.Setup(p => p.HttpContext.User).Returns(claimPrinzipal.Object);

            return new RedirectController(config, unitOfWorkMock.Object, mockHttpContextAccessor.Object);
        }

        [Fact]
        // /demos -> /productions/demos
        public void Demos_main()
        {
            var redirectController = CreateController();
            var result = (RedirectResult)redirectController.Demos();

            Assert.False(result.Permanent);
            Assert.Equal("/productions/demos", result.Url);
        }

        [Fact]
        // /demos/realdetail.php?id=x -> /productions/{id}/{name}
        public async Task Demo_detail()
        {
            var redirectController = CreateController();
            var result = (RedirectResult)await redirectController.DemoDetail(1);

            Assert.Equal("/productions/1/TestProduction", result.Url);
        }

        [Fact]
        // /demos/list.php?group=x&source=group -> /groups/{id}/{name}
        public async Task List_by_group()
        {
            var redirectController = CreateController();
            var result = (RedirectResult)await redirectController.List("group", group: 1);
            Assert.Equal("/groups/1/TestGroup", result.Url);
        }

        [Fact]
        // /demos/list.php?demoname=x&source=demoname -> /productions/demos/namestart/{letter}
        public async Task List_by_starting_letter()
        {
            var redirectController = CreateController();
            var result = (RedirectResult)await redirectController.List("demoname", demoname: "a");
            Assert.Equal("/productions/demos/namestart/a", result.Url);
        }

        [Fact]
        // /demos/list.php?groupname=x&source=groupname -> /productions/demos/groupstart/{letter}
        public async Task List_by_starting_letter_group()
        {
            var redirectController = CreateController();
            var result = (RedirectResult)await redirectController.List("groupname", groupname: "a");
            Assert.Equal("/productions/demos/groupstart/a", result.Url);
        }

        [Fact]
        // /demos/list.php?year=x&source=year -> /productions/demos/year/{year}
        public async Task List_by_year()
        {
            var redirectController = CreateController();
            var result = (RedirectResult)await redirectController.List("year", year: 1999);
            Assert.Equal("/productions/demos/year/1999", result.Url);
        }

        [Fact]
        // /demos/list.php?search=search&source=search -> /search/?search={search}
        public async Task List_by_search()
        {
            var redirectController = CreateController();
            var result = (RedirectResult)await redirectController.List("search", search: "leterm");
            Assert.Equal("/search/?search=leterm", result.Url);
        }

        [Fact]
        // /demos/list.php?source=latest -> /productions/demos/latestadded
        public async Task Latest_Demos()
        {
            var redirectController = CreateController();
            var result = (RedirectResult)await redirectController.List("latest");
            Assert.Equal("/productions/demos/latestadded", result.Url);
        }

        [Fact]
        // /demos/list.php?source=topdownloaded -> /productions/demos/topdownloaded
        public async Task Top_Downloaded()
        {
            var redirectController = CreateController();
            var result = (RedirectResult)await redirectController.List("topdownloaded");
            Assert.Equal("/productions/demos/topdownloaded", result.Url);
        }

        [Fact]
        // /demos/list.php?source=toprated -> /productions/demos/toprated
        public async Task Top_Voted()
        {
            var redirectController = CreateController();
            var result = (RedirectResult)await redirectController.List("toprated");
            Assert.Equal("/productions/demos/toprated", result.Url);
        }

        [Fact]
        // /programming/index.php	/programming
        public void Programming()
        {
            var redirectController = CreateController();
            var result = (RedirectResult)redirectController.Programming();

            Assert.Equal("/programming", result.Url);
        }

        [Fact]
        // /demos/parties.php -> /parties
        public void Parties()
        {
            var redirectController = CreateController();
            var result = (RedirectResult)redirectController.Parties();

            Assert.Equal("/parties", result.Url);
        }

        [Fact]
        // /contact -> /help/contact
        public void Contact()
        {
            var redirectController = CreateController();
            var result = (RedirectResult)redirectController.Contact();

            Assert.Equal("/help/contact", result.Url);
        }

        [Fact]
        // /about -> /help/about
        public void About()
        {
            var redirectController = CreateController();
            var result = (RedirectResult)redirectController.About();

            Assert.Equal("/help/about", result.Url);
        }

        [Fact]
        // /register/	/account/register
        public void Register()
        {
            var redirectController = CreateController();
            var result = (RedirectResult)redirectController.AccountRegister();

            Assert.Equal("/account/register", result.Url);
        }

        [Fact]
        // /register/lostpw.php	/account/forgotpassword
        public void Forgot_Password()
        {
            var redirectController = CreateController();
            var result = (RedirectResult)redirectController.AccountForgotPassword();

            Assert.Equal("/account/forgotpassword", result.Url);
        }

        [Fact]
        // /user/default.php	/account
        public void Control_center()
        {
            var redirectController = CreateController();
            var result = (RedirectResult)redirectController.Account();

            Assert.Equal("/account", result.Url);
        }

        [Fact]
        // /user/profile.php	/account/publicprofile
        public void Edit_profile()
        {
            var redirectController = CreateController();
            var result = (RedirectResult)redirectController.AccountPublicProfile();

            Assert.Equal("/account/publicprofile", result.Url);
        }

        [Fact]
        // /user/personal.php	/favorites/{userId}
        public void Personal()
        {
            var controller = CreateController();
            var result = (RedirectResult)controller.Personal();
            Assert.True(result.Url == "/favorites/11111111-1111-1111-111111111111");
        }

        [Fact]
        // /user/personal.php	/favorites/{userId}
        public void PersonalNotLoggedIn()
        {
            var controller = CreateController(false);
            var result = (RedirectResult)controller.Personal();
            Assert.Equal("/account", result.Url);
        }

        [Fact]
        // /user/changepw.php	/account/password
        public void Change_password()
        {
            var controller = CreateController(false);
            var result = (RedirectResult)controller.AccountChangePw();
            Assert.Equal("/account/password", result.Url);
        }

        [Fact]
        // /user/changeemail.php	/account
        public void Change_email()
        {
            var redirectController = CreateController();
            var result = (RedirectResult)redirectController.Account();

            Assert.Equal("/account", result.Url);
        }
    }
}