using MyAspNetApp.Entities;

namespace MyAspNetApp.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUser();
        Task<User> CreateUser(User user);
        Task<User?> UpdateUser(User user);
        Task<User?> FindUserByEmail(string Email);
        Task<User?> FindUserById(int Id);

    }
}
