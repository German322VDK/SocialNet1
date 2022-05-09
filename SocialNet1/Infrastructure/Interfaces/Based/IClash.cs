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

        ClashDTO Get(int clashId);

        bool Add(string thisGroupName, string groupName);

        bool Delete(string thisGroupName, string groupName);

        bool Confirm(string thisGroupName, string groupName);

        bool AddMessage(int clashId, string sender, string groupName, string text);

        bool AddLike(int clashId, bool is1, string sender);

        bool DeleteLike(int clashId, bool is1, string sender);
    }
}
