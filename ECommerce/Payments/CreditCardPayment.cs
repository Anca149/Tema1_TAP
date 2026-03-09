using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Payments
{
    public class CreditCardPayment : IPaymentMethod
    {
        public string Name => "Credit Card";

        public void Pay(decimal amount)
        {
            Console.WriteLine($"Paid {amount} RON by credit card.");
        }
    }
}