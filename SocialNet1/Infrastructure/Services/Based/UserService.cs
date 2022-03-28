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

        #region BaseCRUD
        public UserService(SocialNetDBSQlite db)
        {
            _db = db;
        }

        public UserDTO Get(string userName) =>
            GetAll().SingleOrDefault(el => el.UserName == userName);

        public ICollection<UserDTO> GetAll() =>
            _db.Users.ToList();

        #endregion

        #region Friend

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

            var image = user.Images.FirstOrDefault(el => el.Id == imageId);

            if (image is null)
                return false;

            using (_db.Database.BeginTransaction())
            {
                var likes = image.Likes;

                if(likes.Count > 0)
                    _db.Remove(likes);

                var coms = image.Coments;

                if (likes.Count > 0)
                    _db.Remove(coms);

                _db.Remove(image);

                _db.SaveChanges();

                _db.Database.CommitTransaction();
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
                    .Likes;

                if (likes.FirstOrDefault(lk => lk.Likers == userName2) is not null)
                    return false;

                likes.Add(new Like
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
                    .Likes
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

            using (_db.Database.BeginTransaction())
            {
                comment = new UserImageComments
                {
                    LikeCount = 0,
                    Text = text,
                    UserName = senderName
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

        
    }
}
