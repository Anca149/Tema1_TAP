using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Payments
{
    public interface IPaymentMethod
    {
        string Name { get; }
        void Pay(decimal amount);
    }
}