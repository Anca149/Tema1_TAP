using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using ECommerce.Models;

namespace ECommerce.Repositories
{
    public class JsonProductRepository : IProductRepository
    {
        private readonly string _filePath;
        private readonly List<Product> _products;

        public JsonProductRepository(string filePath)
        {
            _filePath = filePath;

            try
            {
                if (!File.Exists(_filePath))
                {
                    _products = new List<Product>();
                    SaveChanges();
                }
                else
                {
                    var json = File.ReadAllText(_filePath);
                    _products = JsonSerializer.Deserialize<List<Product>>(json)
                                ?? new List<Product>();
                }
            }
            catch
            {
                Console.WriteLine("Error reading products.json. Starting empty.");
                _products = new List<Product>();
            }
        }

        public IEnumerable<Product> GetAll() => _products.OrderBy(p => p.Id);

        public Product? GetById(int id) => _products.FirstOrDefault(p => p.Id == id);

        public void Add(Product product)
        {
            if (_products.Any(p => p.Id == product.Id))
                throw new InvalidOperationException("Product ID already exists.");

            _products.Add(product);
        }

        public void Update(Product product)
        {
            var existing = GetById(product.Id);
            if (existing == null) return;

            existing.Name = product.Name;
            existing.Price = product.Price;
        }

        public void Delete(int id)
        {
            var existing = GetById(id);
            if (existing != null)
                _products.Remove(existing);
        }

        public void SaveChanges()
        {
            try
            {
                var json = JsonSerializer.Serialize(_products, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                File.WriteAllText(_filePath, json);
            }
            catch
            {
                Console.WriteLine("Error saving products.json.");
            }
        }
    }
}