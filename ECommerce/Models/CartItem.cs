using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Models
{
    public class CartItem
    {
        public Product Product { get; }
        public int Quantity { get; }

        public CartItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }

        public decimal LineTotal => Product.Price * Quantity;
    }
}