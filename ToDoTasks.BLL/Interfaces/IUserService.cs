using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApplication.Core.DTOs;
using ToDoApplication.DAL.DTO;
using ToDoApplication.DAL.Models;

namespace ToDoTasks.BLL.Interfaces
{
    public interface IUserService
    {
        Task<CreateUserResponse> CreateUserAsync(CreateUserRequest createUserRequest);
        Task<bool> VerifyOtpAsync(int userId, OtpVerificationRequest otpRequest);
        Task<User> ValidateUserAsync(string email, string password);

        Task<UserResponse> UpdateUserAsync(int userId, UpdateUserRequest request);
        Task<bool> SoftDeleteUserAsync(int userId);
        Task<List<UserResponse>> GetUsersAsync(int pageNumber, int pageSize);

    }
}
