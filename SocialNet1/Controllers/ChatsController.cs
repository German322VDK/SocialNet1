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

            return View(new ChatViewModel 
            { 
                UserName = userName,
                Chats = chats
            });
        }
    }
}
