using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PointOfSale.Server.Models;
using PointOfSale.DAL.Domains;
using PointOfSale.DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SignInManager<SahlUserIdentity> _signInManager;

        public LoginController(IConfiguration configuration,
                               SignInManager<SahlUserIdentity> signInManager)
        {
            _configuration = configuration;
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, login.RememberMe, false);

            if (!result.Succeeded) return BadRequest(new LoginResult { Successful = false, Error = "Username and password are invalid." });
            var user =await _signInManager.UserManager.FindByNameAsync(login.Email);
        //    var claims = new[]
        //    {
        //    new Claim(ClaimTypes.Name, login.Email),
        //    new Claim("UserId", user.Id)
        //};

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecurityKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var expiry = DateTime.Now.AddDays(Convert.ToInt32(_configuration["JwtExpiryInDays"]));
            var claims = await GetClaims(user);
            var token = GenerateSecurityToken(claims);
            //var token2 = new JwtSecurityToken(
            //    _configuration["JwtIssuer"],
            //    _configuration["JwtAudience"],
            //    claims,
            //    expires: expiry,
            //    signingCredentials: creds
            //);

            return Ok(new LoginResult { Successful = true, Token = token });
        }
        private string GenerateSecurityToken(List<Claim> claims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var expire = System.DateTime.UtcNow.AddDays(1);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecurityKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expire,
                SigningCredentials = creds,
                Audience = _configuration["JwtAudience"],
                Issuer = _configuration["JwtIssuer"]
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private async Task<List<Claim>> GetClaims(SahlUserIdentity user)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim("UserId", user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email),
        };

            // add roles
            //var roleList = await userService.UserRoles(user.Email);
            //foreach (var role in roleList)
            //{
            //    var claim = new Claim(ClaimTypes.Role, role.Role);
            //    claims.Add(claim);
            //}

            return claims;
        }
    }
}
