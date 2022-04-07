using SocialNet1.Domain.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNet1.Infrastructure.Interfaces.Based
{
    public interface IChat
    {
        ICollection<ChatDTO> Get(string userName);

        ICollection<ChatDTO> Get();

        ChatDTO Get(int chatId);


        ChatDTO Get(string userName1, string userName2);

        bool CreateChat(string userName1, string userName2);
    }
}
