using System.Collections.Generic;
using Interfaces;
using Model;

namespace ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IProductProvider _productProvider;

        public ProductService(IProductProvider productProvider)
        {
            _productProvider = productProvider;
        }

        public List<Product> GetList()
        {
            return _productProvider.GetList();
        }

        public Product GetProductById(int id)
        {
            return _productProvider.GetProductById(id);
        }

        public Product AddProduct(Product product)
        {
            return _productProvider.AddProduct(product);
        }

        public Product UpdateProduct(int id, Product product)
        {
            return _productProvider.UpdateProduct(id, product);
        }

        public Product DeleteProductById(int id)
        {
            return _productProvider.DeleteProductById(id);
        }
    }
}
