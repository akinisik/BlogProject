using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Blog.API.Models;
using Blog.API.Services.IServices;
using Blog.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IUserService _userService;
        private UserManager<User> _userManager;
        public TokenController(IUserService userService, UserManager<User> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }

        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            var user = await _userManager.FindByEmailAsync(loginModel.Email);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                ClaimsIdentity identity = GetClaimsIdentity(user, userRoles.ToList());
                return Ok(new
                {
                    UserId = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Token = GetJwtToken(identity)
                });
            }
            return BadRequest();
        }

        private ClaimsIdentity GetClaimsIdentity(User user, List<string> userRoles)
        {
            // Here we can save some values to token.
            // For example we are storing here user id and email
            Claim[] claims = new[]
            {
        new Claim(ClaimTypes.Name, user.Id.ToString()),
        new Claim(ClaimTypes.Email, user.Email)
    };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token");

            // Adding roles code
            // Roles property is string collection but you can modify Select code if it it's not
            claimsIdentity.AddClaims(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));
            return claimsIdentity;
        }

        private string GetJwtToken(ClaimsIdentity identity)
        {
            var claims = identity.Claims;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("11111111111111111111111111111111111111111111111111111111"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(1);

            var token = new JwtSecurityToken(
                "JwtRoleBasedAuth",
                "JwtRoleBasedAuth",
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}