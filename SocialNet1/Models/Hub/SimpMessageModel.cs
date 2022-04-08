using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNet1.Models.Hub
{
    public class SimpMessageModel
    {
        public string SenderName { get; set; }

        public string RecipientName { get; set; }

        public string Content { get; set; }

        public string Date { get; set; }

        public int Id { get; set; }
    }
}
