using Microsoft.AspNetCore.Mvc;
using MyTestedAspNetCoreApp.ViewModel.Home.ViewComponents;
using MyTestedAspNetCoreApp.ViewModel.Recipes;

namespace MyTestedAspNetCoreApp.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class RecipesController : Controller
    {
        public IActionResult Add(AddRecipesInputModel model)
        {
            return this.Json(model);
        }
    }
}
