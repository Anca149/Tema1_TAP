using System;
using System.Collections.Generic;
using ECommerce.Models;
using ECommerce.Repositories;
using ECommerce.Discounts;
using ECommerce.Payments;
using ECommerce.Shipping;
using static ECommerce.Helpers;

namespace ECommerce.Users
{
    public class RegularUser : BaseUser
    {
        private readonly ShoppingCart _cart = new();
        private readonly DiscountManager _discountManager;

        public RegularUser(string name, IProductRepository repo, DiscountManager discountManager)
            : base(name, repo)
        {
            _discountManager = discountManager;
        }

        public override void ShowMenu()
        {
            while (true)
            {
                Console.WriteLine("\n--- Regular User Menu ---");
                Console.WriteLine("1. View products");
                Console.WriteLine("2. Add to cart");
                Console.WriteLine("3. View cart");
                Console.WriteLine("4. Remove item from cart");
                Console.WriteLine("5. Checkout");
                Console.WriteLine("0. Logout");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": ViewProducts(); break;
                    case "2": AddToCart(); break;
                    case "3": ViewCart(); break;
                    case "4": RemoveFromCart(); break;
                    case "5": Checkout(); break;
                    case "0": return;
                }
            }
        }

        private void AddToCart()
        {
            int id = ReadInt("Product ID: ");
            var product = _repo.GetById(id);

            if (product == null)
            {
                Console.WriteLine("Invalid product.");
                return;
            }

            int qty = ReadInt("Quantity: ");
            _cart.AddItem(product, qty);

            Console.WriteLine("Added to cart.");
        }

        private void ViewCart()
        {
            _cart.Display();
        }

        private void RemoveFromCart()
        {
            if (_cart.IsEmpty)
            {
                Console.WriteLine("Cart is empty.");
                return;
            }

            int id = ReadInt("Enter product ID to remove: ");

            if (_cart.RemoveItem(id))
                Console.WriteLine("Item removed.");
            else
                Console.WriteLine("No item with that ID found in cart.");
        }

        private void Checkout()
        {
            if (_cart.IsEmpty)
            {
                Console.WriteLine("Cart is empty.");
                return;
            }

            var discounts = _discountManager.GetAll();

            Console.WriteLine("\nChoose discount:");
            for (int i = 0; i < discounts.Count; i++)
                Console.WriteLine($"{i + 1}. {discounts[i].Name}");

            int dIndex = ReadMenuChoice("Choice: ", 1, discounts.Count) - 1;
            var discount = discounts[dIndex];

            var payments = new List<IPaymentMethod>
            {
                new CreditCardPayment(),
                new CashOnDeliveryPayment()
            };

            Console.WriteLine("\nChoose payment:");
            for (int i = 0; i < payments.Count; i++)
                Console.WriteLine($"{i + 1}. {payments[i].Name}");

            int pIndex = ReadMenuChoice("Choice: ", 1, payments.Count) - 1;
            var payment = payments[pIndex];

            var shippings = new List<IShippingMethod>
            {
                new CargusShipping(),
                new SamedayShipping(),
                new FanCourierShipping()
            };

            Console.WriteLine("\nChoose shipping:");
            for (int i = 0; i < shippings.Count; i++)
                Console.WriteLine($"{i + 1}. {shippings[i].Name}");

            int sIndex = ReadMenuChoice("Choice: ", 1, shippings.Count) - 1;
            var shipping = shippings[sIndex];

            var order = new Order(
                cart: _cart,
                paymentMethod: payment,
                shippingMethod: shipping,
                discountAmount: discount.CalculateDiscount(_cart),
                shippingCost: shipping.GetShippingCost(_cart));

            order.Process();

            _cart.Clear();
            Console.WriteLine("Cart has been reset.");
        }
    }
}