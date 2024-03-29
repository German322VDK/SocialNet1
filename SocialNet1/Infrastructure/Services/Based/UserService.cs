﻿using Social_Net.Domain.Identity;
using Social_Net.Domain.PostCom;
using SocialNet1.Database.SQlite.Context;
using SocialNet1.Domain.Base;
using SocialNet1.Domain.Friends;
using SocialNet1.Domain.Identity;
using SocialNet1.Domain.PostCom;
using SocialNet1.Infrastructure.Interfaces.Based;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNet1.Infrastructure.Services.Based
{
    public class UserService : IUser
    {
        private readonly SocialNetDBSQlite _db;

        
        public UserService(SocialNetDBSQlite db)
        {
            _db = db;
        }

        #region BaseCRUD

        public UserDTO Get(string userName) =>
            GetAll().SingleOrDefault(el => el.UserName == userName);

        public ICollection<UserDTO> GetAll() =>
            _db.Users.ToList();

        public bool Delete(string userName)
        {
            var user = Get(userName);

            if(user is null)
            {
                return false;
            }

            using (_db.Database.BeginTransaction())
            {
                var ugs = _db.UserGroupStatuses.Where(gu => gu.UserName == userName);

                _db.RemoveRange(ugs);

                var ur = _db.UserRoles.Where(ur=> ur.UserId == user.Id);

                _db.RemoveRange(ur);

                var friends = user.Friends;

                _db.RemoveRange(friends);

                var images = user.Images;

                foreach (var image in images)
                {
                    foreach (var com in image.Coments)
                    {
                        _db.RemoveRange(com.UserCommentLikes);

                        _db.Remove(com);
                    }

                    _db.RemoveRange(image.UserLikes);

                    _db.Remove(image);
                }

                var posts = user.SocNetItems.Posts;

                foreach (var post in posts)
                {
                    foreach (var com in post.Comments)
                    {
                        _db.RemoveRange(com.Likes);

                        _db.RemoveRange(com.Images);

                        _db.Remove(com);
                    }

                    _db.RemoveRange(post.ThisPost.Likes);

                    _db.RemoveRange(post.ThisPost.Images);

                    _db.Remove(post.ThisPost);

                    _db.Remove(post);
                }

                _db.Remove(user.SocNetItems);

                _db.Remove(user);

                _db.SaveChanges();

                _db.Database.CommitTransaction();
            }

            return true;
        }

        public bool IsUserInRole(string userName, string roleName)
        {
            var rolesNames = GetRolesByUser(userName).Select(r => r.Name);

            return rolesNames.Contains(roleName);
        }

        public ICollection<RoleDTO> GetRolesByUser(string userName)
        {
            var user = Get(userName);

            var roleIds = _db.UserRoles.Where(ur => ur.UserId == user.Id).Select(ur => ur.RoleId);

            var roles = new List<RoleDTO>();

            foreach (var roleId in roleIds)
            {
                var role = _db.Roles.FirstOrDefault(r => r.Id == roleId);

                roles.Add(role);
            }

            return roles;
        }

        #endregion

        #region Friend

        public ICollection<string> GetFriends(string userName) =>
            Get(userName).Friends.Select(fr => fr.FriendName).ToList();

        public bool IsFriend(string userName1, string userName2)
        {
            var user1 = Get(userName1);

            return user1.Friends.FirstOrDefault(el => el.FriendName == userName2) is not null;
        }

        public bool AddFriend(string userName1, string userName2)
        {
            if (IsFriend(userName1, userName2) && IsFriend(userName2, userName1))
                return false;

            using (_db.Database.BeginTransaction())
            {
                _db.Users
                    .FirstOrDefault(el => el.UserName == userName1)
                    .Friends
                    .Add(new FriendStatus 
                    { 
                        FriendName = userName2,
                        HiddenStatus = false
                    });

                _db.SaveChanges();

                _db.Database.CommitTransaction();
            }

            return true;

        }


        public bool DeleteFriend(string userName1, string userName2)
        {
            if (!IsFriend(userName1, userName2))
                return false;

            using (_db.Database.BeginTransaction())
            {
                var friend1 = _db.Users
                    .FirstOrDefault(el => el.UserName == userName1)
                    .Friends
                    .FirstOrDefault(el => el.FriendName == userName2);

                _db.Remove(friend1);

                _db.SaveChanges();

                _db.Database.CommitTransaction();
            }

            return true;
        }

        #endregion

        #region Photo

        public bool AddPhoto(byte[] image, string userName)
        {
            var user = Get(userName);

            if (user is null || image is null)
                return false;

            int lastNum;
            if (user.Images.Count == 0)
                lastNum = 0;
            else
                lastNum = user.Images.Max(el => el.ImageNumber);

            var newLast = lastNum + 1;

            using (_db.Database.BeginTransaction())
            {
                _db.Users
                    .FirstOrDefault(us => us.UserName == userName)
                    .Images.Add(new UserImages
                    {
                        Image = image,
                        ImageNumber = newLast
                    });

                _db.SaveChanges();

                _db.Database.CommitTransaction();
            }


            return true;
        }

        public bool DeletePhoto(int imageId, string userName)
        {
            var user = Get(userName);

            if (user is null)
                return false;

            if(user.Images.Count < 2)
            {
                return false;
            }

            var image = user.Images.FirstOrDefault(el => el.Id == imageId);

            if (image is null)
                return false;

            var imageNum = image.ImageNumber;

            using (_db.Database.BeginTransaction())
            {
                var likes = image.UserLikes;

                if(likes.Count > 0)
                    _db.RemoveRange(likes);

                var coms = image.Coments;

                if (coms.Count > 0)
                    _db.RemoveRange(coms);

                _db.Remove(image);

                _db.SaveChanges();

                _db.Database.CommitTransaction();
            }

            var user1 = Get(userName);

            if (imageNum == user1.SocNetItems.CurrentImage)
            {
                using (_db.Database.BeginTransaction())
                {
                    var fIm = user.Images.First();
                    user.SocNetItems.CurrentImage = fIm.ImageNumber;

                    _db.SaveChanges();

                    _db.Database.CommitTransaction();
                }
            }

            return true;
        }

        public bool AddLikePhoto(string userName1, string userName2, int imageID)
        {

            using (_db.Database.BeginTransaction())
            {
                var likes = _db.Users
                    .FirstOrDefault(el => el.UserName == userName1)
                    .Images
                    .FirstOrDefault(im => im.Id == imageID)
                    .UserLikes;

                if (likes.FirstOrDefault(lk => lk.Likers == userName2) is not null)
                    return false;

                likes.Add(new UserLike
                {
                    Likers = userName2,
                    Emoji = Emoji.Like
                });

                _db.SaveChanges();

                _db.Database.CommitTransaction();
            }

            return true;
        }

        public bool DeleteLikePhoto(string userName1, string userName2, int imageID)
        {
            using (_db.Database.BeginTransaction())
            {
                var like = _db.Users
                    .FirstOrDefault(el => el.UserName == userName1)
                    .Images
                    .FirstOrDefault(im => im.Id == imageID)
                    .UserLikes
                    .FirstOrDefault(lk => lk.Likers == userName2);

                if (like is null)
                    return false;

                _db.Remove(like);

                _db.SaveChanges();

                _db.Database.CommitTransaction();
            }

            return true;
        }

        public UserImageComments AddCommentToPhoto(string userName, string senderName, string text, int imageId)
        {
            UserImageComments comment = null;

            var user = Get(userName);

            var sender = Get(senderName);

            if (user is null || sender is null || text is null)
                return comment;

            var coms = _db.Users
                    .SingleOrDefault(us => us.UserName == userName)
                    .Images
                    .SingleOrDefault(im => im.Id == imageId)
                    .Coments
                    .ToList();

            using (_db.Database.BeginTransaction())
            {
                int helpId = 1;

                if (coms.Count > 0)
                    helpId = coms[coms.Count - 1].HelpId + 1;

                comment = new UserImageComments
                {
                    Text = text,
                    UserName = senderName,
                    HelpId = helpId
                };

                _db.Users
                    .SingleOrDefault(us => us.UserName == userName)
                    .Images
                    .SingleOrDefault(im => im.Id == imageId)
                    .Coments.Add(comment);


                _db.SaveChanges();

                _db.Database.CommitTransaction();
            }


            return comment;
        }

        public bool DeleteComToPhoto(string userName, int imageId, int comHelpId)
        {
            var user = Get(userName);

            if (user is null)
                return false;

            var image = user.Images.FirstOrDefault(im => im.Id == imageId);

            if (image is null)
                return false;

            var com = image.Coments.FirstOrDefault(cm => cm.HelpId == comHelpId);

            if (com is null)
                return false;

            using (_db.Database.BeginTransaction())
            {
                if(com.UserCommentLikes is not null)
                    _db.RemoveRange(com.UserCommentLikes);

                _db.Remove(com);

                _db.SaveChanges();

                _db.Database.CommitTransaction();
            }

            return true;
        }

        public bool AddLikeComPhoto(string userName1, string userName2, int imageId, int comHelpId)
        {
            
            var user = Get(userName1);

            if (user is null)
                return false;

            var image = user.Images.FirstOrDefault(im => im.Id == imageId);

            if (image is null)
                return false;

            var com = image.Coments.FirstOrDefault(cm => cm.HelpId == comHelpId);

            if (com is null)
                return false;

            using (_db.Database.BeginTransaction())
            {
                var commentLikes = _db.Users
                    .FirstOrDefault(el => el.UserName == userName1)
                    .Images
                    .FirstOrDefault(im => im.Id == imageId)
                    .Coments
                    .FirstOrDefault(cm => cm.HelpId == comHelpId)
                    .UserCommentLikes;

                if (commentLikes.FirstOrDefault(lk => lk.Likers == userName2) is not null)
                    return false;

                commentLikes.Add(new UserCommentLike
                {
                    Likers = userName2,
                    Emoji = Emoji.Like,
                });

                _db.SaveChanges();

                _db.Database.CommitTransaction();
            }

            return true;
        }

        public bool DeleteLikeComPhoto(string userName1, string userName2, int imageId, int comHelpId)
        {
            var user = Get(userName1);

            if (user is null)
                return false;

            var image = user.Images.FirstOrDefault(im => im.Id == imageId);

            if (image is null)
                return false;

            var com = image.Coments.FirstOrDefault(cm => cm.HelpId == comHelpId);

            if (com is null)
                return false;

            using (_db.Database.BeginTransaction())
            {
                var commentLike = _db.Users
                    .FirstOrDefault(el => el.UserName == userName1)
                    .Images
                    .FirstOrDefault(im => im.Id == imageId)
                    .Coments
                    .FirstOrDefault(cm => cm.HelpId == comHelpId)
                    .UserCommentLikes
                    .FirstOrDefault(lk => lk.Likers == userName2);

                if (commentLike is null)
                    return false;

                _db.Remove(commentLike);

                _db.SaveChanges();

                _db.Database.CommitTransaction();
            }

            return true;
        }

        public bool SetAva(int num, string userName)
        {
            var user = Get(userName);

            if (user is null)
                return false;

            var image = user.Images.FirstOrDefault(im => im.ImageNumber == num);

            if(image is null)
                return false;

            using (_db.Database.BeginTransaction())
            {
                _db.Users.FirstOrDefault(us => us.UserName == userName).SocNetItems.CurrentImage = num;

                _db.SaveChanges();

                _db.Database.CommitTransaction();
            }

            return true;
        }

        #endregion

        #region Posts

        public ICollection<PostDTO> GetUserPosts(string userName) =>
            Get(userName).SocNetItems.Posts;

        public PostDTO GetUserPost(string userName, int postId) =>
            GetUserPosts(userName).FirstOrDefault(p => p.Id == postId);

        public bool AddUserPost(string userName, PostDTO post)
        {
            if(Get(userName) is null || GetUserPosts(userName) is null )
                return false;

            using (_db.Database.BeginTransaction())
            {
                _db.Users.FirstOrDefault(us => us.UserName == userName)
                    .SocNetItems
                    .Posts
                    .Add(post);

                _db.SaveChanges();

                _db.Database.CommitTransaction();
            }

            return true;
        }

        public bool DeletePost(string userName, int postId)
        {
            if (Get(userName) is null || GetUserPosts(userName) is null || GetUserPosts(userName).FirstOrDefault(ps => ps.Id == postId) is null)
                return false;

            using (_db.Database.BeginTransaction())
            {
                var post = _db.Users.FirstOrDefault(us => us.UserName == userName)
                    .SocNetItems
                    .Posts
                    .FirstOrDefault(ps => ps.Id == postId);

                var thisPost = post.ThisPost;

                var coms = post.Comments;

                //удаляем сам пост
                _db.RemoveRange(thisPost.Images);
                _db.RemoveRange(thisPost.Likes);
                _db.Remove(thisPost);

                //удаляем комменты
                foreach (var com in coms)
                {
                    _db.RemoveRange(com.Images);
                    _db.RemoveRange(com.Likes);
                }
                _db.RemoveRange(coms);

                _db.Remove(post);

                _db.SaveChanges();

                _db.Database.CommitTransaction();
            }

            return true;
        }

        public bool AddPostLike(string userName, int postId, string liker)
        {
            if (Get(userName) is null )
                return false;

            var userPosts = Get(userName).SocNetItems.Posts;

            if (GetUserPost(userName, postId) is null)
                return false;

            if (GetUserPost(userName, postId).ThisPost.Likes.FirstOrDefault(lk => lk.Likers == liker) is not null)
                return false;

            using (_db.Database.BeginTransaction())
            {
                _db.Users.FirstOrDefault(us => us.UserName == userName)
                    .SocNetItems
                    .Posts
                    .FirstOrDefault(ps => ps.Id == postId)
                    .ThisPost
                    .Likes
                    .Add(new CommentLike 
                    { 
                        Emoji = Emoji.Like,
                        Likers = liker
                    });

                _db.SaveChanges();

                _db.Database.CommitTransaction();
            }

            return true;
        }

        public bool DeletePostLike(string userName, int postId, string liker)
        {
            if (Get(userName) is null || GetUserPost(userName, postId) is null
                || GetUserPost(userName, postId).ThisPost.Likes.FirstOrDefault(lk => lk.Likers == liker) is null)
                return false;

            using (_db.Database.BeginTransaction())
            {
                var like = _db.Users.FirstOrDefault(us => us.UserName == userName)
                    .SocNetItems
                    .Posts
                    .FirstOrDefault(ps => ps.Id == postId)
                    .ThisPost
                    .Likes
                    .FirstOrDefault(lk => lk.Likers == liker);

                _db.Remove(like);

                _db.SaveChanges();

                _db.Database.CommitTransaction();
            }

            return true;

        }

        public CommentDTO AddPostCom(string username, int postId, string commenter, string text)
        {
            if (Get(username) is null)
                return null;

            if (GetUserPost(username, postId) is null)
                return null;

            CommentDTO comment = null;

            int helpId = 1;

            var coms = _db.Users
                    .FirstOrDefault(us => us.UserName == username)
                    .SocNetItems
                    .Posts
                    .FirstOrDefault(ps => ps.Id == postId)
                    .Comments;

            if (coms is not null && coms.Count > 0)
                helpId = coms.Last().HelpId + 1;

            using (_db.Database.BeginTransaction())
            {
                comment = new CommentDTO
                {
                    CommentatorStatus = CommentatorStatus.User,
                    CommentatorName = commenter,
                    Content = text,
                    HelpId = helpId
                };

                _db.Users
                    .FirstOrDefault(us => us.UserName == username)
                    .SocNetItems
                    .Posts
                    .FirstOrDefault(ps => ps.Id == postId)
                    .Comments
                    .Add(comment);

                _db.SaveChanges();

                _db.Database.CommitTransaction();
            }

            return comment;
        }

        public bool AddPostComLike(string userName, int postId, int comId, string liker)
        {
            if (Get(userName) is null || Get(liker) is null)
                return false;

            var userPosts = Get(userName).SocNetItems.Posts;

            if (GetUserPost(userName, postId) is null)
                return false;

            if (GetUserPost(userName, postId).Comments.FirstOrDefault(cm => cm.HelpId == comId) is null)
                return false;

            var like = _db.Users.FirstOrDefault(us => us.UserName == userName)
                    .SocNetItems
                    .Posts
                    .FirstOrDefault(ps => ps.Id == postId)
                    .Comments
                    .FirstOrDefault(cm => cm.HelpId == comId)
                    .Likes
                    .FirstOrDefault(lk => lk.Likers == liker);

            if (like is not null)
                return false;

            using (_db.Database.BeginTransaction())
            {
                _db.Users.FirstOrDefault(us => us.UserName == userName)
                    .SocNetItems
                    .Posts
                    .FirstOrDefault(ps => ps.Id == postId)
                    .Comments
                    .FirstOrDefault(cm => cm.HelpId == comId)
                    .Likes
                    .Add(new CommentLike
                    {
                        Emoji = Emoji.Like,
                        Likers = liker
                    });

                _db.SaveChanges();

                _db.Database.CommitTransaction();
            }

            return true;
        }

        public bool DeletePostComLike(string userName, int postId, int comId, string liker)
        {
            if (Get(userName) is null || Get(liker) is null)
                return false;

            var userPosts = Get(userName).SocNetItems.Posts;

            if (GetUserPost(userName, postId) is null)
                return false;

            if (GetUserPost(userName, postId).Comments.FirstOrDefault(cm => cm.HelpId == comId) is null)
                return false;

            using (_db.Database.BeginTransaction())
            {
                var like = _db.Users.FirstOrDefault(us => us.UserName == userName)
                    .SocNetItems
                    .Posts
                    .FirstOrDefault(ps => ps.Id == postId)
                    .Comments
                    .FirstOrDefault(cm => cm.HelpId == comId)
                    .Likes
                    .FirstOrDefault(lk => lk.Likers == liker);

                if (like is null)
                    return false;

                _db.Remove(like);

                _db.SaveChanges();

                _db.Database.CommitTransaction();
            }

            return true;

        }

        public bool DeletePostCom(string userName, int postId, int comId)
        {
            if (Get(userName) is null)
                return false;

            var userPosts = Get(userName).SocNetItems.Posts;

            if (GetUserPost(userName, postId) is null)
                return false;

            if (GetUserPost(userName, postId).Comments.FirstOrDefault(cm => cm.HelpId == comId) is null)
                return false;

            using (_db.Database.BeginTransaction())
            {
                var comment = _db.Users.FirstOrDefault(us => us.UserName == userName)
                    .SocNetItems
                    .Posts
                    .FirstOrDefault(ps => ps.Id == postId)
                    .Comments
                    .FirstOrDefault(cm => cm.HelpId == comId);

                _db.RemoveRange(comment.Likes);

                _db.RemoveRange(comment.Images);

                _db.Remove(comment);

                _db.SaveChanges();

                _db.Database.CommitTransaction();
            }

            return true;
        }

        #endregion

        public bool SetStatus(string text, string userName)
        {
            var user = Get(userName);

            if (user is null || text is null)
                return false;

            using (_db.Database.BeginTransaction())
            {
                _db.Users
                    .FirstOrDefault(us => us.UserName == userName)
                    .Status = text;

                _db.SaveChanges();

                _db.Database.CommitTransaction();
            }

            return true;
        }

        public bool SetCoord(string userName, int x, int y)
        {
            var user = Get(userName);

            if (user is null || x > 2 || x < -2 || y > 2 || y < -2)
                return false;

            using (_db.Database.BeginTransaction())
            {
                _db.Users
                    .FirstOrDefault(us => us.UserName == userName)
                    .SocNetItems.X = x;

                _db.Users
                   .FirstOrDefault(us => us.UserName == userName)
                   .SocNetItems.Y = y;

                _db.SaveChanges();

                _db.Database.CommitTransaction();
            }

            return true;
        }

    }
}
