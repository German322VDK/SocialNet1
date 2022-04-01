using SocialNet1.Domain.Identity;
using SocialNet1.Domain.PostCom;
using System.Collections.Generic;

namespace SocialNet1.Infrastructure.Interfaces.Based
{
    public interface IUser
    {
        ICollection<UserDTO> GetAll();

        UserDTO Get(string userName);

        bool AddPhoto(byte[] arr, string userName);

        bool DeletePhoto(int imageId, string userName);

        bool AddLikePhoto(string userName1, string userName2, int imageID);
        bool DeleteLikePhoto(string userName1, string userName2, int imageID);

        UserImageComments AddCommentToPhoto(string userName, string senderName, string text, int imageId);

        bool DeleteComToPhoto(string userName, int imageId, int comId);

        bool AddLikeComPhoto(string userName1, string userName2, int imageId, int comId);

        bool DeleteLikeComPhoto(string userName1, string userName2, int imageId, int comId);

        bool SetAva(int num, string userName);

        bool SetStatus(string text, string userName);

        bool SetCoord(string userName, int x, int y);

        bool IsFriend(string userName1, string userName2);

        bool AddFriend(string userName1, string userName2);

        bool DeleteFriend(string userName1, string userName2);

        ICollection<PostDTO> GetUserPosts(string userName);

        PostDTO GetUserPost(string userName, int postId);

        bool AddUserPost(string userName, PostDTO post);
    }
}
