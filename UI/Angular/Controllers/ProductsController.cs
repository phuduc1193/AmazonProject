using System.Collections.Generic;
using AmazonProjectAngular.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace UI.Angular.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        //private IProductService _productService;

        //public ProductsController(IProductService productService)
        //{
        //    _productService = productService;
        //}

        //[HttpGet]
        //public IEnumerable<Product> List()
        //{
        //    return _productService.GetList();
        //}

        //[HttpGet("{id}")]
        //public Product GetProduct(int id)
        //{
        //    return _productService.GetProductById(id);
        //}

        //[HttpPost]
        //public void Post([FromBody] Product product)
        //{
        //    _productService.AddProduct(product);
        //}

        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] Product product)
        //{
        //    _productService.UpdateProduct(id, product);
        //}

        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //    _productService.DeleteProductById(id);
        //}

        //[HttpPost("[action]")]
        //public IEnumerable<Product> Search([FromBody]SearchProductViewModel search)
        //{
        //    return _productService.Search(search.Query);
        //}

        //[HttpGet("[action]")]
        //public Product Featured()
        //{
        //    return _productService.GetFeaturedProduct();
        //}
    }
}
