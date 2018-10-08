using Model;
using System.Collections.Generic;

namespace Interfaces
{
    public interface IProductProvider
    {
        List<Product> GetList();
        Product GetProductById(int id);
        Product AddProduct(Product product);
        Product UpdateProduct(int id, Product product);
        Product DeleteProductById(int id);
    }
}
