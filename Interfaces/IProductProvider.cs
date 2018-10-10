using Model;
using System.Collections.Generic;

namespace Interfaces
{
    public interface IProductProvider
    {
        List<Product> GetList();
        Product GetProductById(int id);
        int AddProduct(Product product);
        int UpdateProduct(int id, Product product);
        void DeleteProductById(int id);
        List<Product> Search(string query);
    }
}
