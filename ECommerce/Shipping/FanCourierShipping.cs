using System;
using System.Collections.Generic;
using System.Text;
using ECommerce.Models;

namespace ECommerce.Shipping
{
    public class FanCourierShipping : IShippingMethod
    {
        public string Name => "Fan Courier";

        public decimal GetShippingCost(ShoppingCart cart) => 25m;
    }
}