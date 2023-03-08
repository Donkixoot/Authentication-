namespace SimpleBlog.Dtos.Requests
{
    /// <summary>
    /// Дто запроса на авторизацию через Google.
    /// </summary>
    public class GoogleLoginRequestDto
    {
        public string IdToken { get; set; }
    }
}
