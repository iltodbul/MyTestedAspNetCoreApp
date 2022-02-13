using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using MyTestedAspNetCoreApp.Models;
using MyTestedAspNetCoreApp.ValidationAttributes;

namespace MyTestedAspNetCoreApp.ViewModel.Recipes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class AddRecipesInputModel
    {
        [Required]
        [MinLength(5, ErrorMessage = "Name must be minimum of 5 characters")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [MinToCurrentYear(1900)] // custom validation attribute
        public int Year { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "First time the recipe is cooked")]
        public DateTime Date { get; set; }

        public RecipeTimeInputModel Time { get; set; }

        public RecipeType Type { get; set; }

        public ICollection<string> Ingredients { get; set; }

        public IFormFile Image { get; set; }
    }

    // Query string example: https://localhost:44392/Recipes/Add?year=2021&type=fastCook&name=Muska&description=Some%20description%20here&date=2022/6/23&ingredients=potatoes&ingredients=meat&time.cookingTime=60&time.PreparationTime=20
}
