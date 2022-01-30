using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
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
        private static string description =
            "A ready-to-use template for ASP.NET Core with repositories, services, models mapping, DI and StyleCop warnings fixed.";

        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;
        private readonly IShortStringService _shortStringService;
        private readonly IConfiguration _configuration;
        private readonly IOptions<ErrorViewModel> _options;

        public HomeController(
            ILogger<HomeController> logger, 
            ApplicationDbContext db,
            IShortStringService shortStringService,
            IConfiguration configuration,
            IOptions<ErrorViewModel> options)
        {
            _logger = logger;
            _db = db;
            _shortStringService = shortStringService;
            _configuration = configuration;
            _options = options;
        }

        public IActionResult Index(int id)
        {
            var usersCount = this._db.Users.Count();
            var userName = this.User.Identity.Name;

            var viewModel = new IndexViewModel()
            {
                Description = description,
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
            var viewModel = new IndexViewModel
            {
                Description = description,
                Name = "Name of IndexViewModel",
            };

            return View(viewModel);
        }

        public IActionResult Exception() => throw new Exception();

        public IActionResult StatusCodeError(int errorCode)
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
