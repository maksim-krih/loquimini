using Loquimini.Manager.Interfaces;
using Loquimini.Model.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;

namespace Loquimini.Manager
{
    public class TokenManager : ITokenManager
    {
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        private readonly IConfiguration _config;

        public TokenManager(IConfiguration config)
        {
            _config = config;
        }

        public string CreateToken(User user)
        {
            var token = new JwtSecurityToken(
                claims: user.GetUserClaims(),
                expires: DateTime.Now.AddDays(int.Parse(_config["Authorization:TokenExpirationIn"]))
            );

            return _jwtSecurityTokenHandler.WriteToken(token);
        }

        public string GenerateToken(int size = 32)
        {
            var randomNumber = new byte[size];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
