using System.ComponentModel.DataAnnotations;

namespace MyTestedAspNetCoreApp.ValidationAttributes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class MinToCurrentYearAttribute : ValidationAttribute
    {
        public MinToCurrentYearAttribute(int minValue)
        {
            MinValue = minValue;
            this.ErrorMessage = $"Year should be between {minValue} and {DateTime.UtcNow.Year}";
        }

        public int MinValue { get; }

        public override bool IsValid(object? value)
        {
            if (value is int intValue)
            {
                if (intValue <= DateTime.UtcNow.Year && intValue >= MinValue)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
