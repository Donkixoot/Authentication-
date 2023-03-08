using SimpleBlog.Data.Models;

namespace SimpleBlog.Interfaces
{
    public interface IPostRepository
    {
        /// <summary>
        /// Получить все посты пользователя.
        /// </summary>
        /// <returns> Список постов. </returns>
        Task<List<Post>> GetAllUserPosts(int userId);

        /// <summary>
        /// Получить пост по идентификатору.
        /// </summary>
        /// <param name="postId"> Идентификатор поста. </param>
        /// <returns> Пост. </returns>
        Task<Post?> GetPostByIdAsync(int postId);

        /// <summary>
        /// Добавить пост.
        /// </summary>
        /// <param name="post"> Пост. </param>
        Task AddOrUpdatePostAsync(Post post);

        /// <summary>
        /// Удалить пост.
        /// </summary>
        /// <param name="post"> Пост. </param>
        Task DeletePostAsync(Post post);
    }
}
