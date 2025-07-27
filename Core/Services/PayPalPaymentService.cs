using Domain.Models;
using ServicesAbstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PayPalPaymentService : IPaymentService
    {
        public Task<bool> ProcessPaymentAsync(Order order)
        {
            
            Console.WriteLine("Processing PayPal payment...");
            return Task.FromResult(true);
        }
    }

}
