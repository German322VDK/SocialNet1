using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialNet1.Infrastructure.Interfaces.Based;
using SocialNet1.ViewModels;

namespace SocialNet1.Controllers
{
    [Authorize]
    public class ChatsController : Controller
    {
        private readonly IChat _chat;
        private readonly ILogger<ChatsController> _logger;

        public ChatsController(IChat chat, ILogger<ChatsController> logger)
        {
            _chat = chat;

            _logger = logger;

        }

        public IActionResult Index()
        {
            var userName = User.Identity.Name;

            var chats = _chat.Get(userName);

            _logger.LogInformation($"Тип {userName} смотрит свои переписки");

            return View(new ChatsViewModel 
            { 
                UserName = userName,
                Chats = chats
            });
        }

        public IActionResult Chat(string userName)
        {
            var autorName = User.Identity.Name;

            var chat = _chat.Get(autorName, userName);

            if(chat is null)
            {
                _logger.LogInformation($"Чата между {autorName} и {userName} не существует, щас создам )");

                var result = _chat.CreateChat(autorName, userName);

                if (result)
                {
                    _logger.LogInformation($"Чат между {autorName} и {userName} успешно создан )");

                    chat = _chat.Get(autorName, userName);
                }
                else
                {
                    _logger.LogWarning($"Чат между {autorName} и {userName} не получилось создать, хз что делать (");

                    return RedirectToAction("Index", "News");
                }

            }

            return View(new ChatViewModel 
            { 
                Chat = chat,
                UserName = userName,
                AutorName = autorName
            });
        }
    }
}
