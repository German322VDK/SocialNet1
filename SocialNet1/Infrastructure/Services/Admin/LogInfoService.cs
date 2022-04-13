using SocialNet1.Infrastructure.Interfaces.Admin;
using System.Collections.Generic;
using System.IO;

namespace SocialNet1.Infrastructure.Services.Admin
{
    public class LogInfoService : ILogInfo
    {
        public ICollection<string> GetErrorLogs() =>
            GetFileNames("Logs/Error");

        public ICollection<string> GetFullLogs() =>
            GetFileNames("Logs/Info");

        public ICollection<string> GetFileNames(string dir)
        {
            var files = Directory.GetFiles(dir);

            var fullFiles = new List<string>();

            foreach (var item in files)
            {
                fullFiles.Add(Path.GetFullPath(item));
            }

            return fullFiles;
        }


    }
}
