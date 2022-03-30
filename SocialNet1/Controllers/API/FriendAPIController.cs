using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialNet1.Infrastructure.Interfaces.Based;

namespace SocialNet1.Controllers.API
{
    [Route("api/friend")]
    [ApiController]
    public class FriendAPIController : ControllerBase
    {
        private IUser _user;
        private readonly ILogger<FriendAPIController> _logger;

        public FriendAPIController(IUser user, ILogger<FriendAPIController> logger)
        {
            _user = user;
            _logger = logger;
        }

        [HttpGet("add")]
        public bool Add(string username1, string username2)
        {
            _logger.LogInformation($"{username1} хочет добавить в друзья {username2}");

            if (_user.IsFriend(username1, username2))
            {
                _logger.LogWarning($"{username1} уже добавил в друзья {username2}");

                return false;
            }
                
            var result = _user.AddFriend(username1, username2);

            _logger.LogInformation($"Результат добавления человеком {username1} в друзья человека {username2}: {result}");

            return result;
        }

        [HttpGet("delete")]
        public bool Delete(string username1, string username2)
        {
            _logger.LogInformation($"{username1} хочет удалить из друзей {username2}");

            if (!_user.IsFriend(username1, username2))
            {
                _logger.LogWarning($"{username1} не может удалить так как он не был другом {username2}");

                return false;
            }
                
            var result = _user.DeleteFriend(username1, username2);

            _logger.LogInformation($"Результат удаление человеком {username1} из друзей человека {username2}: {result}");

            return result;
        }
    }
}
