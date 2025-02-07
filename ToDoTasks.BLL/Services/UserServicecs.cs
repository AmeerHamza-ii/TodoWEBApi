using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApplication.DAL.DTO;
using ToDoApplication.DAL.Models;
using ToDoApplication.DAL.Repository.Interfaces;
using ToDoTasks.BLL.Interfaces;

namespace ToDoTasks.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IOtpRepository _otpRepository;

        public UserService(IUserRepository userRepository, IOtpRepository otpRepository)
        {
            _userRepository = userRepository;
            _otpRepository = otpRepository;
        }

        public async Task<CreateUserResponse> CreateUserAsync(CreateUserRequest createUserRequest)
        {
            var user = new User
            {
                FirstName = createUserRequest.FirstName,
                LastName = createUserRequest.LastName,
                Email = createUserRequest.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(createUserRequest.Password),  // Encrypt the password
                RoleId = createUserRequest.RoleId
            };

            user = await _userRepository.CreateUserAsync(user);

            // Generate OTP and save it
            var otp = new Otp
            {
                UserId = user.Id,
                OtpCode = new Random().Next(100000, 999999).ToString(),
                ExpiresAt = DateTime.Now.AddMinutes(10)
            };
            await _otpRepository.CreateOtpAsync(otp);

            // Send OTP to the user (Email/SMS) (skipped for this implementation)

            return new CreateUserResponse
            {
                Id = user.Id,
                FirstName = user.FirstName,
                Email = user.Email
            };
        }

        public async Task<bool> VerifyOtpAsync(int userId, OtpVerificationRequest otpRequest)
        {
            var otp = await _otpRepository.GetOtpByUserIdAsync(userId);

            if (otp != null && otp.OtpCode == otpRequest.OtpCode && otp.ExpiresAt > DateTime.Now)
            {
                await _otpRepository.VerifyOtpAsync(otp);
                return true;
            }
            return false;
        }
        public async Task<User> ValidateUserAsync(string email, string password)
        {
            // Get the user by email
            var user = await _userRepository.GetByEmailAsync(email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                // Invalid email or password
                return null;
            }

            // Return the user if validation passes
            return user;
        }
    }
}
