using Social_Net.Domain.Clash;
using SocialNet1.Database.SQlite.Context;
using SocialNet1.Infrastructure.Interfaces.Based;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNet1.Infrastructure.Services.Based
{
    public class ClashService : IClash
    {
        private readonly SocialNetDBSQlite _db;

        public ClashService(SocialNetDBSQlite db)
        {
            _db = db;
        }

        public ICollection<ClashDTO> GetAll() =>
            _db.Clashs.ToList();

        public ICollection<ClashDTO> GetByGroup(string groupName) =>
            GetAll().Where(cl => cl.Side1.Group.GroupName == groupName || cl.Side2.Group.GroupName == groupName).ToList();

        public ICollection<ClashDTO> GetByUser(string userName)
        {
            var groupNames = _db.UserGroupStatuses.Where(ugs => ugs.UserName == userName).Select(ugs => ugs.GroupName);

            return GetAll()
                .Where(cl => groupNames.Contains(cl.Side1.Group.GroupName) || groupNames.Contains(cl.Side2.Group.GroupName))
                .ToList();
        }
    }
}
