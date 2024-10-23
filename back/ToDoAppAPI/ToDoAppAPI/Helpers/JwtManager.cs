using Microsoft.IdentityModel.Tokens;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDoAppAPI.Models;

namespace ToDoAppAPI.Helpers
{
    public class JwtManager
    {
        private readonly IConfiguration _configuration;
        public JwtManager(IConfiguration cfg) 
        {
            _configuration = cfg;
        }

        public string CreateJWT(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Secret").Value));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

            JwtSecurityToken token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.UtcNow.AddDays(1), // 24 hours lifespan token
                    signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool IsJWTValid(string jwtToken)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            if (!tokenHandler.CanReadToken(jwtToken))
            {
                return false;
            }

            JwtSecurityToken token = tokenHandler.ReadJwtToken(jwtToken);

            long expiration = (long)(token.Payload.Expiration != null ? token.Payload.Expiration : 0);

            return expiration != 0 && DateTime.FromBinary(expiration).CompareTo(DateTime.UtcNow) > 0;
        }
    }
}
