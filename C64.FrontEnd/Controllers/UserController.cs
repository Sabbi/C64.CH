using C64.Data.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace C64.FrontEnd.Controllers
{
    public class UserController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<UserController> logger;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public UserController(IConfiguration configuration, ILogger<UserController> logger, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.configuration = configuration;
            this.logger = logger;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password, bool persistent, string returnUrl)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return LocalRedirect(returnUrl);

            try
            {
                await signInManager.SignOutAsync();
            }
            catch { }

            var user = await userManager.FindByEmailAsync(username);

            if (user == null)
                return LocalRedirect(returnUrl);

            var passwordIsCorrect = await userManager.CheckPasswordAsync(user, password);
            var canSignIn = await signInManager.CanSignInAsync(user);

            if (passwordIsCorrect && canSignIn)
            {
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = persistent
                };

                var additionalClaims = new[]
                {
                    new Claim(ClaimTypes.Email, user.Email)
                };

                await signInManager.SignInWithClaimsAsync(user, authProperties, additionalClaims);

                user.LastLogin = DateTime.Now;
                await userManager.UpdateAsync(user);

                logger.LogInformation("User {User} successfully logged in.", user.UserName);

                return LocalRedirect(returnUrl);
            }
            logger.LogWarning("User {User} cannot login (Phase 2), {passwordIsCorrect} {canSignIn}", user.UserName, passwordIsCorrect, canSignIn);

            throw new Exception("Invalid Login");
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return LocalRedirect(Url.Content("~/"));
        }

        [HttpPost]
        public IActionResult TheWall(string password)
        {
            var expectedPassword = configuration.GetValue<string>("SitePassword");

            if (password == expectedPassword)
            {
                HttpContext.Session.SetString("key", "ok");
            }

            return Redirect("/");
        }
    }
}