using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Payments
{
    public class CashOnDeliveryPayment : IPaymentMethod
    {
        public string Name => "Cash on Delivery";

        public void Pay(decimal amount)
        {
            Console.WriteLine($"Will pay {amount} RON on delivery.");
        }
    }
}