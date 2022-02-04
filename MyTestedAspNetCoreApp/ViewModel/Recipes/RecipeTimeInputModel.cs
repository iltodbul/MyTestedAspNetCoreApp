using System.ComponentModel.DataAnnotations;

namespace MyTestedAspNetCoreApp.ViewModel.Recipes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class RecipeTimeInputModel : IValidatableObject
    {
        public int CookingTime { get; set; }

        public int PreparationTime { get; set; }

        // validating the all input model
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (PreparationTime > CookingTime)
            {
                yield return new ValidationResult("Prep. time can not be more than Cooking time");
            }

            if (CookingTime > 2*24*60)
            {
                yield return new ValidationResult("Cooking time can not be more than 2 days");
            }
        }
    }
}
