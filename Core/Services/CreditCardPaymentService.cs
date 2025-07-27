using Domain.Models;
using ServicesAbstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CreditCardPaymentService : IPaymentService
    {
        public Task<bool> ProcessPaymentAsync(Order order)
        {
            
            Console.WriteLine("Processing credit card payment...");
            return Task.FromResult(true);
        }
    }

}
