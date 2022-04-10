using SocialNet1.Domain.PostCom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Net.Domain.PostCom
{
    public class CommentLike : Like
    {
        public bool IsPutinLiked { get; set; } = true;
    }
}
