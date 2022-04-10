using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNet1.Models.API
{
    public class LikeComPostModel
    {
        public string UserName1 { get; set; }

        public string UserName2 { get; set; }

        public int PostId { get; set; }

        public int ComId { get; set; }
    }
}
