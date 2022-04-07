using SocialNet1.Database.SQlite.Context;
using SocialNet1.Domain.Message;
using SocialNet1.Infrastructure.Interfaces.Based;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNet1.Infrastructure.Services.Based
{
    public class ChatService : IChat
    {
        private readonly SocialNetDBSQlite _db;

        public ChatService(SocialNetDBSQlite db)
        {
            _db = db;
        }

        public ICollection<ChatDTO> Get(string userName) =>
             Get().Where(ch => ch.UserName1 == userName || ch.UserName2 == userName).ToList();

        public ChatDTO Get(int chatId) =>
             Get().FirstOrDefault(ch => ch.Id == chatId);

        public ICollection<ChatDTO> Get() =>
            _db.Chats.ToList();
    }
}
