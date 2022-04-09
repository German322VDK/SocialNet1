using SocialNet1.Database.SQlite.Context;
using SocialNet1.Domain.Message;
using SocialNet1.Infrastructure.Interfaces.Based;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialNet1.Infrastructure.Services.Based
{
    public class ChatService : IChat
    {
        private readonly SocialNetDBSQlite _db;
        private readonly IUser _user;

        public ChatService(SocialNetDBSQlite db, IUser user)
        {
            _db = db;
            _user = user;
        }

        #region Chat

        public ICollection<ChatDTO> Get(string userName) =>
             Get().Where(ch => ch.UserName1 == userName || ch.UserName2 == userName).ToList();

        public ChatDTO Get(int chatId) =>
             Get().FirstOrDefault(ch => ch.Id == chatId);

        public ChatDTO Get(string userName1, string userName2) =>
             Get()
            .FirstOrDefault(ch => ch.UserName1 == userName1 && ch.UserName2 == userName2 || ch.UserName2 == userName1 && ch.UserName1 == userName2);

        public ICollection<ChatDTO> Get() =>
            _db.Chats.ToList();

        public bool CreateChat(string userName1, string userName2)
        {
            if (Get(userName1, userName2) is not null)
                return false;

            using (_db.Database.BeginTransaction())
            {
                _db.Chats.Add(new ChatDTO 
                { 
                    UserName1 = userName1,
                    UserName2 = userName2,
                });

                _db.SaveChanges();

                _db.Database.CommitTransaction();
            }

            return true;
        }

        #endregion

        #region Message

        public ICollection<MessageDTO> GetMessages(int chatId) =>
            Get(chatId).Messages.ToList();

        public MessageDTO GetMessage(int chatId, int messageHelpId) =>
           Get(chatId).Messages.FirstOrDefault(ms => ms.HelpId == messageHelpId);

        public bool AddMessage(string sender, string recipient, int chatId, string text)
        {
            if (Get(chatId) is null)
                return false;

            if(_user.Get(sender) is null || _user.Get(recipient) is null)
                return false;

            using (_db.Database.BeginTransaction())
            {
                var messages = _db.Chats
                    .FirstOrDefault(ch => ch.Id == chatId)
                    .Messages;

                _db.Chats
                    .FirstOrDefault(ch => ch.Id == chatId)
                    .LastTimeMess = DateTime.Now;

                _db.Chats
                    .FirstOrDefault(ch => ch.Id == chatId)
                    .Messages
                    .Add(new MessageDTO 
                    { 
                        SenderName = sender,
                        Content = text,
                        HelpId = messages is null || messages.Count == 0 ? 1 : messages.Last().HelpId + 1
                    });

                _db.SaveChanges();

                _db.Database.CommitTransaction();
            }

            return true;
        }

        

        #endregion
    }
}
