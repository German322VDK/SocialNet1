using Social_Net.Domain.Message;
using SocialNet1.Database.SQlite.Context;
using SocialNet1.Domain.Message;
using SocialNet1.Infrastructure.Interfaces.Based;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNet1.Infrastructure.Services.Based
{
    public class GroupChatService : IGroupChat
    {
        private readonly SocialNetDBSQlite _db;

        public GroupChatService(SocialNetDBSQlite db)
        {
            _db = db;
        }

        public GroupChatDTO Get(string groupName) =>
            GetAll().FirstOrDefault(gc => gc.Group.ShortGroupName == groupName);

        public ICollection<GroupChatDTO> Get(string[] groupNames) =>
            GetAll().Where(gs => Array.Exists(groupNames, it => it == gs.Group.ShortGroupName)).ToList();

        public ICollection<GroupChatDTO> GetAll() =>
            _db.GroupChats.ToList();

        public bool AddMessage(string sender, string groupName, string text)
        {
            var group = _db.Groups.FirstOrDefault(gr => gr.ShortGroupName == groupName);

            var user = _db.Users.FirstOrDefault(us => us.UserName == sender);


            if(group is null || user is null || string.IsNullOrEmpty(text))
            {
                return false;
            }

            var groupChat = Get(groupName);

            if (groupChat is null)
            {
                return false;
            }

            int helpId = 0;

            using (_db.Database.BeginTransaction())
            {
                var messages = _db.GroupChats
                    .FirstOrDefault(ch => ch.Group.ShortGroupName == groupName)
                    .Messages;

                _db.GroupChats
                    .FirstOrDefault(ch => ch.Group.ShortGroupName == groupName)
                    .LastTimeMess = DateTime.Now;

                helpId = messages is null || messages.Count == 0 ? 1 : messages.Last().HelpId + 1;

                _db.GroupChats
                    .FirstOrDefault(gs => gs.Group.ShortGroupName == groupName)
                    .Messages.Add(new MessageDTO
                    {
                        SenderName = user.UserName,
                        Content = text,
                        HelpId = helpId
                    });

                _db.SaveChanges();

                _db.Database.CommitTransaction();
            }

            return true;
        }

        public bool DeleteMessage(string groupName, int messageHelpId)
        {
            var group = _db.Groups.FirstOrDefault(gr => gr.ShortGroupName == groupName);

            if (group is null)
            {
                return false;
            }

            var groupChat = Get(groupName);

            if (groupChat is null)
            {
                return false;
            }

            var message = groupChat.Messages
                .FirstOrDefault(ms => ms.HelpId == messageHelpId);

            if (message is null)
            {
                return false;
            }

            using (_db.Database.BeginTransaction())
            {
                _db.RemoveRange(message.Images);

                _db.Remove(message);

                _db.SaveChanges();

                _db.Database.CommitTransaction();
            }

            return true;
        }
    }
}
