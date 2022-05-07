using Social_Net.Domain.Clash;
using System.Collections.Generic;

namespace SocialNet1.Infrastructure.Interfaces.Based
{
    public interface IClash
    {
        ICollection<ClashDTO> GetAll();

        ICollection<ClashDTO> GetByGroup(string groupName);

        ICollection<ClashDTO> GetByUser(string userName);

        ICollection<string> GetReadyGroups(string groupName);

        ClashDTO Get(string groupName1, string groupName2);

        bool Add(string thisGroupName, string groupName);

        bool Confirm(string thisGroupName, string groupName); 
    }
}
