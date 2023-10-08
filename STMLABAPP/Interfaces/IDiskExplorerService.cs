using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using STMLABAPP.Contracts;

namespace STMLABAPP.Interfaces
{
    public interface IDiskExplorerService
    {
        Task<List<DiskInfoDto>> CheckDiskSize();
        Task<DirectoryInfoDto> GetDirectoryInfo(FindDirectoryDto dto);
    }
}