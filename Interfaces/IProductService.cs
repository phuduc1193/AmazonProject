﻿using Model;
using System.Collections.Generic;

namespace Interfaces
{
    public interface IProductService
    {
        List<Product> GetList();
        Product GetProductById(int id);
        void AddProduct(Product product);
        void UpdateProduct(int id, Product product);
        void DeleteProductById(int id);
        List<Product> Search(string query);
        Product GetFeaturedProduct();
    }
}
