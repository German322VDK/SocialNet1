using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialNet1.Domain.Base;
using SocialNet1.Domain.Identity;
using SocialNet1.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNet1.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        public readonly UserManager<UserDTO> _userManager;
        private readonly SignInManager<UserDTO> _signInManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(UserManager<UserDTO> userManager, SignInManager<UserDTO> signInManager,
            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        #region Register

        [AllowAnonymous]
        public IActionResult Register() =>
            View(new RegisterUserViewModel());

        [HttpPost, ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterUserViewModel Model)
        {
            if (!ModelState.IsValid)
                return View(Model);

            _logger.LogInformation("Регистрация пользователя {0}", Model.UserName);

            using (_logger.BeginScope($"Регистрация пользователя {Model.UserName}"))
            {
                var user = new UserDTO
                {
                    UserName = Model.UserName
                };

                var registration_result = await _userManager.CreateAsync(user, Model.Password);

                if (registration_result.Succeeded)
                {
                    _logger.LogInformation("Пользователь {0} успешно зарегестрирован", Model.UserName);

                    if (Model.AdminRolePassword == Passwords.Admin)
                    {
                        await _userManager.AddToRoleAsync(user, UserStatus.Admin.ToString());

                        _logger.LogInformation("Пользователь {0} наделён ролью {1}", Model.UserName, UserStatus.Admin);
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, UserStatus.User.ToString());

                        _logger.LogInformation("Пользователь {0} наделён ролью {1}", Model.UserName, UserStatus.User);
                    }
                    await _signInManager.SignInAsync(user, false);

                    return RedirectToAction("Index", "Home", new { id = 0 });
                }

                _logger.LogWarning("В процессе регистрации пользователя {0} возникли ошибки :( {1}",
                    Model.UserName, string.Join(",", registration_result.Errors.Select(e => e.Description)));

                foreach (var errors in registration_result.Errors)
                {
                    ModelState.AddModelError("", errors.Description);
                }
            }

            return View(Model);
        }

        #endregion

        #region Login
        [AllowAnonymous]
        public IActionResult Login(string ReturnUrl) =>
            View(new LoginViewModel { ReturnUrl = ReturnUrl });

        [HttpPost, ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel Model)
        {
            if (!ModelState.IsValid)
                return View(Model);

            _logger.LogInformation("Вход пользователя {0}", Model.UserName);

            var login_result = await _signInManager.PasswordSignInAsync(
                Model.UserName,
                Model.Password,
                Model.RememberMe,
#if DEBUG
                false
#else
                true
#endif
                );

            if (login_result.Succeeded)
            {
                _logger.LogInformation("Пользователь {0} успешно зашёл", Model.UserName);

                return LocalRedirect(Model.ReturnUrl ?? "/");
            }

            _logger.LogWarning("В процессе входа пользователя {0} возникли ошибки :( ",
                Model.UserName);

            ModelState.AddModelError("", "Неверное имя пользователя или пароль");

            return View(Model);
        }

        #endregion

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            _logger.LogInformation("Пользователь вышел");

            return RedirectToAction("Index", "Home", new { id = 0 });
        }

        public IActionResult AccessDenied(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;

            return View();
        }
    }
}
