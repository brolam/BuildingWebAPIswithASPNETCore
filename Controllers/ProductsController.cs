using System;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/{controller}")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public string GetProduts(){
            return "OK.";
        }
    }
}