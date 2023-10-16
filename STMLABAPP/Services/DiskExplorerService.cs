using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using STMLABAPP.Contracts;
using STMLABAPP.Interfaces;

namespace STMLABAPP.Services
{
    public class DiskExplorerService : IDiskExplorerService
    {
        private readonly ILogger<DiskExplorerService> _logger;

        public DiskExplorerService(ILogger<DiskExplorerService> logger)
        {
            _logger = logger;
        }

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
                        TotalSize = await ConvertToMbytes(drive.TotalSize),
                        BusySize = await ConvertToMbytes(drive.TotalSize - drive.TotalFreeSpace)
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
                        ContentSize = await ConvertToMbytes(file.Length)
                    };
                    did.ContentList.Add(subfile);
                }
                catch(Exception e)
                {
                    _logger.LogError(e.Message);
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
                    dir.ContentSize = await ConvertToMbytes(await GetDirectorySize(subDirectory));
                    did.ContentList.Add(dir);
                }
                catch(Exception e)
                {
                    _logger.LogError(e.Message);
                }
            }

            did.DirectorySize = did.ContentList.Sum(x => x.ContentSize);

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

        private async Task<long> GetDirectorySize(DirectoryInfo directoryInfo)
        {
            long size = 0;
            
            try
            {
                size += directoryInfo.GetFiles().Sum(file => file.Length);
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
            }

            try
            {
                foreach (var subDirectory in directoryInfo.GetDirectories())
                {
                    size += await GetDirectorySize(subDirectory);
                }
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
            }
            return size;
        }

        private Task<long> ConvertToMbytes(long size)
        {
            return Task.FromResult(size / (1024 * 1024));
        }
    }
}