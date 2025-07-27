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
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productRepository.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetById(int productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
                return NotFound("Product not found.");

            return Ok(product);
        }


        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            await _productRepository.AddAsync(product);
            return Ok("Product added.");
        }


        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateProduct(int productId, [FromBody] Product updated)
        {
            var existing = await _productRepository.GetByIdAsync(productId);
            if (existing == null)
                return NotFound("Product not found.");

            existing.Name = updated.Name;
            existing.Price = updated.Price;
            existing.Stock = updated.Stock;

            await _productRepository.UpdateAsync(existing);
            return Ok("Product updated.");
        }
    }
}
