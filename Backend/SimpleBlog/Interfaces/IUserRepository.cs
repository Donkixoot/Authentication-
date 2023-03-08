namespace SimpleBlog.Interfaces
{
    using Data.Models;

    public interface IUserRepository
    {
        /// <summary>
        ///     Получить пользователя по электронной почте.
        /// </summary>
        /// <param name="email"> Электронная почте. </param>
        /// <returns> Пользователь. </returns>
        Task<User?> GetUserByEmailAsync(string email);

        /// <summary>
        ///     Добавить пользователя.
        /// </summary>
        /// <param name="user"> Пользователь. </param>
        Task AddUserAsync(User user);
    }
}