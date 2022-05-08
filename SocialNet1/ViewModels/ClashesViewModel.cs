using Social_Net.Domain.Clash;
using System.Collections.Generic;

namespace SocialNet1.ViewModels
{
    public class ClashesViewModel
    {
        public ICollection<ClashDTO> AllClashes { get; set; }

        public ICollection<ClashDTO> UserClashes { get; set; }
    }
}
