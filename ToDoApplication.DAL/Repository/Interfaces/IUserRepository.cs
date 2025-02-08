using System;
using ToDoApplication.DAL.Models;

namespace ToDoApplication.DAL.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByEmailAsync(string email);
        System.Threading.Tasks.Task AddAsync(User user);
        Task<User> CreateUserAsync(User user);

        System.Threading.Tasks.Task<User> GetByIdAsync(int userId);
        System.Threading.Tasks.Task UpdateAsync(User user);
        Task<List<User>> GetUsersAsync(int pageNumber, int pageSize);
    }

}
