using Domain.Contracts;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstractions.Enums;
using ServicesAbstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderService orderService, IOrderRepository orderRepository)
        {
            _orderService = orderService;
            _orderRepository = orderRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            if (order == null || order.OrderItems == null || !order.OrderItems.Any())
                return BadRequest("Order must include at least one item.");

            bool isValid = await _orderService.ValidateOrderAsync(order);
            if (!isValid)
                return BadRequest("Order validation failed. Check product availability.");

            await _orderService.ApplyDiscountsAsync(order);
            await _orderRepository.AddAsync(order);
            await _orderService.UpdateStockAsync(order);
           // await _orderService.ProcessPaymentAsync(order, PaymentMethod.CreditCard); // default method

            var invoice = await _orderService.GenerateInvoiceAsync(order);

            return Ok(new { Message = "Order placed successfully.", Invoice = invoice });
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrder(int orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null)
                return NotFound("Order not found.");

            return Ok(order);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderRepository.GetAllAsync();
            return Ok(orders);
        }

        [HttpPut("{orderId}/status")]
        public async Task<IActionResult> UpdateStatus(int orderId, [FromBody] string status)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null)
                return NotFound("Order not found.");

            order.Status = status;
            await _orderRepository.UpdateAsync(order);
            return Ok("Order status updated.");
        }
    }
}
