using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MyTestedAspNetCoreApp.Models;
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
        private readonly IWebHostEnvironment _webHostEnvironment;

        public RecipesController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Add()
        {
            // default value for the form
            var model = new AddRecipesInputModel
            {
                Type = RecipeType.Unknown,
                Date = DateTime.UtcNow,
                Time = new RecipeTimeInputModel
                {
                    CookingTime = 20,
                    PreparationTime = 10,
                }
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRecipesInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            // TODO Save data in db

            // запазване на файл в руут папката чрез стриим.
            await using (FileStream fs = new FileStream(this._webHostEnvironment.WebRootPath + "/user.png", FileMode.Create))
            {
                await model.Image.CopyToAsync(fs);
            }

            return this.RedirectToAction(nameof(ThankYou));
        }

        public IActionResult ThankYou()
        {
            return this.View();
        }
    }
}
