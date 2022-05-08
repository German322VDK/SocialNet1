using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialNet1.Domain.Identity;
using SocialNet1.Infrastructure.Interfaces.Based;
using SocialNet1.ViewModels;
using System.Linq;

namespace SocialNet1.Controllers
{
    [Authorize]
    public class FriendsController : Controller
    {
        private readonly IUser _user;
        private readonly ILogger<FriendsController> _logger;

        public FriendsController(IUser user, ILogger<FriendsController> logger)
        {
            _user = user;
            _logger = logger;
        }
        public IActionResult Index(string username = null)
        {
            if(_user.Get(User.Identity.Name) is null)
            {
                _logger.LogWarning("Опять эти куки пытаются не существующего пользователя куда-то отправить");

                return RedirectToAction("Login", "Account");
            }

            UserDTO user;

            if (username is not null)
            {
                user = _user.Get(username);
            }
            else
            {
                user = _user.Get(User.Identity.Name);
            }

            _logger.LogInformation($"{User.Identity.Name} заходит к друзьям {user.UserName}");

            var allUser = _user.GetAll();

            var allFriends = user.Friends.Select(el => _user.Get(el.FriendName)).ToList();

            var friends = allFriends
                .Where(el => _user.IsFriend(el.UserName, user.UserName))
                .ToList();

            var subscriptions = allFriends
                .Where(el => !_user.IsFriend(el.UserName, user.UserName))
                .ToList();

            var subscribers = _user.GetAll()
                .Where(el => _user.IsFriend(el.UserName, user.UserName) && !_user.IsFriend(user.UserName, el.UserName))
                .ToList();

            return View(new FriendsViewModel 
            { 
                User = user,
                Friends = friends,
                Subscriptions = subscriptions,
                Subscribers = subscribers,
                All = allUser
            });
        }
    }
}
