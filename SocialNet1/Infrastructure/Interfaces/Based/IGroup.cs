using SocialNet1.Domain.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNet1.Infrastructure.Interfaces.Based
{
    public interface IGroup
    {
        ICollection<GroupDTO> GetAll();

        ICollection<GroupDTO> Get(string[] groups);

        GroupDTO Get(string group);
    }
}
