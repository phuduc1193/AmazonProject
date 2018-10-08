using System.Collections.Generic;
using System.Linq;
using Interfaces;
using Model;

namespace ProductProviders
{
    public class ProductProvider : IProductProvider
    {
        private List<Product> _products;

        public ProductProvider()
        {
            _products = new List<Product>();
        }

        public List<Product> GetList()
        {
            return _products;
        }

        public Product GetProductById(int id)
        {
            return _products.First(p => p.Id == id);
        }

        public Product AddProduct(Product product)
        {
            var id = _products.LastOrDefault()?.Id + 1;
            product.Id = id ?? 1;
            _products.Add(product);
            return product;
        }

        public Product UpdateProduct(int id, Product product)
        {
            DeleteProductById(id);
            product.Id = id;
            _products.Add(product);
            return product;
        }

        public Product DeleteProductById(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product != null) _products.Remove(product);
            return product;
        }
    }
}
