using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNet1.Models.API
{
    public class AddCommentImageModel
    {
        public string Text { get; set; }
        public string Sender { get; set; }
        public string Recipient { get; set; }
        public string ImageId { get; set; }
    }
}
