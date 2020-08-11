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
        public async Task<IActionResult> GetAllProduts([FromQuery] QueryParameters queryParameters)
        {
            IQueryable<Product> products = _context.Products;
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
    }
}