﻿using Social_Net.Domain.Clash;
using SocialNet1.Database.SQlite.Context;
using SocialNet1.Domain.Base;
using SocialNet1.Domain.Message;
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
            GetAll().Where(cl => cl.Side1.Group.ShortGroupName == groupName || cl.Side2.Group.ShortGroupName == groupName).ToList();

        public ICollection<ClashDTO> GetByUser(string userName)
        {
            var groupNames = _db.UserGroupStatuses.Where(ugs => ugs.UserName == userName).Select(ugs => ugs.GroupName);

            return GetAll()
                .Where(cl => groupNames.Contains(cl.Side1.Group.ShortGroupName) || groupNames.Contains(cl.Side2.Group.ShortGroupName))
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

        public bool Delete(string thisGroupName, string groupName)
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
                _db.RemoveRange(clash.Side1.GroupLikes);

                _db.Remove(clash.Side1);

                _db.RemoveRange(clash.Side2.GroupLikes);

                _db.Remove(clash.Side2);

                foreach (var message in clash.Messages)
                {
                    _db.RemoveRange(message.Images);

                    _db.Remove(message);
                }

                _db.Remove(clash);

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

        public bool AddMessage(int clashId, string sender, string groupName, string text)
        {
            var clash = Get(clashId);

            var user = _db.Users.FirstOrDefault(us => us.UserName == sender);

            var group = _db.Groups.FirstOrDefault(gr => gr.ShortGroupName == groupName);

            if(clash is null || user is null || group is null || string.IsNullOrEmpty(text))
            {
                return false;
            }

            using (_db.Database.BeginTransaction())
            {
                clash.Messages.Add(new MessageDTO 
                { 
                    SenderName = sender,
                    Content = text
                });

                _db.SaveChanges();

                _db.Database.CommitTransaction();
            }

            return true;
        }

        public bool AddLike(int clashId, bool is1, string sender)
        {
            var clash = Get(clashId);

            var user = _db.Users.FirstOrDefault(us => us.UserName == sender);

            if (clash is null || user is null)
            {
                return false;
            }

            ClashLike like;

            if (is1)
            {
                like = clash.Side1.GroupLikes.FirstOrDefault(lk => lk.Likers == sender);
            }
            else
            {
                like = clash.Side2.GroupLikes.FirstOrDefault(lk => lk.Likers == sender);
            }

            if(like is not null)
            {
                return false;
            }

            using (_db.Database.BeginTransaction())
            {
                if (is1)
                {
                    clash.Side1.GroupLikes.Add(new ClashLike
                    {
                        Emoji = Emoji.Like,
                        Likers = sender
                    });
                }
                else
                {
                    clash.Side2.GroupLikes.Add(new ClashLike
                    {
                        Emoji = Emoji.Like,
                        Likers = sender
                    });
                }

                _db.SaveChanges();

                _db.Database.CommitTransaction();
            }

            return true;
        }

        public bool DeleteLike(int clashId, bool is1, string sender)
        {
            var clash = Get(clashId);

            var user = _db.Users.FirstOrDefault(us => us.UserName == sender);

            if (clash is null || user is null)
            {
                return false;
            }

            ClashLike like;

            if (is1)
            {
                like = clash.Side1.GroupLikes.FirstOrDefault(lk => lk.Likers == sender);
            }
            else
            {
                like = clash.Side2.GroupLikes.FirstOrDefault(lk => lk.Likers == sender);
            }

            if (like is null)
            {
                return false;
            }

            using (_db.Database.BeginTransaction())
            {
                _db.Remove(like);

                _db.SaveChanges();

                _db.Database.CommitTransaction();
            }

            return true;
        }
    }
}
