using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using STMLABAPP.Contracts;
using STMLABAPP.Interfaces;

namespace STMLABAPP.Controllers
{
    public class DirictoryController : Controller
    {
        private readonly IDiskExplorerService _diskExplorerService;

        public DirictoryController(IDiskExplorerService diskExplorerService)
        {
            _diskExplorerService = diskExplorerService;
        }
        
        public async Task<IActionResult> Index([FromBody] FindDirectoryDto dto)
        {
            var directoryInfo = await _diskExplorerService.GetDirectoryInfo(dto);
            return View(directoryInfo);
        }
    }
}