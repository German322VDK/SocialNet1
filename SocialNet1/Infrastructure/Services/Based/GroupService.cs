using Social_Net.Domain.Group;
using SocialNet1.Database.SQlite.Context;
using SocialNet1.Domain.Base;
using SocialNet1.Domain.Group;
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

        #endregion
    }
}
