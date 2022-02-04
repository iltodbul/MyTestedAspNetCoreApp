using System.ComponentModel.DataAnnotations;
using MyTestedAspNetCoreApp.ValidationAttributes;

namespace MyTestedAspNetCoreApp.ViewModel.Home
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class IndexViewModel
    {
        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        //[Range(1900, 2022)]
        [MinToCurrentYear(1900)] // custom validation attribute
        public int Year { get; set; }

        [Required]
        [MinLength(5)]
        public string Name { get; set; }

        public int UsersCount { get; set; }

        public int ProcessorCount { get; set; }

        public int Id { get; set; }
    }
}
