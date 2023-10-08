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
                        TotalSize = ConvertToMbytes(drive.TotalSize),
                        BusySize = ConvertToMbytes(drive.TotalSize - drive.TotalFreeSpace)
                    });
                }
            }

            return listDisks;
        }
        

        public async Task<DirectoryInfoDto> GetDirectoryInfo(FindDirectoryDto dto)
        {
            var did = new DirectoryInfoDto();
            DirectoryInfo directoryInfo = new DirectoryInfo(dto.Path);

            did.DirectoryName = directoryInfo.FullName;
            did.ContentList = new List<ContentInfoDto>();
            did.PrevPath = directoryInfo.Parent?.FullName;
            
            foreach (var file in directoryInfo.GetFiles())
            {
                try
                {
                    var subfile = new ContentInfoDto()
                    {
                        ContentName = file.Name,
                        ContentType = file.Extension,
                        FullContentName = file.FullName,
                        ContentSize = ConvertToMbytes(file.Length)
                    };
                    did.DirectorySize += subfile.ContentSize;
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
                        dir.ContentSize += ConvertToMbytes(file.Length);
                    }

                    did.DirectorySize += dir.ContentSize;
                    did.ContentList.Add(dir);
                }
                catch (Exception e)
                {
                }

            }

            if (dto.OrderByDesc == false)
            {
                did.ContentList = did.ContentList.OrderBy(x => x.ContentSize).ToList();
            }
            else
            {
                did.ContentList = did.ContentList.OrderByDescending(x => x.ContentSize).ToList();
            }
            
            return did;
        }

        private long ConvertToMbytes(long size)
        {
            return size / (1024 * 1024);
        }

    }
}