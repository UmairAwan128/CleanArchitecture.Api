using CleanArchitecture.Application.Common;
using CleanArchitecture.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace CleanArchitecture.Infrastructure.Identity.Auth
{
    public class TokenBuilder : ITokenBuilder
    {
        private readonly IConfiguration _configuration;
        private readonly string _jwtIssuer;
        public TokenBuilder(IConfiguration configuration)
        {
            _configuration = configuration;
            _jwtIssuer = _configuration.GetValue<string>("ApplicationURLs:ApiUrl");
        }

        public string Build(string name, string role, DateTime expireDate)
        {
            var handler = new JwtSecurityTokenHandler();

            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Role, role));

            ClaimsIdentity identity = new ClaimsIdentity(
                new GenericIdentity(name, "Bearer"),
                claims
            );

            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _jwtIssuer,
                SigningCredentials = TokenAuthOption.SigningCredentials,
                Subject = identity,
                Expires = expireDate
            });

            return handler.WriteToken(securityToken);
        }
    }
}