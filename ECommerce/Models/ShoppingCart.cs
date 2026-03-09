using System;
using System.Collections.Generic;
using System.Linq;

namespace ECommerce.Models
{
    public class ShoppingCart
    {
        private readonly List<CartItem> _items = new();

        public IReadOnlyCollection<CartItem> Items => _items.AsReadOnly();

        public void AddItem(Product product, int quantity)
        {
            var existing = _items.FirstOrDefault(i => i.Product.Id == product.Id);
            if (existing != null)
            {
                _items.Remove(existing);
                _items.Add(new CartItem(product, existing.Quantity + quantity));
            }
            else
            {
                _items.Add(new CartItem(product, quantity));
            }
        }

        public bool RemoveItem(int productId)
        {
            var item = _items.FirstOrDefault(i => i.Product.Id == productId);
            if (item != null)
            {
                _items.Remove(item);
                return true;
            }
            return false;
        }

        public void Clear()
        {
            _items.Clear();
        }

        public decimal GetTotal() => _items.Sum(i => i.LineTotal);

        public bool IsEmpty => !_items.Any();

        public void Display()
        {
            Console.WriteLine("\n--- Cart ---");

            if (IsEmpty)
            {
                Console.WriteLine("Cart is empty.");
                return;
            }

            foreach (var item in _items)
                Console.WriteLine($"{item.Product.Name} x {item.Quantity} = {item.LineTotal} RON");

            Console.WriteLine($"Subtotal: {GetTotal()} RON");
        }
    }
}