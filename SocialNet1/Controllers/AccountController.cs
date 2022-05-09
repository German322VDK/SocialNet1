using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using SocialNet1.Domain.Base;
using SocialNet1.Domain.Identity;
using SocialNet1.Infrastructure.Interfaces.Based;
using SocialNet1.Infrastructure.Methods;
using SocialNet1.ViewModels;
using System;
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
        private readonly IUser _user;
        private readonly IChat _chat;
        private readonly IEmailConfirm _emailConfirm;
        private readonly IGroup _group;

        public AccountController(UserManager<UserDTO> userManager, SignInManager<UserDTO> signInManager,
            ILogger<AccountController> logger, IUser user, IChat chat, IEmailConfirm emailConfirm, IGroup group)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _user = user;
            _chat = chat;
            _emailConfirm = emailConfirm;
            _group = group;
        }

        #region Register

        [AllowAnonymous]
        public IActionResult RegisterStart()
        {
            _logger.LogInformation("Кто-то пытается зарегестрироваться!");

            return View(new RegisterStartViewModel
            {

            });
        }

        [AllowAnonymous]
        public IActionResult Register(RegisterStartViewModel model)
        {
            _logger.LogInformation($"Тип с почтой {model.Email} пытается зарегестрироваться!");

            return View(new RegisterUserViewModel
            {
                Email = model.Email
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterUserViewModel Model)
        {
            if(Model is null || string.IsNullOrEmpty(Model.Email) || _emailConfirm.Get(Model.Email) is null)
            {
                _logger.LogWarning("Какой то конченный хотел меня обмануть");

                return RedirectToAction("RegisterStart", "Account");
            }

            var userNameIsExist = CheckUserName(Model.UserName);

            if (userNameIsExist)
            {
                _logger.LogInformation($"Тип с почтой {Model.Email} пытается забрать существующий {nameof(Model.UserName)}: {Model.UserName}");

                ModelState["UserName"].Errors.Add(new Exception("Логин обязателен и не должен использоваться другими"));
                ModelState["UserName"].ValidationState = ModelValidationState.Invalid;
            }
                

            if (!ModelState.IsValid)
                return View(Model);

            _logger.LogInformation("Регистрация пользователя {0}", Model.UserName);

            using (_logger.BeginScope($"Регистрация пользователя {Model.UserName}"))
            {
                var user = new UserDTO
                {
                    UserName = Model.UserName,
                    FirstName = Model.FirstName,
                    SecondName = Model.SecondName,
                    Status = "",
                    Email = Model.Email,
                    SocNetItems = new SocNetEntityUser
                    {
                        X = 0,
                        Y = 0,
                        CurrentImage = 1
                    },
                };

                var registration_result = await _userManager.CreateAsync(user, Model.Password);

                if (registration_result.Succeeded)
                {
                    _logger.LogInformation("Пользователь {0} успешно зарегестрирован", Model.UserName);

                    if (string.IsNullOrEmpty(Model.AdminRolePassword) || Model.AdminRolePassword != Passwords.Admin)
                    {
                        await _userManager.AddToRoleAsync(user, UserStatus.User.ToString());

                        _logger.LogInformation("Пользователь {0} наделён ролью {1}", Model.UserName, UserStatus.User);
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, UserStatus.Admin.ToString());

                        _logger.LogInformation("Пользователь {0} наделён ролью {1}", Model.UserName, UserStatus.Admin);
                    }
                    await _signInManager.SignInAsync(user, false);

                    var arr = NewImageMethods.GetByteArrFromFile("wwwroot/photo/def/anon.jpg");

                    _user.AddPhoto(arr, user.UserName);

                    var result = _chat.CreateChat(Model.UserName, Model.UserName);

                    if (!result)
                    {
                        _logger.LogWarning($"Не удалось типу {Model.UserName} содать чат с самим собой (");
                    }
                    else
                    {
                        _logger.LogInformation($"Удалось типу {Model.UserName} содать чат с самим собой");
                    }

                    var addGrouPresult = _group.Sub("offgroup", user.UserName);

                    if (!addGrouPresult)
                    {
                        _logger.LogWarning($"Не удалось типа {Model.UserName} подписать на официальную группу (");
                    }
                    else
                    {
                        _logger.LogInformation($"Удалось типа {Model.UserName} подписать на официальную группу");
                    }

                    return RedirectToAction("Index", "News");
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

        public bool CheckUserName(string username) =>
            _user.Get(username) is not null ? true : false;


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
                true,
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

            return RedirectToAction("Index", "News");
        }

        public IActionResult AccessDenied(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;

            var userName = User.Identity.Name;

            if (userName is null || _user.Get(userName) is null)
            {
                _logger.LogWarning("Опять эти куки пытаются не существующего пользователя куда-то отправить");
            }

            _logger.LogWarning($"Тип {userName} хотел попасть на {ReturnUrl}");

            return View();
        }
    }
}
