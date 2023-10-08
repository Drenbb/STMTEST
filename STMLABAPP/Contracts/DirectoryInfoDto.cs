using System;
using System.Collections.Generic;

namespace STMLABAPP.Contracts
{
    public class DirectoryInfoDto
    {
        public string DirectoryName { get; set; }
        public string PrevPath { get; set; }
        public string PrevPathCode => PrevPath != null ? Uri.EscapeUriString(PrevPath) : null;
        public string DirectoryNameCode => Uri.EscapeUriString(DirectoryName);
        public long DirectorySize { get; set; }
        public List<ContentInfoDto> ContentList { get; set; }
    }
}