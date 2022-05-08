using SocialNet1.Domain.Group;
using System.Collections.Generic;

namespace SocialNet1.ViewModels
{
    public class GroupItemViewModel
    {
        public string MainImage { get; set; }

        public string MainFormat { get; set; }

        public string CoordImage { get; set; }

        public string MainName { get; set; }

        public string MainShortName { get; set; }

        public ICollection<GroupImages> Images { get; set; }

        public GroupDTO Group { get; set; }
    }
}
