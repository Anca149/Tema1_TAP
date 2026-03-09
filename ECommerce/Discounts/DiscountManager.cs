using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using static ECommerce.Helpers;

namespace ECommerce.Discounts
{
    public class DiscountManager
    {
        private readonly string _filePath;
        private readonly List<IDiscountStrategy> _discounts = new();

        public DiscountManager(string filePath)
        {
            _filePath = filePath;
            LoadFromJson();
        }

        public IReadOnlyList<IDiscountStrategy> GetAll() => _discounts;

        // JSON model
        private class DiscountJsonModel
        {
            public string BaseName { get; set; }
            public int Percent { get; set; }
        }

        // ---------------- LOAD ----------------

        private void LoadFromJson()
        {
            _discounts.Clear();

            // Always add NoDiscount first
            _discounts.Add(new NoDiscount());

            if (!File.Exists(_filePath))
                return;

            var json = File.ReadAllText(_filePath);
            var items = JsonSerializer.Deserialize<List<DiscountJsonModel>>(json);

            if (items == null)
                return;

            foreach (var item in items)
                _discounts.Add(new CustomDiscount(item.BaseName, item.Percent));
        }

        // ---------------- SAVE ----------------

        private void SaveToJson()
        {
            // Save ONLY custom discounts (skip index 0)
            var list = _discounts
                .Skip(1)
                .Select(d => new DiscountJsonModel
                {
                    BaseName = ((CustomDiscount)d).BaseName,
                    Percent = ((CustomDiscount)d).Percent
                })
                .ToList();

            var json = JsonSerializer.Serialize(list, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(_filePath, json);
        }

        public void SaveChanges()
        {
            SaveToJson();
            Console.WriteLine("Discounts saved.");
        }

        // ---------------- DISPLAY ----------------

        public void DisplayDiscounts()
        {
            Console.WriteLine("\n--- Discounts ---");

            for (int i = 0; i < _discounts.Count; i++)
                Console.WriteLine($"{i + 1}. {_discounts[i].Name}");
        }

        // ---------------- CRUD ----------------

        public void AddDiscount()
        {
            Console.Write("Discount name: ");
            string baseName = Console.ReadLine()!;

            int percent = ReadInt("Percentage (0-100): ");

            _discounts.Add(new CustomDiscount(baseName, percent));

            Console.WriteLine("Discount added (not saved).");
        }

        public void EditDiscount()
        {
            DisplayDiscounts();
            int index = ReadMenuChoice("Select discount to edit: ", 1, _discounts.Count) - 1;

            if (index == 0)
            {
                Console.WriteLine("You cannot edit the 'No Discount' option.");
                return;
            }

            Console.Write("New name: ");
            string baseName = Console.ReadLine()!;

            int percent = ReadInt("New percentage: ");

            _discounts[index] = new CustomDiscount(baseName, percent);

            Console.WriteLine("Discount updated (not saved).");
        }

        public void RemoveDiscount()
        {
            DisplayDiscounts();
            int index = ReadMenuChoice("Select discount to delete: ", 1, _discounts.Count) - 1;

            if (index == 0)
            {
                Console.WriteLine("You cannot delete the 'No Discount' option.");
                return;
            }

            _discounts.RemoveAt(index);

            Console.WriteLine("Discount removed (not saved).");
        }
    }
}