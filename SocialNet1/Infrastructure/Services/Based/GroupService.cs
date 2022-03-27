using SocialNet1.Database.SQlite.Context;
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
    }
}
