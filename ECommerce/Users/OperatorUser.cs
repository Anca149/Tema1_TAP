using System;
using ECommerce.Repositories;
using ECommerce.Discounts;
using ECommerce.Models;

namespace ECommerce.Users
{
    public class OperatorUser : BaseUser
    {
        private readonly ProductManager _productManager;
        private readonly DiscountManager _discountManager;

        public OperatorUser(string name, IProductRepository repo, DiscountManager discountManager)
            : base(name, repo)
        {
            _productManager = new ProductManager(repo);
            _discountManager = discountManager;
        }

        public override void ShowMenu()
        {
            while (true)
            {
                Console.WriteLine("\n--- Operator Menu ---");
                Console.WriteLine("1. Manage products");
                Console.WriteLine("2. Manage discounts");
                Console.WriteLine("0. Logout");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": ManageProducts(); break;
                    case "2": ManageDiscounts(); break;
                    case "0": return;
                }
            }
        }

        private void ManageProducts()
        {
            while (true)
            {
                Console.WriteLine("\n--- Product Management ---");
                Console.WriteLine("1. View products");
                Console.WriteLine("2. Add product");
                Console.WriteLine("3. Edit product");
                Console.WriteLine("4. Delete product");
                Console.WriteLine("5. Save changes");
                Console.WriteLine("0. Back");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": _productManager.DisplayProducts(); break;
                    case "2": _productManager.AddProduct(); break;
                    case "3": _productManager.EditProduct(); break;
                    case "4": _productManager.DeleteProduct(); break;
                    case "5": _productManager.SaveChanges(); break;
                    case "0": return;
                }
            }
        }

        private void ManageDiscounts()
        {
            while (true)
            {
                Console.WriteLine("\n--- Discount Management ---");
                Console.WriteLine("1. View discounts");
                Console.WriteLine("2. Add discount");
                Console.WriteLine("3. Edit discount");
                Console.WriteLine("4. Delete discount");
                Console.WriteLine("5. Save changes");
                Console.WriteLine("0. Back");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": _discountManager.DisplayDiscounts(); break;
                    case "2": _discountManager.AddDiscount(); break;
                    case "3": _discountManager.EditDiscount(); break;
                    case "4": _discountManager.RemoveDiscount(); break;
                    case "5": _discountManager.SaveChanges(); break;
                    case "0": return;
                }
            }
        }
    }
}
