using Social_Net.Domain.Base;
using SocialNet1.Domain.Group;
using System.ComponentModel.DataAnnotations;

namespace Social_Net.Domain.Message
{
    public class GroupChatDTO : BaseChat
    {
        [Required]
        public virtual GroupDTO Group { get; set; }
    }
}
