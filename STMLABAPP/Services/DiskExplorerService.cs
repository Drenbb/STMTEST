using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using STMLABAPP.Contracts;
using STMLABAPP.Interfaces;

namespace STMLABAPP.Services
{
    public class DiskExplorerService : IDiskExplorerService
    {
        public async Task<List<DiskInfoDto>> CheckDiskSize()
        {
            List<DiskInfoDto> listDisks = new List<DiskInfoDto>();
            DriveInfo[] drives = DriveInfo.GetDrives();

            foreach (var drive in drives)
            {
                if (drive.IsReady)
                {
                    listDisks.Add(new DiskInfoDto()
                    {
                        DiskName = drive.Name, 
                        TotalSize = drive.TotalSize,
                        BusySize = drive.TotalSize - drive.TotalFreeSpace
                    });
                }
            }

            return listDisks;
        }
        

        public async Task<DirectoryInfoDto> GetDirectoryInfo(string path)
        {
            var did = new DirectoryInfoDto();
            DirectoryInfo directoryInfo = new DirectoryInfo(path);

            did.DirectoryName = directoryInfo.FullName;
            did.ContentList = new List<ContentInfoDto>();
            
            foreach (var file in directoryInfo.GetFiles())
            {
                try
                {
                    var subfile = new ContentInfoDto()
                    {
                        ContentName = file.Name,
                        ContentType = file.Extension,
                        FullContentName = file.FullName,
                        ContentSize = file.Length
                    };
                    did.ContentList.Add(subfile);
                }
                catch (Exception e)
                {
                    
                }
                
            }

            foreach (var subDirectory in directoryInfo.GetDirectories())
            {
                var dir = new ContentInfoDto()
                {
                    ContentName = subDirectory.Name,
                    FullContentName = subDirectory.FullName,
                    ContentType = "Folder"
                };

                try
                {
                    foreach (var file in subDirectory.GetFiles())
                    {
                        dir.ContentSize += file.Length;
                    }
                    did.ContentList.Add(dir);
                }
                catch (Exception e)
                {
                }

            }

            did.ContentList = did.ContentList.OrderBy(x => x.ContentSize).ToList();
            return did;
        }

    }
}