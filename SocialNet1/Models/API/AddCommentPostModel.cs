using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNet1.Models.API
{
    public class AddCommentPostModel
    {
        public string Text { get; set; }
        public string Commenter { get; set; }
        public string UserName { get; set; }
        public int PostId { get; set; }
    }
}
