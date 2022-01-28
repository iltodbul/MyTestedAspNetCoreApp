using MyTestedAspNetCoreApp.Data;
using MyTestedAspNetCoreApp.Services;
using MyTestedAspNetCoreApp.ViewModel.Home;

namespace MyTestedAspNetCoreApp.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using MyTestedAspNetCoreApp.Models;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;
        private readonly IShortStringService _shortStringService;

        public HomeController(
            ILogger<HomeController> logger, 
            ApplicationDbContext db,
            IShortStringService shortStringService)
        {
            _logger = logger;
            _db = db;
            _shortStringService = shortStringService;
        }

        public IActionResult Index(int id)
        {
            var usersCount = this._db.Users.Count();
            var userName = this.User.Identity.Name;

            var viewModel = new IndexViewModel()
            {
                Description = "A ready-to-use template for ASP.NET Core with repositories, services, models mapping, DI and StyleCop warnings fixed.",
                UsersCount = usersCount,
                ProcessorCount = Environment.ProcessorCount,
                Name = userName,
                Year = DateTime.UtcNow.Year,
                Id = id,
            };

            return View(viewModel);
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
