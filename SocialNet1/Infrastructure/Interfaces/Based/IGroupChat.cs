using Social_Net.Domain.Message;
using System.Collections.Generic;

namespace SocialNet1.Infrastructure.Interfaces.Based
{
    public interface IGroupChat
    {
        ICollection<GroupChatDTO> GetAll();

        ICollection<GroupChatDTO> Get(string[] groupNames);

        GroupChatDTO Get(string groupName);

        bool AddMessage(string sender, string groupName, string text);

        bool DeleteMessage(string groupName, int messageHelpId);
    }
}
