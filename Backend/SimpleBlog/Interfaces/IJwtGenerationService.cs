namespace SimpleBlog.Interfaces
{
    public interface IJwtGenerationService
    {
        /// <summary>
        /// Создать токен доступа.
        /// </summary>
        /// <param name="email"> Почта пользователя. </param>
        /// <returns> Токен доступа. </returns>
        public string CreateAuthToken(string email);
    }
}
