using System;
using System.Collections.Generic;
using System.Text;
using ECommerce.Models;

namespace ECommerce.Shipping
{
    public class CargusShipping : IShippingMethod
    {
        public string Name => "Cargus";

        public decimal GetShippingCost(ShoppingCart cart) => 22m;
    }
}