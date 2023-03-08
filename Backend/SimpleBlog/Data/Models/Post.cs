namespace SimpleBlog.Data.Models
{
    /// <summary>
    ///   Пост.
    /// </summary>
    public class Post
    {
        /// <summary>
        ///     Идентификатор.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Заголовок.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     Текст.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        ///     Идентификатор пользователя.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        ///     Пользователь.
        /// </summary>
        public User User { get; set; }
    }
}