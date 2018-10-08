using System.Collections.Generic;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace AmazonProject.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IEnumerable<Product> List()
        {
            return _productService.GetList();
        }

        [HttpGet("{id}")]
        public Product GetProduct(int id)
        {
            return _productService.GetProductById(id);
        }

        [HttpPost]
        public Product Post([FromBody] Product product)
        {
            return _productService.AddProduct(product);
        }

        [HttpPut("{id}")]
        public Product Put(int id, [FromBody] Product product)
        {
            return _productService.UpdateProduct(id, product);
        }

        [HttpDelete("{id}")]
        public Product Delete(int id)
        {
            return _productService.DeleteProductById(id);
        }
    }
}
