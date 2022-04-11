using SocialNet1.Domain.Identity;
using SocialNet1.Domain.Message;
using System.Collections.Generic;

namespace SocialNet1.ViewModels
{
    public class ChatsViewModel
    {
        public string UserName { get; set; }

        public ICollection<ChatDTO> Chats { get; set; }

        public ICollection<UserDTO> Friends { get; set; }
    }
}
