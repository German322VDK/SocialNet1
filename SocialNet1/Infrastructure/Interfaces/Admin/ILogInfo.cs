using System.Collections.Generic;

namespace SocialNet1.Infrastructure.Interfaces.Admin
{
    public interface ILogInfo
    {
        public ICollection<string> GetFullLogs();

        public ICollection<string> GetErrorLogs();
    }
}
