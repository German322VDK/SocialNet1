using Social_Net.Domain.Group;
using System.Collections.Generic;

namespace SocialNet1.Models
{
    public class PhotoGroupInfoAut
    {
        public int CommId { get; set; }

        public string GroupImage { get; set; }

        public string GroupFormat { get; set; }

        public string ShortGroupName { get; set; }

        public string GroupName { get; set; }

        public string GroupCoordinatesImage { get; set; }

        public string DateTime { get; set; }

        public string Comment { get; set; }

        public ICollection<GroupLike> Likes { get; set; }

        public string Color { get; set; }
    }
}
