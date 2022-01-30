using System;
using MyTestedAspNetCoreApp.Services.Filters;

namespace MyTestedAspNetCoreApp.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [MyAddHeaderActionFilter]
    public class InfoController : Controller
    {
        public IActionResult Time()
        {
            return this.Content(DateTime.Now.ToLongTimeString());
        }

        [MyAddHeaderActionFilter]
        public IActionResult Date()
        {
            return this.Content(DateTime.Now.ToLongDateString());
        }
    }
}
