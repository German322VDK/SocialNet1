using Social_Net.Domain.Group;
using Social_Net.Domain.PostCom;
using SocialNet1.Database.SQlite.Context;
using SocialNet1.Domain.Base;
using SocialNet1.Domain.Group;
using SocialNet1.Domain.PostCom;
using SocialNet1.Infrastructure.Interfaces.Based;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialNet1.Infrastructure.Services.Based
{
    public class GroupService : IGroup
    {
        private readonly SocialNetDBSQlite _db;

        public GroupService(SocialNetDBSQlite db)
        {
            _db = db;
        }

        #region BaseCRUD

        public ICollection<GroupDTO> GetAll() =>
            _db.Groups.ToList();

        public ICollection<GroupDTO> Get(string[] groups) =>
             GetAll().Where(el => groups.Contains(el.ShortGroupName) ).ToList();

        public GroupDTO Get(string group) =>
             GetAll().FirstOrDefault(el => el.ShortGroupName == group);


        #endregion


        #region Photo

        public GroupImages GetPhoto(string groupName, int imageId) =>
             Get(groupName)?.Images.FirstOrDefault(im => im.Id == imageId);

        public bool AddPhoto(byte[] arr, string groupName)
        {
            var group = Get(groupName);

            if (group is null || arr is null)
                return false;

            int lastNum;
            if (group.Images.Count == 0)
                lastNum = 0;
            else
                lastNum = group.Images.Max(el => el.ImageNumber);

            var newLast = lastNum + 1;

            using (_db.Database.BeginTransaction())
            {
                _db.Groups
                    .FirstOrDefault(gr => gr.ShortGroupName == groupName)
                    .Images.Add(new GroupImages
                    {
                        Image = arr,
                        ImageNumber = newLast,
                    });

                _db.SaveChanges();

                _db.Database.CommitTransaction();
            }

            return true;
        }

        public bool DeletePhoto(string groupName, int imageId)
        {
            var group = Get(groupName);

            if (group is null)
                return false;

            var image = GetPhoto(groupName, imageId);

            if (image is null)
                return false;

            using (_db.Database.BeginTransaction())
            {
                var im = _db.Groups
                    .FirstOrDefault(gr => gr.ShortGroupName == groupName)
                    .Images
                    .FirstOrDefault(im => im.Id == imageId);

                foreach (var item in im.Coments)
                {
                    _db.RemoveRange(item.GroupCommentLikes);
                }

                _db.RemoveRange(im.Coments);

                _db.RemoveRange(im.GroupLikes);

                _db.Remove(im);

                _db.SaveChanges();

                _db.Database.CommitTransaction();
            }

            return true;
        }

        public bool AddPhotoLike(string groupName, string userName, int imageId)
        {
            var group = Get(groupName);

            if (group is null)
                return false;

            var image = GetPhoto(groupName, imageId);

            if (image is null)
                return false;

            var liker = image.GroupLikes
                    .FirstOrDefault(lk => lk.Likers == userName);

            if (liker is not null)
                return false;

            using (_db.Database.BeginTransaction())
            {
                _db.Groups
                    .FirstOrDefault(gr => gr.ShortGroupName == groupName)
                    .Images
                    .FirstOrDefault(im => im.Id == imageId)
                    .GroupLikes
                    .Add(new GroupLike 
                    { 
                        Likers = userName,
                        Emoji = Emoji.Like
                    });

                _db.SaveChanges();

                _db.Database.CommitTransaction();
            }

            return true;
        }

        public bool DeletePhotoLike(string groupName, string userName, int imageId)
        {
            var group = Get(groupName);

            if (group is null)
                return false;

            var image = GetPhoto(groupName, imageId);

            if (image is null)
                return false;

            var liker = image.GroupLikes
                    .FirstOrDefault(lk => lk.Likers == userName);

            if (liker is null)
                return false;

            using (_db.Database.BeginTransaction())
            {
                var like = _db.Groups
                    .FirstOrDefault(gr => gr.ShortGroupName == groupName)
                    .Images
                    .FirstOrDefault(im => im.Id == imageId)
                    .GroupLikes
                    .FirstOrDefault(lk => lk.Likers == userName);

                _db.Remove(like);

                _db.SaveChanges();

                _db.Database.CommitTransaction();
            }

            return true;
        }

        public GroupImageComments AddCommentToPhoto(string groupName, string userName, string text, int imageId)
        {
            if (Get(groupName) is null || GetPhoto(groupName, imageId) is null)
                return null;

            GroupImageComments comment = null;

            var coms = _db.Groups
                    .SingleOrDefault(gr => gr.ShortGroupName == groupName)
                    .Images
                    .SingleOrDefault(im => im.Id == imageId)
                    .Coments
                    .ToList();

            using (_db.Database.BeginTransaction())
            {
                int helpId = 1;

                if (coms.Count > 0)
                    helpId = coms[coms.Count - 1].HelpId + 1;

                comment = new GroupImageComments
                {
                    Text = text,
                    UserName = userName,
                    HelpId = helpId
                };

                _db.Groups
                    .SingleOrDefault(gr => gr.ShortGroupName == groupName)
                    .Images
                    .SingleOrDefault(im => im.Id == imageId)
                    .Coments.Add(comment);


                _db.SaveChanges();

                _db.Database.CommitTransaction();
            }

            return comment;
        }

        public bool DeletePhotoCom(string groupName, int imageId, int comHelpId)
        {
            if (Get(groupName) is null || GetPhoto(groupName, imageId) is null)
                return false;

            var com = GetPhoto(groupName, imageId)
                .Coments
                .FirstOrDefault(cm => cm.HelpId == comHelpId);

            if (com is null)
                return false;

            using (_db.Database.BeginTransaction())
            {
                _db.RemoveRange(com.GroupCommentLikes);

                _db.Remove(com);

                _db.SaveChanges();

                _db.Database.CommitTransaction();
            }

            return true;
        }

        public bool AddLikeComPhoto(string groupName, string userName, int imageId, int comHelpId)
        {
            var group = Get(groupName);

            if (group is null)
                return false;

            var image = GetPhoto(groupName, imageId);

            if (image is null)
                return false;

            var com = image.Coments.FirstOrDefault(cm => cm.HelpId == comHelpId);

            if (com is null)
                return false;

            var liker = com.GroupCommentLikes
                    .FirstOrDefault(lk => lk.Likers == userName);

            if (liker is not null)
                return false;

            using (_db.Database.BeginTransaction())
            {
                _db.Groups
                    .FirstOrDefault(gr => gr.ShortGroupName == groupName)
                    .Images
                    .FirstOrDefault(im => im.Id == imageId)
                    .Coments
                    .FirstOrDefault(cm => cm.HelpId == comHelpId)
                    .GroupCommentLikes
                    .Add(new GroupCommentLike
                    {
                        Likers = userName,
                        Emoji = Emoji.Like
                    });

                _db.SaveChanges();

                _db.Database.CommitTransaction();
            }

            return true;
        }

        public bool DeleteLikeComPhoto(string groupName, string userName, int imageId, int comHelpId)
        {
            var group = Get(groupName);

            if (group is null)
                return false;

            var image = GetPhoto(groupName, imageId);

            if (image is null)
                return false;

            var com = image.Coments.FirstOrDefault(cm => cm.HelpId == comHelpId);

            if (com is null)
                return false;

            var liker = com.GroupCommentLikes
                    .FirstOrDefault(lk => lk.Likers == userName);

            if (liker is null)
                return false;

            using (_db.Database.BeginTransaction())
            {
                var like = _db.Groups
                    .FirstOrDefault(gr => gr.ShortGroupName == groupName)
                    .Images
                    .FirstOrDefault(im => im.Id == imageId)
                    .Coments
                    .FirstOrDefault(cm => cm.HelpId == comHelpId)
                    .GroupCommentLikes
                    .FirstOrDefault(lk => lk.Likers == userName);

                _db.Remove(like);

                _db.SaveChanges();

                _db.Database.CommitTransaction();
            }

            return true;
        }



        #endregion

        #region Posts

        public PostDTO GetPost(string groupName, int postId) =>
            Get(groupName).SocNetItems.Posts.FirstOrDefault(p => p.Id == postId);

        public bool AddPost(string groupName, PostDTO post)
        {
            var group = Get(groupName);

            if (group is null)
                return false;

            if (post is null || post.ThisPost is null || string.IsNullOrEmpty(post.ThisPost.CommentatorName))
                return false;

            using (_db.Database.BeginTransaction())
            {
                _db.Groups
                    .FirstOrDefault(gr => gr.ShortGroupName == groupName)
                    .SocNetItems
                    .Posts
                    .Add(post);

                _db.SaveChanges();

                _db.Database.CommitTransaction();
            }

            return true;
        }

        public bool DeletePost(string groupName, int postId)
        {
            var group = Get(groupName);

            if (group is null)
                return false;

            var post = GetPost(groupName, postId);

            if (post is null)
                return false;

            using (_db.Database.BeginTransaction())
            {
                _db.RemoveRange(post.ThisPost.Images);

                _db.RemoveRange(post.ThisPost.Likes);

                _db.Remove(post.ThisPost);

                if (post.Comments is not null)
                {
                    foreach (var item in post.Comments)
                    {
                        _db.RemoveRange(item.Images);

                        _db.RemoveRange(item.Likes);

                        _db.Remove(item);
                    }
                }

                _db.Remove(post);

                _db.SaveChanges();

                _db.Database.CommitTransaction();
            }

            return true;
        }

        public bool AddPostLike(string groupName, string liker, int postId)
        {
            var group = Get(groupName);

            if (group is null)
                return false;

            var post = GetPost(groupName, postId);

            if (post is null)
                return false;

            if (post.ThisPost.Likes.FirstOrDefault(lk => lk.Likers == liker) is not null)
                return false;

            using (_db.Database.BeginTransaction())
            {
                _db.Groups
                    .FirstOrDefault(gr => gr.ShortGroupName == groupName)
                    .SocNetItems.Posts
                    .FirstOrDefault(p => p.Id == postId)
                    .ThisPost
                    .Likes
                    .Add(new CommentLike 
                    { 
                        Likers = liker,
                        Emoji = Emoji.Like
                    });

                _db.SaveChanges();

                _db.Database.CommitTransaction();
            }

            return true;
        }

        public bool DeletePostLike(string groupName, string liker, int postId)
        {
            var group = Get(groupName);

            if (group is null)
                return false;

            var post = GetPost(groupName, postId);

            if (post is null)
                return false;

            var like = post.ThisPost.Likes.FirstOrDefault(lk => lk.Likers == liker);

            if (like is null)
                return false;

            using (_db.Database.BeginTransaction())
            {
                _db.Remove(like);

                _db.SaveChanges();

                _db.Database.CommitTransaction();
            }

            return true;
        }

        #endregion
    }
}
