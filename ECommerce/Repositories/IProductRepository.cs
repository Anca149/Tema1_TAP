using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Generic;
using ECommerce.Models;

namespace ECommerce.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Product? GetById(int id);
        void Add(Product product);
        void Update(Product product);
        void Delete(int id);
        void SaveChanges();
    }
}