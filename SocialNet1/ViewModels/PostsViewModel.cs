using SocialNet1.Domain.PostCom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNet1.ViewModels
{
    public class PostsViewModel
    {
        public string AuthorName { get; set; }
        public ICollection<PostDTO> Posts { get; set; }
    }
}
