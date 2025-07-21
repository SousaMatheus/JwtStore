using AspNetJWT.Configuration;
using AspNetJWT.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AspNetJWT.Services
{
    public class TokenService
    {
        public string Create (User user)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(TokenConfiguration.PrivateKey);            

            var credentials =  new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var descriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = credentials,
                Subject = GenerateClaims(user),
                Expires = DateTime.UtcNow.AddHours(12),
            };

            
            var token = jwtHandler.CreateToken(descriptor);

            return jwtHandler.WriteToken(token);
        }

        private static ClaimsIdentity GenerateClaims(User user)
        {
            var claimsIdentity = new ClaimsIdentity();

            claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, user.Email));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.GivenName, user.Nome));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
            claimsIdentity.AddClaim(new Claim("id", user.Id.ToString()));
            claimsIdentity.AddClaim(new Claim("image", user.Imagem));

            foreach(var role in user.Roles)
            {
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role));
            }

            return claimsIdentity;
        }
    }
}
