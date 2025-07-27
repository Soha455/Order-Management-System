using Domain.Contracts;
using Domain.Models;
using ServicesAbstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IInvoiceRepository _invoiceRepository;

        public OrderService(IOrderRepository orderRepository,
                            IProductRepository productRepository,
                            IInvoiceRepository invoiceRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _invoiceRepository = invoiceRepository;
        }

        public async Task<bool> ValidateOrderAsync(Order order)
        {
            foreach (var item in order.OrderItems)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);
                if (product == null || product.Stock < item.Quantity)
                    return false;
            }
            return true;
        }

        public async Task ApplyDiscountsAsync(Order order)
        {
            foreach (var item in order.OrderItems)
            {
                if (item.Quantity >= 10)
                    item.Discount = 0.10m;
                else if (item.Quantity >= 5)
                    item.Discount = 0.05m;
                else
                    item.Discount = 0;

                item.UnitPrice = item.UnitPrice * (1 - item.Discount);
            }
        }

        public async Task UpdateStockAsync(Order order)
        {
            foreach (var item in order.OrderItems)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);
                if (product != null)
                {
                    product.Stock -= item.Quantity;
                    await _productRepository.UpdateAsync(product);
                }
            }
        }

        public async Task<Invoice> GenerateInvoiceAsync(Order order)
        {
            var invoice = new Invoice
            {
                OrderId = order.Id,
                InvoiceDate = DateTime.UtcNow,
                TotalAmount = order.TotalAmount
            };

            await _invoiceRepository.AddAsync(invoice);
            return invoice;
        }

        public Task ProcessPaymentAsync(Order order)
        {
            //var result = await _paymentGatewayService.ChargeAsync(order);

            //if (result.IsSuccess)
            //{
            //    order.Status = "Paid";
            //}
            //else
            //{
            //    order.Status = "Payment Failed";
            //    throw new Exception("Payment processing failed.");
            //}

            //await _orderRepository.UpdateAsync(order);
        
            order.Status = "Paid";
            return Task.CompletedTask;
        }
    }
}
