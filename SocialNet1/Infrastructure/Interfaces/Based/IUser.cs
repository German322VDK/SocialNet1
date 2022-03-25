using SocialNet1.Domain.Identity;
using System.Collections.Generic;

namespace SocialNet1.Infrastructure.Interfaces.Based
{
    public interface IUser
    {
        ICollection<UserDTO> GetAll();

        UserDTO Get(string userName);

        bool AddPhoto(byte[] arr, string userName);

        UserImageComments AddCommentToPhoto(string userName, string senderName, string text, int imageId);

        bool IsFriend(string userName1, string userName2);

        bool AddFriend(string userName1, string userName2);

        bool DeleteFriend(string userName1, string userName2);
    }
}
