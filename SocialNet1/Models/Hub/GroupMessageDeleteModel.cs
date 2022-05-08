using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNet1.Models.Hub
{
    public class GroupMessageDeleteModel
    {
        public int MessageHelpId { get; set; }

        public string GroupName { get; set; }

        public bool IsSuccess { get; set; }
    }
}
