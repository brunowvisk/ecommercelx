using System.Diagnostics;
using Ecommerce.Models;
using Ecommerce.Models.Database;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    public class HomeController(ILogger<HomeController> logger, EcommerceContext context) : Controller
    {
        private readonly ILogger<HomeController> _logger = logger;
        private readonly EcommerceContext _context = context;

        public IActionResult Index()
        {
            var banners = _context.Banners.ToList();
            ViewData["banners"] = banners;
            return View();
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
