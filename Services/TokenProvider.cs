using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;
using Zadatak1.Models;
using Microsoft.AspNetCore.Identity;

namespace Zadatak1.Services
{
    public class TokenProvider(IConfiguration configuration)
    {
        public string Create(User user)
        {
            if(user.Id != null)
            {
                string secretKey = configuration["Jwt:Secret"];
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity([
                        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                        new Claim(JwtRegisteredClaimNames.Email, user.Email),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                        new Claim(ClaimTypes.Role, user.Purpose.ToString())
                    ]),
                    Expires = DateTime.UtcNow.AddMinutes(configuration.GetValue<int>("Jwt:ExpirationInMinutes")),
                    SigningCredentials = credentials,
                    Issuer = configuration["Jwt:Issuer"],
                    Audience = configuration["Jwt:Audience"]
                };

                var handler = new JsonWebTokenHandler();

                string token = handler.CreateToken(tokenDescriptor);
                return token;

            }
            else { 
                return null;
            }
            
        }
    }
}

