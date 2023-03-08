namespace SimpleBlog.Dtos.Responses
{
    /// <summary>
    /// Дто ответа на авторизацию пользователя.
    /// </summary>
    public class AuthResponseDto
    {
        /// <summary>
        /// Токен доступа.
        /// </summary>
        public string AuthToken { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string UserName { get; set; }
    }
}
