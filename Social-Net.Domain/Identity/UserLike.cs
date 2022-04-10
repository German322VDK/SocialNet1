using SocialNet1.Domain.PostCom;

namespace Social_Net.Domain.Identity
{
    public class UserLike : Like
    {
        public bool IsPutinUser { get; set; } = true;
    }
}
