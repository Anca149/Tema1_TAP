using ECommerce.Payments;
using ECommerce.Shipping;

namespace ECommerce.Models
{
    public class Order
    {
        public int Id { get; }
        private static int _nextId = 1;
        public ShoppingCart Cart { get; }
        public IPaymentMethod PaymentMethod { get; }
        public IShippingMethod ShippingMethod { get; }
        public decimal DiscountAmount { get; }
        public decimal ShippingCost { get; }
        public decimal TotalToPay { get; }

        public Order(
            ShoppingCart cart,
            IPaymentMethod paymentMethod,
            IShippingMethod shippingMethod,
            decimal discountAmount,
            decimal shippingCost)
        {
            Id = _nextId++;
            Cart = cart;
            PaymentMethod = paymentMethod;
            ShippingMethod = shippingMethod;
            DiscountAmount = discountAmount;
            ShippingCost = shippingCost;

            TotalToPay = cart.GetTotal() - DiscountAmount + ShippingCost;
        }

        public void Process()
        {
            System.Console.WriteLine($"\n--- Order #{Id} ---");
            System.Console.WriteLine($"Subtotal: {Cart.GetTotal()} RON");
            System.Console.WriteLine($"Discount: {DiscountAmount} RON");
            System.Console.WriteLine($"Shipping: {ShippingCost} RON");
            System.Console.WriteLine($"Total to pay: {TotalToPay} RON");
            PaymentMethod.Pay(TotalToPay);
            System.Console.WriteLine($"Shipping via {ShippingMethod.Name}");
        }
    }
}