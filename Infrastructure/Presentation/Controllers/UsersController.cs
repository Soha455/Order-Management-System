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
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            var existing = await _userRepository.GetByUsernameAsync(user.Username);
            if (existing != null)
                return BadRequest("Username already taken.");

            await _userRepository.AddAsync(user);
            return Ok("User registered.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User login)
        {
            var user = await _userRepository.GetByUsernameAsync(login.Username);
            if (user == null || user.PasswordHash != login.PasswordHash)
                return Unauthorized("Invalid credentials.");

            // For now, return a mock token
            return Ok(new { Token = "mock-jwt-token", Message = "Login successful" });
        }
    }
}
