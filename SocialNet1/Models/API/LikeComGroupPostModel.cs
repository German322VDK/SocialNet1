namespace SocialNet1.Models.API
{
    public class LikeComGroupPostModel
    {
        public string GroupName { get; set; }

        public string UserName { get; set; }

        public int PostId { get; set; }

        public int ComId { get; set; }
    }
}
