using System;
using System.Collections.Generic;
using System.Text;
using ECommerce.Models;

namespace ECommerce.Shipping
{
    public interface IShippingMethod
    {
        string Name { get; }
        decimal GetShippingCost(ShoppingCart cart);
    }
}