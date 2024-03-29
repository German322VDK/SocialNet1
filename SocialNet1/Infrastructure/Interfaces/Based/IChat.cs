﻿using SocialNet1.Domain.Message;
using System.Collections.Generic;

namespace SocialNet1.Infrastructure.Interfaces.Based
{
    public interface IChat
    {
        ICollection<ChatDTO> Get(string userName);

        ICollection<ChatDTO> Get();

        ChatDTO Get(int chatId);

        ChatDTO Get(string userName1, string userName2);

        bool CreateChat(string userName1, string userName2);


        ICollection<MessageDTO> GetMessages(int chatId);

        MessageDTO GetMessage(int chatId, int messageHelpId);

        int AddMessage(string sender, string recipient, int chatId, string text);

        bool DeleteMessage(int chatId, int messageHelpId);

        bool UpdateMessage(int chatId, int messageHelpId, string text);
    }
}
