using Social_Net.Domain.Group;
using SocialNet1.Domain.Group;
using SocialNet1.Domain.PostCom;
using System.Collections.Generic;

namespace SocialNet1.Infrastructure.Interfaces.Based
{
    public interface IGroup
    {
        ICollection<GroupDTO> GetAll();

        ICollection<GroupDTO> Get(string[] groups);

        GroupDTO Get(string group);

        bool AddPhoto(byte[] arr, string groupName);

        bool DeletePhoto(string groupName, int imageId);

        bool AddPhotoLike(string groupName, string userName, int imageId);

        bool DeletePhotoLike(string groupName, string userName, int imageId);

        GroupImageComments AddCommentToPhoto(string groupName, string userName, string text, int imageId);

        bool DeletePhotoCom(string groupName, int imageId, int comHelpId);

        bool AddLikeComPhoto(string groupName, string userName, int imageId, int comHelpId);

        bool DeleteLikeComPhoto(string groupName, string userName, int imageId, int comHelpId);

        PostDTO GetPost(string groupName, int postId);

        bool AddPost(string groupName, PostDTO post);

        bool DeletePost(string groupName, int postId);

        bool AddPostLike(string groupName, string liker, int postId);

        bool DeletePostLike(string groupName, string liker, int postId);

        CommentDTO AddPostCom(string groupName, int postId, string commenter, string text);

        bool DeleteComPost(string groupName, int postId, int comHelpId);

        bool AddPostComLike(string groupName, int postId, int comId, string userName);

        bool DeletePostComLike(string groupName, int postId, int comId, string userName);
    }
}
