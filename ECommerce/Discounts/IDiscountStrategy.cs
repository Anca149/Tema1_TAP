using System;
using System.Collections.Generic;
using System.Text;
using ECommerce.Models;

namespace ECommerce.Discounts
{
    public interface IDiscountStrategy
    {
        string Name { get; }
        decimal CalculateDiscount(ShoppingCart cart);
    }
}