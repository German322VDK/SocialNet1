using SocialNet1.Domain.Identity;
using System.Collections.Generic;

namespace SocialNet1.Infrastructure.Interfaces.Based
{
    public interface IFriends
    {
        ICollection<UserDTO> GetRange(ICollection<string> friends);
    }
}
