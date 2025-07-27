using Microsoft.Extensions.DependencyInjection;
using ServicesAbstractions.Enums;
using ServicesAbstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PaymentServiceFactory : IPaymentServiceFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public PaymentServiceFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IPaymentService GetPaymentService(PaymentMethod method)
        {
            return method switch
            {
                PaymentMethod.CreditCard => _serviceProvider.GetRequiredService<CreditCardPaymentService>(),
                PaymentMethod.PayPal => _serviceProvider.GetRequiredService<PayPalPaymentService>(),
                _ => throw new NotImplementedException("Unsupported payment method")
            };
        }
    }

}
