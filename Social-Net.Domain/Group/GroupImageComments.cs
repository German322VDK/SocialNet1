using Social_Net.Domain.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Social_Net.Domain.Group
{
    public class GroupImageComments : ImageComments 
    {
        [Required]
        public virtual ICollection<GroupCommentLike> GroupCommentLikes { get; set; } = new List<GroupCommentLike>();
    }
}
