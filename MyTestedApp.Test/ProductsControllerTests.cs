using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyTestedAspNetCoreApp.Controllers;
using MyTestedAspNetCoreApp.Data;
using MyTestedAspNetCoreApp.Models;
using Xunit;

namespace MyTestedApp.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ProductsControllerTests
    {
        [Fact]
        public void GetShouldReturnTheProductIfFound()
        {
            var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("test");
            var dbContext = new ApplicationDbContext(optionBuilder.Options);

            var controller = new ProductsController(dbContext);

            var product = new Product()
            {
                Id = 2,
                Name = "product name",
                Price = 100,
            };
            dbContext.Products.Add(product);
            dbContext.SaveChanges();
            
            var result = controller.Get(2);

            Assert.NotNull(result);
            Assert.Equal("product name", result.Result.Value.Name);
        }

        [Fact]
        public void GetShouldReturnNotFoundIfTheProductDoesNotExist()
        {
            var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("test");
            var dbContext = new ApplicationDbContext(optionBuilder.Options);
            var controller = new ProductsController(dbContext);

            var result = controller.Get(3);

            Assert.Null(result.Result.Value);
            Assert.IsType<NotFoundResult>(result.Result.Result);
        }
    }
}
