using System.Collections.Generic;

namespace STMLABAPP.Contracts
{
    public class DirectoryInfoDto
    {
        public string DirectoryName { get; set; }
        public long DirectorySize { get; set; }
        public List<ContentInfoDto> ContentList { get; set; }
    }
}