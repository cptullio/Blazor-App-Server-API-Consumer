using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace MyAPP.API.Services
{
    public interface ITokenService
    {
       SymmetricSecurityKey Key { get; }

        string GenerateToken(string userName, string role);
    }

    public class TokenService : ITokenService
    {

        protected IConfiguration Configuration { get; private set; }
        public SymmetricSecurityKey Key { get; private set; }
     
        public TokenService(IConfiguration configuration)
        {
            Configuration = configuration;
            var key = System.Text.Encoding.ASCII.GetBytes(Configuration.GetValue<String>("secret"));
            Key = new SymmetricSecurityKey(key);
        }

        public string GenerateToken(string userName, string role)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.Role, role)
                
            };
            var jwt = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(Key,SecurityAlgorithms.HmacSha256Signature));
            var result = new JwtSecurityTokenHandler().WriteToken(jwt);

            return result;
        }

    }
}