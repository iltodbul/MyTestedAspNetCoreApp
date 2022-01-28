using System.ComponentModel.DataAnnotations;

namespace MyTestedAspNetCoreApp.ViewModel.Home
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class IndexViewModel
    {
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [MinLength(1900, ErrorMessage = "Year must be mor than 1900")]
        public int Year { get; set; }

        public string Name { get; set; }

        public int UsersCount { get; set; }

        public int ProcessorCount { get; set; }

        public int Id { get; set; }
    }
}
