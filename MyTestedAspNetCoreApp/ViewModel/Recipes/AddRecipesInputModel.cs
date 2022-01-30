using MyTestedAspNetCoreApp.Models;

namespace MyTestedAspNetCoreApp.ViewModel.Recipes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class AddRecipesInputModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public RecipeTimeInputModel Time { get; set; }

        public RecipeType Type { get; set; }

        public ICollection<string> Ingredients { get; set; }
    }

    // Query string example: https://localhost:44392/Recipes/Add?type=fastCook&name=Muska&description=Some%20description%20here&date=2022/6/23&ingredients=potatoes&ingredients=meat&time.cookingTime=20&time.PreparationTime=60
}
