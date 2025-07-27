using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServicesAbstractions
{
    public interface IOrderService
    {
        Task<bool> ValidateOrderAsync(Order order);
        Task ApplyDiscountsAsync(Order order);
        Task UpdateStockAsync(Order order);
        Task<Invoice> GenerateInvoiceAsync(Order order);
        Task ProcessPaymentAsync(Order order);
    }
}
