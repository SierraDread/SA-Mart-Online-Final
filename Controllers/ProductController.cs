using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Version2SAMart.Data;
using Version2SAMart.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Version2SAMart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly Version2SAMartContext Context;

        public ProductController(Version2SAMartContext context)
        {
            Context = context;
        }

        [HttpGet("{id}")] 
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await Context.Products.FindAsync(id);
            if (product == null) { return NotFound(); }
            return product;
        }

        [HttpGet] 
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts() => await Context.Products.ToListAsync();

        [HttpPost] 
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            Context.Products.Add(product);
            await Context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await Context.Products.FindAsync(id);
            if (product == null) { return NotFound(); }

            Context.Products.Remove(product);
            await Context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id) { return BadRequest();}
           
            Context.Entry(product).State = EntityState.Modified;
            try
            {
                await Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id)) {return NotFound();} 
                else throw;
            }

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return Context.Products.Any(e => e.Id == id);
        }
    }
}
