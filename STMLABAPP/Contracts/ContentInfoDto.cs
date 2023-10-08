using System;

namespace STMLABAPP.Contracts
{
    public class ContentInfoDto
    {
        public string ContentName { get; set; }
        public string FullContentName { get; set; }
        public long ContentSize { get; set; }
        public string ContentType { get; set; }
        public string FullContentNameCode => Uri.EscapeUriString(FullContentName);
    }
}