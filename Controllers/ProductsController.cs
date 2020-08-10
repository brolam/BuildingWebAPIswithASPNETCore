using System.Collections.Generic;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Api.Controllers
{
    [Route("api/{controller}")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ShopContext _context;

        public ProductsController(ShopContext context){
            this._context = context;
            this._context.Database.EnsureCreated();
        }
        [HttpGet]
        public IEnumerable<Product> GetAllProduts(){
            return _context.Products.ToArray();
        }
    }
}