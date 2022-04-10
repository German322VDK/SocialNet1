using SocialNet1.Domain.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNet1.ViewModels
{
    public class ChatViewModel
    {
        public ChatDTO Chat { get; set; }

        public string UserName { get; set; }

        public string AutorName { get; set; }
        
    }
}
