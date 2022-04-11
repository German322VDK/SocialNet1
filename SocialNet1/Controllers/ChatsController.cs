using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialNet1.Infrastructure.Interfaces.Based;
using SocialNet1.ViewModels;
using System.Linq;

namespace SocialNet1.Controllers
{
    [Authorize]
    public class ChatsController : Controller
    {
        private readonly IChat _chat;
        private readonly IUser _user;
        private readonly ILogger<ChatsController> _logger;

        public ChatsController(IChat chat, IUser user, ILogger<ChatsController> logger)
        {
            _chat = chat;
            _user = user;
            _logger = logger;

        }

        public IActionResult Index()
        {
            var userName = User.Identity.Name;

            var user = _user.Get(userName);

            if (user is null)
            {
                _logger.LogWarning("Опять эти куки пытаются не существующего пользователя куда-то отправить");
            }

            var chats = _chat.Get(userName);

            var allFriends = user.Friends.Select(el => _user.Get(el.FriendName)).ToList();

            var friends = allFriends
                .Where(el => _user.IsFriend(el.UserName, user.UserName))
                .ToList();

            _logger.LogInformation($"Тип {userName} смотрит свои переписки");

            return View(new ChatsViewModel 
            { 
                UserName = userName,
                Chats = chats,
                Friends = friends
            });
        }

        public IActionResult Chat(string userName)
        {
            if (_user.Get(User.Identity.Name) is null)
            {
                _logger.LogWarning("Опять эти куки пытаются не существующего пользователя куда-то отправить");
            }

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
