using ECommerce.Models;

namespace ECommerce.Discounts
{
    public class NoDiscount : IDiscountStrategy
    {
        public string Name => "No Discount";

        public decimal CalculateDiscount(ShoppingCart cart) => 0m;
    }
}