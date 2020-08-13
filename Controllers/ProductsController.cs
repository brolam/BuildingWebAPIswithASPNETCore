using Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Api.Classes;
using System.Linq;

namespace Api.Controllers
{
    [Route("api/{controller}")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ShopContext _context;

        public ProductsController(ShopContext context)
        {
            this._context = context;
            this._context.Database.EnsureCreated();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProduts([FromQuery] ProductQueryParameters queryParameters)
        {
            IQueryable<Product> products = _context.Products;
            if (queryParameters.HasSku)
            {
                products = products.Where(product => product.Sku == queryParameters.Sku);
            }

            if (queryParameters.HasMinAndMaxPrice)
            {
                products = products.Where(
                    product => product.Price >= queryParameters.MinPrice &&
                    product.Price <= queryParameters.MaxPrice
                );
            }

            if (queryParameters.HasSearch)
            {
                products = products.Where(
                    product => product.Name.ToLower().Contains(queryParameters.Search.ToLower())
                );
            }

            if (queryParameters.HasSortBy)
            {
                products = products.OrderByCustom(queryParameters.SortBy, queryParameters.SortOrder);
            }
            products = products
            .Skip(queryParameters.RegisterSkip)
            .Take(queryParameters.Size);
            return Ok(await products.ToArrayAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct([FromBody] Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return CreatedAtAction(
                "GetProduct",
                new { id = product.Id },
                product
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct([FromRoute] int id, [FromBody] Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }
            _context.Entry(product).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_context.Products.Find(id) == null)
                {
                    return NotFound();
                }
                throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id){
            var product = await _context.Products.FindAsync(id);
            if ( product == null) return NotFound();
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return product;
        }
    }
}