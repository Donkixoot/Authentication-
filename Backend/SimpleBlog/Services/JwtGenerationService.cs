namespace SimpleBlog.Services
{
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Security.Cryptography;

    using Microsoft.IdentityModel.Tokens;

    using SimpleBlog.Interfaces;

    /// <summary>
    /// Генератор JWT.
    /// </summary>
    public class JwtGenerationService : IJwtGenerationService
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Генератор JWT.
        /// </summary>
        /// <param name="configuration"> Конфигурация приложения. </param>
        public JwtGenerationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <inheritdoc />
        public string CreateAuthToken(string email)
        {
            var privateRsa = RSA.Create();
            privateRsa.ImportRSAPrivateKey(Convert.FromBase64String(_configuration.GetValue<string>("JwtPrivateKey")!), out _);
            var privateKey = new RsaSecurityKey(privateRsa);
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
                SigningCredentials = new SigningCredentials(privateKey, SecurityAlgorithms.RsaSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}