using System;

namespace STMLABAPP.Contracts
{
    public class FindDirectoryDto
    {
        public string Path { get; set; }
        public bool? OrderByDesc { get; set; }
    }
}