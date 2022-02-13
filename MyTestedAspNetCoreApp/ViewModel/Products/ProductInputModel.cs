using System.ComponentModel.DataAnnotations;

namespace MyTestedAspNetCoreApp.ViewModel.Products
{
    using System;
    
    public class ProductInputModel
    {
        [Required]
        [MinLength(10)]
        public string Name { get; set; }

        [MinLength(10)]
        public string Description { get; set; }

        public DateTime ActiveFrom { get; set; }

        [Range(0, 1000)]
        public double Price { get; set; }
    }
}
