using DotCruz.CoreAuth.Common.Settings;
using DotCruz.CoreAuth.Domain.Interfaces.Security.Tokens;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DotCruz.CoreAuth.Infrastructure.Security.Tokens.Access.Generator
{
    public class JwtTokenGenerator : JwtTokenHandler, IAccessTokenGenerator
    {
        private readonly JwtTokenSettings _jwtTokenSettings;

        public JwtTokenGenerator(IOptions<JwtTokenSettings> jwtTokenSettings)
        {
            _jwtTokenSettings = jwtTokenSettings.Value;
        }

        public string Generate(Guid userId)
        {
            var claims = new List<Claim>()
            {
                new(ClaimTypes.Sid, userId.ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_jwtTokenSettings.ExpirationTimeMinutes),
                SigningCredentials = new SigningCredentials(SecurityKey(_jwtTokenSettings.SigningKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(securityToken);
        }
    }
}
