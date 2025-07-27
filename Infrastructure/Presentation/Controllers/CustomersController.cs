using Domain.Contracts;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        
        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] Customer customer)
        {
            if (customer == null)
                return BadRequest("Please provide valid data");

            await _customerRepository.AddAsync(customer);
            return CreatedAtAction(nameof(GetCustomerOrders), new { customerId = customer.Id }, customer);
        }


        
        [HttpGet("{customerId}/orders")]
        public async Task<IActionResult> GetCustomerOrders(int customerId)
        {
            var customer = await _customerRepository.GetCustomerWithOrdersAsync(customerId);
            if (customer == null)
                return NotFound("Customer not found");

            return Ok(customer.Orders);
        }
    }
}
