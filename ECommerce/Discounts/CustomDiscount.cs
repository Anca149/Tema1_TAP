using ECommerce.Models;

namespace ECommerce.Discounts
{
    public class CustomDiscount : IDiscountStrategy
    {
        public string BaseName { get; }
        public int Percent { get; }

        public string Name => $"{BaseName} ({Percent}% off)";

        public CustomDiscount(string baseName, int percent)
        {
            BaseName = baseName;
            Percent = percent;
        }

        public decimal CalculateDiscount(ShoppingCart cart)
        {
            return cart.GetTotal() * (Percent / 100m);
        }
    }
}