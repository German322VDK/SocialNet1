using SocialNet1.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNet1.Infrastructure.Interfaces.Based
{
    public interface IUser
    {
        ICollection<UserDTO> GetAll();

        UserDTO Get(string userName);

        bool AddPhoto(byte[] arr, string userName);

        UserImageComments AddCommentToPhoto(string userName, string senderName, string text, int imageId);
    }
}
