using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Interfaces;
using Koleo.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;


namespace API.Services
{
    public class TokenService : ITokenService
    {
        private SymmetricSecurityKey key;

        public TokenService(IConfiguration config)
        {
            key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }
        public string CreateToken(User user)
        {
            Console.WriteLine("przed tokenem: " + user.Id);
            Console.WriteLine("token: " + user.Id.ToString());
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Name, user.Id.ToString()),
                new Claim("Role", user.Role)
            };
            
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}