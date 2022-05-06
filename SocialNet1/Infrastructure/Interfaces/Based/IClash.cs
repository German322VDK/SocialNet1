using Social_Net.Domain.Clash;
using System.Collections.Generic;

namespace SocialNet1.Infrastructure.Interfaces.Based
{
    public interface IClash
    {
        ICollection<ClashDTO> GetAll();

        ICollection<ClashDTO> GetByGroup(string groupName);

        ICollection<ClashDTO> GetByUser(string userName);
    }
}
