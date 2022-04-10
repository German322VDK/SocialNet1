using SocialNet1.Domain.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNet1.ViewModels
{
    public class ChatsViewModel
    {
        public string UserName { get; set; }

        public ICollection<ChatDTO> Chats { get; set; }
    }
}
