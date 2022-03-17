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

        public UserService(SocialNetDBSQlite db)
        {
            _db = db;
        }

        public UserDTO Get(string userName) =>
            GetAll().SingleOrDefault(el => el.UserName == userName);

        public ICollection<UserDTO> GetAll() =>
            _db.Users.ToList();
    }
}
