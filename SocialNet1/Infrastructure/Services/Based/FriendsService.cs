using SocialNet1.Domain.Identity;
using SocialNet1.Infrastructure.Interfaces.Based;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNet1.Infrastructure.Services.Based
{
    public class FriendsService : IFriends
    {
        private readonly IUser _user;

        public FriendsService(IUser user)
        {
            _user = user;
        }

        public ICollection<UserDTO> GetRange(ICollection<string> friends)
        {
            var friendUsers = new List<UserDTO>();

            foreach (var friend in friends)
            {
                friendUsers.Add(_user.Get(friend));
            }

            return friendUsers;
        }

    }
}
