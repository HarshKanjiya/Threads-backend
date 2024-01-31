using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserAuthenticationManager.Model;

namespace UserAuthenticationManager
{
    public class JwtTokenHandler
    {
        public const string JWT_SECURITY_KEY = "qwqwqwasaqerfsgrfggrgttysfdadsdfgth";
        public const int JWT_VALIDITY_MIN = 20;

        public TokenResponseDTO GenerateJwtToken(GenerateTokenRequestDTO req)
        {
            var ExpiryTime = DateTime.Now.AddMinutes(JWT_VALIDITY_MIN);
            var TokenKey = Encoding.UTF8.GetBytes(JWT_SECURITY_KEY);
            var claims = new ClaimsIdentity(new List<Claim>
            {
                new Claim (JwtRegisteredClaimNames.Name,req.UserName),
                new Claim (ClaimTypes.Name,req.Role),
            });

            var signingCred = new SigningCredentials(
                new SymmetricSecurityKey(TokenKey),
                SecurityAlgorithms.HmacSha256Signature
                );

            var tokenDiscriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = ExpiryTime,
                SigningCredentials = signingCred

            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDiscriptor);
            var token = tokenHandler.WriteToken(securityToken);

            return new TokenResponseDTO()
            {
                Token = token,
                ExpiresIn = (int)ExpiryTime.Subtract(DateTime.Now).TotalSeconds,
                UserName = req.UserName
            };

        }

    }
}
