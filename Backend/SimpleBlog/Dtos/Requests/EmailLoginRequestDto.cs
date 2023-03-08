namespace SimpleBlog.Dtos.Requests
{
    /// <summary>
    /// Дто запроса на авторизацию пользователя.
    /// </summary>
    public class EmailLoginRequestDto
    {
        /// <summary>
        /// Электронная почта.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Пароль.
        /// </summary>
        public string Password { get; set; }
    }
}
