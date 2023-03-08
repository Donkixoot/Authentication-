namespace SimpleBlog.Dtos
{
    /// <summary>
    /// Дто поста.
    /// </summary>
    public class PostDto
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Текст.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Почта пользователя.
        /// </summary>
        public string UserEmail { get; set; }
    }
}
