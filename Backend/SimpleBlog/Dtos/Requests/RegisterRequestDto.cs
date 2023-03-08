using System.ComponentModel.DataAnnotations;

namespace SimpleBlog.Dtos.Requests
{
    using Microsoft.AspNetCore.SignalR;

    using Newtonsoft.Json;

    /// <summary>
    /// Дто запроса на регистрацию пользователя.
    /// </summary>
    public class RegisterRequestDto
    {
        /// <summary>
        /// Электронная почта.
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        /// Пароль.
        /// </summary>
        [JsonProperty("password")]
        public string Password { get; set; }

        /// <summary>
        /// Имя.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
