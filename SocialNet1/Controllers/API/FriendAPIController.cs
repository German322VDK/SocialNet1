using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNet1.Infrastructure.Interfaces.Based;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNet1.Controllers.API
{
    [Route("api/friend")]
    [ApiController]
    public class FriendAPIController : ControllerBase
    {
        private IUser _user;

        public FriendAPIController(IUser user)
        {
            _user = user;
        }

        [HttpGet("add")]
        public bool Add(string username1, string username2)
        {
            if (_user.IsFriend(username1, username2))
                return false;

            var result = _user.AddFriend(username1, username2);

            return result;
        }

        [HttpGet("delete")]
        public bool Delete(string username1, string username2)
        {
            if (!_user.IsFriend(username1, username2))
                return false;

            var result = _user.DeleteFriend(username1, username2);

            return result;
        }
    }
}
