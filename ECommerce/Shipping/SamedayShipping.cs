using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ECommerce.Shipping
{
    public class SamedayShipping : IShippingMethod
    {
        public string Name => "Sameday";

        public decimal GetShippingCost(ShoppingCart cart) => 20m;
    }
}
