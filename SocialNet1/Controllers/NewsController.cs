using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialNet1.Infrastructure.Interfaces.Based;

namespace SocialNet1.Controllers
{

    [Authorize]
    public class NewsController : Controller
    {
        private IUser _user;
        private readonly ILogger<NewsController> _logger;
        public NewsController(IUser user, ILogger<NewsController> logger)
        {
            _user = user;
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (_user.Get(User.Identity.Name) is null)
            {
                _logger.LogWarning("Опять эти куки пытаются не существующего пользователя куда-то отправить");
            }

            return View();
        }
    }
}
