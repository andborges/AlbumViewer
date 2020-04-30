using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AlbumViewer.Models;
using AlbumViewer.Application.Services;

namespace AlbumViewer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IAlbumApiService _albumApiService;

        public HomeController(ILogger<HomeController> logger, IAlbumApiService albumApiService)
        {
            _logger = logger;
            _albumApiService = albumApiService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _albumApiService.GetAlbunsAsync();

            return View(model);
        }

        public async Task<IActionResult> Photos(int id)
        {
            var model = await _albumApiService.GetPhotosAsync(id);

            return Ok(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
