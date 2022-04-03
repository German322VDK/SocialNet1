using SocialNet1.Domain.PostCom;

namespace Social_Net.Domain.Group
{
    public class GroupCommentLike : Like
    {
        public bool IsPutinAdminGroupCom { get; set; } = true;
    }
}
