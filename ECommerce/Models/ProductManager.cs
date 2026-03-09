using System;
using ECommerce.Repositories;
using static ECommerce.Helpers;

namespace ECommerce.Models
{
    public class ProductManager
    {
        private readonly IProductRepository _repo;

        public ProductManager(IProductRepository repo)
        {
            _repo = repo;
        }

        public void DisplayProducts()
        {
            Console.WriteLine("\n--- Products ---");
            foreach (var p in _repo.GetAll())
                Console.WriteLine(p);
        }

        public void AddProduct()
        {
            try
            {
                int id = ReadInt("ID: ");
                Console.Write("Name: ");
                string name = Console.ReadLine()!;
                Console.Write("Price: ");
                decimal price = decimal.Parse(Console.ReadLine()!);

                _repo.Add(new Product(id, name, price));
                Console.WriteLine("Product added (not yet saved).");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding product: {ex.Message}");
            }
        }

        public void EditProduct()
        {
            int id = ReadInt("Product ID: ");
            var existing = _repo.GetById(id);

            if (existing == null)
            {
                Console.WriteLine("Product not found.");
                return;
            }

            Console.Write($"New name ({existing.Name}): ");
            string name = Console.ReadLine()!;
            if (string.IsNullOrWhiteSpace(name)) name = existing.Name;

            Console.Write($"New price ({existing.Price}): ");
            string priceInput = Console.ReadLine()!;
            decimal price = existing.Price;

            if (!string.IsNullOrWhiteSpace(priceInput))
                price = decimal.Parse(priceInput);

            _repo.Update(new Product(id, name, price));
            Console.WriteLine("Product updated (not yet saved).");
        }

        public void DeleteProduct()
        {
            int id = ReadInt("Product ID: ");
            _repo.Delete(id);
            Console.WriteLine("Product deleted (not yet saved).");
        }

        public void SaveChanges()
        {
            _repo.SaveChanges();
            Console.WriteLine("Changes saved to JSON file.");
        }
    }
}