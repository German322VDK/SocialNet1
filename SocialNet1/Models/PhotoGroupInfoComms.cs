using Social_Net.Domain.Group;
using System.Collections.Generic;

namespace SocialNet1.Models
{
    public class PhotoGroupInfoComms
    {
        public int CommId { get; set; }

        public int HelpId { get; set; }

        public string AuthorImage { get; set; }

        public string AuthorFormat { get; set; }

        public string AuthorUserName { get; set; }

        public string AuthorFirstName { get; set; }

        public string AuthorSecondName { get; set; }

        public string AuthorCoordinatesImage { get; set; }

        public string DateTime { get; set; }

        public string Comment { get; set; }

        public ICollection<GroupCommentLike> Likes { get; set; }

        public string Color { get; set; }
    }
}
