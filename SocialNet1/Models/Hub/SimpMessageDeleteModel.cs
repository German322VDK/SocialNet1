using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNet1.Models.Hub
{
    public class SimpMessageDeleteModel
    {
        public string SenderName { get; set; }

        public string RecipientName { get; set; }

        public int ChatId { get; set; }

        public int MessageHelpId { get; set; }

        public bool IsSuccess { get; set; }
    }
}
