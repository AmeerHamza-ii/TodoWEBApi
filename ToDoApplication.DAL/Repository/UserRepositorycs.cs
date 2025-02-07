using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoApplication.DAL.Data;
using ToDoApplication.DAL.Models;
using ToDoApplication.DAL.Repository.Interfaces;

namespace ToDoApplication.DAL.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly StudentDbContext _context;

        public UserRepository(StudentDbContext context)
        {
            _context = context;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users
                .Where(u => u.Id == id && u.IsDeleted.HasValue && !u.IsDeleted.Value)
                .FirstOrDefaultAsync();
        }

        public async System.Threading.Tasks.Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users
                .Where(u => u.Email == email)
                .FirstOrDefaultAsync();
        }

        public async System.Threading.Tasks.Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

    }

}
