using Social_Net.Domain.Group;
using SocialNet1.Database.SQlite.Context;
using SocialNet1.Domain.Base;
using SocialNet1.Domain.Group;
using SocialNet1.Infrastructure.Interfaces.Based;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        #endregion
    }
}
