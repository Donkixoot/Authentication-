using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace SimpleBlog.Services
{
    /// <summary>
    /// Генератор JWT.
    /// </summary>
    public class JwtGenerator
    {
        /// <summary>
        /// Приватный ключ.
        /// </summary>
        private readonly RsaSecurityKey _privateKey;

        /// <summary>
        /// Генератор JWT.
        /// </summary>
        /// <param name="privateKey"> Приватный ключ. </param>
        public JwtGenerator(string? privateKey)
        {
            var privateRSA = RSA.Create();
            privateRSA.ImportRSAPrivateKey(Convert.FromBase64String(privateKey), out _);
            _privateKey = new RsaSecurityKey(privateRSA);
        }

        /// <summary>
        /// Создать токен доступа.
        /// </summary>
        /// <param name="email"> Почта пользователя. </param>
        /// <returns> Токен доступа. </returns>
        public string CreateAuthToken(string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = "myApi",
                Issuer = "AuthService",
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Sid, email)
                }),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(_privateKey, SecurityAlgorithms.RsaSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}