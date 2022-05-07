using System.Collections.Generic;

namespace SocialNet1.ViewModels
{
    public class GroupManagementViewModel
    {
        public List<GroupViewModel> Requests { get; set; }

        public List<GroupViewModel> All { get; set; }

        public string ThisGroupName { get; set; }
    }
}
