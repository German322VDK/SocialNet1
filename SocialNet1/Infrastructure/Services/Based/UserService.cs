using SocialNet1.Database.SQlite.Context;
using SocialNet1.Domain.Identity;
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


        public bool AddPhoto(byte[] image, string userName)
        {
            var user = Get(userName);

            if (user is null || image is null)
                return false;

            var lastNum = user.Images.Max(el => el.ImageNumber);

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
    }
}
