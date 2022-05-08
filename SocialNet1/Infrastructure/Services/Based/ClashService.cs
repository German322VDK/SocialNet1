using Social_Net.Domain.Clash;
using SocialNet1.Database.SQlite.Context;
using SocialNet1.Infrastructure.Interfaces.Based;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public ClashDTO Get(string groupName1, string groupName2) =>
             GetAll()
            .FirstOrDefault(cl => 
                (cl.Side1.Group.ShortGroupName == groupName1 && cl.Side2.Group.ShortGroupName == groupName2) ||
                (cl.Side2.Group.ShortGroupName == groupName1 && cl.Side1.Group.ShortGroupName == groupName2)
            );

        public ClashDTO Get(int clashId) =>
            GetAll().FirstOrDefault(cl => cl.Id == clashId);

        public ICollection<ClashDTO> GetByGroup(string groupName) =>
            GetAll().Where(cl => cl.Side1.Group.GroupName == groupName || cl.Side2.Group.GroupName == groupName).ToList();

        public ICollection<ClashDTO> GetByUser(string userName)
        {
            var groupNames = _db.UserGroupStatuses.Where(ugs => ugs.UserName == userName).Select(ugs => ugs.GroupName);

            return GetAll()
                .Where(cl => groupNames.Contains(cl.Side1.Group.GroupName) || groupNames.Contains(cl.Side2.Group.GroupName))
                .ToList();
        }

        public ICollection<string> GetReadyGroups(string groupName)
        {
            var allGroups = GetAll();

            var groups = allGroups
                .Where(cl => (cl.Side1.Group.ShortGroupName == groupName && !cl.Side1.IsReady) ||
                    (cl.Side2.Group.ShortGroupName == groupName && !cl.Side2.IsReady));

            return groups.Select(cl =>
            {
                if (cl.Side1.Group.ShortGroupName == groupName)
                    return cl.Side2.Group.ShortGroupName;
                else
                    return cl.Side1.Group.ShortGroupName;
            })
            .ToList();
        }

        public bool Add(string thisGroupName, string groupName)
        {
            var thisGroup = _db.Groups.FirstOrDefault(gr => gr.ShortGroupName == thisGroupName);
            var group = _db.Groups.FirstOrDefault(gr => gr.ShortGroupName == groupName);

            if(thisGroup is null || group is null)
            {
                return false;
            }

            var clash = Get(thisGroupName, groupName);

            if(clash is not null)
            {
                return false;
            }

            using (_db.Database.BeginTransaction())
            {
                _db.Clashs.Add(new ClashDTO 
                { 
                    Side1 = new Side
                    {
                        Group = thisGroup,
                        IsReady = true
                    },
                    Side2 = new Side
                    {
                        Group = group
                    }
                });

                _db.SaveChanges();

                _db.Database.CommitTransaction();
            }

            return true;
        }

        public bool Confirm(string thisGroupName, string groupName)
        {
            var thisGroup = _db.Groups.FirstOrDefault(gr => gr.ShortGroupName == thisGroupName);
            var group = _db.Groups.FirstOrDefault(gr => gr.ShortGroupName == groupName);

            if (thisGroup is null || group is null)
            {
                return false;
            }

            var clash = Get(thisGroupName, groupName);

            if (clash is null)
            {
                return false;
            }

            using (_db.Database.BeginTransaction())
            {
                if (clash.Side1.IsReady&&!clash.Side2.IsReady)
                {
                    clash.Side2.IsReady = true;
                }
                else if (clash.Side2.IsReady && !clash.Side1.IsReady)
                {
                    clash.Side1.IsReady = true;
                }
                else
                {
                    return false;
                }

                _db.SaveChanges();

                _db.Database.CommitTransaction();
            }

            return true;
        }
    }
}
