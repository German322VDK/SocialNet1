using SocialNet1.Domain.PostCom;

namespace Social_Net.Domain.Group
{
    public class GroupLike : Like
    {
        public bool IsPutinAdminGroup { get; set; } = true;
    }
}
