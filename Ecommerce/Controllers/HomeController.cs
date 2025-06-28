using System.Diagnostics;
using Ecommerce.Models;
using Ecommerce.Models.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Ecommerce.Controllers
{
    public class HomeController(ILogger<HomeController> logger, EcommerceContext context) : Controller
    {
        private readonly ILogger<HomeController> _logger = logger;
        private readonly EcommerceContext _context = context;

        [Authorize]
        public IActionResult Index()
        {
            var banners = _context.Banners.ToList();
            ViewData["banners"] = banners;
            return View();
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize]
        public IActionResult About()
        {
            return View();
        }

        [Authorize]
        public IActionResult Shop()
        {
            return View();
        }

        [Authorize]
        public IActionResult Product()
        {
            return View();
        }

        [Authorize]
        public IActionResult Blog()
        {
            return View();
        }

        [Authorize]
        public IActionResult BlogDetails()
        {
            return View();
        }

        public IActionResult Contact()
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
