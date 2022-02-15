using System.Collections;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using MyTestedAspNetCoreApp.Data;
using MyTestedAspNetCoreApp.Models;
using MyTestedAspNetCoreApp.ViewModel.Products;

namespace MyTestedAspNetCoreApp.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductsController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            var products = this._dbContext.Products.ToArray();

            return this.Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            var product = await this._dbContext.Products.FindAsync(id);

            if (product == null)
            {
                return this.NotFound();
            }

            return product;
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductInputModel inputModel)
        {
            var product = new Product
            {
                Name = inputModel.Name,
                Description = inputModel.Description,
                ActiveFrom = inputModel.ActiveFrom,
                Price = inputModel.Price,
            };

            await this._dbContext.Products.AddAsync(product);
            await this._dbContext.SaveChangesAsync();

            return this.CreatedAtAction("GetById", new { id = product.Id }, product);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Product product)
        {
            this._dbContext.Products.Update(product);
            await this._dbContext.SaveChangesAsync();

            return this.NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await this._dbContext.Products.FindAsync(id);

            if (product == null)
            {
                return this.NotFound();
            }

            this._dbContext.Products.Remove(product);
            await this._dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
