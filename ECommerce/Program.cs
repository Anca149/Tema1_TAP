using System;
using ECommerce.Repositories;
using ECommerce.Users;
using ECommerce.Discounts;
using static ECommerce.Helpers;

namespace ECommerce
{
    class Program
    {
        static void Main()
        {
            var productRepo = new JsonProductRepository("products.json");
            var discountManager = new DiscountManager("discounts.json");

            while (true)
            {
                Console.WriteLine("\n=== E-Commerce ===");
                Console.WriteLine("1. Regular User");
                Console.WriteLine("2. Operator");
                Console.WriteLine("0. Exit");

                var choice = Console.ReadLine();

                IUser? user = choice switch
                {
                    "1" => new RegularUser("Customer", productRepo, discountManager),
                    "2" => new OperatorUser("Operator", productRepo, discountManager),
                    _ => null
                };

                if (choice == "0" || user == null)
                    return;

                user.ShowMenu();
            }
        }
    }
}