﻿using SocialNet1.Domain.Identity;
using SocialNet1.Domain.PostCom;
using System.Collections.Generic;

namespace SocialNet1.Infrastructure.Interfaces.Based
{
    public interface IUser
    {
        ICollection<UserDTO> GetAll();

        UserDTO Get(string userName);

        bool Delete(string userName);

        ICollection<RoleDTO> GetRolesByUser(string userName);

        bool IsUserInRole(string userName, string roleName);

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

        ICollection<string> GetFriends(string userName);

        bool IsFriend(string userName1, string userName2);

        bool AddFriend(string userName1, string userName2);

        bool DeleteFriend(string userName1, string userName2);

        ICollection<PostDTO> GetUserPosts(string userName);

        PostDTO GetUserPost(string userName, int postId);

        bool AddUserPost(string userName, PostDTO post);

        bool DeletePost(string userName, int postId);

        bool AddPostLike(string userName, int postId, string liker);

        bool DeletePostLike(string userName, int postId, string liker);

        CommentDTO AddPostCom(string username, int postId, string commenter, string text);

        bool AddPostComLike(string userName, int postId, int comId, string liker);

        bool DeletePostComLike(string userName, int postId, int comId, string liker);

        bool DeletePostCom(string userName, int postId, int comId);
    }
}
