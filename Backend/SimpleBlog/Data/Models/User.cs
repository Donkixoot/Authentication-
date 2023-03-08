namespace SimpleBlog.Data.Models
{
    /// <summary>
    /// Пользователь.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Электронная почта.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Имя.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Хэш пароля.
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        /// Посты.
        /// </summary>
        public List<Post> Posts { get; set; }
    }
}