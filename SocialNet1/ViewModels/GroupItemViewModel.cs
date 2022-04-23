using SocialNet1.Domain.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
