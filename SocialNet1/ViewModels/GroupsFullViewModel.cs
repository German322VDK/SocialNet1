using System.Collections.Generic;

namespace SocialNet1.ViewModels
{
    public class GroupsFullViewModel
    {
        public List<GroupViewModel> AllGroups { get; set; }

        public List<GroupViewModel> Admins { get; set; }

        public bool IsAuthor { get; set; }
    }
}
