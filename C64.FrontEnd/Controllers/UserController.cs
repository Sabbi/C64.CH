using C64.Data.Entities;
using C64.FrontEnd.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace C64.FrontEnd.Controllers
{
    public class UserController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<UserController> logger;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly HttpContextAccessor httpContextAccessor;

        public UserController(IConfiguration configuration, ILogger<UserController> logger, UserManager<User> userManager, SignInManager<User> signInManager, HttpContextAccessor httpContextAccessor)
        {
            this.configuration = configuration;
            this.logger = logger;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.httpContextAccessor = httpContextAccessor;
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

                await signInManager.SignInAsync(user, authProperties);

                user.LastLogin = DateTime.Now;
                await userManager.UpdateAsync(user);

                logger.LogInformation("User {User} successfully logged in.", user.UserName);

                return LocalRedirect(returnUrl);
            }
            logger.LogWarning("User {User} cannot login (Phase 2), {passwordIsCorrect} {canSignIn}", user.UserName, passwordIsCorrect, canSignIn);

            throw new Exception("Invalid Login");
        }

        [HttpPost]
        public async Task<IActionResult> Register(string username, string email, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(email))
                throw new InvalidEnumArgumentException();

            try
            {
                await signInManager.SignOutAsync();
            }
            catch { }

            var user = new User() { UserName = username, Email = email };

            var result = await userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, false);
                return LocalRedirect("/account");
            }

            throw new Exception(string.Join(" - ", result.Errors.Select(p => p.Description)));
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return LocalRedirect(Url.Content("~/"));
        }

        public async Task<IActionResult> Relogin(string returnUrl)
        {
            var user = await userManager.FindByIdAsync(httpContextAccessor.HttpContext.GetUserId());
            await signInManager.RefreshSignInAsync(user);

            return LocalRedirect(returnUrl ?? "/");
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
                return RedirectToPage("/");

            code = code.Replace(" ", "+");

            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
                throw new InvalidOperationException($"Unable to load user with ID '{userId}'.");

            var result = await userManager.ConfirmEmailAsync(user, code);
            if (!result.Succeeded)
                throw new InvalidOperationException($"Error confirming email for user with ID '{userId}':");

            return Redirect("/account/?confirmed=true");
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