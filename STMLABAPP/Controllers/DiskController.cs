using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using STMLABAPP.Interfaces;

namespace STMLABAPP.Controllers
{
    public class DiskController : Controller
    {
        private readonly IDiskExplorerService _diskExplorerService;

        public DiskController(IDiskExplorerService diskExplorerService)
        {
            _diskExplorerService = diskExplorerService;
        }
        
        public async Task<IActionResult> Index()
        {
            var disks = await _diskExplorerService.CheckDiskSize();
            return View(disks);
        }
        
        // public async Task<IActionResult> DirectoryInformatuon()
        // {
        //     var disks = await _diskExplorerService.CheckDiskSize();
        //     return View(disks);
        // }
    }
}