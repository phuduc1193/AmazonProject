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

        public void AddProduct(Product product)
        {
            _productProvider.AddProduct(product);
        }

        public void UpdateProduct(int id, Product product)
        {
            _productProvider.UpdateProduct(id, product);
        }

        public void DeleteProductById(int id)
        {
            _productProvider.DeleteProductById(id);
        }

        public List<Product> Search(string query)
        {
            return _productProvider.Search(query);
        }
    }
}
