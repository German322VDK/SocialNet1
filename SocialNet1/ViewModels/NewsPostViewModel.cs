using SocialNet1.Domain.PostCom;

namespace SocialNet1.ViewModels
{
    public class NewsPostViewModel
    {
        public string AuthorName { get; set; }

        public PostDTO Post { get; set; }

        public bool IsUser { get; set; }
    }
}
