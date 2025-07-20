using AspNetJWT.Configuration;
using AspNetJWT.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace AspNetJWT.Services
{
    public class TokenServices
    {
        public string Create (User user)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(TokenConfiguration.PrivateKey);            

            var credentials =  new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var descriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = credentials,
                Subject = ,
                Expires = DateTime.UtcNow.AddHours(12),
            };

            var token = jwtHandler.CreateToken(descriptor);

            return jwtHandler.WriteToken(token);
        }
    }
}
